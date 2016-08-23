// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace JsonApiFramework.Json
{
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

        string ToJson(JsonSerializerSettings serializerSettings);

        string ToJson<T>(JsonSerializerSettings serializerSettings);

        string ToJson(JsonSerializerSettings serializerSettings, Type declaredType);

        Task<string> ToJsonAsync();

        Task<string> ToJsonAsync<T>();

        Task<string> ToJsonAsync(Type declaredType);

        Task<string> ToJsonAsync(JsonSerializerSettings serializerSettings);

        Task<string> ToJsonAsync<T>(JsonSerializerSettings serializerSettings);

        Task<string> ToJsonAsync(JsonSerializerSettings serializerSettings, Type declaredType);
        #endregion
    }
}
