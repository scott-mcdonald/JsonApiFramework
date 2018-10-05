// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

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

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region IHypermediaContext Implementation
        public IServiceModel ServiceModel { get; }

        public IUrlBuilderConfiguration UrlBuilderConfiguration { get; }
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

        public IUrlBuilderConfiguration GetUrlBuilderConfiguration(Uri uri)
        {
            Contract.Requires(uri != null);

            var urlBuilderConfiguration = this.UrlBuilderConfigurationPerResourceType?.Values
                                              .FirstOrDefault(x => IsUriMatchForUrlBuilderConfiguration(x, uri));
            if (urlBuilderConfiguration != null)
                return urlBuilderConfiguration;

            if (IsUriMatchForUrlBuilderConfiguration(this.UrlBuilderConfiguration, uri))
                return this.UrlBuilderConfiguration;

            var detail = $"Unable to get a URL builer configuration based on the following URI: {uri}";
            throw new DocumentBuildException(detail);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDictionary<Type, IUrlBuilderConfiguration> UrlBuilderConfigurationPerResourceType { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        public static bool IsUriMatchForUrlBuilderConfiguration(IUrlBuilderConfiguration urlBuilderConfiguration, Uri uri)
        {
            Contract.Requires(uri != null);

            if (urlBuilderConfiguration == null)
                return false;

            // Scheme
            if (String.Compare(urlBuilderConfiguration.Scheme, uri.Scheme, StringComparison.OrdinalIgnoreCase) != 0)
                return false;

            // Host
            if (String.Compare(urlBuilderConfiguration.Host, uri.Host, StringComparison.OrdinalIgnoreCase) != 0)
                return false;

            // Port
            if (urlBuilderConfiguration.Port.HasValue)
            {
                if (urlBuilderConfiguration.Port.Value != uri.Port)
                    return false;
            }

            // Root Path Segments
            var rootPathSegments = urlBuilderConfiguration.RootPathSegments.SafeToReadOnlyList();
            if (rootPathSegments.Any() == false)
                return true;
            var rootPathSegmentsCount = rootPathSegments.Count;

            var uriPathSegments = uri.GetPathSegments()
                                     .SafeToReadOnlyList();
            var uriPathSegmentsCount = uriPathSegments.Count;

            if (uriPathSegmentsCount < rootPathSegmentsCount)
                return false;

            for (var i = 0; i < rootPathSegmentsCount; i++)
            {
                var rootPathSegment = rootPathSegments[i];
                var uriPathSegment  = uriPathSegments[i];

                if (String.Compare(rootPathSegment, uriPathSegment, StringComparison.OrdinalIgnoreCase) != 0)
                    return false;
            }

            return true;
        }

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
