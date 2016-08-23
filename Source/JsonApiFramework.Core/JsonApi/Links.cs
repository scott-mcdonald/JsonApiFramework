// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    [JsonDictionary]
    public class Links : JsonDictionary<Link>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Links()
        { }

        public Links(IDictionary<string, Link> dictionary)
            : base(dictionary)
        { }

        public Links(int capacity)
            : base(capacity)
        { }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Standard Links
        public Link About { get { this.ValidateLinkExists(Keywords.About); return this[Keywords.About]; } set { this[Keywords.About] = value; } }
        public Link Canonical { get { this.ValidateLinkExists(Keywords.Canonical); return this[Keywords.Canonical]; } set { this[Keywords.Canonical] = value; } }
        public Link First { get { this.ValidateLinkExists(Keywords.First); return this[Keywords.First]; } set { this[Keywords.First] = value; } }
        public Link Last { get { this.ValidateLinkExists(Keywords.Last); return this[Keywords.Last]; } set { this[Keywords.Last] = value; } }
        public Link Next { get { this.ValidateLinkExists(Keywords.Next); return this[Keywords.Next]; } set { this[Keywords.Next] = value; } }
        public Link Prev { get { this.ValidateLinkExists(Keywords.Prev); return this[Keywords.Prev]; } set { this[Keywords.Prev] = value; } }
        public Link Related { get { this.ValidateLinkExists(Keywords.Related); return this[Keywords.Related]; } set { this[Keywords.Related] = value; } }
        public Link Self { get { this.ValidateLinkExists(Keywords.Self); return this[Keywords.Self]; } set { this[Keywords.Self] = value; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            if (!this.Any())
                return String.Format("{0} []", TypeName);

            var content = this.Select(x => String.Format("{0}={1}", x.Key, x.Value.ToString()))
                              .Aggregate((current, next) => String.Format("{0} {1}", current, next));

            return String.Format("{0} [{1}]", TypeName, content);
        }
        #endregion

        #region Contains Methods
        public bool ContainsLink(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.ContainsKey(rel);
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
            if (this.TryGetValue(rel, out link) == false)
            {
                return false;
            }

            hRef = link.HRef;
            return true;
        }

        public bool TryGetLink(string rel, out Link link)
        {
            link = null;
            return !String.IsNullOrWhiteSpace(rel) && this.TryGetValue(rel, out link);
        }

        public bool TryGetUri(string rel, out Uri uri)
        {
            uri = null;

            if (String.IsNullOrWhiteSpace(rel))
                return false;

            Link link;
            if (this.TryGetValue(rel, out link) == false)
            {
                return false;
            }

            uri = new Uri(link.HRef);
            return true;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Validation Methods
        private void ValidateLinkExists(string rel)
        {
            if (String.IsNullOrWhiteSpace(rel))
                throw new LinkNotFoundException(rel);

            if (this.ContainsKey(rel))
                return;

            throw new LinkNotFoundException(rel);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Links).Name;
        #endregion
    }
}