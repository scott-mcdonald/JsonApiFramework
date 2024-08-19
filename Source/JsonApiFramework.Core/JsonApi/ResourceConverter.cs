// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// JSON.Net converter for <c>Resource</c> objects.
/// </summary>
public class ResourceConverter : Converter<Resource>
{
    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected override Resource ReadTypedObject(JsonElement resourceJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        var resource = new Resource();

        ReadType(resourceJsonElement, options, resource);
        ReadId(resourceJsonElement, options, resource);
        ReadAttributes(resourceJsonElement, options, resource);
        ReadRelationships(resourceJsonElement, options, resource);
        ReadLinks(resourceJsonElement, options, resource);
        ReadMeta(resourceJsonElement, options, resource);

        return resource;
    }

    protected override void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions options, Resource resource)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(resource != null);

        writer.WriteStartObject();

        WriteType(writer, options, resource);
        WriteId(writer, options, resource);
        WriteAttributes(writer, options, resource);
        WriteRelationships(writer, options, resource);
        WriteLinks(writer, options, resource);
        WriteMeta(writer, options, resource);

        writer.WriteEndObject();
    }
    #endregion
}