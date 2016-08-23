// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Http;

namespace JsonApiFramework.Server.Hypermedia
{
    public static class HypermediaContextExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static UrlBuilder CreateUrlBuilder(this IHypermediaContext hypermediaContext)
        {
            Contract.Requires(hypermediaContext != null);

            var urlBuilderConfiguration = hypermediaContext.UrlBuilderConfiguration;
            var urlBuilder = UrlBuilder.Create(urlBuilderConfiguration);
            return urlBuilder;
        }
        #endregion
    }
}