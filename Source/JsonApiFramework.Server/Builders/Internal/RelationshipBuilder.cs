// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class RelationshipBuilder<TParentBuilder, TResource> : IRelationshipBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            this.DomReadWriteRelationship.SetDomReadOnlyMeta(meta);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(IEnumerable<Meta> metaCollection)
        {
            var rel = this.Rel;
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildLinkWithCollectionOfObjects
                                           .FormatWith(DomNodeType.Meta, rel);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>> Links()
        {
            var linksBuilder = new RelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>>(this, this.DomReadWriteRelationship, this.Rel);
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
        internal RelationshipBuilder(TParentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, string rel)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domContainerNode != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.ParentBuilder = parentBuilder;

            var domReadWriteRelationships = (DomReadWriteRelationships)domContainerNode;
            var domReadWriteRelationship = domReadWriteRelationships.AddDomReadWriteRelationship(rel);
            this.DomReadWriteRelationship = domReadWriteRelationship;

            this.Rel = rel;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        private DomReadWriteRelationship DomReadWriteRelationship { get; set; }
        private string Rel { get; set; }
        #endregion
    }
}
