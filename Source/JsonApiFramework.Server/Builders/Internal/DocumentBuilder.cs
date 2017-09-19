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
using JsonApiFramework.Server.Hypermedia;
using JsonApiFramework.Server.Hypermedia.Internal;
using JsonApiFramework.Server.Internal.Dom;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    /// <summary>
    /// Implementation of the building of a DOM object tree used in the
    /// writing of a json:api document through a progressive fluent interface.
    /// </summary>
    /// <remarks>
    /// The following is the overall internal strategy used by this and other
    /// builder classes in building the DOM object tree:
    /// 1 Build a DOM object tree based on the order of json:api document
    ///   elements either being added or set through the progressive fluent
    ///   interface.
    /// 2 Ensure resources are only added once based on json:api resource
    ///   identity.
    /// 3 Track what resources are involved in resource linkage.
    /// 4 For each resource in the DOM object tree update the DOM object tree
    ///   for the following reasons:
    ///   4.1 Resolve resource relationships
    ///       4.1.1 Relationship links
    ///       4.1.2 Relationship resource linkage.
    ///   4.2 Resolve resource links
    /// 5 Resolve document links
    /// 6 Compact read-write resources to read-only resources.
    /// </remarks>
    internal class DocumentBuilder : IDocumentBuilder
        , IGetDomDocument
        , IIncludedBuilder
        , IIncludedResourcesBuilder
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
                        // Resolve all read-write resource nodes if needed.
                        this.ResolveResourceNodes();

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

            // Resolve all document nodes if needed.
            this.ResolveDocumentNodes();

            // Compact all read-write document nodes to read-only document nodes.
            this.CompactDocumentNodes();

            // At this point, the DOM object tree is completely built including
            // framework generated hypermedia if needed. Forward the call to the
            // given document writer to generate a json:api compliant document
            // from the DOM object tree.
            var apiDocument = this.DocumentWriter.WriteDocument();
            return apiDocument;
        }
        #endregion

        #region IDocumentBuilder Implementation
        public IDocumentBuilder SetJsonApiVersion(JsonApiVersion jsonApiVersion)
        {
            Contract.Requires(jsonApiVersion != null);

            this.DomDocument.SetDomReadOnlyJsonApiVersion(jsonApiVersion);
            return this;
        }

        public IDocumentBuilder SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            this.DomDocument.SetDomReadOnlyMeta(meta);
            return this;
        }

        public IDocumentLinksBuilder<IDocumentBuilder> Links()
        {
            var documentLinksBuilder = new DocumentLinksBuilder<IDocumentBuilder>(this, this.DomDocument);
            return documentLinksBuilder;
        }

        // Resource /////////////////////////////////////////////////////////
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

        // ResourceCollection ///////////////////////////////////////////////
        public IPrimaryResourceCollectionBuilder<TResource> ResourceCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class, IResource
        {
            var primaryResourceCollectionBuilder = new PrimaryResourceCollectionBuilder<TResource>(this, this.DomDocument, clrResourceCollection);
            return primaryResourceCollectionBuilder;
        }

        public IPrimaryResourceCollectionBuilder<TResource> ResourceCollection<TResource>(IResourceCollectionSource<TResource> resourceCollectionSource)
            where TResource : class, IResource
        {
            Contract.Requires(resourceCollectionSource != null);

            var clrResourceCollection = resourceCollectionSource.GetResourceCollection();
            var primaryResourceCollectionBuilder = new PrimaryResourceCollectionBuilder<TResource>(this, this.DomDocument, clrResourceCollection);
            return primaryResourceCollectionBuilder;
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
            primaryResourceIdentifierBuilder.SetId(resourceId);
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
            primaryResourceIdentifierCollectionBuilder.SetId(resourceIdCollection);
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

        // Errors ///////////////////////////////////////////////////////////
        public IErrorsBuilder Errors()
        {
            var errorsBuilder = new ErrorsBuilder(this, this.DomDocument);
            return errorsBuilder;
        }
        #endregion

        #region IIncludedBuilder Implementation
        public IIncludedResourcesBuilder Included()
        { return this; }
        #endregion

        #region IIncludedResourcesBuilder Implementation
        public IDocumentWriter IncludedEnd()
        { return this; }

        // ToOneIncludedResource /////////////////////////////////////////////
        public IToOneIncludedResourceBuilder<TToResource> ToOne<TFromResource, TToResource>(IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toOneIncludedResource != null);

            var toOneIncludedResourceBuilder = new ToOneIncludedResourceBuilder<TFromResource, TToResource>(this, this.DomDocument, toOneIncludedResource);
            return toOneIncludedResourceBuilder;
        }

        public IToOneIncludedResourceBuilder<TToResource> ToOne<TFromResource, TToResource>(IEnumerable<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toOneIncludedResourceCollection != null);

            var toOneIncludedResourceBuilder = new ToOneIncludedResourceCollectionBuilder<TFromResource, TToResource>(this, this.DomDocument, toOneIncludedResourceCollection.SafeToReadOnlyCollection());
            return toOneIncludedResourceBuilder;
        }

        public IToOneIncludedResourceBuilder<TToResource> ToOne<TFromResource, TToResource>(IToOneIncludedResourceSource<TFromResource, TToResource> toOneIncludedResourceSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toOneIncludedResourceSource != null);

            var toOneIncludedResource = toOneIncludedResourceSource.GetToOneIncludedResource();
            var toOneIncludedResourceBuilder = new ToOneIncludedResourceBuilder<TFromResource, TToResource>(this, this.DomDocument, toOneIncludedResource);
            return toOneIncludedResourceBuilder;
        }

        public IToOneIncludedResourceBuilder<TToResource> ToOne<TFromResource, TToResource>(IToOneIncludedResourceCollectionSource<TFromResource, TToResource> toOneIncludedResourceCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toOneIncludedResourceCollectionSource != null);

            var toOneIncludedResourceCollection = toOneIncludedResourceCollectionSource.GetToOneIncludedResourceCollection();
            var toOneIncludedResourceBuilder = new ToOneIncludedResourceCollectionBuilder<TFromResource, TToResource>(this, this.DomDocument, toOneIncludedResourceCollection.SafeToReadOnlyCollection());
            return toOneIncludedResourceBuilder;
        }

        public IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toOneIncludedResource != null);

            var toOneIncludedResourceBuilder = this.ToOne(toOneIncludedResource);
            toOneIncludedResourceBuilder.ToOneEnd();
            return this;
        }

        public IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(IEnumerable<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toOneIncludedResourceCollection != null);

            var toOneIncludedResourceBuilder = this.ToOne(toOneIncludedResourceCollection);
            toOneIncludedResourceBuilder.ToOneEnd();
            return this;
        }

        public IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(IToOneIncludedResourceSource<TFromResource, TToResource> toOneIncludedResourceSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toOneIncludedResourceSource != null);

            var toOneIncludedResourceBuilder = this.ToOne(toOneIncludedResourceSource);
            toOneIncludedResourceBuilder.ToOneEnd();
            return this;
        }

        public IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(IToOneIncludedResourceCollectionSource<TFromResource, TToResource> toOneIncludedResourceCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toOneIncludedResourceCollectionSource != null);

            var toOneIncludedResourceBuilder = this.ToOne(toOneIncludedResourceCollectionSource);
            toOneIncludedResourceBuilder.ToOneEnd();
            return this;
        }

        // ToManyIncludedResources ////////////////////////////////////////////
        public IToManyIncludedResourcesBuilder<TToResource> ToMany<TFromResource, TToResource>(IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toManyIncludedResources != null);

            var toManyIncludedResourcesBuilder = new ToManyIncludedResourcesBuilder<TFromResource, TToResource>(this, this.DomDocument, toManyIncludedResources);
            return toManyIncludedResourcesBuilder;
        }

        public IToManyIncludedResourcesBuilder<TToResource> ToMany<TFromResource, TToResource>(IEnumerable<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toManyIncludedResourcesCollection != null);

            var toManyIncludedResourcesBuilder = new ToManyIncludedResourcesCollectionBuilder<TFromResource, TToResource>(this, this.DomDocument, toManyIncludedResourcesCollection.SafeToReadOnlyCollection());
            return toManyIncludedResourcesBuilder;
        }

        public IToManyIncludedResourcesBuilder<TToResource> ToMany<TFromResource, TToResource>(IToManyIncludedResourcesSource<TFromResource, TToResource> toManyIncludedResourcesSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toManyIncludedResourcesSource != null);

            var toManyIncludedResources = toManyIncludedResourcesSource.GetToManyIncludedResources();
            var toManyIncludedResourcesBuilder = new ToManyIncludedResourcesBuilder<TFromResource, TToResource>(this, this.DomDocument, toManyIncludedResources);
            return toManyIncludedResourcesBuilder;
        }

        public IToManyIncludedResourcesBuilder<TToResource> ToMany<TFromResource, TToResource>(IToManyIncludedResourcesCollectionSource<TFromResource, TToResource> toManyIncludedResourcesCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toManyIncludedResourcesCollectionSource != null);

            var toManyIncludedResourcesCollection = toManyIncludedResourcesCollectionSource.GetToManyIncludedResourcesCollection();
            var toManyIncludedResourcesBuilder = new ToManyIncludedResourcesCollectionBuilder<TFromResource, TToResource>(this, this.DomDocument, toManyIncludedResourcesCollection.SafeToReadOnlyCollection());
            return toManyIncludedResourcesBuilder;
        }

        public IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toManyIncludedResources != null);

            var toManyIncludedResourcesBuilder = this.ToMany(toManyIncludedResources);
            toManyIncludedResourcesBuilder.ToManyEnd();
            return this;
        }

        public IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(IEnumerable<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toManyIncludedResourcesCollection != null);

            var toManyIncludedResourcesBuilder = this.ToMany(toManyIncludedResourcesCollection);
            toManyIncludedResourcesBuilder.ToManyEnd();
            return this;
        }

        public IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(IToManyIncludedResourcesSource<TFromResource, TToResource> toManyIncludedResourcesSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toManyIncludedResourcesSource != null);

            var toManyIncludedResourcesBuilder = this.ToMany(toManyIncludedResourcesSource);
            toManyIncludedResourcesBuilder.ToManyEnd();
            return this;
        }

        public IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(IToManyIncludedResourcesCollectionSource<TFromResource, TToResource> toManyIncludedResourcesCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(toManyIncludedResourcesCollectionSource != null);

            var toManyIncludedResourcesBuilder = this.ToMany(toManyIncludedResourcesCollectionSource);
            toManyIncludedResourcesBuilder.ToManyEnd();
            return this;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal DocumentBuilder(DocumentWriter documentWriter,
                                 IHypermediaAssemblerRegistry hypermediaAssemblerRegistry,
                                 IHypermediaContext hypermediaContext,
                                 DocumentBuilderContext documentBuilderContext)
        {
            Contract.Requires(documentWriter != null);
            Contract.Requires(documentBuilderContext != null);

            var serviceModel = documentWriter.ServiceModel;
            var domDocument = documentWriter.DomDocument;

            this.DomDocument = domDocument;
            this.ServiceModel = serviceModel;
            this.DocumentWriter = documentWriter;
            this.HypermediaAssemblerRegistry = hypermediaAssemblerRegistry;
            this.HypermediaContext = hypermediaContext;
            this.DocumentBuilderContext = documentBuilderContext;

            this.SetDocumentPathContextNodeAttribute();
        }

        internal DocumentBuilder(DomDocument domDocument,
                                 IServiceModel serviceModel,
                                 IDocumentWriter documentWriter,
                                 IHypermediaAssemblerRegistry hypermediaAssemblerRegistry,
                                 IHypermediaContext hypermediaContext,
                                 DocumentBuilderContext documentBuilderContext)
        {
            Contract.Requires(domDocument != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(documentWriter != null);
            Contract.Requires(hypermediaAssemblerRegistry != null);
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(documentBuilderContext != null);

            this.DomDocument = domDocument;
            this.ServiceModel = serviceModel;
            this.DocumentWriter = documentWriter;
            this.HypermediaAssemblerRegistry = hypermediaAssemblerRegistry;
            this.HypermediaContext = hypermediaContext;
            this.DocumentBuilderContext = documentBuilderContext;

            this.SetDocumentPathContextNodeAttribute();
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region Properties
        internal DocumentBuilderContext DocumentBuilderContext { get; private set; }
        internal IServiceModel ServiceModel { get; private set; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDocumentWriter DocumentWriter { get; set; }
        private IHypermediaAssemblerRegistry HypermediaAssemblerRegistry { get; set; }
        private IHypermediaContext HypermediaContext { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Helper Methods
        /// <summary>Compact document read-write links to read-only links.</summary>
        private void CompactDocumentNodes()
        {
            this.CompactDomLinks();
            this.CompactDomErrors();
        }

        private void CompactDomLinks()
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

        private void CompactDomErrors()
        {
            var domErrors = (IDomErrors)this.DomDocument.GetNode(DomNodeType.Errors);
            if (domErrors == null || domErrors.IsReadOnly)
                return;

            var domReadWriteErrors = (DomReadWriteErrors)domErrors;
            var domParentNode = (IContainerNode<DomNodeType>)domReadWriteErrors.ParentNode;

            var apiErrors = domErrors.Errors;
            var domReadOnlyErrors = DomReadOnlyErrors.Create(apiErrors);

            domParentNode.ReplaceNode(domReadWriteErrors, domReadOnlyErrors);
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

        private static IDocumentPathContext CreateDocumentPathContext(IHypermediaContext hypermediaContext, DocumentBuilderContext documentBuilderContext)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(documentBuilderContext != null);

            var currentRequestUrl = documentBuilderContext.CurrentRequestUrl;
            if (currentRequestUrl == null)
                return null;

            var documentPathContext = new DocumentPathContext(hypermediaContext, currentRequestUrl);
            return documentPathContext;
        }

        private static IDocumentPathContext GetDocumentPathContext(DomDocument domDocument)
        {
            Contract.Requires(domDocument != null);

            var documentPathContext = domDocument.GetDocumentPathContext();
            return documentPathContext;
        }

        private static IResourcePathContext GetResourcePathContext(DomReadWriteResource domReadWriteResource)
        {
            Contract.Requires(domReadWriteResource != null);

            var resourcePathContext = domReadWriteResource.GetResourcePathContext();
            return resourcePathContext;
        }

        private void ResolveDocumentNodes()
        {
            this.ResolveDocumentHypermedia();
        }

        private void ResolveDocumentHypermedia()
        {
            var documentPathContext = GetDocumentPathContext(this.DomDocument);
            if (documentPathContext == null)
                return;

            var hypermediaAssembler = this.HypermediaAssemblerRegistry.SafeGetAssembler(documentPathContext.DocumentSelfPath);

            var domLinks = (IDomLinks)this.DomDocument.GetNode(DomNodeType.Links);
            if (domLinks == null || domLinks.IsReadOnly)
                return;

            var domReadWriteLinks = (DomReadWriteLinks)domLinks;
            foreach (var domLink in domReadWriteLinks.Nodes().Cast<IDomLink>())
            {
                if (domLink.IsReadOnly)
                    continue;

                // Resolve read-write link
                var domReadWriteLink = (DomReadWriteLink)domLink;
                var apiLinkRel = domReadWriteLink.Rel;

                var apiLinkMeta = default(Meta);
                var domMeta = (IDomMeta)domReadWriteLink.GetNode(DomNodeType.Meta);
                if (domMeta != null)
                {
                    apiLinkMeta = domMeta.Meta;
                }

                var linkContext = new LinkContext(apiLinkRel, apiLinkMeta);

                var apiDocumentType = this.GetDocumentType();
                switch (apiDocumentType)
                {
                    case DocumentType.Document:
                    case DocumentType.ErrorsDocument:
                        {
                            var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, linkContext);
                            var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiDocumentLink);
                            domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
                        }
                        break;

                    case DocumentType.EmptyDocument:
                        {
                            var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, null, Enumerable.Empty<object>(), linkContext);
                            var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiDocumentLink);
                            domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
                        }
                        break;

                    case DocumentType.NullDocument:
                        {
                            var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, null, null, linkContext);
                            var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiDocumentLink);
                            domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
                        }
                        break;

                    case DocumentType.ResourceCollectionDocument:
                        {
                            var domResourceCollection = this.DomDocument
                                                            .DomResources(false)
                                                            .ToList();
                            var clrResourceType = domResourceCollection.Any()
                                ? domResourceCollection.First().ClrResourceType
                                : default(Type);
                            var clrResourceCollection = domResourceCollection.Select(x => x.ClrResource)
                                                                             .ToList();

                            var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, clrResourceType, clrResourceCollection, linkContext);
                            var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiDocumentLink);
                            domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
                        }
                        break;

                    case DocumentType.ResourceDocument:
                        {
                            var domResource = this.DomDocument
                                                  .DomResources(false)
                                                  .SingleOrDefault();
                            var clrResourceType = domResource != null ? domResource.ClrResourceType : default(Type);
                            var clrResource = domResource != null ? domResource.ClrResource : default(object);
                            var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, clrResourceType, clrResource, linkContext);
                            var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiDocumentLink);
                            domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
                        }
                        break;

                    case DocumentType.ResourceIdentifierCollectionDocument:
                        {
                            var domResourceIdentifierCollection = this.DomDocument
                                                                      .DomResourceIdentitifiers()
                                                                      .ToList();
                            var clrResourceType = domResourceIdentifierCollection.Any()
                                ? domResourceIdentifierCollection.First().ClrResourceType
                                : default(Type);
                            var clrResourceCollection = Enumerable.Empty<object>();
                            if (clrResourceType != null)
                            {
                                var resourceType = this.ServiceModel.GetResourceType(clrResourceType);
                                clrResourceCollection = domResourceIdentifierCollection
                                    .Select(domResourceIdentifier =>
                                    {
                                        var clrResource = resourceType.CreateClrObject();

                                        var apiResourceId = domResourceIdentifier.ApiResourceId;
                                        resourceType.SetClrId(clrResource, apiResourceId);
                                        return clrResource;
                                    })
                                    .ToList();
                            }

                            var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, clrResourceType, clrResourceCollection, linkContext);
                            var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiDocumentLink);
                            domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
                        }
                        break;

                    case DocumentType.ResourceIdentifierDocument:
                        {
                            var domResourceIdentifier = this.DomDocument
                                                            .DomResourceIdentitifiers()
                                                            .SingleOrDefault();
                            var clrResourceType = domResourceIdentifier != null ? domResourceIdentifier.ClrResourceType : default(Type);
                            var clrResource = default(object);
                            if (clrResourceType != null)
                            {
                                var resourceType = this.ServiceModel.GetResourceType(clrResourceType);
                                clrResource = resourceType.CreateClrObject();

                                var apiResourceId = domResourceIdentifier.ApiResourceId;
                                resourceType.SetClrId(clrResource, apiResourceId);
                            }

                            var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, clrResourceType, clrResource, linkContext);
                            var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiDocumentLink);
                            domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                                   .FormatWith(typeof(DocumentType).Name, apiDocumentType);
                            throw new InternalErrorException(detail);
                        }
                }
            }
        }

        private void ResolveResourceNodes()
        {
            foreach (var domResource in this.DomDocument.DomResources())
            {
                this.ResolveResourceHypermedia(domResource);
            }
        }

        private void ResolveResourceHypermedia(IDomResource domResource)
        {
            Contract.Requires(domResource != null);

            if (domResource.IsReadOnly)
                return;

            var hypermediaContext = this.HypermediaContext;

            var domReadWriteResource = (DomReadWriteResource)domResource;

            var resourcePathContext = GetResourcePathContext(domReadWriteResource);
            var hypermediaAssembler = this.HypermediaAssemblerRegistry.SafeGetAssembler(resourcePathContext.ResourceSelfBasePath);

            var clrResourceType = domResource.ClrResourceType;
            var clrResource = domResource.ClrResource;

            this.ResolveResourceRelationships(domReadWriteResource, hypermediaContext, hypermediaAssembler, resourcePathContext, clrResourceType, clrResource);
            ResolveResourceLinks(domReadWriteResource, hypermediaContext, hypermediaAssembler, resourcePathContext, clrResourceType, clrResource);
        }

        private static void ResolveResourceLinks(DomReadWriteResource domReadWriteResource,
                                                 IHypermediaContext hypermediaContext,
                                                 IHypermediaAssembler hypermediaAssembler,
                                                 IResourcePathContext resourcePathContext,
                                                 Type clrResourceType,
                                                 object clrResource)
        {
            var domLinks = (IDomLinks)domReadWriteResource.GetNode(DomNodeType.Links);
            if (domLinks == null || domLinks.IsReadOnly)
                return;

            var domReadWriteLinks = (DomReadWriteLinks)domLinks;
            foreach (var domLink in domReadWriteLinks.Nodes().Cast<IDomLink>())
            {
                ResolveResourceLink(domLink, hypermediaContext, hypermediaAssembler, resourcePathContext, clrResourceType, clrResource, domReadWriteLinks);
            }
        }

        private static void ResolveResourceLink(IDomLink domLink,
                                                IHypermediaContext hypermediaContext,
                                                IHypermediaAssembler hypermediaAssembler,
                                                IResourcePathContext resourcePathContext,
                                                Type clrResourceType,
                                                object clrResource,
                                                DomReadWriteLinks domReadWriteLinks)
        {
            if (domLink.IsReadOnly)
                return;

            // Resolve read-write resource link
            var domReadWriteLink = (DomReadWriteLink)domLink;
            var apiLinkRel = domReadWriteLink.Rel;

            var apiLinkMeta = default(Meta);
            var domLinkMeta = (IDomMeta)domReadWriteLink.GetNode(DomNodeType.Meta);
            if (domLinkMeta != null)
            {
                apiLinkMeta = domLinkMeta.Meta;
            }

            // Create link.
            var linkContext = new LinkContext(apiLinkRel, apiLinkMeta);
            var apiResourceLink = hypermediaAssembler.CreateResourceLink(hypermediaContext, resourcePathContext, clrResourceType, clrResource, linkContext);

            // Replace the old DOM read-write link node with a new DOM read-only link created by the framework.
            var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiResourceLink);
            domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
        }

        private void ResolveResourceRelationships(DomReadWriteResource domReadWriteResource,
                                                  IHypermediaContext hypermediaContext,
                                                  IHypermediaAssembler hypermediaAssembler,
                                                  IResourcePathContext resourcePathContext,
                                                  Type clrResourceType,
                                                  object clrResource)
        {
            var domRelationships = (IDomRelationships)domReadWriteResource.GetNode(DomNodeType.Relationships);
            if (domRelationships == null || domRelationships.IsReadOnly)
                return;

            var domReadWriteRelationships = (DomReadWriteRelationships)domRelationships;
            foreach (var domRelationship in domReadWriteRelationships.Nodes().Cast<IDomRelationship>())
            {
                this.ResolveResourceRelationship(domRelationship, hypermediaContext, hypermediaAssembler, resourcePathContext, clrResourceType, clrResource, domReadWriteRelationships);
            }
        }

        private void ResolveResourceRelationship(IDomRelationship domRelationship,
                                                 IHypermediaContext hypermediaContext,
                                                 IHypermediaAssembler hypermediaAssembler,
                                                 IResourcePathContext resourcePathContext,
                                                 Type clrResourceType,
                                                 object clrResource,
                                                 DomReadWriteRelationships domReadWriteRelationships)
        {
            if (domRelationship.IsReadOnly)
                return;

            // Resolve read-write relationship
            var domReadWriteRelationship = (DomReadWriteRelationship)domRelationship;

            // .. Rel
            var apiRelationshipRel = domReadWriteRelationship.Rel;

            // .. Links
            var linkContexts = new List<ILinkContext>();
            var domRelationshipLinks = (IDomLinks)domReadWriteRelationship.GetNode(DomNodeType.Links);
            if (domRelationshipLinks != null)
            {
                if (domRelationshipLinks.IsReadOnly)
                {
                    // A read-write relationship contains unexpected read-only relationship links.
                    var detail = ServerErrorStrings.DomExceptionDetailReadWriteNodeHasUnexpectedReadOnlyChildNode
                                                   .FormatWith(DomNodeType.Relationship, DomNodeType.Links);
                    throw new DomException(detail);
                }

                var domReadWriteRelationshipLinks = (DomReadWriteLinks)domRelationshipLinks;
                foreach (var domLink in domReadWriteRelationshipLinks.Nodes().Cast<IDomLink>())
                {
                    if (domLink.IsReadOnly)
                    {
                        // A read-write relationship contains unexpected read-only relationship link.
                        var detail = ServerErrorStrings.DomExceptionDetailReadWriteNodeHasUnexpectedReadOnlyChildNode
                                                       .FormatWith(DomNodeType.Relationship, DomNodeType.Link);
                        throw new DomException(detail);
                    }

                    // Resolve read-write relationship link
                    var domReadWriteLink = (DomReadWriteLink)domLink;
                    var apiLinkRel = domReadWriteLink.Rel;

                    var apiLinkMeta = default(Meta);
                    var domMeta = (IDomMeta)domReadWriteLink.GetNode(DomNodeType.Meta);
                    if (domMeta != null)
                    {
                        apiLinkMeta = domMeta.Meta;
                    }

                    var linkContext = new LinkContext(apiLinkRel, apiLinkMeta);
                    linkContexts.Add(linkContext);
                }
            }

            // .. Data
            var resourceType = this.ServiceModel.GetResourceType(clrResourceType);
            var fromApiResourceIdentifier = resourceType.GetApiResourceIdentifier(clrResource);

            var resourceLinkageKey = new ResourceLinkageKey(fromApiResourceIdentifier, apiRelationshipRel);
            ResourceLinkage resourceLinkage;
            var hasResourceLinkage = this.DocumentBuilderContext.TryGetResourceLinkage(resourceLinkageKey, out resourceLinkage);

            // .. Meta
            var apiRelationshipMeta = default(Meta);
            var domRelationshipMeta = (IDomMeta)domReadWriteRelationship.GetNode(DomNodeType.Meta);
            if (domRelationshipMeta != null)
            {
                apiRelationshipMeta = domRelationshipMeta.Meta;
            }

            // Create the correct relationship context based on resource linkage (if any).
            RelationshipContext relationshipContext;
            if (hasResourceLinkage)
            {
                var resourceLinkageType = resourceLinkage.Type;
                switch (resourceLinkageType)
                {
                    case ResourceLinkageType.ToOneResourceLinkage:
                        {
                            var toOneResourceLinkage = resourceLinkage.ToOneResourceLinkage;
                            relationshipContext = new ToOneRelationshipContext(apiRelationshipRel, linkContexts, toOneResourceLinkage, apiRelationshipMeta);
                        }
                        break;

                    case ResourceLinkageType.ToManyResourceLinkage:
                        {
                            var toManyResourceLinkage = resourceLinkage.ToManyResourceLinkage;
                            relationshipContext = new ToManyRelationshipContext(apiRelationshipRel, linkContexts, toManyResourceLinkage, apiRelationshipMeta);
                        }
                        break;

                    default:
                        {
                            var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                                   .FormatWith(typeof(ResourceLinkageType).Name, resourceLinkageType);
                            throw new InternalErrorException(detail);
                        }
                }
            }
            else
            {
                relationshipContext = new RelationshipContext(apiRelationshipRel, linkContexts, apiRelationshipMeta);
            }

            // Create relationship.
            var apiResourceRelationship = hypermediaAssembler.CreateResourceRelationship(hypermediaContext, resourcePathContext, clrResourceType, clrResource, relationshipContext);

            // Replace the old DOM read-write relationship node with a new DOM read-only relationship created by the framework.
            var domReadOnlyRelationship = DomReadOnlyRelationship.Create(apiRelationshipRel, apiResourceRelationship);
            domReadWriteRelationships.ReplaceNode(domReadWriteRelationship, domReadOnlyRelationship);
        }

        private void SetDocumentPathContextNodeAttribute()
        {
            var documentPathContext = CreateDocumentPathContext(this.HypermediaContext, this.DocumentBuilderContext);
            if (documentPathContext == null)
                return;

            this.DomDocument.SetDocumentPathContext(documentPathContext);
        }

        private DocumentType GetDocumentType()
        {
            var documentType = this.DomDocument.GetDocumentType();
            return documentType;
        }
        #endregion
    }
}
