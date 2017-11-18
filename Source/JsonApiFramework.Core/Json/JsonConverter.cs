// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.Json
{
    public abstract class JsonConverter<T> : JsonConverter
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

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Contract.Requires(reader != null);
            Contract.Requires(objectType != null);
            Contract.Requires(serializer != null);

            switch (reader.TokenType)
            {
                case JsonToken.None:
                case JsonToken.Null:
                return null;

                case JsonToken.String:
                {
                    var stringValue = (string)reader.Value;
                    var clrObject = this.DeserializeJsonString(stringValue);
                    return clrObject;
                }

                case JsonToken.StartObject:
                {
                    var jObject = JObject.Load(reader);
                    var clrObject = this.DeserializeJsonObject(jObject, serializer);
                    return clrObject;
                }

                default:
                throw new ArgumentOutOfRangeException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            var clrObject = (T)value;
            this.SerializeClrObject(writer, serializer, clrObject);
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter<T> Overrides
        protected virtual T DeserializeJsonString(string stringValue)
        { throw new NotImplementedException(); }

        protected abstract T DeserializeJsonObject(JObject jObject, JsonSerializer serializer);

        protected abstract void SerializeClrObject(JsonWriter writer, JsonSerializer serializer, T clrObject);
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly TypeInfo TypeInfo = typeof(T).GetTypeInfo();
        #endregion
    }
}