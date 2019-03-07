// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Server.Internal.Dom;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal abstract class ResourceBuilder<TBuilder> : IResourceBuilder<TBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceBuilder<TBuilder> Implementation
        public TBuilder SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            if (this.NotBuildingResource)
                return this.Builder;

            this.DomReadWriteResource.SetDomReadOnlyMeta(meta);
            return this.Builder;
        }

        public TBuilder SetMeta(IEnumerable<Meta> metaCollection)
        {
            Contract.Requires(metaCollection != null);

            if (this.NotBuildingResource)
                return this.Builder;

            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(DomNodeType.Meta, this.ClrResourceType.Name);
            throw new DocumentBuildException(detail);
        }

        public IResourcePathContextBuilder<TBuilder> Paths()
        {
            var resourcePathContextBuilder = this.NotBuildingResource
                ? (IResourcePathContextBuilder<TBuilder>)(new NullResourcePathContextBuilder<TBuilder>(this.Builder))
                : (IResourcePathContextBuilder<TBuilder>)(this.ResourcePathContextBuilder);
            return resourcePathContextBuilder;
        }

        public IRelationshipsBuilder<TBuilder> Relationships()
        {
            var relationshipsBuilder = this.NotBuildingResource
                ? (IRelationshipsBuilder<TBuilder>)(new NullRelationshipsBuilder<TBuilder>(this.Builder))
                : (IRelationshipsBuilder<TBuilder>)(new RelationshipsBuilder<TBuilder>(this.Builder, this.ServiceModel, this.DomReadWriteResource, this.ClrResourceType, this.ClrResource));
            return relationshipsBuilder;
        }

        public IResourceLinksBuilder<TBuilder> Links()
        {
            var linksBuilder = this.NotBuildingResource
                ? (IResourceLinksBuilder<TBuilder>)(new NullResourceLinksBuilder<TBuilder>(this.Builder))
                : (IResourceLinksBuilder<TBuilder>)(new ResourceLinksBuilder<TBuilder>(this.Builder, this.DomReadWriteResource, this.ClrResourceType, this.ClrResource));
            return linksBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceBuilder(DocumentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, object clrResource)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domContainerNode != null);
            Contract.Requires(clrResourceType != null);

            this.ParentBuilder = parentBuilder;

            if (clrResourceType == null)
                return;

            var resourceType = this.ServiceModel.GetResourceType(clrResourceType);
            this.ResourceType = resourceType;

            this.InitializeResource(domContainerNode, clrResource);

            this.InitializeResourcePathContextBuilder();
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected DocumentBuilder ParentBuilder { get; }

        protected TBuilder Builder { get; set; }

        protected DomReadWriteResource DomReadWriteResource { get; private set; }

        protected object ClrResource { get; private set; }
        #endregion

        #region Calculated Properties
        protected DocumentBuilderContext DocumentBuilderContext => this.ParentBuilder.DocumentBuilderContext;

        protected bool NotBuildingResource => !this.BuildingResource;

        protected IServiceModel ServiceModel => this.ParentBuilder.ServiceModel;
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Methods
        protected void OnBuildEnd()
        {
            if (this.NotBuildingResource)
                return;

            this.AddResourcePathContext();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IResourceType ResourceType { get; }

        private bool BuildingResource { get; set; }

        private Lazy<ResourcePathContextBuilder<TBuilder>> LazyResourcePathContextBuilder { get; set; }

        private ResourcePathContextBuilder<TBuilder> ResourcePathContextBuilder => this.LazyResourcePathContextBuilder.Value;
        #endregion

        #region Inherited Properties
        private Type ClrResourceType => this.ResourceType.ClrType;
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourcePathContext()
        {
            var clrResource         = this.ClrResource;
            var resourcePathContext = this.ResourcePathContextBuilder.CreateResourcePathContext(clrResource);
            this.DomReadWriteResource.SetResourcePathContext(resourcePathContext);
        }

        private void InitializeResource(IContainerNode<DomNodeType> domContainerNode, object clrResource)
        {
            Contract.Requires(clrResource != null);

            if (clrResource == null)
                return;

            // Map the incoming CLR resource to the DOM resource node.
            var resourceType = this.ResourceType;

            var domReadWriteResource = DomReadWriteResource.Create();
            resourceType.MapClrTypeToDomResource(domReadWriteResource);
            resourceType.MapClrIdToDomResource(domReadWriteResource, clrResource);

            var domResource    = (IDomResource)domReadWriteResource;
            var domResourceKey = domResource.CreateDomResourceKey();

            // Do not add the DOM read-write resource node if it already has been added to the DOM document.
            if (this.DocumentBuilderContext.ContainsDomReadWriteResource(domResourceKey))
                return;

            // Add the DOM read/write resource nodes to the DOM document.
            this.DocumentBuilderContext.AddDomReadWriteResource(domResourceKey, domReadWriteResource);
            domContainerNode.Add(domReadWriteResource);

            // Finish mapping the DOM read/write resource attributes nodes to the DOM document.
            var queryParameters    = this.DocumentBuilderContext.QueryParameters;
            var apiType            = resourceType.ResourceIdentityInfo.ApiType;
            var useSparseFieldsets = this.DocumentBuilderContext.SparseFieldsetsEnabled && queryParameters.ContainsField(apiType);

            if (!useSparseFieldsets)
            {
                resourceType.MapClrAttributesToDomResource(domReadWriteResource, clrResource);
            }
            else
            {
                resourceType.MapClrAttributesToDomResource(domReadWriteResource, clrResource, (x, y) => queryParameters.ContainsField(x, y));
            }

            this.BuildingResource     = true;
            this.DomReadWriteResource = domReadWriteResource;
            this.ClrResource          = clrResource;
        }

        private void InitializeResourcePathContextBuilder()
        {
            this.LazyResourcePathContextBuilder = new Lazy<ResourcePathContextBuilder<TBuilder>>(() => new ResourcePathContextBuilder<TBuilder>(this.Builder, this.ServiceModel, this.ClrResourceType));
        }
        #endregion
    }

    internal abstract class ResourceBuilder<TBuilder, TResource> : ResourceBuilder<TBuilder>, IResourceBuilder<TBuilder, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceBuilder<TBuilder, TResource> Implementation
        public new IRelationshipsBuilder<TBuilder, TResource> Relationships()
        {
            var relationshipsBuilder = this.NotBuildingResource
                ? (IRelationshipsBuilder<TBuilder, TResource>)(new NullRelationshipsBuilder<TBuilder, TResource>(this.Builder))
                : (IRelationshipsBuilder<TBuilder, TResource>)(new RelationshipsBuilder<TBuilder, TResource>(this.Builder, this.ServiceModel, this.DomReadWriteResource, (TResource)this.ClrResource));
            return relationshipsBuilder;
        }

        public new IResourceLinksBuilder<TBuilder, TResource> Links()
        {
            var linksBuilder = this.NotBuildingResource
                ? (IResourceLinksBuilder<TBuilder, TResource>)(new NullResourceLinksBuilder<TBuilder, TResource>(this.Builder))
                : (IResourceLinksBuilder<TBuilder, TResource>)(new ResourceLinksBuilder<TBuilder, TResource>(this.Builder, this.DomReadWriteResource, (TResource)this.ClrResource));
            return linksBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceBuilder(DocumentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, TResource clrResource)
            : base(parentBuilder, domContainerNode, clrResourceType, clrResource)
        {
        }
        #endregion
    }
}
