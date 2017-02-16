// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Converters;
using JsonApiFramework.Properties;
using JsonApiFramework.Reflection;
using JsonApiFramework.Tree.Internal;

namespace JsonApiFramework.Tree
{
    /// <summary>
    /// Represents a node within a 1-N tree.
    /// </summary>
    /// <remarks>
    /// Conceptually a node in a 1-N tree may have 1 parent node and may have
    /// N or many children nodes.
    /// </remarks>
    public class Node : INode
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Node(string name, params Node[] nodes)
            : this(name, nodes.AsEnumerable())
        { }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region INode Implementation
        /// <summary>Gets the name of this node.</summary>
        public string Name { get; }
        #endregion

        #region Properties
        /// <summary>Gets the root node of the tree.</summary>
        public Node RootNode { get; private set; }

        /// <summary>Gets the parent node of this node.</summary>
        public Node ParentNode { get; private set; }

        /// <summary>Gets the previous sibling node of this node.</summary>
        public Node PreviousNode { get; private set; }

        /// <summary>Gets the next sibling node of this node.</summary>
        public Node NextNode { get; private set; }

        /// <summary>Gets the first child node of this node.</summary>
        public Node FirstNode { get; private set; }

        /// <summary>Gets the last child node of this node.</summary>
        public Node LastNode { get; private set; }

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

        #region INode Implementation
        /// <summary>Returns a collection of the attributes for this node.</summary>
        public IEnumerable<NodeAttribute> Attributes()
        {
            var attribute = this.FirstAttribute;
            while (attribute != null)
            {
                yield return attribute;
                attribute = attribute.NextAttribute;
            }
        }

        /// <summary>
        /// Returns a collection of the direct child nodes for this node, in document order.
        /// </summary>
        public IEnumerable<INode> Nodes()
        {
            if (this.HasNodes() == false)
                yield break;

            var node = this.FirstNode;
            while (node != null)
            {
                yield return node;
                node = node.NextNode;
            }
        }

        /// <summary>
        /// Returns a collection starting with this node followed by the direct child nodes for this node, in document order.
        /// </summary>
        public IEnumerable<INode> NodesIncludeSelf()
        {
            yield return this;

            if (this.HasNodes() == false)
                yield break;

            var node = this.FirstNode;
            while (node != null)
            {
                yield return node;
                node = node.NextNode;
            }
        }

        /// <summary>
        /// Returns a collection of the descendant nodes for this node, in document order.
        /// </summary>
        public IEnumerable<INode> DescendantNodes()
        {
            if (this.HasNodes() == false)
                yield break;

            var node = this.FirstNode;
            while (true)
            {
                yield return node;

                if (node.FirstNode != null)
                {
                    node = node.FirstNode;         // walk down
                }
                else
                {
                    while (node.NextNode == null)
                    {
                        if (ReferenceEquals(node, this))
                            yield break;

                        node = node.ParentNode;     // walk up ...
                    }
                    node = node.NextNode;           // ... and right
                }
            }
        }

        /// <summary>
        /// Returns a collection starting with this node followed by the descendant nodes for this node, in document order.
        /// </summary>
        public IEnumerable<INode> DescendantNodesIncludeSelf()
        {
            yield return this;

            if (this.HasNodes() == false)
                yield break;

            var node = this.FirstNode;
            while (true)
            {
                yield return node;

                if (node.FirstNode != null)
                {
                    node = node.FirstNode;         // walk down
                }
                else
                {
                    while (node.NextNode == null)
                    {
                        if (ReferenceEquals(node, this))
                            yield break;

                        node = node.ParentNode;     // walk up ...
                    }
                    node = node.NextNode;           // ... and right
                }
            }
        }

        /// <summary>
        /// Create a string representation of this 1-N object tree.
        /// </summary>
        public string ToTreeString()
        {
            var treeStringNodeVisitor = (TreeStringNodeVisitor)this.CreateTreeStringNodeVisitor();
            this.Accept(treeStringNodeVisitor, 0);

            var treeString = treeStringNodeVisitor.TreeString;
            return treeString;
        }
        #endregion

