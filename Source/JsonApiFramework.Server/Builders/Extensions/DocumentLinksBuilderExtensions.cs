// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class DocumentLinksBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IDocumentLinksBuilder<TParentBuilder> AddUpLink<TParentBuilder>(this IDocumentLinksBuilder<TParentBuilder> documentLinksBuilder, Link link)
        {
            Contract.Requires(documentLinksBuilder != null);
            Contract.Requires(link != null);

            return documentLinksBuilder.AddLink(Keywords.Up, link);
        }

        public static IDocumentLinksBuilder<TParentBuilder> AddUpLink<TParentBuilder>(this IDocumentLinksBuilder<TParentBuilder> documentLinksBuilder, IEnumerable<Link> linkCollection)
        {
            Contract.Requires(documentLinksBuilder != null);
            Contract.Requires(linkCollection != null);

            return documentLinksBuilder.AddLink(Keywords.Up, linkCollection);
        }

        public static IDocumentLinksBuilder<TParentBuilder> AddUpLink<TParentBuilder>(this IDocumentLinksBuilder<TParentBuilder> documentLinksBuilder)
        {
            Contract.Requires(documentLinksBuilder != null);

            return documentLinksBuilder.AddLink(Keywords.Up);
        }
        #endregion
    }
}