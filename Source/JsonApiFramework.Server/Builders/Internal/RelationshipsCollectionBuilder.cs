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
    internal class RelationshipsCollectionBuilder<TParentBuilder, TResource> : IRelationshipsBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Relationship relationship)
        {
            var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionWithSingleObject
                                                   .FormatWith(relationshipDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, Relationship relationship)
        {
            var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionWithSingleObject
                                                   .FormatWith(relationshipDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IEnumerable<Relationship> relationshipCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationshipCollection != null);

            return this.AddRelationship(rel, default(Func<TResource, bool>), relationshipCollection);
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<Relationship> relationshipCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationshipCollection != null);

            var relationshipReadOnlyList = relationshipCollection.SafeToReadOnlyList();
            var relationshipReadOnlyListCount = relationshipReadOnlyList.Count;
            var domReadWriteRelationshipsCollectionCount = this.DomReadWriteRelationshipsCollection.Count;
            if (relationshipReadOnlyListCount != domReadWriteRelationshipsCollectionCount)
            {
                var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
                var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionCountMismatch
                                                       .FormatWith(relationshipDescription, domReadWriteRelationshipsCollectionCount, typeof(TResource).Name, relationshipReadOnlyListCount);
                throw new DocumentBuildException(detail);
            }

            var count = this.Count;
            for (var i = 0; i < count; ++i)
            {
                var clrResource = this.ClrResourceCollection[i];
                var canAddRelationship = predicate == null || predicate(clrResource);
                if (canAddRelationship == false)
                    continue;

                var domReadWriteRelationships = this.DomReadWriteRelationshipsCollection[i];
                var relationship = relationshipReadOnlyList[i];

                domReadWriteRelationships.AddDomReadOnlyRelationship(rel, relationship);
            }

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, IEnumerable<string> linkRelCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            return this.AddRelationship(rel, default(Func<TResource, bool>), linkRelCollection);
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var linkRelReadOnlyList = linkRelCollection.SafeToReadOnlyList();

            var count = this.Count;
            for (var i = 0; i < count; ++i)
            {
                var clrResource = this.ClrResourceCollection[i];
                var canAddRelationship = predicate == null || predicate(clrResource);
                if (canAddRelationship == false)
                    continue;

                var domReadWriteRelationships = this.DomReadWriteRelationshipsCollection[i];

                domReadWriteRelationships.AddDomReadWriteRelationship(rel, linkRelReadOnlyList);
            }

            return this;
        }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.Relationship(rel, default(Func<TResource, bool>));
        }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipCollectionBuilder = new RelationshipCollectionBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource>(this, this.DomReadWriteRelationshipsCollection, this.ClrResourceCollection, rel, predicate);
            return relationshipCollectionBuilder;
        }

        public TParentBuilder RelationshipsEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsCollectionBuilder(TParentBuilder parentBuilder, IReadOnlyCollection<IContainerNode<DomNodeType>> domContainerNodeCollection, IReadOnlyList<TResource> clrResourceCollection)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(domContainerNodeCollection != null);

            var domReadWriteRelationshipsCollectionCount = domContainerNodeCollection.Count;
            var clrResourceCollectionCount = clrResourceCollection.Count;
            if (clrResourceCollectionCount != domReadWriteRelationshipsCollectionCount)
            {
                var detail = ServerErrorStrings
                    .InternalErrorExceptionDetailCollectionCountMismatch
                    .FormatWith("DOM read-write relationships collection", domReadWriteRelationshipsCollectionCount, "CLR resource collection", clrResourceCollectionCount);
                throw new InternalErrorException(detail);
            }

            this.ParentBuilder = parentBuilder;

            var domReadWriteRelationshipsCollection = domContainerNodeCollection
                .Select(x => x.GetOrAddNode(DomNodeType.Relationships, () => DomReadWriteRelationships.Create()))
                .ToList();
            this.DomReadWriteRelationshipsCollection = domReadWriteRelationshipsCollection;

            this.ClrResourceCollection = clrResourceCollection;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        private int Count { get { return this.ClrResourceCollection.Count; } }
        private IReadOnlyList<DomReadWriteRelationships> DomReadWriteRelationshipsCollection { get; set; }
        private IReadOnlyList<TResource> ClrResourceCollection { get; set; }
        #endregion
    }
}
