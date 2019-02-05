// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server.Internal
{
    internal abstract class LinksCollectionBuilder<TBuilder, TParentBuilder> : ILinksBuilder<TBuilder, TParentBuilder>
        where TBuilder : class
        where TParentBuilder : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region ILinksBuilder<TBuilder, TParentBuilder> Implementation
        public abstract TBuilder AddLink(string rel, Link link);

        public TBuilder AddLink(string rel, IEnumerable<Link> linkCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkCollection != null);

            return this.AddLink(rel, linkCollection, default(IReadOnlyList<NullResource>), default(Func<NullResource, bool>));
        }

        public TBuilder AddLink(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.AddLink(rel, default(IReadOnlyList<NullResource>), default(Func<NullResource, bool>));
        }

        public ILinkBuilder<TBuilder> Link(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var linkCollectionBuilder = new LinkCollectionBuilder<TBuilder>(this.Builder, this.DomReadWriteLinksCollection, rel);
            return linkCollectionBuilder;
        }

        public TParentBuilder LinksEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected LinksCollectionBuilder(TParentBuilder parentBuilder, IEnumerable<IContainerNode<DomNodeType>> domContainerNodeCollection)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domContainerNodeCollection != null);

            this.ParentBuilder = parentBuilder;

            var domReadWriteLinksCollection = domContainerNodeCollection
                .Select(x => x.GetOrAddNode(DomNodeType.Links, () => DomReadWriteLinks.Create()))
                .ToList();

            this.DomReadWriteLinksCollection = domReadWriteLinksCollection;
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected TBuilder Builder { get; set; }
        protected IReadOnlyList<DomReadWriteLinks> DomReadWriteLinksCollection { get; private set; }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Methods
        protected TBuilder AddLink<TResource>(string rel, IEnumerable<Link> linkCollection, IReadOnlyList<TResource> clrResourceCollection, Func<TResource, bool> predicate)
            where TResource : class
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkCollection != null);

            var linkReadOnlyList = linkCollection.SafeToReadOnlyList();
            var linkReadOnlyListCount = linkReadOnlyList.Count;
            var domReadWriteLinksCollectionCount = this.DomReadWriteLinksCollection.Count;
            if (linkReadOnlyListCount != domReadWriteLinksCollectionCount)
            {
                var detail = ServerErrorStrings
                    .InternalErrorExceptionDetailCollectionCountMismatch
                    .FormatWith("DOM read-write links collection", domReadWriteLinksCollectionCount, "CLR links collection", linkReadOnlyListCount);
                throw new InternalErrorException(detail);
            }

            var count = domReadWriteLinksCollectionCount;
            for (var i = 0; i < count; ++i)
            {
                var clrResource = clrResourceCollection != null ? clrResourceCollection[i] : default(TResource);
                var canAddLink = clrResource == null || predicate == null || predicate(clrResource);
                if (canAddLink == false)
                    continue;

                var domReadWriteLinks = this.DomReadWriteLinksCollection[i];
                var link = linkReadOnlyList[i];

                domReadWriteLinks.AddDomReadOnlyLink(rel, link);
            }

            var builder = this.Builder;
            return builder;
        }

        protected TBuilder AddLink<TResource>(string rel, IReadOnlyList<TResource> clrResourceCollection, Func<TResource, bool> predicate)
            where TResource : class
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var count = this.DomReadWriteLinksCollection.Count;
            for (var i = 0; i < count; ++i)
            {
                var clrResource = clrResourceCollection != null ? clrResourceCollection[i] : default(TResource);
                var canAddLink = clrResource == null || predicate == null || predicate(clrResource);
                if (canAddLink == false)
                    continue;

                var domReadWriteLinks = this.DomReadWriteLinksCollection[i];

                domReadWriteLinks.AddDomReadWriteLink(rel);
            }

            var builder = this.Builder;
            return builder;
        }

        protected TBuilder AddLink<TResource>(string rel, Meta meta, IReadOnlyList<TResource> clrResourceCollection, Func<TResource, bool> predicate)
            where TResource : class
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(meta != null);

            var count = this.DomReadWriteLinksCollection.Count;
            for (var i = 0; i < count; ++i)
            {
                var clrResource = clrResourceCollection != null ? clrResourceCollection[i] : default(TResource);
                var canAddLink = clrResource == null || predicate == null || predicate(clrResource);
                if (canAddLink == false)
                    continue;

                var domReadWriteLinks = this.DomReadWriteLinksCollection[i];

                domReadWriteLinks.AddDomReadWriteLink(rel, meta);
            }

            var builder = this.Builder;
            return builder;
        }

        protected TBuilder AddLink<TResource>(string rel, IEnumerable<Meta> metaCollection, IReadOnlyList<TResource> clrResourceCollection, Func<TResource, bool> predicate)
            where TResource : class
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(metaCollection != null);

            var metaReadOnlyList = metaCollection.SafeToReadOnlyList();
            var metaReadOnlyListCount = metaReadOnlyList.Count;
            var domReadWriteLinksCollectionCount = this.DomReadWriteLinksCollection.Count;
            if (metaReadOnlyListCount != domReadWriteLinksCollectionCount)
            {
                var detail = ServerErrorStrings
                    .InternalErrorExceptionDetailCollectionCountMismatch
                    .FormatWith("DOM read-write links collection", domReadWriteLinksCollectionCount, "CLR meta collection", metaReadOnlyListCount);
                throw new InternalErrorException(detail);
            }

            var count = domReadWriteLinksCollectionCount;
            for (var i = 0; i < count; ++i)
            {
                var clrResource = clrResourceCollection != null ? clrResourceCollection[i] : default(TResource);
                var canAddLink = clrResource == null || predicate == null || predicate(clrResource);
                if (canAddLink == false)
                    continue;

                var domReadWriteLinks = this.DomReadWriteLinksCollection[i];
                var meta = metaReadOnlyList[i];

                domReadWriteLinks.AddDomReadWriteLink(rel, meta);
            }

            var builder = this.Builder;
            return builder;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        // ReSharper disable once ClassNeverInstantiated.Local
        private class NullResource
        { }
        #endregion
    }
}
