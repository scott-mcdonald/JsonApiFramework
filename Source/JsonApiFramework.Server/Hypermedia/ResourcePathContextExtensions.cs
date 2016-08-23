// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Server.Hypermedia
{
    public static class ResourcePathContextExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static IEnumerable<string> GetResourceCanonicalPath(this IResourcePathContext resourcePathContext, string apiId)
        {
            Contract.Requires(resourcePathContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiId) == false);

            var resourceCanonicalBasePath = resourcePathContext.ResourceCanonicalBasePath;
            var resourceCanonicalPathMode = resourcePathContext.ResourceCanonicalPathMode;
            var resourceCanonicalPath = GetResourcePath(resourceCanonicalBasePath, resourceCanonicalPathMode, apiId);
            return resourceCanonicalPath;
        }

        public static IEnumerable<string> GetResourceSelfPath(this IResourcePathContext resourcePathContext, string apiId)
        {
            Contract.Requires(resourcePathContext != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiId) == false);

            var resourceSelfBasePath = resourcePathContext.ResourceSelfBasePath;
            var resourceSelfPathMode = resourcePathContext.ResourceSelfPathMode;
            var resourceSelfPath = GetResourcePath(resourceSelfBasePath, resourceSelfPathMode, apiId);
            return resourceSelfPath;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IEnumerable<string> GetResourcePath(IEnumerable<IHypermediaPath> resourceBasePath, ResourcePathMode resourcePathMode, string apiId)
        {
            Contract.Requires(resourceBasePath != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiId) == false);

            var resourcePath = resourceBasePath.SelectMany(x => x.PathSegments).ToList();
            switch (resourcePathMode)
            {
                case ResourcePathMode.IncludeApiId:
                    {
                        resourcePath.Add(apiId);
                    }
                    break;

                case ResourcePathMode.IgnoreApiId:
                    {
                        // NOOP
                    }
                    break;

                default:
                    {
                        var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                               .FormatWith(typeof(ResourcePathMode).Name, resourcePathMode);
                        throw new InternalErrorException(detail);
                    }
            }

            return resourcePath;
        }
        #endregion
    }
}
