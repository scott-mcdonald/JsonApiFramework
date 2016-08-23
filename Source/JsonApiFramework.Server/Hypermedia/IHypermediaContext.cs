// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

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
        IServiceModel ServiceModel { get; }

        /// <summary>URL builder configuration when building URL strings.</summary>
        IUrlBuilderConfiguration UrlBuilderConfiguration { get; }
        #endregion
    }
}
