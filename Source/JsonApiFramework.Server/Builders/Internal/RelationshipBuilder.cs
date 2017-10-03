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
    internal class RelationshipBuilder<TParentBuilder, TResource> : IRelationshipBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            this.DomReadWriteRelationship.SetDomReadOnlyMeta(meta);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(IEnumerable<Meta> metaCollection)
        {
            var rel = this.Rel;
            var detail = ServerErrorStrings.DocumentBuildExceptionDetailBuildLinkWithCollectionOfObjects
                                           .FormatWith(DomNodeType.Meta, rel);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>> Links()
        {
            var linksBuilder = new RelationshipLinksBuilder<IRelationshipBuilder<TParentBuilder, TResource>>(this, this.DomReadWriteRelationship, this.Rel);
            return linksBuilder;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetData(IToOneResourceLinkage toOneResourceLinkage)
        {
            if (toOneResourceLinkage == null || toOneResourceLinkage.HasValue == false)
            {
                this.SetDataNullOrEmpty();
                return this;
            }

            var rel = this.Rel;
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationshipInfo(rel);

            var toCardinality = relationship.ToCardinality;
            var toClrType = relationship.ToClrType;
            var toResourceType = this.ServiceModel.GetResourceType(toClrType);

            switch (toCardinality)
            {
                case RelationshipCardinality.ToOne:
                {
                    var toApiResourceIdentifier = toOneResourceLinkage.CreateApiResourceIdentifier(toResourceType);
                    this.DomReadWriteRelationship.SetDomData(toApiResourceIdentifier, toClrType);
                }
                break;

                case RelationshipCardinality.ToMany:
                {
                    var clrResourceTypeName = resourceType.ClrType.Name;
                    var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildToOneRelationshipResourceLinkageCardinalityMismatch
                                                           .FormatWith(clrResourceTypeName, rel);
                    throw new DocumentBuildException(detail);
                }

                default:
                {
                    var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                           .FormatWith(typeof(RelationshipCardinality).Name, toCardinality);
                    throw new InternalErrorException(detail);
                }
            }

            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetData(IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        {
            var rel = this.Rel;
            var toOneResourceLinkageDescription = "{0} [rel={1}]".FormatWith("ToOneResourceLinkage", rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(toOneResourceLinkageDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetData(IToManyResourceLinkage toManyResourceLinkage)
        {
            if (toManyResourceLinkage == null || toManyResourceLinkage.HasValueCollection == false)
            {
                this.SetDataNullOrEmpty();
                return this;
            }

            var rel = this.Rel;
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationshipInfo(rel);

            var toCardinality = relationship.ToCardinality;
            var toClrType = relationship.ToClrType;
            var toResourceType = this.ServiceModel.GetResourceType(toClrType);

            switch (toCardinality)
            {
                case RelationshipCardinality.ToOne:
                {
                    var clrResourceTypeName = resourceType.ClrType.Name;
                    var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildToManyRelationshipResourceLinkageCardinalityMismatch
                                                           .FormatWith(clrResourceTypeName, rel);
                    throw new DocumentBuildException(detail);
                }

                case RelationshipCardinality.ToMany:
                {
                    var toApiResourceIdentifierCollection = toManyResourceLinkage.CreateApiResourceIdentifierCollection(toResourceType);
                    this.DomReadWriteRelationship.SetDomDataCollection(toApiResourceIdentifierCollection, toClrType);
                }
                break;

                default:
                {
                    var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                           .FormatWith(typeof(RelationshipCardinality).Name, toCardinality);
                    throw new InternalErrorException(detail);
                }
            }

            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetData(IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
        {
            var rel = this.Rel;
            var toManyResourceLinkageDescription = "{0} [rel={1}]".FormatWith("ToManyResourceLinkage", rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(toManyResourceLinkageDescription, typeof(TResource).Name);
            throw new DocumentBuildException(detail);
        }

        public TParentBuilder RelationshipEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, string rel)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNode != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            this.ParentBuilder = parentBuilder;

            this.ServiceModel = serviceModel;

            var resourceType = serviceModel.GetResourceType<TResource>();
            this.ResourceType = resourceType;

            this.Rel = rel;

            var domReadWriteRelationships = (DomReadWriteRelationships)domContainerNode;
            var domReadWriteRelationship = domReadWriteRelationships.AddDomReadWriteRelationship(rel);
            this.DomReadWriteRelationship = domReadWriteRelationship;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; }
        private IServiceModel ServiceModel { get; }
        private IResourceType ResourceType { get; }
        private string Rel { get; }
        private DomReadWriteRelationship DomReadWriteRelationship { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void SetDataNullOrEmpty()
        {
            var rel = this.Rel;
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationshipInfo(rel);

            var toCardinality = relationship.ToCardinality;
            switch (toCardinality)
            {
                case RelationshipCardinality.ToOne:
                {
                    this.DomReadWriteRelationship.SetDomDataNull();
                }
                break;

                case RelationshipCardinality.ToMany:
                {
                    this.DomReadWriteRelationship.SetDomDataCollectionEmpty();
                }
                break;

                default:
                {
                    var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                           .FormatWith(typeof(RelationshipCardinality).Name, toCardinality);
                    throw new InternalErrorException(detail);
                }
            }
        }
        #endregion
    }
}
