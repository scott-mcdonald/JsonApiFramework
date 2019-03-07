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
    internal class RelationshipsBuilder<TParentBuilder> : IRelationshipsBuilder<TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, Relationship relationship)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationship != null);

            this.DomReadWriteRelationships.AddDomReadOnlyRelationship(rel, relationship);
            return this;
        }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<Relationship> relationshipCollection)
        {
            var relationshipDescription = "{0} [rel={1}]".FormatWith(DomNodeType.Relationship, rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(relationshipDescription, this.ClrResourceType.Name);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(linkRelCollection != null);

            var relationshipBuilder = this.CreateRelationshipBuilder(rel);
            var linksBuilder        = relationshipBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }

            linksBuilder.LinksEnd();

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage)
        {
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

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        {
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

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IToManyResourceLinkage toManyResourceLinkage)
        {
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

        public IRelationshipsBuilder<TParentBuilder> AddRelationship(string rel, IEnumerable<string> linkRelCollection, IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
        {
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

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            return this.CreateRelationshipBuilder(rel);
        }

        public TParentBuilder RelationshipsEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, object clrResource)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNode != null);
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrResource != null);

            this.ParentBuilder = parentBuilder;

            this.ServiceModel = serviceModel;

            var resourceType = serviceModel.GetResourceType(clrResourceType);
            this.ResourceType = resourceType;

            var domReadWriteRelationships = domContainerNode.GetOrAddNode(DomNodeType.Relationships, () => DomReadWriteRelationships.Create());
            this.DomReadWriteRelationships = domReadWriteRelationships;

            this.ClrResource = clrResource;
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected IServiceModel ServiceModel { get; }
        protected DomReadWriteRelationships DomReadWriteRelationships { get; }
        protected object ClrResource { get; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder            ParentBuilder             { get; }
        
        private IResourceType             ResourceType              { get; }
        #endregion

        #region Calculated Properties
        private Type ClrResourceType => this.ResourceType.ClrType;
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> CreateRelationshipBuilder(string rel)
        {
            var relationshipBuilder = new RelationshipBuilder<IRelationshipsBuilder<TParentBuilder>>(this, this.ServiceModel, this.DomReadWriteRelationships, rel, this.ClrResourceType);
            return relationshipBuilder;
        }
        #endregion
    }

    internal class RelationshipsBuilder<TParentBuilder, TResource> : RelationshipsBuilder<TParentBuilder>, IRelationshipsBuilder<TParentBuilder, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, Relationship relationship)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(relationship != null);

            if (!CanAddRelationship(predicate, this.ClrResourceStronglyTyped))
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

            if (!CanAddRelationship(predicate, this.ClrResourceStronglyTyped))
                return this;

            var relationshipBuilder = this.CreateRelationshipBuilder(rel);
            var linksBuilder        = relationshipBuilder.Links();
            foreach (var linkRel in linkRelCollection)
            {
                linksBuilder.AddLink(linkRel);
            }
            linksBuilder.LinksEnd();

            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel, Func<TResource, bool> predicate, IEnumerable<string> linkRelCollection, IToOneResourceLinkage toOneResourceLinkage)
        {
            if (!CanAddRelationship(predicate, this.ClrResourceStronglyTyped))
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
            if (!CanAddRelationship(predicate, this.ClrResourceStronglyTyped))
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
            if (!CanAddRelationship(predicate, this.ClrResourceStronglyTyped))
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
            if (!CanAddRelationship(predicate, this.ClrResourceStronglyTyped))
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

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> Relationship(string rel, Func<TResource, bool> predicate)
        {
            var canAddRelationship = CanAddRelationship(predicate, this.ClrResourceStronglyTyped);
            return canAddRelationship
                // NOTE: We use CreateRelationshipBuilder instead of calling this.Relationship(rel) to avoid infinite recursion.
                ? this.CreateRelationshipBuilder(rel)
                : this.CreateNullRelationshipBuilder();
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, TResource clrResource)
            : base(parentBuilder, serviceModel, domContainerNode, typeof(TResource), clrResource)
        { }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TResource ClrResourceStronglyTyped => (TResource)this.ClrResource;
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static bool CanAddRelationship(Func<TResource, bool> predicate, TResource clrResource)
        {
            var canAddRelationship = predicate == null || predicate(clrResource);
            return canAddRelationship;
        }

        private IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> CreateNullRelationshipBuilder()
        {
            var relationshipBuilder = new NullRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>>(this);
            return relationshipBuilder;
        }

        private IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder>> CreateRelationshipBuilder(string rel)
        {
            var relationshipBuilder = new RelationshipBuilder<IRelationshipsBuilder<TParentBuilder>>(this, this.ServiceModel, this.DomReadWriteRelationships, rel, typeof(TResource));
            return relationshipBuilder;
        }
        #endregion
    }
}
