// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for <c>ResourceIdentifier</c> objects.
    /// </summary>
    public class ResourceIdentifierConverter : Converter<ResourceIdentifier>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override ResourceIdentifier ReadObject(JObject resourceIdentifierJObject, JsonSerializer serializer)
        {
            Contract.Requires(resourceIdentifierJObject != null);
            Contract.Requires(serializer != null);

            var resourceIdentifier = new ResourceIdentifier();

            ReadType(resourceIdentifierJObject, serializer, resourceIdentifier);
            ReadId(resourceIdentifierJObject, serializer, resourceIdentifier);
            ReadMeta(resourceIdentifierJObject, serializer, resourceIdentifier);

            return resourceIdentifier;
        }

        protected override void WriteObject(JsonWriter writer, JsonSerializer serializer, ResourceIdentifier resourceIdentifier)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(resourceIdentifier != null);

            writer.WriteStartObject();

            WriteType(writer, serializer, resourceIdentifier);
            WriteId(writer, serializer, resourceIdentifier);
            WriteMeta(writer, serializer, resourceIdentifier);

            writer.WriteEndObject();
        }
        #endregion
    }
}