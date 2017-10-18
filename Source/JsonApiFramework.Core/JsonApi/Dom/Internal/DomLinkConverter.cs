// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>JSON.Net converter for DomLink nodes.</summary>
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

            var domDeserializationContext = new DomDeserializationContext();
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
                    var domLink = CreateDomLink(domDeserializationContext, jValue);
                    if (!domDeserializationContext.AnyErrors())
                        return domLink;
                }
                break;

                case JsonToken.StartObject:
                {
                    var jObject = JObject.Load(jsonReader);
                    var domLink = CreateDomLink(domDeserializationContext, jObject);
                    if (!domDeserializationContext.AnyErrors())
                        return domLink;
                }
                break;

                default:
                {
                    domDeserializationContext.AddJsonApiLinkError(jsonReader.Path);
                }
                break;
            }

            var errorsCollection = domDeserializationContext.ErrorsCollection;
            throw new JsonApiDeserializationException(HttpStatusCode.BadRequest, errorsCollection);
        }
        #endregion
    }
}