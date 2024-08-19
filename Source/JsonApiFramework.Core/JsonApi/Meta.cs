// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonApiFramework.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// Represents a json:api compliant meta object.
/// </summary>
/// <see cref="http://jsonapi.org"/>
[JsonConverter(typeof(MetaConverter))]
public class Meta : JsonObject
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Object Overrides
    public override string ToString()
    {
        var metaAsJson = this.JsonElement.SafeToString();
        return string.Format("{0} [{1}]", TypeName, metaAsJson);
    }
    #endregion

    #region Metadata Methods
    public static Meta Create<T>(T data)
    {
        var jObject = JsonSerializer.SerializeToElement(data);
        var meta = (Meta)jObject;
        return meta;
    }

    public static Meta Create<T>(T data, JsonSerializerOptions jsonSerializerOptions)
    {
        Contract.Requires(jsonSerializerOptions != null);

        var element = JsonSerializer.SerializeToElement(data, jsonSerializerOptions);
        var meta = (Meta)element;
        return meta;
    }

    public T GetData<T>() => JsonElement.Deserialize<T>() ?? default(T);

    public void SetData<T>(T data) => JsonElement = JsonSerializer.SerializeToElement(data);
    #endregion

    // PUBLIC OPERATORS /////////////////////////////////////////////////
    #region Conversion Operators
    public static implicit operator JsonElement(Meta meta) => meta.JsonElement;

    public static implicit operator Meta(JsonElement jObject) => new() { JsonElement = jObject };
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Non-JSON Properties
    private JsonElement JsonElement { get; set; }
    #endregion

    // PRIVATE FIELDS ///////////////////////////////////////////////////
    #region Fields
    private static readonly string TypeName = typeof(Meta).Name;
    #endregion
}