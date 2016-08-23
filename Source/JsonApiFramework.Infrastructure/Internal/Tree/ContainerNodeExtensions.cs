// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Extension methods for the <c>IContainerNode</c> abstraction.
    /// </summary>
    internal static class ContainerNodeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static TNode CreateAndAddNode<TNodeType, TNode>(this IContainerNode<TNodeType> containerNode)
            where TNode : Node<TNodeType>, new()
        {
            Contract.Requires(containerNode != null);

            return containerNode.CreateAndAddNode(() => new TNode());
        }

        public static TNode CreateAndAddNode<TNodeType, TNode>(this IContainerNode<TNodeType> containerNode, Func<TNode> nodeFactoryMethod)
            where TNode : Node<TNodeType>
        {
            Contract.Requires(containerNode != null);
            Contract.Requires(nodeFactoryMethod != null);

            // Create and add new node to the container node.
            var newNode = nodeFactoryMethod();
            containerNode.Add(newNode);
            return newNode;
        }

        public static bool ContainsNode<TNodeType>(this IContainerNode<TNodeType> containerNode, TNodeType nodeType)
        {
            Contract.Requires(containerNode != null);

            return containerNode.ContainsNode(nodeType, Node<TNodeType>.DefaultNodeTypeEqualityComarer);
        }

        public static bool ContainsNode<TNodeType>(this IContainerNode<TNodeType> containerNode, TNodeType nodeType, IEqualityComparer<TNodeType> nodeTypeEqualityComparer)
        {
            Contract.Requires(containerNode != null);
            Contract.Requires(nodeTypeEqualityComparer != null);

            var containsNode = containerNode.Nodes()
                                            .Any(x => nodeTypeEqualityComparer.Equals(x.NodeType, nodeType));
            return containsNode;
        }

        public static Node<TNodeType> GetNode<TNodeType>(this IContainerNode<TNodeType> containerNode, TNodeType nodeType)
        {
            Contract.Requires(containerNode != null);

            return containerNode.GetNode<TNodeType>(nodeType, Node<TNodeType>.DefaultNodeTypeEqualityComarer);
        }

        public static Node<TNodeType> GetNode<TNodeType>(this IContainerNode<TNodeType> containerNode, TNodeType nodeType, IEqualityComparer<TNodeType> nodeTypeEqualityComparer)
        {
            Contract.Requires(containerNode != null);

            var node = containerNode.Nodes()
                                    .SingleOrDefault(x => nodeTypeEqualityComparer.Equals(x.NodeType, nodeType));
            return node;
        }

        public static TNode GetNode<TNodeType, TNode>(this IContainerNode<TNodeType> containerNode, TNodeType nodeType)
            where TNode : Node<TNodeType>
        {
            Contract.Requires(containerNode != null);

            var node = containerNode.GetNode(nodeType);
            return node != null ? (TNode)node : null;
        }

        public static TNode GetNode<TNodeType, TNode>(this IContainerNode<TNodeType> containerNode, TNodeType nodeType, IEqualityComparer<TNodeType> nodeTypeEqualityComparer)
            where TNode : Node<TNodeType>
        {
            Contract.Requires(containerNode != null);

            var node = containerNode.GetNode(nodeType, nodeTypeEqualityComparer);
            return node != null ? (TNode)node : null;
        }

        public static TNode GetOrAddNode<TNodeType, TNode>(this IContainerNode<TNodeType> containerNode, TNodeType nodeType)
            where TNode : Node<TNodeType>, new()
        {
            Contract.Requires(containerNode != null);

            return containerNode.GetOrAddNode<TNodeType, TNode>(nodeType, () => new TNode(), Node<TNodeType>.DefaultNodeTypeEqualityComarer);
        }

        public static TNode GetOrAddNode<TNodeType, TNode>(this IContainerNode<TNodeType> containerNode, TNodeType nodeType, Func<TNode> nodeFactoryMethod)
            where TNode : Node<TNodeType>
        {
            Contract.Requires(containerNode != null);
            Contract.Requires(nodeFactoryMethod != null);

            return containerNode.GetOrAddNode<TNodeType, TNode>(nodeType, nodeFactoryMethod, Node<TNodeType>.DefaultNodeTypeEqualityComarer);
        }

        public static TNode GetOrAddNode<TNodeType, TNode>(this IContainerNode<TNodeType> containerNode, TNodeType nodeType, Func<TNode> nodeFactoryMethod, IEqualityComparer<TNodeType> nodeTypeEqualityComparer)
            where TNode : Node<TNodeType>
        {
            Contract.Requires(containerNode != null);
            Contract.Requires(nodeFactoryMethod != null);
            Contract.Requires(nodeTypeEqualityComparer != null);

            // Try and get an existing node from the container node.
            var existingNode = containerNode.GetNode<TNodeType, TNode>(nodeType, nodeTypeEqualityComparer);
            if (existingNode != null)
                return existingNode;

            // Add new node to the container node.
            var newNode = nodeFactoryMethod();
            containerNode.Add(newNode);
            return newNode;
        }
        #endregion
    }
}