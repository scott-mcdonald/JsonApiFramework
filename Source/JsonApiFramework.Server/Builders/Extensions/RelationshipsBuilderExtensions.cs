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
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), relationship);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<Relationship> relationshipCollection)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), relationshipCollection);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection, toOneResourceLinkage);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection, toOneResourceLinkageCollection);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection, IToManyResourceLinkage toManyResourceLinkage)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection, toManyResourceLinkage);
        }

        public static IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection, toManyResourceLinkageCollection);
        }

        public static IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship<TParentBuilder, TResource>(this IRelationshipsBuilder<TParentBuilder, TResource> relationshipsBuilder, string rel)
            where TParentBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(relationshipsBuilder != null);

            return relationshipsBuilder.Relationship(rel, default(Func<TResource, bool>));
        }
        #endregion
    }
}