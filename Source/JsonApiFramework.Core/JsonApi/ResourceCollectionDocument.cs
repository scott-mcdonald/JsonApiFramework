// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents a json:api compliant document where the primary data is a
    /// collection of resource objects.
    /// </summary>
    /// <remarks>
    /// Data should be an <b>empty</b> array for if no resource objects are in
    /// the document.
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]
    public class ResourceCollectionDocument : Document
        , IGetIncluded
        , ISetIncluded
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceCollectionDocument()
        {
            this.Data = Enumerable.Empty<Resource>()
                                  .ToList();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        /// <summary>Primary data as a resource collection.</summary>
        [JsonProperty(Keywords.Data)] public List<Resource> Data { get; set; }

        /// <summary>Included related resources.</summary>
        [JsonProperty(Keywords.Included)] public List<Resource> Included { get; set; }
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

        public override void AddResource(Resource resource)
        {
            if (resource == null)
                return;

            this.Data = this.Data ?? new List<Resource>();
            this.Data.Add(resource);
        }

        public override void AddResourceRange(IEnumerable<Resource> resourceCollection)
        {
            if (resourceCollection == null)
                return;

            this.Data = this.Data ?? new List<Resource>();
            this.Data.AddRange(resourceCollection);
        }

        public override object GetData()
        { return this.Data; }

        public override DocumentType GetDocumentType()
        { return DocumentType.ResourceCollectionDocument; }

        public override IEnumerable<Resource> GetIncludedResources()
        { return this.Included; }

        public override IEnumerable<Resource> GetResourceCollection()
        { return this.Data; }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public new static readonly ResourceCollectionDocument Empty = new ResourceCollectionDocument();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ResourceCollectionDocument).Name;
        #endregion
    }
}