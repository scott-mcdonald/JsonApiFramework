// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.JsonApi.Internal
{
    /// <summary>Extension methods specific for json:api for the JSON.NET JValue class.</summary>
    internal static class JValueExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static HttpStatusCode? ReadStatusValue(this JValue jValue, JsonSerializer serializer)
        {
            Contract.Requires(jValue != null);
            Contract.Requires(serializer != null);

            var statusAsString = (string)jValue;
            if (String.IsNullOrWhiteSpace(statusAsString))
                return default(HttpStatusCode?);

            if (!Int32.TryParse(statusAsString, out var statusAsInteger))
                return default(HttpStatusCode?);

            var status = (HttpStatusCode)statusAsInteger;
            return status;
        }
        #endregion
    }
}
