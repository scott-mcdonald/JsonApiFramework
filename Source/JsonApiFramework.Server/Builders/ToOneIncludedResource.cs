// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Server.Internal;

namespace JsonApiFramework.Server
{
    public static class ToOneIncludedResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static IToOneIncludedResource<TFromResource, TToResource> Create<TFromResource, TToResource>(TFromResource fromResource, string fromRel, TToResource toResource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));

            var toOneIncludedResource = new ToOneIncludedResource<TFromResource, TToResource>(fromResource, fromRel, toResource);
            return toOneIncludedResource;
        }
        #endregion
    }
}