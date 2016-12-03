// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents an exception that is thrown when an internal DOM error
    /// occurs while either reading, building, or writing a JSON API document.
    /// </summary>
    internal class DomException : ErrorException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomException()
        { }

        public DomException(string detail)
            : base(null, HttpStatusCode.InternalServerError, null, CoreErrorStrings.DomExceptionTitle, detail)
        { }

        public DomException(string detail, Exception innerException)
            : base(null, HttpStatusCode.InternalServerError, null, CoreErrorStrings.DomExceptionTitle, detail, innerException)
        { }
        #endregion
    }
}