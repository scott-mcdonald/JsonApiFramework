// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server
{
    public static class ToOneIncludedResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static IToOneIncludedResource Create(Type fromResourceType, object fromResource, string fromRel, Type toResourceType, object toResource)
        {
            Contract.Requires(fromResourceType != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));
            Contract.Requires(toResourceType != null);

            var toOneIncludedResource = new Internal.ToOneIncludedResource(fromResourceType, fromResource, fromRel, toResourceType, toResource);
            return toOneIncludedResource;
        }

        public static IToOneIncludedResource Create(object fromResource, string fromRel, Type toResourceType, object toResource)
        {
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));
            Contract.Requires(toResourceType != null);

            var fromResourceType      = fromResource.GetType();
            var toOneIncludedResource = new Internal.ToOneIncludedResource(fromResourceType, fromResource, fromRel, toResourceType, toResource);
            return toOneIncludedResource;
        }

        public static IToOneIncludedResource<TFromResource, TToResource> Create<TFromResource, TToResource>(Type fromResourceType, TFromResource fromResource, string fromRel, Type toResourceType, TToResource toResource)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(fromResourceType != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));
            Contract.Requires(toResourceType != null);

            var toOneIncludedResource = new Internal.ToOneIncludedResource<TFromResource, TToResource>(fromResourceType, fromResource, fromRel, toResourceType, toResource);
            return toOneIncludedResource;
        }

        public static IToOneIncludedResource<TFromResource, TToResource> Create<TFromResource, TToResource>(TFromResource fromResource, string fromRel, TToResource toResource)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));

            var fromResourceType      = typeof(TFromResource);
            var toResourceType        = typeof(TToResource);
            var toOneIncludedResource = new Internal.ToOneIncludedResource<TFromResource, TToResource>(fromResourceType, fromResource, fromRel, toResourceType, toResource);
            return toOneIncludedResource;
        }
        #endregion
    }
}