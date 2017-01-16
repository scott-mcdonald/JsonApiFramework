// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api link object.</summary>
    public class Link : JsonObject
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Link(string hRef)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(hRef));

            this.HRef = hRef.ToLowerInvariant();
        }

        public Link(Uri uri)
        {
            Contract.Requires(uri != null);

            var hRef = uri.ToString();
            this.HRef = hRef.ToLowerInvariant();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public string HRef { get; }
        #endregion

        #region Non-JSON Properties
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
    }
}