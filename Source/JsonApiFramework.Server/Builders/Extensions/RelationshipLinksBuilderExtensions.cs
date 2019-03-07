// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class RelationshipLinksBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IRelationshipLinksBuilder<TParentBuilder> AddRelatedLink<TParentBuilder>(this IRelationshipLinksBuilder<TParentBuilder> relationshipLinksBuilder, Link link)
        {
            Contract.Requires(relationshipLinksBuilder != null);
            Contract.Requires(link != null);

            return relationshipLinksBuilder.AddLink(Keywords.Related, link);
        }

        public static IRelationshipLinksBuilder<TParentBuilder> AddRelatedLink<TParentBuilder>(this IRelationshipLinksBuilder<TParentBuilder> relationshipLinksBuilder, IEnumerable<Link> linkCollection)
        {
            Contract.Requires(relationshipLinksBuilder != null);
            Contract.Requires(linkCollection != null);

            return relationshipLinksBuilder.AddLink(Keywords.Related, linkCollection);
        }

        public static IRelationshipLinksBuilder<TParentBuilder> AddRelatedLink<TParentBuilder>(this IRelationshipLinksBuilder<TParentBuilder> relationshipLinksBuilder)
        {
            Contract.Requires(relationshipLinksBuilder != null);

            return relationshipLinksBuilder.AddLink(Keywords.Related);
        }
        #endregion
    }
}