        #region Add/Remove/Replace Attribute Methods
        /// <summary>
        /// Adds an attribute to this node.
        /// </summary>
        /// <param name="newAttribute">Attribute to add to this node.</param>
        public void AddAttribute(NodeAttribute newAttribute)
        {
            Contract.Requires(newAttribute != null);

            // Validate attribute has not already been added to the node.
            newAttribute.ValidateHasNotBeenAdded();

            // Add attribute to the node.

            // Handle special case of this being the first attribute being
            // added to this node.
            if (this.HasAttributes() == false)
            {
                // Add first attribute to this node.
                this.FirstAttribute = newAttribute;
                this.LastAttribute = newAttribute;

                newAttribute.NextAttribute = null;
                newAttribute.PreviousAttribute = null;
                return;
            }

            // Add subsequent attributes to this node.
            var previousLastAttribute = this.LastAttribute;
            this.LastAttribute = newAttribute;
            previousLastAttribute.NextAttribute = newAttribute;

            newAttribute.NextAttribute = null;
            newAttribute.PreviousAttribute = previousLastAttribute;
        }

        /// <summary>Adds a collection of attributes to this node.</summary>
        /// <param name="attributeCollection">Child nodes to add to this parent node.</param>
        public void AddAttributes(IEnumerable<NodeAttribute> attributeCollection)
        {
            if (attributeCollection == null)
                return;

            foreach (var attribute in attributeCollection)
            {
                this.AddAttribute(attribute);
            }
        }

        /// <summary>Removes the old attribute from this attribute.</summary>
        /// <param name="oldAttribute">Attribute to remove from this node.</param>
        public void RemoveAttribute(NodeAttribute oldAttribute)
        {
            Contract.Requires(oldAttribute != null);

            // Ensure old attribute exists before removing.
            var oldAttributeExists = this.Attributes()
                                         .Any(x => ReferenceEquals(x, oldAttribute));
            if (oldAttributeExists == false)
                return;

            /////////////////////////////////////////////////////////////////
            // Remove old attribute.
            /////////////////////////////////////////////////////////////////

            // 1. If the first attribute of this node was the old attribute.
            if (ReferenceEquals(oldAttribute, this.FirstAttribute))
            {
                this.FirstAttribute = oldAttribute.NextAttribute;
            }

            // 2. If the last attribute of this node was the old attribute.
            if (ReferenceEquals(oldAttribute, this.LastAttribute))
            {
                this.LastAttribute = oldAttribute.PreviousAttribute;
            }

            // 3. If the old attribute previous attribute was not null.
            if (oldAttribute.PreviousAttribute != null)
            {
                oldAttribute.PreviousAttribute.NextAttribute = oldAttribute.NextAttribute;
            }

            // 4. If the old attribute next attribute was not null.
            if (oldAttribute.NextAttribute != null)
            {
                oldAttribute.NextAttribute.PreviousAttribute = oldAttribute.PreviousAttribute;
            }
        }

        /// <summary>
        /// Replaces the old attribute with the new attribute for this node.
        /// </summary>
        /// <param name="oldAttribute">Attribute to remove from this node.</param>
        /// <param name="newAttribute">Attribute to add to this node.</param>
        public void ReplaceAttribute(NodeAttribute oldAttribute, NodeAttribute newAttribute)
        {
            Contract.Requires(oldAttribute != null);
            Contract.Requires(newAttribute != null);

            // Ensure old attribute exists as a child before removing.
            var oldAttributeExists = this.Attributes()
                                         .Any(x => ReferenceEquals(x, oldAttribute));
            if (oldAttributeExists == false)
                return;

            // Validate attribute has not already been added to the tree.
            newAttribute.ValidateHasNotBeenAdded();

            /////////////////////////////////////////////////////////////////
            // Replace old attribute with new attribute.
            /////////////////////////////////////////////////////////////////
            newAttribute.NextAttribute = oldAttribute.NextAttribute;
            newAttribute.PreviousAttribute = oldAttribute.PreviousAttribute;

            // 1. If the first attribute of this node was the old attribute.
            if (ReferenceEquals(oldAttribute, this.FirstAttribute))
            {
                this.FirstAttribute = newAttribute;
            }

            // 2. If the last attribute of this node was the old attribute.
            if (ReferenceEquals(oldAttribute, this.LastAttribute))
            {
                this.LastAttribute = newAttribute;
            }

            // 3. If the old attribute previous attribute was not null.
            if (oldAttribute.PreviousAttribute != null)
            {
                oldAttribute.PreviousAttribute.NextAttribute = newAttribute;
            }

            // 4. If the old attribute next attribute was not null.
            if (oldAttribute.NextAttribute != null)
            {
                oldAttribute.NextAttribute.PreviousAttribute = newAttribute;
            }
        }
        #endregion

