// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server
{
    public static class ResourcePathContextBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IResourcePathContextBuilder<TParentBuilder> AddPath<TParentBuilder, TFromResource, TToResource>(this IResourcePathContextBuilder<TParentBuilder> resourcePathContextBuilder, IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            where TParentBuilder : class
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(resourcePathContextBuilder != null);
            Contract.Requires(toOneIncludedResource != null);

            var fromResource = toOneIncludedResource.FromResource;
            var fromRel = toOneIncludedResource.FromRel;
            return resourcePathContextBuilder.AddPath(fromResource, fromRel);
        }

        public static IResourcePathContextBuilder<TParentBuilder> AddPath<TParentBuilder, TFromResource, TToResource>(this IResourcePathContextBuilder<TParentBuilder> resourcePathContextBuilder, IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            where TParentBuilder : class
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(resourcePathContextBuilder != null);
            Contract.Requires(toManyIncludedResources != null);

            var fromResource = toManyIncludedResources.FromResource;
            var fromRel = toManyIncludedResources.FromRel;
            return resourcePathContextBuilder.AddPath(fromResource, fromRel);
        }
        #endregion
    }
}