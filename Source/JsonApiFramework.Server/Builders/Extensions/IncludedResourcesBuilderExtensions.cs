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
        public static IToOneResourceLinkageBuilder<TToResource> ToOne<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, TFromResource fromResource, string fromRel, TToResource toResource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));

            var toOneResourceLinkage = new ToOneResourceLinkage<TFromResource, TToResource>(fromResource, fromRel, toResource);
            return includedResourcesBuilder.ToOne(toOneResourceLinkage);
        }

        public static IToOneResourceLinkageBuilder<TToResource> ToOne<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneResourceLinkage<TFromResource, TToResource>[] toOneResourceLinkageCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.ToOne(toOneResourceLinkageCollection);
        }

        public static IToManyResourceLinkageBuilder<TToResource> ToMany<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, TFromResource fromResource, string fromRel, IEnumerable<TToResource> toResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));

            var toManyResourceLinkage = new ToManyResourceLinkage<TFromResource, TToResource>(fromResource, fromRel, toResourceCollection);
            return includedResourcesBuilder.ToMany(toManyResourceLinkage);
        }

        public static IToManyResourceLinkageBuilder<TToResource> ToMany<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyResourceLinkage<TFromResource, TToResource>[] toManyResourceLinkageCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.ToMany(toManyResourceLinkageCollection);
        }

        public static IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToOneResourceLinkage<TFromResource, TToResource>[] toOneResourceLinkageCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddToOne(toOneResourceLinkageCollection);
        }

        public static IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(this IIncludedResourcesBuilder includedResourcesBuilder, params IToManyResourceLinkage<TFromResource, TToResource>[] toManyResourceLinkageCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(includedResourcesBuilder != null);

            return includedResourcesBuilder.AddToMany(toManyResourceLinkageCollection);
        }
        #endregion
    }
}