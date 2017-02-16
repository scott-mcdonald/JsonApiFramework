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
    /// <summary>JSON.Net converter for DomRelationship nodes.</summary>
    internal class DomRelationshipConverter : DomNodeConverter
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomRelationshipConverter(DomJsonSerializerSettings domJsonSerializerSettings)
            : base(domJsonSerializerSettings)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region JsonConverter Overrides
        public override bool CanConvert(Type objectType)
        {
            Contract.Requires(objectType != null);

            var canConvert = objectType == typeof(IDomRelationship) || objectType == typeof(DomRelationship);
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

                case JsonToken.StartObject:
                    {
                        var jObject = JObject.Load(jsonReader);
                        var domRelationship = CreateDomRelationship(domReadJsonContext, jObject);
                        if (!domReadJsonContext.AnyErrors())
                            return domRelationship;
                    }
                    break;

                default:
                    {
                        var title = CoreErrorStrings.JsonReadErrorTitle;
                        var detail = "Expected JSON null or JSON object when reading JSON representing a json:api relationship object.";
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