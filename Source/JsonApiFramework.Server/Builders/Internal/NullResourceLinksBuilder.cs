// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class NullResourceLinksBuilder<TParentBuilder, TResource> : NullLinksBuilder<IResourceLinksBuilder<TParentBuilder, TResource>, TParentBuilder>, IResourceLinksBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceLinksBuilder<TParentBuilder, TResource>
        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, Link link)
        { return this; }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, IEnumerable<Link> linkCollection)
        { return this; }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate)
        { return this; }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Meta meta, Func<TResource, bool> predicate)
        { return this; }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, IEnumerable<Meta> metaCollection, Func<TResource, bool> predicate)
        { return this; }

        public ILinkBuilder<IResourceLinksBuilder<TParentBuilder, TResource>> Link(string rel, Func<TResource, bool> predicate)
        {
            var nullLinkBuilder = new NullLinkBuilder<IResourceLinksBuilder<TParentBuilder, TResource>>(this);
            return nullLinkBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        internal NullResourceLinksBuilder(TParentBuilder parentBuilder)
            : base(parentBuilder)
        {
            this.Builder = this;
        }
        #endregion

    }
}
