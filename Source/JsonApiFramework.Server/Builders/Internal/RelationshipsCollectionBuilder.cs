// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal class RelationshipsCollectionBuilder<TParentBuilder, TResource> : IRelationshipsBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, Relationship relationship)
        {
            var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionWithSingleObject
                                                   .FormatWith(relationshipDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
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
                var canAddRelationship = CanAddRelationship(predicate, clrResource);
                if (canAddRelationship == false)
                    continue;

                var domReadWriteRelationships = this.DomReadWriteRelationshipsCollection[i];
                var relationship = relationshipReadOnlyList[i];

                domReadWriteRelationships.AddDomReadOnlyRelationship(rel, relationship);
            }

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.Relationship(rel, predicate);

            // Links
            var linksBuilder = relationshipCollectionBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.Relationship(rel, predicate);

            // Links
            var linksBuilder = relationshipCollectionBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            // Data
            relationshipCollectionBuilder.SetData(toOneResourceLinkage);

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.Relationship(rel, predicate);

            // Links
            var linksBuilder = relationshipCollectionBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            // Data
            relationshipCollectionBuilder.SetData(toOneResourceLinkageCollection);

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IToManyResourceLinkage toManyResourceLinkage)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.Relationship(rel, predicate);

            // Links
            var linksBuilder = relationshipCollectionBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            // Data
            relationshipCollectionBuilder.SetData(toManyResourceLinkage);

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.Relationship(rel, predicate);

            // Links
            var linksBuilder = relationshipCollectionBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            // Data
            relationshipCollectionBuilder.SetData(toManyResourceLinkageCollection);

            return this;
        }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipCollectionBuilder = new RelationshipCollectionBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource>(this, this.ServiceModel, this.DomReadWriteRelationshipsCollection, this.ClrResourceCollection, rel, predicate);
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
        internal RelationshipsCollectionBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IReadOnlyCollection<IContainerNode<DomNodeType>> domContainerNodeCollection, IReadOnlyList<TResource> clrResourceCollection)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
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

            this.ServiceModel = serviceModel;

            var resourceType = serviceModel.GetResourceType<TResource>();
            this.ResourceType = resourceType;

            var domReadWriteRelationshipsCollection = domContainerNodeCollection
                .Select(x => x.GetOrAddNode(DomNodeType.Relationships, () => DomReadWriteRelationships.Create()))
                .ToList();
            this.DomReadWriteRelationshipsCollection = domReadWriteRelationshipsCollection;

            this.ClrResourceCollection = clrResourceCollection;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; }
        private IServiceModel ServiceModel { get; }
        private IResourceType ResourceType { get; }
        private int Count => this.ClrResourceCollection.Count;
        private IReadOnlyList<DomReadWriteRelationships> DomReadWriteRelationshipsCollection { get; }
        private IReadOnlyList<TResource> ClrResourceCollection { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static bool CanAddRelationship(Func<TResource, bool> predicate, TResource clrResource)
        {
            var canAddRelationship = predicate == null || predicate(clrResource);
            return canAddRelationship;
        }
        #endregion
    }
}
