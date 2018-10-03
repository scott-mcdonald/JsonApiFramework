// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Http;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Server.Hypermedia.Internal;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Hypermedia
{
    public class HypermediaAssembler : IHypermediaAssembler
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IHypermediaAssembler Implementation
        public string Name { get { return TypeName; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IHypermediaAssembler Implementation
        public Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(documentPathContext != null);
            Contract.Requires(linkContext != null);

            var apiDocumentLink = CreateStandardDocumentLink(hypermediaContext, documentPathContext, linkContext);
            if (apiDocumentLink != null)
            {
                return apiDocumentLink;
            }

            var apiRel = linkContext.Rel;
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildNonStandardDocumentLink
                                           .FormatWith(apiRel);
            throw new DocumentBuildException(detail);
        }

        public Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, Type clrResourceType, IEnumerable<object> clrResourceCollection, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(documentPathContext != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResourceCollection != null);
            Contract.Requires(linkContext != null);

            var apiDocumentLink = CreateStandardDocumentLink(hypermediaContext, documentPathContext, linkContext);
            if (apiDocumentLink != null)
            {
                return apiDocumentLink;
            }

            var apiRel = linkContext.Rel;
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildNonStandardDocumentLink
                                           .FormatWith(apiRel);
            throw new DocumentBuildException(detail);
        }

        public Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, Type clrResourceType, object clrResource, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(documentPathContext != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(linkContext != null);

            var apiDocumentLink = CreateStandardDocumentLink(hypermediaContext, documentPathContext, linkContext);
            if (apiDocumentLink != null)
            {
                return apiDocumentLink;
            }

            var apiRel = linkContext.Rel;
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildNonStandardDocumentLink
                                           .FormatWith(apiRel);
            throw new DocumentBuildException(detail);
        }

        public Link CreateResourceLink(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, Type clrResourceType, object clrResource, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(resourcePathContext != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(linkContext != null);

            var apiResourceLink = CreateStandardResourceLink(hypermediaContext, resourcePathContext, clrResourceType, clrResource, linkContext);
            if (apiResourceLink != null)
            {
                return apiResourceLink;
            }

            var apiRel = linkContext.Rel;
            var clrResourceTypeName = clrResourceType.Name;
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildNonStandardResourceLink
                                           .FormatWith(apiRel, clrResourceTypeName);
            throw new DocumentBuildException(detail);
        }

        public Relationship CreateResourceRelationship(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, Type clrResourceType, object clrResource, IRelationshipContext relationshipContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(resourcePathContext != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(relationshipContext != null);

            var apiRel = relationshipContext.Rel;

            var serviceModel = hypermediaContext.GetServiceModel();
            var resourceType = serviceModel.GetResourceType(clrResourceType);
            var relationship = resourceType.GetRelationshipInfo(apiRel);

            // Create the Links object of the relationship.
            var apiRelationshipLinks = CreateResourceRelationshipLinks(hypermediaContext, resourcePathContext, resourceType, clrResource, relationship, relationshipContext);

            // Create the relationship:
            var apiRelationship = CreateResourceRelationship(apiRelationshipLinks, resourceType, relationship, relationshipContext);
            return apiRelationship;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Link CreateStandardDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(documentPathContext != null);
            Contract.Requires(linkContext != null);

            var apiRel = linkContext.Rel;
            var apiLinkMeta = linkContext.Meta;
            switch (apiRel)
            {
                case Keywords.Up:
                    {
                        var apiDocumentSelfPath = documentPathContext.DocumentSelfPath
                                                                     .SafeToList();
                        var apiDocumentSelfPathCount = apiDocumentSelfPath.Count;

                        var apiDocumentUpPath = new List<IHypermediaPath>(apiDocumentSelfPathCount);
                        for (var i = 0; i < apiDocumentSelfPathCount; ++i)
                        {
                            var hypermediaPath = apiDocumentSelfPath[i];

                            var lastHypermediaPath = i == apiDocumentSelfPathCount - 1;
                            if (lastHypermediaPath)
                            {
                                var hypermediaPathType = hypermediaPath.HypermediaPathType;
                                switch (hypermediaPathType)
                                {
                                    // Don't add the last hypermedia path.
                                    case HypermediaPathType.ResourceCollectionPath:
                                    case HypermediaPathType.ToOneResourcePath:
                                    case HypermediaPathType.ToManyResourceCollectionPath:
                                        continue;

                                    case HypermediaPathType.ResourcePath:
                                        {
                                            // Turn last resource path into a resource collection path.
                                            var resourceHypermediaPath = (ResourceHypermediaPath)hypermediaPath;
                                            var clrResourceType = resourceHypermediaPath.ClrResourceType;
                                            var apiCollectionPathSegment = resourceHypermediaPath.ApiCollectionPathSegment;
                                            var resourceCollectionHypermediaPath = new ResourceCollectionHypermediaPath(clrResourceType, apiCollectionPathSegment);

                                            hypermediaPath = resourceCollectionHypermediaPath;
                                        }
                                        break;

                                    case HypermediaPathType.ToManyResourcePath:
                                        {
                                            // Turn last resource path into a to-many resource collection path.
                                            var toManyResourceHypermediaPath = (ToManyResourceHypermediaPath)hypermediaPath;
                                            var clrResourceType = toManyResourceHypermediaPath.ClrResourceType;
                                            var apiRelationshipPathSegment = toManyResourceHypermediaPath.ApiRelationshipPathSegment;
                                            var toManyResourceCollectionHypermediaPath = new ToManyResourceCollectionHypermediaPath(clrResourceType, apiRelationshipPathSegment);

                                            hypermediaPath = toManyResourceCollectionHypermediaPath;
                                        }
                                        break;

                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            }

                            apiDocumentUpPath.Add(hypermediaPath);
                        }

                        var apiDocumentUpPathContext = new DocumentPathContext(apiDocumentUpPath);

                        var clrPrimaryResourceType = apiDocumentUpPathContext.ClrResourceTypes.Any()
                            ? apiDocumentUpPathContext.GetPrimaryClrResourceType()
                            : documentPathContext.GetPrimaryClrResourceType();

                        var urlBuilderConfiguration = hypermediaContext.GetUrlBuilderConfiguration(clrPrimaryResourceType);

                        var apiHRef = UrlBuilder.Create(urlBuilderConfiguration)
                                                .Path(apiDocumentUpPath)
                                                .Build();
                        var apiDocumentLink = new Link
                                              {
                                                  HRef = apiHRef,
                                                  Meta = apiLinkMeta
                                              };
                        return apiDocumentLink;
                    }

                case Keywords.Self:
                    {
                        var apiDocumentSelfPath = documentPathContext.DocumentSelfPath
                                                                     .SafeToList();

                        var clrPrimaryResourceType  = documentPathContext.GetPrimaryClrResourceType();
                        var urlBuilderConfiguration = hypermediaContext.GetUrlBuilderConfiguration(clrPrimaryResourceType);

                        var query = documentPathContext.DocumentSelfQuery;

                        var apiHRef = UrlBuilder.Create(urlBuilderConfiguration)
                                                .Path(apiDocumentSelfPath)
                                                .Query(query)
                                                .Build();
                        var apiDocumentLink = new Link
                            {
                                HRef = apiHRef,
                                Meta = apiLinkMeta
                            };
                        return apiDocumentLink;
                    }
            }

            return null;
        }

        private static Link CreateStandardResourceLink(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, Type clrResourceType, object clrResource, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(resourcePathContext != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(linkContext != null);

            var apiRel = linkContext.Rel;
            var apiLinkMeta = linkContext.Meta;
            switch (apiRel)
            {
                case Keywords.Canonical:
                case Keywords.Self:
                    {
                        var urlBuilderConfiguration = hypermediaContext.GetUrlBuilderConfiguration(clrResourceType);

                        var serviceModel = hypermediaContext.GetServiceModel();
                        var resourceType = serviceModel.GetResourceType(clrResourceType);

                        var isSelfLink = String.Compare(Keywords.Self, apiRel, StringComparison.Ordinal) == 0;

                        var apiId = resourceType.GetApiId(clrResource);
                        var apiResourcePath = isSelfLink
                            ? resourcePathContext.GetResourceSelfPath(apiId)
                            : resourcePathContext.GetResourceCanonicalPath(apiId);
                        var apiHRef = UrlBuilder.Create(urlBuilderConfiguration)
                                                .Path(apiResourcePath)
                                                .Build();
                        var apiResourceLink = new Link
                            {
                                HRef = apiHRef,
                                Meta = apiLinkMeta
                            };
                        return apiResourceLink;
                    }
            }

            return null;
        }

        private static Link CreateResourceRelationshipLink(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, IResourceType resourceType, object clrResource, bool addRelationshipsPathSegment, IRelationshipInfo relationship, Meta apiRelationshipLinkMeta)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(resourcePathContext != null);
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(relationship != null);

            var clrResourceType         = resourceType.ClrType;
            var urlBuilderConfiguration = hypermediaContext.GetUrlBuilderConfiguration(clrResourceType);

            var apiId = resourceType.GetApiId(clrResource);
            var apiResourcePath = resourcePathContext.GetResourceSelfPath(apiId);
            var apiRelationshipRelPathSegment = relationship.ApiRelPathSegment;
            var apiRelationshipLinkHRef = UrlBuilder.Create(urlBuilderConfiguration)
                                                    .Path(apiResourcePath)
                                                    .Path(Keywords.Relationships, addRelationshipsPathSegment)
                                                    .Path(apiRelationshipRelPathSegment)
                                                    .Build();
            var apiRelationshipLink = new Link
                {
                    HRef = apiRelationshipLinkHRef,
                    Meta = apiRelationshipLinkMeta
                };
            return apiRelationshipLink;
        }

        private static Links CreateResourceRelationshipLinks(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, IResourceType resourceType, object clrResource, IRelationshipInfo relationship, IRelationshipContext relationshipContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(resourcePathContext != null);
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(relationship != null);
            Contract.Requires(relationshipContext != null);

            var apiRelationshipHasLinks = relationshipContext.HasLinks();
            if (!apiRelationshipHasLinks)
                return null;

            var links = relationshipContext.LinkContexts;
            var apiRelationshipRelToLinkDictionary = links
                .Select(x =>
                    {
                        var rel = x.Rel;
                        var meta = x.Meta;
                        var isSelfLink = String.Compare(Keywords.Self, rel, StringComparison.Ordinal) == 0;
                        var link = CreateResourceRelationshipLink(hypermediaContext, resourcePathContext, resourceType, clrResource, isSelfLink, relationship, meta);
                        return new Tuple<string, Link>(rel, link);
                    })
                .ToDictionary(x => x.Item1, x => x.Item2);

            var apiRelationshipLinks = new Links(apiRelationshipRelToLinkDictionary);
            return apiRelationshipLinks;
        }

        private static Relationship CreateResourceRelationship(Links apiRelationshipLinks, IResourceType resourceType, IRelationshipInfo relationship, IRelationshipContext relationshipContext)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(relationship != null);
            Contract.Requires(relationshipContext != null);

            var apiRelationshipToCardinality = relationship.ToCardinality;
            var apiRelationshipMeta = relationshipContext.Meta;
            var apiRelationshipType = relationshipContext.GetRelationshipType();
            switch (apiRelationshipType)
            {
                case RelationshipType.Relationship:
                    {
                        var apiRelationship = new Relationship
                            {
                                Links = apiRelationshipLinks,
                                Meta = apiRelationshipMeta
                            };
                        return apiRelationship;
                    }

                case RelationshipType.ToOneRelationship:
                    {
                        if (apiRelationshipToCardinality != RelationshipCardinality.ToOne)
                        {
                            var apiRel = relationshipContext.Rel;
                            var fromClrResourceTypeName = resourceType.ClrType.Name;
                            var toClrResourceTypeName = relationship.ToClrType.Name;
                            var detail = ServerErrorStrings
                                .DocumentBuildExceptionDetailBuildResourceRelationshipCardinalityMismatch
                                .FormatWith(apiRel, fromClrResourceTypeName, toClrResourceTypeName, RelationshipCardinality.ToOne, apiRelationshipToCardinality);
                            throw new DocumentBuildException(detail);
                        }

                        var apiToOneResourceLinkage = relationshipContext.GetToOneResourceLinkage();
                        var apiRelationship = new ToOneRelationship
                            {
                                Links = apiRelationshipLinks,
                                Data = apiToOneResourceLinkage,
                                Meta = apiRelationshipMeta
                            };
                        return apiRelationship;
                    }

                case RelationshipType.ToManyRelationship:
                    {
                        if (apiRelationshipToCardinality != RelationshipCardinality.ToMany)
                        {
                            var apiRel = relationshipContext.Rel;
                            var fromClrResourceTypeName = resourceType.ClrType.Name;
                            var toClrResourceTypeName = relationship.ToClrType.Name;
                            var detail = ServerErrorStrings
                                .DocumentBuildExceptionDetailBuildResourceRelationshipCardinalityMismatch
                                .FormatWith(apiRel, fromClrResourceTypeName, toClrResourceTypeName, RelationshipCardinality.ToMany, apiRelationshipToCardinality);
                            throw new DocumentBuildException(detail);
                        }

                        var apiToManyResourceLinkage = relationshipContext.GetToManyResourceLinkage()
                                                                          .SafeToList();
                        var apiRelationship = new ToManyRelationship
                            {
                                Links = apiRelationshipLinks,
                                Data = apiToManyResourceLinkage,
                                Meta = apiRelationshipMeta
                            };
                        return apiRelationship;
                    }

                default:
                    {
                        var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                               .FormatWith(typeof(RelationshipType).Name, apiRelationshipType);
                        throw new InternalErrorException(detail);
                    }
            }
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(HypermediaAssembler).Name;
        #endregion
    }

    public abstract class HypermediaAssemblerBase
    {
        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected static readonly IHypermediaAssembler DefaultHypermediaAssembler = new HypermediaAssembler();
        #endregion
    }

    public abstract class HypermediaAssembler<TResource> : HypermediaAssemblerBase, IHypermediaAssembler<TResource>
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IHypermediaAssembler Implementation
        public string Name { get; private set; }
        #endregion

        #region IHypermediaAssembler<TResource> Implementation
        public Type ClrResourceType { get { return typeof(TResource); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IHypermediaAssembler Implementation
        Link IHypermediaAssembler.CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(linkContext != null);

            var documentLink = this.CreateDocumentLink(hypermediaContext, documentPathContext, documentType, linkContext);
            return documentLink ?? DefaultHypermediaAssembler.CreateDocumentLink(hypermediaContext, documentPathContext, documentType, linkContext);
        }

        Link IHypermediaAssembler.CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, Type clrResourceType, IEnumerable<object> clrResourceCollection, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResourceCollection != null);
            Contract.Requires(linkContext != null);

            // ReSharper disable PossibleMultipleEnumeration
            var clrStronglyTypedResourceCollection = clrResourceCollection.SafeCast<TResource>();
            var documentLink = this.CreateDocumentLink(hypermediaContext, documentPathContext, documentType, clrStronglyTypedResourceCollection, linkContext);
            return documentLink ?? DefaultHypermediaAssembler.CreateDocumentLink(hypermediaContext, documentPathContext, documentType, clrResourceType, clrResourceCollection, linkContext);
            // ReSharper restore PossibleMultipleEnumeration
        }

        Link IHypermediaAssembler.CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, Type clrResourceType, object clrResource, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(linkContext != null);

            var clrStronglyTypedResource = (TResource)clrResource;
            var documentLink = this.CreateDocumentLink(hypermediaContext, documentPathContext, documentType, clrStronglyTypedResource, linkContext);
            return documentLink ?? DefaultHypermediaAssembler.CreateDocumentLink(hypermediaContext, documentPathContext, documentType, clrResourceType, clrResource, linkContext);
        }

        Link IHypermediaAssembler.CreateResourceLink(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, Type clrResourceType, object clrResource, ILinkContext linkContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(linkContext != null);

            var clrStronglyTypedResource = (TResource)clrResource;
            var resourceLink = this.CreateResourceLink(hypermediaContext, resourcePathContext, clrStronglyTypedResource, linkContext);
            return resourceLink ?? DefaultHypermediaAssembler.CreateResourceLink(hypermediaContext, resourcePathContext, clrResourceType, clrResource, linkContext);
        }

        Relationship IHypermediaAssembler.CreateResourceRelationship(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, Type clrResourceType, object clrResource, IRelationshipContext relationshipContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(relationshipContext != null);

            var clrStronglyTypedResource = (TResource)clrResource;
            var resourceRelationship = this.CreateResourceRelationship(hypermediaContext, resourcePathContext, clrStronglyTypedResource, relationshipContext);
            return resourceRelationship ?? DefaultHypermediaAssembler.CreateResourceRelationship(hypermediaContext, resourcePathContext, clrResourceType, clrResource, relationshipContext);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected HypermediaAssembler()
        {
            this.Name = this.GetType().Name;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region HypermediaAssembler<TResource> Overrides
        // ReSharper disable VirtualMemberNeverOverriden.Global
        // ReSharper disable UnusedParameter.Global
        protected virtual Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, ILinkContext linkContext)
        { return null; }

        protected virtual Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, IEnumerable<TResource> clrResourceCollection, ILinkContext linkContext)
        { return null; }

        protected virtual Link CreateDocumentLink(IHypermediaContext hypermediaContext, IDocumentPathContext documentPathContext, DocumentType documentType, TResource clrResource, ILinkContext linkContext)
        { return null; }

        protected virtual Link CreateResourceLink(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, TResource clrResource, ILinkContext linkContext)
        { return null; }

        protected virtual Relationship CreateResourceRelationship(IHypermediaContext hypermediaContext, IResourcePathContext resourcePathContext, TResource clrResource, IRelationshipContext relationshipContext)
        { return null; }
        // ReSharper restore UnusedParameter.Global
        // ReSharper restore VirtualMemberNeverOverriden.Global
        #endregion
    }

    public abstract class HypermediaAssembler<TPath1, TResource> : HypermediaAssembler<TResource>, IHypermediaAssembler<TPath1, TResource>
        where TPath1 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IHypermediaAssembler<TPath, TResource> Implementation
        public Type ClrPath1Type { get { return typeof(TPath1); } }
        #endregion
    }

    public abstract class HypermediaAssembler<TPath1, TPath2, TResource> : HypermediaAssembler<TResource>, IHypermediaAssembler<TPath1, TPath2, TResource>
        where TPath1 : class
        where TPath2 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IHypermediaAssembler<TPath1, TPath2, TResource> Implementation
        public Type ClrPath1Type { get { return typeof(TPath1); } }
        public Type ClrPath2Type { get { return typeof(TPath2); } }
        #endregion
    }

    public abstract class HypermediaAssembler<TPath1, TPath2, TPath3, TResource> : HypermediaAssembler<TResource>, IHypermediaAssembler<TPath1, TPath2, TPath3, TResource>
        where TPath1 : class
        where TPath2 : class
        where TPath3 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IHypermediaAssembler<TPath1, TPath2, TPath3, TResource> Implementation
        public Type ClrPath1Type { get { return typeof(TPath1); } }
        public Type ClrPath2Type { get { return typeof(TPath2); } }
        public Type ClrPath3Type { get { return typeof(TPath3); } }
        #endregion
    }

    public abstract class HypermediaAssembler<TPath1, TPath2, TPath3, TPath4, TResource> : HypermediaAssembler<TResource>, IHypermediaAssembler<TPath1, TPath2, TPath3, TPath4, TResource>
        where TPath1 : class
        where TPath2 : class
        where TPath3 : class
        where TPath4 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IHypermediaAssembler<TPath1, TPath2, TPath3, TPath4, TResource> Implementation
        public Type ClrPath1Type { get { return typeof(TPath1); } }
        public Type ClrPath2Type { get { return typeof(TPath2); } }
        public Type ClrPath3Type { get { return typeof(TPath3); } }
        public Type ClrPath4Type { get { return typeof(TPath4); } }
        #endregion
    }

    public abstract class HypermediaAssembler<TPath1, TPath2, TPath3, TPath4, TPath5, TResource> : HypermediaAssembler<TResource>, IHypermediaAssembler<TPath1, TPath2, TPath3, TPath4, TPath5, TResource>
        where TPath1 : class
        where TPath2 : class
        where TPath3 : class
        where TPath4 : class
        where TPath5 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IHypermediaAssembler<TPath1, TPath2, TPath3, TPath4, TPath5, TResource> Implementation
        public Type ClrPath1Type { get { return typeof(TPath1); } }
        public Type ClrPath2Type { get { return typeof(TPath2); } }
        public Type ClrPath3Type { get { return typeof(TPath3); } }
        public Type ClrPath4Type { get { return typeof(TPath4); } }
        public Type ClrPath5Type { get { return typeof(TPath5); } }
        #endregion
    }

    public abstract class HypermediaAssembler<TPath1, TPath2, TPath3, TPath4, TPath5, TPath6, TResource> : HypermediaAssembler<TResource>, IHypermediaAssembler<TPath1, TPath2, TPath3, TPath4, TPath5, TPath6, TResource>
        where TPath1 : class
        where TPath2 : class
        where TPath3 : class
        where TPath4 : class
        where TPath5 : class
        where TPath6 : class
        where TResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IHypermediaAssembler<TPath1, TPath2, TPath3, TPath4, TPath5, TPath6, TResource> Implementation
        public Type ClrPath1Type { get { return typeof(TPath1); } }
        public Type ClrPath2Type { get { return typeof(TPath2); } }
        public Type ClrPath3Type { get { return typeof(TPath3); } }
        public Type ClrPath4Type { get { return typeof(TPath4); } }
        public Type ClrPath5Type { get { return typeof(TPath5); } }
        public Type ClrPath6Type { get { return typeof(TPath6); } }
        #endregion
    }
}
