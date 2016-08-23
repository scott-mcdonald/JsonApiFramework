// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public interface IRelationshipBuilder<out TParentBuilder, out TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IRelationshipBuilder<TParentBuilder, TResource> SetMeta(Meta meta);
        IRelationshipBuilder<TParentBuilder, TResource> SetMeta(IEnumerable<Meta> metaCollection);

        IRelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>> Links();

        TParentBuilder RelationshipEnd();
        #endregion
    }
}