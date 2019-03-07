// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal abstract class NullLinksBuilder<TBuilder, TParentBuilder> : ILinksBuilder<TBuilder, TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ILinksBuilder<TParentBuilder> Implementation
        public TBuilder AddLink(string rel, Link link)
        {
            var builder = this.Builder;
            return builder;
        }

        public TBuilder AddLink(string rel, IEnumerable<Link> linkCollection)
        {
            var builder = this.Builder;
            return builder;
        }

        public TBuilder AddLink(string rel)
        {
            var builder = this.Builder;
            return builder;
        }

        public TBuilder AddLink(string rel, Meta meta)
        {
            var builder = this.Builder;
            return builder;
        }

        public TBuilder AddLink(string rel, IEnumerable<Meta> metaCollection)
        {
            var builder = this.Builder;
            return builder;
        }

        public ILinkBuilder<TBuilder> Link(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var linkBuilder = new NullLinkBuilder<TBuilder>(this.Builder);
            return linkBuilder;
        }

        public TParentBuilder LinksEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected NullLinksBuilder(TParentBuilder parentBuilder)
        {
            Contract.Requires(parentBuilder != null);

            this.ParentBuilder = parentBuilder;
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected TBuilder Builder { private get; set; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        #endregion
    }
}
