// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api link object.</summary>
    public class Link : IGetMeta
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Link(string hRef)
            : this(null, hRef)
        { }

        public Link(Uri uri)
            : this(null, uri.ToString())
        { }

        public Link(Meta meta, Uri uri)
            : this(meta, uri.ToString())
        { }

        public Link(Meta meta, string hRef)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(hRef));

            this.Meta = meta;
            this.HRef = hRef.ToLowerInvariant();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public Meta Meta { get; }
        public string HRef { get; }
        #endregion

        #region Calculated Properties
        public IEnumerable<string> PathSegments => this.Uri.GetPathSegments();

        public Uri Uri => new Uri(this.HRef, UriKind.RelativeOrAbsolute);
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Conversion Operators
        public static implicit operator string(Link link)
        {
            return link.HRef;
        }

        public static implicit operator Uri(Link link)
        {
            return link.Uri;
        }

        public static implicit operator Link(string hRef)
        {
            return new Link(hRef);
        }

        public static implicit operator Link(Uri uri)
        {
            return new Link(uri);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var hRef = this.HRef ?? String.Empty;
            return $"{TypeName} [hRef={hRef}]";
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(Link).Name;
        #endregion
    }
}