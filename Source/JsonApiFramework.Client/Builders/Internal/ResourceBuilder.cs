// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Client.Internal
{
    internal abstract class ResourceBuilder<TBuilder> : IResourceBuilder<TBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceBuilder<TBuilder> Implementation
        public TBuilder SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            this.DomReadWriteResource.SetDomReadOnlyMeta(meta);
            return this.Builder;
        }

        public TBuilder SetId<T>(IId<T> id)
        {
            var clrId = id.ClrId;
            this.DomReadWriteResource.SetDomIdFromClrResourceId(this.ResourceType, clrId);
            return this.Builder;
        }

        public IAttributesBuilder<TBuilder> Attributes()
        {
            var attributesBuilder = new AttributesBuilder<TBuilder>(this.Builder, this.ResourceType, this.DomReadWriteResource);
            return attributesBuilder;
        }

        public IRelationshipsBuilder<TBuilder> Relationships()
        {
            var relationshipsBuilder = new RelationshipsBuilder<TBuilder>(this.Builder, this.ServiceModel, this.DomReadWriteResource, this.ClrResourceType);
            return relationshipsBuilder;
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

            this.InitializeResource(domContainerNode, clrResourceType, clrResource);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected DocumentBuilder      ParentBuilder        { get; }
        protected TBuilder             Builder              { get; set; }
        protected IResourceType        ResourceType         { get; private set; }
        protected DomReadWriteResource DomReadWriteResource { get; private set; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Calculated Properties
        private Type          ClrResourceType => this.ResourceType.ClrType;
        private IServiceModel ServiceModel    => this.ParentBuilder.ServiceModel;
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void InitializeResource(IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, object clrResource)
        {
            Contract.Requires(clrResourceType != null);

            // Add the DOM read/write resource node to the DOM document.
            var domReadWriteResource = DomReadWriteResource.Create();
            domContainerNode.Add(domReadWriteResource);

            // Map the incoming CLR resource to the DOM resource node.
            var serviceModel = this.ServiceModel;
            var resourceType = serviceModel.GetResourceType(clrResourceType);

            resourceType.MapClrTypeToDomResource(domReadWriteResource);
            resourceType.MapClrIdToDomResource(domReadWriteResource, clrResource);
            resourceType.MapClrAttributesToDomResource(domReadWriteResource, clrResource);

            this.ResourceType         = resourceType;
            this.DomReadWriteResource = domReadWriteResource;
        }
        #endregion
    }

    internal abstract class ResourceBuilder<TBuilder, TResource> : ResourceBuilder<TBuilder>, IResourceBuilder<TBuilder, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceBuilder<TBuilder, TResource> Implementation
        public new IAttributesBuilder<TBuilder, TResource> Attributes()
        {
            var attributesBuilder = new AttributesBuilder<TBuilder, TResource>(this.Builder, this.ResourceType, this.DomReadWriteResource);
            return attributesBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceBuilder(DocumentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
            : base(parentBuilder, domContainerNode, typeof(TResource), clrResource)
        {
        }
        #endregion
    }
}
