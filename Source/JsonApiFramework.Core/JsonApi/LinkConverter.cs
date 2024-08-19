// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// JSON.Net converter for <c>Link</c> objects.
/// </summary>
public class LinkConverter : Converter<Link>
{
    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected override Link ReadTypedString(string hRef)
    {
        var link = new Link
            {
                HRef = hRef
            };
        return link;
    }

    protected override Link ReadTypedObject(JsonElement linkJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        var link = new Link();

        ReadHRef(linkJsonElement, options, link);
        ReadMeta(linkJsonElement, options, link);

        return link;
    }

    protected override void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions options, Link link)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(link != null);

        // If HRef only, then serialize the Link object as a string whose
        // value is the HRef value.
        if (link.Meta == null && string.IsNullOrWhiteSpace(link.HRef) == false)
        {
            var hRef = link.HRef;
            writer.WriteStringValue(hRef);
            return;
        }

        // Serialize the Link object as a JSON object.
        writer.WriteStartObject();

        WriteHRef(writer, options, link);
        WriteMeta(writer, options, link);

        writer.WriteEndObject();
    }
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Methods
    // ReSharper disable once UnusedParameter.Local
    private static void ReadHRef(JsonElement linkJsonElement, JsonSerializerOptions serializer, Link link)
    {
        Contract.Requires(serializer != null);
        Contract.Requires(link != null);

        var hRef = ReadString(linkJsonElement, Keywords.HRef);
        link.HRef = hRef;
    }

    // ReSharper disable once UnusedParameter.Local
    private static void WriteHRef(Utf8JsonWriter writer, JsonSerializerOptions serializer, Link link)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(link != null);

        var hRef = link.HRef;
        WriteString(writer, serializer, Keywords.HRef, hRef);
    }
    #endregion
}