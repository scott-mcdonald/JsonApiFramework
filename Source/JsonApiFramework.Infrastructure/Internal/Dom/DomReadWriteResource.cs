// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal.Dom
{
    using Attributes = JsonApiFramework.JsonApi.ApiObject;

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

        public static DomReadWriteResource Create(IServiceModel serviceModel, params Node<DomNodeType>[] domNodes)
        {
            Contract.Requires(serviceModel != null);

            var domReadWriteResource = new DomReadWriteResource(domNodes)
                {
                    ServiceModel = serviceModel
                };
            return domReadWriteResource;
        }
        #endregion

        // PRIVATE CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        private DomReadWriteResource(params Node<DomNodeType>[] domNodes)
            : base(domNodes)
        { }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IServiceModel ServiceModel { get; set; }
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

        private static Relationships GetApiResourceRelationships(NodesContainer<DomNodeType> domResource)
        {
            Contract.Requires(domResource != null);

            var domRelationshipsNode = domResource.GetNode(DomNodeType.Relationships);
            if (domRelationshipsNode == null)
                return null;

            var domRelationships = (IDomRelationships)domRelationshipsNode;
            var apiRelationships = domRelationships.Relationships;
            return apiRelationships;
        }

        private static Links GetApiResourceLinks(NodesContainer<DomNodeType> domResource)
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
            var serviceModel = this.ServiceModel ?? this.GetServiceModel();
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
            var clrResource = resourceType.CreateClrResource();

            // Map the DOM nodes to properties on the CLR resource object.
            var domResource = this;
            resourceType.MapDomResourceToClrMeta(clrResource, domResource);
            resourceType.MapDomResourceToClrId(clrResource, domResource);
            resourceType.MapDomResourceToClrAttributes(clrResource, domResource);
            resourceType.MapDomResourceToClrRelationships(clrResource, domResource);
            resourceType.MapDomResourceToClrLinks(clrResource, domResource);

            return clrResource;
        }

        private static void MapDomResourceToApiMeta(ISetMeta apiSetMeta, NodesContainer<DomNodeType> domResource)
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

        private static void MapDomResourceToApiType(ISetResourceIdentity apiSetResourceIdentity, NodesContainer<DomNodeType> domResource)
        {
            Contract.Requires(apiSetResourceIdentity != null);
            Contract.Requires(domResource != null);

            var domTypeNode = domResource.GetNode<DomNodeType, DomType>(DomNodeType.Type);
            if (domTypeNode == null)
                return;

            var apiResourceType = domTypeNode.ApiType;
            apiSetResourceIdentity.Type = apiResourceType;
        }

        private static void MapDomResourceToApiId(ISetResourceIdentity apiSetResourceIdentity, NodesContainer<DomNodeType> domResource)
        {
            Contract.Requires(apiSetResourceIdentity != null);
            Contract.Requires(domResource != null);

            var domIdNode = domResource.GetNode<DomNodeType, DomId>(DomNodeType.Id);
            if (domIdNode == null)
                return;

            var apiId = domIdNode.ApiId;
            apiSetResourceIdentity.Id = apiId;
        }

        private static void MapDomResourceToApiAttributes(ISetAttributes apiSetAttributes, NodesContainer<DomNodeType> domResource)
        {
            Contract.Requires(apiSetAttributes != null);
            Contract.Requires(domResource != null);

            var domAttributesNode = domResource.GetNode<DomNodeType, DomAttributes>(DomNodeType.Attributes);
            if (domAttributesNode == null)
                return;

            var domAttributeNodes = domAttributesNode.Nodes()
                                                     .Cast<DomAttribute>()
                                                     .ToList();
            if (!domAttributeNodes.Any())
                return;

            var apiObjectProperties = domAttributeNodes.Select(x => x.ApiAttribute)
                                                       .ToList();
            var apiAttributes = new Attributes(apiObjectProperties);
            apiSetAttributes.Attributes = apiAttributes;
        }

        private static void MapDomResourceToApiRelationships(ISetRelationships apiSetRelationships, NodesContainer<DomNodeType> domResource)
        {
            Contract.Requires(apiSetRelationships != null);
            Contract.Requires(domResource != null);

            var apiRelationships = GetApiResourceRelationships(domResource);
            apiSetRelationships.Relationships = apiRelationships;
        }

        private static void MapDomResourceToApiLinks(ISetLinks apiSetLinks, NodesContainer<DomNodeType> domResource)
        {
            Contract.Requires(apiSetLinks != null);
            Contract.Requires(domResource != null);

            var apiLinks = GetApiResourceLinks(domResource);
            apiSetLinks.Links = apiLinks;
        }
        #endregion
    }
}
