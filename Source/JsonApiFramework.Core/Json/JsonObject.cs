// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

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
            var settings = JsonObject.DefaultJsonSerializerSettings;
            var type = this.GetType();
            return this.ToJson(settings, type);
        }

        public string ToJson(JsonSerializerSettings settings)
        {
            Contract.Requires(settings != null);

            var type = this.GetType();
            return this.ToJson(settings, type);
        }
        #endregion

        #region IDeepCloneable Implementation
        public virtual object DeepClone()
        { return this.DeepCloneWithJson(); }
        #endregion

        #region Parse Methods
        public static T Parse<T>(string json)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);

            var settings = JsonObject.DefaultJsonSerializerSettings;
            return JsonObject.Parse<T>(json, settings);
        }

        /// <summary>
        /// Parse a JSON representation and return newly created object using
        /// default serializer settings.
        /// </summary>
        public static T Parse<T>(string json, JsonSerializerSettings settings)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(json) == false);
            Contract.Requires(settings != null);

            var instance = JsonConvert.DeserializeObject<T>(json, settings);
            return instance;
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
        private static JsonSerializerSettings DefaultJsonSerializerSettings
        { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static JsonSerializerSettings CreateDefaultJsonSerializerSettings()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            return jsonSerializerSettings;
        }

        private string ToJson(JsonSerializerSettings settings, Type type)
        {
            Contract.Requires(settings != null);
            Contract.Requires(type != null);

            var json = JsonConvert.SerializeObject(this, type, settings);
            return json;
        }
        #endregion
    }
}