// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Hypermedia
{
    /// <summary>
    /// Abstracts runtime context for creating an individual relationship object.
    /// </summary>
    public interface IRelationshipContext
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Relation name of the relationship.</summary>
        string Rel { get; }

        /// <summary>Collection of link context objects that control what links the relationship contains.</summary>
        IEnumerable<ILinkContext> LinkContexts { get; }

        /// <summary>Meta object to associate with the relationship.</summary>
        Meta Meta { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the relationship type based on available or unavailable resource linkage.</summary>
        RelationshipType GetRelationshipType();

        /// <summary>Gets resource linkage for intended for a to-one relationship.</summary>
        ResourceIdentifier GetToOneResourceLinkage();

        /// <summary>Gets resource linkage for intended for a to-one relationship.</summary>
        IEnumerable<ResourceIdentifier> GetToManyResourceLinkage();
        #endregion
    }
}
