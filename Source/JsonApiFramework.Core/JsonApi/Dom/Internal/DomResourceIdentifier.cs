// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomResourceIdentifier : DomObject
        , IDomResourceIdentifier
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomResourceIdentifier(params DomProperty[] domProperties)
            : this(domProperties.AsEnumerable())
        { }

        public DomResourceIdentifier(IEnumerable<DomProperty> domProperties)
            : base("resource identifier object", domProperties)
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
        #region IDomResourceIdentifier Implementation
        public IDomProperty DomType { get; }

        public IDomProperty DomId { get; }

        public IDomProperty DomMeta { get; }
        #endregion
    }
}
