// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class LinksBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static TBuilder AddLink<TBuilder, TParentBuilder>(this ILinksBuilder<TBuilder, TParentBuilder> linksBuilder, string rel, params Link[] linkCollection)
        {
            Contract.Requires(linksBuilder != null);
            Contract.Requires(linkCollection != null);

            return linksBuilder.AddLink(rel, linkCollection.AsEnumerable());
        }

        public static TBuilder AddSelfLink<TBuilder, TParentBuilder>(this ILinksBuilder<TBuilder, TParentBuilder> linksBuilder, Link link)
        {
            Contract.Requires(linksBuilder != null);
            Contract.Requires(link != null);

            return linksBuilder.AddLink(Keywords.Self, link);
        }

        public static TBuilder AddSelfLink<TBuilder, TParentBuilder>(this ILinksBuilder<TBuilder, TParentBuilder> linksBuilder, IEnumerable<Link> linkCollection)
        {
            Contract.Requires(linksBuilder != null);
            Contract.Requires(linkCollection != null);

            return linksBuilder.AddLink(Keywords.Self, linkCollection);
        }

        public static TBuilder AddSelfLink<TBuilder, TParentBuilder>(this ILinksBuilder<TBuilder, TParentBuilder> linksBuilder)
        {
            Contract.Requires(linksBuilder != null);

            return linksBuilder.AddLink(Keywords.Self);
        }
        #endregion
    }
}