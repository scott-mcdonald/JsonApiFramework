// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

using JsonApiFramework.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>JSON.Net converter for DomValue{TValue} nodes.</summary>
    internal class DomValueConverter : DomNodeConverter
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomValueConverter(DomJsonSerializerSettings domJsonSerializerSettings)
            : base(domJsonSerializerSettings)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region JsonConverter Overrides
        public override bool CanConvert(Type objectType)
        {
            Contract.Requires(objectType != null);

            var canConvert = objectType == typeof(IDomValue) || TypeReflection.IsImplementationOf(objectType, typeof(IDomValue));
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

                case JsonToken.Boolean:
                case JsonToken.Bytes:
                case JsonToken.Date:
                case JsonToken.Float:
                case JsonToken.Integer:
                case JsonToken.String:
                    {
                        var jValue = (JValue)JToken.Load(jsonReader);
                        var domValue = CreateDomValue(domDeserializationContext, jValue);
                        if (!domDeserializationContext.AnyErrors())
                            return domValue;
                    }
                    break;

                default:
                    {
                        domDeserializationContext.AddJsonValueError(jsonReader.Path);
                    }
                    break;
            }

            var errorsCollection = domDeserializationContext.ErrorsCollection;
            throw new JsonApiDeserializationException(HttpStatusCode.BadRequest, errorsCollection);
        }
        #endregion
    }
}