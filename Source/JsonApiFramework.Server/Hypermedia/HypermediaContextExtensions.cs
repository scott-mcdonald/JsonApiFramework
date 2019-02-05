// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Http;

namespace JsonApiFramework.Server.Hypermedia
{
    public static class HypermediaContextExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static UrlBuilder CreateUrlBuilder(this IHypermediaContext hypermediaContext, Type resourceType)
        {
            Contract.Requires(hypermediaContext != null);
            Contract.Requires(resourceType != null);

            var urlBuilderConfiguration = hypermediaContext.GetUrlBuilderConfiguration(resourceType);
            var urlBuilder = UrlBuilder.Create(urlBuilderConfiguration);
            return urlBuilder;
        }

        public static UrlBuilder CreateUrlBuilder<TResource>(this IHypermediaContext hypermediaContext)
            where TResource : class
        {
            Contract.Requires(hypermediaContext != null);

            var resourceType = typeof(TResource);
            return hypermediaContext.CreateUrlBuilder(resourceType);
        }
        #endregion
    }
}