// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;

namespace JsonApiFramework.Json;

/// <summary>
/// Abstracts any object that can create JSON from itself.
/// </summary>
public interface IJsonObject
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    string ToJson();

    string ToJson<T>();

    string ToJson(Type declaredType);

    string ToJson(JsonSerializerOptions serializerOptions);

    string ToJson<T>(JsonSerializerOptions serializerOptions);

    string ToJson(JsonSerializerOptions serializerOptions, Type declaredType);

    Task<string> ToJsonAsync();

    Task<string> ToJsonAsync<T>();

    Task<string> ToJsonAsync(Type declaredType);

    Task<string> ToJsonAsync(JsonSerializerOptions serializerOptions);

    Task<string> ToJsonAsync<T>(JsonSerializerOptions serializerOptions);

    Task<string> ToJsonAsync(JsonSerializerOptions serializerOptions, Type declaredType);
    #endregion
}
