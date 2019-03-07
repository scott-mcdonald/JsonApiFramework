// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
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
        public DomDocument DomDocument { get; private set; }
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
                    break;
                }

                case DocumentType.ResourceIdentifierCollectionDocument:
                case DocumentType.ResourceIdentifierDocument:
                {
                    // Compact all read-write resource identifier nodes to read-only resource identifier nodes.
                    this.CompactResourceIdentifierNodes();
                    break;
                }
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
        #endregion

        // Resource /////////////////////////////////////////////////////////
        #region Generic Versions
        public IPrimaryResourceBuilder<TResource> Resource<TResource>()
            where TResource : class
        {
            var primaryResourceBuilder = new PrimaryResourceBuilder<TResource>(this, this.DomDocument, null);
            return primaryResourceBuilder;
        }

        public IPrimaryResourceBuilder<TResource> Resource<TResource>(TResource clrResource)
            where TResource : class
        {
            var primaryResourceBuilder = new PrimaryResourceBuilder<TResource>(this, this.DomDocument, clrResource);
            return primaryResourceBuilder;
        }
        #endregion

        #region Non-Generic Versions
        public IPrimaryResourceBuilder Resource(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceBuilder = new PrimaryResourceBuilder(this, this.DomDocument, clrResourceType, null);
            return primaryResourceBuilder;
        }

        public IPrimaryResourceBuilder Resource(Type clrResourceType, object clrResource)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceBuilder = new PrimaryResourceBuilder(this, this.DomDocument, clrResourceType, clrResource);
            return primaryResourceBuilder;
        }

        public IPrimaryResourceBuilder Resource(object clrResource)
        {
            if (clrResource == null)
            {
                const string message = "CLR resource can not be null, use the other Resource method that takes the CLR resource type as a single parameter instead.";
                throw new ArgumentNullException(nameof(clrResource), message);
            }

            var clrResourceType        = clrResource.GetType();
            var primaryResourceBuilder = new PrimaryResourceBuilder(this, this.DomDocument, clrResourceType, clrResource);
            return primaryResourceBuilder;
        }
        #endregion

        // ResourceIdentifierCollection /////////////////////////////////////
        #region Generic Versions
        public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection<TResource>()
            where TResource : class
        {
            var primaryResourceIdentifierCollectionBuilder = new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, typeof(TResource));
            return primaryResourceIdentifierCollectionBuilder;
        }

        public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class
        {
            var primaryResourceIdentifierCollectionBuilder = new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, typeof(TResource), clrResourceCollection);
            return primaryResourceIdentifierCollectionBuilder;
        }

        public IDocumentWriter SetResourceIdentifierCollection<TResource>()
            where TResource : class
        {
            var primaryResourceIdentifierBuilder = this.ResourceIdentifierCollection<TResource>();
            return primaryResourceIdentifierBuilder.ResourceIdentifierCollectionEnd();
        }

        public IDocumentWriter SetResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class
        {
            var primaryResourceIdentifierBuilder = this.ResourceIdentifierCollection(clrResourceCollection);
            return primaryResourceIdentifierBuilder.ResourceIdentifierCollectionEnd();
        }
        #endregion

        #region Non-Generic Versions
        public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceIdentifierCollectionBuilder = new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType);
            return primaryResourceIdentifierCollectionBuilder;
        }

        public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection(Type clrResourceType, IEnumerable<object> clrResourceCollection)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceIdentifierCollectionBuilder = new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType, clrResourceCollection);
            return primaryResourceIdentifierCollectionBuilder;
        }

        public IDocumentWriter SetResourceIdentifierCollection(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceIdentifierBuilder = this.ResourceIdentifierCollection(clrResourceType);
            return primaryResourceIdentifierBuilder.ResourceIdentifierCollectionEnd();
        }

        public IDocumentWriter SetResourceIdentifierCollection(Type clrResourceType, IEnumerable<object> clrResourceCollection)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceIdentifierBuilder = this.ResourceIdentifierCollection(clrResourceType, clrResourceCollection);
            return primaryResourceIdentifierBuilder.ResourceIdentifierCollectionEnd();
        }
        #endregion

        // ResourceIdentifier ///////////////////////////////////////////////
        #region Generic Versions
        public IPrimaryResourceIdentifierBuilder ResourceIdentifier<TResource>()
            where TResource : class
        {
            var primaryResourceIdentifierBuilder = new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, typeof(TResource));
            return primaryResourceIdentifierBuilder;
        }

        public IPrimaryResourceIdentifierBuilder ResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class
        {
            var primaryResourceIdentifierBuilder = new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, typeof(TResource), clrResource);
            return primaryResourceIdentifierBuilder;
        }

        public IDocumentWriter SetResourceIdentifier<TResource>()
            where TResource : class
        {
            var primaryResourceIdentifierBuilder = this.ResourceIdentifier<TResource>();
            return primaryResourceIdentifierBuilder.ResourceIdentifierEnd();
        }

        public IDocumentWriter SetResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class
        {
            var primaryResourceIdentifierBuilder = this.ResourceIdentifier(clrResource);
            return primaryResourceIdentifierBuilder.ResourceIdentifierEnd();
        }
        #endregion

        #region Non-Generic Versions
        public IPrimaryResourceIdentifierBuilder ResourceIdentifier(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceIdentifierBuilder = new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType);
            return primaryResourceIdentifierBuilder;
        }

        public IPrimaryResourceIdentifierBuilder ResourceIdentifier(Type clrResourceType, object clrResource)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceIdentifierBuilder = new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType, clrResource);
            return primaryResourceIdentifierBuilder;
        }

        public IPrimaryResourceIdentifierBuilder ResourceIdentifier(object clrResource)
        {
            Contract.Requires(clrResource != null);

            var clrResourceType                  = clrResource.GetType();
            var primaryResourceIdentifierBuilder = new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType, clrResource);
            return primaryResourceIdentifierBuilder;
        }

        public IDocumentWriter SetResourceIdentifier(Type clrResourceType)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceIdentifierBuilder = this.ResourceIdentifier(clrResourceType);
            return primaryResourceIdentifierBuilder.ResourceIdentifierEnd();
        }

        public IDocumentWriter SetResourceIdentifier(Type clrResourceType, object clrResource)
        {
            Contract.Requires(clrResourceType != null);

            var primaryResourceIdentifierBuilder = this.ResourceIdentifier(clrResourceType, clrResource);
            return primaryResourceIdentifierBuilder.ResourceIdentifierEnd();
        }

        public IDocumentWriter SetResourceIdentifier(object clrResource)
        {
            Contract.Requires(clrResource != null);

            var primaryResourceIdentifierBuilder = this.ResourceIdentifier(clrResource);
            return primaryResourceIdentifierBuilder.ResourceIdentifierEnd();
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
            this.ServiceModel   = serviceModel;
            this.DomDocument    = domDocument;
        }

        internal DocumentBuilder(DocumentWriter documentWriter)
        {
            Contract.Requires(documentWriter != null);

            this.DocumentWriter = documentWriter;

            var serviceModel = documentWriter.ServiceModel;
            var domDocument  = documentWriter.DomDocument;

            this.ServiceModel = serviceModel;
            this.DomDocument  = domDocument;
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
            var domParentNode     = (IContainerNode<DomNodeType>)domReadWriteLinks.ParentNode;

            var apiLinks         = domLinks.Links;
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
                var domParentNode        = (IContainerNode<DomNodeType>)domReadWriteResource.ParentNode;

                var apiResource         = domResource.ApiResource;
                var clrResource         = domResource.ClrResource;
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
                var domParentNode                  = (IContainerNode<DomNodeType>)domReadWriteResourceIdentifier.ParentNode;

                var apiResourceIdentifier = domResourceIdentifier.ApiResourceIdentifier;
                if (apiResourceIdentifier.IsUndefined())
                {
                    domParentNode.RemoveNode(domReadWriteResourceIdentifier);
                    continue;
                }

                var clrResourceType               = domResourceIdentifier.ClrResourceType;
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
