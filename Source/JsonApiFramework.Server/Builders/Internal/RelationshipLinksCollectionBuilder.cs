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
    internal class RelationshipLinksCollectionBuilder<TParentBuilder> : LinksCollectionBuilder<IRelationshipLinksBuilder<TParentBuilder>, TParentBuilder>, IRelationshipLinksBuilder<TParentBuilder>
        where TParentBuilder : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region LinksCollectionBuilder<TBuilder, TParentBuilder> Overrides
        public override IRelationshipLinksBuilder<TParentBuilder> AddLink(string rel, Link link)
        {
            var linkRel = rel;
            var relationshipRel = this.Rel;
            var linkDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Link, linkRel);
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildRelationshipCollectionWithSingleObject
                                           .FormatWith(linkDescription, relationshipRel);
            throw new DocumentBuildException(detail);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        internal RelationshipLinksCollectionBuilder(TParentBuilder parentBuilder, IEnumerable<IContainerNode<DomNodeType>> domContainerNodeCollection, string rel)
            : base(parentBuilder, domContainerNodeCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.Builder = this;
            this.Rel = rel;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private string Rel { get; set; }
        #endregion
    }
}
