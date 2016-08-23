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

namespace JsonApiFramework.Client.Internal
{
    internal class RelationshipBuilder<TParentBuilder, TResource> : IRelationshipBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipBuilder<TParentBuilder, TResource> SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            this.DomReadWriteRelationship.SetDomReadOnlyMeta(meta);
            return this;
        }

        public IRelationshipBuilder<TParentBuilder, TResource> SetId()
        {
            var rel = this.Rel;
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationship(rel);

            var toCardinality = relationship.ToCardinality;
            switch (toCardinality)
            {
                case RelationshipCardinality.ToOne:
                    {
                        var domData = this.DomReadWriteRelationship.SetDomDataNull();
                    }
                    break;

                case RelationshipCardinality.ToMany:
                    {
                        var domDataCollection = this.DomReadWriteRelationship.SetDomDataCollectionEmpty();
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

        public IRelationshipBuilder<TParentBuilder, TResource> SetId<TResourceId>(TResourceId clrResourceId)
        {
            // Build a JSON API resource identifier from the given relation name and CLR related resource identifier.
            var rel = this.Rel;
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationship(rel);

            var toClrType = relationship.ToClrType;

            var toResourceType = this.ServiceModel.GetResourceType(toClrType);
            var toApiResourceIdentifier = toResourceType.CreateApiResourceIdentifier(clrResourceId);

            var toCardinality = relationship.ToCardinality;
            switch (toCardinality)
            {
                case RelationshipCardinality.ToOne:
                    {
                        var domData = toApiResourceIdentifier != null
                            ? this.DomReadWriteRelationship.SetDomData(toApiResourceIdentifier, toClrType)
                            : this.DomReadWriteRelationship.SetDomDataNull();
                    }
                    break;

                case RelationshipCardinality.ToMany:
                    {
                        var domDataCollection = toApiResourceIdentifier != null
                            ? this.DomReadWriteRelationship.SetDomDataCollection(new[] {toApiResourceIdentifier}, toClrType)
                            : this.DomReadWriteRelationship.SetDomDataCollectionEmpty();
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

        public IRelationshipBuilder<TParentBuilder, TResource> SetId<TResourceId>(IEnumerable<TResourceId> clrResourceIdCollection)
        {
            // Build a JSON API resource identifier collection from the given relation name and CLR related resource identifier collection.
            var rel = this.Rel;
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationship(rel);

            var toClrType = relationship.ToClrType;

            var toResourceType = this.ServiceModel.GetResourceType(toClrType);
            var toApiResourceIdentifierCollection = clrResourceIdCollection
                .EmptyIfNull()
                .Select(toResourceType.CreateApiResourceIdentifier)
                .Where(toApiResourceIdentifier => toApiResourceIdentifier != null)
                .ToList();

            var toCardinality = relationship.ToCardinality;
            switch (toCardinality)
            {
                case RelationshipCardinality.ToOne:
                    {
                        var clrResourceTypeName = resourceType.ClrResourceType.Name;
                        var detail = ClientErrorStrings.DocumentBuildExceptionDetailBuildToOneRelationshipResourceLinkageCardinalityMismatch
                                                       .FormatWith(clrResourceTypeName, rel);
                        throw new DocumentBuildException(detail);
                    }

                case RelationshipCardinality.ToMany:
                    {
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
        private TParentBuilder ParentBuilder { get; set; }
        private IServiceModel ServiceModel { get; set; }
        private IResourceType ResourceType { get; set; }
        private string Rel { get; set; }
        private DomReadWriteRelationship DomReadWriteRelationship { get; set; }
        #endregion
    }
}
