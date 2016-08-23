// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal class ToOneRelationshipContext : RelationshipContext
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToOneRelationshipContext(string rel, IEnumerable<ILinkContext> links, ResourceIdentifier toOneResourceLinkage, Meta meta = null)
            : base(rel, links, meta)
        {
            this.ToOneResourceLinkage = toOneResourceLinkage;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region RelationshipContext Overrides
        public override RelationshipType GetRelationshipType()
        { return RelationshipType.ToOneRelationship; }

        public override ResourceIdentifier GetToOneResourceLinkage()
        { return this.ToOneResourceLinkage; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ResourceIdentifier ToOneResourceLinkage { get; set; }
        #endregion
    }
}
