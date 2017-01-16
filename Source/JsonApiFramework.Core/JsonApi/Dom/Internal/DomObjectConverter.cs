// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>JSON.Net converter for IDomObject or DomObject nodes.</summary>
    internal class DomObjectConverter : DomNodeConverter
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomObjectConverter(DomJsonSerializerSettings domJsonSerializerSettings)
            : base(domJsonSerializerSettings)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region JsonConverter Overrides
        public override bool CanConvert(Type objectType)
        {
            Contract.Requires(objectType != null);

            var canConvert = objectType == typeof(IDomObject) || objectType == typeof(DomObject);
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

                case JsonToken.StartObject:
                    {
                        var jObject = JObject.Load(jsonReader);
                        var domObject = CreateDomObject(jObject);
                        return domObject;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(tokenType));
            }
        }
        #endregion
    }
}