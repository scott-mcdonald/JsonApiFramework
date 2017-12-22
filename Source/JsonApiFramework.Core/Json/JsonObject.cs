// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Infrastructure;

using Newtonsoft.Json;

namespace JsonApiFramework.Json
{
    /// <summary>
    /// Represent a base class for any class that can serialize and
    /// deserialize itself into or from a JSON string representation.
    /// </summary>
    public abstract class JsonObject : IJsonObject, IDeepCloneable
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IJsonObject Implementation
        public string ToJson()
        {
            var settings = DefaultJsonSerializerSettings;
            var type = this.GetType();
            return ToJson(this, type, settings);
        }

        public string ToJson(JsonSerializerSettings settings)
        {
            Contract.Requires(settings != null);

            var type = this.GetType();
            return ToJson(this, type, settings);
        }
        #endregion

        #region IDeepCloneable Implementation
        public virtual object DeepClone()
        { return this.DeepCloneWithJson(); }
        #endregion

        #region Object Overrides
        public override string ToString()
        { return this.ToJson(); }
        #endregion

        #region Parse Methods
        public static T Parse<T>(string json)
        {
            if (json == null)
                return default(T);

            var settings = DefaultJsonSerializerSettings;
            return Parse<T>(json, settings);
        }

        /// <summary>
        /// Parse a JSON representation and return newly created object using
        /// default serializer settings.
        /// </summary>
        public static T Parse<T>(string json, JsonSerializerSettings settings)
        {
            Contract.Requires(settings != null);

            if (json == null)
                return default(T);

            var instance = JsonConvert.DeserializeObject<T>(json, settings);
            return instance;
        }
        #endregion

        #region ToJson Methods
        public static string ToJson<T>(T value)
        {
            var settings = DefaultJsonSerializerSettings;
            var valueType = typeof(T);
            return ToJson(value, valueType, settings);
        }

        public static string ToJson<T>(T value, JsonSerializerSettings settings)
        {
            var valueType = typeof(T);
            return ToJson(value, valueType, settings);
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        static JsonObject()
        {
            DefaultJsonSerializerSettings = CreateDefaultJsonSerializerSettings();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private static JsonSerializerSettings DefaultJsonSerializerSettings { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static JsonSerializerSettings CreateDefaultJsonSerializerSettings()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Include
                };
            return jsonSerializerSettings;
        }

        private static string ToJson(object obj, Type objType, JsonSerializerSettings settings)
        {
            Contract.Requires(objType != null);
            Contract.Requires(settings != null);

            var json = JsonConvert.SerializeObject(obj, objType, settings);
            return json;
        }
        #endregion
    }
}