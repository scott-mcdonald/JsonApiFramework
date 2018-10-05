// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal static class UriExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static IEnumerable<IHypermediaPath> ParseDocumentSelfPath(this Uri url, IHypermediaContext hypermediaContext)
        {
            Contract.Requires(url               != null);
            Contract.Requires(hypermediaContext != null);

            // Create a URL path segment enumerator to enumerate over the URL path segments.
            var urlPathSegmentsFromUri = new List<string>(GetUrlPathSegments(url));

            // Remove any root path segments
            var uriBuilderConfiguration = hypermediaContext.GetUrlBuilderConfiguration(url);
            if (uriBuilderConfiguration.RootPathSegments != null)
            {
                var rootPathSegments      = uriBuilderConfiguration.RootPathSegments.SafeToReadOnlyList();
                var rootPathSegmentsCount = rootPathSegments.Count;

                urlPathSegmentsFromUri = urlPathSegmentsFromUri.SkipWhile((pathSegment, index) =>
                                                                {
                                                                    if (index >= rootPathSegmentsCount)
                                                                        return false;

                                                                    var rootPathSegment                   = rootPathSegments[index];
                                                                    var rootPathSegmentEqualToPathSegment = String.CompareOrdinal(rootPathSegment, pathSegment) == 0;
                                                                    return rootPathSegmentEqualToPathSegment;
                                                                })
                                                               .ToList();
            }

            var urlPathSegments = new List<string>();

            var serviceModel       = hypermediaContext.GetServiceModel();
            var homeDocumentExists = serviceModel.TryGetHomeResourceType(out var homeResourceType);
            if (homeDocumentExists)
            {
                var homeResourceTypeApiCollectionPathSegment = homeResourceType.HypermediaInfo.ApiCollectionPathSegment;
                if (String.IsNullOrEmpty(homeResourceTypeApiCollectionPathSegment))
                {
                    urlPathSegments.Add(String.Empty);
                }
            }

            urlPathSegments.AddRange(urlPathSegmentsFromUri);

            var urlPathSegmentsEnumerator = (IEnumerator<string>)urlPathSegments.GetEnumerator();

            // Parse the raw URL path for document self link path objects by
            // looking for the following:
            // 1. resource collection hypermedia path objects
            // 2. resource hypermedia path objects
            // 3. non-resource hypermedia path objects
            // 4. to-many resource collection hypermedia path objects
            // 5. to-many resource hypermedia path objects
            var documentSelfPath  = new List<IHypermediaPath>();
            var continueIterating = InitialIteration(serviceModel, urlPathSegmentsEnumerator, documentSelfPath);

            if (continueIterating && homeDocumentExists)
            {
                continueIterating = InitialIteration(serviceModel, urlPathSegmentsEnumerator, documentSelfPath);
            }

            while (continueIterating)
            {
                continueIterating = NextIteration(serviceModel, urlPathSegmentsEnumerator, documentSelfPath);
            }

            return documentSelfPath;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Parse Methods
        private static List<string> GetUrlPathSegments(Uri url)
        {
            Contract.Requires(url != null);

            // Skip the root path segments that are part of the URL builder
            // configuration that maybe are in the beginning of the raw URL path.
            var urlPathSegments = url.GetPathSegments().SafeToList();
            return urlPathSegments;
        }

        private static bool InitialIteration(IServiceModel serviceModel, IEnumerator<string> urlPathSegmentsEnumerator, ICollection<IHypermediaPath> documentSelfPath)
        {
            // Parse for the initial CLR resource type represented as either
            // a resource or resource collection in the raw URL path.
            var resourceType                        = default(IResourceType);
            var clrResourceType                     = default(Type);
            var nonResourcePathSegments             = default(List<string>);
            var pathSegmentToResourceTypeDictionary = default(IDictionary<string, IResourceType>);

            // Parse for initial resource or resource collection path objects.
            while (urlPathSegmentsEnumerator.MoveNext())
            {
                var currentUrlPathSegment = urlPathSegmentsEnumerator.Current;

                pathSegmentToResourceTypeDictionary = pathSegmentToResourceTypeDictionary ?? serviceModel
                                                                                            .ResourceTypes
                                                                                            .ToDictionary(x => x.HypermediaInfo.ApiCollectionPathSegment, StringComparer.OrdinalIgnoreCase);

                // Iterate over URL path segments until the current URL path segment
                // represents a CLR resource collection path segment.
                if (pathSegmentToResourceTypeDictionary.TryGetValue(currentUrlPathSegment, out resourceType))
                {
                    // Done iterating.
                    clrResourceType = resourceType.ClrType;
                    break;
                }

                // Keep iterating.
                nonResourcePathSegments = nonResourcePathSegments ?? new List<string>();
                nonResourcePathSegments.Add(currentUrlPathSegment);
            }

            // Add any non-resource path segments, if needed.
            var nonResourcePathSegmentsFound = nonResourcePathSegments != null;
            if (nonResourcePathSegmentsFound)
            {
                var nonResourceTypePath = new NonResourceHypermediaPath(nonResourcePathSegments);
                documentSelfPath.Add(nonResourceTypePath);
            }

            // If no CLR related resource path segments found, then done.
            var noClrResourcePathSegments = clrResourceType == null;
            if (noClrResourcePathSegments)
                return false;

            // Take into account singleton resources.
            if (resourceType.IsSingleton())
            {
                var apiSingletonPathSegment = urlPathSegmentsEnumerator.Current;
                var apiSingletonPath        = new SingletonHypermediaPath(clrResourceType, apiSingletonPathSegment);
                documentSelfPath.Add(apiSingletonPath);
                return true;
            }

            // Iterate one more URL path segment for a possible resource identifier.
            var apiCollectionPathSegment = urlPathSegmentsEnumerator.Current;
            var moreUrlPathSegments = urlPathSegmentsEnumerator.MoveNext();
            if (!moreUrlPathSegments)
            {
                var resourceCollectionPath = new ResourceCollectionHypermediaPath(clrResourceType, apiCollectionPathSegment);
                documentSelfPath.Add(resourceCollectionPath);
            }
            else
            {
                var apiId        = urlPathSegmentsEnumerator.Current;
                var resourcePath = new ResourceHypermediaPath(clrResourceType, apiCollectionPathSegment, apiId);
                documentSelfPath.Add(resourcePath);
            }

            return moreUrlPathSegments;
        }

        private static bool NextIteration(IServiceModel serviceModel, IEnumerator<string> urlPathSegmentsEnumerator, ICollection<IHypermediaPath> documentSelfPath)
        {
            // Parse for the next relationship CLR resource type represented as
            // either a to-many resource collection, to-many resource, or to-one resource
            // in the URL path.
            var previousResourceTypePath = documentSelfPath.Last(x => x.HasClrResourceType());
            var previousClrResourceType  = previousResourceTypePath.GetClrResourceType();
            var previousResourceType     = serviceModel.GetResourceType(previousClrResourceType);

            var relationship                        = default(IRelationshipInfo);
            var nonResourcePathSegments             = default(List<string>);
            var pathSegmentToRelationshipDictionary = default(IDictionary<string, IRelationshipInfo>);

            while (urlPathSegmentsEnumerator.MoveNext())
            {
                var currentUrlPathSegment = urlPathSegmentsEnumerator.Current;

                pathSegmentToRelationshipDictionary = pathSegmentToRelationshipDictionary ?? previousResourceType
                                                                                            .RelationshipsInfo
                                                                                            .Collection
                                                                                            .ToDictionary(x => x.ApiRelPathSegment, StringComparer.OrdinalIgnoreCase);

                // Iterate over URL path segments until the current URL path segment
                // represents a relationship path segment.
                if (pathSegmentToRelationshipDictionary.TryGetValue(currentUrlPathSegment, out relationship))
                {
                    // Done iterating.
                    break;
                }

                // Keep iterating.
                nonResourcePathSegments = nonResourcePathSegments ?? new List<string>();
                nonResourcePathSegments.Add(currentUrlPathSegment);
            }

            // Add any non-resource path segments if needed.
            var nonResourcePathSegmentsFound = nonResourcePathSegments != null;
            if (nonResourcePathSegmentsFound)
            {
                var nonResourceTypePath = new NonResourceHypermediaPath(nonResourcePathSegments);
                documentSelfPath.Add(nonResourceTypePath);
            }

            // If no relationship path segments found, then done.
            var noRelationshipPathSegments = relationship == null;
            if (noRelationshipPathSegments)
                return false;

            // Iterate one more URL path segment for a possible resource identifier.
            var clrResourceType               = relationship.ToClrType;
            var apiRelationshipRelPathSegment = urlPathSegmentsEnumerator.Current;
            var apiRelationshipCardinality    = relationship.ToCardinality;

            bool continueIterating;
            switch (apiRelationshipCardinality)
            {
                case RelationshipCardinality.ToOne:
                {
                    continueIterating = true;

                    var toOneResourcePath = new ToOneResourceHypermediaPath(clrResourceType, apiRelationshipRelPathSegment);
                    documentSelfPath.Add(toOneResourcePath);
                }
                    break;

                case RelationshipCardinality.ToMany:
                {
                    var moreUrlPathSegments = urlPathSegmentsEnumerator.MoveNext();
                    continueIterating = moreUrlPathSegments;

                    if (!moreUrlPathSegments)
                    {
                        var toManyResourceCollectionPath = new ToManyResourceCollectionHypermediaPath(clrResourceType, apiRelationshipRelPathSegment);
                        documentSelfPath.Add(toManyResourceCollectionPath);
                    }
                    else
                    {
                        var apiId              = urlPathSegmentsEnumerator.Current;
                        var toManyResourcePath = new ToManyResourceHypermediaPath(clrResourceType, apiRelationshipRelPathSegment, apiId);
                        documentSelfPath.Add(toManyResourcePath);
                    }
                }
                    break;

                default:
                {
                    var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                           .FormatWith(typeof(RelationshipCardinality).Name, apiRelationshipCardinality);
                    throw new InternalErrorException(detail);
                }
            }

            return continueIterating;
        }
        #endregion
    }
}
