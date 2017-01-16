// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomResource : DomObject
        , IDomResource
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomResource(params DomProperty[] domProperties)
            : base("resource", domProperties.AsEnumerable())
        {
            if (domProperties == null)
                return;

            foreach (var domProperty in domProperties)
            {
                var apiPropertyType = domProperty.ApiPropertyType;
                switch (apiPropertyType)
                {
                    case ApiPropertyType.Meta:
                        this.DomMeta = domProperty;
                        break;

                    case ApiPropertyType.Type:
                        this.DomType = domProperty;
                        break;

                    case ApiPropertyType.Id:
                        this.DomId = domProperty;
                        break;

                    case ApiPropertyType.Attributes:
                        this.DomAttributes = domProperty;
                        break;

                    case ApiPropertyType.Relationships:
                        this.DomRelationships = domProperty;
                        break;

                    case ApiPropertyType.Links:
                        this.DomLinks = domProperty;
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

        public IDomProperty DomType { get; }

        public IDomProperty DomId { get; }

        public IDomProperty DomAttributes { get; }

        public IDomProperty DomRelationships { get; }

        public IDomProperty DomLinks { get; }
        #endregion
    }
}
