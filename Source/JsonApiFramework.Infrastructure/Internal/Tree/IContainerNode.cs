// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Abstracts a node that can contain other nodes.
    /// </summary>
    internal interface IContainerNode<TNodeType>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Adds a node to this container node.
        /// </summary>
        /// <param name="node">Node to add to this container node.</param>
        void Add(Node<TNodeType> node);

        /// <summary>
        /// Returns a collection of the descendant nodes for this container node, in document order.
        /// </summary>
        IEnumerable<Node<TNodeType>> DescendantNodes();

        /// <summary>
        /// Returns if this container node has any contained nodes.
        /// </summary>
        /// <returns>True if this container node has contained nodes, false otherwise.</returns>
        bool HasNodes();

        /// <summary>
        /// Returns a collection of the direct contained nodes for this container node, in document order.
        /// </summary>
        IEnumerable<Node<TNodeType>> Nodes();

        /// <summary>
        /// Remove the old node from this container node.
        /// </summary>
        /// <remarks>
        /// Note if the old node was a container of nodes, those contained nodes will be lost.
        /// </remarks>
        /// <param name="oldNode">Old node to remove.</param>
        void RemoveNode(Node<TNodeType> oldNode);

        /// <summary>
        /// Replaces the old node with the new node for this container node.
        /// </summary>
        /// <remarks>
        /// Note if the old node was a container of nodes, those contained nodes will be lost.
        /// </remarks>
        /// <param name="oldNode">Old node to replace.</param>
        /// <param name="newNode">New node to replace the old node with.</param>
        void ReplaceNode(Node<TNodeType> oldNode, Node<TNodeType> newNode);
        #endregion
    }
}