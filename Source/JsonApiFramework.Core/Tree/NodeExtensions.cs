// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Tree
{
    /// <summary>
    /// Extension methods for any object that implements the <c>INode</c>
    /// interface or <c>Node</c> class.
    /// </summary>
    public static class NodeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static TValue GetAttributeValue<TValue>(this INode node, string attributeName)
        {
            Contract.Requires(node != null);
            Contract.Requires(String.IsNullOrWhiteSpace(attributeName) == false);

            var attribute = (NodeAttribute<TValue>)node.Attributes().Single(x => x.Name == attributeName);
            var attributeValue = attribute.Value;
            return attributeValue;
        }

        public static bool TryGetAttributeValue<TValue>(this INode node, string attributeName, out TValue attributeValue)
        {
            Contract.Requires(node != null);
            Contract.Requires(String.IsNullOrWhiteSpace(attributeName) == false);

            var attribute = (NodeAttribute<TValue>)node.Attributes().SingleOrDefault(x => x.Name == attributeName);
            if (attribute == null)
            {
                attributeValue = default(TValue);
                return false;
            }

            attributeValue = attribute.Value;
            return true;
        }

        public static void RemoveAttributeValue(this Node node, string attributeName)
        {
            Contract.Requires(node != null);
            Contract.Requires(String.IsNullOrWhiteSpace(attributeName) == false);

            var oldAttribute = node.Attributes().SingleOrDefault(x => x.Name == attributeName);
            if (oldAttribute == null)
                return;

            node.RemoveAttribute(oldAttribute);
        }

        public static void SetAttributeValue<TValue>(this Node node, string attributeName, TValue attributeValue)
        {
            Contract.Requires(node != null);
            Contract.Requires(String.IsNullOrWhiteSpace(attributeName) == false);

            var newAttribute = new NodeAttribute<TValue>(attributeName, attributeValue);
            var oldAttribute = (NodeAttribute<TValue>)node.Attributes().SingleOrDefault(x => x.Name == attributeName);
            if (oldAttribute == null)
            {
                node.AddAttribute(newAttribute);
            }
            else
            {
                node.ReplaceAttribute(oldAttribute, newAttribute);
            }
        }
        #endregion
    }
}
