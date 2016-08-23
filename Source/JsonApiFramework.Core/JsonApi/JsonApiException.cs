// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents a general violation of the json:api standard.</summary>
    public class JsonApiException : ErrorException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonApiException()
        { }

        public JsonApiException(string title, string detail)
            : base(null, HttpStatusCode.InternalServerError, null, title, detail)
        { }

        public JsonApiException(string title, string detail, Exception innerException)
            : base(null, HttpStatusCode.InternalServerError, null, title, detail, innerException)
        { }
        #endregion
    }
}