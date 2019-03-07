// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class NullResourceLinksBuilder<TParentBuilder> : NullLinksBuilder<IResourceLinksBuilder<TParentBuilder>, TParentBuilder>, IResourceLinksBuilder<TParentBuilder>
    {
        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        internal NullResourceLinksBuilder(TParentBuilder parentBuilder)
            : base(parentBuilder)
        {
            this.Builder = this;
        }
        #endregion

    }

    internal class NullResourceLinksBuilder<TParentBuilder, TResource> : NullLinksBuilder<IResourceLinksBuilder<TParentBuilder, TResource>, TParentBuilder>, IResourceLinksBuilder<TParentBuilder, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceLinksBuilder<TParentBuilder, TResource> Implementation
        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, Link link)
        {
            return this;
        }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, IEnumerable<Link> linkCollection)
        {
            return this;
        }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate)
        {
            return this;
        }

        public ILinkBuilder<IResourceLinksBuilder<TParentBuilder, TResource>> Link(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var linkBuilder = new NullLinkBuilder<IResourceLinksBuilder<TParentBuilder, TResource>>(this);
            return linkBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal NullResourceLinksBuilder(TParentBuilder parentBuilder)
            : base(parentBuilder)
        {
            this.Builder = this;
        }
        #endregion
    }
}
