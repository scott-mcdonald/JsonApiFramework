// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// JSON.Net converter for <c>Meta</c> objects.
/// </summary>
public class MetaConverter : Converter<Meta>
{
    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected override Meta ReadTypedObject(JsonElement metaJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        var meta = (Meta)metaJsonElement;
        return meta;
    }

    protected override void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions options, Meta meta)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(meta != null);

        var metaJsonElement = (JsonElement)meta;
        metaJsonElement.WriteTo(writer);
    }
    #endregion
}