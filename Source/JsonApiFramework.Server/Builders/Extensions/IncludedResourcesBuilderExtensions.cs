// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Server.Internal;

namespace JsonApiFramework.Server
{
    public static class IncludedResourcesBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IToOneIncludedResourceBuilder<TToResource> ToOne<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, TFromResource fromResource, string fromRel, TToResource toResource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));

            var toOneIncludedResource = new ToOneIncludedResource<TFromResource, TToResource>(fromResource, fromRel, toResource);
            return includedResourcesBuilder.ToOne(toOneIncludedResource);
        }

        public static IToOneIncludedResourceBuilder<TToResource> ToOne<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneIncludedResource<TFromResource, TToResource>[] toOneIncludedResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.ToOne(toOneIncludedResourceCollection);
        }

        public static IToManyIncludedResourcesBuilder<TToResource> ToMany<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, TFromResource fromResource, string fromRel, IEnumerable<TToResource> toResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));

            var toManyIncludedResources = new ToManyIncludedResources<TFromResource, TToResource>(fromResource, fromRel, toResourceCollection);
            return includedResourcesBuilder.ToMany(toManyIncludedResources);
        }

        public static IToManyIncludedResourcesBuilder<TToResource> ToMany<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyIncludedResources<TFromResource, TToResource>[] toManyIncludedResourcesCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.ToMany(toManyIncludedResourcesCollection);
        }

        public static IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneIncludedResource<TFromResource, TToResource>[] toOneIncludedResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddToOne(toOneIncludedResourceCollection);
        }

        public static IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyIncludedResources<TFromResource, TToResource>[] toManyIncludedResourcesCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddToMany(toManyIncludedResourcesCollection);
        }
        #endregion
    }
}