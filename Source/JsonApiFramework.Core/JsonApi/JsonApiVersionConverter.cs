// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for <c>JsonApiVersion</c> objects.
    /// </summary>
    public class JsonApiVersionConverter : Converter<JsonApiVersion>
    {
        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override JsonApiVersion ReadTypedObject(JObject jsonApiJObject, JsonSerializer serializer)
        {
            Contract.Requires(jsonApiJObject != null);
            Contract.Requires(serializer != null);

            var jsonApi = new JsonApiVersion();

            ReadVersion(jsonApiJObject, serializer, jsonApi);
            ReadMeta(jsonApiJObject, serializer, jsonApi);

            return jsonApi;
        }

        protected override void WriteTypedObject(JsonWriter writer, JsonSerializer serializer, JsonApiVersion jsonApi)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(jsonApi != null);

            writer.WriteStartObject();

            WriteVersion(writer, serializer, jsonApi);
            WriteMeta(writer, serializer, jsonApi);

            writer.WriteEndObject();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Read Methods
        // ReSharper disable once UnusedParameter.Local
        private static void ReadVersion(JToken versionJToken, JsonSerializer serializer, JsonApiVersion jsonApi)
        {
            Contract.Requires(versionJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(jsonApi != null);

            var version = ReadString(versionJToken, Keywords.Version);
            jsonApi.Version = version;
        }
        #endregion

        #region Write Methods
        // ReSharper disable once UnusedParameter.Local
        private static void WriteVersion(JsonWriter writer, JsonSerializer serializer, JsonApiVersion jsonApi)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(jsonApi != null);

            var version = jsonApi.Version;
            WriteString(writer, serializer, Keywords.Version, version);
        }
        #endregion
    }
}