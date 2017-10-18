// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents an immutable json:api to-many relationship object.
    /// </summary>
    /// <remarks>
    /// Per the json:api standard, the to-many "resource linkage" should be
    /// an empty array for an empty to-many relationship.
    /// </remarks>
    public class ToManyRelationship : Relationship
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToManyRelationship(IEnumerable<ResourceIdentifier> data)
            : this(null, data, null)
        { }

        public ToManyRelationship(Links links, IEnumerable<ResourceIdentifier> data)
            : this(links, data, null)
        { }

        public ToManyRelationship(IEnumerable<ResourceIdentifier> data, Meta meta)
            : this(null, data, meta)
        { }

        public ToManyRelationship(Links links, IEnumerable<ResourceIdentifier> data, Meta meta)
            : base(links, meta)
        { this.Data = data ?? Enumerable.Empty<ResourceIdentifier>(); }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region JSON Properties
        public IEnumerable<ResourceIdentifier> Data { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var links = this.Links ?? Links.Empty;
            var data = this.Data;
            var dataAsString = String.Join(",", data.Select(x => x.ToString()));
            return $"{TypeName} [links={links} data=[{dataAsString}]]";
        }
        #endregion

        #region Relationship Overrides
        public override RelationshipType GetRelationshipType()
        { return RelationshipType.ToManyRelationship; }

        public override IEnumerable<ResourceIdentifier> GetToManyResourceLinkage()
        { return this.Data; }

        public override bool IsResourceLinkageNullOrEmpty()
        { return this.Data.Any() == false; }

        public override bool IsToManyRelationship()
        { return true; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ToManyRelationship).Name;
        #endregion
    }
}