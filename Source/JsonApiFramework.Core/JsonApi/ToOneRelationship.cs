// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Properties;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents an immutable json:api to-one relationship object.
    /// </summary>
    /// <remarks>
    /// Per the json:api standard, the to-one "resource linkage" should be
    /// null for an empty to-one relationship.
    /// </remarks>
    public class ToOneRelationship : Relationship
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToOneRelationship(ResourceIdentifier data)
            : this(null, data, null)
        { }

        public ToOneRelationship(Links links, ResourceIdentifier data)
            : this(links, data, null)
        { }

        public ToOneRelationship(ResourceIdentifier data, Meta meta)
            : this(null, data, meta)
        { }

        public ToOneRelationship(Links links, ResourceIdentifier data, Meta meta)
            : base(links, meta)
        { this.Data = data; }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public ResourceIdentifier Data { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var links = this.Links ?? Links.Empty;
            var data = this.Data != null ? this.Data.ToString() : CoreStrings.NullText;
            return $"{TypeName} [links={links} data={data}]";
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
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ToOneRelationship).Name;
        #endregion
    }
}