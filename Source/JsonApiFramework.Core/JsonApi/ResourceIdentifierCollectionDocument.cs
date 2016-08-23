// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a json:api compliant document where the primary data is a
    /// collection of resource identifier objects.
    /// </summary>
    /// <remarks>
    /// Data should be an <b>empty</b> array for if no resource identifier
    /// objects are in the document.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceIdentifierCollectionDocument : Document
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentifierCollectionDocument()
        {
            this.Data = Enumerable.Empty<ResourceIdentifier>()
                                  .ToList();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        /// <summary>Primary data as a resource identifier collection.</summary>
        [JsonProperty(Keywords.Data)] public List<ResourceIdentifier> Data { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return TypeName; }
        #endregion

        #region Document Overrides
        public override void AddResourceIdentifier(ResourceIdentifier resourceIdentifier)
        {
            if (resourceIdentifier == null)
                return;

            this.Data = this.Data ?? new List<ResourceIdentifier>();
            this.Data.Add(resourceIdentifier);
        }

        public override void AddResourceIdentifierRange(IEnumerable<ResourceIdentifier> resourceIdentifierCollection)
        {
            if (resourceIdentifierCollection == null)
                return;

            this.Data = this.Data ?? new List<ResourceIdentifier>();
            this.Data.AddRange(resourceIdentifierCollection);
        }

        public override object GetData()
        { return this.Data; }

        public override DocumentType GetDocumentType()
        { return DocumentType.ResourceIdentifierCollectionDocument; }

        public override IEnumerable<ResourceIdentifier> GetResourceIdentifierCollection()
        { return this.Data; }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public new static readonly ResourceIdentifierCollectionDocument Empty = new ResourceIdentifierCollectionDocument();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ResourceIdentifierCollectionDocument).Name;
        #endregion
    }
}