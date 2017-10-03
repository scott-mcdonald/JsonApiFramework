// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Client.Internal
{
    internal class RelationshipsBuilder<TParentBuilder, TResource> : IRelationshipsBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IToOneResourceLinkage toOneResourceLinkage)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipBuilder = this.Relationship(rel);

            // Data
            relationshipBuilder.SetData(toOneResourceLinkage);

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IToManyResourceLinkage toManyResourceLinkage)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipBuilder = this.Relationship(rel);

            // Data
            relationshipBuilder.SetData(toManyResourceLinkage);

            return this;
        }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipBuilder = new RelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource>(this, this.ServiceModel, this.DomReadWriteRelationships, rel);
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
        internal RelationshipsBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNode != null);

            this.ParentBuilder = parentBuilder;

            this.ServiceModel = serviceModel;

            var resourceType = serviceModel.GetResourceType<TResource>();
            this.ResourceType = resourceType;

            var domReadWriteRelationships = domContainerNode.GetOrAddNode(DomNodeType.Relationships, () => DomReadWriteRelationships.Create());
            this.DomReadWriteRelationships = domReadWriteRelationships;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        private IServiceModel ServiceModel { get; set; }
        private IResourceType ResourceType { get; set; }
        private DomReadWriteRelationships DomReadWriteRelationships { get; set; }
        #endregion
    }
}
