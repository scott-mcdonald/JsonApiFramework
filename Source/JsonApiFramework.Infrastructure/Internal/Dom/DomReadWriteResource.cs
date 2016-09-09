// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Internal.Dom
{
    /// <summary>
    /// Represents a read/write resource node in the DOM tree.
    /// </summary>
    internal class DomReadWriteResource : DomReadWriteResourceIdentity
        , IDomResource
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region DomNode Overrides
        public override DomNodeType NodeType
        { get { return DomNodeType.Resource; } }

        public override string Name
        { get { return "ReadWriteResource"; } }
        #endregion

        #region IDomResource Implementation
        public Resource ApiResource
        { get { return this.GetApiResource(); } }

        public Relationships ApiResourceRelationships
        { get { return GetApiResourceRelationships(this); } }

        public Links ApiResourceLinks
        { get { return GetApiResourceLinks(this); } }

        public object ClrResource
        { get { return this.GetClrResource(); } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static DomReadWriteResource Create(params Node<DomNodeType>[] domNodes)
        {
            var domReadWriteResource = new DomReadWriteResource(domNodes);
            return domReadWriteResource;
        }
        #endregion

        // PRIVATE CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        private DomReadWriteResource(params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        { }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private Resource GetApiResource()
        {
            // Create JsonApi resource object.
            var apiResource = new Resource();

            // Map the DOM nodes to properties on the JsonApi resource object.
            var domResource = this;
            MapDomResourceToApiMeta(apiResource, domResource);
            MapDomResourceToApiType(apiResource, domResource);
            MapDomResourceToApiId(apiResource, domResource);
            MapDomResourceToApiAttributes(apiResource, domResource);
            MapDomResourceToApiRelationships(apiResource, domResource);
            MapDomResourceToApiLinks(apiResource, domResource);

            return apiResource;
        }

        private static Relationships GetApiResourceRelationships(DomReadWriteResource domResource)
        {
            Contract.Requires(domResource != null);

            var domRelationshipsNode = domResource.GetNode(DomNodeType.Relationships);
            if (domRelationshipsNode == null)
                return null;

            var domRelationships = (IDomRelationships)domRelationshipsNode;
            var apiRelationships = domRelationships.Relationships;
            return apiRelationships;
        }

        private static Links GetApiResourceLinks(DomReadWriteResource domResource)
        {
            Contract.Requires(domResource != null);

            var domLinksNode = domResource.GetNode(DomNodeType.Links);
            if (domLinksNode == null)
                return null;

            var domLinks = (IDomLinks)domLinksNode;
            var apiLinks = domLinks.Links;
            return apiLinks;
        }

        private object GetClrResource()
        {
            // Get the service model for this DOM tree.
            var serviceModel = this.GetServiceModel();
            if (serviceModel == null)
            {
                var detail = InfrastructureErrorStrings.DomExceptionDetailNodeSetExistingAttribute
                                                       .FormatWith(this.NodeType);
                throw new DomException(detail);
            }

            // Find metadata based on the JsonApi resource.
            var apiResourceType = this.ApiResourceType;
            if (String.IsNullOrWhiteSpace(apiResourceType))
                return null;

            var resourceType = serviceModel.GetResourceType(apiResourceType);

            // Create CLR resource object.
            var clrResource = resourceType.CreateClrObject();

            // Map the DOM nodes to properties on the CLR resource object.
            var domResource = this;
            resourceType.MapDomResourceToClrMeta(clrResource, domResource);
            resourceType.MapDomResourceToClrId(clrResource, domResource);
            resourceType.MapDomResourceToClrAttributes(clrResource, domResource);
            resourceType.MapDomResourceToClrRelationships(clrResource, domResource);
            resourceType.MapDomResourceToClrLinks(clrResource, domResource);

            return clrResource;
        }

        private static void MapDomResourceToApiMeta(ISetMeta apiSetMeta, DomReadWriteResource domResource)
        {
            Contract.Requires(apiSetMeta != null);
            Contract.Requires(domResource != null);

            var domMetaNode = domResource.GetNode(DomNodeType.Meta);
            if (domMetaNode == null)
                return;

            var domMeta = (IDomMeta)domMetaNode;
            var apiMeta = domMeta.Meta;
            apiSetMeta.Meta = apiMeta;
        }

        private static void MapDomResourceToApiType(ISetResourceIdentity apiSetResourceIdentity, DomReadWriteResource domResource)
        {
            Contract.Requires(apiSetResourceIdentity != null);
            Contract.Requires(domResource != null);

            var domTypeNode = domResource.GetNode<DomNodeType, DomType>(DomNodeType.Type);
            if (domTypeNode == null)
                return;

            var apiResourceType = domTypeNode.ApiType;
            apiSetResourceIdentity.Type = apiResourceType;
        }

        private static void MapDomResourceToApiId(ISetResourceIdentity apiSetResourceIdentity, DomReadWriteResource domResource)
        {
            Contract.Requires(apiSetResourceIdentity != null);
            Contract.Requires(domResource != null);

            var domIdNode = domResource.GetNode<DomNodeType, DomId>(DomNodeType.Id);
            if (domIdNode == null)
                return;

            var apiId = domIdNode.ApiId;
            apiSetResourceIdentity.Id = apiId;
        }

        private static void MapDomResourceToApiAttributes(ISetAttributes apiSetAttributes, DomReadWriteResource domResource)
        {
            Contract.Requires(apiSetAttributes != null);
            Contract.Requires(domResource != null);

            var domAttributesNode = domResource.GetNode<DomNodeType, DomAttributes>(DomNodeType.Attributes);
            if (domAttributesNode == null)
                return;

            var domAttributeCollection = domAttributesNode.Nodes()
                                                          .Cast<DomAttribute>()
                                                          .ToList();
            if (!domAttributeCollection.Any())
                return;

            var apiObject = CreateApiObjectFromDomAttributeCollection(domAttributeCollection);
            apiSetAttributes.Attributes = apiObject;
        }

        private static ApiObject CreateApiObjectFromDomAttributeCollection(IEnumerable<DomAttribute> domAttributeCollection)
        {
            Contract.Requires(domAttributeCollection != null);

            var apiProperties = domAttributeCollection
                .Select(domAttribute =>
                    {
                        if (!domAttribute.HasNodes())
                        {
                            // Simple attribute, has no children.
                            return domAttribute.ApiAttribute;
                        }

                        // Complex attribute or collection of Complex attribute
                        var isCollection = domAttribute.FirstNode.NodeType == DomNodeType.Index;
                        if (isCollection)
                        {
                            // Collection of Complex attributes, has children of DomIndex.
                            var domIndexCollection = domAttribute.Nodes()
                                                                 .Cast<DomIndex>()
                                                                 .ToList();

                            var apiObjectArray = domIndexCollection.Select(CreateApiObjectFromDomIndex)
                                                                   .ToArray();

                            var apiPropertyName = domAttribute.ApiPropertyName;
                            var apiProperty = ApiProperty.Create(apiPropertyName, apiObjectArray);
                            return apiProperty;
                        }

                        // Complex attribute, children are DomAttribute nodes.
                        return CreateApiPropertyFromDomAttribute(domAttribute);
                    })
                .ToList();

            var apiObject = new ApiObject(apiProperties);
            return apiObject;
        }

        private static ApiObject CreateApiObjectFromDomIndex(DomIndex domIndex)
        {
            Contract.Requires(domIndex != null);

            var domAttributeCollection = domIndex.Nodes()
                                                 .Cast<DomAttribute>()
                                                 .ToList();
            var apiObject = CreateApiObjectFromDomAttributeCollection(domAttributeCollection);
            return apiObject;
        }

        private static ApiProperty CreateApiPropertyFromDomAttribute(DomAttribute domAttribute)
        {
            Contract.Requires(domAttribute != null);

            var apiPropertyName = domAttribute.ApiPropertyName;
            var domAttributeCollection = domAttribute.Nodes()
                                                     .Cast<DomAttribute>()
                                                     .ToList();
            var apiObject = CreateApiObjectFromDomAttributeCollection(domAttributeCollection);
            var apiProperty = ApiProperty.Create(apiPropertyName, apiObject);
            return apiProperty;
        }

        private static void MapDomResourceToApiRelationships(ISetRelationships apiSetRelationships, DomReadWriteResource domResource)
        {
            Contract.Requires(apiSetRelationships != null);
            Contract.Requires(domResource != null);

            var apiRelationships = GetApiResourceRelationships(domResource);
            apiSetRelationships.Relationships = apiRelationships;
        }

        private static void MapDomResourceToApiLinks(ISetLinks apiSetLinks, DomReadWriteResource domResource)
        {
            Contract.Requires(apiSetLinks != null);
            Contract.Requires(domResource != null);

            var apiLinks = GetApiResourceLinks(domResource);
            apiSetLinks.Links = apiLinks;
        }
        #endregion
    }
}
