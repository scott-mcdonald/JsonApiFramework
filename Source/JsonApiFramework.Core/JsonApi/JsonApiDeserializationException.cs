// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an unexpected exception during deserialization of JSON with respect to the json:api specification.</summary>
    public class JsonApiDeserializationException : JsonApiException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonApiDeserializationException(HttpStatusCode statusCode, Error error)
            : base(statusCode, error)
        { }

        public JsonApiDeserializationException(HttpStatusCode statusCode, Error error, Exception innerException)
            : base(statusCode, error, innerException)
        { }

        public JsonApiDeserializationException(HttpStatusCode statusCode, IEnumerable<Error> errors)
            : base(statusCode, errors)
        { }

        public JsonApiDeserializationException(HttpStatusCode statusCode, IEnumerable<Error> errors, Exception innerException)
            : base(statusCode, errors, innerException)
        { }
        #endregion
    }
}