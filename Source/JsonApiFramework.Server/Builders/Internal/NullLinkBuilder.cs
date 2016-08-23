// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class NullLinkBuilder<TParentBuilder> : ILinkBuilder<TParentBuilder>
        where TParentBuilder : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ILinksBuilder<TParentBuilder> Implementation
        public ILinkBuilder<TParentBuilder> SetMeta(Meta meta)
        { return this; }

        public ILinkBuilder<TParentBuilder> SetMeta(IEnumerable<Meta> metaCollection)
        { return this; }

        public TParentBuilder LinkEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal NullLinkBuilder(TParentBuilder parentBuilder)
        {
            Contract.Requires(parentBuilder != null);

            this.ParentBuilder = parentBuilder;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        #endregion
    }
}
