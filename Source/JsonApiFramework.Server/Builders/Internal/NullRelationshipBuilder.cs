// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class NullRelationshipBuilder<TParentBuilder, TResource> : IRelationshipBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(Meta meta)
        { return this; }

        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(IEnumerable<Meta> metaCollection)
        { return this; }

        public IRelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>> Links()
        {
            var linksBuilder = new NullRelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>>(this);
            return linksBuilder;
        }

        public TParentBuilder RelationshipEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal NullRelationshipBuilder(TParentBuilder parentBuilder)
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
