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
    internal abstract class ResourceBuilder<TBuilder, TResource> : IResourceBuilder<TBuilder, TResource>
        where TBuilder : class, IResourceBuilder<TBuilder, TResource>
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceBuilder<TBuilder, TResource> Implementation
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

            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(DomNodeType.Meta, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public IResourcePathContextBuilder<TBuilder> Paths()
        {
            var resourcePathContextBuilder = this.NotBuildingResource
                ? (IResourcePathContextBuilder<TBuilder>)(new NullResourcePathContextBuilder<TBuilder>(this.Builder))
                : (IResourcePathContextBuilder<TBuilder>)(this.ResourcePathContextBuilder);
            return resourcePathContextBuilder;
        }

        public IRelationshipsBuilder<TBuilder, TResource> Relationships()
        {
            var relationshipsBuilder = this.NotBuildingResource
                ? (IRelationshipsBuilder<TBuilder, TResource>)(new NullRelationshipsBuilder<TBuilder, TResource>(this.Builder))
                : (IRelationshipsBuilder<TBuilder, TResource>)(new RelationshipsBuilder<TBuilder, TResource>(this.Builder, this.DomReadWriteResource, this.ClrResource));
            return relationshipsBuilder;
        }

        public IResourceLinksBuilder<TBuilder, TResource> Links()
        {
            var linksBuilder = this.NotBuildingResource
                ? (IResourceLinksBuilder<TBuilder, TResource>)(new NullResourceLinksBuilder<TBuilder, TResource>(this.Builder))
                : (IResourceLinksBuilder<TBuilder, TResource>)(new ResourceLinksBuilder<TBuilder, TResource>(this.Builder, this.DomReadWriteResource, this.ClrResource));
            return linksBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceBuilder(DocumentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domContainerNode != null);

            this.ParentBuilder = parentBuilder;

            this.InitializeResourcePathContextBuilder();
            this.InitializeResource(domContainerNode, clrResource);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected DocumentBuilder ParentBuilder { get; private set; }
        protected TBuilder Builder { private get; set; }
        #endregion

        #region Inherited Properties
        protected IServiceModel ServiceModel
        { get { return this.ParentBuilder.ServiceModel; } }

        protected DocumentBuilderContext DocumentBuilderContext
        { get { return this.ParentBuilder.DocumentBuilderContext; } }
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
        private Lazy<ResourcePathContextBuilder<TBuilder, TResource>> LazyResourcePathContextBuilder { get; set; }
        private ResourcePathContextBuilder<TBuilder, TResource> ResourcePathContextBuilder { get { return this.LazyResourcePathContextBuilder.Value; } }

        private bool BuildingResource { get; set; }
        private bool NotBuildingResource { get { return !this.BuildingResource; } }

        private DomReadWriteResource DomReadWriteResource { get; set; }
        private TResource ClrResource { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourcePathContext()
        {
            var resourcePathContext = this.ResourcePathContextBuilder.CreateResourcePathContext();
            this.DomReadWriteResource.SetResourcePathContext(resourcePathContext);
        }

        private void InitializeResource(IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
        {
            this.BuildingResource = false;
            if (clrResource == null)
                return;

            // Map the incoming CLR resource to the DOM resource node.
            var serviceModel = this.ServiceModel;
            var clrResourceType = typeof(TResource);
            var resourceType = serviceModel.GetResourceType(clrResourceType);

            var domReadWriteResource = DomReadWriteResource.Create();
            resourceType.MapClrTypeToDomResource(domReadWriteResource);
            resourceType.MapClrIdToDomResource(domReadWriteResource, clrResource);

            var domResource = (IDomResource)domReadWriteResource;
            var domResourceKey = domResource.CreateDomResourceKey();

            // Do not add the DOM read-write resource node if it already has been added to the DOM document.
            if (this.DocumentBuilderContext.ContainsDomReadWriteResource(domResourceKey))
                return;

            // Add the DOM read/write resource nodes to the DOM document.
            resourceType.MapClrAttributesToDomResource(domReadWriteResource, clrResource);

            this.DocumentBuilderContext.AddDomReadWriteResource(domResourceKey, domReadWriteResource);
            domContainerNode.Add(domReadWriteResource);

            this.BuildingResource = true;
            this.DomReadWriteResource = domReadWriteResource;
            this.ClrResource = clrResource;
        }

        private void InitializeResourcePathContextBuilder()
        {
            this.LazyResourcePathContextBuilder =
                new Lazy<ResourcePathContextBuilder<TBuilder, TResource>>(
                    () => new ResourcePathContextBuilder<TBuilder, TResource>(this.Builder, this.ServiceModel));
        }
        #endregion
    }
}
