// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a json:api compliant document where the primary data is a
    /// single resource object.
    /// </summary>
    /// <remarks>
    /// Data should be <b>null</b> if no resource object is in the document.
    /// </remarks>
    public class ResourceDocument : Document
        , IGetIncluded
        , ISetIncluded
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        /// <summary>Primary data as a single resource.</summary>
        public Resource Data { get; set; }

        /// <summary>Included related resources.</summary>
        public List<Resource> Included { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return TypeName; }
        #endregion

        #region Document Overrides
        public override void AddIncludedResource(Resource resource)
        {
            if (resource == null)
                return;

            this.Included = this.Included ?? new List<Resource>();
            this.Included.Add(resource);
        }

        public override void AddIncludedResources(IEnumerable<Resource> resourceCollection)
        {
            if (resourceCollection == null)
                return;

            this.Included = this.Included ?? new List<Resource>();
            this.Included.AddRange(resourceCollection);
        }

        public override object GetData()
        { return this.Data; }

        public override DocumentType GetDocumentType()
        { return DocumentType.ResourceDocument; }

        public override IEnumerable<Resource> GetIncludedResources()
        { return this.Included; }

        public override Resource GetResource()
        { return this.Data; }

        public override void SetResource(Resource resource)
        { this.Data = resource; }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public new static readonly ResourceDocument Empty = new ResourceDocument();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ResourceDocument).Name;
        #endregion
    }
}