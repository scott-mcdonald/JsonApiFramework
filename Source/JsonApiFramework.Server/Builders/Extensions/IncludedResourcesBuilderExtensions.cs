// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Server
{
    public static class IncludedResourcesBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods

        public static IToOneIncludedResourceBuilder Include(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneIncludedResource[] toOneIncludedResourceCollection)
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.Include(toOneIncludedResourceCollection.AsEnumerable());
        }

        public static IToManyIncludedResourcesBuilder Include(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyIncludedResources[] toManyIncludedResourcesCollection)
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.Include(toManyIncludedResourcesCollection.AsEnumerable());
        }

        public static IIncludedResourcesBuilder AddInclude(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneIncludedResource[] toOneIncludedResourceCollection)
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddInclude(toOneIncludedResourceCollection.AsEnumerable());
        }

        public static IIncludedResourcesBuilder AddInclude(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyIncludedResources[] toManyIncludedResourcesCollection)
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddInclude(toManyIncludedResourcesCollection.AsEnumerable());
        }

        public static IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneIncludedResource<TFromResource, TToResource>[] toOneIncludedResourceCollection)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.Include(toOneIncludedResourceCollection.AsEnumerable());
        }

        public static IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyIncludedResources<TFromResource, TToResource>[] toManyIncludedResourcesCollection)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.Include(toManyIncludedResourcesCollection.AsEnumerable());
        }

        public static IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneIncludedResource<TFromResource, TToResource>[] toOneIncludedResourceCollection)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddInclude(toOneIncludedResourceCollection.AsEnumerable());
        }

        public static IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyIncludedResources<TFromResource, TToResource>[] toManyIncludedResourcesCollection)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddInclude(toManyIncludedResourcesCollection.AsEnumerable());
        }
        #endregion
    }
}