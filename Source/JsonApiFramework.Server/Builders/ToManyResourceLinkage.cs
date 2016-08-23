// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Server.Internal;

namespace JsonApiFramework.Server
{
    public static class ToManyResourceLinkage
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static IToManyResourceLinkage<TFromResource, TToResource> Create<TFromResource, TToResource>(TFromResource fromResource, string fromRel, IEnumerable<TToResource> toResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));

            var toManyResourceLinkage = new ToManyResourceLinkage<TFromResource, TToResource>(fromResource, fromRel, toResourceCollection);
            return toManyResourceLinkage;
        }

        public static IToManyResourceLinkage<TFromResource, TToResource> Create<TFromResource, TToResource>(TFromResource fromResource, string fromRel, params TToResource[] toResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel));

            var toManyResourceLinkage = ToManyResourceLinkage.Create(fromResource, fromRel, toResourceCollection.AsEnumerable());
            return toManyResourceLinkage;
        }
        #endregion
    }
}