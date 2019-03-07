// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public static class LinkBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static ILinkBuilder<TParentBuilder> SetMeta<TParentBuilder>(this ILinkBuilder<TParentBuilder> linkBuilder, params Meta[] metaCollection)
        {
            Contract.Requires(linkBuilder != null);

            return linkBuilder.SetMeta(metaCollection.AsEnumerable());
        }
        #endregion
    }
}