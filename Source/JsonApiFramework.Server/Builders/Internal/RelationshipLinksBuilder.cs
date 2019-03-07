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
    internal class RelationshipLinksBuilder<TParentBuilder> : LinksBuilder<IRelationshipLinksBuilder<TParentBuilder>, TParentBuilder>, IRelationshipLinksBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region LinksBuilder<TBuilder, TParentBuilder> Overrides
        public override IRelationshipLinksBuilder<TParentBuilder> AddLink(string rel, IEnumerable<Link> linkCollection)
        {
            var linkRel         = rel;
            var relationshipRel = this.Rel;
            var linkDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Link, linkRel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildRelationshipWithCollectionOfObjects
                                                   .FormatWith(linkDescription, relationshipRel);
            throw new DocumentBuildException(detail);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        internal RelationshipLinksBuilder(TParentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, string rel)
            : base(parentBuilder, domContainerNode)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.Builder = this;
            this.Rel     = rel;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private string Rel { get; }
        #endregion
    }
}
