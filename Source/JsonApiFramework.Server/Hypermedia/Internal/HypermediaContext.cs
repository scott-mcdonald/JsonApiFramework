// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Http;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal class HypermediaContext : IHypermediaContext
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public HypermediaContext(IServiceModel                               serviceModel,
                                 IUrlBuilderConfiguration                    urlBuilderConfiguration,
                                 IDictionary<Type, IUrlBuilderConfiguration> urlBuilderConfigurationPerResourceType)
        {
            Contract.Requires(serviceModel            != null);
            Contract.Requires(urlBuilderConfiguration != null);

            this.ServiceModel                           = serviceModel;
            this.UrlBuilderConfiguration                = urlBuilderConfiguration;
            this.UrlBuilderConfigurationPerResourceType = urlBuilderConfigurationPerResourceType;
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public IServiceModel GetServiceModel()
        {
            return this.ServiceModel;
        }

        public IUrlBuilderConfiguration GetUrlBuilderConfiguration(Type resourceType)
        {
            Contract.Requires(resourceType != null);

            var urlBuilderConfiguration = this.ResolveUrlBuilderConfiguration(resourceType);
            if (urlBuilderConfiguration != null)
                return urlBuilderConfiguration;

            if (resourceType == null)
            {
                throw new DocumentBuildException("Unable to get the global URL builder configuration. Please ensure there is a global URL builder configuration defined.");
            }

            var resourceTypeName = resourceType.Name;
            var detail           = $"Unable to get a URL builder configuration for CLR resource type [name={resourceTypeName}]. Please ensure there is a URL builder configuration for the specific CLR resource type or a global URL builder configuration defined.";
            throw new DocumentBuildException(detail);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region IHypermediaContext Implementation
        private IServiceModel ServiceModel { get; }

        private IUrlBuilderConfiguration UrlBuilderConfiguration { get; }

        private IDictionary<Type, IUrlBuilderConfiguration> UrlBuilderConfigurationPerResourceType { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        public IUrlBuilderConfiguration ResolveUrlBuilderConfiguration(Type resourceType)
        {
            if (resourceType == null || this.UrlBuilderConfigurationPerResourceType == null)
                return this.UrlBuilderConfiguration;

            return this.UrlBuilderConfigurationPerResourceType.TryGetValue(resourceType, out var urlBuilderConfiguration)
                ? urlBuilderConfiguration
                : this.UrlBuilderConfiguration;
        }
        #endregion
    }
}
