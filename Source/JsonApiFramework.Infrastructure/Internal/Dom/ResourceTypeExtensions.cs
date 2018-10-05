// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal.Dom
{
    internal static class ResourceTypeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static void MapClrTypeToDomResource(this IResourceType resourceType, DomReadWriteResource domResource)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(domResource != null);

            domResource.CreateAndAddNode(() => DomType.CreateFromResourceType(resourceType));
        }

        public static void MapClrIdToDomResource(this IResourceType resourceType, DomReadWriteResource domResource, object clrResource)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(domResource != null);

            if (resourceType.IsSingleton())
                return;

            if (clrResource == null)
                return;

            domResource.CreateAndAddNode(() => DomId.CreateFromClrResource(resourceType, clrResource));
        }

        public static void MapClrAttributesToDomResource(this IResourceType resourceType, DomReadWriteResource domResource, object clrResource)
        {
            resourceType.MapClrAttributesToDomResource(domResource, clrResource, null);
        }

        public static void MapClrAttributesToDomResource(this IResourceType resourceType, DomReadWriteResource domResource, object clrResource, Func<string, string, bool> attributePredicate)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(domResource != null);

            if (clrResource == null)
                return;

            var serviceModel = domResource.GetServiceModel();
            var domAttributes = domResource.CreateAndAddNode(() => DomAttributes.Create());

            var apiType = resourceType.ResourceIdentityInfo.ApiType;
            var attributeInfoCollection = resourceType.AttributesInfo.Collection;
            foreach (var attributeInfo in attributeInfoCollection)
            {
                var apiField = attributeInfo.ApiPropertyName;
                if (attributePredicate != null && attributePredicate(apiType, apiField) == false)
                {
                    // Skip adding this attribute.
                    continue;
                }

                var localAttributeInfo = attributeInfo;
                domAttributes.CreateAndAddNode(() => DomAttribute.CreateFromClrResource(serviceModel, localAttributeInfo, clrResource));
            }
        }

        public static void MapClrAttributeToDomAttributes(this IResourceType resourceType, DomAttributes domAttributes, string clrAttributeName, object clrAttribute)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(domAttributes != null);
            Contract.Requires(String.IsNullOrWhiteSpace(clrAttributeName) == false);

            if (clrAttribute == null)
                return;

            var serviceModel = domAttributes.GetServiceModel();
            var attribute = resourceType.GetClrAttributeInfo(clrAttributeName);
            var domAttribute = DomAttribute.CreateFromClrAttribute(serviceModel, attribute, clrAttribute);
            if (domAttribute == null)
                return;

            domAttributes.Add(domAttribute);
        }

        public static void MapDomResourceToClrMeta(this IResourceType resourceType, object clrResource, DomReadWriteResource domResource)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(domResource != null);

            var domMetaNode = domResource.GetNode(DomNodeType.Meta);
            if (domMetaNode == null)
                return;

            var domMeta = (IDomMeta)domMetaNode;
            var clrMeta = domMeta.Meta;
            resourceType.SetClrMeta(clrResource, clrMeta);
        }

        public static void MapDomResourceToClrId(this IResourceType resourceType, object clrResource, DomReadWriteResource domResource)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(domResource != null);

            var domIdNode = domResource.GetNode<DomNodeType, DomId>(DomNodeType.Id);
            if (domIdNode == null)
                return;

            var clrId = domIdNode.ClrId;
            resourceType.SetClrId(clrResource, clrId);
        }

        public static void MapDomResourceToClrAttributes(this IResourceType resourceType, object clrResource, DomReadWriteResource domResource)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(domResource != null);

            var domAttributesNode = domResource.GetNode<DomNodeType, DomAttributes>(DomNodeType.Attributes);
            if (domAttributesNode == null)
                return;

            var domAttributeNodes = domAttributesNode.Nodes()
                                                     .Cast<DomAttribute>()
                                                     .ToList();
            foreach (var domAttributeNode in domAttributeNodes)
            {
                var clrPropertyName = domAttributeNode.ClrPropertyName;
                var clrPropertyValue = domAttributeNode.ClrAttribute;

                var clrAttribute = resourceType.GetClrAttributeInfo(clrPropertyName);
                clrAttribute.SetClrProperty(clrResource, clrPropertyValue);
            }
        }

        public static void MapDomResourceToClrRelationships(this IResourceType resourceType, object clrResource, DomReadWriteResource domResource)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(domResource != null);

            var domRelationshipsNode = domResource.GetNode(DomNodeType.Relationships);
            if (domRelationshipsNode == null)
                return;

            var domRelationships = (IDomRelationships)domRelationshipsNode;
            var clrRelationships = domRelationships.Relationships;
            resourceType.SetClrRelationships(clrResource, clrRelationships);
        }

        public static void MapDomResourceToClrLinks(this IResourceType resourceType, object clrResource, DomReadWriteResource domResource)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(domResource != null);

            var domLinksNode = domResource.GetNode(DomNodeType.Links);
            if (domLinksNode == null)
                return;

            var domLinks = (IDomLinks)domLinksNode;
            var clrLinks = domLinks.Links;
            resourceType.SetClrLinks(clrResource, clrLinks);
        }
        #endregion
    }
}
