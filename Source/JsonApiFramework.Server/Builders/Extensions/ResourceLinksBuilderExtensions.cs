// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class ResourceLinksBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IResourceLinksBuilder<TParentBuilder, TResource> AddLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder, string rel, Func<TResource, bool> predicate, params Link[] linkCollection)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);
            Contract.Requires(linkCollection != null);

            return resourceLinksBuilder.AddLink(rel, predicate, linkCollection.AsEnumerable());
        }

        public static IResourceLinksBuilder<TParentBuilder, TResource> AddCanonicalLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder, Link link)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);
            Contract.Requires(link != null);

            return resourceLinksBuilder.AddLink(Keywords.Canonical, link);
        }

        public static IResourceLinksBuilder<TParentBuilder, TResource> AddCanonicalLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder, IEnumerable<Link> linkCollection)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);
            Contract.Requires(linkCollection != null);

            return resourceLinksBuilder.AddLink(Keywords.Canonical, linkCollection);
        }

        public static IResourceLinksBuilder<TParentBuilder, TResource> AddCanonicalLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);

            return resourceLinksBuilder.AddLink(Keywords.Canonical);
        }

        public static IResourceLinksBuilder<TParentBuilder, TResource> AddCanonicalLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder, Func<TResource, bool> predicate, Link link)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);
            Contract.Requires(link != null);

            return resourceLinksBuilder.AddLink(Keywords.Canonical, predicate, link);
        }

        public static IResourceLinksBuilder<TParentBuilder, TResource> AddCanonicalLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder, Func<TResource, bool> predicate, IEnumerable<Link> linkCollection)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);
            Contract.Requires(linkCollection != null);

            return resourceLinksBuilder.AddLink(Keywords.Canonical, predicate, linkCollection);
        }

        public static IResourceLinksBuilder<TParentBuilder, TResource> AddCanonicalLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder, Func<TResource, bool> predicate)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);

            return resourceLinksBuilder.AddLink(Keywords.Canonical, predicate);
        }

        public static IResourceLinksBuilder<TParentBuilder, TResource> AddSelfLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder, Func<TResource, bool> predicate, Link link)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);
            Contract.Requires(link != null);

            return resourceLinksBuilder.AddLink(Keywords.Self, predicate, link);
        }

        public static IResourceLinksBuilder<TParentBuilder, TResource> AddSelfLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder, Func<TResource, bool> predicate, IEnumerable<Link> linkCollection)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);
            Contract.Requires(linkCollection != null);

            return resourceLinksBuilder.AddLink(Keywords.Self, predicate, linkCollection);
        }

        public static IResourceLinksBuilder<TParentBuilder, TResource> AddSelfLink<TParentBuilder, TResource>(this IResourceLinksBuilder<TParentBuilder, TResource> resourceLinksBuilder, Func<TResource, bool> predicate)
            where TResource : class
        {
            Contract.Requires(resourceLinksBuilder != null);

            return resourceLinksBuilder.AddLink(Keywords.Self, predicate);
        }
        #endregion
    }
}