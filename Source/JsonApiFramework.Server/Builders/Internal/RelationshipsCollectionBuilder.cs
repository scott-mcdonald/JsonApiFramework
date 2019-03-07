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
    internal class RelationshipsCollectionBuilder<TParentBuilder> : IRelationshipsBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, Relationship relationship)
        {
            var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionWithSingleObject
                                                   .FormatWith(relationshipDescription, this.ClrResourceType.Name);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<Relationship> relationshipCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationshipCollection != null);

            var relationshipReadOnlyList                 = relationshipCollection.SafeToReadOnlyList();
            var relationshipReadOnlyListCount            = relationshipReadOnlyList.Count;
            var domReadWriteRelationshipsCollectionCount = this.DomReadWriteRelationshipsCollection.Count;
            if (relationshipReadOnlyListCount != domReadWriteRelationshipsCollectionCount)
            {
                var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
                var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionCountMismatch
                                                       .FormatWith(relationshipDescription, domReadWriteRelationshipsCollectionCount, this.ClrResourceType.Name, relationshipReadOnlyListCount);
                throw new DocumentBuildException(detail);
            }

            var count = this.DomReadWriteRelationshipsCollection.Count;
            for (var i = 0; i < count; ++i)
            {
                var domReadWriteRelationships = this.DomReadWriteRelationshipsCollection[i];
                var relationship              = relationshipReadOnlyList[i];

                domReadWriteRelationships.AddDomReadOnlyRelationship(rel, relationship);
            }

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel);

            // Links
            var linksBuilder = relationshipCollectionBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }

            linksBuilder.LinksEnd();

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel);

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

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel);

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

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IToManyResourceLinkage toManyResourceLinkage)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel);

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

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel);

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

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.CreateRelationshipCollectionBuilder(rel);
        }

        public TParentBuilder RelationshipsEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected IServiceModel                            ServiceModel                        { get; }

        protected IReadOnlyList<DomReadWriteRelationships> DomReadWriteRelationshipsCollection { get; }

        protected IReadOnlyList<object> ClrResourceCollection { get; }
        #endregion

        #region Calculated Properties
        protected Type ClrResourceType => this.ResourceType.ClrType;

        protected int Count => this.ClrResourceCollection.Count;
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsCollectionBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IReadOnlyCollection<IContainerNode<DomNodeType>> domContainerNodeCollection, Type clrResourceType, IReadOnlyList<object> clrResourceCollection)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNodeCollection != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResourceCollection != null);

            this.ParentBuilder = parentBuilder;

            this.ServiceModel = serviceModel;

            var resourceType = serviceModel.GetResourceType(clrResourceType);
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
        private IResourceType  ResourceType  { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> CreateRelationshipCollectionBuilder(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipCollectionBuilder = new RelationshipCollectionBuilder<IRelationshipsBuilder<TParentBuilder>>(this, this.ServiceModel, this.DomReadWriteRelationshipsCollection, this.ClrResourceType, rel);
            return relationshipCollectionBuilder;
        }
        #endregion
    }

    internal class RelationshipsCollectionBuilder<TParentBuilder, TResource> : RelationshipsCollectionBuilder<TParentBuilder>, IRelationshipsBuilder<TParentBuilder, TResource>
        where TResource : class
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

            var relationshipReadOnlyList                 = relationshipCollection.SafeToReadOnlyList();
            var relationshipReadOnlyListCount            = relationshipReadOnlyList.Count;
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
                var clrResource        = this.ClrResourceCollectionStronglyTyped[i];
                var canAddRelationship = CanAddRelationship(predicate, clrResource);
                if (canAddRelationship == false)
                    continue;

                var domReadWriteRelationships = this.DomReadWriteRelationshipsCollection[i];
                var relationship              = relationshipReadOnlyList[i];

                domReadWriteRelationships.AddDomReadOnlyRelationship(rel, relationship);
            }

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel, predicate);

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

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel, predicate);

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

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel, predicate);

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

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel, predicate);

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

            var relationshipCollectionBuilder = this.CreateRelationshipCollectionBuilder(rel, predicate);

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

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.CreateRelationshipCollectionBuilder(rel, predicate);
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsCollectionBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IReadOnlyCollection<IContainerNode<DomNodeType>> domContainerNodeCollection, IReadOnlyList<TResource> clrResourceCollection)
            : base(parentBuilder, serviceModel, domContainerNodeCollection, typeof(TResource), clrResourceCollection)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNodeCollection != null);

            var domReadWriteRelationshipsCollectionCount = domContainerNodeCollection.Count;
            var clrResourceCollectionCount               = clrResourceCollection.Count;
            if (clrResourceCollectionCount != domReadWriteRelationshipsCollectionCount)
            {
                var detail = ServerErrorStrings
                             .InternalErrorExceptionDetailCollectionCountMismatch
                             .FormatWith("DOM read-write relationships collection", domReadWriteRelationshipsCollectionCount, "CLR resource collection", clrResourceCollectionCount);
                throw new InternalErrorException(detail);
            }
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IReadOnlyList<TResource> ClrResourceCollectionStronglyTyped => this.ClrResourceCollection.Cast<TResource>().SafeToReadOnlyList();
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static bool CanAddRelationship(Func<TResource, bool> predicate, TResource clrResource)
        {
            var canAddRelationship = predicate == null || predicate(clrResource);
            return canAddRelationship;
        }

        private IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> CreateRelationshipCollectionBuilder(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var domReadWriteRelationshipsCollectionFiltered = new List<DomReadWriteRelationships>();

            var count = this.ClrResourceCollectionStronglyTyped.Count;
            for (var i = 0; i < count; ++i)
            {
                var clrResource        = this.ClrResourceCollectionStronglyTyped[i];
                var canAddRelationship = predicate == null || predicate(clrResource);
                if (canAddRelationship == false)
                    continue;

                var domReadWriteRelationships = this.DomReadWriteRelationshipsCollection[i];
                domReadWriteRelationshipsCollectionFiltered.Add(domReadWriteRelationships);
            }

            var relationshipCollectionBuilder = new RelationshipCollectionBuilder<IRelationshipsBuilder<TParentBuilder>>(this, this.ServiceModel, domReadWriteRelationshipsCollectionFiltered, this.ClrResourceType, rel);
            return relationshipCollectionBuilder;
        }
        #endregion
    }
}
