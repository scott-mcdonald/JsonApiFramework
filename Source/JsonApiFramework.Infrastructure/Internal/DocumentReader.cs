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

        public IResource GetRelatedResource(Type clrRelatedResourceType, Relationship relationship)
        {
            Contract.Requires(clrRelatedResourceType != null);
            Contract.Requires(relationship != null);

            if (relationship.IsToManyRelationship())
            {
                var detail = InfrastructureErrorStrings.DocumentReadExceptionGetToOneRelatedResourceWithToManyRelationship
                    .FormatWith(clrRelatedResourceType.Name);
                throw new DocumentReadException(detail);
            }

            if (relationship.IsResourceLinkageNullOrEmpty())
                return default(IResource);

            var apiRelatedResourceIdentifier = relationship.GetToOneResourceLinkage();
            if (apiRelatedResourceIdentifier == null)
                return default(IResource);

            var clrRelatedResource = this.DomDocument
                .DomResources()
                .Where(x => apiRelatedResourceIdentifier == (ResourceIdentifier)x.ApiResource)
                .Select(x => x.ClrResource)
                .Cast<IResource>()
                .SingleOrDefault();
            return clrRelatedResource;
        }

        public IEnumerable<IResource> GetRelatedResourceCollection(Type clrRelatedResourceType, Relationship relationship)
        {
            Contract.Requires(clrRelatedResourceType != null);
            Contract.Requires(relationship != null);

            if (relationship.IsToOneRelationship())
            {
                var detail = InfrastructureErrorStrings.DocumentReadExceptionGetToManyRelatedResourceCollectionWithToOneRelationship
                    .FormatWith(clrRelatedResourceType.Name);
                throw new DocumentReadException(detail);
            }

            if (relationship.IsResourceLinkageNullOrEmpty())
                return Enumerable.Empty<IResource>();

            var apiRelatedResourceIdentifierCollection = relationship.GetToManyResourceLinkage()
                .SafeToList();
            if (apiRelatedResourceIdentifierCollection == null || !apiRelatedResourceIdentifierCollection.Any())
                return Enumerable.Empty<IResource>();

            var clrRelatedResourceCollection = this.DomDocument
                .DomResources()
                .Where(x => apiRelatedResourceIdentifierCollection.Contains((ResourceIdentifier)x.ApiResource))
                .Select(x => x.ClrResource)
                .Cast<IResource>();
            return clrRelatedResourceCollection;
        }

        public IResource GetResource(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var clrResourceCollection = this.GetResourceCollection(clrResourceType)
                .SafeToList();
            if (clrResourceCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(clrResourceType.Name);
                throw new DocumentReadException(detail);
            }

            var clrResource = clrResourceCollection.SingleOrDefault();
            return clrResource;
        }

        public IResource GetResource<TResourceId>(Type clrResourceType, TResourceId clrResourceId)
        {
            Contract.Requires(clrResourceType != null);

            var resourceType = this.DomDocument
                .ServiceModel
                .GetResourceType(clrResourceType);

            List<IResource> clrResourceCollection;

            var clrResourceIdEquatable = clrResourceId as IEquatable<TResourceId>;
            if (clrResourceIdEquatable != null)
            {
                clrResourceCollection = this.GetResourceCollection(clrResourceType)
                    .Where(x => clrResourceIdEquatable.Equals((TResourceId)resourceType.GetClrId(x)))
                    .ToList();
            }
            else
            {
                clrResourceCollection = this.GetResourceCollection(clrResourceType)
                    .Where(x => clrResourceId.Equals(resourceType.GetClrId(x)))
                    .ToList();
            }

            if (clrResourceCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings.DocumentReadExceptionGetMultipleResourcesExistWithSameIdentity
                    .FormatWith(clrResourceType.Name, clrResourceId);
                throw new DocumentReadException(detail);
            }

            var clrResource = clrResourceCollection.SingleOrDefault();
            return clrResource;
        }

        public IEnumerable<IResource> GetResourceCollection(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var clrResourceCollection = this.DomDocument
                .DomResources()
                .Where(x => x.ClrResourceType == clrResourceType)
                .Select(x => x.ClrResource)
                .Cast<IResource>();
            return clrResourceCollection;
        }

        public TResourceId GetResourceId<TResourceId>(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var clrResourceIdCollection = this.GetResourceIdCollection<TResourceId>(clrResourceType)
                                              .SafeToList();
            if (clrResourceIdCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(clrResourceType.Name);
                throw new DocumentReadException(detail);
            }

            var clrResourceId = clrResourceIdCollection.SingleOrDefault();
            return clrResourceId;
        }

        public IEnumerable<TResourceId> GetResourceIdCollection<TResourceId>(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var clrResourceIdCollection = this.DomDocument
                                              .DomResourceIdentities()
                                              .Where(x => x.ClrResourceType == clrResourceType)
                                              .Select(MapResourceId<TResourceId>);
            return clrResourceIdCollection;
        }

        public Meta GetResourceMeta(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var apiResourceMetaCollection = this.GetResourceMetaCollection(clrResourceType)
                                                .SafeToList();
            if (apiResourceMetaCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(clrResourceType.Name);
                throw new DocumentReadException(detail);
            }

            var apiResourceMeta = apiResourceMetaCollection.SingleOrDefault();
            return apiResourceMeta;
        }

        public Meta GetResourceMeta(IResource clrResource)
        {
            Contract.Requires(clrResource != null);

            var domResourceIdentity = this.GetDomResourceIdentity(clrResource);
            if (domResourceIdentity == null)
                return null;

            var apiResourceMeta = domResourceIdentity.ApiResourceMeta;
            return apiResourceMeta;
        }

        public Meta GetResourceMeta<TResourceId>(Type clrResourceType, TResourceId clrResourceId)
        {
            Contract.Requires(clrResourceType != null);

            var domResourceIdentity = this.GetDomResourceIdentity(clrResourceType, clrResourceId);
            if (domResourceIdentity == null)
                return null;

            var apiResourceMeta = domResourceIdentity.ApiResourceMeta;
            return apiResourceMeta;
        }

        public IEnumerable<Meta> GetResourceMetaCollection(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var apiResourceMetaCollection = this.DomDocument
                                                .DomResourceIdentities()
                                                .Where(x => x.ClrResourceType == clrResourceType)
                                                .Select(x => x.ApiResourceMeta);
            return apiResourceMetaCollection;
        }

        public Links GetResourceLinks(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var apiResourceLinksCollection = this.GetResourceLinksCollection(clrResourceType)
                                                 .SafeToList();
            if (apiResourceLinksCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(clrResourceType.Name);
                throw new DocumentReadException(detail);
            }

            var apiResourceLinks = apiResourceLinksCollection.SingleOrDefault();
            return apiResourceLinks;
        }

        public Links GetResourceLinks(IResource clrResource)
        {
            var domResource = this.GetDomResource(clrResource);
            if (domResource == null)
                return null;

            var apiResourceLinks = domResource.ApiResourceLinks;
            return apiResourceLinks;
        }

        public Links GetResourceLinks<TResourceId>(Type clrResourceType, TResourceId clrResourceId)
        {
            Contract.Requires(clrResourceType != null);

            var domResource = this.GetDomResource(clrResourceType, clrResourceId);
            if (domResource == null)
                return null;

            var apiResourceLinks = domResource.ApiResourceLinks;
            return apiResourceLinks;
        }

        public IEnumerable<Links> GetResourceLinksCollection(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var apiResourceLinksCollection = this.DomDocument
                                                 .DomResources()
                                                 .Where(x => x.ClrResourceType == clrResourceType)
                                                 .Select(x => x.ApiResourceLinks);
            return apiResourceLinksCollection;
        }

        public Relationships GetResourceRelationships(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var apiResourceRelationshipsCollection = this.GetResourceRelationshipsCollection(clrResourceType)
                                                         .SafeToList();
            if (apiResourceRelationshipsCollection.Count > 1)
            {
                var detail = InfrastructureErrorStrings
                    .DocumentReadExceptionGetMultipleResourcesExistWithSameType
                    .FormatWith(clrResourceType.Name);
                throw new DocumentReadException(detail);
            }

            var apiResourceRelationships = apiResourceRelationshipsCollection.SingleOrDefault();
            return apiResourceRelationships;
        }

        public Relationships GetResourceRelationships(IResource clrResource)
        {
            var domResource = this.GetDomResource(clrResource);
            if (domResource == null)
                return null;

            var apiResourceRelationships = domResource.ApiResourceRelationships;
            return apiResourceRelationships;
        }

        public Relationships GetResourceRelationships<TResourceId>(Type clrResourceType, TResourceId clrResourceId)
        {
            Contract.Requires(clrResourceType != null);

            var domResource = this.GetDomResource(clrResourceType, clrResourceId);
            if (domResource == null)
                return null;

            var apiResourceRelationships = domResource.ApiResourceRelationships;
            return apiResourceRelationships;
        }

        public IEnumerable<Relationships> GetResourceRelationshipsCollection(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var apiResourceRelationshipsCollection = this.DomDocument
                                                         .DomResources()
                                                         .Where(x => x.ClrResourceType == clrResourceType)
                                                         .Select(x => x.ApiResourceRelationships);
            return apiResourceRelationshipsCollection;
        }

        public IEnumerable<Type> GetResourceTypeCollection()
        {
            var clrResourceTypeCollection = this.DomDocument
                                                .DomResources()
                                                .Select(x => x.ClrResourceType)
                                                .Distinct();
            return clrResourceTypeCollection;
        }

        public IEnumerable<Error> GetErrorCollection()
        {
            var domErrors = this.DomDocument.DomErrors();
            var apiErrorCollection = domErrors != null
                ? domErrors.Errors
                : Enumerable.Empty<Error>();
            return apiErrorCollection;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private bool IsDisposed { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Helper Methods
        private IDomResource GetDomResource(IResource clrResource)
        {
            if (clrResource == null)
                return null;

            var clrResourceType = clrResource.GetType();
            var resourceType = this.DomDocument
                                   .ServiceModel
                                   .GetResourceType(clrResourceType);

            var apiResourceId = resourceType.GetApiId(clrResource);
            var domResource = this.DomDocument
                                  .DomResources()
                                  .SingleOrDefault(x => x.ClrResourceType == clrResourceType && x.ApiResourceId == apiResourceId);
            return domResource;
        }

        private IDomResource GetDomResource<TResourceId>(Type clrResourceType, TResourceId clrResourceId)
        {
            Contract.Requires(clrResourceType != null);

            var resourceType = this.DomDocument
                                   .ServiceModel
                                   .GetResourceType(clrResourceType);

            var apiResourceId = resourceType.ToApiId(clrResourceId);
            var domResource = this.DomDocument
                                  .DomResources()
                                  .SingleOrDefault(x => x.ClrResourceType == clrResourceType && x.ApiResourceId == apiResourceId);
            return domResource;
        }

        private IDomResourceIdentity GetDomResourceIdentity(IResource clrResource)
        {
            if (clrResource == null)
                return null;

            var clrResourceType = clrResource.GetType();
            var resourceType = this.DomDocument
                                   .ServiceModel
                                   .GetResourceType(clrResourceType);

            var apiResourceId = resourceType.GetApiId(clrResource);
            var domResourceIdentity = this.DomDocument
                                          .DomResourceIdentities()
                                          .SingleOrDefault(x => x.ClrResourceType == clrResourceType && x.ApiResourceId == apiResourceId);
            return domResourceIdentity;
        }

        private IDomResourceIdentity GetDomResourceIdentity<TResourceId>(Type clrResourceType, TResourceId clrResourceId)
        {
            Contract.Requires(clrResourceType != null);

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
