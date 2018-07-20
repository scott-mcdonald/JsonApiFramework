// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Http;
using JsonApiFramework.Internal;
using JsonApiFramework.Server.Hypermedia;

namespace JsonApiFramework.Server.Internal
{
    internal static class DocumentContextImplementationExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IHypermediaAssemblerRegistry GetHypermediaAssemblerRegistry(this IDocumentContextImplementation implementation)
        {
            Contract.Requires(implementation != null);

            var serverExtension             = implementation.Options.GetExtension<ServerDocumentContextExtension>();
            var hypermediaAssemblerRegistry = serverExtension.HypermediaAssemblerRegistry;
            return hypermediaAssemblerRegistry;
        }

        public static IHypermediaContext GetHypermediaContext(this IDocumentContextImplementation implementation)
        {
            Contract.Requires(implementation != null);

            var serverExtension   = implementation.Options.GetExtension<ServerDocumentContextExtension>();
            var hypermediaContext = serverExtension.HypermediaContext;
            return hypermediaContext;
        }

        public static bool IsSparseFieldsetsEnabled(this IDocumentContextImplementation implementation)
        {
            Contract.Requires(implementation != null);

            var serverExtension        = implementation.Options.GetExtension<ServerDocumentContextExtension>();
            var sparseFieldsetsEnabled = serverExtension.SparseFieldsetsEnabled;
            return sparseFieldsetsEnabled;
        }

        public static IUrlBuilderConfiguration GetUrlBuilderConfiguration(this IDocumentContextImplementation implementation)
        {
            Contract.Requires(implementation != null);

            var serverExtension         = implementation.Options.GetExtension<ServerDocumentContextExtension>();
            var urlBuilderConfiguration = serverExtension.UrlBuilderConfiguration;
            return urlBuilderConfiguration;
        }

        public static void SetHypermediaAssemblerRegistry(this IDocumentContextImplementation implementation, IHypermediaAssemblerRegistry hypermediaAssemblerRegistry)
        {
            Contract.Requires(implementation              != null);
            Contract.Requires(hypermediaAssemblerRegistry != null);

            implementation.Options.ModifyExtension<ServerDocumentContextExtension>(x => x.HypermediaAssemblerRegistry = hypermediaAssemblerRegistry);
        }

        public static void SetHypermediaContext(this IDocumentContextImplementation implementation, IHypermediaContext hypermediaContext)
        {
            Contract.Requires(implementation    != null);
            Contract.Requires(hypermediaContext != null);

            implementation.Options.ModifyExtension<ServerDocumentContextExtension>(x => x.HypermediaContext = hypermediaContext);
        }

        public static void SetUrlBuilderConfiguration(this IDocumentContextImplementation implementation, IUrlBuilderConfiguration urlBuilderConfiguration)
        {
            Contract.Requires(implementation          != null);
            Contract.Requires(urlBuilderConfiguration != null);

            implementation.Options.ModifyExtension<ServerDocumentContextExtension>(x => x.UrlBuilderConfiguration = urlBuilderConfiguration);
        }
        #endregion
    }
}