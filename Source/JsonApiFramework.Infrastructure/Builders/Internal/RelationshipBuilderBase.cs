// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    internal class RelationshipBuilderBase
    {
        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal RelationshipBuilderBase(IServiceModel serviceModel, IContainerNode<DomNodeType> domContainerNode, string rel, Type clrResourceType)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(domContainerNode != null);
            Contract.Requires(String.IsNullOrWhiteSpace(rel) == false);
            Contract.Requires(clrResourceType != null);

            this.ServiceModel = serviceModel;

            var resourceType = serviceModel.GetResourceType(clrResourceType);
            this.ResourceType = resourceType;

            this.Rel = rel;

            var domReadWriteRelationships = (DomReadWriteRelationships)domContainerNode;
            var domReadWriteRelationship  = domReadWriteRelationships.AddDomReadWriteRelationship(rel);
            this.DomReadWriteRelationship = domReadWriteRelationship;
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        internal void SetMetaInternal(Meta meta)
        {
            Contract.Requires(meta != null);

            this.DomReadWriteRelationship.SetDomReadOnlyMeta(meta);
        }

        internal void SetMetaInternal(IEnumerable<Meta> metaCollection)
        {
            var rel = this.Rel;
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildRelationshipWithCollectionOfObjects
                                                   .FormatWith(DomNodeType.Meta, rel);
            throw new DocumentBuildException(detail);
        }

        internal void SetDataInternal(IToOneResourceLinkage toOneResourceLinkage)
        {
            if (toOneResourceLinkage == null || toOneResourceLinkage.HasValue == false)
            {
                this.SetDataNullOrEmpty();
                return;
            }

            var rel          = this.Rel;
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationshipInfo(rel);

            var toCardinality  = relationship.ToCardinality;
            var toClrType      = relationship.ToClrType;
            var toResourceType = this.ServiceModel.GetResourceType(toClrType);

            switch (toCardinality)
            {
                case RelationshipCardinality.ToOne:
                {
                    // ReSharper disable once PossibleNullReferenceException
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
        }

        internal void SetDataInternal(IEnumerable<IToOneResourceLinkage> toOneResourceLinkageCollection)
        {
            var rel                             = this.Rel;
            var toOneResourceLinkageDescription = "{0} [rel={1}]".FormatWith("ToOneResourceLinkage", rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(toOneResourceLinkageDescription, this.ResourceType.ClrTypeName);
            throw new DocumentBuildException(detail);
        }

        internal void SetDataInternal(IToManyResourceLinkage toManyResourceLinkage)
        {
            if (toManyResourceLinkage == null || toManyResourceLinkage.HasValueCollection == false)
            {
                this.SetDataNullOrEmpty();
                return;
            }

            var rel          = this.Rel;
            var resourceType = this.ResourceType;
            var relationship = resourceType.GetRelationshipInfo(rel);

            var toCardinality  = relationship.ToCardinality;
            var toClrType      = relationship.ToClrType;
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
                    // ReSharper disable once PossibleNullReferenceException
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
        }

        internal void SetDataInternal(IEnumerable<IToManyResourceLinkage> toManyResourceLinkageCollection)
        {
            var rel                              = this.Rel;
            var toManyResourceLinkageDescription = "{0} [rel={1}]".FormatWith("ToManyResourceLinkage", rel);
            var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceWithCollectionOfObjects
                                                   .FormatWith(toManyResourceLinkageDescription, this.ResourceType.ClrTypeName);
            throw new DocumentBuildException(detail);
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region Properties
        internal string                   Rel                      { get; }
        internal DomReadWriteRelationship DomReadWriteRelationship { get; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IServiceModel ServiceModel { get; }
        private IResourceType ResourceType { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void SetDataNullOrEmpty()
        {
            var rel          = this.Rel;
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
