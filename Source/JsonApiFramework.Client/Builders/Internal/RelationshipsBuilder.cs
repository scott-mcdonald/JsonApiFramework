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
    internal class RelationshipsBuilder<TParentBuilder, TResource> : IRelationshipsBuilder<TParentBuilder, TResource>
        where TParentBuilder : class
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IRelationshipsBuilder<TParentBuilder, TResource> Implementation
        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var apiRelationship = this.CreateApiRelationship(rel);

            this.DomReadWriteRelationships.AddDomReadOnlyRelationship(rel, apiRelationship);
            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TResourceId>(string rel, TResourceId clrResourceId)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var apiRelationship = this.CreateApiRelationship(rel, clrResourceId);

            this.DomReadWriteRelationships.AddDomReadOnlyRelationship(rel, apiRelationship);
            return this;
        }

        public IRelationshipsBuilder<TParentBuilder, TResource> AddRelationship<TResourceId>(string rel, IEnumerable<TResourceId> clrResourceIdCollection)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var apiRelationship = this.CreateApiRelationship(rel, clrResourceIdCollection);

            this.DomReadWriteRelationships.AddDomReadOnlyRelationship(rel, apiRelationship);
            return this;
        }

        public IRelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource> Relationship(string rel)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);

            var relationshipBuilder = new RelationshipBuilder<IRelationshipsBuilder<TParentBuilder, TResource>, TResource>(this, this.ServiceModel, this.DomReadWriteRelationships, rel);
            return relationshipBuilder;
        }

        public TParentBuilder RelationshipsEnd()
        {
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipsBuilder(TParentBuilder parentBuilder, IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode)
        {
            Contract.Requires(parentBuilder != null);
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNode != null);

            this.ParentBuilder = parentBuilder;

            this.ServiceModel = serviceModel;

            var resourceType = serviceModel.GetResourceType<TResource>();
            this.ResourceType = resourceType;

            var domReadWriteRelationships = domContainerNode.GetOrAddNode(DomNodeType.Relationships, () => DomReadWriteRelationships.Create());
            this.DomReadWriteRelationships = domReadWriteRelationships;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TParentBuilder ParentBuilder { get; set; }
        private IServiceModel ServiceModel { get; set; }
        private IResourceType ResourceType { get; set; }
        private DomReadWriteRelationships DomReadWriteRelationships { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private Relationship CreateApiRelationship(string rel)
        {
            // Build a JSON API relationship from the given relation name and CLR related resource identifier.
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationshipInfo(rel);

            var toCardinality = relationship.ToCardinality;
            switch (toCardinality)
            {
                case RelationshipCardinality.ToOne: return new ToOneRelationship();
                case RelationshipCardinality.ToMany: return new ToManyRelationship();

                default:
                    {
                        var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                               .FormatWith(typeof(RelationshipCardinality).Name, toCardinality);
                        throw new InternalErrorException(detail);
                    }
            }
        }

        private Relationship CreateApiRelationship<TResourceId>(string rel, TResourceId clrResourceId)
        {
            // Build a JSON API relationship from the given relation name and CLR related resource identifier.
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationshipInfo(rel);

            var toClrType = relationship.ToClrType;

            var toResourceType = this.ServiceModel.GetResourceType(toClrType);
            var toApiResourceIdentifier = toResourceType.CreateApiResourceIdentifier(clrResourceId);

            var toCardinality = relationship.ToCardinality;
            switch (toCardinality)
            {
                case RelationshipCardinality.ToOne:
                    {
                        return toApiResourceIdentifier != null
                            ? new ToOneRelationship { Data = toApiResourceIdentifier }
                            : new ToOneRelationship();
                    }

                case RelationshipCardinality.ToMany:
                    {
                        return toApiResourceIdentifier != null
                            ? new ToManyRelationship {Data = new List<ResourceIdentifier> {toApiResourceIdentifier}}
                            : new ToManyRelationship();
                    }

                default:
                    {
                        var detail = InfrastructureErrorStrings.InternalErrorExceptionDetailUnknownEnumerationValue
                                                               .FormatWith(typeof(RelationshipCardinality).Name, toCardinality);
                        throw new InternalErrorException(detail);
                    }
            }
        }

        private Relationship CreateApiRelationship<TResourceId>(string rel, IEnumerable<TResourceId> clrResourceIdCollection)
        {
            // Build a JSON API relationship from the given relation name and CLR related resource identifier collection.
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationshipInfo(rel);

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
                        var clrResourceTypeName = resourceType.ClrType.Name;
                        var detail = ClientErrorStrings.DocumentBuildExceptionDetailBuildToOneRelationshipResourceLinkageCardinalityMismatch
                                                       .FormatWith(clrResourceTypeName, rel);
                        throw new DocumentBuildException(detail);
                    }

                case RelationshipCardinality.ToMany:
                    {
                        return new ToManyRelationship
                            {
                                Data = toApiResourceIdentifierCollection
                            };
                    }

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
