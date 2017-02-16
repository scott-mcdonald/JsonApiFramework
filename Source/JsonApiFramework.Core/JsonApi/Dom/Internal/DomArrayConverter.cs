// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Net;

using JsonApiFramework.Properties;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
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

            var domReadJsonContext = new DomReadJsonContext();
            var tokenType = jsonReader.TokenType;
            switch (tokenType)
            {
                case JsonToken.Null:
                    {
                        return null;
                    }

                case JsonToken.StartArray:
                    {
                        var jArray = JArray.Load(jsonReader);
                        var domArray = CreateDomArray(domReadJsonContext, jArray);
                        if (!domReadJsonContext.AnyErrors())
                            return domArray;
                    }
                    break;

                default:
                    {
                        var title = CoreErrorStrings.JsonReadErrorTitle;
                        var detail = "Expected JSON null or JSON array when reading JSON representing a JSON array.";
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