// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Json;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Extends the <c>Relationship</c> class to include "resource linkage"
    /// for a one to one relationship.
    /// </summary>
    /// <remarks>
    /// Object should be null for empty to-one relationships if including
    /// "resource linkage".
    /// </remarks>
    public class ToOneRelationship : Relationship
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public ResourceIdentifier Data { get; set; }
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

            var data = this.Data != null
                ? this.Data.ToString()
                : CoreStrings.NullText;

            return String.Format("{0} [self={1} related={2} data={3}]", TypeName, self, related, data);
        }
        #endregion

        #region Relationship Overrides
        public override RelationshipType GetRelationshipType()
        { return RelationshipType.ToOneRelationship; }

        public override ResourceIdentifier GetToOneResourceLinkage()
        { return this.Data; }

        public override bool IsResourceLinkageNullOrEmpty()
        { return this.Data == null; }

        public override bool IsToOneRelationship()
        { return true; }

        public override void SetToOneResourceLinkage(Resource resource)
        {
            var resourceIdentifier = resource != null
                ? (ResourceIdentifier)resource
                : null;
            this.Data = resourceIdentifier;
        }

        public override void SetToOneResourceLinkage(ResourceIdentifier resourceIdentifier)
        {
            this.Data = resourceIdentifier;
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public new static readonly ToOneRelationship Empty = new ToOneRelationship();
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ToOneRelationship).Name;
        #endregion
    }
}