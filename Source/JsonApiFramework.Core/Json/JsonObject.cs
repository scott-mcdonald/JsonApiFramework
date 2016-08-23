// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace JsonApiFramework.Json
{
    /// <summary>
    /// Base class for any object that wants to be serialized/deserialized
    /// into/from JSON respectively.
    /// </summary>
    public abstract class JsonObject : IJsonObject, IDeepCloneable
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public static JsonSerializerSettings DefaultToJsonSerializerSettings
        { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IJsonObject Implementation
        public string ToJson()
        {
            var toJsonSerializerSettings = JsonObject.DefaultToJsonSerializerSettings;
            var objectType = this.GetType();
            return this.ToJson(toJsonSerializerSettings, objectType);
        }

        public string ToJson<T>()
        {
            var toJsonSerializerSettings = JsonObject.DefaultToJsonSerializerSettings;
            var declaredType = typeof(T);
            return this.ToJson(toJsonSerializerSettings, declaredType);
        }

        public string ToJson(Type declaredType)
        {
            Contract.Requires(declaredType != null);

            var toJsonSerializerSettings = JsonObject.DefaultToJsonSerializerSettings;
            return this.ToJson(toJsonSerializerSettings, declaredType);
        }

        public string ToJson(JsonSerializerSettings toJsonSerializerSettings)
        {
            Contract.Requires(toJsonSerializerSettings != null);

            var objectType = this.GetType();
            return this.ToJson(toJsonSerializerSettings, objectType);
        }

        public string ToJson<T>(JsonSerializerSettings toJsonSerializerSettings)
        {
            Contract.Requires(toJsonSerializerSettings != null);

            var declaredType = typeof(T);
            return this.ToJson(toJsonSerializerSettings, declaredType);
        }

        public string ToJson(JsonSerializerSettings toJsonSerializerSettings, Type declaredType)
        {
            Contract.Requires(toJsonSerializerSettings != null);
            Contract.Requires(declaredType != null);

            var json = JsonConvert.SerializeObject(this, declaredType, toJsonSerializerSettings);
            return json;
        }

        public Task<string> ToJsonAsync()
        {
            var toJsonSerializerSettings = JsonObject.DefaultToJsonSerializerSettings;
            var objectType = this.GetType();
            return this.ToJsonAsync(toJsonSerializerSettings, objectType);
        }

        public Task<string> ToJsonAsync<T>()
        {
            var toJsonSerializerSettings = JsonObject.DefaultToJsonSerializerSettings;
            var declaredType = typeof(T);
            return this.ToJsonAsync(toJsonSerializerSettings, declaredType);
        }

        public Task<string> ToJsonAsync(Type declaredType)
        {
            Contract.Requires(declaredType != null);

            var toJsonSerializerSettings = JsonObject.DefaultToJsonSerializerSettings;
            return this.ToJsonAsync(toJsonSerializerSettings, declaredType);
        }

        public Task<string> ToJsonAsync(JsonSerializerSettings toJsonSerializerSettings)
        {
            var objectType = this.GetType();
            return this.ToJsonAsync(toJsonSerializerSettings, objectType);
        }

        public Task<string> ToJsonAsync<T>(JsonSerializerSettings toJsonSerializerSettings)
        {
            Contract.Requires(toJsonSerializerSettings != null);

            var declaredType = typeof(T);
            return this.ToJsonAsync(toJsonSerializerSettings, declaredType);
        }

        public async Task<string> ToJsonAsync(JsonSerializerSettings toJsonSerializerSettings, Type declaredType)
        {
            Contract.Requires(toJsonSerializerSettings != null);
            Contract.Requires(declaredType != null);

            var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(this, declaredType, toJsonSerializerSettings));
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
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);
            Contract.Requires(type != null);

            var settings = JsonObject.DefaultToJsonSerializerSettings;
            return JsonObject.Parse(json, type, settings);
        }

        public static object Parse(string json, Type type, JsonSerializerSettings toJsonSerializerSettings)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);
            Contract.Requires(type != null);
            Contract.Requires(toJsonSerializerSettings != null);

            var obj = JsonConvert.DeserializeObject(json, type, toJsonSerializerSettings);
            return obj;
        }

        public static T Parse<T>(string json)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);

            var settings = JsonObject.DefaultToJsonSerializerSettings;
            return JsonObject.Parse<T>(json, settings);
        }

        public static T Parse<T>(string json, JsonSerializerSettings toJsonSerializerSettings)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);
            Contract.Requires(toJsonSerializerSettings != null);

            var typedObject = JsonConvert.DeserializeObject<T>(json, toJsonSerializerSettings);
            return typedObject;
        }


        public static Task<object> ParseAsync(string json, Type type)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);
            Contract.Requires(type != null);

            var settings = JsonObject.DefaultToJsonSerializerSettings;
            return JsonObject.ParseAsync(json, type, settings);
        }

        public static async Task<object> ParseAsync(string json, Type type, JsonSerializerSettings toJsonSerializerSettings)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);
            Contract.Requires(type != null);
            Contract.Requires(toJsonSerializerSettings != null);

            var obj = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject(json, type, toJsonSerializerSettings));
            return obj;
        }

        public static Task<T> ParseAsync<T>(string json)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);

            var settings = JsonObject.DefaultToJsonSerializerSettings;
            return JsonObject.ParseAsync<T>(json, settings);
        }

        public static async Task<T> ParseAsync<T>(string json, JsonSerializerSettings toJsonSerializerSettings)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);
            Contract.Requires(toJsonSerializerSettings != null);

            var typedObject = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json, toJsonSerializerSettings));
            return typedObject;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        static JsonObject()
        {
            DefaultToJsonSerializerSettings = CreateDefaultToJsonSerializerSettings();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static JsonSerializerSettings CreateDefaultToJsonSerializerSettings()
        {
            var toJsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            return toJsonSerializerSettings;
        }
        #endregion
    }
}