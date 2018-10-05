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
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Service model metadata of the resource ecosystem to build hypermedia for.</summary>
        [Obsolete("SericeModel property will be deprecated in the future, please use GetServiceModel() method instead.")]
        IServiceModel ServiceModel { get; }

        /// <summary>URL builder configuration when building URL strings.</summary>
        [Obsolete("UrlBuilderConfiguration property will be deprecated in the future, please use GetUrlBuilderConfiguration(resourceType) method instead.")]
        IUrlBuilderConfiguration UrlBuilderConfiguration { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the service model metadata of the resource ecosystem to build hypermedia for.</summary>
        IServiceModel GetServiceModel();

        /// <summary>
        /// Gets the URL builder configuration for a given resource type when building URL strings for hypermedia purposes.
        /// </summary>
        /// <param name="resourceType">The resource type to get the URL builder configuration for.</param>
        IUrlBuilderConfiguration GetUrlBuilderConfiguration(Type resourceType);

        /// <summary>
        /// Gets the URL builder configuration for a given URI when building URL strings for hypermedia purposes.
        /// </summary>
        /// <param name="uri">The URI object to get the URL builder configuration for.</param>
        IUrlBuilderConfiguration GetUrlBuilderConfiguration(Uri uri);
        #endregion
    }
}
