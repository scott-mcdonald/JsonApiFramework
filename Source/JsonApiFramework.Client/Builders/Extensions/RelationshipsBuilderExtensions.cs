// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;


namespace JsonApiFramework.Client
{
    public static class RelationshipBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IRelationshipBuilder<TParentBuilder, TResource> SetId<TParentBuilder, TResource, TResourceId>(this IRelationshipBuilder<TParentBuilder, TResource> relationshipBuilder, params TResourceId[] clrResourceIdCollection)
            where TParentBuilder : class
            where TResource : class, IResource
            where TResourceId : IEquatable<TResourceId>
        {
            Contract.Requires(relationshipBuilder != null);
            Contract.Requires(clrResourceIdCollection != null);

            return relationshipBuilder.SetId(clrResourceIdCollection.AsEnumerable());
        }
        #endregion
    }
}