// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework
{
    /// <summary>
    /// Represents an exception that is thrown when an internal error occurs
    /// while writing a JSON API document.
    /// </summary>
    public class DocumentWriteException : ErrorException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentWriteException()
        { }

        public DocumentWriteException(string detail)
            : base(null, HttpStatusCode.InternalServerError, null, InfrastructureErrorStrings.DocumentWriteExceptionTitle, detail)
        { }

        public DocumentWriteException(string detail, Exception innerException)
            : base(null, HttpStatusCode.InternalServerError, null, InfrastructureErrorStrings.DocumentWriteExceptionTitle, detail, innerException)
        { }
        #endregion
    }
}