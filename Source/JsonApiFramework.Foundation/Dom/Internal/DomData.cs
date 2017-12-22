// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.Dom.Internal
{
    internal class DomData : DomObject
        , IDomData
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomData(params DomProperty[] domProperties)
            : this(domProperties.AsEnumerable())
        { }

        public DomData(IEnumerable<DomProperty> domProperties)
            : base("data object", domProperties)
        {
            foreach (var domProperty in this.DomProperties())
            {
                var apiPropertyType = domProperty.ApiPropertyType;
                switch (apiPropertyType)
                {
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

                    case ApiPropertyType.Meta:
                        this.DomMeta = domProperty;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomData Implementation
        public IDomProperty DomType { get; }

        public IDomProperty DomId { get; }

        public IDomProperty DomAttributes { get; }

        public IDomProperty DomRelationships { get; }

        public IDomProperty DomLinks { get; }

        public IDomProperty DomMeta { get; }
        #endregion
    }
}
