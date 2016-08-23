// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Baseclass that encapsulates boilerplate JSON.Net converter code.
    /// </summary>
    public abstract class Converter<T> : JsonConverter
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
                        var typedObject = this.ReadString(stringValue);
                        return typedObject;
                    }

                case JsonToken.StartObject:
                    {
                        var jObject = JObject.Load(reader);
                        var typedObject = this.ReadObject(jObject, serializer);
                        return typedObject;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);

            if (value == null)
                return;

            var typedObject = (T)value;
            this.WriteObject(writer, serializer, typedObject);
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected virtual T ReadString(string stringValue)
        { throw new NotImplementedException(); }

        protected abstract T ReadObject(JObject jObject, JsonSerializer serializer);
        protected abstract void WriteObject(JsonWriter writer, JsonSerializer serializer, T typedObject);
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Read Methods
        protected static void ReadAttributes(JToken jParentToken, JsonSerializer serializer, ISetAttributes setAttributes)
        {
            Contract.Requires(jParentToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(setAttributes != null);

            var attributesJToken = jParentToken.SelectToken(Keywords.Attributes);
            if (attributesJToken == null)
                return;

            var attributesJObject = (JObject)attributesJToken;
            setAttributes.Attributes = attributesJObject;
        }

        protected static void ReadId(JToken jParentToken, JsonSerializer serializer, ISetResourceIdentity setResourceIdentity)
        {
            Contract.Requires(jParentToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(setResourceIdentity != null);

            var id = ReadString(jParentToken, Keywords.Id);
            setResourceIdentity.Id = id;
        }

        protected static void ReadMeta(JToken jParentToken, JsonSerializer serializer, ISetMeta setMeta)
        {
            Contract.Requires(jParentToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(setMeta != null);

            var metaJToken = jParentToken.SelectToken(Keywords.Meta);
            if (metaJToken == null)
                return;

            var metaJObject = (JObject)metaJToken;
            var meta = (Meta)metaJObject;
            setMeta.Meta = meta;
        }

        protected static void ReadLinks(JToken jParentToken, JsonSerializer serializer, ISetLinks setLinks)
        {
            Contract.Requires(jParentToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(setLinks != null);

            var linksJToken = jParentToken.SelectToken(Keywords.Links);
            if (linksJToken == null)
                return;

            var links = linksJToken.ToObject<Links>(serializer);
            setLinks.Links = links;
        }

        protected static void ReadRelationships(JToken jParentToken, JsonSerializer serializer, ISetRelationships setRelationships)
        {
            Contract.Requires(jParentToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(setRelationships != null);

            var relationshipsJToken = jParentToken.SelectToken(Keywords.Relationships);
            if (relationshipsJToken == null)
                return;

            var relationships = relationshipsJToken.ToObject<Relationships>(serializer);
            setRelationships.Relationships = relationships;
        }

        protected static string ReadString(JToken jParentToken, string childPath)
        {
            Contract.Requires(jParentToken != null);
            Contract.Requires(String.IsNullOrWhiteSpace(childPath) == false);

            var childJToken = jParentToken.SelectToken(childPath);
            if (childJToken == null)
                return null;

            var childJTokenType = childJToken.Type;
            switch (childJTokenType)
            {
                case JTokenType.None:
                case JTokenType.Null:
                    return null;
                case JTokenType.String:
                    var childString = (string)childJToken;
                    return childString;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected static void ReadType(JToken jParentToken, JsonSerializer serializer, ISetResourceIdentity setResourceIdentity)
        {
            Contract.Requires(jParentToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(setResourceIdentity != null);

            var type = ReadString(jParentToken, Keywords.Type);
            setResourceIdentity.Type = type;
        }
        #endregion

        #region Write Methods
        protected static void WriteAttributes(JsonWriter writer, JsonSerializer serializer, IGetAttributes getAttributes)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(getAttributes != null);

            if (getAttributes.Attributes == null)
                return;

            writer.WritePropertyName(Keywords.Attributes);
            getAttributes.Attributes.WriteTo(writer);
        }

        protected static void WriteId(JsonWriter writer, JsonSerializer serializer, IGetResourceIdentity getResourceIdentity)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(getResourceIdentity != null);

            WriteString(writer, Keywords.Id, getResourceIdentity.Id);
        }

        protected static void WriteMeta(JsonWriter writer, JsonSerializer serializer, IGetMeta getMeta)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(getMeta != null);

            if (getMeta.Meta == null)
                return;

            var metaJObject = (JObject)getMeta.Meta;

            writer.WritePropertyName(Keywords.Meta);
            metaJObject.WriteTo(writer);
        }

        protected static void WriteLinks(JsonWriter writer, JsonSerializer serializer, IGetLinks getLinks)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(getLinks != null);

            if (getLinks.Links == null || getLinks.Links.Any() == false)
                return;

            writer.WritePropertyName(Keywords.Links);
            var linksJToken = JToken.FromObject(getLinks.Links, serializer);
            var linksJObject = (JObject)linksJToken;
            linksJObject.WriteTo(writer);
        }

        protected static void WriteRelationships(JsonWriter writer, JsonSerializer serializer, IGetRelationships getRelationships)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(getRelationships != null);

            if (getRelationships.Relationships == null || getRelationships.Relationships.Any() == false)
                return;

            writer.WritePropertyName(Keywords.Relationships);
            var relationshipsJToken = JToken.FromObject(getRelationships.Relationships, serializer);
            var relationshipsJObject = (JObject)relationshipsJToken;
            relationshipsJObject.WriteTo(writer);
        }

        protected static void WriteString(JsonWriter writer, string childPath, string childValue)
        {
            Contract.Requires(writer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(childPath) == false);

            if (childValue == null)
                return;

            writer.WritePropertyName(childPath);
            writer.WriteValue(childValue);
        }

        protected static void WriteType(JsonWriter writer, JsonSerializer serializer, IGetResourceIdentity getResourceIdentity)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(getResourceIdentity != null);

            WriteString(writer, Keywords.Type, getResourceIdentity.Type);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly TypeInfo TypeInfo = typeof(T).GetTypeInfo();
        #endregion
    }
}