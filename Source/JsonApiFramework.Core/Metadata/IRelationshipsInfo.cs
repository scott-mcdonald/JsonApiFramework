// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the metadata for all the 'relationships' as a whole on a json:api/CLR resource type.</summary>
    public interface IRelationshipsInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the optional CLR property binding of a <see cref="JsonApiFramework.JsonApi.Relationships"/> property for the CLR resource type.</summary>
        IClrPropertyBinding ClrRelationshipsPropertyBinding { get; }

        /// <summary>Gets the collection of all individual 'relationship' metadata for the json:api/CLR resource type.</summary>
        IReadOnlyCollection<IRelationshipInfo> RelationshipInfoCollection { get; }
        #endregion
    }
}