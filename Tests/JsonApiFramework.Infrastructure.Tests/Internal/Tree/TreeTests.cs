// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Internal.Tree;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Tree
{
    using DocumentNode = TreeTestTypes.DocumentNode;
    using Node = TreeTestTypes.Node;
    using NodeContainer = TreeTestTypes.NodeContainer;
    using NodesContainer = TreeTestTypes.NodesContainer;

    public class TreeTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TreeTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestCreateEmptyTree()
        {
            // Arrange
            DocumentNode document;

            // Act
            CreateTreeEmpty(out document);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Null(document.FirstNode);
            Assert.Null(document.LastNode);
        }

        [Fact]
        public void TestCreateTreeWithDepthOf1And1Node()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;

            // Act
            CreateTreeWithDepthOf1And1Node(out document, out node11);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Equal(node11, document.FirstNode);
            Assert.Equal(node11, document.LastNode);

            // .. Node 11
            Assert.Equal(document, node11.RootNode);
            Assert.Equal(document, node11.ParentNode);
            Assert.Null(node11.PreviousNode);
            Assert.Null(node11.NextNode);
            Assert.Null(node11.FirstNode);
            Assert.Null(node11.LastNode);
        }

        [Fact]
        public void TestCreateTreeWithDepthOf1And2Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;

            // Act
            CreateTreeWithDepthOf1And2Nodes(out document, out node11, out node12);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Equal(node11, document.FirstNode);
            Assert.Equal(node12, document.LastNode);

            // .. Node 11
            Assert.Equal(document, node11.RootNode);
            Assert.Equal(document, node11.ParentNode);
            Assert.Null(node11.PreviousNode);
            Assert.Equal(node12, node11.NextNode);
            Assert.Null(node11.FirstNode);
            Assert.Null(node11.LastNode);

            // .. Node 12
            Assert.Equal(document, node12.RootNode);
            Assert.Equal(document, node12.ParentNode);
            Assert.Equal(node11, node12.PreviousNode);
            Assert.Null(node12.NextNode);
            Assert.Null(node12.FirstNode);
            Assert.Null(node12.LastNode);
        }

        [Fact]
        public void TestCreateTreeWithDepthOf1And3Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;

            // Act
            CreateTreeWithDepthOf1And3Nodes(out document, out node11, out node12, out node13);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Equal(node11, document.FirstNode);
            Assert.Equal(node13, document.LastNode);

            // .. Node 11
            Assert.Equal(document, node11.RootNode);
            Assert.Equal(document, node11.ParentNode);
            Assert.Null(node11.PreviousNode);
            Assert.Equal(node12, node11.NextNode);
            Assert.Null(node11.FirstNode);
            Assert.Null(node11.LastNode);

            // .. Node 12
            Assert.Equal(document, node12.RootNode);
            Assert.Equal(document, node12.ParentNode);
            Assert.Equal(node11, node12.PreviousNode);
            Assert.Equal(node13, node12.NextNode);
            Assert.Null(node12.FirstNode);
            Assert.Null(node12.LastNode);

            // .. Node 13
            Assert.Equal(document, node13.RootNode);
            Assert.Equal(document, node13.ParentNode);
            Assert.Equal(node12, node13.PreviousNode);
            Assert.Null(node13.NextNode);
            Assert.Null(node13.FirstNode);
            Assert.Null(node13.LastNode);
        }

        [Fact]
        public void TestCreateTreeWithDepthOf2And4Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            NodeContainer node21;

            // Act
            CreateTreeWithDepthOf2And4Nodes(out document, out node11, out node12, out node13, out node21);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Equal(node11, document.FirstNode);
            Assert.Equal(node13, document.LastNode);

            // .. Node 11
            Assert.Equal(document, node11.RootNode);
            Assert.Equal(document, node11.ParentNode);
            Assert.Null(node11.PreviousNode);
            Assert.Equal(node12, node11.NextNode);
            Assert.Equal(node21, node11.FirstNode);
            Assert.Equal(node21, node11.LastNode);

            // .. Node 12
            Assert.Equal(document, node12.RootNode);
            Assert.Equal(document, node12.ParentNode);
            Assert.Equal(node11, node12.PreviousNode);
            Assert.Equal(node13, node12.NextNode);
            Assert.Null(node12.FirstNode);
            Assert.Null(node12.LastNode);

            // .. Node 13
            Assert.Equal(document, node13.RootNode);
            Assert.Equal(document, node13.ParentNode);
            Assert.Equal(node12, node13.PreviousNode);
            Assert.Null(node13.NextNode);
            Assert.Null(node13.FirstNode);
            Assert.Null(node13.LastNode);

            // .. Node 21
            Assert.Equal(document, node21.RootNode);
            Assert.Equal(node11, node21.ParentNode);
            Assert.Null(node21.PreviousNode);
            Assert.Null(node21.NextNode);
            Assert.Null(node21.Node);
        }

        [Fact]
        public void TestCreateTreeWithDepthOf2And5Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            NodeContainer node21;
            NodeContainer node22;

            // Act
            CreateTreeWithDepthOf2And5Nodes(out document, out node11, out node12, out node13, out node21, out node22);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Equal(node11, document.FirstNode);
            Assert.Equal(node13, document.LastNode);

            // .. Node 11
            Assert.Equal(document, node11.RootNode);
            Assert.Equal(document, node11.ParentNode);
            Assert.Null(node11.PreviousNode);
            Assert.Equal(node12, node11.NextNode);
            Assert.Equal(node21, node11.FirstNode);
            Assert.Equal(node22, node11.LastNode);

            // .. Node 12
            Assert.Equal(document, node12.RootNode);
            Assert.Equal(document, node12.ParentNode);
            Assert.Equal(node11, node12.PreviousNode);
            Assert.Equal(node13, node12.NextNode);
            Assert.Null(node12.FirstNode);
            Assert.Null(node12.LastNode);

            // .. Node 13
            Assert.Equal(document, node13.RootNode);
            Assert.Equal(document, node13.ParentNode);
            Assert.Equal(node12, node13.PreviousNode);
            Assert.Null(node13.NextNode);
            Assert.Null(node13.FirstNode);
            Assert.Null(node13.LastNode);

            // .. Node 21
            Assert.Equal(document, node21.RootNode);
            Assert.Equal(node11, node21.ParentNode);
            Assert.Null(node21.PreviousNode);
            Assert.Equal(node22, node21.NextNode);
            Assert.Null(node21.Node);

            // .. Node 22
            Assert.Equal(document, node22.RootNode);
            Assert.Equal(node11, node22.ParentNode);
            Assert.Equal(node21, node22.PreviousNode);
            Assert.Null(node22.NextNode);
            Assert.Null(node22.Node);
        }

        [Fact]
        public void TestCreateTreeWithDepthOf2And6Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            NodeContainer node21;
            NodeContainer node22;
            NodeContainer node23;

            // Act
            CreateTreeWithDepthOf2And6Nodes(out document, out node11, out node12, out node13, out node21, out node22, out node23);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Equal(node11, document.FirstNode);
            Assert.Equal(node13, document.LastNode);

            // .. Node 11
            Assert.Equal(document, node11.RootNode);
            Assert.Equal(document, node11.ParentNode);
            Assert.Null(node11.PreviousNode);
            Assert.Equal(node12, node11.NextNode);
            Assert.Equal(node21, node11.FirstNode);
            Assert.Equal(node23, node11.LastNode);

            // .. Node 12
            Assert.Equal(document, node12.RootNode);
            Assert.Equal(document, node12.ParentNode);
            Assert.Equal(node11, node12.PreviousNode);
            Assert.Equal(node13, node12.NextNode);
            Assert.Null(node12.FirstNode);
            Assert.Null(node12.LastNode);

            // .. Node 13
            Assert.Equal(document, node13.RootNode);
            Assert.Equal(document, node13.ParentNode);
            Assert.Equal(node12, node13.PreviousNode);
            Assert.Null(node13.NextNode);
            Assert.Null(node13.FirstNode);
            Assert.Null(node13.LastNode);

            // .. Node 21
            Assert.Equal(document, node21.RootNode);
            Assert.Equal(node11, node21.ParentNode);
            Assert.Null(node21.PreviousNode);
            Assert.Equal(node22, node21.NextNode);
            Assert.Null(node21.Node);

            // .. Node 22
            Assert.Equal(document, node22.RootNode);
            Assert.Equal(node11, node22.ParentNode);
            Assert.Equal(node21, node22.PreviousNode);
            Assert.Equal(node23, node22.NextNode);
            Assert.Null(node22.Node);

            // .. Node 23
            Assert.Equal(document, node23.RootNode);
            Assert.Equal(node11, node23.ParentNode);
            Assert.Equal(node22, node23.PreviousNode);
            Assert.Null(node23.NextNode);
            Assert.Null(node23.Node);
        }

        [Fact]
        public void TestCreateTreeWithDepthOf3And7Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            NodeContainer node21;
            NodeContainer node22;
            NodeContainer node23;
            Node node31;

            // Act
            CreateTreeWithDepthOf3And7Nodes(out document, out node11, out node12, out node13, out node21, out node22, out node23, out node31);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Equal(node11, document.FirstNode);
            Assert.Equal(node13, document.LastNode);

            // .. Node 11
            Assert.Equal(document, node11.RootNode);
            Assert.Equal(document, node11.ParentNode);
            Assert.Null(node11.PreviousNode);
            Assert.Equal(node12, node11.NextNode);
            Assert.Equal(node21, node11.FirstNode);
            Assert.Equal(node23, node11.LastNode);

            // .. Node 12
            Assert.Equal(document, node12.RootNode);
            Assert.Equal(document, node12.ParentNode);
            Assert.Equal(node11, node12.PreviousNode);
            Assert.Equal(node13, node12.NextNode);
            Assert.Null(node12.FirstNode);
            Assert.Null(node12.LastNode);

            // .. Node 13
            Assert.Equal(document, node13.RootNode);
            Assert.Equal(document, node13.ParentNode);
            Assert.Equal(node12, node13.PreviousNode);
            Assert.Null(node13.NextNode);
            Assert.Null(node13.FirstNode);
            Assert.Null(node13.LastNode);

            // .. Node 21
            Assert.Equal(document, node21.RootNode);
            Assert.Equal(node11, node21.ParentNode);
            Assert.Null(node21.PreviousNode);
            Assert.Equal(node22, node21.NextNode);
            Assert.Equal(node31, node21.Node);

            // .. Node 22
            Assert.Equal(document, node22.RootNode);
            Assert.Equal(node11, node22.ParentNode);
            Assert.Equal(node21, node22.PreviousNode);
            Assert.Equal(node23, node22.NextNode);
            Assert.Null(node22.Node);

            // .. Node 23
            Assert.Equal(document, node23.RootNode);
            Assert.Equal(node11, node23.ParentNode);
            Assert.Equal(node22, node23.PreviousNode);
            Assert.Null(node23.NextNode);
            Assert.Null(node23.Node);

            // .. Node 31
            Assert.Equal(document, node31.RootNode);
            Assert.Equal(node21, node31.ParentNode);
            Assert.Null(node31.PreviousNode);
            Assert.Null(node31.NextNode);
        }

        [Fact]
        public void TestCreateTreeWithDepthOf3And8Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            NodeContainer node21;
            NodeContainer node22;
            NodeContainer node23;
            Node node31;
            Node node32;

            // Act
            CreateTreeWithDepthOf3And8Nodes(out document, out node11, out node12, out node13, out node21, out node22, out node23, out node31, out node32);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Equal(node11, document.FirstNode);
            Assert.Equal(node13, document.LastNode);

            // .. Node 11
            Assert.Equal(document, node11.RootNode);
            Assert.Equal(document, node11.ParentNode);
            Assert.Null(node11.PreviousNode);
            Assert.Equal(node12, node11.NextNode);
            Assert.Equal(node21, node11.FirstNode);
            Assert.Equal(node23, node11.LastNode);

            // .. Node 12
            Assert.Equal(document, node12.RootNode);
            Assert.Equal(document, node12.ParentNode);
            Assert.Equal(node11, node12.PreviousNode);
            Assert.Equal(node13, node12.NextNode);
            Assert.Null(node12.FirstNode);
            Assert.Null(node12.LastNode);

            // .. Node 13
            Assert.Equal(document, node13.RootNode);
            Assert.Equal(document, node13.ParentNode);
            Assert.Equal(node12, node13.PreviousNode);
            Assert.Null(node13.NextNode);
            Assert.Null(node13.FirstNode);
            Assert.Null(node13.LastNode);

            // .. Node 21
            Assert.Equal(document, node21.RootNode);
            Assert.Equal(node11, node21.ParentNode);
            Assert.Null(node21.PreviousNode);
            Assert.Equal(node22, node21.NextNode);
            Assert.Equal(node31, node21.Node);

            // .. Node 22
            Assert.Equal(document, node22.RootNode);
            Assert.Equal(node11, node22.ParentNode);
            Assert.Equal(node21, node22.PreviousNode);
            Assert.Equal(node23, node22.NextNode);
            Assert.Equal(node32, node22.Node);

            // .. Node 23
            Assert.Equal(document, node23.RootNode);
            Assert.Equal(node11, node23.ParentNode);
            Assert.Equal(node22, node23.PreviousNode);
            Assert.Null(node23.NextNode);
            Assert.Null(node23.Node);

            // .. Node 31
            Assert.Equal(document, node31.RootNode);
            Assert.Equal(node21, node31.ParentNode);
            Assert.Null(node31.PreviousNode);
            Assert.Null(node31.NextNode);

            // .. Node 32
            Assert.Equal(document, node32.RootNode);
            Assert.Equal(node22, node32.ParentNode);
            Assert.Null(node32.PreviousNode);
            Assert.Null(node32.NextNode);
        }

        [Fact]
        public void TestCreateTreeWithDepthOf3And9Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            NodeContainer node21;
            NodeContainer node22;
            NodeContainer node23;
            Node node31;
            Node node32;
            Node node33;

            // Act
            CreateTreeWithDepthOf3And9Nodes(out document, out node11, out node12, out node13, out node21, out node22, out node23, out node31, out node32, out node33);
            OutputTree(this, ObjectTreeHeader, document);

            // Assert

            // .. Document
            Assert.Equal(document, document.RootNode);
            Assert.Null(document.ParentNode);
            Assert.Null(document.PreviousNode);
            Assert.Null(document.NextNode);
            Assert.Equal(node11, document.FirstNode);
            Assert.Equal(node13, document.LastNode);

            // .. Node 11
            Assert.Equal(document, node11.RootNode);
            Assert.Equal(document, node11.ParentNode);
            Assert.Null(node11.PreviousNode);
            Assert.Equal(node12, node11.NextNode);
            Assert.Equal(node21, node11.FirstNode);
            Assert.Equal(node23, node11.LastNode);

            // .. Node 12
            Assert.Equal(document, node12.RootNode);
            Assert.Equal(document, node12.ParentNode);
            Assert.Equal(node11, node12.PreviousNode);
            Assert.Equal(node13, node12.NextNode);
            Assert.Null(node12.FirstNode);
            Assert.Null(node12.LastNode);

            // .. Node 13
            Assert.Equal(document, node13.RootNode);
            Assert.Equal(document, node13.ParentNode);
            Assert.Equal(node12, node13.PreviousNode);
            Assert.Null(node13.NextNode);
            Assert.Null(node13.FirstNode);
            Assert.Null(node13.LastNode);

            // .. Node 21
            Assert.Equal(document, node21.RootNode);
            Assert.Equal(node11, node21.ParentNode);
            Assert.Null(node21.PreviousNode);
            Assert.Equal(node22, node21.NextNode);
            Assert.Equal(node31, node21.Node);

            // .. Node 22
            Assert.Equal(document, node22.RootNode);
            Assert.Equal(node11, node22.ParentNode);
            Assert.Equal(node21, node22.PreviousNode);
            Assert.Equal(node23, node22.NextNode);
            Assert.Equal(node32, node22.Node);

            // .. Node 23
            Assert.Equal(document, node23.RootNode);
            Assert.Equal(node11, node23.ParentNode);
            Assert.Equal(node22, node23.PreviousNode);
            Assert.Null(node23.NextNode);
            Assert.Equal(node33, node23.Node);

            // .. Node 31
            Assert.Equal(document, node31.RootNode);
            Assert.Equal(node21, node31.ParentNode);
            Assert.Null(node31.PreviousNode);
            Assert.Null(node31.NextNode);

            // .. Node 32
            Assert.Equal(document, node32.RootNode);
            Assert.Equal(node22, node32.ParentNode);
            Assert.Null(node32.PreviousNode);
            Assert.Null(node32.NextNode);

            // .. Node 33
            Assert.Equal(document, node33.RootNode);
            Assert.Equal(node23, node33.ParentNode);
            Assert.Null(node33.PreviousNode);
            Assert.Null(node33.NextNode);
        }

        [Fact]
        public void TestNodesWithEmptyTree()
        {
            // Arrange
            DocumentNode document;
            CreateTreeEmpty(out document);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var nodes = document.Nodes()
                                .ToList();

            // Assert
            Assert.Equal(0, nodes.Count);
        }

        [Fact]
        public void TestNodesWithWithDepthOf1And1Node()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            CreateTreeWithDepthOf1And1Node(out document, out node11);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var nodes = document.Nodes()
                                .ToList();

            // Assert
            Assert.Equal(1, nodes.Count);
            Assert.Equal(node11.Name, nodes[0].Name);
        }

        [Fact]
        public void TestNodesWithWithDepthOf1And2Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            CreateTreeWithDepthOf1And2Nodes(out document, out node11, out node12);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var nodes = document.Nodes()
                                .ToList();

            // Assert
            Assert.Equal(2, nodes.Count);
            Assert.Equal(node11.Name, nodes[0].Name);
            Assert.Equal(node12.Name, nodes[1].Name);
        }

        [Fact]
        public void TestNodesWithWithDepthOf1And3Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            CreateTreeWithDepthOf1And3Nodes(out document, out node11, out node12, out node13);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var nodes = document.Nodes()
                                .ToList();

            // Assert
            Assert.Equal(3, nodes.Count);
            Assert.Equal(node11.Name, nodes[0].Name);
            Assert.Equal(node12.Name, nodes[1].Name);
            Assert.Equal(node13.Name, nodes[2].Name);
        }

        [Fact]
        public void TestDescendentNodesWithEmptyTree()
        {
            // Arrange
            DocumentNode document;
            CreateTreeEmpty(out document);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(0, descendentNodes.Count);
        }

        [Fact]
        public void TestDescendentNodesWithDepthOf1And1Node()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            CreateTreeWithDepthOf1And1Node(out document, out node11);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(1, descendentNodes.Count);
            Assert.Equal(node11.Name, descendentNodes[0].Name);
        }

        [Fact]
        public void TestDescendentNodesWithDepthOf1And2Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            CreateTreeWithDepthOf1And2Nodes(out document, out node11, out node12);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(2, descendentNodes.Count);
            Assert.Equal(node11.Name, descendentNodes[0].Name);
            Assert.Equal(node12.Name, descendentNodes[1].Name);
        }

        [Fact]
        public void TestDescendentNodesWithDepthOf1And3Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            CreateTreeWithDepthOf1And3Nodes(out document, out node11, out node12, out node13);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(3, descendentNodes.Count);
            Assert.Equal(node11.Name, descendentNodes[0].Name);
            Assert.Equal(node12.Name, descendentNodes[1].Name);
            Assert.Equal(node13.Name, descendentNodes[2].Name);
        }

        [Fact]
        public void TestDescendentNodesWithDepthOf2And6Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            NodeContainer node21;
            NodeContainer node22;
            NodeContainer node23;
            CreateTreeWithDepthOf2And6Nodes(out document, out node11, out node12, out node13, out node21, out node22, out node23);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(6, descendentNodes.Count);
            Assert.Equal(node11.Name, descendentNodes[0].Name);
            Assert.Equal(node21.Name, descendentNodes[1].Name);
            Assert.Equal(node22.Name, descendentNodes[2].Name);
            Assert.Equal(node23.Name, descendentNodes[3].Name);
            Assert.Equal(node12.Name, descendentNodes[4].Name);
            Assert.Equal(node13.Name, descendentNodes[5].Name);
        }

        [Fact]
        public void TestDescendentNodesWithDepthOf3And9Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            NodeContainer node21;
            NodeContainer node22;
            NodeContainer node23;
            Node node31;
            Node node32;
            Node node33;
            CreateTreeWithDepthOf3And9Nodes(out document, out node11, out node12, out node13, out node21, out node22, out node23, out node31, out node32, out node33);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(9, descendentNodes.Count);
            Assert.Equal(node11.Name, descendentNodes[0].Name);
            Assert.Equal(node21.Name, descendentNodes[1].Name);
            Assert.Equal(node31.Name, descendentNodes[2].Name);
            Assert.Equal(node22.Name, descendentNodes[3].Name);
            Assert.Equal(node32.Name, descendentNodes[4].Name);
            Assert.Equal(node23.Name, descendentNodes[5].Name);
            Assert.Equal(node33.Name, descendentNodes[6].Name);
            Assert.Equal(node12.Name, descendentNodes[7].Name);
            Assert.Equal(node13.Name, descendentNodes[8].Name);
        }

        [Fact]
        public void TestRemoveNodeWithDepthOf1And1Node()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            CreateTreeWithDepthOf1And1Node(out document, out oldNode11);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            document.RemoveNode(oldNode11);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(0, descendentNodes.Count);
        }

        [Fact]
        public void TestRemoveNode1WithDepthOf1And2Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            NodesContainer oldNode12;
            CreateTreeWithDepthOf1And2Nodes(out document, out oldNode11, out oldNode12);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            document.RemoveNode(oldNode11);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(1, descendentNodes.Count);
            Assert.Equal(oldNode12.Name, descendentNodes[0].Name);
        }

        [Fact]
        public void TestRemoveNode2WithDepthOf1And2Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            NodesContainer oldNode12;
            CreateTreeWithDepthOf1And2Nodes(out document, out oldNode11, out oldNode12);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            document.RemoveNode(oldNode12);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(1, descendentNodes.Count);
            Assert.Equal(oldNode11.Name, descendentNodes[0].Name);
        }

        [Fact]
        public void TestRemoveNode1WithDepthOf1And3Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            NodesContainer oldNode12;
            NodesContainer oldNode13;
            CreateTreeWithDepthOf1And3Nodes(out document, out oldNode11, out oldNode12, out oldNode13);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            document.RemoveNode(oldNode11);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(2, descendentNodes.Count);
            Assert.Equal(oldNode12.Name, descendentNodes[0].Name);
            Assert.Equal(oldNode13.Name, descendentNodes[1].Name);
        }

        [Fact]
        public void TestRemoveNode2WithDepthOf1And3Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            NodesContainer oldNode12;
            NodesContainer oldNode13;
            CreateTreeWithDepthOf1And3Nodes(out document, out oldNode11, out oldNode12, out oldNode13);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            document.RemoveNode(oldNode12);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(2, descendentNodes.Count);
            Assert.Equal(oldNode11.Name, descendentNodes[0].Name);
            Assert.Equal(oldNode13.Name, descendentNodes[1].Name);
        }

        [Fact]
        public void TestRemoveNode3WithDepthOf1And3Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            NodesContainer oldNode12;
            NodesContainer oldNode13;
            CreateTreeWithDepthOf1And3Nodes(out document, out oldNode11, out oldNode12, out oldNode13);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            document.RemoveNode(oldNode13);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(2, descendentNodes.Count);
            Assert.Equal(oldNode11.Name, descendentNodes[0].Name);
            Assert.Equal(oldNode12.Name, descendentNodes[1].Name);
        }

        [Fact]
        public void TestReplaceNodeWithDepthOf1And1Node()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            CreateTreeWithDepthOf1And1Node(out document, out oldNode11);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            var newNode11 = new NodesContainer("NEW 1-1");
            document.ReplaceNode(oldNode11, newNode11);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(1, descendentNodes.Count);
            Assert.Equal(newNode11.Name, descendentNodes[0].Name);
        }

        [Fact]
        public void TestReplaceNodeWithDepthOf1And2Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            NodesContainer oldNode12;
            CreateTreeWithDepthOf1And2Nodes(out document, out oldNode11, out oldNode12);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            var newNode11 = new NodesContainer("NEW 1-1");
            document.ReplaceNode(oldNode11, newNode11);

            var newNode12 = new NodesContainer("NEW 1-2");
            document.ReplaceNode(oldNode12, newNode12);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(2, descendentNodes.Count);
            Assert.Equal(newNode11.Name, descendentNodes[0].Name);
            Assert.Equal(newNode12.Name, descendentNodes[1].Name);
        }

        [Fact]
        public void TestReplaceNodeWithDepthOf1And3Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            NodesContainer oldNode12;
            NodesContainer oldNode13;
            CreateTreeWithDepthOf1And3Nodes(out document, out oldNode11, out oldNode12, out oldNode13);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            var newNode11 = new NodesContainer("NEW 1-1");
            document.ReplaceNode(oldNode11, newNode11);

            var newNode12 = new NodesContainer("NEW 1-2");
            document.ReplaceNode(oldNode12, newNode12);

            var newNode13 = new NodesContainer("NEW 1-3");
            document.ReplaceNode(oldNode13, newNode13);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(3, descendentNodes.Count);
            Assert.Equal(newNode11.Name, descendentNodes[0].Name);
            Assert.Equal(newNode12.Name, descendentNodes[1].Name);
            Assert.Equal(newNode13.Name, descendentNodes[2].Name);
        }

        [Fact]
        public void TestReplaceNodeWithDepthOf2And6Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            NodesContainer oldNode12;
            NodesContainer oldNode13;
            NodeContainer oldNode21;
            NodeContainer oldNode22;
            NodeContainer oldNode23;
            CreateTreeWithDepthOf2And6Nodes(out document, out oldNode11, out oldNode12, out oldNode13, out oldNode21, out oldNode22, out oldNode23);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            var newNode11 = new NodesContainer("NEW 1-1");
            document.ReplaceNode(oldNode11, newNode11);

            var newNode12 = new NodesContainer("NEW 1-2");
            document.ReplaceNode(oldNode12, newNode12);

            var newNode13 = new NodesContainer("NEW 1-3");
            document.ReplaceNode(oldNode13, newNode13);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(3, descendentNodes.Count);
            Assert.Equal(newNode11.Name, descendentNodes[0].Name);
            Assert.Equal(newNode12.Name, descendentNodes[1].Name);
            Assert.Equal(newNode13.Name, descendentNodes[2].Name);
        }

        [Fact]
        public void TestReplaceNodeWithDepthOf3And9Nodes()
        {
            // Arrange
            DocumentNode document;
            NodesContainer oldNode11;
            NodesContainer oldNode12;
            NodesContainer oldNode13;
            NodeContainer oldNode21;
            NodeContainer oldNode22;
            NodeContainer oldNode23;
            Node oldNode31;
            Node oldNode32;
            Node oldNode33;
            CreateTreeWithDepthOf3And9Nodes(out document, out oldNode11, out oldNode12, out oldNode13, out oldNode21, out oldNode22, out oldNode23, out oldNode31, out oldNode32, out oldNode33);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            var newNode21 = new NodeContainer("NEW 2-1");
            oldNode11.ReplaceNode(oldNode21, newNode21);

            var newNode22 = new NodeContainer("NEW 2-2");
            oldNode11.ReplaceNode(oldNode22, newNode22);

            var newNode33 = new NodeContainer("NEW 3-3");
            oldNode23.ReplaceNode(oldNode33, newNode33);

            OutputTree(this, AfterObjectTreeHeader, document);

            var descendentNodes = document.DescendantNodes()
                                          .ToList();

            // Assert
            Assert.Equal(7, descendentNodes.Count);
            Assert.Equal(oldNode11.Name, descendentNodes[0].Name);
            Assert.Equal(newNode21.Name, descendentNodes[1].Name);
            Assert.Equal(newNode22.Name, descendentNodes[2].Name);
            Assert.Equal(oldNode23.Name, descendentNodes[3].Name);
            Assert.Equal(newNode33.Name, descendentNodes[4].Name);
            Assert.Equal(oldNode12.Name, descendentNodes[5].Name);
            Assert.Equal(oldNode13.Name, descendentNodes[6].Name);
        }

        [Fact]
        public void TestCreateAndAddNode()
        {
            // Arrange
            DocumentNode document;
            CreateTreeEmpty(out document);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            var beforeContainsNode = document.ContainsNode(TreeTestTypes.TestNodeType.NodesContainer);
            var nodesContainer = document.CreateAndAddNode(() => new NodesContainer("1-1"));
            var afterContainsNode = document.ContainsNode(TreeTestTypes.TestNodeType.NodesContainer);

            OutputTree(this, AfterObjectTreeHeader, document);

            // Assert
            Assert.False(beforeContainsNode);
            Assert.True(afterContainsNode);

            Assert.NotNull(nodesContainer);
            Assert.IsType<NodesContainer>(nodesContainer);
            Assert.Equal("1-1", nodesContainer.Name);
        }

        [Fact]
        public void TestCreateAndAddNodeMultipleTimes()
        {
            // Arrange
            DocumentNode document;
            CreateTreeEmpty(out document);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            var beforeNodeCount = document.Nodes().Count(x => x.NodeType == TreeTestTypes.TestNodeType.NodesContainer);
            var nodesContainer1 = document.CreateAndAddNode(() => new NodesContainer("1-1"));
            var nodesContainer2 = document.CreateAndAddNode(() => new NodesContainer("1-2"));
            var nodesContainer3 = document.CreateAndAddNode(() => new NodesContainer("1-3"));
            var nodesContainer4 = document.CreateAndAddNode(() => new NodesContainer("1-4"));
            var afterNodeCount = document.Nodes().Count(x => x.NodeType == TreeTestTypes.TestNodeType.NodesContainer);

            OutputTree(this, AfterObjectTreeHeader, document);

            // Assert
            Assert.Equal(0, beforeNodeCount);
            Assert.Equal(4, afterNodeCount);

            Assert.IsType<NodesContainer>(nodesContainer1);
            Assert.Equal("1-1", nodesContainer1.Name);

            Assert.IsType<NodesContainer>(nodesContainer2);
            Assert.Equal("1-2", nodesContainer2.Name);

            Assert.IsType<NodesContainer>(nodesContainer3);
            Assert.Equal("1-3", nodesContainer3.Name);

            Assert.IsType<NodesContainer>(nodesContainer4);
            Assert.Equal("1-4", nodesContainer4.Name);
        }

        [Fact]
        public void TestContainsNodeWithNodeTypeThatExists()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            CreateTreeWithDepthOf1And3Nodes(out document, out node11, out node12, out node13);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var treeContainsNodesContainer = document.ContainsNode(TreeTestTypes.TestNodeType.NodesContainer, EqualityComparer<TreeTestTypes.TestNodeType>.Default);

            // Assert
            Assert.True(treeContainsNodesContainer);
        }

        [Fact]
        public void TestContainsNodeWithNodeTypeThatDoesNotExist()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            NodesContainer node12;
            NodesContainer node13;
            CreateTreeWithDepthOf1And3Nodes(out document, out node11, out node12, out node13);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var treeContainsNodeContainer = document.ContainsNode(TreeTestTypes.TestNodeType.NodeContainer, EqualityComparer<TreeTestTypes.TestNodeType>.Default);

            // Assert
            Assert.False(treeContainsNodeContainer);
        }

        [Fact]
        public void TestGetNodeNonGenericWithNodeTypeThatExists()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            CreateTreeWithDepthOf1And1Node(out document, out node11);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var node = document.GetNode(TreeTestTypes.TestNodeType.NodesContainer, EqualityComparer<TreeTestTypes.TestNodeType>.Default);

            // Assert
            Assert.NotNull(node);
            Assert.True(node.NodeType == TreeTestTypes.TestNodeType.NodesContainer);
        }

        [Fact]
        public void TestGetNodeNonGenericWithNodeTypeThatDoesNotExist()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            CreateTreeWithDepthOf1And1Node(out document, out node11);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var node = document.GetNode(TreeTestTypes.TestNodeType.NodeContainer, EqualityComparer<TreeTestTypes.TestNodeType>.Default);

            // Assert
            Assert.Null(node);
        }

        [Fact]
        public void TestGetNodeGenericWithNodeTypeThatExists()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            CreateTreeWithDepthOf1And1Node(out document, out node11);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var nodesContainer = document.GetNode<TreeTestTypes.TestNodeType, NodesContainer>(TreeTestTypes.TestNodeType.NodesContainer, EqualityComparer<TreeTestTypes.TestNodeType>.Default);

            // Assert
            Assert.NotNull(nodesContainer);
            Assert.IsType<NodesContainer>(nodesContainer);
        }

        [Fact]
        public void TestGetNodeGenericWithNodeTypeThatDoesNotExist()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            CreateTreeWithDepthOf1And1Node(out document, out node11);

            // Act
            OutputTree(this, ObjectTreeHeader, document);

            var node = document.GetNode<TreeTestTypes.TestNodeType, NodesContainer>(TreeTestTypes.TestNodeType.NodeContainer, EqualityComparer<TreeTestTypes.TestNodeType>.Default);

            // Assert
            Assert.Null(node);
        }

        [Fact]
        public void TestGetOrAddWithNodeTypeThatExists()
        {
            // Arrange
            DocumentNode document;
            NodesContainer node11;
            CreateTreeWithDepthOf1And1Node(out document, out node11);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            var beforeContainsNode = document.ContainsNode(TreeTestTypes.TestNodeType.NodesContainer);
            var nodesContainer = document.GetOrAddNode(TreeTestTypes.TestNodeType.NodesContainer, () => new NodesContainer("Foo"), EqualityComparer<TreeTestTypes.TestNodeType>.Default);
            var afterContainsNode = document.ContainsNode(TreeTestTypes.TestNodeType.NodesContainer);

            OutputTree(this, AfterObjectTreeHeader, document);

            // Assert
            Assert.True(beforeContainsNode);
            Assert.True(afterContainsNode);

            Assert.NotNull(nodesContainer);
            Assert.IsType<NodesContainer>(nodesContainer);
            Assert.Equal(node11.Name, nodesContainer.Name);
        }

        [Fact]
        public void TestGetOrAddNodeWithNodeTypeThatDoesNotExist()
        {
            // Arrange
            DocumentNode document;
            CreateTreeEmpty(out document);

            // Act
            OutputTree(this, BeforeObjectTreeHeader, document);

            var beforeContainsNode = document.ContainsNode(TreeTestTypes.TestNodeType.NodesContainer);
            var nodesContainer = document.GetOrAddNode(TreeTestTypes.TestNodeType.NodesContainer, () => new NodesContainer("Foo"), EqualityComparer<TreeTestTypes.TestNodeType>.Default);
            var afterContainsNode = document.ContainsNode(TreeTestTypes.TestNodeType.NodesContainer);

            OutputTree(this, AfterObjectTreeHeader, document);

            // Assert
            Assert.False(beforeContainsNode);
            Assert.True(afterContainsNode);

            Assert.NotNull(nodesContainer);
            Assert.IsType<NodesContainer>(nodesContainer);
            Assert.Equal("Foo", nodesContainer.Name);
        }

        [Fact]
        public void TestCreateNodeWith1Attribute()
        {
            // Arrange
            var node = new Node("node");
            var attribute = new NodeAttribute("name", "value");

            // Act
            OutputTree(this, BeforeObjectTreeHeader, node);

            var beforeAttributes = node.Attributes()
                                       .ToList();

            node.Add(attribute);

            var afterAttributes = node.Attributes()
                                      .ToList();

            OutputTree(this, AfterObjectTreeHeader, node);

            // Assert
            Assert.Equal(0, beforeAttributes.Count);
            Assert.Equal(1, afterAttributes.Count);

            Assert.Equal(attribute, afterAttributes[0]);
        }

        [Fact]
        public void TestCreateNodeWith2Attributes()
        {
            // Arrange
            var node = new Node("node");
            var attribute1 = new NodeAttribute("name1", "value1");
            var attribute2 = new NodeAttribute("name2", "value2");

            // Act
            OutputTree(this, BeforeObjectTreeHeader, node);

            var beforeAttributes = node.Attributes()
                                       .ToList();

            node.Add(attribute1);
            node.Add(attribute2);

            var afterAttributes = node.Attributes()
                                      .ToList();

            OutputTree(this, AfterObjectTreeHeader, node);

            // Assert
            Assert.Equal(0, beforeAttributes.Count);
            Assert.Equal(2, afterAttributes.Count);

            Assert.Equal(attribute1, afterAttributes[0]);
            Assert.Equal(attribute2, afterAttributes[1]);
        }

        [Fact]
        public void TestCreateNodeWith3Attributes()
        {
            // Arrange
            var node = new Node("node");
            var attribute1 = new NodeAttribute("name1", "value1");
            var attribute2 = new NodeAttribute("name2", "value2");
            var attribute3 = new NodeAttribute("name3", "value3");

            // Act
            OutputTree(this, BeforeObjectTreeHeader, node);

            var beforeAttributes = node.Attributes()
                                       .ToList();

            node.Add(attribute1);
            node.Add(attribute2);
            node.Add(attribute3);

            var afterAttributes = node.Attributes()
                                      .ToList();

            OutputTree(this, AfterObjectTreeHeader, node);

            // Assert
            Assert.Equal(0, beforeAttributes.Count);
            Assert.Equal(3, afterAttributes.Count);

            Assert.Equal(attribute1, afterAttributes[0]);
            Assert.Equal(attribute2, afterAttributes[1]);
            Assert.Equal(attribute3, afterAttributes[2]);
        }

        [Fact]
        public void TestCreateNodeWith4Attributes()
        {
            // Arrange
            var node = new Node("node");
            var attribute1 = new NodeAttribute("name1", "value1");
            var attribute2 = new NodeAttribute("name2", "value2");
            var attribute3 = new NodeAttribute("name3", "value3");
            var attribute4 = new NodeAttribute("name4", "value4");

            // Act
            OutputTree(this, BeforeObjectTreeHeader, node);

            var beforeAttributes = node.Attributes()
                                       .ToList();

            node.Add(attribute1);
            node.Add(attribute2);
            node.Add(attribute3);
            node.Add(attribute4);

            var afterAttributes = node.Attributes()
                                      .ToList();

            OutputTree(this, AfterObjectTreeHeader, node);

            // Assert
            Assert.Equal(0, beforeAttributes.Count);
            Assert.Equal(4, afterAttributes.Count);

            Assert.Equal(attribute1, afterAttributes[0]);
            Assert.Equal(attribute2, afterAttributes[1]);
            Assert.Equal(attribute3, afterAttributes[2]);
            Assert.Equal(attribute4, afterAttributes[3]);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static void CreateTreeEmpty(out DocumentNode document)
        {
            document = DocumentNode.Create();
        }

        private static void CreateTreeWithDepthOf1And1Node(
            out DocumentNode document,
            out NodesContainer node11)
        {
            node11 = new NodesContainer("1-1");

            document = DocumentNode.Create(node11);
        }

        private static void CreateTreeWithDepthOf1And2Nodes(
            out DocumentNode document,
            out NodesContainer node11,
            out NodesContainer node12)
        {
            node11 = new NodesContainer("1-1");
            node12 = new NodesContainer("1-2");

            document = DocumentNode.Create(node11, node12);
        }

        private static void CreateTreeWithDepthOf1And3Nodes(
            out DocumentNode document,
            out NodesContainer node11,
            out NodesContainer node12,
            out NodesContainer node13)
        {
            node11 = new NodesContainer("1-1");
            node12 = new NodesContainer("1-2");
            node13 = new NodesContainer("1-3");

            document = DocumentNode.Create(node11, node12, node13);
        }

        private static void CreateTreeWithDepthOf2And4Nodes(
            out DocumentNode document,
            out NodesContainer node11,
            out NodesContainer node12,
            out NodesContainer node13,
            out NodeContainer node21)
        {
            node21 = new NodeContainer("2-1");

            node11 = new NodesContainer("1-1", node21);
            node12 = new NodesContainer("1-2");
            node13 = new NodesContainer("1-3");

            document = DocumentNode.Create(node11, node12, node13);
        }

        private static void CreateTreeWithDepthOf2And5Nodes(
            out DocumentNode document,
            out NodesContainer node11,
            out NodesContainer node12,
            out NodesContainer node13,
            out NodeContainer node21,
            out NodeContainer node22)
        {
            node21 = new NodeContainer("2-1");
            node22 = new NodeContainer("2-2");

            node11 = new NodesContainer("1-1", node21, node22);
            node12 = new NodesContainer("1-2");
            node13 = new NodesContainer("1-3");

            document = DocumentNode.Create(node11, node12, node13);
        }

        private static void CreateTreeWithDepthOf2And6Nodes(
            out DocumentNode document,
            out NodesContainer node11,
            out NodesContainer node12,
            out NodesContainer node13,
            out NodeContainer node21,
            out NodeContainer node22,
            out NodeContainer node23)
        {
            node21 = new NodeContainer("2-1");
            node22 = new NodeContainer("2-2");
            node23 = new NodeContainer("2-3");

            node11 = new NodesContainer("1-1", node21, node22, node23);
            node12 = new NodesContainer("1-2");
            node13 = new NodesContainer("1-3");

            document = DocumentNode.Create(node11, node12, node13);
        }

        private static void CreateTreeWithDepthOf3And7Nodes(
            out DocumentNode document,
            out NodesContainer node11,
            out NodesContainer node12,
            out NodesContainer node13,
            out NodeContainer node21,
            out NodeContainer node22,
            out NodeContainer node23,
            out Node node31)
        {
            node31 = new Node("3-1");

            node21 = new NodeContainer("2-1", node31);
            node22 = new NodeContainer("2-2");
            node23 = new NodeContainer("2-3");

            node11 = new NodesContainer("1-1", node21, node22, node23);
            node12 = new NodesContainer("1-2");
            node13 = new NodesContainer("1-3");

            document = DocumentNode.Create(node11, node12, node13);
        }

        private static void CreateTreeWithDepthOf3And8Nodes(
            out DocumentNode document,
            out NodesContainer node11,
            out NodesContainer node12,
            out NodesContainer node13,
            out NodeContainer node21,
            out NodeContainer node22,
            out NodeContainer node23,
            out Node node31,
            out Node node32)
        {
            node31 = new Node("3-1");
            node32 = new Node("3-2");

            node21 = new NodeContainer("2-1", node31);
            node22 = new NodeContainer("2-2", node32);
            node23 = new NodeContainer("2-3");

            node11 = new NodesContainer("1-1", node21, node22, node23);
            node12 = new NodesContainer("1-2");
            node13 = new NodesContainer("1-3");

            document = DocumentNode.Create(node11, node12, node13);
        }

        private static void CreateTreeWithDepthOf3And9Nodes(
            out DocumentNode document,
            out NodesContainer node11,
            out NodesContainer node12,
            out NodesContainer node13,
            out NodeContainer node21,
            out NodeContainer node22,
            out NodeContainer node23,
            out Node node31,
            out Node node32,
            out Node node33)
        {
            node31 = new Node("3-1");
            node32 = new Node("3-2");
            node33 = new Node("3-3");

            node21 = new NodeContainer("2-1", node31);
            node22 = new NodeContainer("2-2", node32);
            node23 = new NodeContainer("2-3", node33);

            node11 = new NodesContainer("1-1", node21, node22, node23);
            node12 = new NodesContainer("1-2");
            node13 = new NodesContainer("1-3");

            document = DocumentNode.Create(node11, node12, node13);
        }

        private static void OutputTree(TreeTests test, string header, Node<TreeTestTypes.TestNodeType> node)
        {
            var treeString = node.ToTreeString();

            test.Output.WriteLine(header);
            test.Output.WriteLine(String.Empty);
            test.Output.WriteLine(treeString);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private const string ObjectTreeHeader = "Object Tree";

        private const string BeforeObjectTreeHeader = "Before Object Tree";
        private const string AfterObjectTreeHeader = "After Object Tree";
        #endregion
    }
}
