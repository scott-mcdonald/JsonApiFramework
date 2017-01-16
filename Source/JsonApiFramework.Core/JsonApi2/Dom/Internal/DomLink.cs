// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi2.Dom.Internal
{
    internal class DomLink : DomObject
        , IDomLink
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomLink(params DomProperty[] domProperties)
            : this(domProperties.AsEnumerable())
        { }

        public DomLink(IEnumerable<DomProperty> domProperties)
            : base("object", domProperties)
        {
            foreach (var domProperty in this.DomProperties())
            {
                var apiPropertyType = domProperty.ApiPropertyType;
                switch (apiPropertyType)
                {
                    case ApiPropertyType.Meta:
                        this.DomMeta = domProperty;
                        break;

                    case ApiPropertyType.HRef:
                        this.DomHRef = domProperty;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomResource Implementation
        public IDomProperty DomMeta { get; }

        public IDomProperty DomHRef { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region DomNode Overrides
        public override void WriteJson(JsonWriter jsonWriter, JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonWriter != null);
            Contract.Requires(jsonSerializer != null);
            Contract.Requires(domJsonSerializerSettings != null);

            // Handle special case when HRef is writeable and Meta is not writeable to JSON.
            var isDomHRefWriteable = this.IsDomHRefWriteable();
            var isDomMetaNotWriteable = !this.IsDomMetaWriteable(jsonSerializer, domJsonSerializerSettings);
            if (isDomHRefWriteable && isDomMetaNotWriteable)
            {
                var domHRefValue = (DomNode)this.DomHRef.DomPropertyValue();
                domHRefValue.WriteJson(jsonWriter, jsonSerializer, domJsonSerializerSettings);
                return;
            }

            base.WriteJson(jsonWriter, jsonSerializer, domJsonSerializerSettings);
        }

        private bool IsDomHRefWriteable()
        {
            var isDomHRefWriteable = this.DomHRef != null && this.DomHRef.HasValue();
            return isDomHRefWriteable;
        }

        private bool IsDomMetaWriteable(JsonSerializer jsonSerializer, DomJsonSerializerSettings domJsonSerializerSettings)
        {
            Contract.Requires(jsonSerializer != null);
            Contract.Requires(domJsonSerializerSettings != null);

            if (this.DomMeta == null)
                return false;

            if (this.DomMeta.HasValue())
                return true;

            var domMetaNullValueHandling = domJsonSerializerSettings.ResolveNullValueHandling(jsonSerializer, ApiPropertyType.Meta);
            switch (domMetaNullValueHandling)
            {
                case NullValueHandling.Include:
                    return true;
                case NullValueHandling.Ignore:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
