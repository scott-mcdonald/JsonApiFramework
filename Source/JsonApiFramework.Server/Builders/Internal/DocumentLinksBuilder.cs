// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class DocumentLinksBuilder<TParentBuilder> : LinksBuilder<IDocumentLinksBuilder<TParentBuilder>, TParentBuilder>, IDocumentLinksBuilder<TParentBuilder>
    {
        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        internal DocumentLinksBuilder(TParentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode)
            : base(parentBuilder, domContainerNode)
        {
            this.Builder = this;
        }
        #endregion

        #region LinksBuilder<TBuilder, TParentBuilder> Overrides
        public override IDocumentLinksBuilder<TParentBuilder> AddLink(string rel, IEnumerable<Link> linkCollection)
        {
            var linkDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Link, rel);
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildDocumentWithCollectionOfObjects
                                           .FormatWith(linkDescription);
            throw new DocumentBuildException(detail);
        }
        #endregion
    }
}
