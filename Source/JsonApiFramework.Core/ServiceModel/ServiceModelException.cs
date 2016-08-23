// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Net;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.ServiceModel
{
    /// <summary>
    /// Represents an exception that is thrown when an error occurs while
    /// configuring or reading service model information.
    /// </summary>
    public class ServiceModelException : ErrorException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ServiceModelException()
        { }

        public ServiceModelException(string detail)
            : base(null, HttpStatusCode.InternalServerError, null, CoreErrorStrings.ServiceModelExceptionTitle, detail)
        { }

        public ServiceModelException(string detail, Exception innerException)
            : base(null, HttpStatusCode.InternalServerError, null, CoreErrorStrings.ServiceModelExceptionTitle, detail, innerException)
        { }
        #endregion
    }
}