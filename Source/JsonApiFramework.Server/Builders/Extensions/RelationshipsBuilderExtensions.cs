// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class RelationshipsBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, params Relationship[] relationshipCollection)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationshipCollection != null);

            return relationshipsBuilder.AddRelationship(rel, relationshipCollection.AsEnumerable());
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, Func<TResource, bool> predicate, params Relationship[] relationshipCollection)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(predicate != null);
            Contract.Requires(relationshipCollection != null);

            return relationshipsBuilder.AddRelationship(rel, predicate, relationshipCollection.AsEnumerable());
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, params string[] linkRelCollection)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            return relationshipsBuilder.AddRelationship(rel, linkRelCollection.AsEnumerable());
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, Func<TResource, bool> predicate, params string[] linkRelCollection)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            return relationshipsBuilder.AddRelationship(rel, predicate, linkRelCollection.AsEnumerable());
        }
        #endregion
    }
}