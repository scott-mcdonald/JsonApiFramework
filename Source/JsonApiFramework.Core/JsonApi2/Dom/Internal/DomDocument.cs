// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Linq;

using JsonApiFramework.Tree;

namespace JsonApiFramework.JsonApi2.Dom.Internal
{
    internal class DomDocument : DomObject
        , IDomDocument
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomDocument(ApiDocumentType apiDocumentType, params DomProperty[] domProperties)
            : base("document", domProperties.AsEnumerable())
        {
            this.ApiDocumentType = apiDocumentType;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IDomDocument Implementation
        public ApiDocumentType ApiDocumentType
        {
            get { return this.GetAttributeValue<ApiDocumentType>(ApiDocumentTypeAttributeName); }
            private set { this.SetAttributeValue(ApiDocumentTypeAttributeName, value); }
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string ApiDocumentTypeAttributeName = "document-type";
        #endregion
    }
}
