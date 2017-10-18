// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Collections;
using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api links object.</summary>
    [JsonConverter(typeof(LinksConverter))]
    public class Links : JsonObject
        , IEnumerable<KeyValuePair<string, Link>>
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
        public Link About => this.TryGetLink(Keywords.About, out var link) ? link : null;
        public Link Canonical => this.TryGetLink(Keywords.Canonical, out var link) ? link : null;
        public Link First => this.TryGetLink(Keywords.First, out var link) ? link : null;
        public Link Last => this.TryGetLink(Keywords.Last, out var link) ? link : null;
        public Link Next => this.TryGetLink(Keywords.Next, out var link) ? link : null;
        public Link Prev => this.TryGetLink(Keywords.Prev, out var link) ? link : null;
        public Link Related => this.TryGetLink(Keywords.Related, out var link) ? link : null;
        public Link Self => this.TryGetLink(Keywords.Self, out var link) ? link : null;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var content = String.Join(ToStringDelimiter, this.OrderedReadOnlyLinkDictionary.Select(x => $"{x.Key}={x.Value.ToString()}"));
            return "{0} [{1}]".FormatWith(TypeName, content);
        }
        #endregion

        #region IEnumerable<KeyValuePair<string, Link>> Implementation
        public IEnumerator<KeyValuePair<string, Link>> GetEnumerator()
        { return this.OrderedReadOnlyLinkDictionary.GetEnumerator(); }
        #endregion

        #region IEnumerable Implementation
        IEnumerator IEnumerable.GetEnumerator()
        { return this.GetEnumerator(); }
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

            if (this.TryGetLink(rel, out var link))
                return link.HRef;

            throw LinksException.CreateNotFoundException(rel);
        }

        public Link GetLink(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            if (this.TryGetLink(rel, out var link))
                return link;

            throw LinksException.CreateNotFoundException(rel);
        }

        public IEnumerable<Link> GetLinks()
        { return this.OrderedReadOnlyLinkDictionary.Values; }

        public Uri GetUri(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            if (this.TryGetLink(rel, out var link))
                return new Uri(link.HRef);

            throw LinksException.CreateNotFoundException(rel);
        }

        public IEnumerable<string> GetRels()
        { return this.OrderedReadOnlyLinkDictionary.Keys; }

        public bool TryGetHRef(string rel, out string hRef)
        {
            hRef = null;

            if (String.IsNullOrWhiteSpace(rel))
                return false;

            if (this.OrderedReadOnlyLinkDictionary.TryGetValue(rel, out var link) == false)
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

            if (this.OrderedReadOnlyLinkDictionary.TryGetValue(rel, out var link) == false)
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
        private const string ToStringDelimiter = " ";
        private static readonly string TypeName = typeof(Links).Name;
        #endregion
    }
}