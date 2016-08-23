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
    internal static class DomExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region DomNode Extension Methods
        public static DomDocument GetDomDocument(this Node<DomNodeType> domNode)
        {
            Contract.Requires(domNode != null);

            var rootNode = domNode.RootNode;
            if (rootNode == null)
                return null;

            var domDocument = (DomDocument)rootNode;
            return domDocument;
        }

        public static IServiceModel GetServiceModel(this Node<DomNodeType> domNode)
        {
            Contract.Requires(domNode != null);

            var domDocument = domNode.GetDomDocument();
            if (domDocument == null)
                return null;

            var serviceModel = domDocument.ServiceModel;
            return serviceModel;
        }

        public static TAttribute GetSingleAttribute<TAttribute>(this Node<DomNodeType> domNode, string attributeName)
        {
            Contract.Requires(domNode != null);
            Contract.Requires(String.IsNullOrWhiteSpace(attributeName) == false);

            var nodeAttribute = domNode
                .Attributes(attributeName)
                .SingleOrDefault();
            var attributeMissing = nodeAttribute == null;
            if (attributeMissing)
            {
                var detail = InfrastructureErrorStrings.DomExceptionDetailNodeGetMissingAttribute
                                                       .FormatWith(domNode.NodeType, attributeName);
                throw new DomException(detail);
            }

            var attribute = (TAttribute)nodeAttribute.Value;
            return attribute;
        }

        public static void SetSingleAttribute<TAttribute>(this Node<DomNodeType> domNode, string attributeName, TAttribute attribute)
        {
            Contract.Requires(domNode != null);
            Contract.Requires(String.IsNullOrWhiteSpace(attributeName) == false);

            var attributeExists = domNode
                .Attributes(attributeName)
                .Any();
            if (attributeExists)
            {
                var detail = InfrastructureErrorStrings.DomExceptionDetailNodeSetExistingAttribute
                                                       .FormatWith(domNode.NodeType, attributeName);
                throw new DomException(detail);
            }

            var nodeAttribute = new NodeAttribute(attributeName, attribute);
            domNode.Add(nodeAttribute);
        }

        public static bool TryAndGetSingleAttribute<TAttribute>(this Node<DomNodeType> domNode, string attributeName, out TAttribute attribute)
        {
            Contract.Requires(domNode != null);
            Contract.Requires(String.IsNullOrWhiteSpace(attributeName) == false);

            var nodeAttribute = domNode
                .Attributes(attributeName)
                .SingleOrDefault();
            var attributeMissing = nodeAttribute == null;
            if (attributeMissing)
            {
                attribute = default(TAttribute);
                return false;
            }

            attribute = (TAttribute)nodeAttribute.Value;
            return true;
        }
        #endregion

        #region IDomResourceIdentity Extension Methods
        public static ResourceIdentifier CreateDomResourceKey(this IDomResourceIdentity domResourceIdentity)
        {
            Contract.Requires(domResourceIdentity != null);

            var apiResourceType = domResourceIdentity.ApiResourceType;
            var apiResourceId = domResourceIdentity.ApiResourceId;
            var apiResourceIdentifier = new ResourceIdentifier(apiResourceType, apiResourceId);
            return apiResourceIdentifier;
        }
        #endregion

        #region IGetIsReadOnly Extension Methods
        public static bool IsReadWrite(this IGetIsReadOnly getIsReadOnly)
        {
            Contract.Requires(getIsReadOnly != null);

            return !getIsReadOnly.IsReadOnly;
        }
        #endregion
    }
}