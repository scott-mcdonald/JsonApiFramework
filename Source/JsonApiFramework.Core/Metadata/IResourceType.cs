// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the metadata for a resource type in a service model.</summary>
    public interface IResourceType : ITypeBase
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the 'resource identity' metadata of the resource type.</summary>
        IResourceIdentityInfo ResourceIdentityInfo { get; }

        /// <summary>Gets the 'relationships' metadata of the resource type.</summary>
        IRelationshipsInfo RelationshipsInfo { get; }

        /// <summary>Gets the 'links' metadata of the resource type.</summary>
        ILinksInfo LinksInfo { get; }
        #endregion
    }
}