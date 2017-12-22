// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.Dom.Internal
{
    internal class DomJsonApi : DomObject
        , IDomJsonApi
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomJsonApi(params DomProperty[] domProperties)
            : this(domProperties.AsEnumerable())
        { }

        public DomJsonApi(IEnumerable<DomProperty> domProperties)
            : base("jsonapi object", domProperties)
        {
            foreach (var domProperty in this.DomProperties())
            {
                var apiPropertyType = domProperty.ApiPropertyType;
                switch (apiPropertyType)
                {
                    case ApiPropertyType.Version:
                        this.DomVersion = domProperty;
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
        #region IDomJsonApi Implementation
        public IDomProperty DomVersion { get; }

        public IDomProperty DomMeta { get; }
        #endregion
    }
}
