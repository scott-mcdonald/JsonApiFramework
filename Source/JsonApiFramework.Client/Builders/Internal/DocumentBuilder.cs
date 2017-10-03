// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal;
using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Client.Internal
{
    internal class DocumentBuilder : IDocumentBuilder
        , IGetDomDocument
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IGetDomDocument Implementation
        public DomDocument DomDocument
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDocumentWriter Implementation
        public Document WriteDocument()
        {
            // Resolve/Compact any resource or resource identifier nodes.
            var apiDocumentType = this.GetDocumentType();
            switch (apiDocumentType)
            {
                case DocumentType.ResourceCollectionDocument:
                case DocumentType.ResourceDocument:
                    {
                        // Compact all read-write resource nodes to read-only resource nodes.
                        this.CompactResourceNodes();
                    }
                    break;

                case DocumentType.ResourceIdentifierCollectionDocument:
                case DocumentType.ResourceIdentifierDocument:
                    {
                        // Compact all read-write resource identifier nodes to read-only resource identifier nodes.
                        this.CompactResourceIdentifierNodes();
                    }
                    break;
            }

            // Compact all read-write document nodes to read-only document nodes.
            this.CompactDocumentNodes();

            // At this point, the DOM object tree is completely built.
            // Forward the call to the given document writer to generate a
            // json:api compliant document from the DOM object tree.
            var apiDocument = this.DocumentWriter.WriteDocument();
            return apiDocument;
        }
        #endregion

        #region IDocumentBuilder Implementation
        public IDocumentBuilder SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            this.DomDocument.SetDomReadOnlyMeta(meta);
            return this;
        }

        // Resource /////////////////////////////////////////////////////////
        public IPrimaryResourceBuilder<TResource> Resource<TResource>()
            where TResource : class, IResource
        {
            var primaryResourceBuilder = new PrimaryResourceBuilder<TResource>(this, this.DomDocument, default(TResource));
            return primaryResourceBuilder;
        }

        public IPrimaryResourceBuilder<TResource> Resource<TResource>(TResource clrResource)
            where TResource : class, IResource
        {
            var primaryResourceBuilder = new PrimaryResourceBuilder<TResource>(this, this.DomDocument, clrResource);
            return primaryResourceBuilder;
        }

        public IPrimaryResourceBuilder<TResource> Resource<TResource>(IResourceSource<TResource> resourceSource)
            where TResource : class, IResource
        {
            Contract.Requires(resourceSource != null);

            var clrResource = resourceSource.GetResource();

            var primaryResourceBuilder = new PrimaryResourceBuilder<TResource>(this, this.DomDocument, clrResource);
            return primaryResourceBuilder;
        }

        // ResourceIdentifier ///////////////////////////////////////////////
        public IPrimaryResourceIdentifierBuilder<TResource> ResourceIdentifier<TResource>()
            where TResource : class, IResource
        {
            var primaryResourceIdentifierBuilder = new PrimaryResourceIdentifierBuilder<TResource>(this, this.ServiceModel, this.DomDocument);
            return primaryResourceIdentifierBuilder;
        }

        public IPrimaryResourceIdentifierBuilder<TResource> ResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class, IResource
        {
            var primaryResourceIdentifierBuilder = new PrimaryResourceIdentifierBuilder<TResource>(this, this.ServiceModel, this.DomDocument, clrResource);
            return primaryResourceIdentifierBuilder;
        }

        public IPrimaryResourceIdentifierBuilder<TResource> ResourceIdentifier<TResource, TResourceId>(IResourceIdentifierSource<TResourceId> resourceIdentifierSource)
            where TResource : class, IResource
        {
            Contract.Requires(resourceIdentifierSource != null);

            var primaryResourceIdentifierBuilder = new PrimaryResourceIdentifierBuilder<TResource>(this, this.ServiceModel, this.DomDocument);
            var resourceId = resourceIdentifierSource.GetResourceId();
            primaryResourceIdentifierBuilder.SetId(Id.Create(resourceId));
            return primaryResourceIdentifierBuilder;
        }

        public IDocumentWriter SetResourceIdentifier<TResource>()
            where TResource : class, IResource
        {
            var primaryResourceIdentifierBuilder = this.ResourceIdentifier<TResource>();
            return primaryResourceIdentifierBuilder.ResourceIdentifierEnd();
        }

        public IDocumentWriter SetResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class, IResource
        {
            var primaryResourceIdentifierBuilder = this.ResourceIdentifier(clrResource);
            return primaryResourceIdentifierBuilder.ResourceIdentifierEnd();
        }

        public IDocumentWriter SetResourceIdentifier<TResource, TResourceId>(IResourceIdentifierSource<TResourceId> resourceIdentifierSource)
            where TResource : class, IResource
        {
            Contract.Requires(resourceIdentifierSource != null);

            var primaryResourceIdentifierBuilder = this.ResourceIdentifier<TResource, TResourceId>(resourceIdentifierSource);
            return primaryResourceIdentifierBuilder.ResourceIdentifierEnd();
        }

        // ResourceIdentifierCollection /////////////////////////////////////
        public IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource>()
            where TResource : class, IResource
        {
            var primaryResourceIdentifierCollectionBuilder = new PrimaryResourceIdentifierCollectionBuilder<TResource>(this, this.ServiceModel, this.DomDocument);
            return primaryResourceIdentifierCollectionBuilder;
        }

        public IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class, IResource
        {
            var primaryResourceIdentifierCollectionBuilder = new PrimaryResourceIdentifierCollectionBuilder<TResource>(this, this.ServiceModel, this.DomDocument, clrResourceCollection);
            return primaryResourceIdentifierCollectionBuilder;
        }

        public IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource, TResourceId>(IResourceIdentifierCollectionSource<TResourceId> resourceIdentifierCollectionSource)
            where TResource : class, IResource
        {
            Contract.Requires(resourceIdentifierCollectionSource != null);

            var primaryResourceIdentifierCollectionBuilder = new PrimaryResourceIdentifierCollectionBuilder<TResource>(this, this.ServiceModel, this.DomDocument);
            var resourceIdCollection = resourceIdentifierCollectionSource.GetResourceIdCollection();
            primaryResourceIdentifierCollectionBuilder.SetId(IdCollection.Create(resourceIdCollection));
            return primaryResourceIdentifierCollectionBuilder;

        }

        public IDocumentWriter SetResourceIdentifierCollection<TResource>()
            where TResource : class, IResource
        {
            var primaryResourceIdentifierBuilder = this.ResourceIdentifierCollection<TResource>();
            return primaryResourceIdentifierBuilder.ResourceIdentifierCollectionEnd();
        }

        public IDocumentWriter SetResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class, IResource
        {
            var primaryResourceIdentifierBuilder = this.ResourceIdentifierCollection(clrResourceCollection);
            return primaryResourceIdentifierBuilder.ResourceIdentifierCollectionEnd();
        }

        public IDocumentWriter SetResourceIdentifierCollection<TResource, TResourceId>(IResourceIdentifierCollectionSource<TResourceId> resourceIdentifierCollectionSource)
            where TResource : class, IResource
        {
            var primaryResourceIdentifierBuilder = this.ResourceIdentifierCollection<TResource, TResourceId>(resourceIdentifierCollectionSource);
            return primaryResourceIdentifierBuilder.ResourceIdentifierCollectionEnd();
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal DocumentBuilder(DomDocument domDocument, IServiceModel serviceModel, IDocumentWriter documentWriter)
        {
            Contract.Requires(documentWriter != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(domDocument != null);

            this.DocumentWriter = documentWriter;
            this.ServiceModel = serviceModel;
            this.DomDocument = domDocument;
        }

        internal DocumentBuilder(DocumentWriter documentWriter)
        {
            Contract.Requires(documentWriter != null);

            this.DocumentWriter = documentWriter;

            var serviceModel = documentWriter.ServiceModel;
            var domDocument = documentWriter.DomDocument;

            this.ServiceModel = serviceModel;
            this.DomDocument = domDocument;
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region Properties
        internal IServiceModel ServiceModel { get; private set; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDocumentWriter DocumentWriter { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Helper Methods
        /// <summary>Compact document read-write links to read-only links.</summary>
        private void CompactDocumentNodes()
        {
            var domLinks = (IDomLinks)this.DomDocument.GetNode(DomNodeType.Links);
            if (domLinks == null || domLinks.IsReadOnly)
                return;

            var domReadWriteLinks = (DomReadWriteLinks)domLinks;
            var domParentNode = (IContainerNode<DomNodeType>)domReadWriteLinks.ParentNode;

            var apiLinks = domLinks.Links;
            var domReadOnlyLinks = DomReadOnlyLinks.Create(apiLinks);

            domParentNode.ReplaceNode(domReadWriteLinks, domReadOnlyLinks);
        }

        /// <summary>Compact all primary and included resources to read-only resources.</summary>
        private void CompactResourceNodes()
        {
            var domResources = this.DomDocument.DomResources().ToList();
            foreach (var domResource in domResources)
            {
                if (domResource.IsReadOnly)
                    continue;

                var domReadWriteResource = (DomReadWriteResource)domResource;
                var domParentNode = (IContainerNode<DomNodeType>)domReadWriteResource.ParentNode;

                var apiResource = domResource.ApiResource;
                var clrResource = domResource.ClrResource;
                var domReadOnlyResource = DomReadOnlyResource.Create(apiResource, clrResource);

                domParentNode.ReplaceNode(domReadWriteResource, domReadOnlyResource);
            }
        }

        /// <summary>Compact all primary resource identifiers to read-only identifiers.</summary>
        private void CompactResourceIdentifierNodes()
        {
            var domResourceIdentifiers = this.DomDocument.DomResourceIdentitifiers().ToList();
            foreach (var domResourceIdentifier in domResourceIdentifiers)
            {
                if (domResourceIdentifier.IsReadOnly)
                    continue;

                var domReadWriteResourceIdentifier = (DomReadWriteResourceIdentifier)domResourceIdentifier;
                var domParentNode = (IContainerNode<DomNodeType>)domReadWriteResourceIdentifier.ParentNode;

                var apiResourceIdentifier = domResourceIdentifier.ApiResourceIdentifier;
                if (apiResourceIdentifier.IsUndefined())
                {
                    domParentNode.RemoveNode(domReadWriteResourceIdentifier);
                    continue;
                }

                var clrResourceType = domResourceIdentifier.ClrResourceType;
                var domReadOnlyResourceIdentifier = DomReadOnlyResourceIdentifier.Create(apiResourceIdentifier, clrResourceType);

                domParentNode.ReplaceNode(domReadWriteResourceIdentifier, domReadOnlyResourceIdentifier);
            }
        }

        private DocumentType GetDocumentType()
        {
            var documentType = this.DomDocument.GetDocumentType();
            return documentType;
        }
        #endregion
    }
}
