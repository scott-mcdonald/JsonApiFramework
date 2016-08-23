// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Http;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal static class UriExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static IEnumerable<IHypermediaPath> ParseDocumentSelfPath(this Uri url, IServiceModel serviceModel, IUrlBuilderConfiguration urlBuilderConfiguration)
        {
            Contract.Requires(url != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(urlBuilderConfiguration != null);

            // Create a URL path segment enumerator to enumerate over the URL path segments.
            var urlPathSegments = GetUrlPathSegments(urlBuilderConfiguration, url);
            var urlPathSegmentsEnumerator = (IEnumerator<string>)urlPathSegments.GetEnumerator();

            // Parse the raw URL path for document self link path objects by
            // looking for the following:
            // 1. resource collection hypermedia path objects
            // 2. resource hypermedia path objects
            // 3. non-resource hypermedia path objects
            // 4. to-many resource collection hypermedia path objects
            // 5. to-many resource hypermedia path objects
            var documentSelfPath = new List<IHypermediaPath>();
            var continueIterating = InitialIteration(serviceModel, urlPathSegmentsEnumerator, documentSelfPath);
            while (continueIterating)
            {
                continueIterating = NextIteration(serviceModel, urlPathSegmentsEnumerator, documentSelfPath);
            }
            return documentSelfPath;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Parse Methods
        private static List<string> GetUrlPathSegments(IUrlBuilderConfiguration urlBuilderConfiguration, Uri url)
        {
            Contract.Requires(urlBuilderConfiguration != null);
            Contract.Requires(url != null);

            // Skip the root path segments that are part of the URL builder
            // configuration that maybe are in the beginning of the raw URL path.
            var rootPathSegments = EnumerableExtensions.SafeToList(urlBuilderConfiguration.RootPathSegments);
            var rootPathSegmentsCount = rootPathSegments.Count;

            var urlPathSegments = EnumerableExtensions.SafeToList(JsonApiFramework.UriExtensions.GetPathSegments(url)
                                             .SkipWhile((pathSegment, index) =>
                                                 {
                                                     if (index >= rootPathSegmentsCount)
                                                         return false;

                                                     var rootPathSegment = rootPathSegments[index];
                                                     var rootPathSegmentEqualToPathSegment = String.CompareOrdinal(rootPathSegment, pathSegment) == 0;
                                                     return rootPathSegmentEqualToPathSegment;
                                                 }));
            return urlPathSegments;
        }

        private static bool InitialIteration(IServiceModel serviceModel, IEnumerator<string> urlPathSegmentsEnumerator, ICollection<IHypermediaPath> documentSelfPath)
        {
            // Parse for the initial CLR resource type represented as either
            // a resource or resource collection in the raw URL path.
            var clrResourceType = default(Type);
            var nonResourcePathSegments = default(List<string>);
            var pathSegmentToTypeDictionary = default(IDictionary<string, Type>);

            // Parse for initial resource or resource collection path objects.
            while (urlPathSegmentsEnumerator.MoveNext())
            {
                var currentUrlPathSegment = urlPathSegmentsEnumerator.Current;

                pathSegmentToTypeDictionary = pathSegmentToTypeDictionary ?? serviceModel
                    .ResourceTypes
                    .ToDictionary(x => x.Hypermedia.ApiCollectionPathSegment, x => x.ClrResourceType, StringComparer.OrdinalIgnoreCase);

                // Iterate over URL path segments until the current URL path segment
                // represents a CLR resource collection path segment.
                if (pathSegmentToTypeDictionary.TryGetValue(currentUrlPathSegment, out clrResourceType))
                {
                    // Done iterating.
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

            // If no resource or resource collection path segments found, then done.
            var noResourceOrResourceCollectionPathSegments = clrResourceType == null;
            if (noResourceOrResourceCollectionPathSegments)
                return false;

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
                var apiId = urlPathSegmentsEnumerator.Current;
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
            var previousClrResourceType = previousResourceTypePath.GetClrResourceType();
            var previousResourceType = serviceModel.GetResourceType(previousClrResourceType);

            var relationship = default(IRelationshipInfo);
            var nonResourcePathSegments = default(List<string>);
            var pathSegmentToRelationshipDictionary = default(IDictionary<string, IRelationshipInfo>);

            while (urlPathSegmentsEnumerator.MoveNext())
            {
                var currentUrlPathSegment = urlPathSegmentsEnumerator.Current;

                pathSegmentToRelationshipDictionary = pathSegmentToRelationshipDictionary ?? previousResourceType
                    .Relationships
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
            var clrResourceType = relationship.ToClrType;
            var apiRelationshipRelPathSegment = urlPathSegmentsEnumerator.Current;
            var apiRelationshipCardinality = relationship.ToCardinality;

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
                            var apiId = urlPathSegmentsEnumerator.Current;
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
