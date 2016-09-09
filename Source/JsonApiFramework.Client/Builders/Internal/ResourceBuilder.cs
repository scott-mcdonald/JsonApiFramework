// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Client.Internal
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

            this.DomReadWriteResource.SetDomReadOnlyMeta(meta);
            return this.Builder;
        }

        public TBuilder SetId<TResourceId>(TResourceId clrResourceId)
        {
            this.DomReadWriteResource.SetDomIdFromClrResourceId(this.ResourceType, clrResourceId);
            return this.Builder;
        }

        public IAttributesBuilder<TBuilder, TResource> Attributes()
        {
            var attributesBuilder = new AttributesBuilder<TBuilder, TResource>(this.Builder, this.ResourceType, this.DomReadWriteResource);
            return attributesBuilder;
        }

        public IRelationshipsBuilder<TBuilder, TResource> Relationships()
        {
            var relationshipsBuilder = new RelationshipsBuilder<TBuilder, TResource>(this.Builder, this.ServiceModel, this.DomReadWriteResource);
            return relationshipsBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceBuilder(DocumentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domContainerNode != null);

            this.ParentBuilder = parentBuilder;

            this.InitializeResource(domContainerNode, clrResource);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected DocumentBuilder ParentBuilder { get; private set; }
        protected TBuilder Builder { private get; set; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IResourceType ResourceType { get; set; }
        private DomReadWriteResource DomReadWriteResource { get; set; }
        private TResource ClrResource { get; set; }
        #endregion

        #region Inherited Properties
        private IServiceModel ServiceModel { get { return this.ParentBuilder.ServiceModel; } }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void InitializeResource(IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
        {
            // Add the DOM read/write resource node to the DOM document.
            var domReadWriteResource = DomReadWriteResource.Create();
            domContainerNode.Add(domReadWriteResource);

            // Map the incoming CLR resource to the DOM resource node.
            var serviceModel = this.ServiceModel;
            var clrResourceType = typeof(TResource);
            var resourceType = serviceModel.GetResourceType(clrResourceType);

            resourceType.MapClrTypeToDomResource(domReadWriteResource);
            resourceType.MapClrIdToDomResource(domReadWriteResource, clrResource);
            resourceType.MapClrAttributesToDomResource(domReadWriteResource, clrResource);

            this.ResourceType = resourceType;
            this.DomReadWriteResource = domReadWriteResource;
            this.ClrResource = clrResource;
        }
        #endregion
    }
}