        #region Content Methods
        /// <summary>Returns if this node has any content.</summary>
        public virtual bool HasContent()
        { return false; }

        /// <summary>Gets the content contained in this node.</summary>
        public virtual T GetContent<T>()
        { return default(T); }

        /// <summary>Sets the content contained in this node.</summary>
        public virtual void SetContent<T>(T content)
        { }
        #endregion

        #region Query Methods
        /// <summary>
        /// Returns if this node has any attributes.
        /// </summary>
        /// <returns>True if this node has attributes, false otherwise.</returns>
        public bool HasAttributes()
        { return this.FirstAttribute != null; }

        /// <summary>
        /// Returns if this node has any children nodes.
        /// </summary>
        /// <returns>True if this node has children nodes, false otherwise.</returns>
        public bool HasNodes()
        { return this.FirstNode != null; }

        /// <summary>
        /// Returns if this node has a parent node.
        /// </summary>
        /// <returns>True if this node has a parent node, false otherwise.</returns>
        public bool HasParent()
        { return this.ParentNode != null; }
        #endregion

        #region Add/Remove/Replace Node Methods
        /// <summary>
        /// Adds the new node as child of this node.
        /// </summary>
        /// <param name="newNode">Child node to add to this parent node.</param>
        public void AddNode(Node newNode)
        {
            Contract.Requires(newNode != null);

            this.AddNodeImpl(newNode);
        }

        /// <summary>
        /// Adds the new node collection as children of this node.
        /// </summary>
        /// <param name="newNodeCollection">Child nodes to add to this parent node.</param>
        public void AddNodes(IEnumerable<Node> newNodeCollection)
        {
            this.AddNodesImpl(newNodeCollection);
        }

        /// <summary>
        /// Removes the old node as a child from this node.
        /// </summary>
        /// <param name="oldNode">Child node to remove from this parent node.</param>
        public void RemoveNode(Node oldNode)
        {
            Contract.Requires(oldNode != null);

            // Ensure old node exists as a child before removing.
            var oldNodeExists = this.Nodes()
                                    .Any(x => ReferenceEquals(x, oldNode));
            if (oldNodeExists == false)
                return;

            /////////////////////////////////////////////////////////////////
            // Remove old node.
            /////////////////////////////////////////////////////////////////

            // Handle special cases where the old node was referenced by other nodes.

            // 1. If the first node of this node was the old node.
            if (ReferenceEquals(oldNode, this.FirstNode))
            {
                this.FirstNode = oldNode.NextNode;
            }

            // 2. If the last node of this node was the old node.
            if (ReferenceEquals(oldNode, this.LastNode))
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

        /// <summary>
        /// Replaces the old node with the new node as a child for this node.
        /// </summary>
        /// <param name="oldNode">Child node to remove from this parent node.</param>
        /// <param name="newNode">Child node to add to this parent node.</param>
        public void ReplaceNode(Node oldNode, Node newNode)
        {
            Contract.Requires(oldNode != null);
            Contract.Requires(newNode != null);

            // Ensure old node exists as a child before removing.
            var oldNodeExists = this.Nodes()
                                    .Any(x => ReferenceEquals(x, oldNode));
            if (oldNodeExists == false)
                return;

            // Validate node has not already been added to the tree.
            newNode.ValidateHasNotBeenAdded();

            /////////////////////////////////////////////////////////////////
            // Replace old node with new node.
            /////////////////////////////////////////////////////////////////
            newNode.RootNode = oldNode.RootNode;
            newNode.ParentNode = oldNode.ParentNode;
            newNode.NextNode = oldNode.NextNode;
            newNode.PreviousNode = oldNode.PreviousNode;

            // Handle special cases where the old node was referenced by other nodes.

            // 1. If the first node of this node was the old node.
            if (ReferenceEquals(oldNode, this.FirstNode))
            {
                this.FirstNode = newNode;
            }

            // 2. If the last node of this node was the old node.
            if (ReferenceEquals(oldNode, this.LastNode))
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

        #region Visitor Methods
        public void Accept(NodeVisitor nodeVisitor, int depth)
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
                    case VisitResult.ContinueWithChildAndSiblingNodes:
                        this.FirstNode.Accept(nodeVisitor, depth + 1);
                        break;
                }
            }

