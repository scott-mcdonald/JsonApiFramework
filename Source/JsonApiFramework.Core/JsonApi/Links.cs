// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Collections;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api links object.</summary>
    public class Links
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Links()
            : this(null)
        { }

        public Links(IReadOnlyDictionary<string, Link> dictionary)
        {
            this.OrderedReadOnlyLinkDictionary = new OrderedReadOnlyDictionary<string, Link>(dictionary ?? new Dictionary<string, Link>(), StringComparer.OrdinalIgnoreCase);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Standard Links
        public Link About { get { Link link; return this.TryGetLink(Keywords.About, out link) ? link : null; } }
        public Link Canonical { get { Link link; return this.TryGetLink(Keywords.Canonical, out link) ? link : null; } }
        public Link First { get { Link link; return this.TryGetLink(Keywords.First, out link) ? link : null; } }
        public Link Last { get { Link link; return this.TryGetLink(Keywords.Last, out link) ? link : null; } }
        public Link Next { get { Link link; return this.TryGetLink(Keywords.Next, out link) ? link : null; } }
        public Link Prev { get { Link link; return this.TryGetLink(Keywords.Prev, out link) ? link : null; } }
        public Link Related { get { Link link; return this.TryGetLink(Keywords.Related, out link) ? link : null; } }
        public Link Self { get { Link link; return this.TryGetLink(Keywords.Self, out link) ? link : null; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            if (!this.OrderedReadOnlyLinkDictionary.Any())
                return "{0} []".FormatWith(TypeName);

            var content = this.OrderedReadOnlyLinkDictionary
                              .Select(x => $"{x.Key}={x.Value.ToString()}")
                              .Aggregate((current, next) => $"{current} {next}");

            return "{0} [{1}]".FormatWith(TypeName, content);
        }
        #endregion

        #region Contains Methods
        public bool ContainsLink(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.OrderedReadOnlyLinkDictionary.ContainsKey(rel);
        }
        #endregion

        #region Get Methods
        public string GetHRef(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            Link link;
            if (this.TryGetLink(rel, out link))
                return link.HRef;

            throw new LinkNotFoundException(rel);
        }

        public Link GetLink(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            Link link;
            if (this.TryGetLink(rel, out link))
                return link;

            throw new LinkNotFoundException(rel);
        }

        public Uri GetUri(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            Link link;
            if (this.TryGetLink(rel, out link))
                return new Uri(link.HRef);

            throw new LinkNotFoundException(rel);
        }

        public bool TryGetHRef(string rel, out string hRef)
        {
            hRef = null;

            if (String.IsNullOrWhiteSpace(rel))
                return false;

            Link link;
            if (this.OrderedReadOnlyLinkDictionary.TryGetValue(rel, out link) == false)
            {
                return false;
            }

            hRef = link.HRef;
            return true;
        }

        public bool TryGetLink(string rel, out Link link)
        {
            link = null;
            return !String.IsNullOrWhiteSpace(rel) && this.OrderedReadOnlyLinkDictionary.TryGetValue(rel, out link);
        }

        public bool TryGetUri(string rel, out Uri uri)
        {
            uri = null;

            if (String.IsNullOrWhiteSpace(rel))
                return false;

            Link link;
            if (this.OrderedReadOnlyLinkDictionary.TryGetValue(rel, out link) == false)
            {
                return false;
            }

            uri = new Uri(link.HRef);
            return true;
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly Links Empty = new Links();
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private OrderedReadOnlyDictionary<string, Link> OrderedReadOnlyLinkDictionary { get; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Links).Name;
        #endregion
    }
}