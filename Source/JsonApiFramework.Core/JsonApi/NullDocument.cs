// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a special case json:api compliant document where the
    /// primary data is a single null object.
    /// </summary>
    public class NullDocument : Document
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return TypeName; }
        #endregion

        #region Document Overrides
        public override object GetData()
        { return null; }

        public override DocumentType GetDocumentType()
        { return DocumentType.NullDocument; }

        public override Resource GetResource()
        { return null; }

        public override ResourceIdentifier GetResourceIdentifier()
        { return null; }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public new static readonly NullDocument Empty = new NullDocument();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(NullDocument).Name;
        #endregion
    }
}