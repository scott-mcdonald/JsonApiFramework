// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// JSON.Net converter for <c>Error</c> objects.
/// </summary>
public class ErrorConverter : Converter<Error>
{
    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected override Error ReadTypedObject(JsonElement errorJsonElement, JsonSerializerOptions options)
    {
        Contract.Requires(options != null);

        var error = new Error();

        ReadId(errorJsonElement, options, error);
        ReadStatus(errorJsonElement, options, error);
        ReadCode(errorJsonElement, options, error);
        ReadTitle(errorJsonElement, options, error);
        ReadDetail(errorJsonElement, options, error);
        ReadSource(errorJsonElement, options, error);
        ReadLinks(errorJsonElement, options, error);
        ReadMeta(errorJsonElement, options, error);

        return error;
    }

    protected override void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions options, Error error)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(error != null);

        writer.WriteStartObject();

        WriteId(writer, options, error);
        WriteStatus(writer, options, error);
        WriteCode(writer, options, error);
        WriteTitle(writer, options, error);
        WriteDetail(writer, options, error);
        WriteSource(writer, options, error);
        WriteLinks(writer, options, error);
        WriteMeta(writer, options, error);

        writer.WriteEndObject();
    }
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Read Methods
    // ReSharper disable once UnusedParameter.Local
    private static void ReadId(JsonElement errorJsonElement, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        var id = ReadString(errorJsonElement, Keywords.Id);
        error.Id = id;
    }

    // ReSharper disable once UnusedParameter.Local
    private static void ReadStatus(JsonElement errorJsonElement, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        var status = ReadString(errorJsonElement, Keywords.Status);
        error.Status = status;
    }

    // ReSharper disable once UnusedParameter.Local
    private static void ReadCode(JsonElement errorJsonElement, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        var code = ReadString(errorJsonElement, Keywords.Code);
        error.Code = code;
    }

    // ReSharper disable once UnusedParameter.Local
    private static void ReadTitle(JsonElement errorJsonElement, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        var title = ReadString(errorJsonElement, Keywords.Title);
        error.Title = title;
    }

    // ReSharper disable once UnusedParameter.Local
    private static void ReadDetail(JsonElement errorJsonElement, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        var detail = ReadString(errorJsonElement, Keywords.Detail);
        error.Detail = detail;
    }

    // ReSharper disable once UnusedParameter.Local
    private static void ReadSource(JsonElement errorJsonElement, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        try
        {
            var sourceJsonElement = errorJsonElement.GetProperty(Keywords.Source);
            error.Source = sourceJsonElement;
        }
        catch (KeyNotFoundException)
        {
            return;
        }
    }
    #endregion

    #region Write Methods
    // ReSharper disable once UnusedParameter.Local
    private static void WriteId(Utf8JsonWriter writer, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        WriteString(writer, serializer, Keywords.Id, error.Id);
    }

    // ReSharper disable once UnusedParameter.Local
    private static void WriteStatus(Utf8JsonWriter writer, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        WriteString(writer, serializer, Keywords.Status, error.Status);
    }

    // ReSharper disable once UnusedParameter.Local
    private static void WriteCode(Utf8JsonWriter writer, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        WriteString(writer, serializer, Keywords.Code, error.Code);
    }

    // ReSharper disable once UnusedParameter.Local
    private static void WriteTitle(Utf8JsonWriter writer, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        WriteString(writer, serializer, Keywords.Title, error.Title);
    }

    // ReSharper disable once UnusedParameter.Local
    private static void WriteDetail(Utf8JsonWriter writer, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        WriteString(writer, serializer, Keywords.Detail, error.Detail);
    }

    // ReSharper disable once UnusedParameter.Local
    private static void WriteSource(Utf8JsonWriter writer, JsonSerializerOptions serializer, Error error)
    {
        Contract.Requires(writer != null);
        Contract.Requires(serializer != null);
        Contract.Requires(error != null);

        if (error.Source == null)
            return;

        writer.WritePropertyName(Keywords.Source);
        ((JsonElement)error.Source).WriteTo(writer);
    }
    #endregion
}