// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Represents an immutable json:api error source object.</summary>
    [JsonConverter(typeof(ErrorSourceConverter))]
    public class ErrorSource : JsonObject
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ErrorSource(string parameter, string pointer)
        {
            this.Parameter = parameter;
            this.Pointer = pointer;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public string Parameter { get; }
        public string Pointer { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static ErrorSource CreateFromJsonPointer(string jsonPointer)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(jsonPointer) == false);

            var errorSource = new ErrorSource(null, jsonPointer);
            return errorSource;
        }

        public static ErrorSource CreateFromQueryParameter(string queryParameter)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(queryParameter) == false);

            var errorSource = new ErrorSource(queryParameter, null);
            return errorSource;
        }
        #endregion
    }
}