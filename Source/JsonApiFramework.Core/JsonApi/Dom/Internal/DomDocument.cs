// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Tree;

namespace JsonApiFramework.JsonApi.Dom.Internal
{
    internal class DomDocument : DomObject
        , IDomDocument
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomDocument(DocumentType apiDocumentType, params DomProperty[] domProperties)
            : this(apiDocumentType, domProperties.AsEnumerable())
        { }

        public DomDocument(DocumentType apiDocumentType, IEnumerable<DomProperty> domProperties)
            : base("document", domProperties)
        {
            this.ApiDocumentType = apiDocumentType;

            foreach (var domProperty in this.DomProperties())
            {
                var apiPropertyType = domProperty.ApiPropertyType;
                switch (apiPropertyType)
                {
                    case PropertyType.JsonApi:
                        this.DomJsonApiVersion = domProperty;
                        break;

                    case PropertyType.Meta:
                        this.DomMeta = domProperty;
                        break;

                    case PropertyType.Links:
                        this.DomLinks = domProperty;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomDocument Implementation
        public DocumentType ApiDocumentType
        {
            get { return this.GetAttributeValue<DocumentType>(ApiDocumentTypeAttributeName); }
            private set { this.SetAttributeValue(ApiDocumentTypeAttributeName, value); }
        }

        public IDomProperty DomJsonApiVersion { get; }

        public IDomProperty DomMeta { get; }

        public IDomProperty DomLinks { get; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string ApiDocumentTypeAttributeName = "document-type";
        #endregion
    }
}
