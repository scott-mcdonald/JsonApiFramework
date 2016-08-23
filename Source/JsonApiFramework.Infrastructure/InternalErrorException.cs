// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework
{
    /// <summary>
    /// Represents an exception that is thrown when a general purpose
    /// internal error occurs.
    /// </summary>
    public class InternalErrorException : ErrorException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public InternalErrorException()
        { }

        public InternalErrorException(string detail)
            : base(null, HttpStatusCode.InternalServerError, null, InfrastructureErrorStrings.InternalErrorExceptionTitle, detail)
        { }

        public InternalErrorException(string detail, Exception innerException)
            : base(null, HttpStatusCode.InternalServerError, null, InfrastructureErrorStrings.InternalErrorExceptionTitle, detail, innerException)
        { }
        #endregion
    }
}