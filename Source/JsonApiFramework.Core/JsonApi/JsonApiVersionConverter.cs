// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// JSON.Net converter for <c>JsonApiVersion</c> objects.
/// </summary>
public class JsonApiVersionConverter : Converter<JsonApiVersion>
{
    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected override JsonApiVersion ReadTypedObject(JsonElement jsonApiJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        var jsonApi = new JsonApiVersion();

        ReadVersion(jsonApiJsonElement, options, jsonApi);
        ReadMeta(jsonApiJsonElement, options, jsonApi);

        return jsonApi;
    }

    protected override void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions serializer, JsonApiVersion jsonApi)
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
    private static void ReadVersion(JsonElement versionJsonElement, JsonSerializerOptions serializer, JsonApiVersion jsonApi)
    {
        Contract.Requires(serializer != null);
        Contract.Requires(jsonApi != null);

        var version = ReadString(versionJsonElement, Keywords.Version);
        jsonApi.Version = version;
    }
    #endregion

    #region Write Methods
    // ReSharper disable once UnusedParameter.Local
    private static void WriteVersion(Utf8JsonWriter writer, JsonSerializerOptions serializer, JsonApiVersion jsonApi)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(jsonApi != null);

        var version = jsonApi.Version;
        WriteString(writer, serializer, Keywords.Version, version);
    }
    #endregion
}