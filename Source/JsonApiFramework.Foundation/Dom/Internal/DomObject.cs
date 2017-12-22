// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi.Dom.Internal;

using Newtonsoft.Json;

namespace JsonApiFramework.Dom.Internal
{
    internal class DomObject : DomNode
        , IDomObject
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomObject(params DomProperty[] domProperties)
            : this("object", domProperties.AsEnumerable())
        { }

        public DomObject(IEnumerable<DomProperty> domProperties)
            : this("object", domProperties)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IDomObject Implementation
        public IEnumerable<IDomProperty> DomProperties()
        {
            return this.Nodes()
                       .Cast<IDomProperty>();
        }
        #endregion

        #region DomNode Overrides
        public override void WriteJson(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(jsonSerializer != null);
            Contract.Requires(domJsonSerializerSettings != null);

            jsonWriter.WriteStartObject();

            var domProperties = this.DomProperties();
            WriteDomProperties(jsonWriter, jsonSerializer, domJsonSerializerSettings, domProperties);

            jsonWriter.WriteEndObject();
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Constants
        public static readonly DomObject Empty = new DomObject();
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected DomObject(string name)
            : base(DomNodeType.Object, name)
        { }

        protected DomObject(string name, DomNode domNode)
            : base(DomNodeType.Object, name, domNode)
        { }

        protected DomObject(string name, IEnumerable<DomNode> domNodes)
            : base(DomNodeType.Object, name, domNodes)
        { }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Write Methods
        private static void WriteDomProperties(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings, IEnumerable<IDomProperty> domProperties)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(jsonSerializer != null);
            Contract.Requires(domJsonSerializerSettings != null);

            if (domProperties == null)
                return;

            foreach (var domProperty in domProperties)
            {
                WriteDomProperty(jsonWriter, jsonSerializer, domJsonSerializerSettings, domProperty);
            }
        }

        private static void WriteDomProperty(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings, IDomProperty domProperty)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(jsonSerializer != null);
            Contract.Requires(domJsonSerializerSettings != null);
            Contract.Requires(domProperty != null);

            var apiPropertyType = domProperty.ApiPropertyType;
            var apiPropertyName = domProperty.ApiPropertyName;
            var domPropertyValue = domProperty.DomPropertyValue();

            if (domPropertyValue == null)
            {
                var nullValueHandling = domJsonSerializerSettings.ResolveNullValueHandling(jsonSerializer, apiPropertyType);
                switch (nullValueHandling)
                {
                    case NullValueHandling.Include:
                        jsonWriter.WritePropertyName(apiPropertyName);
                        jsonWriter.WriteToken(JsonToken.Null);
                        return;

                    case NullValueHandling.Ignore:
                        // Ignore a null property.
                        return;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            jsonWriter.WritePropertyName(apiPropertyName);

            var domWriteable = (IDomWriteable)domPropertyValue;
            domWriteable.WriteJson(jsonWriter, jsonSerializer, domJsonSerializerSettings);
        }
        #endregion
    }
}
