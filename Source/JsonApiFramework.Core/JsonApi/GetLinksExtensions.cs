// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Extension methods for any object that implements the <c>IGetLinks</c> interface.</summary>
    public static class GetLinksExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static bool HasLinks(this IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null);

            return getLinks.Links != null;
        }

        public static string GetHRef(this IGetLinks getLinks, string rel)
        {
            Contract.Requires(getLinks != null);

            var links = getLinks.Links;
            if (links != null)
                return links.GetHRef(rel);

            throw new LinkNotFoundException(rel);
        }

        public static Link GetLink(this IGetLinks getLinks, string rel)
        {
            Contract.Requires(getLinks != null);

            var links = getLinks.Links;
            if (links != null)
                return links.GetLink(rel);

            throw new LinkNotFoundException(rel);
        }

        public static Uri GetUri(this IGetLinks getLinks, string rel)
        {
            Contract.Requires(getLinks != null);

            var links = getLinks.Links;
            if (links != null)
                return links.GetUri(rel);

            throw new LinkNotFoundException(rel);
        }

        public static bool TryGetHRef(this IGetLinks getLinks, string rel, out string hRef)
        {
            Contract.Requires(getLinks != null);

            var links = getLinks.Links;
            if (links != null)
                return links.TryGetHRef(rel, out hRef);

            hRef = null;
            return false;
        }

        public static bool TryGetLink(this IGetLinks getLinks, string rel, out Link link)
        {
            Contract.Requires(getLinks != null);

            var links = getLinks.Links;
            if (links != null)
                return links.TryGetLink(rel, out link);

            link = null;
            return false;
        }

        public static bool TryGetUri(this IGetLinks getLinks, string rel, out Uri uri)
        {
            Contract.Requires(getLinks != null);

            var links = getLinks.Links;
            if (links != null)
                return links.TryGetUri(rel, out uri);

            uri = null;
            return false;
        }
        #endregion
    }
}