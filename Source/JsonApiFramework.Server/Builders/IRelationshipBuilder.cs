// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public interface IRelationshipBuilder<out TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IRelationshipBuilder<TParentBuilder> SetMeta(Meta meta);
        IRelationshipBuilder<TParentBuilder> SetMeta(IEnumerable<Meta> metaCollection);

        IRelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder>> Links();

        IRelationshipBuilder<TParentBuilder> SetData(IToOneResourceLinkage toOneResourceLinkage);
        IRelationshipBuilder<TParentBuilder> SetData(IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection);

        IRelationshipBuilder<TParentBuilder> SetData(IToManyResourceLinkage toManyResourceLinkage);
        IRelationshipBuilder<TParentBuilder> SetData(IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection);

        TParentBuilder RelationshipEnd();
        #endregion
    }
}