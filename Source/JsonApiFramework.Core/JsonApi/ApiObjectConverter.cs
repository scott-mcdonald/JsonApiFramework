// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for <c>ApiObject</c>.
    /// </summary>
    public class ApiObjectConverter : Converter<ApiObject>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override ApiObject ReadTypedObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);

            var apiObject = ReadApiObject(jObject, serializer);
            return apiObject;
        }

        protected override void WriteTypedObject(JsonWriter writer, JsonSerializer serializer, ApiObject attributes)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(attributes != null);

            WriteApiObject(writer, serializer, attributes);
        }
        #endregion
    }
}