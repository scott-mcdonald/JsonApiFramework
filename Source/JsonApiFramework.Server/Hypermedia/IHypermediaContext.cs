// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Http;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Hypermedia
{
    /// <summary>
    /// Abstracts runtime context for hypermedia assembly of document links,
    /// resource links, and resource relationships.
    /// </summary>
    public interface IHypermediaContext
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the service model metadata of the resource ecosystem to build hypermedia for.</summary>
        IServiceModel GetServiceModel();

        /// <summary>
        /// Gets the URL builder configuration for a given resource type when building URL strings for hypermedia purposes.
        /// </summary>
        /// <param name="resourceType">The resource type to get the URL builder configuration for.</param>
        IUrlBuilderConfiguration GetUrlBuilderConfiguration(Type resourceType);
        #endregion
    }
}
