// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

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

        public ToManyRelationship(IEnumerable<ResourceIdentifier> data, Links links)
            : this(null, data, links)
        { }

        public ToManyRelationship(Meta meta, IEnumerable<ResourceIdentifier> data)
            : this(meta, data, null)
        { }

        public ToManyRelationship(Meta meta, IEnumerable<ResourceIdentifier> data, Links links)
            : base(meta, links)
        {
            this.Data = data;
        }
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
            var dataCollection = this.Data ?? Enumerable.Empty<ResourceIdentifier>();
            var data = dataCollection.Select(x => x.ToString())
                                     .Aggregate((current, next) => current.ToString() + ", " + next.ToString());
            return $"{TypeName} [links={links} data={data}]";
        }
        #endregion

        #region Relationship Overrides
        public override RelationshipType GetRelationshipType()
        { return RelationshipType.ToManyRelationship; }

        public override IEnumerable<ResourceIdentifier> GetToManyResourceLinkage()
        { return this.Data; }

        public override bool IsResourceLinkageNullOrEmpty()
        { return this.Data == null || !this.Data.Any(); }

        public override bool IsToManyRelationship()
        { return true; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly string TypeName = typeof(ToManyRelationship).Name;
        #endregion
    }
}