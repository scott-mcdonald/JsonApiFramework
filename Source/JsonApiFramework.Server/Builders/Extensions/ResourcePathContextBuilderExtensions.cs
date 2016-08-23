// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server
{
    public static class ResourcePathContextBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IResourcePathContextBuilder<TParentBuilder> AddPath<TParentBuilder, TFromResource, TToResource>(this IResourcePathContextBuilder<TParentBuilder> resourcePathContextBuilder, IToOneResourceLinkage<TFromResource, TToResource> toOneResourceLinkage)
            where TParentBuilder : class
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(resourcePathContextBuilder != null);
            Contract.Requires(toOneResourceLinkage != null);

            var fromResource = toOneResourceLinkage.FromResource;
            var fromRel = toOneResourceLinkage.FromRel;
            return resourcePathContextBuilder.AddPath(fromResource, fromRel);
        }

        public static IResourcePathContextBuilder<TParentBuilder> AddPath<TParentBuilder, TFromResource, TToResource>(this IResourcePathContextBuilder<TParentBuilder> resourcePathContextBuilder, IToManyResourceLinkage<TFromResource, TToResource> toManyResourceLinkage)
            where TParentBuilder : class
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(resourcePathContextBuilder != null);
            Contract.Requires(toManyResourceLinkage != null);

            var fromResource = toManyResourceLinkage.FromResource;
            var fromRel = toManyResourceLinkage.FromRel;
            return resourcePathContextBuilder.AddPath(fromResource, fromRel);
        }
        #endregion
    }
}