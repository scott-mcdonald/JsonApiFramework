// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Client.Internal
{
    internal class RelationshipsBuilder<TParentBuilder> : IRelationshipsBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder> Implementation
        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IToOneResourceLinkage toOneResourceLinkage)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipBuilder = this.Relationship(rel);

            // Data
            relationshipBuilder.SetData(toOneResourceLinkage);

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IToManyResourceLinkage toManyResourceLinkage)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipBuilder = this.Relationship(rel);

            // Data
            relationshipBuilder.SetData(toManyResourceLinkage);

            return this;
        }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipBuilder = new RelationshipBuilder<IRelationshipsBuilder<TParentBuilder>>(this, this.ServiceModel, this.DomReadWriteRelationships, rel, this.ClrResourceType);
            return relationshipBuilder;
        }

        public TParentBuilder RelationshipsEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, Type clrResourceType)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNode != null);
            Contract.Requires(clrResourceType != null);

            this.ParentBuilder = parentBuilder;

            this.ServiceModel = serviceModel;

            var resourceType = serviceModel.GetResourceType(clrResourceType);
            this.ResourceType = resourceType;

            var domReadWriteRelationships = domContainerNode.GetOrAddNode(DomNodeType.Relationships, () => DomReadWriteRelationships.Create());
            this.DomReadWriteRelationships = domReadWriteRelationships;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder            ParentBuilder             { get; }
        private IServiceModel             ServiceModel              { get; }
        private IResourceType             ResourceType              { get; }
        private DomReadWriteRelationships DomReadWriteRelationships { get; }
        #endregion

        #region Calculated Properties
        private Type ClrResourceType => this.ResourceType.ClrType;
        #endregion
    }
}
