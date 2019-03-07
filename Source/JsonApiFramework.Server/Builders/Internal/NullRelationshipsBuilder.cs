// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class NullRelationshipsBuilder<TParentBuilder> : IRelationshipsBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder> Implementation
        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, Relationship relationship)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<Relationship> relationshipCollection)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IToManyResourceLinkage toManyResourceLinkage)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
        { return this; }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipBuilder = new NullRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>>(this);
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
        internal NullRelationshipsBuilder(TParentBuilder parentBuilder)
        {
            Contract.Requires(parentBuilder != null);

            this.ParentBuilder = parentBuilder;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        #endregion
    }

    internal class NullRelationshipsBuilder<TParentBuilder, TResource> : NullRelationshipsBuilder<TParentBuilder>, IRelationshipsBuilder<TParentBuilder, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, Relationship relationship)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<Relationship> relationshipCollection)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IToManyResourceLinkage toManyResourceLinkage)
        { return this; }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
        { return this; }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipBuilder = new NullRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>>(this);
            return relationshipBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal NullRelationshipsBuilder(TParentBuilder parentBuilder)
            : base(parentBuilder)
        {
        }
        #endregion
    }
}
