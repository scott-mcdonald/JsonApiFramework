// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
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
            Contract.Requires(hypermediaAssemblerRegistry   != null);

            documentContextOptionsBuilder.ModifyExtension<ServerDocumentContextExtension>(x => x.HypermediaAssemblerRegistry = hypermediaAssemblerRegistry);
            return documentContextOptionsBuilder;
        }

        public static IDocumentContextOptionsBuilder UseUrlBuilderConfiguration(this IDocumentContextOptionsBuilder documentContextOptionsBuilder, IUrlBuilderConfiguration urlBuilderConfiguration)
        {
            Contract.Requires(documentContextOptionsBuilder != null);
            Contract.Requires(urlBuilderConfiguration       != null);

            documentContextOptionsBuilder.ModifyExtension<ServerDocumentContextExtension>(x => x.UrlBuilderConfiguration = urlBuilderConfiguration);
            return documentContextOptionsBuilder;
        }

        public static IDocumentContextOptionsBuilder UseUrlBuilderConfiguration(this IDocumentContextOptionsBuilder documentContextOptionsBuilder, Type resourceType, IUrlBuilderConfiguration urlBuilderConfiguration)
        {
            Contract.Requires(documentContextOptionsBuilder != null);
            Contract.Requires(resourceType                  != null);
            Contract.Requires(urlBuilderConfiguration       != null);

            documentContextOptionsBuilder.ModifyExtension<ServerDocumentContextExtension>(x =>
            {
                x.UrlBuilderConfigurationPerResourceType = x.UrlBuilderConfigurationPerResourceType ?? new Dictionary<Type, IUrlBuilderConfiguration>();
                if (x.UrlBuilderConfigurationPerResourceType.ContainsKey(resourceType))
                    return;

                x.UrlBuilderConfigurationPerResourceType.Add(resourceType, urlBuilderConfiguration);
            });
            return documentContextOptionsBuilder;
        }

        public static IDocumentContextOptionsBuilder UseUrlBuilderConfiguration<TResource>(this IDocumentContextOptionsBuilder documentContextOptionsBuilder, IUrlBuilderConfiguration urlBuilderConfiguration)
            where TResource : class
        {
            Contract.Requires(documentContextOptionsBuilder != null);
            Contract.Requires(urlBuilderConfiguration       != null);

            var resourceType = typeof(TResource);
            return documentContextOptionsBuilder.UseUrlBuilderConfiguration(resourceType, urlBuilderConfiguration);
        }

        public static IDocumentContextOptionsBuilder UseSparseFieldsets(this IDocumentContextOptionsBuilder documentContextOptionsBuilder, bool enabled)
        {
            Contract.Requires(documentContextOptionsBuilder != null);

            documentContextOptionsBuilder.ModifyExtension<ServerDocumentContextExtension>(x => x.SparseFieldsetsEnabled = enabled);
            return documentContextOptionsBuilder;
        }
        #endregion
    }
}