// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class LinkCollectionBuilder<TParentBuilder> : ILinkBuilder<TParentBuilder>
        where TParentBuilder : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ILinksBuilder<TParentBuilder> Implementation
        public ILinkBuilder<TParentBuilder> SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            var count = this.DomReadWriteLinkCollection.Count;
            for (var i = 0; i < count; ++i)
            {
                var domReadWriteLink = this.DomReadWriteLinkCollection[i];

                domReadWriteLink.SetDomReadOnlyMeta(meta);
            }

            return this;
        }

        public ILinkBuilder<TParentBuilder> SetMeta(IEnumerable<Meta> metaCollection)
        {
            Contract.Requires(metaCollection != null);

            var metaReadOnlyList = metaCollection.SafeToReadOnlyList();
            var metaReadOnlyListCount = metaReadOnlyList.Count;
            var domReadWriteLinkCollectionCount = this.DomReadWriteLinkCollection.Count;
            if (metaReadOnlyListCount != domReadWriteLinkCollectionCount)
            {
                var rel = this.Rel;
                var detail = ServerErrorStrings
                    .DocumentBuildExceptionDetailBuildLinkCollectionCountMismatch
                    .FormatWith(DomNodeType.Meta, domReadWriteLinkCollectionCount, rel, metaReadOnlyListCount);
                throw new DocumentBuildException(detail);
            }

            var count = domReadWriteLinkCollectionCount;
            for (var i = 0; i < count; ++i)
            {
                var domReadWriteLink = this.DomReadWriteLinkCollection[i];
                var meta = metaReadOnlyList[i];

                domReadWriteLink.SetDomReadOnlyMeta(meta);
            }

            return this;
        }

        public TParentBuilder LinkEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal LinkCollectionBuilder(TParentBuilder parentBuilder, IEnumerable<DomReadWriteLinks> domReadWriteLinksCollection, string rel)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domReadWriteLinksCollection != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.ParentBuilder = parentBuilder;

            var domReadWriteLinkCollection = domReadWriteLinksCollection
                .Select(x => x.AddDomReadWriteLink(rel))
                .ToList();
            this.DomReadWriteLinkCollection = domReadWriteLinkCollection;

            this.Rel = rel;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        private IReadOnlyList<DomReadWriteLink> DomReadWriteLinkCollection { get; set; }
        private string Rel { get; set; }
        #endregion
    }
}
