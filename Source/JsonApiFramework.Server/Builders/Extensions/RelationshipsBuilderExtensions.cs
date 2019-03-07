// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class RelationshipsBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, Relationship relationship)
            where TResource : class
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), relationship);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<Relationship> relationshipCollection)
            where TResource : class
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), relationshipCollection);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection)
            where TResource : class
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage)
            where TResource : class
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection, toOneResourceLinkage);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
            where TResource : class
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection, toOneResourceLinkageCollection);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection, IToManyResourceLinkage toManyResourceLinkage)
            where TResource : class
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection, toManyResourceLinkage);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
            where TResource : class
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection, toManyResourceLinkageCollection);
        }

        public static IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel)
            where TResource : class
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.Relationship(rel, default(Func<TResource, bool>));
        }
        #endregion
    }
}