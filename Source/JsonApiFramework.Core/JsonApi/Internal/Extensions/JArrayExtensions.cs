// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.JsonApi.Internal
{
    /// <summary>Extension methods specific for json:api for the JSON.NET JArray class.</summary>
    internal static class JArrayExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static IEnumerable<ResourceIdentifier> ReadResourceIdentifierArray(this JArray jArray, JsonSerializer serializer)
        {
            Contract.Requires(jArray != null);
            Contract.Requires(serializer != null);

            var clrResourceIdentifierCollection = jArray
                .Where(jToken => jToken.Type == JTokenType.Object)
                .Select(jToken =>
                {
                    var jObject = (JObject)jToken;
                    var clrResourceIdentifier = jObject.ReadResourceIdentifierObject(serializer);
                    return clrResourceIdentifier;
                });
            return clrResourceIdentifierCollection;
        }
        #endregion
    }
}
