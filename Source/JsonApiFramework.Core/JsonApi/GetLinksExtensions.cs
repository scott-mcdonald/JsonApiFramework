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
        #region IGetLinks Extension Methods
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

        // Standard Links ///////////////////////////////////////////////////
        public static Link AboutLink(this IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null);
            return GetStandardLink(getLinks, Keywords.About);
        }

        public static Link CanonicalLink(this IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null);
            return GetStandardLink(getLinks, Keywords.Canonical);
        }

        public static Link FirstLink(this IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null);
            return GetStandardLink(getLinks, Keywords.First);
        }

        public static Link LastLink(this IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null);
            return GetStandardLink(getLinks, Keywords.Last);
        }

        public static Link NextLink(this IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null);
            return GetStandardLink(getLinks, Keywords.Next);
        }

        public static Link PrevLink(this IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null);
            return GetStandardLink(getLinks, Keywords.Prev);
        }

        public static Link RelatedLink(this IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null);
            return GetStandardLink(getLinks, Keywords.Related);
        }

        public static Link SelfLink(this IGetLinks getLinks)
        {
            Contract.Requires(getLinks != null);
            return GetStandardLink(getLinks, Keywords.Self);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Link GetStandardLink(IGetLinks getLinks, string rel)
        {
            Contract.Requires(getLinks != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            Link standardLink;
            return getLinks.TryGetLink(rel, out standardLink) ? standardLink : null;
        }
        #endregion
    }
}
