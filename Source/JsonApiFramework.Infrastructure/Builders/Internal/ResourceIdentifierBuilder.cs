// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    internal abstract class ResourceIdentifierBuilder<TBuilder, TResource> : IResourceIdentifierBuilder<TBuilder, TResource>
        where TBuilder : class, IResourceIdentifierBuilder<TBuilder, TResource>
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceIdentifierBuilder<TBuilder> Implementation
        public TBuilder SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            this.CreateAndAddDomReadWriteResourceIdentifierIfNeeded();

            this.DomReadWriteResourceIdentifier.SetDomReadOnlyMeta(meta);
            return this.Builder;
        }

        public TBuilder SetMeta(IEnumerable<Meta> metaCollection)
        {
            Contract.Requires(metaCollection != null);

            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(DomNodeType.Meta, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public TBuilder SetId<TResourceId>(TResourceId clrResourceId)
        {
            this.CreateAndAddDomReadWriteResourceIdentifierIfNeeded();

            this.DomReadWriteResourceIdentifier.SetDomIdFromClrResourceId(this.ResourceType, clrResourceId);
            return this.Builder;
        }

        public TBuilder SetId<TResourceId>(IEnumerable<TResourceId> clrResourceIdCollection)
        {
            Contract.Requires(clrResourceIdCollection != null);

            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(DomNodeType.Id, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceIdentifierBuilder(IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNode != null);

            this.ServiceModel = serviceModel;
            this.DomContainerNode = domContainerNode;
        }

        protected ResourceIdentifierBuilder(IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
            : this(serviceModel, domContainerNode)
        {
            if (clrResource == null)
                return;

            this.CreateAndAddDomReadWriteResourceIdentifierIfNeeded();

            this.DomReadWriteResourceIdentifier.SetDomIdFromClrResource(this.ResourceType, clrResource);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected TBuilder Builder { private get; set; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IServiceModel ServiceModel { get; set; }
        private IResourceType ResourceType { get; set; }
        private IContainerNode<DomNodeType> DomContainerNode { get; set; }
        private DomReadWriteResourceIdentifier DomReadWriteResourceIdentifier { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void CreateAndAddDomReadWriteResourceIdentifierIfNeeded()
        {
            if (this.DomReadWriteResourceIdentifier != null)
                return;

            var clrResourceType = typeof(TResource);
            var resourceType = this.ServiceModel.GetResourceType(clrResourceType);
            this.ResourceType = resourceType;

            var domResourceType = DomType.CreateFromResourceType(resourceType);
            var domReadWriteResourceIdentifier = DomReadWriteResourceIdentifier.Create(domResourceType);

            var domContainerNode = this.DomContainerNode;
            domContainerNode.Add(domReadWriteResourceIdentifier);

            this.DomReadWriteResourceIdentifier = domReadWriteResourceIdentifier;
        }
        #endregion
    }
}
