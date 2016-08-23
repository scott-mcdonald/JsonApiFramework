// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Abstracts the concept of a node that can contain many child nodes in a tree.
    /// </summary>
    internal abstract class NodesContainer<TNodeType> : Node<TNodeType>
        , IContainerNode<TNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the first node of this node container.</summary>
        public Node<TNodeType> FirstNode { get; private set; }

        /// <summary>Gets the last node of this node container.</summary>
        public Node<TNodeType> LastNode { get; private set; }
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

            if (this.FirstNode != null)
            {
                switch (visitResult)
                {
                    case VisitResult.Continue:
                        this.FirstNode.Accept(nodeVisitor, depth + 1);
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

            // Handle special case of this being the first child node being
            // added to this container node.
            if (this.HasNodes() == false)
            {
                // Add first child node to this container node.
                this.FirstNode = node;
                this.LastNode = node;

                node.NextNode = null;
                node.PreviousNode = null;
                return;
            }

            // Add subsequent child nodes to this container node.
            var previousLastNode = this.LastNode;
            this.LastNode = node;
            previousLastNode.NextNode = node;

            node.NextNode = null;
            node.PreviousNode = previousLastNode;
        }

        public IEnumerable<Node<TNodeType>> DescendantNodes()
        { return Node<TNodeType>.DescendantNodes(this, this.FirstNode); }

        public bool HasNodes()
        { return this.FirstNode != null; }

        public IEnumerable<Node<TNodeType>> Nodes()
        {
            var node = this.FirstNode;
            while (node != null)
            {
                yield return node;
                node = node.NextNode;
            }
        }

        public void RemoveNode(Node<TNodeType> oldNode)
        {
            if (oldNode == null)
                return;

            // Find old node from the contained nodes.
            var childNode = this.Nodes()
                                .SingleOrDefault(x => Object.ReferenceEquals(x, oldNode));
            if (childNode == null)
            {
                var message = "Unable to remove node [name={0}], does not exist as a node of this container node [name={1}].".FormatWith(oldNode.Name, this.Name);
                throw new TreeException(message);
            }

            /////////////////////////////////////////////////////////////////
            // Remove old node.
            /////////////////////////////////////////////////////////////////

            // Handle special cases where the old node was referenced by other nodes.

            // 1. If the first node of this nodes container was the old node.
            if (Object.ReferenceEquals(oldNode, this.FirstNode))
            {
                this.FirstNode = oldNode.NextNode;
            }

            // 2. If the last node of this nodes container was the old node.
            if (Object.ReferenceEquals(oldNode, this.LastNode))
            {
                this.LastNode = oldNode.PreviousNode;
            }

            // 3. If the old node previous node was not null.
            if (oldNode.PreviousNode != null)
            {
                oldNode.PreviousNode.NextNode = oldNode.NextNode;
            }

            // 4. If the old node next node was not null.
            if (oldNode.NextNode != null)
            {
                oldNode.NextNode.PreviousNode = oldNode.PreviousNode;
            }
        }

        public void ReplaceNode(Node<TNodeType> oldNode, Node<TNodeType> newNode)
        {
            if (oldNode == null || newNode == null)
                return;

            // Find old node from the contained nodes.
            var childNode = this.Nodes()
                                .SingleOrDefault(x => Object.ReferenceEquals(x, oldNode));
            if (childNode == null)
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
            newNode.NextNode = oldNode.NextNode;
            newNode.PreviousNode = oldNode.PreviousNode;

            // Handle special cases where the old node was referenced by other nodes.

            // 1. If the first node of this nodes container was the old node.
            if (Object.ReferenceEquals(oldNode, this.FirstNode))
            {
                this.FirstNode = newNode;
            }

            // 2. If the last node of this nodes container was the old node.
            if (Object.ReferenceEquals(oldNode, this.LastNode))
            {
                this.LastNode = newNode;
            }

            // 3. If the old node previous node was not null.
            if (oldNode.PreviousNode != null)
            {
                oldNode.PreviousNode.NextNode = newNode;
            }

            // 4. If the old node next node was not null.
            if (oldNode.NextNode != null)
            {
                oldNode.NextNode.PreviousNode = newNode;
            }
        }
        #endregion

        #region Methods
        // ReSharper disable MemberCanBePrivate.Global
        public void Add(IEnumerable<Node<TNodeType>> nodes)
        // ReSharper restore MemberCanBePrivate.Global
        {
            if (nodes == null)
                return;

            foreach (var node in nodes)
            {
                this.Add(node);
            }
        }

        // ReSharper disable MemberCanBePrivate.Global
        public void Add(params Node<TNodeType>[] nodes)
        // ReSharper restore MemberCanBePrivate.Global
        {
            if (nodes == null || nodes.Length == 0)
                return;

            foreach (var node in nodes)
            {
                this.Add(node);
            }
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected NodesContainer()
        { }

        protected NodesContainer(IEnumerable<Node<TNodeType>> nodes)
        {
            Contract.Requires(nodes != null);

            this.Add(nodes);
        }

        protected NodesContainer(Node<TNodeType>[] nodes)
        {
            Contract.Requires(nodes != null);

            this.Add(nodes);
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Node Overrides
        protected override bool IsNodesContainer()
        { return true; }
        #endregion

        #region Methods
        protected static TRoot CreateRoot<TRoot>(Func<TRoot> rootFactory, params Node<TNodeType>[] nodes)
            where TRoot : NodesContainer<TNodeType>
        {
            Contract.Requires(rootFactory != null);

            var root = rootFactory();
            root.RootNode = root;

            if (nodes == null || nodes.Length == 0)
                return root;

            // Add the immediate nodes to the root.
            root.Add(nodes.AsEnumerable());

            // Ensure all descendant nodes of the root have the root property
            // set correctly.
            foreach (var descendantNode in root.DescendantNodes())
            {
                descendantNode.RootNode = root;
            }

            return root;
        }
        #endregion
    }
}
