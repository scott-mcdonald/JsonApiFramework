// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class ResourceBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static TBuilder SetMeta<TBuilder, TResource>(this IResourceBuilder<TBuilder, TResource> resourceBuilder, params Meta[] metaCollection)
            where TBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(resourceBuilder != null);

            return resourceBuilder.SetMeta(metaCollection.AsEnumerable());
        }
        #endregion
    }
}