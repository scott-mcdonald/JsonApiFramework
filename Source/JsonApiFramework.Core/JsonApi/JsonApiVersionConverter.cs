// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// JSON.Net converter for <c>JsonApiVersion</c> objects.
    /// </summary>
    public class JsonApiVersionConverter : Converter<JsonApiVersion>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonApiVersionConverter()
            : base(default(IServiceModel))
        { }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override JsonApiVersion ReadTypedObject(JObject jsonApiJObject, JsonSerializer serializer)
        {
            Contract.Requires(jsonApiJObject != null);
            Contract.Requires(serializer != null);

            var jsonApiVersion = new JsonApiVersion();

            ReadVersion(jsonApiJObject, serializer, jsonApiVersion);
            ReadMeta(jsonApiJObject, serializer, jsonApiVersion);

            return jsonApiVersion;
        }

        protected override void WriteTypedObject(JsonWriter writer, JsonSerializer serializer, JsonApiVersion jsonApiVersion)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(jsonApiVersion != null);

            writer.WriteStartObject();

            WriteVersion(writer, serializer, jsonApiVersion);
            WriteMeta(writer, serializer, jsonApiVersion);

            writer.WriteEndObject();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Read Methods
        // ReSharper disable once UnusedParameter.Local
        private static void ReadVersion(JToken versionJToken, JsonSerializer serializer, JsonApiVersion jsonApiVersion)
        {
            Contract.Requires(versionJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(jsonApiVersion != null);

            var version = ReadString(versionJToken, Keywords.Version);
            jsonApiVersion.Version = version;
        }
        #endregion

        #region Write Methods
        // ReSharper disable once UnusedParameter.Local
        private static void WriteVersion(JsonWriter writer, JsonSerializer serializer, JsonApiVersion jsonApiVersion)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(jsonApiVersion != null);

            var version = jsonApiVersion.Version;
            WriteString(writer, serializer, Keywords.Version, version);
        }
        #endregion
    }
}