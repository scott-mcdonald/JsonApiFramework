// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Properties;

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents an exception that is thrown for an invalid operation in any of the metadata classes of the service model.</summary>
    public class MetadataException : Exception
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public MetadataException(string message)
            : base(message)
        { }

        public MetadataException(string message, Exception innerException)
            : base(message, innerException)
        { }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Conversion Operators
        public static explicit operator Error(MetadataException metadataException)
        {
            Contract.Requires(metadataException != null);

            var error = Error.Create(metadataException, CoreErrorStrings.MetadataExceptionTitle);
            return error;
        }
        #endregion
    }
}