// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Hypermedia
{
    /// <summary>
    /// Abstracts runtime context for creating an individual link object.
    /// </summary>
    public interface ILinkContext
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Relation name of the link.</summary>
        string Rel { get; }

        /// <summary>Meta object to associate with the link.</summary>
        Meta Meta { get; }
        #endregion
    }
}
