// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomError : DomObject
        , IDomError
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomError(params DomProperty[] domProperties)
            : this(domProperties.AsEnumerable())
        { }

        public DomError(IEnumerable<DomProperty> domProperties)
            : base("error object", domProperties)
        {
            foreach (var domProperty in this.DomProperties())
            {
                var apiPropertyType = domProperty.ApiPropertyType;
                switch (apiPropertyType)
                {
                    case PropertyType.Id:
                        this.DomId = domProperty;
                        break;

                    case PropertyType.Links:
                        this.DomLinks = domProperty;
                        break;

                    case PropertyType.Status:
                        this.DomStatus = domProperty;
                        break;

                    case PropertyType.Code:
                        this.DomCode = domProperty;
                        break;

                    case PropertyType.Title:
                        this.DomTitle = domProperty;
                        break;

                    case PropertyType.Detail:
                        this.DomDetail = domProperty;
                        break;

                    case PropertyType.Source:
                        this.DomSource = domProperty;
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
        #region IDomJsonApi Implementation
        public IDomProperty DomId { get; }
        public IDomProperty DomLinks { get; }
        public IDomProperty DomStatus { get; }
        public IDomProperty DomCode { get; }
        public IDomProperty DomTitle { get; }
        public IDomProperty DomDetail { get; }
        public IDomProperty DomSource { get; }
        public IDomProperty DomMeta { get; }
        #endregion
    }
}
