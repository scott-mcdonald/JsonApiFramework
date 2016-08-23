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
    internal class ResourceLinksCollectionBuilder<TParentBuilder, TResource> : LinksCollectionBuilder<IResourceLinksBuilder<TParentBuilder, TResource>, TParentBuilder>, IResourceLinksBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region LinksCollectionBuilder<TBuilder, TParentBuilder> Overrides
        public override IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Link link)
        {
            var linkDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Link, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionWithSingleObject
                                                   .FormatWith(linkDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }
        #endregion

        #region IResourceLinksBuilder<TParentBuilder, TResource>
        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, Link link)
        {
            var linkDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Link, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionWithSingleObject
                                                   .FormatWith(linkDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate, IEnumerable<Link> linkCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkCollection != null);

            var clrResourceCollection = this.ClrResourceCollection;
            return this.AddLink(rel, linkCollection, clrResourceCollection, predicate);
        }

        public IResourceLinksBuilder<TParentBuilder, TResource> AddLink(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var clrResourceCollection = this.ClrResourceCollection;
            return this.AddLink(rel, clrResourceCollection, predicate);
        }

        public ILinkBuilder<IResourceLinksBuilder<TParentBuilder, TResource>> Link(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var domReadWriteLinksCollectionCount = this.DomReadWriteLinksCollection.Count;
            var clrResourceCollectionCount = this.ClrResourceCollection.Count;
            if (clrResourceCollectionCount != domReadWriteLinksCollectionCount)
            {
                var detail = ServerErrorStrings
                    .InternalErrorExceptionDetailCollectionCountMismatch
                    .FormatWith("DOM read-write links collection", domReadWriteLinksCollectionCount, "CLR resource collection", clrResourceCollectionCount);
                throw new InternalErrorException(detail);
            }

            var domReadWriteLinksCollectionFiltered = new List<DomReadWriteLinks>();
            var count = clrResourceCollectionCount;
            for (var i = 0; i < count; ++i)
            {
                var clrResource = this.ClrResourceCollection[i];
                var canAddLink = predicate == null || predicate(clrResource);
                if (canAddLink == false)
                    continue;

                var domReadWriteLinks = this.DomReadWriteLinksCollection[i];
                domReadWriteLinksCollectionFiltered.Add(domReadWriteLinks);
            }

            var linkBuilder = new LinkCollectionBuilder<IResourceLinksBuilder<TParentBuilder, TResource>>(this.Builder, domReadWriteLinksCollectionFiltered, rel);
            return linkBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        internal ResourceLinksCollectionBuilder(TParentBuilder parentBuilder, IEnumerable<IContainerNode<DomNodeType>> domContainerNode, IReadOnlyList<TResource> clrResourceCollection)
            : base(parentBuilder, domContainerNode)
        {
            Contract.Requires(clrResourceCollection != null);

            this.Builder = this;
            this.ClrResourceCollection = clrResourceCollection;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IReadOnlyList<TResource> ClrResourceCollection { get; set; }
        #endregion
    }
}
