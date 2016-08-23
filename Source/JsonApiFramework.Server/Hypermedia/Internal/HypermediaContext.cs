// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Http;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal class HypermediaContext : IHypermediaContext
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public HypermediaContext(IServiceModel serviceModel, IUrlBuilderConfiguration urlBuilderConfiguration)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(urlBuilderConfiguration != null);

            this.ServiceModel = serviceModel;
            this.UrlBuilderConfiguration = urlBuilderConfiguration;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IHypermediaContext Implementation
        public IServiceModel ServiceModel { get; private set; }
        public IUrlBuilderConfiguration UrlBuilderConfiguration { get; private set; }
        #endregion
    }
}
