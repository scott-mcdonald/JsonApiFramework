// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi.Internal;
using JsonApiFramework.ServiceModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    public class DomDocumentConverter : Converter<IDomDocument>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomDocumentConverter(IServiceModel serviceModel)
            : base(serviceModel)
        { }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Converter Overrides
        protected override IDomDocument ReadTypedObject(JObject documentJObject, JsonSerializer serializer)
        {
            Contract.Requires(documentJObject != null);
            Contract.Requires(serializer != null);

            var domDocument = this.CreateDomDocument();

            ReadJsonApiVersion(documentJObject, serializer, domDocument);
            ReadMeta(documentJObject, serializer, domDocument);
            ReadLinks(documentJObject, serializer, domDocument);

            return domDocument;
        }

        protected override void WriteTypedObject(JsonWriter writer, JsonSerializer serializer, IDomDocument domDocument)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(domDocument != null);

            writer.WriteStartObject();

            WriteJsonApiVersion(writer, serializer, domDocument);
            WriteMeta(writer, serializer, domDocument);
            WriteLinks(writer, serializer, domDocument);
            //WriteData(writer, serializer, document);
            //WriteErrors(writer, serializer, document);
            //WriteIncluded(writer, serializer, document);

            writer.WriteEndObject();
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Read Methods
        private DomDocument CreateDomDocument()
        {
            var serviceModel = this.ServiceModel;
            return new DomDocument(serviceModel);
        }

        private static void ReadJsonApiVersion(JToken documentJToken, JsonSerializer serializer, ISetJsonApiVersion domDocument)
        {
            Contract.Requires(documentJToken != null);
            Contract.Requires(serializer != null);
            Contract.Requires(domDocument != null);

            var jsonApiJToken = documentJToken.SelectToken(Keywords.JsonApi);
            if (jsonApiJToken == null)
                return;

            var jsonApiJTokenType = jsonApiJToken.Type;
            switch (jsonApiJTokenType)
            {
                case JTokenType.None:
                case JTokenType.Null:
                    return;

                case JTokenType.Object:
                    {
                        var jsonApiVersion = jsonApiJToken.ToObject<JsonApiVersion>(serializer);
                        domDocument.SetJsonApiVersion(jsonApiVersion);
                    }
                    return;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region Write Methods
        private static void WriteJsonApiVersion(JsonWriter writer, JsonSerializer serializer, IGetJsonApiVersion domDocument)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(domDocument != null);

            var jsonApiVersion = domDocument.GetJsonApiVersion();
            if (jsonApiVersion == null)
            {
                WriteNull(writer, serializer, Keywords.JsonApi);
                return;
            }

            writer.WritePropertyName(Keywords.JsonApi);
            var jsonApiJToken = JToken.FromObject(jsonApiVersion, serializer);
            jsonApiJToken.WriteTo(writer);
        }
        #endregion
    }
}