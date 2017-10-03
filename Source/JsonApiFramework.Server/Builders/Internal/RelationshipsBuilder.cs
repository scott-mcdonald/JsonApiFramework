// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal class RelationshipsBuilder<TParentBuilder, TResource> : IRelationshipsBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, Relationship relationship)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationship != null);

            if (!CanAddRelationship(predicate, this.ClrResource))
                return this;

            this.DomReadWriteRelationships.AddDomReadOnlyRelationship(rel, relationship);
            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<Relationship> relationshipCollection)
        {
            var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(relationshipDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            if (!CanAddRelationship(predicate, this.ClrResource))
                return this;

            var relationshipBuilder = this.CreateRelationshipBuilder(rel);
            var linksBuilder = relationshipBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage)
        {
            if (!CanAddRelationship(predicate, this.ClrResource))
                return this;

            var relationshipBuilder = this.CreateRelationshipBuilder(rel);

            // Links
            var linksBuilder = relationshipBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            // Data
            relationshipBuilder.SetData(toOneResourceLinkage);

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        {
            if (!CanAddRelationship(predicate, this.ClrResource))
                return this;

            var relationshipBuilder = this.CreateRelationshipBuilder(rel);

            // Links
            var linksBuilder = relationshipBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            // Data
            relationshipBuilder.SetData(toOneResourceLinkageCollection);

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IToManyResourceLinkage toManyResourceLinkage)
        {
            if (!CanAddRelationship(predicate, this.ClrResource))
                return this;

            var relationshipBuilder = this.CreateRelationshipBuilder(rel);

            // Links
            var linksBuilder = relationshipBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            // Data
            relationshipBuilder.SetData(toManyResourceLinkage);

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
        {
            if (!CanAddRelationship(predicate, this.ClrResource))
                return this;

            var relationshipBuilder = this.CreateRelationshipBuilder(rel);

            // Links
            var linksBuilder = relationshipBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            // Data
            relationshipBuilder.SetData(toManyResourceLinkageCollection);

            return this;
        }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel, Func<TResource, bool> predicate)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var clrResource = this.ClrResource;
            var canAddRelationship = predicate == null || predicate(clrResource);
            return canAddRelationship
                // NOTE: We use CreateRelationshipBuilder instead of calling this.Relationship(rel) to avoid infinite recursion.
                ? this.CreateRelationshipBuilder(rel)
                : this.CreateNullRelationshipBuilder();
        }

        public TParentBuilder RelationshipsEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNode != null);
            Contract.Requires(clrResource != null);

            this.ParentBuilder = parentBuilder;

            this.ServiceModel = serviceModel;

            var resourceType = serviceModel.GetResourceType<TResource>();
            this.ResourceType = resourceType;

            var domReadWriteRelationships = domContainerNode.GetOrAddNode(DomNodeType.Relationships, () => DomReadWriteRelationships.Create());
            this.DomReadWriteRelationships = domReadWriteRelationships;

            this.ClrResource = clrResource;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; }
        private IServiceModel ServiceModel { get; }
        private IResourceType ResourceType { get; }
        private DomReadWriteRelationships DomReadWriteRelationships { get; }
        private TResource ClrResource { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static bool CanAddRelationship(Func<TResource, bool> predicate, TResource clrResource)
        {
            var canAddRelationship = predicate == null || predicate(clrResource);
            return canAddRelationship;
        }

        private IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> CreateNullRelationshipBuilder()
        {
            var relationshipBuilder = new NullRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource>(this);
            return relationshipBuilder;
        }

        private IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> CreateRelationshipBuilder(string rel)
        {
            var relationshipBuilder = new RelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource>(this, this.ServiceModel, this.DomReadWriteRelationships, rel);
            return relationshipBuilder;
        }
        #endregion
    }
}
