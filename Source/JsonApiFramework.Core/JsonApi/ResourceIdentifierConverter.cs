// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// JSON.Net converter for <c>ResourceIdentifier</c> objects.
/// </summary>
public class ResourceIdentifierConverter : Converter<ResourceIdentifier>
{
    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected override ResourceIdentifier ReadTypedObject(JsonElement resourceIdentifierJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        var resourceIdentifier = new ResourceIdentifier();

        ReadType(resourceIdentifierJsonElement, options, resourceIdentifier);
        ReadId(resourceIdentifierJsonElement, options, resourceIdentifier);
        ReadMeta(resourceIdentifierJsonElement, options, resourceIdentifier);

        return resourceIdentifier;
    }

    protected override void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions options, ResourceIdentifier resourceIdentifier)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(resourceIdentifier != null);

        writer.WriteStartObject();

        WriteType(writer, options, resourceIdentifier);
        WriteId(writer, options, resourceIdentifier);
        WriteMeta(writer, options, resourceIdentifier);

        writer.WriteEndObject();
    }
    #endregion
}