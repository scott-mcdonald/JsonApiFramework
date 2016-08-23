// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework
{
    public static class ResourceIdentifierBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceIdentifierBuilder<TBuilder> Extensions
        public static TBuilder SetMeta<TBuilder, TResource>(this IResourceIdentifierBuilder<TBuilder, TResource> resourceIdentifierBuilder, params Meta[] metaCollection)
            where TBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(resourceIdentifierBuilder != null);

            return resourceIdentifierBuilder.SetMeta(metaCollection.AsEnumerable());
        }

        public static TBuilder SetId<TBuilder, TResource, TResourceId>(this IResourceIdentifierBuilder<TBuilder, TResource> resourceIdentifierBuilder, params TResourceId[] clrResourceIdCollection)
            where TBuilder : class
            where TResource : class, IResource
        {
            Contract.Requires(resourceIdentifierBuilder != null);

            return resourceIdentifierBuilder.SetId(clrResourceIdCollection.AsEnumerable());
        }
        #endregion
    }
}