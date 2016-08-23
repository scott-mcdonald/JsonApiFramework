// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Http;
using JsonApiFramework.Server.Hypermedia;
using JsonApiFramework.Server.Internal;

namespace JsonApiFramework.Server
{
    /// <summary>
    /// Server side document context options builder.
    /// </summary>
    public static class ServerDocumentContextExtensionBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IDocumentContextOptionsBuilder UseHypermediaAssemblerRegistry(this IDocumentContextOptionsBuilder documentContextOptionsBuilder, IHypermediaAssemblerRegistry hypermediaAssemblerRegistry)
        {
            Contract.Requires(documentContextOptionsBuilder != null);
            Contract.Requires(hypermediaAssemblerRegistry != null);

            documentContextOptionsBuilder.ModifyExtension<ServerDocumentContextExtension>(x => x.HypermediaAssemblerRegistry = hypermediaAssemblerRegistry);
            return documentContextOptionsBuilder;
        }

        public static IDocumentContextOptionsBuilder UseUrlBuilderConfiguration(this IDocumentContextOptionsBuilder documentContextOptionsBuilder, IUrlBuilderConfiguration urlBuilderConfiguration)
        {
            Contract.Requires(documentContextOptionsBuilder != null);
            Contract.Requires(urlBuilderConfiguration != null);

            documentContextOptionsBuilder.ModifyExtension<ServerDocumentContextExtension>(x => x.UrlBuilderConfiguration = urlBuilderConfiguration);
            return documentContextOptionsBuilder;
        }
        #endregion
    }
}