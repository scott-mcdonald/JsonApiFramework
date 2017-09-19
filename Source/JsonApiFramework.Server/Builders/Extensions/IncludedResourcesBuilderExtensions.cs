// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server
{
    public static class IncludedResourcesBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneIncludedResource<TFromResource, TToResource>[] toOneIncludedResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.Include(toOneIncludedResourceCollection);
        }

        public static IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyIncludedResources<TFromResource, TToResource>[] toManyIncludedResourcesCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.Include(toManyIncludedResourcesCollection);
        }

        public static IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneIncludedResource<TFromResource, TToResource>[] toOneIncludedResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddInclude(toOneIncludedResourceCollection);
        }

        public static IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyIncludedResources<TFromResource, TToResource>[] toManyIncludedResourcesCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddInclude(toManyIncludedResourcesCollection);
        }
        #endregion
    }
}