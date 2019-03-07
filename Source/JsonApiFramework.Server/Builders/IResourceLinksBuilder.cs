// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public interface IResourceLinksBuilder<out TParentBuilder> : ILinksBuilder<IResourceLinksBuilder<TParentBuilder>, TParentBuilder>
    {
    }

    public interface IResourceLinksBuilder<out TParentBuilder, out TResource> : ILinksBuilder<IResourceLinksBuilder<TParentBuilder, TResource>, TParentBuilder>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, Link link);
        IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, IEnumerable<Link> linkCollection);
        IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate);

        ILinkBuilder<IResourceLinksBuilder<TParentBuilder, TResource>> Link(string rel, Func<TResource, bool> predicate);
        #endregion
    }
}