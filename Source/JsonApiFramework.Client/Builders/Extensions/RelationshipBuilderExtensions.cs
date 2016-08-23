// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Client
{
    public static class RelationshipsBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource, TResourceId>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, params TResourceId[] clrResourceIdCollection)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(clrResourceIdCollection != null);

            return relationshipsBuilder.AddRelationship(rel, clrResourceIdCollection.AsEnumerable());
        }
        #endregion
    }
}