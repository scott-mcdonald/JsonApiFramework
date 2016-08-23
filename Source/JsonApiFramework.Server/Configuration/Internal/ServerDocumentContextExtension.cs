// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Http;
using JsonApiFramework.Server.Hypermedia;

namespace JsonApiFramework.Server.Internal
{
    /// <summary>Server document context extension. Internal use only</summary>
    internal class ServerDocumentContextExtension : IDocumentContextExtension
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Server URL builder configuration.</summary>
        public IUrlBuilderConfiguration UrlBuilderConfiguration { get; set; }

        /// <summary>Server hypermedia assembler registry for custom hypermedia assemblers.</summary>
        public IHypermediaAssemblerRegistry HypermediaAssemblerRegistry { get; set; }

        /// <summary>Server hypermedia context for building framework-level hypermedia.</summary>
        public IHypermediaContext HypermediaContext { get; set; }
        #endregion
    }
}