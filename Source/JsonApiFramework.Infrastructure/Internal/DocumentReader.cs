// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    internal class DocumentReader : IDocumentReader
        , IDisposable
        , IGetDomDocument
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentReader(Document apiDocument, IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(apiDocument != null);

            var domDocument = DomDocument.Parse(apiDocument, serviceModel);
            this.DomDocument = domDocument;
        }

        public DocumentReader(DomDocument domDocument)
        {
            Contract.Requires(domDocument != null);

            this.DomDocument = domDocument;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IGetDomDocument Implementation
        public DomDocument DomDocument
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDisposable Implementation
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IDocumentReader Implementation
        public JsonApiVersion GetJsonApiVersion()
        {
            var clrDocumentJsonApiVersionNode = this.DomDocument
                                                    .GetNode(DomNodeType.JsonApiVersion);
            var clrDocumentDomJsonApiVersionNode = (IDomJsonApiVersion)clrDocumentJsonApiVersionNode;
            var clrDocumentJsonApiVersion = clrDocumentDomJsonApiVersionNode != null
                ? clrDocumentDomJsonApiVersionNode.JsonApiVersion
                : null;
            return clrDocumentJsonApiVersion;
        }

        public DocumentType GetDocumentType()
        {
            var documentType = this.DomDocument.GetDocumentType();
            return documentType;
        }

        public Meta GetDocumentMeta()
        {
            var clrDocumentMetaNode = this.DomDocument
                                          .GetNode(DomNodeType.Meta);
            var clrDocumentDomMetaNode = (IDomMeta)clrDocumentMetaNode;
            var clrDocumentMeta = clrDocumentDomMetaNode != null
                ? clrDocumentDomMetaNode.Meta
                : null;
            return clrDocumentMeta;
        }

        public Links GetDocumentLinks()
        {
            var clrDocumentLinksNode = this.DomDocument
                                           .GetNode(DomNodeType.Links);
            var clrDocumentDomLinksNode = (IDomLinks)clrDocumentLinksNode;
            var clrDocumentLinks = clrDocumentDomLinksNode != null
                ? clrDocumentDomLinksNode.Links
                : null;
            return clrDocumentLinks;
        }

        public TResource GetRelatedToOneResource<TResource>(Relationship relationship)
            where TResource : class, IResource
        {
            Contract.Requires(relationship != null);

            if (relationship.IsToManyRelationship())
            {
                var detail = InfrastructureErrorStrings.DocumentReadExceptionGetToOneRelatedResourceWithToManyRelationship
                                                       .FormatWith(typeof(TResource).Name);
                throw new DocumentReadException(detail);
            }

            if (relationship.IsResourceLinkageNullOrEmpty())
                return default(TResource);

            var apiRelatedResourceIdentifier = relationship.GetToOneResourceLinkage();
            if (apiRelatedResourceIdentifier == null)
                return default(TResource);

            var clrRelatedResource = this.DomDocument
                                         .DomResources()
                                         .Where(x => apiRelatedResourceIdentifier == (ResourceIdentifier)x.ApiResource)
                                         .Select(x => x.ClrResource)
                                         .Cast<TResource>()
                                         .SingleOrDefault();
            return clrRelatedResource;
        }

        public IEnumerable<TResource> GetRelatedToManyResourceCollection<TResource>(Relationship relationship)
            where TResource : class, IResource
        {
            Contract.Requires(relationship != null);

            if (relationship.IsToOneRelationship())
            {
                var detail = InfrastructureErrorStrings.DocumentReadExceptionGetToManyRelatedResourceCollectionWithToOneRelationship
                                                       .FormatWith(typeof(TResource).Name);
                throw new DocumentReadException(detail);
            }

            if (relationship.IsResourceLinkageNullOrEmpty())
                return Enumerable.Empty<TResource>();

            var apiRelatedResourceIdentifierCollection = relationship.GetToManyResourceLinkage()
                                                                     .SafeToList();
            if (apiRelatedResourceIdentifierCollection == null || !apiRelatedResourceIdentifierCollection.Any())
                return Enumerable.Empty<TResource>();

            var clrRelatedResourceCollection = this.DomDocument
                                                   .DomResources()
                                                   .Where(x => apiRelatedResourceIdentifierCollection.Contains((ResourceIdentifier)x.ApiResource))
                                                   .Select(x => x.ClrResource)
                                                   .Cast<TResource>();
            return clrRelatedResourceCollection;
        }

        public TResource GetResource<TResource>()
            where TResource : class, IResource
        {
            var clrResourceCollection = this.GetResourceCollection<TResource>()
                                            .SafeToList();
            if (clrResourceCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(typeof(TResource).Name);
                throw new DocumentReadException(detail);
            }

            var clrResource = clrResourceCollection.SingleOrDefault();
            return clrResource;
        }

        public TResource GetResource<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource
        {
            var clrResourceType = typeof(TResource);
            var resourceType = this.DomDocument
                                   .ServiceModel
                                   .GetResourceType(clrResourceType);

            List<TResource> clrResourceCollection;

            var clrResourceIdEquatable = clrResourceId as IEquatable<TResourceId>;
            if (clrResourceIdEquatable != null)
            {
                clrResourceCollection = this.GetResourceCollection<TResource>()
                                            .Where(x => clrResourceIdEquatable.Equals((TResourceId)resourceType.GetClrId(x)))
                                            .ToList();
            }
            else
            {
                clrResourceCollection = this.GetResourceCollection<TResource>()
                                            .Where(x => clrResourceId.Equals(resourceType.GetClrId(x)))
                                            .ToList();
            }

            if (clrResourceCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings.DocumentReadExceptionGetMultipleResourcesExistWithSameIdentity
                                                       .FormatWith(typeof(TResource).Name, clrResourceId);
                throw new DocumentReadException(detail);
            }

            var clrResource = clrResourceCollection.SingleOrDefault();
            return clrResource;
        }

        public IEnumerable<TResource> GetResourceCollection<TResource>()
            where TResource : class, IResource
        {
            var clrResourceType = typeof(TResource);
            var clrResourceCollection = this.DomDocument
                                            .DomResources()
                                            .Where(x => x.ClrResourceType == clrResourceType)
                                            .Select(x => x.ClrResource)
                                            .Cast<TResource>();
            return clrResourceCollection;
        }

        public TResourceId GetResourceId<TResource, TResourceId>()
            where TResource : class, IResource
        {
            var clrResourceIdCollection = this.GetResourceIdCollection<TResource, TResourceId>()
                                              .SafeToList();
            if (clrResourceIdCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(typeof(TResource).Name);
                throw new DocumentReadException(detail);
            }

            var clrResourceId = clrResourceIdCollection.SingleOrDefault();
            return clrResourceId;
        }

        public IEnumerable<TResourceId> GetResourceIdCollection<TResource, TResourceId>()
            where TResource : class, IResource
        {
            var clrResourceType = typeof(TResource);
            var clrResourceIdCollection = this.DomDocument
                                              .DomResourceIdentities()
                                              .Where(x => x.ClrResourceType == clrResourceType)
                                              .Select(MapResourceId<TResourceId>);
            return clrResourceIdCollection;
        }

        public Meta GetResourceMeta<TResource>()
            where TResource : class, IResource
        {
            var apiResourceMetaCollection = this.GetResourceMetaCollection<TResource>()
                                                .SafeToList();
            if (apiResourceMetaCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(typeof(TResource).Name);
                throw new DocumentReadException(detail);
            }

            var apiResourceMeta = apiResourceMetaCollection.SingleOrDefault();
            return apiResourceMeta;
        }

        public Meta GetResourceMeta<TResource>(TResource clrResource)
            where TResource : class, IResource
        {
            Contract.Requires(clrResource != null);

            var domResourceIdentity = this.GetDomResourceIdentity(clrResource);
            if (domResourceIdentity == null)
                return null;

            var apiResourceMeta = domResourceIdentity.ApiResourceMeta;
            return apiResourceMeta;
        }

        public Meta GetResourceMeta<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource
        {
            var domResourceIdentity = this.GetDomResourceIdentity<TResource, TResourceId>(clrResourceId);
            if (domResourceIdentity == null)
                return null;

            var apiResourceMeta = domResourceIdentity.ApiResourceMeta;
            return apiResourceMeta;
        }

        public IEnumerable<Meta> GetResourceMetaCollection<TResource>()
            where TResource : class, IResource
        {
            var clrResourceType = typeof(TResource);
            var apiResourceMetaCollection = this.DomDocument
                                                .DomResourceIdentities()
                                                .Where(x => x.ClrResourceType == clrResourceType)
                                                .Select(x => x.ApiResourceMeta);
            return apiResourceMetaCollection;
        }

        public Links GetResourceLinks<TResource>()
            where TResource : class, IResource
        {
            var apiResourceLinksCollection = this.GetResourceLinksCollection<TResource>()
                                                 .SafeToList();
            if (apiResourceLinksCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(typeof(TResource).Name);
                throw new DocumentReadException(detail);
            }

            var apiResourceLinks = apiResourceLinksCollection.SingleOrDefault();
            return apiResourceLinks;
        }

        public Links GetResourceLinks<TResource>(TResource clrResource)
            where TResource : class, IResource
        {
            var domResource = this.GetDomResource(clrResource);
            if (domResource == null)
                return null;

            var apiResourceLinks = domResource.ApiResourceLinks;
            return apiResourceLinks;
        }

        public Links GetResourceLinks<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource
        {
            var domResource = this.GetDomResource<TResource, TResourceId>(clrResourceId);
            if (domResource == null)
                return null;

            var apiResourceLinks = domResource.ApiResourceLinks;
            return apiResourceLinks;
        }

        public IEnumerable<Links> GetResourceLinksCollection<TResource>()
            where TResource : class, IResource
        {
            var clrResourceType = typeof(TResource);
            var apiResourceLinksCollection = this.DomDocument
                                                 .DomResources()
                                                 .Where(x => x.ClrResourceType == clrResourceType)
                                                 .Select(x => x.ApiResourceLinks);
            return apiResourceLinksCollection;
        }

        public Relationships GetResourceRelationships<TResource>()
            where TResource : class, IResource
        {
            var apiResourceRelationshipsCollection = this.GetResourceRelationshipsCollection<TResource>()
                                                         .SafeToList();
            if (apiResourceRelationshipsCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(typeof(TResource).Name);
                throw new DocumentReadException(detail);
            }

            var apiResourceRelationships = apiResourceRelationshipsCollection.SingleOrDefault();
            return apiResourceRelationships;
        }

        public Relationships GetResourceRelationships<TResource>(TResource clrResource) where TResource : class, IResource
        {
            var domResource = this.GetDomResource(clrResource);
            if (domResource == null)
                return null;

            var apiResourceRelationships = domResource.ApiResourceRelationships;
            return apiResourceRelationships;
        }

        public Relationships GetResourceRelationships<TResource, TResourceId>(TResourceId clrResourceId) where TResource : class, IResource
        {
            var domResource = this.GetDomResource<TResource, TResourceId>(clrResourceId);
            if (domResource == null)
                return null;

            var apiResourceRelationships = domResource.ApiResourceRelationships;
            return apiResourceRelationships;
        }

        public IEnumerable<Relationships> GetResourceRelationshipsCollection<TResource>()
            where TResource : class, IResource
        {
            var clrResourceType = typeof(TResource);
            var apiResourceRelationshipsCollection = this.DomDocument
                                                         .DomResources()
                                                         .Where(x => x.ClrResourceType == clrResourceType)
                                                         .Select(x => x.ApiResourceRelationships);
            return apiResourceRelationshipsCollection;
        }

        public IEnumerable<Type> GetResourceTypes()
        {
            var clrResourceTypes = this.DomDocument
                                       .DomResources()
                                       .Select(x => x.ClrResourceType)
                                       .Distinct();
            return clrResourceTypes;
        }

        public IEnumerable<Error> GetErrors()
        {
            var domErrors = this.DomDocument.DomErrors();
            return domErrors != null
                ? domErrors.Errors
                : Enumerable.Empty<Error>();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private bool IsDisposed { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Helper Methods
        private IDomResource GetDomResource<TResource>(TResource clrResource)
            where TResource : class, IResource
        {
            Contract.Requires(clrResource != null);

            var clrResourceType = typeof(TResource);
            var resourceType = this.DomDocument
                                   .ServiceModel
                                   .GetResourceType(clrResourceType);

            var apiResourceId = resourceType.GetApiId(clrResource);
            var domResource = this.DomDocument
                                  .DomResources()
                                  .SingleOrDefault(x => x.ClrResourceType == clrResourceType && x.ApiResourceId == apiResourceId);
            return domResource;
        }

        private IDomResource GetDomResource<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource
        {
            var clrResourceType = typeof(TResource);
            var resourceType = this.DomDocument
                                   .ServiceModel
                                   .GetResourceType(clrResourceType);

            var apiResourceId = resourceType.ToApiId(clrResourceId);
            var domResource = this.DomDocument
                                  .DomResources()
                                  .SingleOrDefault(x => x.ClrResourceType == clrResourceType && x.ApiResourceId == apiResourceId);
            return domResource;
        }

        private IDomResourceIdentity GetDomResourceIdentity<TResource>(TResource clrResource)
            where TResource : class, IResource
        {
            Contract.Requires(clrResource != null);

            var clrResourceType = typeof(TResource);
            var resourceType = this.DomDocument
                                   .ServiceModel
                                   .GetResourceType(clrResourceType);

            var apiResourceId = resourceType.GetApiId(clrResource);
            var domResourceIdentity = this.DomDocument
                                          .DomResourceIdentities()
                                          .SingleOrDefault(x => x.ClrResourceType == clrResourceType && x.ApiResourceId == apiResourceId);
            return domResourceIdentity;
        }

        private IDomResourceIdentity GetDomResourceIdentity<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource
        {
            var clrResourceType = typeof(TResource);
            var resourceType = this.DomDocument
                                   .ServiceModel
                                   .GetResourceType(clrResourceType);

            var apiResourceId = resourceType.ToApiId(clrResourceId);
            var domResourceIdentity = this.DomDocument
                                          .DomResourceIdentities()
                                          .SingleOrDefault(x => x.ClrResourceType == clrResourceType && x.ApiResourceId == apiResourceId);
            return domResourceIdentity;
        }

        private static TResourceId MapResourceId<TResourceId>(IDomResourceIdentity domResourceIdentity)
        {
            Contract.Requires(domResourceIdentity != null);

            var apiResourceId = domResourceIdentity.ApiResourceId;
            var clrId = TypeConverter.Convert<TResourceId>(apiResourceId);
            return clrId;
        }
        #endregion

        #region Dispose Methods
        private void Dispose(bool disposeManagedResources)
        {
            try
            {
                // Don't dispose more than once.
                if (this.IsDisposed)
                    return;

                // Dispose of managed resources conditionally and unmanaged
                // resources unconditionally. Lastly null out reference handles
                // to remove references to the respective objects for the GC.
                if (disposeManagedResources)
                {
                    this.DisposeManagedResources();
                }
                this.DisposeUnmanagedResources();
                this.NullOutReferences();
            }
            finally
            {
                // Set disposed flag.
                this.IsDisposed = true;
            }
        }

        private void DisposeManagedResources()
        { }

        private void DisposeUnmanagedResources()
        { }

        private void NullOutReferences()
        {
            this.DomDocument = null;
        }
        #endregion
    }
}
