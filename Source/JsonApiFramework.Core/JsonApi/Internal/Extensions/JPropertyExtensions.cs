// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.JsonApi.Internal
{
    /// <summary>Extension methods specific for json:api for the JSON.NET JProperty class.</summary>
    internal static class JPropertyExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Returns the json:api standard for property names being all lower-case for consistent string compares.</summary>
        public static string GetApiPropertyName(this JProperty jProperty)
        {
            Contract.Requires(jProperty != null);

            var apiPropertyName = jProperty.Name.ToLowerInvariant();
            return apiPropertyName;
        }

        public static ErrorSource ReadErrorSourcePropertyValue(this JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(ErrorSource);

            var jObject = (JObject)jToken;
            var clrErrorSource = jObject.ReadErrorSourceObject(serializer);
            return clrErrorSource;
        }

        public static Link ReadLinkPropertyValue(this JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            switch (jTokenType)
            {
                case JTokenType.Object:
                {
                    var jObject = (JObject)jToken;
                    return jObject.ReadLinkObject(serializer);
                }

                case JTokenType.String:
                {
                    var jValue = (JValue)jToken;
                    var hRef = (string)jValue;
                    return new Link(hRef);
                }

                default:
                {
                    return default(Link);
                }
            }
        }

        public static Links ReadLinksPropertyValue(this JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(Links);

            var jObject = (JObject)jToken;
            var clrLinks = jObject.ReadLinksObject(serializer);
            return clrLinks;
        }

        public static Meta ReadMetaPropertyValue(this JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(Meta);

            var jObject = (JObject)jToken;
            var clrMeta = jObject.ReadMetaObject(serializer);
            return clrMeta;
        }

        public static Relationship ReadRelationshipPropertyValue(this JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(Relationship);

            var jObject = (JObject)jToken;
            var clrRelationship = jObject.ReadRelationshipObject(serializer);
            return clrRelationship;
        }

        public static HttpStatusCode? ReadStatusPropertyValue(this JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.String)
                return default(HttpStatusCode?);

            var jValue = (JValue)jToken;
            var status = jValue.ReadStatusValue(serializer);
            return status;
        }
        #endregion
    }
}
