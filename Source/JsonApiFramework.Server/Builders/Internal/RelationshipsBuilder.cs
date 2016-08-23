// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class RelationshipsBuilder<TParentBuilder, TResource> : IRelationshipsBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Relationship relationship)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationship != null);

            return this.AddRelationship(rel, default(Func<TResource, bool>), relationship);
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, Relationship relationship)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationship != null);

            var clrResource = this.ClrResource;
            var canAddRelationship = predicate == null || predicate(clrResource);
            if (canAddRelationship == false)
                return this;

            this.DomReadWriteRelationships.AddDomReadOnlyRelationship(rel, relationship);
            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IEnumerable<Relationship> relationshipCollection)
        {
            var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(relationshipDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<Relationship> relationshipCollection)
        {
            var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(relationshipDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IEnumerable<string> linkRelCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            return this.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection);
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var clrResource = this.ClrResource;
            var canAddRelationship = predicate == null || predicate(clrResource);
            if (canAddRelationship == false)
                return this;

            this.DomReadWriteRelationships.AddDomReadWriteRelationship(rel, linkRelCollection);
            return this;
        }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.Relationship(rel, default(Func<TResource, bool>));
        }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var clrResource = this.ClrResource;
            var canAddRelationship = predicate == null || predicate(clrResource);
            return canAddRelationship
                // NOTE: We use CreateRelationshipBuilder instead of calling this.Relationship(rel) to avoid infinite recursion.
                ? this.CreateRelationshipBuilder(rel)
                : this.CreateNullRelationshipBuilder();
        }

        public TParentBuilder RelationshipsEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsBuilder(TParentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domContainerNode != null);
            Contract.Requires(clrResource != null);

            this.ParentBuilder = parentBuilder;

            var domReadWriteRelationships = domContainerNode.GetOrAddNode(DomNodeType.Relationships, () => DomReadWriteRelationships.Create());
            this.DomReadWriteRelationships = domReadWriteRelationships;

            this.ClrResource = clrResource;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        private DomReadWriteRelationships DomReadWriteRelationships { get; set; }
        private TResource ClrResource { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> CreateNullRelationshipBuilder()
        {
            var relationshipBuilder = new NullRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource>(this);
            return relationshipBuilder;
        }

        private IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> CreateRelationshipBuilder(string rel)
        {
            var relationshipBuilder = new RelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource>(this, this.DomReadWriteRelationships, rel);
            return relationshipBuilder;
        }
        #endregion
    }
}
