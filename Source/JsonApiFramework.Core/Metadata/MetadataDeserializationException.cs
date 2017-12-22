// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Properties;

namespace JsonApiFramework.Metadata
{
    /// <summary>
    /// Represents a specializaiton of <c>MetadataException</c> for exceptions
    /// that are thrown when deserializing JSON for any of the metadata classes of the service model.
    /// </summary>
    public class MetadataDeserializationException : MetadataException
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public MetadataDeserializationException(string message)
            : base(message)
        { }

        public MetadataDeserializationException(string message, Exception innerException)
            : base(message, innerException)
        { }
        #endregion

        // PUBLIC OPERATORS /////////////////////////////////////////////////
        #region Conversion Operators
        public static explicit operator Error(MetadataDeserializationException metadataDeserializationException)
        {
            Contract.Requires(metadataDeserializationException != null);

            var error = Error.Create(metadataDeserializationException, CoreErrorStrings.MetadataDeserializationExceptionTitle);
            return error;
        }
        #endregion

    }
}