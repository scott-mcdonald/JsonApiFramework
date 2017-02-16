// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

using JsonApiFramework.Properties;
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

            var domReadJsonContext = new DomReadJsonContext();
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
                        var domValue = CreateDomValue(domReadJsonContext, jValue);
                        if (!domReadJsonContext.AnyErrors())
                            return domValue;
                    }
                    break;

                default:
                    {
                        var title = CoreErrorStrings.JsonReadErrorTitle;
                        var detail = "Expected JSON null, JSON boolean, JSON number, or JSON string when reading JSON representing a JSON value.";
                        var source = ErrorSource.CreatePointer(jsonReader.Path);
                        var error = new Error(null, null, HttpStatusCode.BadRequest, null, title, detail, source, null);
                        domReadJsonContext.AddError(error);
                    }
                    break;
            }

            var errorsCollection = domReadJsonContext.ErrorsCollection;
            throw new ErrorsException(HttpStatusCode.BadRequest, errorsCollection);
        }
        #endregion
    }
}