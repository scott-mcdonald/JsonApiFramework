// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Abstracts the concept of a node that can only contain one node in a tree.
    /// </summary>
    internal abstract class NodeContainer<TNodeType> : Node<TNodeType>
        , IContainerNode<TNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the only node of this node container.</summary>
        public Node<TNodeType> Node { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Node Overrides
        public override void Accept(NodeVisitor<TNodeType> nodeVisitor, int depth)
        {
            Contract.Requires(nodeVisitor != null);
            Contract.Requires(depth >= 0);

            var visitResult = nodeVisitor.Visit(this, depth);
            if (visitResult == VisitResult.Done)
                return;

            if (this.Node != null)
            {
                switch (visitResult)
                {
                    case VisitResult.Continue:
                        this.Node.Accept(nodeVisitor, depth + 1);
                        break;
                }
            }

            if (this.NextNode == null)
                return;

            switch (visitResult)
            {
                case VisitResult.Continue:
                case VisitResult.ContinueWithSiblingNodesOnly:
                    this.NextNode.Accept(nodeVisitor, depth);
                    break;
            }
        }
        #endregion

        #region IContainerNode<T> Implementation
        public void Add(Node<TNodeType> node)
        {
            if (node == null)
                return;

            // Validate node has not already been added to the object tree.
            node.ValidateHasNotBeenAdded();

            // Add node to the object graph.
            node.RootNode = this.RootNode;
            node.ParentNode = this;
            node.NextNode = null;
            node.PreviousNode = null;

            this.Node = node;
        }

        public IEnumerable<Node<TNodeType>> DescendantNodes()
        { return Node<TNodeType>.DescendantNodes(this, this.Node); }

        public bool HasNodes()
        { return this.Node != null; }

        public IEnumerable<Node<TNodeType>> Nodes()
        {
            if (this.Node == null)
                yield break;

            yield return this.Node;
        }

        public void RemoveNode(Node<TNodeType> oldNode)
        {
            if (oldNode == null)
                return;

            // Validate current node and node to be removed are the same.
            var isNodeAndOldNodeSame = Object.ReferenceEquals(this.Node, oldNode);
            if (isNodeAndOldNodeSame == false)
            {
                var message = "Unable to remove node [name={0}], does not exist as a node of this container node [name={1}].".FormatWith(oldNode.Name, this.Name);
                throw new TreeException(message);
            }

            /////////////////////////////////////////////////////////////////
            // Remove old node.
            /////////////////////////////////////////////////////////////////
            this.Node = null;
        }

        public void ReplaceNode(Node<TNodeType> oldNode, Node<TNodeType> newNode)
        {
            if (oldNode == null || newNode == null)
                return;

            // Validate current node and old node are the same.
            var isNodeAndOldNodeSame = Object.ReferenceEquals(this.Node, oldNode);
            if (isNodeAndOldNodeSame == false)
            {
                var message = "Unable to replace node [name={0}], does not exist as a node of this container node [name={1}].".FormatWith(oldNode.Name, this.Name);
                throw new TreeException(message);
            }

            // Validate node has not already been added to the object tree.
            newNode.ValidateHasNotBeenAdded();

            /////////////////////////////////////////////////////////////////
            // Replace old node with new node.
            /////////////////////////////////////////////////////////////////
            newNode.RootNode = oldNode.RootNode;
            newNode.ParentNode = oldNode.ParentNode;
            newNode.NextNode = null;
            newNode.PreviousNode = null;

            this.Node = newNode;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected NodeContainer()
        { }

        protected NodeContainer(Node<TNodeType> node)
        {
            Contract.Requires(node != null);

            this.Add(node);
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Node Overrides
        protected override bool IsNodeContainer()
        { return true; }
        #endregion
    }
}
