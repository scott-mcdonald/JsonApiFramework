// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Client
{
    public interface IRelationshipBuilder<out TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IRelationshipBuilder<TParentBuilder> SetMeta(Meta meta);

        IRelationshipBuilder<TParentBuilder> SetData(IToOneResourceLinkage toOneResourceLinkage);
        IRelationshipBuilder<TParentBuilder> SetData(IToManyResourceLinkage toManyResourceLinkage);

        TParentBuilder RelationshipEnd();
        #endregion
    }
}