// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;

namespace JsonApiFramework.Json;

/// <summary>
/// Base class for any object that wants to be serialized/deserialized
/// into/from JSON respectively.
/// </summary>
public abstract class JsonObject : IJsonObject, IDeepCloneable
{
    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region Properties
    public static JsonSerializerOptions DefaultToJsonSerializerOptions
    { get; set; }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region IJsonObject Implementation
    public string ToJson()
    {
        var toJsonSerializerOptions = JsonObject.DefaultToJsonSerializerOptions;
        var objectType = this.GetType();
        return this.ToJson(toJsonSerializerOptions, objectType);
    }

    public string ToJson<T>()
    {
        var toJsonSerializerOptions = JsonObject.DefaultToJsonSerializerOptions;
        var declaredType = typeof(T);
        return this.ToJson(toJsonSerializerOptions, declaredType);
    }

    public string ToJson(Type declaredType)
    {
        Contract.Requires(declaredType != null);

        var toJsonSerializerOptions = JsonObject.DefaultToJsonSerializerOptions;
        return this.ToJson(toJsonSerializerOptions, declaredType);
    }

    public string ToJson(JsonSerializerOptions toJsonSerializerOptions)
    {
        Contract.Requires(toJsonSerializerOptions != null);

        var objectType = this.GetType();
        return this.ToJson(toJsonSerializerOptions, objectType);
    }

    public string ToJson<T>(JsonSerializerOptions toJsonSerializerOptions)
    {
        Contract.Requires(toJsonSerializerOptions != null);

        var declaredType = typeof(T);
        return this.ToJson(toJsonSerializerOptions, declaredType);
    }

    public string ToJson(JsonSerializerOptions toJsonSerializerOptions, Type declaredType)
    {
        Contract.Requires(toJsonSerializerOptions != null);
        Contract.Requires(declaredType != null);

        var json = JsonSerializer.Serialize(this, declaredType, toJsonSerializerOptions);
        return json;
    }

    public Task<string> ToJsonAsync()
    {
        var toJsonSerializerOptions = JsonObject.DefaultToJsonSerializerOptions;
        var objectType = this.GetType();
        return this.ToJsonAsync(toJsonSerializerOptions, objectType);
    }

    public Task<string> ToJsonAsync<T>()
    {
        var toJsonSerializerOptions = JsonObject.DefaultToJsonSerializerOptions;
        var declaredType = typeof(T);
        return this.ToJsonAsync(toJsonSerializerOptions, declaredType);
    }

    public Task<string> ToJsonAsync(Type declaredType)
    {
        Contract.Requires(declaredType != null);

        var toJsonSerializerOptions = JsonObject.DefaultToJsonSerializerOptions;
        return this.ToJsonAsync(toJsonSerializerOptions, declaredType);
    }

    public Task<string> ToJsonAsync(JsonSerializerOptions toJsonSerializerOptions)
    {
        var objectType = this.GetType();
        return this.ToJsonAsync(toJsonSerializerOptions, objectType);
    }

    public Task<string> ToJsonAsync<T>(JsonSerializerOptions toJsonSerializerOptions)
    {
        Contract.Requires(toJsonSerializerOptions != null);

        var declaredType = typeof(T);
        return this.ToJsonAsync(toJsonSerializerOptions, declaredType);
    }

    public async Task<string> ToJsonAsync(JsonSerializerOptions toJsonSerializerOptions, Type declaredType)
    {
        Contract.Requires(toJsonSerializerOptions != null);
        Contract.Requires(declaredType != null);

        var json = await Task.Factory.StartNew(() => JsonSerializer.Serialize(this, declaredType, toJsonSerializerOptions));
        return json;
    }
    #endregion

    #region IDeepCloneable Implementation
    public virtual object DeepClone()
    { return this.DeepCloneWithJson(); }
    #endregion

    #region Parse Methods
    public static object Parse(string json, Type type)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(json) == false);
        Contract.Requires(type != null);

        var settings = JsonObject.DefaultToJsonSerializerOptions;
        return JsonObject.Parse(json, type, settings);
    }

    public static object Parse(string json, Type type, JsonSerializerOptions toJsonSerializerOptions)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(json) == false);
        Contract.Requires(type != null);
        Contract.Requires(toJsonSerializerOptions != null);

        var obj = JsonSerializer.Deserialize(json, type, toJsonSerializerOptions);
        return obj;
    }

    public static T Parse<T>(string json)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(json) == false);

        var settings = JsonObject.DefaultToJsonSerializerOptions;
        return JsonObject.Parse<T>(json, settings);
    }

    public static T Parse<T>(string json, JsonSerializerOptions toJsonSerializerOptions)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(json) == false);
        Contract.Requires(toJsonSerializerOptions != null);

        var typedObject = JsonSerializer.Deserialize<T>(json, toJsonSerializerOptions);
        return typedObject;
    }


    public static Task<object> ParseAsync(string json, Type type)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(json) == false);
        Contract.Requires(type != null);

        var settings = JsonObject.DefaultToJsonSerializerOptions;
        return JsonObject.ParseAsync(json, type, settings);
    }

    public static async Task<object> ParseAsync(string json, Type type, JsonSerializerOptions toJsonSerializerOptions)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(json) == false);
        Contract.Requires(type != null);
        Contract.Requires(toJsonSerializerOptions != null);

        var obj = await Task.Factory.StartNew(() => JsonSerializer.Deserialize(json, type, toJsonSerializerOptions));
        return obj;
    }

    public static Task<T> ParseAsync<T>(string json)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(json) == false);

        var settings = JsonObject.DefaultToJsonSerializerOptions;
        return JsonObject.ParseAsync<T>(json, settings);
    }

    public static async Task<T> ParseAsync<T>(string json, JsonSerializerOptions toJsonSerializerOptions)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(json) == false);
        Contract.Requires(toJsonSerializerOptions != null);

        var typedObject = await Task.Factory.StartNew(() => JsonSerializer.Deserialize<T>(json, toJsonSerializerOptions));
        return typedObject;
    }
    #endregion

    // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
    #region Constructors
    static JsonObject()
    {
        DefaultToJsonSerializerOptions = CreateDefaultToJsonSerializerOptions();
    }
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Methods
    private static JsonSerializerOptions CreateDefaultToJsonSerializerOptions()
    {
        var toJsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        return toJsonSerializerOptions;
    }
    #endregion
}