// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Represents a node visitor that builds a string of the object tree by
    /// visiting the nodes in document order.
    /// </summary>
    /// <typeparam name="TNodeType"></typeparam>
    internal class TreeStringBuilderNodeVisitor<TNodeType> : NodeVisitor<TNodeType>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TreeStringBuilderNodeVisitor()
        {
            this.StringBuilder = new StringBuilder();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        public string TreeString
        { get { return this.StringBuilder.ToString(); } }

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region NodeVisitor<T> Overrides
        public override VisitResult Visit(Node<TNodeType> node, int depth)
        {
            Contract.Requires(node != null);
            Contract.Requires(depth >= 0);

            this.AddNameToTreeString(node, depth);

            return VisitResult.Continue;
        }

        public override VisitResult Visit(NodeContainer<TNodeType> nodeContainer, int depth)
        {
            Contract.Requires(nodeContainer != null);
            Contract.Requires(depth >= 0);

            this.AddNameToTreeString(nodeContainer, depth);

            return VisitResult.Continue;
        }

        public override VisitResult Visit(NodesContainer<TNodeType> nodesContainer, int depth)
        {
            Contract.Requires(nodesContainer != null);
            Contract.Requires(depth >= 0);

            this.AddNameToTreeString(nodesContainer, depth);

            return VisitResult.Continue;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private StringBuilder StringBuilder { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddNameToTreeString(Node<TNodeType> node, int depth)
        {
            var indentSpace = depth * IndentSize;
            this.StringBuilder.Append(Whitespace, indentSpace);
            if (node.HasAttributes() == false)
            {
                this.StringBuilder.AppendLine(node.Name);
                return;
            }

            var attributesAsStrings = node.Attributes()
                                          .Select(x => x.ToString())
                                          .Aggregate((current, next) => "{0} {1}".FormatWith(current, next));
            var nodeNameWithAttributes = "{0} attributes({1})".FormatWith(node.Name, attributesAsStrings);
            this.StringBuilder.AppendLine(nodeNameWithAttributes);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const char Whitespace = ' ';
        private const int IndentSize = 2;
        #endregion
    }
}
