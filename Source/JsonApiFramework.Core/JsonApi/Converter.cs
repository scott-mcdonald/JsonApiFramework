// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// Baseclass that encapsulates boilerplate JSON.Net converter code.
/// </summary>
public abstract class Converter<T> : JsonConverter<T>
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region JsonConverter Overrides
    public override bool CanConvert(Type objectType)
    {
        Contract.Requires(objectType != null);

        var objectTypeInfo = objectType.GetTypeInfo();
        var canConvert = TypeInfo.IsAssignableFrom(objectTypeInfo);
        return canConvert;
    }

    public override T Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        Contract.Requires(objectType != null);
        Contract.Requires(options != null);

        switch (reader.TokenType)
        {
            case JsonTokenType.None:
            case JsonTokenType.Null:
                return default(T);

            case JsonTokenType.String:
                {
                    var stringValue = reader.GetString();
                    var typedObject = this.ReadTypedString(stringValue);
                    return typedObject;
                }

            case JsonTokenType.StartObject:
                {
                    var element = JsonElement.ParseValue(ref reader);
                    var typedObject = this.ReadTypedObject(element, options);
                    return typedObject;
                }

            default:
                throw new ArgumentOutOfRangeException($"The '{reader.TokenType}' token type is not supported.");
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);

        if (value == null)
            return;

        this.WriteTypedObject(writer, options, value);
    }
    #endregion

    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Converter Overrides
    protected virtual T ReadTypedString(string stringValue)
    { throw new NotImplementedException(); }

    protected abstract T ReadTypedObject(JsonElement element, JsonSerializerOptions options);

    protected abstract void WriteTypedObject(Utf8JsonWriter writer, JsonSerializerOptions options, T typedObject);
    #endregion

    // PROTECTED METHODS ////////////////////////////////////////////////
    #region Read Methods
    protected static ApiObject ReadApiObject(JsonElement element, JsonSerializerOptions options)
    {
        var apiProperties = element
            .EnumerateObject()
            .Select(x =>
            {
                var propertyName = x.Name;
                var propertyValue = x.Value;
                var apiProperty = new ApiReadProperty(propertyName, propertyValue);
                return apiProperty;
            })
            .ToList();

        return new ApiObject(apiProperties);
    }

    protected static void ReadAttributes(JsonElement parentElement, JsonSerializerOptions options, ISetAttributes setAttributes)
    {
        Contract.Requires(options != null);
        Contract.Requires(setAttributes != null);

        try
        {
            var attributesElement = parentElement.GetProperty(Keywords.Attributes);
            var attributes = ReadApiObject(attributesElement, options);
            setAttributes.Attributes = attributes;
        }
        catch (KeyNotFoundException)
        {
            return;
        }
    }

    protected static void ReadId(JsonElement parentElement, JsonSerializerOptions options, ISetResourceIdentity setResourceIdentity)
    {
        Contract.Requires(options != null);
        Contract.Requires(setResourceIdentity != null);

        var id = ReadString(parentElement, Keywords.Id);
        setResourceIdentity.Id = id;
    }

    protected static void ReadLinks(JsonElement parentElement, JsonSerializerOptions options, ISetLinks setLinks)
    {
        Contract.Requires(options != null);
        Contract.Requires(setLinks != null);

        try
        {
            var linksElement = parentElement.GetProperty(Keywords.Links);
            var links = linksElement.Deserialize<Links>(options);
            setLinks.Links = links;
        }
        catch (KeyNotFoundException)
        {
            return;
        }
    }

    protected static void ReadMeta(JsonElement parentElement, JsonSerializerOptions options, ISetMeta setMeta)
    {
        Contract.Requires(options != null);
        Contract.Requires(setMeta != null);

        try
        {
            var metaElement = parentElement.GetProperty(Keywords.Meta);
            var meta = (Meta)metaElement;
            setMeta.Meta = meta;
        }
        catch (KeyNotFoundException)
        {
            return;
        }
    }

    protected static void ReadRelationships(JsonElement parentElement, JsonSerializerOptions options, ISetRelationships setRelationships)
    {
        Contract.Requires(options != null);
        Contract.Requires(setRelationships != null);

        try
        {
            var relationshipsElement = parentElement.GetProperty(Keywords.Relationships);
            var relationships = relationshipsElement.Deserialize<Relationships>(options);
            setRelationships.Relationships = relationships;
        }
        catch (KeyNotFoundException)
        {
            return;
        }
    }

    protected static string ReadString(JsonElement parentElement, string propertyName)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(propertyName) == false);

        try
        {
        var propertyValueElement = parentElement.GetProperty(propertyName);

        var propertyValueJsonValueKind = propertyValueElement.ValueKind;
        switch (propertyValueJsonValueKind)
        {
            case JsonValueKind.Undefined:
            case JsonValueKind.Null:
                return null;

            case JsonValueKind.String:
                return propertyValueElement.GetString();

            default:
                throw new ArgumentOutOfRangeException($"The '{propertyValueJsonValueKind}' token type is not supported.");
        }
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }

    protected static void ReadType(JsonElement parentElement, JsonSerializerOptions options, ISetResourceIdentity setResourceIdentity)
    {
        Contract.Requires(options != null);
        Contract.Requires(setResourceIdentity != null);

        var type = ReadString(parentElement, Keywords.Type);
        setResourceIdentity.Type = type;
    }
    #endregion

    #region Write Methods
    // ReSharper disable once ParameterTypeCanBeEnumerable.Global
    protected static void WriteApiObject(Utf8JsonWriter writer, JsonSerializerOptions options, ApiObject apiObject)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);

        if (apiObject == null)
            return;

        writer.WriteStartObject();
        foreach (var apiProperty in apiObject)
        {
            WriteApiProperty(writer, options, apiProperty);
        }
        writer.WriteEndObject();
    }

    protected static void WriteApiProperty(Utf8JsonWriter writer, JsonSerializerOptions options, ApiProperty apiProperty)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(apiProperty != null);

        if (apiProperty == null)
            return;

        apiProperty.Write(writer, options);
    }

    protected static void WriteAttributes(Utf8JsonWriter writer, JsonSerializerOptions options, IGetAttributes getAttributes)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(getAttributes != null);

        if (getAttributes.Attributes == null)
            return;

        writer.WritePropertyName(Keywords.Attributes);

        var attributes = getAttributes.Attributes;
        WriteApiObject(writer, options, attributes);
    }

    protected static void WriteId(Utf8JsonWriter writer, JsonSerializerOptions options, IGetResourceIdentity getResourceIdentity)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(getResourceIdentity != null);

        WriteString(writer, options, Keywords.Id, getResourceIdentity.Id);
    }

    protected static void WriteLinks(Utf8JsonWriter writer, JsonSerializerOptions options, IGetLinks getLinks)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(getLinks != null);

        if (getLinks.Links == null || getLinks.Links.Any() == false)
            return;

        writer.WritePropertyName(Keywords.Links);
        var linksElement = JsonSerializer.SerializeToElement(getLinks.Links, options);
        linksElement.WriteTo(writer);
    }

    protected static void WriteMeta(Utf8JsonWriter writer, JsonSerializerOptions options, IGetMeta getMeta)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(getMeta != null);

        if (getMeta.Meta == null)
            return;

        var metaElement = (JsonElement)getMeta.Meta;

        writer.WritePropertyName(Keywords.Meta);
        metaElement.WriteTo(writer);
    }

    protected static void WriteRelationships(Utf8JsonWriter writer, JsonSerializerOptions options, IGetRelationships getRelationships)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(getRelationships != null);

        if (getRelationships.Relationships == null || getRelationships.Relationships.Any() == false)
            return;

        writer.WritePropertyName(Keywords.Relationships);
        var relationshipsElement = JsonSerializer.SerializeToElement(getRelationships.Relationships, options);
        relationshipsElement.WriteTo(writer);
    }

    protected static void WriteString(Utf8JsonWriter writer, JsonSerializerOptions options, string propertyName, string propertyStringValue)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(string.IsNullOrWhiteSpace(propertyName) == false);

        if (string.IsNullOrWhiteSpace(propertyStringValue))
        {
            switch (options.DefaultIgnoreCondition)
            {
                case JsonIgnoreCondition.Never:
                    writer.WriteNull(propertyName);
                    return;

                default:
                    // Ignore a null property.
                    return;
            }
        }

        writer.WriteString(propertyName, propertyStringValue);
    }

    protected static void WriteType(Utf8JsonWriter writer, JsonSerializerOptions options, IGetResourceIdentity getResourceIdentity)
    {
        Contract.Requires(writer != null);
        Contract.Requires(options != null);
        Contract.Requires(getResourceIdentity != null);

        WriteString(writer, options, Keywords.Type, getResourceIdentity.Type);
    }
    #endregion

    // PRIVATE FIELDS ///////////////////////////////////////////////////
    #region Constants
    private static readonly TypeInfo TypeInfo = typeof(T).GetTypeInfo();
    #endregion
}