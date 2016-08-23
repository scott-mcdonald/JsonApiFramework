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
    internal class LinkBuilder<TParentBuilder> : ILinkBuilder<TParentBuilder>
        where TParentBuilder : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ILinksBuilder<TParentBuilder> Implementation
        public ILinkBuilder<TParentBuilder> SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            this.DomReadWriteLink.SetDomReadOnlyMeta(meta);
            return this;
        }

        public ILinkBuilder<TParentBuilder> SetMeta(IEnumerable<Meta> metaCollection)
        {
            var rel = this.Rel;
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildLinkWithCollectionOfObjects
                                           .FormatWith(DomNodeType.Meta, rel);
            throw new DocumentBuildException(detail);
        }

        public TParentBuilder LinkEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal LinkBuilder(TParentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, string rel)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domContainerNode != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.ParentBuilder = parentBuilder;

            var domReadWriteLinks = (DomReadWriteLinks)domContainerNode;
            var domReadWriteLink = domReadWriteLinks.AddDomReadWriteLink(rel);
            this.DomReadWriteLink = domReadWriteLink;

            this.Rel = rel;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        private DomReadWriteLink DomReadWriteLink { get; set; }
        private string Rel { get; set; }
        #endregion
    }
}
