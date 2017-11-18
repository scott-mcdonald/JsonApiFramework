// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi.Internal;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public class ResourceIdentifierConverter : JsonConverter<ResourceIdentifier>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region JsonConverter<T> Overrides
        protected override ResourceIdentifier DeserializeJsonObject(JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var clrResourceIdentifier = jObject.ReadResourceIdentifierObject(serializer);
            return clrResourceIdentifier;
        }

        protected override void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, ResourceIdentifier clrResourceIdentifier)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrResourceIdentifier != null);

            writer.WriteResourceIdentifierObject(serializer, clrResourceIdentifier);
        }
        #endregion
    }
}