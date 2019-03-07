// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server
{
    public static class ToManyIncludedResources
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static IToManyIncludedResources Create(Type fromResourceType, object fromResource, string fromRel, Type toResourceType, IEnumerable<object> toResourceCollection)
        {
            Contract.Requires(fromResourceType != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));
            Contract.Requires(toResourceType != null);

            var toManyIncludedResources = new Internal.ToManyIncludedResources(fromResourceType, fromResource, fromRel, toResourceType, toResourceCollection);
            return toManyIncludedResources;
        }

        public static IToManyIncludedResources Create(object fromResource, string fromRel, Type toResourceType, IEnumerable<object> toResourceCollection)
        {
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));
            Contract.Requires(toResourceType != null);

            var fromResourceType        = fromResource.GetType();
            var toManyIncludedResources = new Internal.ToManyIncludedResources(fromResourceType, fromResource, fromRel, toResourceType, toResourceCollection);
            return toManyIncludedResources;
        }

        public static IToManyIncludedResources<TFromResource, TToResource> Create<TFromResource, TToResource>(Type fromResourceType, TFromResource fromResource, string fromRel, Type toResourceType, IEnumerable<TToResource> toResourceCollection)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(fromResourceType != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));
            Contract.Requires(toResourceType != null);

            var toManyIncludedResources = new Internal.ToManyIncludedResources<TFromResource, TToResource>(fromResourceType, fromResource, fromRel, toResourceType, toResourceCollection);
            return toManyIncludedResources;
        }

        public static IToManyIncludedResources<TFromResource, TToResource> Create<TFromResource, TToResource>(TFromResource fromResource, string fromRel, IEnumerable<TToResource> toResourceCollection)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));

            var fromResourceType        = typeof(TFromResource);
            var toResourceType          = typeof(TToResource);
            var toManyIncludedResources = new Internal.ToManyIncludedResources<TFromResource, TToResource>(fromResourceType, fromResource, fromRel, toResourceType, toResourceCollection);
            return toManyIncludedResources;
        }
        #endregion
    }
}