            if (this.NextNode == null)
                return;

            switch (visitResult)
            {
                case VisitResult.ContinueWithChildAndSiblingNodes:
                case VisitResult.ContinueWithSiblingNodesOnly:
                    this.NextNode.Accept(nodeVisitor, depth);
                    break;
            }
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected Node(string name)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.Name = name;
            this.RootNode = this;
        }

        protected Node(string name, Node node)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.Name = name;
            this.RootNode = this;

            this.AddNodeImpl(node);
        }

        protected Node(string name, IEnumerable<Node> nodes)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(name) == false);

            this.Name = name;
            this.RootNode = this;

            this.AddNodesImpl(nodes);
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Node Overrides
        internal virtual TreeStringNodeVisitor CreateTreeStringNodeVisitor()
        {
            var treeStringNodeVisitor = new TreeStringNodeVisitor();
            return treeStringNodeVisitor;
        }
        #endregion

        // INTERNAL FIELDS //////////////////////////////////////////////////
        #region Properties
        internal static readonly TypeConverter TypeConverter = new TypeConverter();
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddNodeImpl(Node newNode)
        {
            if (newNode == null)
                return;

            // Validate node has not already been added to the tree.
            newNode.ValidateHasNotBeenAdded();

            // Add node to the object graph.
            newNode.RootNode = this.RootNode;
            newNode.ParentNode = this;

            // Handle special case of this being the first child node being
            // added to this node.
            if (this.HasNodes() == false)
            {
                // Add first child node to this node.
                this.FirstNode = newNode;
                this.LastNode = newNode;

                newNode.NextNode = null;
                newNode.PreviousNode = null;
                return;
            }

            // Add subsequent child nodes to this node.
            var previousLastNode = this.LastNode;
            this.LastNode = newNode;
            previousLastNode.NextNode = newNode;

            newNode.NextNode = null;
            newNode.PreviousNode = previousLastNode;
        }

        private void AddNodesImpl(IEnumerable<Node> newNodeCollection)
        {
            if (newNodeCollection == null)
                return;

            foreach (var newNode in newNodeCollection)
            {
                this.AddNodeImpl(newNode);
            }
        }

        private void ThrowExceptionForAlreadyBeenAdded()
        {
            var message = CoreErrorStrings.TreeExceptionNodeAlreadyAddedMessage.FormatWith(this.Name);
            throw new TreeException(message);
        }

        private void ValidateHasNotBeenAdded()
        {
            if (this.ParentNode == null && this.PreviousNode == null && this.NextNode == null)
                return;

            this.ThrowExceptionForAlreadyBeenAdded();
        }
        #endregion
    }

    /// <summary>
    /// Represents a node that may have content within a 1-N tree.
    /// </summary>
    public class Node<TContent> : Node
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public Node(string name, params Node[] nodes)
            : base(name, nodes)
        { }

        public Node(string name, TContent content, params Node[] nodes)
            : base(name, nodes)
        { this.Content = content; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Content Overrides
        /// <summary>Returns if this node has any content.</summary>
        public override bool HasContent()
        {
            if (IsContentValueType)
                return true;

            var contentAsObject = (object)this.Content;
            return contentAsObject != null;
        }

        /// <summary>Gets the content contained in this node.</summary>
        public override T GetContent<T>()
        {
            var content = TypeConverter.Convert<TContent, T>(this.Content, null);
            return content;
        }

        /// <summary>Sets the content contained in this node.</summary>
        public override void SetContent<T>(T content)
        {
            var value = TypeConverter.Convert<T, TContent>(content, null);
            this.Content = value;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private TContent Content { get; set; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Properties
        private static readonly bool IsContentValueType = TypeReflection.IsValueType(typeof(TContent));
        #endregion
    }
}
