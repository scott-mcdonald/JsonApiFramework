// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public interface IRelationshipsBuilder<out TParentBuilder, out TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Relationship relationship);
        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, Relationship relationship);

        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IEnumerable<Relationship> relationshipCollection);
        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<Relationship> relationshipCollection);

        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IEnumerable<string> linkRelCollection);
        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection);

        IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel);
        IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel, Func<TResource, bool> predicate);

        TParentBuilder RelationshipsEnd();
        #endregion
    }
}