// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomResource : DomObject
        , IDomResource
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomResource(params DomProperty[] domProperties)
            : this(domProperties.AsEnumerable())
        { }

        public DomResource(IEnumerable<DomProperty> domProperties)
            : base("resource object", domProperties)
        {
            foreach (var domProperty in this.DomProperties())
            {
                var apiPropertyType = domProperty.ApiPropertyType;
                switch (apiPropertyType)
                {
                    case PropertyType.Type:
                        this.DomType = domProperty;
                        break;

                    case PropertyType.Id:
                        this.DomId = domProperty;
                        break;

                    case PropertyType.Attributes:
                        this.DomAttributes = domProperty;
                        break;

                    case PropertyType.Relationships:
                        this.DomRelationships = domProperty;
                        break;

                    case PropertyType.Links:
                        this.DomLinks = domProperty;
                        break;

                    case PropertyType.Meta:
                        this.DomMeta = domProperty;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomResource Implementation
        public IDomProperty DomType { get; }

        public IDomProperty DomId { get; }

        public IDomProperty DomAttributes { get; }

        public IDomProperty DomRelationships { get; }

        public IDomProperty DomLinks { get; }

        public IDomProperty DomMeta { get; }
        #endregion
    }
}
