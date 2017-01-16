// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>JSON.Net converter for IDomLink or DomLink nodes.</summary>
    internal class DomLinkConverter : DomNodeConverter
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomLinkConverter(DomJsonSerializerSettings domJsonSerializerSettings)
            : base(domJsonSerializerSettings)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region JsonConverter Overrides
        public override bool CanConvert(Type objectType)
        {
            Contract.Requires(objectType != null);

            var canConvert = objectType == typeof(IDomLink) || objectType == typeof(DomLink);
            return canConvert;
        }

        public override object ReadJson(JsonReader jsonReader, Type objectType, object existingValue, JsonSerializer jsonSerializer)
        {
            Contract.Requires(jsonReader != null);
            Contract.Requires(objectType != null);
            Contract.Requires(jsonSerializer != null);

            var tokenType = jsonReader.TokenType;
            switch (tokenType)
            {
                case JsonToken.Null:
                    {
                        return null;
                    }

                case JsonToken.String:
                    {
                        var jValue = (JValue)JToken.Load(jsonReader);
                        var domLink = CreateDomLink(jValue);
                        return domLink;
                    }

                case JsonToken.StartObject:
                    {
                        var jObject = JObject.Load(jsonReader);
                        var domLink = CreateDomLink(jObject);
                        return domLink;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(tokenType));
            }
        }
        #endregion
    }
}