// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for <c>Resource</c> objects.
    /// </summary>
    public class ResourceConverter : Converter<Resource>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override Resource ReadTypedObject(JObject resourceJObject, JsonSerializer serializer)
        {
            Contract.Requires(resourceJObject != null);
            Contract.Requires(serializer != null);

            var resource = new Resource();

            ReadType(resourceJObject, serializer, resource);
            ReadId(resourceJObject, serializer, resource);
            ReadAttributes(resourceJObject, serializer, resource);
            ReadRelationships(resourceJObject, serializer, resource);
            ReadLinks(resourceJObject, serializer, resource);
            ReadMeta(resourceJObject, serializer, resource);

            return resource;
        }

        protected override void WriteTypedObject(JsonWriter writer, JsonSerializer serializer, Resource resource)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(resource != null);

            writer.WriteStartObject();

            WriteType(writer, serializer, resource);
            WriteId(writer, serializer, resource);
            WriteAttributes(writer, serializer, resource);
            WriteRelationships(writer, serializer, resource);
            WriteLinks(writer, serializer, resource);
            WriteMeta(writer, serializer, resource);

            writer.WriteEndObject();
        }
        #endregion
    }
}