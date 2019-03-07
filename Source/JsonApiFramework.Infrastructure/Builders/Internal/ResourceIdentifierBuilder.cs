// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    internal abstract class ResourceIdentifierBuilder<TBuilder> : IResourceIdentifierBuilder<TBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceIdentifierBuilder<TBuilder> Implementation
        public TBuilder SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            if (this.NotBuildingResourceIdentifier)
                return this.Builder;

            this.CreateAndAddDomReadWriteResourceIdentifierIfNeeded();

            this.DomReadWriteResourceIdentifier.SetDomReadOnlyMeta(meta);
            return this.Builder;
        }

        public TBuilder SetMeta(IEnumerable<Meta> metaCollection)
        {
            Contract.Requires(metaCollection != null);

            if (this.NotBuildingResourceIdentifier)
                return this.Builder;

            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(DomNodeType.Meta, this.ClrResourceType.Name);
            throw new DocumentBuildException(detail);
        }

        public TBuilder SetId<T>(IId<T> id)
        {
            Contract.Requires(id != null);

            if (this.NotBuildingResourceIdentifier)
                return this.Builder;

            this.CreateAndAddDomReadWriteResourceIdentifierIfNeeded();

            var clrId = id.ClrId;
            this.DomReadWriteResourceIdentifier.SetDomIdFromClrResourceId(this.ResourceType, clrId);
            return this.Builder;
        }

        public TBuilder SetId<T>(IIdCollection<T> idCollection)
        {
            Contract.Requires(idCollection != null);

            if (this.NotBuildingResourceIdentifier)
                return this.Builder;

            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(DomNodeType.Id, this.ClrResourceType.Name);
            throw new DocumentBuildException(detail);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceIdentifierBuilder(IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, object clrResource)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNode != null);

            this.DomContainerNode = domContainerNode;

            if (clrResourceType == null)
                return;

            this.BuildingResourceIdentifier = true;

            var resourceType = serviceModel.GetResourceType(clrResourceType);
            this.ResourceType = resourceType;

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
        private bool                           BuildingResourceIdentifier     { get; }
        private IContainerNode<DomNodeType>    DomContainerNode               { get; }
        private DomReadWriteResourceIdentifier DomReadWriteResourceIdentifier { get; set; }
        private IResourceType ResourceType { get; }
        #endregion

        #region Calculated Properties
        private Type ClrResourceType => this.ResourceType?.ClrType;

        private bool NotBuildingResourceIdentifier => !this.BuildingResourceIdentifier;
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void CreateAndAddDomReadWriteResourceIdentifierIfNeeded()
        {
            if (this.DomReadWriteResourceIdentifier != null)
                return;

            var domResourceType                = DomType.CreateFromResourceType(this.ResourceType);
            var domReadWriteResourceIdentifier = DomReadWriteResourceIdentifier.Create(domResourceType);

            var domContainerNode = this.DomContainerNode;
            domContainerNode.Add(domReadWriteResourceIdentifier);

            this.DomReadWriteResourceIdentifier = domReadWriteResourceIdentifier;
        }
        #endregion
    }
}
