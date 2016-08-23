// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Extends the <c>Relationship</c> class to include "resource linkage"
    /// for a one to many relationship.
    /// </summary>
    /// <remarks>
    /// Array should be an empty array [] for empty to-many relationships if
    /// including "resource linkage".
    /// </remarks>
    [JsonObject(MemberSerialization.OptIn)]    
    public class ToManyRelationship : Relationship
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToManyRelationship()
        {
            this.Data = new List<ResourceIdentifier>();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        [JsonProperty(Keywords.Data)] public List<ResourceIdentifier> Data { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var self = this.Links != null
                ? this.Links.Self ?? Link.Empty
                : Link.Empty;

            var related = this.Links != null
                ? this.Links.Related ?? Link.Empty
                : Link.Empty;

            var dataElements = this.Data != null && this.Data.Any()
                ? this.Data
                      .Select(x => x.ToString())
                      .Aggregate((current, next) => current.ToString() + ", " + next.ToString())
                : null;

            var data = this.Data != null
                ? String.Format("[{0}]", dataElements)
                : JsonConstants.Null;

            return String.Format("{0} [self={1} related={2} data={3}]", TypeName, self, related, data);
        }
        #endregion

        #region Relationship Overrides
        public override void AddToManyResourceLinkage(Resource resource)
        {
            Contract.Requires(resource != null);

            this.Data = this.Data ?? new List<ResourceIdentifier>();
            this.Data.Add((ResourceIdentifier)resource);
        }

        public override void AddToManyResourceLinkageRange(IEnumerable<Resource> resourceCollection)
        {
            Contract.Requires(resourceCollection != null && resourceCollection.All(x => x != null));

            this.Data = this.Data ?? new List<ResourceIdentifier>();
            this.Data.AddRange(resourceCollection.Select(x => (ResourceIdentifier)x));
        }

        public override void AddToManyResourceLinkage(ResourceIdentifier resourceIdentifier)
        {
            Contract.Requires(resourceIdentifier != null);

            this.Data = this.Data ?? new List<ResourceIdentifier>();
            this.Data.Add(resourceIdentifier);
        }

        public override void AddToManyResourceLinkageRange(IEnumerable<ResourceIdentifier> resourceIdentifierCollection)
        {
            Contract.Requires(resourceIdentifierCollection != null && resourceIdentifierCollection.All(x => x != null));

            this.Data = this.Data ?? new List<ResourceIdentifier>();
            this.Data.AddRange(resourceIdentifierCollection);
        }

        public override RelationshipType GetRelationshipType()
        { return RelationshipType.ToManyRelationship; }

        public override IEnumerable<ResourceIdentifier> GetToManyResourceLinkage()
        { return this.Data; }

        public override bool IsResourceLinkageNullOrEmpty()
        { return this.Data == null || !this.Data.Any(); }

        public override bool IsToManyRelationship()
        { return true; }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public new static readonly ToManyRelationship Empty = new ToManyRelationship();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ToManyRelationship).Name;
        #endregion
    }
}