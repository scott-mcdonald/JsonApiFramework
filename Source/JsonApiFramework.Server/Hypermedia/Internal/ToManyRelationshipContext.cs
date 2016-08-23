// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal class ToManyRelationshipContext : RelationshipContext
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToManyRelationshipContext(string rel, IEnumerable<ILinkContext> links, IEnumerable<ResourceIdentifier> toManyResourceLinkage, Meta meta = null)
            : base(rel, links, meta)
        {
            this.ToManyResourceLinkage = toManyResourceLinkage.EmptyIfNull();
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region RelationshipContext Overrides
        public override RelationshipType GetRelationshipType()
        { return RelationshipType.ToManyRelationship; }

        public override IEnumerable<ResourceIdentifier> GetToManyResourceLinkage()
        { return this.ToManyResourceLinkage; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IEnumerable<ResourceIdentifier> ToManyResourceLinkage { get; set; }
        #endregion
    }
}
