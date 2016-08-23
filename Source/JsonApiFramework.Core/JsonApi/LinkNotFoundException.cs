// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents the exception that is thrown from a <c>Links</c> object
    /// when a requested <c>Link</c> is not found.
    /// </summary>
    public class LinkNotFoundException : ErrorException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinkNotFoundException(string rel)
            : base(null, HttpStatusCode.NotFound, null, CoreErrorStrings.LinksLinkNotFoundTitle, CoreErrorStrings.LinksLinkNotFoundDetail.FormatWith(rel))
        { this.Rel = rel; }

        public LinkNotFoundException(string rel, Exception innerException)
            : base(null, HttpStatusCode.NotFound, null, CoreErrorStrings.LinksLinkNotFoundTitle, CoreErrorStrings.LinksLinkNotFoundDetail.FormatWith(rel), innerException)
        { this.Rel = rel; }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string Rel { get; private set; }
        #endregion
    }
}