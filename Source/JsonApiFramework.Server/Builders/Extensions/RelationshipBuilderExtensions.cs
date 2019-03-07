// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class RelationshipBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IRelationshipBuilder<TParentBuilder> SetMeta<TParentBuilder, TResource>(this IRelationshipBuilder<TParentBuilder> relationshipBuilder, params Meta[] metaCollection)
            where TResource : class
        {
            Contract.Requires(relationshipBuilder != null);

            return relationshipBuilder.SetMeta(metaCollection.AsEnumerable());
        }
        #endregion
    }
}