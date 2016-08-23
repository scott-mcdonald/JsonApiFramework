// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a json:api compliant document where the primary data is a
    /// single resource identifier object.
    /// </summary>
    /// <remarks>
    /// Data should be <b>null</b> if no resource identifier object is in the
    /// document.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceIdentifierDocument : Document
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        /// <summary>Primary data as a single resource identifier.</summary>
        [JsonProperty(Keywords.Data)] public ResourceIdentifier Data { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return TypeName; }
        #endregion

        #region Document Overrides
        public override object GetData()
        { return this.Data; }

        public override DocumentType GetDocumentType()
        { return DocumentType.ResourceIdentifierDocument; }

        public override ResourceIdentifier GetResourceIdentifier()
        { return this.Data; }

        public override void SetResourceIdentifier(ResourceIdentifier resourceIdentifier)
        { this.Data = resourceIdentifier; }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public new static readonly ResourceIdentifierDocument Empty = new ResourceIdentifierDocument();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ResourceIdentifierDocument).Name;
        #endregion
    }
}