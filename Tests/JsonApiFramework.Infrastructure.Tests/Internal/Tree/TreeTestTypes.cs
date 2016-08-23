// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Tree;

namespace JsonApiFramework.Tests.Internal.Tree
{
    internal static class TreeTestTypes
    {
        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Types
        public enum TestNodeType
        {
            Unknown,
            Document,
            Node,
            NodeContainer,
            NodesContainer
        };

        public class DocumentNode : NodesContainer<TestNodeType>
        {
            #region Node Overrides
            public override TestNodeType NodeType
            { get { return TestNodeType.Document; } }

            public override string Name
            { get { return "Document"; } }
            #endregion

            #region Methods
            public static DocumentNode Create(params Node<TestNodeType>[] nodes)
            {
                var documentNode = NodesContainer.CreateRoot(() => new DocumentNode(), nodes);
                return documentNode;
            }
            #endregion

            #region Constructors
            private DocumentNode()
            { }
            #endregion
        }

        public class Node : Node<TestNodeType>
        {
            #region Constructors
            public Node(string name)
            { _name = name; }
            #endregion

            #region Node Overrides
            public override TestNodeType NodeType
            { get { return TestNodeType.Node; } }

            public override string Name
            { get { return _name; } }
            #endregion

            #region Fields
            private readonly string _name;
            #endregion
        }

        public class NodeContainer : NodeContainer<TestNodeType>
        {
            #region Constructors
            public NodeContainer(string name, Node<TestNodeType> node = null)
                : base(node)
            { _name = name; }
            #endregion

            #region Node Overrides
            public override TestNodeType NodeType
            { get { return TestNodeType.NodeContainer; } }

            public override string Name
            { get { return _name; } }
            #endregion

            #region Fields
            private readonly string _name;
            #endregion
        }

        public class NodesContainer : NodesContainer<TestNodeType>
        {
            #region Constructors
            public NodesContainer(string name, params Node<TestNodeType>[] nodes)
                : base(nodes)
            { _name = name; }
            #endregion

            #region Node Overrides
            public override TestNodeType NodeType
            { get { return TestNodeType.NodesContainer; } }

            public override string Name
            { get { return _name; } }
            #endregion

            #region Fields
            private readonly string _name;
            #endregion
        }
        #endregion
    }
}
