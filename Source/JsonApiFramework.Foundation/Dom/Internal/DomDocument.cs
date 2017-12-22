// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Tree;

namespace JsonApiFramework.Dom.Internal
{
    internal class DomDocument : DomObject
        , IDomDocument
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomDocument(ApiDocumentType apiDocumentType, params DomProperty[] domProperties)
            : this(apiDocumentType, domProperties.AsEnumerable())
        { }

        public DomDocument(ApiDocumentType apiDocumentType, IEnumerable<DomProperty> domProperties)
            : base("document object", domProperties)
        {
            this.ApiDocumentType = apiDocumentType;

            foreach (var domProperty in this.DomProperties())
            {
                var apiPropertyType = domProperty.ApiPropertyType;
                switch (apiPropertyType)
                {
                    case ApiPropertyType.JsonApi:
                        this.DomJsonApi = domProperty;
                        break;

                    case ApiPropertyType.Meta:
                        this.DomMeta = domProperty;
                        break;

                    case ApiPropertyType.Links:
                        this.DomLinks = domProperty;
                        break;

                    case ApiPropertyType.Data:
                        this.DomData = domProperty;
                        break;

                    case ApiPropertyType.Included:
                        this.DomIncluded = domProperty;
                        break;

                    case ApiPropertyType.Errors:
                        this.DomErrors = domProperty;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomDocument Implementation
        public ApiDocumentType ApiDocumentType
        {
            get => this.GetAttributeValue<ApiDocumentType>(ApiDocumentTypeAttributeName);
            private set => this.SetAttributeValue(ApiDocumentTypeAttributeName, value);
        }

        public IDomProperty DomJsonApi { get; }

        public IDomProperty DomMeta { get; }

        public IDomProperty DomLinks { get; }

        public IDomProperty DomData { get; }

        public IDomProperty DomIncluded { get; }

        public IDomProperty DomErrors { get; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string ApiDocumentTypeAttributeName = "document-type";
        #endregion
    }
}
