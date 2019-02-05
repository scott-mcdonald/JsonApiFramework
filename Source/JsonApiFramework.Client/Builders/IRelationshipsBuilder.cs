// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Client
{
    public interface IRelationshipsBuilder<out TParentBuilder, out TResource>
        where TParentBuilder : class
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IToOneResourceLinkage toOneResourceLinkage);
        IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IToManyResourceLinkage toManyResourceLinkage);

        IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel);

        TParentBuilder RelationshipsEnd();
        #endregion
    }
}