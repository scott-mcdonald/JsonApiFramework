// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Dom.Internal;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.Dom.Internal
{
    /// <summary>JSON.Net converter for DomArray nodes.</summary>
    internal class DomArrayConverter : DomNodeConverter
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomArrayConverter(DomJsonSerializerSettings domJsonSerializerSettings)
            : base(domJsonSerializerSettings)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region JsonConverter Overrides
        public override bool CanConvert(Type objectType)
        {
            Contract.Requires(objectType != null);

            var canConvert = objectType == typeof(IDomArray) || objectType == typeof(DomArray);
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
                    // Convert JSON null into an empty array.
                    var domArray = new DomArray();
                    return domArray;
                }

                case JsonToken.StartArray:
                {
                    var jArray = JArray.Load(jsonReader);
                    var domArray = CreateDomArray(domDeserializationContext, jArray);
                    if (!domDeserializationContext.AnyErrors())
                        return domArray;
                }
                break;

                default:
                {
                    var jsonPointer = jsonReader.GetJsonPointer();
                    domDeserializationContext.AddJsonArrayError(jsonPointer);
                }
                break;
            }

            var errorsCollection = domDeserializationContext.ErrorsCollection;
            throw new JsonApiDeserializationException(HttpStatusCode.BadRequest, errorsCollection);
        }
        #endregion
    }
}