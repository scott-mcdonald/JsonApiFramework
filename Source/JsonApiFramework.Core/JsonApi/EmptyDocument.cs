// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a special case json:api compliant document where the
    /// primary data is an empty resource or resource identifier arrary.
    /// </summary>
    public class EmptyDocument : Document
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return TypeName; }
        #endregion

        #region Document Overrides
        public override object GetData()
        { return Enumerable.Empty<object>(); }

        public override DocumentType GetDocumentType()
        { return DocumentType.EmptyDocument; }

        public override IEnumerable<Resource> GetResourceCollection()
        { return Enumerable.Empty<Resource>(); }

        public override IEnumerable<ResourceIdentifier> GetResourceIdentifierCollection()
        { return Enumerable.Empty<ResourceIdentifier>(); }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public new static readonly EmptyDocument Empty = new EmptyDocument();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(EmptyDocument).Name;
        #endregion
    }
}