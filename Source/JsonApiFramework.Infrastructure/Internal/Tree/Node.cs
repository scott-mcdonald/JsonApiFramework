// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Abstracts the concept of a node in an object tree.
    /// </summary>
    internal abstract class Node<TNodeType>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Node Overrides
        /// <summary>Gets the node type of this node.</summary>
        public abstract TNodeType NodeType { get; }

        /// <summary>Gets the name of this node.</summary>
        public abstract string Name { get; }
        #endregion

        #region Properties
        /// <summary>Gets the root node of the object tree.</summary>
        public Node<TNodeType> RootNode { get; protected internal set; }

        /// <summary>Gets the parent node of this node.</summary>
        public Node<TNodeType> ParentNode { get; internal set; }

        /// <summary>Gets the previous sibling node of this node.</summary>
        public Node<TNodeType> PreviousNode { get; internal set; }

        /// <summary>Gets the next sibling node of this node.</summary>
        public Node<TNodeType> NextNode { get; internal set; }

        /// <summary>Gets the first attribute of this node.</summary>
        public NodeAttribute FirstAttribute { get; private set; }

        /// <summary>Gets the last attribute of this node.</summary>
        public NodeAttribute LastAttribute { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        { return this.Name; }
        #endregion

        #region Node Overrides
        /// <summary>
        /// Abstracts the acceptance of a node visitor in document order per
        /// the "Visitor" design pattern.
        /// </summary>
        /// <param name="nodeVisitor">The node visitor to accept.</param>
        /// <param name="depth">The current document order depth of this node
        /// upon acceptance of the node visitor.</param>
        public virtual void Accept(NodeVisitor<TNodeType> nodeVisitor, int depth)
        {
            Contract.Requires(nodeVisitor != null);
            Contract.Requires(depth >= 0);

            var visitResult = nodeVisitor.Visit(this, depth);
            if (visitResult == VisitResult.Done)
                return;

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

        #region Methods
        /// <summary>
        /// Adds an attribute to this node.
        /// </summary>
        public void Add(NodeAttribute attribute)
        {
            if (attribute == null)
                return;

            // Validate attribute has not already been added to the node.
            attribute.ValidateHasNotBeenAdded();

            // Add attribute to the node.

            // Handle special case of this being the first attribute being
            // added to this node.
            if (this.HasAttributes() == false)
            {
                // Add first attribute to this node.
                this.FirstAttribute = attribute;
                this.LastAttribute = attribute;

                attribute.NextAttribute = null;
                attribute.PreviousAttribute = null;
                return;
            }

            // Add subsequent attributes to this node.
            var previousLastAttribute = this.LastAttribute;
            this.LastAttribute = attribute;
            previousLastAttribute.NextAttribute = attribute;

            attribute.NextAttribute = null;
            attribute.PreviousAttribute = previousLastAttribute;
        }

        /// <summary>
        /// Returns a collection of the attributes for this node.
        /// </summary>
        public IEnumerable<NodeAttribute> Attributes()
        {
            var attribute = this.FirstAttribute;
            while (attribute != null)
            {
                yield return attribute;
                attribute = attribute.NextAttribute;
            }
        }

        public IEnumerable<NodeAttribute> Attributes(string name)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            return this.Attributes()
                       .Where(x => x.Name == name);
        }

        /// <summary>
        /// Returns if this node has any attributes.
        /// </summary>
        public bool HasAttributes()
        { return this.FirstAttribute != null; }

        public bool HasAttribute(string name)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            return this.Attributes()
                       .Any(x => x.Name == name);
        }

        /// <summary>
        /// Create a string that represents the object tree.
        /// </summary>
        public string ToTreeString()
        {
            var treeStringNodeVisitor = new TreeStringBuilderNodeVisitor<TNodeType>();
            this.Accept(treeStringNodeVisitor, 0);

            var treeString = treeStringNodeVisitor.TreeString;
            return treeString;
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Node Overrides
        protected virtual bool IsNodeContainer()
        { return false; }

        protected virtual bool IsNodesContainer()
        { return false; }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        internal static IEnumerable<Node<TNodeType>> DescendantNodes(Node<TNodeType> root, Node<TNodeType> initial)
        {
            if (root == null || initial == null)
                yield break;

            var node = initial;
            while (true)
            {
                yield return node;

                if (node.IsNodesContainer())
                {
                    var nodesContainer = (NodesContainer<TNodeType>)node;
                    if (nodesContainer.FirstNode != null)
                    {
                        node = nodesContainer.FirstNode;    // walk down
                    }
                    else
                    {
                        while (node.NextNode == null)
                        {
                            if (Object.ReferenceEquals(node, root))
                                yield break;

                            node = node.ParentNode;         // walk up ...
                        }
                        node = node.NextNode;               // ... and right
                    }
                    continue;
                }

                if (node.IsNodeContainer())
                {
                    var nodeContainer = (NodeContainer<TNodeType>)node;
                    if (nodeContainer.Node != null)
                    {
                        node = nodeContainer.Node;          // walk down
                    }
                    else
                    {
                        while (node.NextNode == null)
                        {
                            if (Object.ReferenceEquals(node, root))
                                yield break;

                            node = node.ParentNode;         // walk up ...
                        }
                        node = node.NextNode;               // ... and right
                    }
                    continue;
                }

                while (node.NextNode == null)
                {
                    if (Object.ReferenceEquals(node, root))
                        yield break;

                    node = node.ParentNode;                 // walk up ...
                }
                node = node.NextNode;                       // ... and right
            }
        }

        internal void ValidateHasNotBeenAdded()
        {
            if (this.RootNode == null && this.ParentNode == null && this.PreviousNode == null && this.NextNode == null)
                return;

            this.ThrowExceptionForAlreadyBeenAdded();
        }
        #endregion

        // INTERNAL FIELDS //////////////////////////////////////////////////
        #region Fields
        internal static readonly IEqualityComparer<TNodeType> DefaultNodeTypeEqualityComarer =
            EqualityComparer<TNodeType>.Default;
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ThrowExceptionForAlreadyBeenAdded()
        {
            var message = "{0} has already been added to the object tree.".FormatWith(this.Name);
            throw new TreeException(message);
        }
        #endregion
    }
}
