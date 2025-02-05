﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
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

namespace JsonApiFramework.Server.Internal;

using DomNode = Node<DomNodeType>;

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
    public DomDocument DomDocument { get; }
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

                // Prune all resource relationships from sparse fieldsets if needed.
                this.PruneResourceRelationshipNodes();

                // Compact all read-write resource nodes to read-only resource nodes.
                this.CompactResourceNodes();

                // Sort included resource nodes
                this.SortIncludedResourceNodes();

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

    // ResourceCollection ///////////////////////////////////////////////
    #region Generic Versions
    public IPrimaryResourceCollectionBuilder<TResource> ResourceCollection<TResource>(Type clrResourceType, IEnumerable<TResource> clrResourceCollection)
        where TResource : class
    {
        return new PrimaryResourceCollectionBuilder<TResource>(this, this.DomDocument, clrResourceType, clrResourceCollection);
    }

    public IPrimaryResourceCollectionBuilder<TResource> ResourceCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
        where TResource : class
    {
        return new PrimaryResourceCollectionBuilder<TResource>(this, this.DomDocument, typeof(TResource), clrResourceCollection);
    }
    #endregion

    #region Non-Generic Versions
    public IPrimaryResourceCollectionBuilder ResourceCollection(Type clrResourceType, IEnumerable<object> clrResourceCollection)
    {
        return new PrimaryResourceCollectionBuilder(this, this.DomDocument, clrResourceType, clrResourceCollection);
    }

    public IPrimaryResourceCollectionBuilder ResourceCollection(IEnumerable<object> clrResourceCollection)
    {
        // ReSharper disable PossibleMultipleEnumeration
        var clrResourceType = clrResourceCollection?.FirstOrDefault()?.GetType();
        return new PrimaryResourceCollectionBuilder(this, this.DomDocument, clrResourceType, clrResourceCollection);
        // ReSharper restore PossibleMultipleEnumeration
    }
    #endregion

    // Resource /////////////////////////////////////////////////////////
    #region Generic Versions
    public IPrimaryResourceBuilder<TResource> Resource<TResource>(Type clrResourceType, TResource clrResource)
        where TResource : class
    {
        return new PrimaryResourceBuilder<TResource>(this, this.DomDocument, clrResourceType, clrResource);
    }

    public IPrimaryResourceBuilder<TResource> Resource<TResource>(TResource clrResource)
        where TResource : class
    {
        return new PrimaryResourceBuilder<TResource>(this, this.DomDocument, typeof(TResource), clrResource);
    }
    #endregion

    #region Non-Generic Versions
    public IPrimaryResourceBuilder Resource(Type clrResourceType, object clrResource)
    {
        return new PrimaryResourceBuilder(this, this.DomDocument, clrResourceType, clrResource);
    }

    public IPrimaryResourceBuilder Resource(object clrResource)
    {
        var clrResourceType = clrResource?.GetType();
        return new PrimaryResourceBuilder(this, this.DomDocument, clrResourceType, clrResource);
    }
    #endregion

    // ResourceIdentifierCollection /////////////////////////////////////
    #region Generic Versions
    public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection<TResource>()
        where TResource : class
    {
        return new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, typeof(TResource));
    }

    public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection<TResource>(Type clrResourceType, IEnumerable<TResource> clrResourceCollection)
        where TResource : class
    {
        Contract.Requires(clrResourceType != null);

        return new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType, clrResourceCollection);
    }

    public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
        where TResource : class
    {
        return new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, typeof(TResource), clrResourceCollection);
    }

    public IDocumentWriter SetResourceIdentifierCollection<TResource>()
        where TResource : class
    {
        return this.ResourceIdentifierCollection<TResource>().ResourceIdentifierCollectionEnd();
    }

    public IDocumentWriter SetResourceIdentifierCollection<TResource>(Type clrResourceType, IEnumerable<TResource> clrResourceCollection)
        where TResource : class
    {
        Contract.Requires(clrResourceType != null);

        return this.ResourceIdentifierCollection(clrResourceType, clrResourceCollection).ResourceIdentifierCollectionEnd();
    }

    public IDocumentWriter SetResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
        where TResource : class
    {
        return this.ResourceIdentifierCollection(clrResourceCollection).ResourceIdentifierCollectionEnd();
    }
    #endregion

    #region Non-Generic Versions
    public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection(Type clrResourceType)
    {
        Contract.Requires(clrResourceType != null);

        return new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType);
    }

    public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection(Type clrResourceType, IEnumerable<object> clrResourceCollection)
    {
        Contract.Requires(clrResourceType != null);

        return new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType, clrResourceCollection);
    }

    public IPrimaryResourceIdentifierCollectionBuilder ResourceIdentifierCollection(IEnumerable<object> clrResourceCollection)
    {
        // ReSharper disable PossibleMultipleEnumeration
        var clrResourceType = clrResourceCollection?.FirstOrDefault()?.GetType();
        return new PrimaryResourceIdentifierCollectionBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType, clrResourceCollection);
        // ReSharper restore PossibleMultipleEnumeration
    }

    public IDocumentWriter SetResourceIdentifierCollection(Type clrResourceType)
    {
        Contract.Requires(clrResourceType != null);

        return this.ResourceIdentifierCollection(clrResourceType).ResourceIdentifierCollectionEnd();
    }

    public IDocumentWriter SetResourceIdentifierCollection(Type clrResourceType, IEnumerable<object> clrResourceCollection)
    {
        Contract.Requires(clrResourceType != null);

        return this.ResourceIdentifierCollection(clrResourceType, clrResourceCollection).ResourceIdentifierCollectionEnd();
    }

    public IDocumentWriter SetResourceIdentifierCollection(IEnumerable<object> clrResourceCollection)
    {
        return this.ResourceIdentifierCollection(clrResourceCollection).ResourceIdentifierCollectionEnd();
    }
    #endregion

    // ResourceIdentifier ///////////////////////////////////////////////
    #region Generic Versions
    public IPrimaryResourceIdentifierBuilder ResourceIdentifier<TResource>()
        where TResource : class
    {
        return new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, typeof(TResource));
    }

    public IPrimaryResourceIdentifierBuilder ResourceIdentifier<TResource>(Type clrResourceType, TResource clrResource)
        where TResource : class
    {
        Contract.Requires(clrResourceType != null);

        return new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType, clrResource);
    }

    public IPrimaryResourceIdentifierBuilder ResourceIdentifier<TResource>(TResource clrResource)
        where TResource : class
    {
        return new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, typeof(TResource), clrResource);
    }

    public IDocumentWriter SetResourceIdentifier<TResource>()
        where TResource : class
    {
        return this.ResourceIdentifier<TResource>().ResourceIdentifierEnd();
    }

    public IDocumentWriter SetResourceIdentifier<TResource>(Type clrResourceType, TResource clrResource)
        where TResource : class
    {
        Contract.Requires(clrResourceType != null);

        return this.ResourceIdentifier(clrResourceType, clrResource).ResourceIdentifierEnd();
    }

    public IDocumentWriter SetResourceIdentifier<TResource>(TResource clrResource)
        where TResource : class
    {
        return this.ResourceIdentifier(clrResource).ResourceIdentifierEnd();
    }
    #endregion

    #region Non-Generic Versions
    public IPrimaryResourceIdentifierBuilder ResourceIdentifier(Type clrResourceType)
    {
        Contract.Requires(clrResourceType != null);

        return new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType);
    }

    public IPrimaryResourceIdentifierBuilder ResourceIdentifier(Type clrResourceType, object clrResource)
    {
        Contract.Requires(clrResourceType != null);

        return new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType, clrResource);
    }

    public IPrimaryResourceIdentifierBuilder ResourceIdentifier(object clrResource)
    {
        var clrResourceType = clrResource?.GetType();
        return new PrimaryResourceIdentifierBuilder(this, this.ServiceModel, this.DomDocument, clrResourceType, clrResource);
    }

    public IDocumentWriter SetResourceIdentifier(Type clrResourceType)
    {
        return this.ResourceIdentifier(clrResourceType).ResourceIdentifierEnd();
    }

    public IDocumentWriter SetResourceIdentifier(Type clrResourceType, object clrResource)
    {
        return this.ResourceIdentifier(clrResourceType, clrResource).ResourceIdentifierEnd();
    }

    public IDocumentWriter SetResourceIdentifier(object clrResource)
    {
        return this.ResourceIdentifier(clrResource).ResourceIdentifierEnd();
    }
    #endregion

    #region IIncludedBuilder Implementation
    public IIncludedResourcesBuilder Included()
    {
        return this;
    }
    #endregion

    #region IIncludedResourcesBuilder Implementation
    public IDocumentWriter IncludedEnd()
    {
        return this;
    }

    #region Generic Versions
    // ToOne ////////////////////////////////////////////////////////////
    public IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
        where TFromResource : class
        where TToResource : class
    {
        return new ToOneIncludedResourceBuilder<TFromResource, TToResource>(this, this.DomDocument, toOneIncludedResource);
    }

    public IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(IEnumerable<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
        where TFromResource : class
        where TToResource : class
    {
        return new ToOneIncludedResourceCollectionBuilder<TFromResource, TToResource>(this, this.DomDocument, toOneIncludedResourceCollection.SafeToReadOnlyCollection());
    }

    public IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
        where TFromResource : class
        where TToResource : class
    {
        this.Include(toOneIncludedResource).IncludeEnd();
        return this;
    }

    public IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IEnumerable<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
        where TFromResource : class
        where TToResource : class
    {
        this.Include(toOneIncludedResourceCollection).IncludeEnd();
        return this;
    }

    // ToMany ///////////////////////////////////////////////////////////
    public IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
        where TFromResource : class
        where TToResource : class
    {
        return new ToManyIncludedResourcesBuilder<TFromResource, TToResource>(this, this.DomDocument, toManyIncludedResources);
    }

    public IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(IEnumerable<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
        where TFromResource : class
        where TToResource : class
    {
        return new ToManyIncludedResourcesCollectionBuilder<TFromResource, TToResource>(this, this.DomDocument, toManyIncludedResourcesCollection.SafeToReadOnlyCollection());
    }

    public IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
        where TFromResource : class
        where TToResource : class
    {
        this.Include(toManyIncludedResources).IncludeEnd();
        return this;
    }

    public IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IEnumerable<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
        where TFromResource : class
        where TToResource : class
    {
        this.Include(toManyIncludedResourcesCollection).IncludeEnd();
        return this;
    }
    #endregion

    #region Non-Generic Versions
    // ToOne ////////////////////////////////////////////////////////////
    public IToOneIncludedResourceBuilder Include(IToOneIncludedResource toOneIncludedResource)
    {
        return new ToOneIncludedResourceBuilder(this, this.DomDocument, toOneIncludedResource);
    }

    public IToOneIncludedResourceBuilder Include(IEnumerable<IToOneIncludedResource> toOneIncludedResourceCollection)
    {
        return new ToOneIncludedResourceCollectionBuilder(this, this.DomDocument, toOneIncludedResourceCollection.SafeToReadOnlyCollection());
    }

    public IIncludedResourcesBuilder AddInclude(IToOneIncludedResource toOneIncludedResource)
    {
        this.Include(toOneIncludedResource).IncludeEnd();
        return this;
    }

    public IIncludedResourcesBuilder AddInclude(IEnumerable<IToOneIncludedResource> toOneIncludedResourceCollection)
    {
        this.Include(toOneIncludedResourceCollection).IncludeEnd();
        return this;
    }

    // ToMany ///////////////////////////////////////////////////////////
    public IToManyIncludedResourcesBuilder Include(IToManyIncludedResources toManyIncludedResources)
    {
        return new ToManyIncludedResourcesBuilder(this, this.DomDocument, toManyIncludedResources);
    }

    public IToManyIncludedResourcesBuilder Include(IEnumerable<IToManyIncludedResources> toManyIncludedResourcesCollection)
    {
        return new ToManyIncludedResourcesCollectionBuilder(this, this.DomDocument, toManyIncludedResourcesCollection.SafeToReadOnlyCollection());
    }

    public IIncludedResourcesBuilder AddInclude(IToManyIncludedResources toManyIncludedResources)
    {
        this.Include(toManyIncludedResources).IncludeEnd();
        return this;
    }

    public IIncludedResourcesBuilder AddInclude(IEnumerable<IToManyIncludedResources> toManyIncludedResourcesCollection)
    {
        this.Include(toManyIncludedResourcesCollection).IncludeEnd();
        return this;
    }
    #endregion
    #endregion

    // Errors ///////////////////////////////////////////////////////////
    public IErrorsBuilder Errors()
    {
        return new ErrorsBuilder(this, this.DomDocument);
    }
    #endregion

    // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
    #region Constructors
    internal DocumentBuilder(DocumentWriter               documentWriter,
                             IHypermediaAssemblerRegistry hypermediaAssemblerRegistry,
                             IHypermediaContext           hypermediaContext,
                             DocumentBuilderContext       documentBuilderContext)
    {
        Contract.Requires(documentWriter != null);
        Contract.Requires(documentBuilderContext != null);

        var serviceModel = documentWriter.ServiceModel;
        var domDocument  = documentWriter.DomDocument;

        this.DomDocument                 = domDocument;
        this.ServiceModel                = serviceModel;
        this.DocumentWriter              = documentWriter;
        this.HypermediaAssemblerRegistry = hypermediaAssemblerRegistry;
        this.HypermediaContext           = hypermediaContext;
        this.DocumentBuilderContext      = documentBuilderContext;

        this.SetDocumentPathContextNodeAttribute();
    }

    internal DocumentBuilder(DomDocument                  domDocument,
                             IServiceModel                serviceModel,
                             IDocumentWriter              documentWriter,
                             IHypermediaAssemblerRegistry hypermediaAssemblerRegistry,
                             IHypermediaContext           hypermediaContext,
                             DocumentBuilderContext       documentBuilderContext)
    {
        Contract.Requires(domDocument != null);
        Contract.Requires(serviceModel != null);
        Contract.Requires(documentWriter != null);
        Contract.Requires(hypermediaAssemblerRegistry != null);
        Contract.Requires(hypermediaContext != null);
        Contract.Requires(documentBuilderContext != null);

        this.DomDocument                 = domDocument;
        this.ServiceModel                = serviceModel;
        this.DocumentWriter              = documentWriter;
        this.HypermediaAssemblerRegistry = hypermediaAssemblerRegistry;
        this.HypermediaContext           = hypermediaContext;
        this.DocumentBuilderContext      = documentBuilderContext;

        this.SetDocumentPathContextNodeAttribute();
    }
    #endregion

    // INTERNAL PROPERTIES //////////////////////////////////////////////
    #region Properties
    internal DocumentBuilderContext DocumentBuilderContext { get; private set; }
    internal IServiceModel          ServiceModel           { get; private set; }
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    private IDocumentWriter              DocumentWriter              { get; set; }
    private IHypermediaAssemblerRegistry HypermediaAssemblerRegistry { get; set; }
    private IHypermediaContext           HypermediaContext           { get; set; }
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
        var domParentNode     = (IContainerNode<DomNodeType>)domReadWriteLinks.ParentNode;

        var apiLinks         = domLinks.Links;
        var domReadOnlyLinks = DomReadOnlyLinks.Create(apiLinks);

        domParentNode.ReplaceNode(domReadWriteLinks, domReadOnlyLinks);
    }

    private void CompactDomErrors()
    {
        var domErrors = (IDomErrors)this.DomDocument.GetNode(DomNodeType.Errors);
        if (domErrors == null || domErrors.IsReadOnly)
            return;

        var domReadWriteErrors = (DomReadWriteErrors)domErrors;
        var domParentNode      = (IContainerNode<DomNodeType>)domReadWriteErrors.ParentNode;

        var apiErrors         = domErrors.Errors;
        var domReadOnlyErrors = DomReadOnlyErrors.Create(apiErrors);

        domParentNode.ReplaceNode(domReadWriteErrors, domReadOnlyErrors);
    }

    /// <summary>Compact all primary and included resources to read-only resources.</summary>
    private void CompactResourceNodes()
    {
        var domResources = this.DomDocument.DomResources(enumerateIncludedResources: true).ToList();
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

    private DocumentType GetDocumentType()
    {
        var documentType = this.DomDocument.GetDocumentType();
        return documentType;
    }

    private static IResourcePathContext GetResourcePathContext(DomReadWriteResource domReadWriteResource)
    {
        Contract.Requires(domReadWriteResource != null);

        var resourcePathContext = domReadWriteResource.GetResourcePathContext();
        return resourcePathContext;
    }

    private void PruneResourceRelationshipNodes()
    {
        var areSparseFieldsetsEnabled = this.DocumentBuilderContext.SparseFieldsetsEnabled;
        if (areSparseFieldsetsEnabled == false)
            return;

        foreach (var domResource in this.DomDocument.DomResources(enumerateIncludedResources: true))
        {
            this.PruneResourceRelationshipNodes(domResource);
        }
    }

    private void PruneResourceRelationshipNodes(IDomResource domResource)
    {
        Contract.Requires(domResource != null);

        if (domResource.IsReadOnly)
            return;

        var queryParameters    = this.DocumentBuilderContext.QueryParameters;
        var apiType            = domResource.ApiResourceType;
        var useSparseFieldsets = queryParameters.ContainsField(apiType);
        if (useSparseFieldsets == false)
            return;

        var domReadWriteResource = (DomReadWriteResource)domResource;

        var domRelationships = (IDomRelationships)domReadWriteResource.GetNode(DomNodeType.Relationships);
        if (domRelationships == null || domRelationships.IsReadOnly)
            return;

        var domReadWriteRelationships = (DomReadWriteRelationships)domRelationships;
        foreach (var domRelationship in domReadWriteRelationships.Nodes().Cast<IDomRelationship>())
        {
            var apiField = domRelationship.Rel;
            if (queryParameters.ContainsField(apiType, apiField))
                continue;

            // Prune this relationship.
            // 1. If this doesn't have resource linkage, just remove relationship node.
            // 2. If this does have resource linkage, remove the links and meta but keep the resource linkage.
            var apiRelationship     = domRelationship.Relationship;
            var apiRelationshipType = apiRelationship.GetRelationshipType();
            switch (apiRelationshipType)
            {
                case RelationshipType.Relationship:
                {
                    domReadWriteRelationships.RemoveNode((DomNode)domRelationship);
                }
                    break;

                case RelationshipType.ToOneRelationship:
                {
                    var apiRelationshipDataOnly = new ToOneRelationship
                    {
                        Data = apiRelationship.GetToOneResourceLinkage()
                    };
                    var domReadOnlyRelationship = DomReadOnlyRelationship.Create(apiField, apiRelationshipDataOnly);

                    domReadWriteRelationships.ReplaceNode((DomNode)domRelationship, domReadOnlyRelationship);
                }
                    break;

                case RelationshipType.ToManyRelationship:
                {
                    var apiRelationshipDataOnly = new ToManyRelationship
                    {
                        Data = apiRelationship.GetToManyResourceLinkage().SafeToList()
                    };
                    var domReadOnlyRelationship = DomReadOnlyRelationship.Create(apiField, apiRelationshipDataOnly);

                    domReadWriteRelationships.ReplaceNode((DomNode)domRelationship, domReadOnlyRelationship);
                }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
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
            var apiLinkRel       = domReadWriteLink.Rel;

            var apiLinkMeta = default(Meta);
            var domMeta     = (IDomMeta)domReadWriteLink.GetNode(DomNodeType.Meta);
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
                    if (apiDocumentLink == null)
                    {
                        // Unable to create document link, remove link node in DOM tree.
                        domReadWriteLinks.RemoveNode(domReadWriteLink);
                        return;
                    }

                    var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiDocumentLink);
                    domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
                }
                    break;

                case DocumentType.EmptyDocument:
                {
                    var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, null, Enumerable.Empty<object>(), linkContext);
                    if (apiDocumentLink == null)
                    {
                        // Unable to create document link, remove link node in DOM tree.
                        domReadWriteLinks.RemoveNode(domReadWriteLink);
                        return;
                    }

                    var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiDocumentLink);
                    domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
                }
                    break;

                case DocumentType.NullDocument:
                {
                    var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, null, null, linkContext);
                    if (apiDocumentLink == null)
                    {
                        // Unable to create document link, remove link node in DOM tree.
                        domReadWriteLinks.RemoveNode(domReadWriteLink);
                        return;
                    }

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
                    if (apiDocumentLink == null)
                    {
                        // Unable to create document link, remove link node in DOM tree.
                        domReadWriteLinks.RemoveNode(domReadWriteLink);
                        return;
                    }

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
                    var clrResource     = domResource != null ? domResource.ClrResource : default(object);
                    var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, clrResourceType, clrResource, linkContext);
                    if (apiDocumentLink == null)
                    {
                        // Unable to create document link, remove link node in DOM tree.
                        domReadWriteLinks.RemoveNode(domReadWriteLink);
                        return;
                    }

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
                    if (apiDocumentLink == null)
                    {
                        // Unable to create document link, remove link node in DOM tree.
                        domReadWriteLinks.RemoveNode(domReadWriteLink);
                        return;
                    }

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
                    var clrResource     = default(object);
                    if (clrResourceType != null)
                    {
                        var resourceType = this.ServiceModel.GetResourceType(clrResourceType);
                        clrResource = resourceType.CreateClrObject();

                        var apiResourceId = domResourceIdentifier.ApiResourceId;
                        resourceType.SetClrId(clrResource, apiResourceId);
                    }

                    var apiDocumentLink = hypermediaAssembler.CreateDocumentLink(this.HypermediaContext, documentPathContext, apiDocumentType, clrResourceType, clrResource, linkContext);
                    if (apiDocumentLink == null)
                    {
                        // Unable to create document link, remove link node in DOM tree.
                        domReadWriteLinks.RemoveNode(domReadWriteLink);
                        return;
                    }

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
        foreach (var domResource in this.DomDocument.DomResources(enumerateIncludedResources: true))
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
        var clrResource     = domResource.ClrResource;

        this.ResolveResourceRelationships(domReadWriteResource, hypermediaContext, hypermediaAssembler, resourcePathContext, clrResourceType, clrResource);
        ResolveResourceLinks(domReadWriteResource, hypermediaContext, hypermediaAssembler, resourcePathContext, clrResourceType, clrResource);
    }

    private static void ResolveResourceLinks(DomReadWriteResource domReadWriteResource,
                                             IHypermediaContext   hypermediaContext,
                                             IHypermediaAssembler hypermediaAssembler,
                                             IResourcePathContext resourcePathContext,
                                             Type                 clrResourceType,
                                             object               clrResource)
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

    private static void ResolveResourceLink(IDomLink             domLink,
                                            IHypermediaContext   hypermediaContext,
                                            IHypermediaAssembler hypermediaAssembler,
                                            IResourcePathContext resourcePathContext,
                                            Type                 clrResourceType,
                                            object               clrResource,
                                            DomReadWriteLinks    domReadWriteLinks)
    {
        if (domLink.IsReadOnly)
            return;

        // Resolve read-write resource link
        var domReadWriteLink = (DomReadWriteLink)domLink;
        var apiLinkRel       = domReadWriteLink.Rel;

        var apiLinkMeta = default(Meta);
        var domLinkMeta = (IDomMeta)domReadWriteLink.GetNode(DomNodeType.Meta);
        if (domLinkMeta != null)
        {
            apiLinkMeta = domLinkMeta.Meta;
        }

        // Create link.
        var linkContext     = new LinkContext(apiLinkRel, apiLinkMeta);
        var apiResourceLink = hypermediaAssembler.CreateResourceLink(hypermediaContext, resourcePathContext, clrResourceType, clrResource, linkContext);
        if (apiResourceLink == null)
        {
            // Unable to create resource link, remove link node in DOM tree.
            domReadWriteLinks.RemoveNode(domReadWriteLink);
            return;
        }

        // Replace the old DOM read-write link node with a new DOM read-only link created by the framework.
        var domReadOnlyLink = DomReadOnlyLink.Create(apiLinkRel, apiResourceLink);
        domReadWriteLinks.ReplaceNode(domReadWriteLink, domReadOnlyLink);
    }

    private void ResolveResourceRelationships(DomReadWriteResource domReadWriteResource,
                                              IHypermediaContext   hypermediaContext,
                                              IHypermediaAssembler hypermediaAssembler,
                                              IResourcePathContext resourcePathContext,
                                              Type                 clrResourceType,
                                              object               clrResource)
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

    private void ResolveResourceRelationship(IDomRelationship          domRelationship,
                                             IHypermediaContext        hypermediaContext,
                                             IHypermediaAssembler      hypermediaAssembler,
                                             IResourcePathContext      resourcePathContext,
                                             Type                      clrResourceType,
                                             object                    clrResource,
                                             DomReadWriteRelationships domReadWriteRelationships)
    {
        if (domRelationship.IsReadOnly)
            return;

        // Resolve read-write relationship
        var domReadWriteRelationship = (DomReadWriteRelationship)domRelationship;

        // .. Rel
        var apiRelationshipRel = domReadWriteRelationship.Rel;

        // .. Links
        var linkContexts         = new List<ILinkContext>();
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
                var apiLinkRel       = domReadWriteLink.Rel;

                var apiLinkMeta = default(Meta);
                var domMeta     = (IDomMeta)domReadWriteLink.GetNode(DomNodeType.Meta);
                if (domMeta != null)
                {
                    apiLinkMeta = domMeta.Meta;
                }

                var linkContext = new LinkContext(apiLinkRel, apiLinkMeta);
                linkContexts.Add(linkContext);
            }
        }

        // .. Data
        var relationshipTypeToCreate = RelationshipType.Relationship;
        var toOneResourceLinkage     = default(ResourceIdentifier);
        var toManyResourceLinkage    = default(IEnumerable<ResourceIdentifier>);

        var resourceType = this.ServiceModel.GetResourceType(clrResourceType);
        var relationship = resourceType.GetRelationshipInfo(apiRelationshipRel);

        var fromApiResourceIdentifier = resourceType.GetApiResourceIdentifier(clrResource);
        var apiResourceLinkageKey     = new ApiResourceLinkageKey(fromApiResourceIdentifier, apiRelationshipRel);

        var apiRelationship = domReadWriteRelationship.Relationship;

        var toCardinality = relationship.ToCardinality;
        switch (toCardinality)
        {
            case RelationshipCardinality.ToOne:
            {
                var hasToOneResourceLinkageFromDom = apiRelationship.IsToOneRelationship();
                var toOneResourceLinkageFromDom    = hasToOneResourceLinkageFromDom ? apiRelationship.GetToOneResourceLinkage() : default(ResourceIdentifier);

                var hasToOneResourceLinkageFromInclude = this.DocumentBuilderContext.TryGetResourceLinkage(apiResourceLinkageKey, out var apiResourceLinkage);
                var toOneResourceLinkageFromInclude    = hasToOneResourceLinkageFromInclude ? apiResourceLinkage.ToOneResourceLinkage : default(ResourceIdentifier);

                if (hasToOneResourceLinkageFromDom && hasToOneResourceLinkageFromInclude)
                {
                    if (toOneResourceLinkageFromDom != toOneResourceLinkageFromInclude)
                    {
                        var detail = InfrastructureErrorStrings.DocumentWriteToOneResourceLinkageMismatch
                                                               .FormatWith(fromApiResourceIdentifier.Type, fromApiResourceIdentifier.Id, apiRelationshipRel, toOneResourceLinkageFromDom, toOneResourceLinkageFromInclude);
                        throw new DocumentWriteException(detail);
                    }

                    relationshipTypeToCreate = RelationshipType.ToOneRelationship;
                    toOneResourceLinkage     = toOneResourceLinkageFromInclude;
                }
                else if (hasToOneResourceLinkageFromDom)
                {
                    relationshipTypeToCreate = RelationshipType.ToOneRelationship;
                    toOneResourceLinkage     = toOneResourceLinkageFromDom;
                }
                else if (hasToOneResourceLinkageFromInclude)
                {
                    relationshipTypeToCreate = RelationshipType.ToOneRelationship;
                    toOneResourceLinkage     = toOneResourceLinkageFromInclude;
                }
            }
                break;

            case RelationshipCardinality.ToMany:
            {
                var hasToManyResourceLinkageFromDom = apiRelationship.IsToManyRelationship();
                var toManyResourceLinkageFromDom    = hasToManyResourceLinkageFromDom ? apiRelationship.GetToManyResourceLinkage() : Enumerable.Empty<ResourceIdentifier>();

                var hasToManyResourceLinkageFromInclude = this.DocumentBuilderContext.TryGetResourceLinkage(apiResourceLinkageKey, out var apiResourceLinkage);
                var toManyResourceLinkageFromInclude    = hasToManyResourceLinkageFromInclude ? apiResourceLinkage.ToManyResourceLinkage : Enumerable.Empty<ResourceIdentifier>();

                if (hasToManyResourceLinkageFromDom && hasToManyResourceLinkageFromInclude)
                {
                    relationshipTypeToCreate = RelationshipType.ToManyRelationship;
                    toManyResourceLinkage    = toManyResourceLinkageFromDom.Union(toManyResourceLinkageFromInclude);
                }
                else if (hasToManyResourceLinkageFromDom)
                {
                    relationshipTypeToCreate = RelationshipType.ToManyRelationship;
                    toManyResourceLinkage    = toManyResourceLinkageFromDom;
                }
                else if (hasToManyResourceLinkageFromInclude)
                {
                    relationshipTypeToCreate = RelationshipType.ToManyRelationship;
                    toManyResourceLinkage    = toManyResourceLinkageFromInclude;
                }
            }
                break;

            default:
            {
                var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                       .FormatWith(typeof(RelationshipCardinality).Name, toCardinality);
                throw new InternalErrorException(detail);
            }
        }

        // .. Meta
        var apiRelationshipMeta = default(Meta);
        var domRelationshipMeta = (IDomMeta)domReadWriteRelationship.GetNode(DomNodeType.Meta);
        if (domRelationshipMeta != null)
        {
            apiRelationshipMeta = domRelationshipMeta.Meta;
        }

        // Create the correct relationship context based on resource linkage (if any).
        RelationshipContext relationshipContext;
        switch (relationshipTypeToCreate)
        {
            case RelationshipType.Relationship:
            {
                relationshipContext = new RelationshipContext(apiRelationshipRel, linkContexts, apiRelationshipMeta);
                break;
            }

            case RelationshipType.ToOneRelationship:
            {
                relationshipContext = new ToOneRelationshipContext(apiRelationshipRel, linkContexts, toOneResourceLinkage, apiRelationshipMeta);
                break;
            }

            case RelationshipType.ToManyRelationship:
            {
                relationshipContext = new ToManyRelationshipContext(apiRelationshipRel, linkContexts, toManyResourceLinkage, apiRelationshipMeta);
                break;
            }

            default:
            {
                var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                       .FormatWith(typeof(RelationshipType).Name, relationshipTypeToCreate);
                throw new InternalErrorException(detail);
            }
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

    private void SortIncludedResourceNodes()
    {
        var domIncludedNode = this.DomDocument.GetNode<DomNodeType, DomIncluded>(DomNodeType.Included);
        if (domIncludedNode == null)
            return;

        var domIncludedNodesSorted = domIncludedNode.Nodes()
                                                    .Cast<DomReadOnlyResource>()
                                                    .OrderBy(x => x.ApiResource)
                                                    .Select(x => DomReadOnlyResource.Create(x.ApiResource, x.ClrResource))
                                                    .ToList();

        var domIncludedNodeSorted = DomIncluded.Create(domIncludedNodesSorted);
        this.DomDocument.ReplaceNode(domIncludedNode, domIncludedNodeSorted);
    }
    #endregion
}
