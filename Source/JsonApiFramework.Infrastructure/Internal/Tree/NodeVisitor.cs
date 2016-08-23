// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.Internal.Tree
{
    /// <summary>
    /// Abstracts a visit operation for each concrete type of node in the
    /// node tree per the "Visitor" design pattern.
    /// </summary>
    /// <remarks>
    /// The depth parameter in the visit operations is the current document
    /// order depth of the node at the time of the visit.
    /// </remarks>
    internal abstract class NodeVisitor<TNodeType>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region NodeVisitor<T> Overrides
        public abstract VisitResult Visit(Node<TNodeType> node, int depth);
        public abstract VisitResult Visit(NodeContainer<TNodeType> nodeContainer, int depth);
        public abstract VisitResult Visit(NodesContainer<TNodeType> nodesContainer, int depth);
        #endregion
    }
}
