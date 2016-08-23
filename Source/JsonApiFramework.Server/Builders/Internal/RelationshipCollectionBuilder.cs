// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal class RelationshipCollectionBuilder<TParentBuilder, TResource> : IRelationshipBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            var count = this.Count;
            for (var i = 0; i < count; ++i)
            {
                var domReadWriteRelationship = this.DomReadWriteRelationshipCollection[i];

                domReadWriteRelationship.SetDomReadOnlyMeta(meta);
            }

            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(IEnumerable<Meta> metaCollection)
        {
            Contract.Requires(metaCollection != null);

            var metaReadOnlyList = metaCollection.SafeToReadOnlyList();
            var metaReadOnlyListCount = metaReadOnlyList.Count;
            var domReadWriteRelationshipCollectionCount = this.DomReadWriteRelationshipCollection.Count;
            if (metaReadOnlyListCount != domReadWriteRelationshipCollectionCount)
            {
                var rel = this.Rel;
                var detail = ServerErrorStrings
                    .DocumentBuildExceptionDetailBuildRelationshipCollectionCountMismatch
                    .FormatWith(DomNodeType.Meta, domReadWriteRelationshipCollectionCount, rel, metaReadOnlyListCount);
                throw new DocumentBuildException(detail);
            }

            var count = this.Count;
            for (var i = 0; i < count; ++i)
            {
                var domReadWriteRelationship = this.DomReadWriteRelationshipCollection[i];
                var meta = metaReadOnlyList[i];

                domReadWriteRelationship.SetDomReadOnlyMeta(meta);
            }

            return this;
        }

        public IRelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>> Links()
        {
            var linksCollectionBuilder = new RelationshipLinksCollectionBuilder<IRelationshipBuilder<TParentBuilder, TResource>>(this, this.DomReadWriteRelationshipCollection, this.Rel);
            return linksCollectionBuilder;
        }

        public TParentBuilder RelationshipEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipCollectionBuilder(TParentBuilder parentBuilder, IReadOnlyList<DomReadWriteRelationships> domReadWriteRelationshipsCollection, IReadOnlyList<TResource> clrResourceCollection, string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domReadWriteRelationshipsCollection != null);
            Contract.Requires(clrResourceCollection != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var domReadWriteRelationshipsCollectionCount = domReadWriteRelationshipsCollection.Count;
            var clrResourceCollectionCount = clrResourceCollection.Count;
            if (clrResourceCollectionCount != domReadWriteRelationshipsCollectionCount)
            {
                var detail = ServerErrorStrings
                    .InternalErrorExceptionDetailCollectionCountMismatch
                    .FormatWith("DOM read-write relationships collection", domReadWriteRelationshipsCollectionCount, "CLR resource collection", clrResourceCollectionCount);
                throw new InternalErrorException(detail);
            }

            this.ParentBuilder = parentBuilder;

            var domReadWriteRelationshipCollection = new List<DomReadWriteRelationship>();
            var clrResourceCollectionFiltered = new List<TResource>();

            var count = clrResourceCollectionCount;
            for (var i = 0; i < count; ++i)
            {
                var clrResource = clrResourceCollection[i];
                var canAddRelationship = predicate == null || predicate(clrResource);
                if (canAddRelationship == false)
                    continue;

                var domReadWriteRelationships = domReadWriteRelationshipsCollection[i];
                var domReadWriteRelationship = domReadWriteRelationships.AddDomReadWriteRelationship(rel);
                domReadWriteRelationshipCollection.Add(domReadWriteRelationship);

                clrResourceCollectionFiltered.Add(clrResource);
            }

            this.DomReadWriteRelationshipCollection = domReadWriteRelationshipCollection;
            this.ClrResourceCollection = clrResourceCollectionFiltered;

            this.Rel = rel;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        private int Count { get { return this.ClrResourceCollection.Count; } }
        private IReadOnlyList<DomReadWriteRelationship> DomReadWriteRelationshipCollection { get; set; }
        private IReadOnlyList<TResource> ClrResourceCollection { get; set; }
        private string Rel { get; set; }
        #endregion
    }
}
