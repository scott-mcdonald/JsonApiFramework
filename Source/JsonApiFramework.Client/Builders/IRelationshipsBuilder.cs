// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Client
{
    public interface IRelationshipsBuilder<out TParentBuilder, out TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel);
        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TResourceId>(string rel, TResourceId clrResourceId);
        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TResourceId>(string rel, IEnumerable<TResourceId> clrResourceIdCollection);

        IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel);

        TParentBuilder RelationshipsEnd();
        #endregion
    }
}