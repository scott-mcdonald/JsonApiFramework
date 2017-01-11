// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using FluentAssertions;

using JsonApiFramework.Tree;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Tree
{
    public class TreeTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TreeTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region AddNode Test Methods
        [Fact]
        public void TestTreeAddNodeWith1MaxDepth()
        {
            // Arrange
            const int maxDepth = 1;
            const int maxChildren = 0;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeDictionary.Should().HaveCount(1);
            nodeDictionary.Should().ContainKey("1");
            var node1 = nodeDictionary["1"];

            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeNull();
            node1.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeWith2MaxDepthAnd1MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 1;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeDictionary.Should().HaveCount(2);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];

            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node11);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeNull();
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeWith2MaxDepthAnd2MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 2;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeDictionary.Should().HaveCount(3);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];

            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node12);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeNull();
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeWith2MaxDepthAnd3MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 3;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeDictionary.Should().HaveCount(4);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];

            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node13);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeNull();
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeWith2MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeDictionary.Should().HaveCount(5);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-4");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];
            var node14 = nodeDictionary["1-4"];

            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node14);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeSameAs(node14);
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();

            node14.Name.Should().Be("1-4");
            node14.RootNode.Should().BeSameAs(tree);
            node14.ParentNode.Should().BeSameAs(node1);
            node14.PreviousNode.Should().BeSameAs(node13);
            node14.NextNode.Should().BeNull();
            node14.FirstNode.Should().BeNull();
            node14.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeWith3MaxDepthAnd1MaxChildren()
        {
            // Arrange
            const int maxDepth = 3;
            const int maxChildren = 1;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeDictionary.Should().HaveCount(3);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-1-1");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node111 = nodeDictionary["1-1-1"];

            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node11);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeNull();
            node11.FirstNode.Should().BeSameAs(node111);
            node11.LastNode.Should().BeSameAs(node111);

            node111.Name.Should().Be("1-1-1");
            node111.RootNode.Should().BeSameAs(tree);
            node111.ParentNode.Should().BeSameAs(node11);
            node111.PreviousNode.Should().BeNull();
            node111.NextNode.Should().BeNull();
            node111.FirstNode.Should().BeNull();
            node111.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeWith3MaxDepthAnd2MaxChildren()
        {
            // Arrange
            const int maxDepth = 3;
            const int maxChildren = 2;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeDictionary.Should().HaveCount(7);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-1-1");
            nodeDictionary.Should().ContainKey("1-1-2");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-2-1");
            nodeDictionary.Should().ContainKey("1-2-2");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node111 = nodeDictionary["1-1-1"];
            var node112 = nodeDictionary["1-1-2"];
            var node12 = nodeDictionary["1-2"];
            var node121 = nodeDictionary["1-2-1"];
            var node122 = nodeDictionary["1-2-2"];

            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node12);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeSameAs(node111);
            node11.LastNode.Should().BeSameAs(node112);

            node111.Name.Should().Be("1-1-1");
            node111.RootNode.Should().BeSameAs(tree);
            node111.ParentNode.Should().BeSameAs(node11);
            node111.PreviousNode.Should().BeNull();
            node111.NextNode.Should().BeSameAs(node112);
            node111.FirstNode.Should().BeNull();
            node111.LastNode.Should().BeNull();

            node112.Name.Should().Be("1-1-2");
            node112.RootNode.Should().BeSameAs(tree);
            node112.ParentNode.Should().BeSameAs(node11);
            node112.PreviousNode.Should().BeSameAs(node111);
            node112.NextNode.Should().BeNull();
            node112.FirstNode.Should().BeNull();
            node112.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeNull();
            node12.FirstNode.Should().BeSameAs(node121);
            node12.LastNode.Should().BeSameAs(node122);

            node121.Name.Should().Be("1-2-1");
            node121.RootNode.Should().BeSameAs(tree);
            node121.ParentNode.Should().BeSameAs(node12);
            node121.PreviousNode.Should().BeNull();
            node121.NextNode.Should().BeSameAs(node122);
            node121.FirstNode.Should().BeNull();
            node121.LastNode.Should().BeNull();

            node122.Name.Should().Be("1-2-2");
            node122.RootNode.Should().BeSameAs(tree);
            node122.ParentNode.Should().BeSameAs(node12);
            node122.PreviousNode.Should().BeSameAs(node121);
            node122.NextNode.Should().BeNull();
            node122.FirstNode.Should().BeNull();
            node122.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeWith3MaxDepthAnd3MaxChildren()
        {
            // Arrange
            const int maxDepth = 3;
            const int maxChildren = 3;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeDictionary.Should().HaveCount(13);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-1-1");
            nodeDictionary.Should().ContainKey("1-1-2");
            nodeDictionary.Should().ContainKey("1-1-3");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-2-1");
            nodeDictionary.Should().ContainKey("1-2-2");
            nodeDictionary.Should().ContainKey("1-2-3");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-3-1");
            nodeDictionary.Should().ContainKey("1-3-2");
            nodeDictionary.Should().ContainKey("1-3-3");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node111 = nodeDictionary["1-1-1"];
            var node112 = nodeDictionary["1-1-2"];
            var node113 = nodeDictionary["1-1-3"];
            var node12 = nodeDictionary["1-2"];
            var node121 = nodeDictionary["1-2-1"];
            var node122 = nodeDictionary["1-2-2"];
            var node123 = nodeDictionary["1-2-3"];
            var node13 = nodeDictionary["1-3"];
            var node131 = nodeDictionary["1-3-1"];
            var node132 = nodeDictionary["1-3-2"];
            var node133 = nodeDictionary["1-3-3"];

            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node13);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeSameAs(node111);
            node11.LastNode.Should().BeSameAs(node113);

            node111.Name.Should().Be("1-1-1");
            node111.RootNode.Should().BeSameAs(tree);
            node111.ParentNode.Should().BeSameAs(node11);
            node111.PreviousNode.Should().BeNull();
            node111.NextNode.Should().BeSameAs(node112);
            node111.FirstNode.Should().BeNull();
            node111.LastNode.Should().BeNull();

            node112.Name.Should().Be("1-1-2");
            node112.RootNode.Should().BeSameAs(tree);
            node112.ParentNode.Should().BeSameAs(node11);
            node112.PreviousNode.Should().BeSameAs(node111);
            node112.NextNode.Should().BeSameAs(node113);
            node112.FirstNode.Should().BeNull();
            node112.LastNode.Should().BeNull();

            node113.Name.Should().Be("1-1-3");
            node113.RootNode.Should().BeSameAs(tree);
            node113.ParentNode.Should().BeSameAs(node11);
            node113.PreviousNode.Should().BeSameAs(node112);
            node113.NextNode.Should().BeNull();
            node113.FirstNode.Should().BeNull();
            node113.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeSameAs(node121);
            node12.LastNode.Should().BeSameAs(node123);

            node121.Name.Should().Be("1-2-1");
            node121.RootNode.Should().BeSameAs(tree);
            node121.ParentNode.Should().BeSameAs(node12);
            node121.PreviousNode.Should().BeNull();
            node121.NextNode.Should().BeSameAs(node122);
            node121.FirstNode.Should().BeNull();
            node121.LastNode.Should().BeNull();

            node122.Name.Should().Be("1-2-2");
            node122.RootNode.Should().BeSameAs(tree);
            node122.ParentNode.Should().BeSameAs(node12);
            node122.PreviousNode.Should().BeSameAs(node121);
            node122.NextNode.Should().BeSameAs(node123);
            node122.FirstNode.Should().BeNull();
            node122.LastNode.Should().BeNull();

            node123.Name.Should().Be("1-2-3");
            node123.RootNode.Should().BeSameAs(tree);
            node123.ParentNode.Should().BeSameAs(node12);
            node123.PreviousNode.Should().BeSameAs(node122);
            node123.NextNode.Should().BeNull();
            node123.FirstNode.Should().BeNull();
            node123.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeNull();
            node13.FirstNode.Should().BeSameAs(node131);
            node13.LastNode.Should().BeSameAs(node133);

            node131.Name.Should().Be("1-3-1");
            node131.RootNode.Should().BeSameAs(tree);
            node131.ParentNode.Should().BeSameAs(node13);
            node131.PreviousNode.Should().BeNull();
            node131.NextNode.Should().BeSameAs(node132);
            node131.FirstNode.Should().BeNull();
            node131.LastNode.Should().BeNull();

            node132.Name.Should().Be("1-3-2");
            node132.RootNode.Should().BeSameAs(tree);
            node132.ParentNode.Should().BeSameAs(node13);
            node132.PreviousNode.Should().BeSameAs(node131);
            node132.NextNode.Should().BeSameAs(node133);
            node132.FirstNode.Should().BeNull();
            node132.LastNode.Should().BeNull();

            node133.Name.Should().Be("1-3-3");
            node133.RootNode.Should().BeSameAs(tree);
            node133.ParentNode.Should().BeSameAs(node13);
            node133.PreviousNode.Should().BeSameAs(node132);
            node133.NextNode.Should().BeNull();
            node133.FirstNode.Should().BeNull();
            node133.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeWith3MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 3;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeDictionary.Should().HaveCount(21);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-1-1");
            nodeDictionary.Should().ContainKey("1-1-2");
            nodeDictionary.Should().ContainKey("1-1-3");
            nodeDictionary.Should().ContainKey("1-1-4");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-2-1");
            nodeDictionary.Should().ContainKey("1-2-2");
            nodeDictionary.Should().ContainKey("1-2-3");
            nodeDictionary.Should().ContainKey("1-2-4");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-3-1");
            nodeDictionary.Should().ContainKey("1-3-2");
            nodeDictionary.Should().ContainKey("1-3-3");
            nodeDictionary.Should().ContainKey("1-3-4");
            nodeDictionary.Should().ContainKey("1-4");
            nodeDictionary.Should().ContainKey("1-4-1");
            nodeDictionary.Should().ContainKey("1-4-2");
            nodeDictionary.Should().ContainKey("1-4-3");
            nodeDictionary.Should().ContainKey("1-4-4");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node111 = nodeDictionary["1-1-1"];
            var node112 = nodeDictionary["1-1-2"];
            var node113 = nodeDictionary["1-1-3"];
            var node114 = nodeDictionary["1-1-4"];
            var node12 = nodeDictionary["1-2"];
            var node121 = nodeDictionary["1-2-1"];
            var node122 = nodeDictionary["1-2-2"];
            var node123 = nodeDictionary["1-2-3"];
            var node124 = nodeDictionary["1-2-4"];
            var node13 = nodeDictionary["1-3"];
            var node131 = nodeDictionary["1-3-1"];
            var node132 = nodeDictionary["1-3-2"];
            var node133 = nodeDictionary["1-3-3"];
            var node134 = nodeDictionary["1-3-4"];
            var node14 = nodeDictionary["1-4"];
            var node141 = nodeDictionary["1-4-1"];
            var node142 = nodeDictionary["1-4-2"];
            var node143 = nodeDictionary["1-4-3"];
            var node144 = nodeDictionary["1-4-4"];

            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node14);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeSameAs(node111);
            node11.LastNode.Should().BeSameAs(node114);

            node111.Name.Should().Be("1-1-1");
            node111.RootNode.Should().BeSameAs(tree);
            node111.ParentNode.Should().BeSameAs(node11);
            node111.PreviousNode.Should().BeNull();
            node111.NextNode.Should().BeSameAs(node112);
            node111.FirstNode.Should().BeNull();
            node111.LastNode.Should().BeNull();

            node112.Name.Should().Be("1-1-2");
            node112.RootNode.Should().BeSameAs(tree);
            node112.ParentNode.Should().BeSameAs(node11);
            node112.PreviousNode.Should().BeSameAs(node111);
            node112.NextNode.Should().BeSameAs(node113);
            node112.FirstNode.Should().BeNull();
            node112.LastNode.Should().BeNull();

            node113.Name.Should().Be("1-1-3");
            node113.RootNode.Should().BeSameAs(tree);
            node113.ParentNode.Should().BeSameAs(node11);
            node113.PreviousNode.Should().BeSameAs(node112);
            node113.NextNode.Should().BeSameAs(node114);
            node113.FirstNode.Should().BeNull();
            node113.LastNode.Should().BeNull();

            node114.Name.Should().Be("1-1-4");
            node114.RootNode.Should().BeSameAs(tree);
            node114.ParentNode.Should().BeSameAs(node11);
            node114.PreviousNode.Should().BeSameAs(node113);
            node114.NextNode.Should().BeNull();
            node114.FirstNode.Should().BeNull();
            node114.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeSameAs(node121);
            node12.LastNode.Should().BeSameAs(node124);

            node121.Name.Should().Be("1-2-1");
            node121.RootNode.Should().BeSameAs(tree);
            node121.ParentNode.Should().BeSameAs(node12);
            node121.PreviousNode.Should().BeNull();
            node121.NextNode.Should().BeSameAs(node122);
            node121.FirstNode.Should().BeNull();
            node121.LastNode.Should().BeNull();

            node122.Name.Should().Be("1-2-2");
            node122.RootNode.Should().BeSameAs(tree);
            node122.ParentNode.Should().BeSameAs(node12);
            node122.PreviousNode.Should().BeSameAs(node121);
            node122.NextNode.Should().BeSameAs(node123);
            node122.FirstNode.Should().BeNull();
            node122.LastNode.Should().BeNull();

            node123.Name.Should().Be("1-2-3");
            node123.RootNode.Should().BeSameAs(tree);
            node123.ParentNode.Should().BeSameAs(node12);
            node123.PreviousNode.Should().BeSameAs(node122);
            node123.NextNode.Should().BeSameAs(node124);
            node123.FirstNode.Should().BeNull();
            node123.LastNode.Should().BeNull();

            node124.Name.Should().Be("1-2-4");
            node124.RootNode.Should().BeSameAs(tree);
            node124.ParentNode.Should().BeSameAs(node12);
            node124.PreviousNode.Should().BeSameAs(node123);
            node124.NextNode.Should().BeNull();
            node124.FirstNode.Should().BeNull();
            node124.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeSameAs(node14);
            node13.FirstNode.Should().BeSameAs(node131);
            node13.LastNode.Should().BeSameAs(node134);

            node131.Name.Should().Be("1-3-1");
            node131.RootNode.Should().BeSameAs(tree);
            node131.ParentNode.Should().BeSameAs(node13);
            node131.PreviousNode.Should().BeNull();
            node131.NextNode.Should().BeSameAs(node132);
            node131.FirstNode.Should().BeNull();
            node131.LastNode.Should().BeNull();

            node132.Name.Should().Be("1-3-2");
            node132.RootNode.Should().BeSameAs(tree);
            node132.ParentNode.Should().BeSameAs(node13);
            node132.PreviousNode.Should().BeSameAs(node131);
            node132.NextNode.Should().BeSameAs(node133);
            node132.FirstNode.Should().BeNull();
            node132.LastNode.Should().BeNull();

            node133.Name.Should().Be("1-3-3");
            node133.RootNode.Should().BeSameAs(tree);
            node133.ParentNode.Should().BeSameAs(node13);
            node133.PreviousNode.Should().BeSameAs(node132);
            node133.NextNode.Should().BeSameAs(node134);
            node133.FirstNode.Should().BeNull();
            node133.LastNode.Should().BeNull();

            node134.Name.Should().Be("1-3-4");
            node134.RootNode.Should().BeSameAs(tree);
            node134.ParentNode.Should().BeSameAs(node13);
            node134.PreviousNode.Should().BeSameAs(node133);
            node134.NextNode.Should().BeNull();
            node134.FirstNode.Should().BeNull();
            node134.LastNode.Should().BeNull();

            node14.Name.Should().Be("1-4");
            node14.RootNode.Should().BeSameAs(tree);
            node14.ParentNode.Should().BeSameAs(node1);
            node14.PreviousNode.Should().BeSameAs(node13);
            node14.NextNode.Should().BeNull();
            node14.FirstNode.Should().BeSameAs(node141);
            node14.LastNode.Should().BeSameAs(node144);

            node141.Name.Should().Be("1-4-1");
            node141.RootNode.Should().BeSameAs(tree);
            node141.ParentNode.Should().BeSameAs(node14);
            node141.PreviousNode.Should().BeNull();
            node141.NextNode.Should().BeSameAs(node142);
            node141.FirstNode.Should().BeNull();
            node141.LastNode.Should().BeNull();

            node142.Name.Should().Be("1-4-2");
            node142.RootNode.Should().BeSameAs(tree);
            node142.ParentNode.Should().BeSameAs(node14);
            node142.PreviousNode.Should().BeSameAs(node141);
            node142.NextNode.Should().BeSameAs(node143);
            node142.FirstNode.Should().BeNull();
            node142.LastNode.Should().BeNull();

            node143.Name.Should().Be("1-4-3");
            node143.RootNode.Should().BeSameAs(tree);
            node143.ParentNode.Should().BeSameAs(node14);
            node143.PreviousNode.Should().BeSameAs(node142);
            node143.NextNode.Should().BeSameAs(node144);
            node143.FirstNode.Should().BeNull();
            node143.LastNode.Should().BeNull();

            node144.Name.Should().Be("1-4-4");
            node144.RootNode.Should().BeSameAs(tree);
            node144.ParentNode.Should().BeSameAs(node14);
            node144.PreviousNode.Should().BeSameAs(node143);
            node144.NextNode.Should().BeNull();
            node144.FirstNode.Should().BeNull();
            node144.LastNode.Should().BeNull();
        }
        #endregion

        #region RemoveNode Test Methods
        [Fact]
        public void TestTreeRemoveNode11With2MaxDepthAnd1MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 1;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(2);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];

            node1.RemoveNode(node11);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeNull();
            node1.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNode11With2MaxDepthAnd2MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 2;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(3);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];

            node1.RemoveNode(node11);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node12);
            node1.LastNode.Should().BeSameAs(node12);

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeNull();
            node12.NextNode.Should().BeNull();
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNode12With2MaxDepthAnd2MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 2;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(3);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];

            node1.RemoveNode(node12);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node11);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeNull();
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNode11With2MaxDepthAnd3MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 3;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(4);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];

            node1.RemoveNode(node11);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node12);
            node1.LastNode.Should().BeSameAs(node13);

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeNull();
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeNull();
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNode12With2MaxDepthAnd3MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 3;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(4);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];

            node1.RemoveNode(node12);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node13);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node13);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node11);
            node13.NextNode.Should().BeNull();
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNode13With2MaxDepthAnd3MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 3;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(4);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];

            node1.RemoveNode(node13);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node12);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeNull();
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNode11With2MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(5);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-4");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];
            var node14 = nodeDictionary["1-4"];

            node1.RemoveNode(node11);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node12);
            node1.LastNode.Should().BeSameAs(node14);

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeNull();
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeSameAs(node14);
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();

            node14.Name.Should().Be("1-4");
            node14.RootNode.Should().BeSameAs(tree);
            node14.ParentNode.Should().BeSameAs(node1);
            node14.PreviousNode.Should().BeSameAs(node13);
            node14.NextNode.Should().BeNull();
            node14.FirstNode.Should().BeNull();
            node14.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNode12With2MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(5);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-4");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];
            var node14 = nodeDictionary["1-4"];

            node1.RemoveNode(node12);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node14);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node13);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node11);
            node13.NextNode.Should().BeSameAs(node14);
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();

            node14.Name.Should().Be("1-4");
            node14.RootNode.Should().BeSameAs(tree);
            node14.ParentNode.Should().BeSameAs(node1);
            node14.PreviousNode.Should().BeSameAs(node13);
            node14.NextNode.Should().BeNull();
            node14.FirstNode.Should().BeNull();
            node14.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNode13With2MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(5);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-4");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];
            var node14 = nodeDictionary["1-4"];

            node1.RemoveNode(node13);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node14);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeSameAs(node14);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            node14.Name.Should().Be("1-4");
            node14.RootNode.Should().BeSameAs(tree);
            node14.ParentNode.Should().BeSameAs(node1);
            node14.PreviousNode.Should().BeSameAs(node12);
            node14.NextNode.Should().BeNull();
            node14.FirstNode.Should().BeNull();
            node14.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNode14With2MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(5);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-4");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];
            var node14 = nodeDictionary["1-4"];

            node1.RemoveNode(node14);

            this.WriteLine();
            this.WriteLine("Tree After RemoveNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node13);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeNull();
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();
        }
        #endregion

        #region ReplaceNode Test Methods
        [Fact]
        public void TestTreeReplaceNode11With2MaxDepthAnd1MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 1;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(2);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            var node1 = nodeDictionary["1"];
            var oldNode11 = nodeDictionary["1-1"];
            var newNode11 = new Node("NEW 1-1");

            node1.ReplaceNode(oldNode11, newNode11);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(newNode11);
            node1.LastNode.Should().BeSameAs(newNode11);

            newNode11.Name.Should().Be("NEW 1-1");
            newNode11.RootNode.Should().BeSameAs(tree);
            newNode11.ParentNode.Should().BeSameAs(node1);
            newNode11.PreviousNode.Should().BeNull();
            newNode11.NextNode.Should().BeNull();
            newNode11.FirstNode.Should().BeNull();
            newNode11.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNode11With2MaxDepthAnd2MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 2;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(3);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            var node1 = nodeDictionary["1"];
            var oldNode11 = nodeDictionary["1-1"];
            var newNode11 = new Node("NEW 1-1");
            var node12 = nodeDictionary["1-2"];

            node1.ReplaceNode(oldNode11, newNode11);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(newNode11);
            node1.LastNode.Should().BeSameAs(node12);

            newNode11.Name.Should().Be("NEW 1-1");
            newNode11.RootNode.Should().BeSameAs(tree);
            newNode11.ParentNode.Should().BeSameAs(node1);
            newNode11.PreviousNode.Should().BeNull();
            newNode11.NextNode.Should().BeSameAs(node12);
            newNode11.FirstNode.Should().BeNull();
            newNode11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(newNode11);
            node12.NextNode.Should().BeNull();
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNode12With2MaxDepthAnd2MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 2;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(3);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var oldNode12 = nodeDictionary["1-2"];
            var newNode12 = new Node("NEW 1-2");

            node1.ReplaceNode(oldNode12, newNode12);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(newNode12);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(newNode12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            newNode12.Name.Should().Be("NEW 1-2");
            newNode12.RootNode.Should().BeSameAs(tree);
            newNode12.ParentNode.Should().BeSameAs(node1);
            newNode12.PreviousNode.Should().BeSameAs(node11);
            newNode12.NextNode.Should().BeNull();
            newNode12.FirstNode.Should().BeNull();
            newNode12.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNode11With2MaxDepthAnd3MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 3;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(4);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            var node1 = nodeDictionary["1"];
            var oldNode11 = nodeDictionary["1-1"];
            var newNode11 = new Node("NEW 1-1");
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];

            node1.ReplaceNode(oldNode11, newNode11);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(newNode11);
            node1.LastNode.Should().BeSameAs(node13);

            newNode11.Name.Should().Be("NEW 1-1");
            newNode11.RootNode.Should().BeSameAs(tree);
            newNode11.ParentNode.Should().BeSameAs(node1);
            newNode11.PreviousNode.Should().BeNull();
            newNode11.NextNode.Should().BeSameAs(node12);
            newNode11.FirstNode.Should().BeNull();
            newNode11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(newNode11);
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeNull();
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNode12With2MaxDepthAnd3MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 3;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(4);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var oldNode12 = nodeDictionary["1-2"];
            var newNode12 = new Node("NEW 1-2");
            var node13 = nodeDictionary["1-3"];

            node1.ReplaceNode(oldNode12, newNode12);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node13);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(newNode12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            newNode12.Name.Should().Be("NEW 1-2");
            newNode12.RootNode.Should().BeSameAs(tree);
            newNode12.ParentNode.Should().BeSameAs(node1);
            newNode12.PreviousNode.Should().BeSameAs(node11);
            newNode12.NextNode.Should().BeSameAs(node13);
            newNode12.FirstNode.Should().BeNull();
            newNode12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(newNode12);
            node13.NextNode.Should().BeNull();
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNode13With2MaxDepthAnd3MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 3;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(4);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var oldNode13 = nodeDictionary["1-3"];
            var newNode13 = new Node("NEW 1-3");

            node1.ReplaceNode(oldNode13, newNode13);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(newNode13);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeSameAs(newNode13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            newNode13.Name.Should().Be("NEW 1-3");
            newNode13.RootNode.Should().BeSameAs(tree);
            newNode13.ParentNode.Should().BeSameAs(node1);
            newNode13.PreviousNode.Should().BeSameAs(node12);
            newNode13.NextNode.Should().BeNull();
            newNode13.FirstNode.Should().BeNull();
            newNode13.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNode11With2MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(5);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-4");
            var node1 = nodeDictionary["1"];
            var oldNode11 = nodeDictionary["1-1"];
            var newNode11 = new Node("NEW 1-1");
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];
            var node14 = nodeDictionary["1-4"];

            node1.ReplaceNode(oldNode11, newNode11);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(newNode11);
            node1.LastNode.Should().BeSameAs(node14);

            newNode11.Name.Should().Be("NEW 1-1");
            newNode11.RootNode.Should().BeSameAs(tree);
            newNode11.ParentNode.Should().BeSameAs(node1);
            newNode11.PreviousNode.Should().BeNull();
            newNode11.NextNode.Should().BeSameAs(node12);
            newNode11.FirstNode.Should().BeNull();
            newNode11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(newNode11);
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeSameAs(node14);
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();

            node14.Name.Should().Be("1-4");
            node14.RootNode.Should().BeSameAs(tree);
            node14.ParentNode.Should().BeSameAs(node1);
            node14.PreviousNode.Should().BeSameAs(node13);
            node14.NextNode.Should().BeNull();
            node14.FirstNode.Should().BeNull();
            node14.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNode12With2MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(5);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-4");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var oldNode12 = nodeDictionary["1-2"];
            var newNode12 = new Node("NEW 1-2");
            var node13 = nodeDictionary["1-3"];
            var node14 = nodeDictionary["1-4"];

            node1.ReplaceNode(oldNode12, newNode12);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node14);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(newNode12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            newNode12.Name.Should().Be("NEW 1-2");
            newNode12.RootNode.Should().BeSameAs(tree);
            newNode12.ParentNode.Should().BeSameAs(node1);
            newNode12.PreviousNode.Should().BeSameAs(node11);
            newNode12.NextNode.Should().BeSameAs(node13);
            newNode12.FirstNode.Should().BeNull();
            newNode12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(newNode12);
            node13.NextNode.Should().BeSameAs(node14);
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();

            node14.Name.Should().Be("1-4");
            node14.RootNode.Should().BeSameAs(tree);
            node14.ParentNode.Should().BeSameAs(node1);
            node14.PreviousNode.Should().BeSameAs(node13);
            node14.NextNode.Should().BeNull();
            node14.FirstNode.Should().BeNull();
            node14.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNode13With2MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(5);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-4");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var oldNode13 = nodeDictionary["1-3"];
            var newNode13 = new Node("NEW 1-3");
            var node14 = nodeDictionary["1-4"];

            node1.ReplaceNode(oldNode13, newNode13);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(node14);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeSameAs(newNode13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            newNode13.Name.Should().Be("NEW 1-3");
            newNode13.RootNode.Should().BeSameAs(tree);
            newNode13.ParentNode.Should().BeSameAs(node1);
            newNode13.PreviousNode.Should().BeSameAs(node12);
            newNode13.NextNode.Should().BeSameAs(node14);
            newNode13.FirstNode.Should().BeNull();
            newNode13.LastNode.Should().BeNull();

            node14.Name.Should().Be("1-4");
            node14.RootNode.Should().BeSameAs(tree);
            node14.ParentNode.Should().BeSameAs(node1);
            node14.PreviousNode.Should().BeSameAs(newNode13);
            node14.NextNode.Should().BeNull();
            node14.FirstNode.Should().BeNull();
            node14.LastNode.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNode14With2MaxDepthAnd4MaxChildren()
        {
            // Arrange
            const int maxDepth = 2;
            const int maxChildren = 4;
            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            nodeDictionary.Should().HaveCount(5);
            nodeDictionary.Should().ContainKey("1");
            nodeDictionary.Should().ContainKey("1-1");
            nodeDictionary.Should().ContainKey("1-2");
            nodeDictionary.Should().ContainKey("1-3");
            nodeDictionary.Should().ContainKey("1-4");
            var node1 = nodeDictionary["1"];
            var node11 = nodeDictionary["1-1"];
            var node12 = nodeDictionary["1-2"];
            var node13 = nodeDictionary["1-3"];
            var oldNode14 = nodeDictionary["1-4"];
            var newNode14 = new Node("NEW 1-4");

            node1.ReplaceNode(oldNode14, newNode14);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceNode");
            this.WriteLine(tree.ToTreeString());

            // Assert
            node1.Name.Should().Be("1");
            node1.RootNode.Should().BeSameAs(tree);
            node1.ParentNode.Should().BeNull();
            node1.PreviousNode.Should().BeNull();
            node1.NextNode.Should().BeNull();
            node1.FirstNode.Should().BeSameAs(node11);
            node1.LastNode.Should().BeSameAs(newNode14);

            node11.Name.Should().Be("1-1");
            node11.RootNode.Should().BeSameAs(tree);
            node11.ParentNode.Should().BeSameAs(node1);
            node11.PreviousNode.Should().BeNull();
            node11.NextNode.Should().BeSameAs(node12);
            node11.FirstNode.Should().BeNull();
            node11.LastNode.Should().BeNull();

            node12.Name.Should().Be("1-2");
            node12.RootNode.Should().BeSameAs(tree);
            node12.ParentNode.Should().BeSameAs(node1);
            node12.PreviousNode.Should().BeSameAs(node11);
            node12.NextNode.Should().BeSameAs(node13);
            node12.FirstNode.Should().BeNull();
            node12.LastNode.Should().BeNull();

            node13.Name.Should().Be("1-3");
            node13.RootNode.Should().BeSameAs(tree);
            node13.ParentNode.Should().BeSameAs(node1);
            node13.PreviousNode.Should().BeSameAs(node12);
            node13.NextNode.Should().BeSameAs(newNode14);
            node13.FirstNode.Should().BeNull();
            node13.LastNode.Should().BeNull();

            newNode14.Name.Should().Be("NEW 1-4");
            newNode14.RootNode.Should().BeSameAs(tree);
            newNode14.ParentNode.Should().BeSameAs(node1);
            newNode14.PreviousNode.Should().BeSameAs(node13);
            newNode14.NextNode.Should().BeNull();
            newNode14.FirstNode.Should().BeNull();
            newNode14.LastNode.Should().BeNull();
        }
        #endregion

        #region LINQ Test Methods
        [Theory]
        [MemberData("TestTreeNodesTestData")]
        public void TestTreeNodes(string name, int maxDepth, int maxChildren)
        {
            // Arrange
            this.WriteLine("Test Name: {0}", name);
            this.WriteLine();

            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());
            this.WriteLine();
            this.WriteLine("Nodes from   = {0}", tree.Name);

            var expected = nodeDictionary.Values
                                         .Where(x => Object.ReferenceEquals(x.ParentNode, tree))
                                         .Select(x => x.Name)
                                         .OrderBy(x => x)
                                         .ToList();
            var expectedAsString = expected.Any()
                ? expected.Aggregate((current, next) => current + " " + next)
                : String.Empty;
            this.WriteLine();
            this.WriteLine("Expected     = {0}", expectedAsString);

            var actual = tree.Nodes()
                             .Select(x => x.Name)
                             .ToList();
            var actualAsString = actual.Any()
                ? actual.Aggregate((current, next) => current + " " + next)
                : String.Empty;
            this.WriteLine();
            this.WriteLine("Actual       = {0}", actualAsString);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData("TestTreeDescendentNodesTestData")]
        public void TestTreeDescendentNodes(string name, int maxDepth, int maxChildren)
        {
            // Arrange
            this.WriteLine("Test Name: {0}", name);
            this.WriteLine();

            this.WriteLine("Max Depth    = {0}", maxDepth);
            this.WriteLine("Max Children = {0}", maxChildren);

            IDictionary<string, Node> nodeDictionary;

            // Act
            var tree = BuildTree(maxDepth, maxChildren, out nodeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());
            this.WriteLine();
            this.WriteLine("Nodes from   = {0}", tree.Name);

            var expected = nodeDictionary.Values
                                         .Select(x => x.Name)
                                         .OrderBy(x => x)
                                         .Skip(1) // skip the node where Nodes is being called on.
                                         .ToList();
            var expectedAsString = expected.Any()
                ? expected.Aggregate((current, next) => current + " " + next)
                : String.Empty;
            this.WriteLine();
            this.WriteLine("Expected     = {0}", expectedAsString);

            var actual = tree.DescendantNodes()
                             .Select(x => x.Name)
                             .ToList();
            var actualAsString = actual.Any()
                ? actual.Aggregate((current, next) => current + " " + next)
                : String.Empty;
            this.WriteLine();
            this.WriteLine("Actual       = {0}", actualAsString);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData("TestTreeAttributesTestData")]
        public void TestTreeAttributes(string name, int attributeCount)
        {
            // Arrange
            this.WriteLine("Test Name: {0}", name);
            this.WriteLine();

            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);

            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());
            this.WriteLine();
            this.WriteLine("Attributes from = {0}", tree.Name);

            var expected = nodeAttributeDictionary.Values
                                                  .Select(x => x.Name)
                                                  .OrderBy(x => x)
                                                  .ToList();
            var expectedAsString = expected.Any()
                ? expected.Aggregate((current, next) => current + " " + next)
                : String.Empty;
            this.WriteLine();
            this.WriteLine("Expected        = {0}", expectedAsString);

            var actual = tree.Attributes()
                             .Select(x => x.Name)
                             .ToList();
            var actualAsString = actual.Any()
                ? actual.Aggregate((current, next) => current + " " + next)
                : String.Empty;
            this.WriteLine();
            this.WriteLine("Actual          = {0}", actualAsString);

            // Assert
            actual.ShouldAllBeEquivalentTo(expected);
        }
        #endregion

        #region DescendentNodes Test Methods
        #endregion

        #region Content Methods
        [Theory]
        [MemberData("TestTreeContentTestData")]
        public void TestTreeHasContent(string name, Content content)
        {
            // Arrange
            this.WriteLine("Test Name: {0}", name);
            this.WriteLine();
            this.WriteLine("Content   = {0}", content != null ? content.ToString() : "null");

            var expected = content != null;
            this.WriteLine("Expected  = {0}", expected);

            // Act
            var node = new Node<Content>("1", content);

            var actual = node.HasContent();
            this.WriteLine("Actual    = {0}", actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData("TestTreeContentTestData")]
        public void TestTreeGetContent(string name, Content content)
        {
            // Arrange
            this.WriteLine("Test Name: {0}", name);
            this.WriteLine();

            var expected = content;
            this.WriteLine("Expected  = {0}", expected != null ? expected.ToString() : "null");

            // Act
            var node = new Node<Content>("1", content);

            var actual = node.GetContent<Content>();
            this.WriteLine("Actual    = {0}", actual != null ? actual.ToString() : "null");

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }

        [Theory]
        [MemberData("TestTreeContentTestData")]
        public void TestTreeSetContent(string name, Content newContent)
        {
            // Arrange
            this.WriteLine("Test Name: {0}", name);
            this.WriteLine();

            var oldContent = new Content("Old Content");
            var expectedBefore = oldContent;
            this.WriteLine("Expected Before = {0}", expectedBefore);

            var expectedAfter = newContent;
            this.WriteLine("Expected After  = {0}", expectedAfter != null ? expectedAfter.ToString() : "null");

            // Act
            var node = new Node<Content>("1", oldContent);

            var actualBefore = node.GetContent<Content>();
            this.WriteLine("Actual Before   = {0}", actualBefore != null ? actualBefore.ToString() : "null");

            node.SetContent(newContent);
            var actualAfter = node.GetContent<Content>();
            this.WriteLine("Actual After    = {0}", actualAfter != null ? actualAfter.ToString() : "null");

            // Assert
            actualBefore.ShouldBeEquivalentTo(expectedBefore);
            actualAfter.ShouldBeEquivalentTo(expectedAfter);
        }
        #endregion

        #region AddNodeAttribute Test Methods
        [Fact]
        public void TestTreeAddNodeAttributeWith0AttributeCount()
        {
            // Arrange
            const int attributeCount = 0;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeAttributeDictionary.Should().HaveCount(0);

            tree.FirstAttribute.Should().BeNull();
            tree.LastAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeAttributeWith1AttributeCount()
        {
            // Arrange
            const int attributeCount = 1;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeAttributeDictionary.Should().HaveCount(1);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];

            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute1);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeAttributeWith2AttributeCount()
        {
            // Arrange
            const int attributeCount = 2;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeAttributeDictionary.Should().HaveCount(2);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];

            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeAttributeWith3AttributeCount()
        {
            // Arrange
            const int attributeCount = 3;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeAttributeDictionary.Should().HaveCount(3);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];

            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeAddNodeAttributeWith4AttributeCount()
        {
            // Arrange
            const int attributeCount = 4;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree");
            this.WriteLine(tree.ToTreeString());

            // Assert
            nodeAttributeDictionary.Should().HaveCount(4);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            nodeAttributeDictionary.Should().ContainKey("Attribute4");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var nodeAttribute4 = nodeAttributeDictionary["Attribute4"];

            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute4.Name.Should().Be("Attribute4");
            nodeAttribute4.PreviousAttribute.Should().BeSameAs(nodeAttribute3);
            nodeAttribute4.NextAttribute.Should().BeNull();
        }
        #endregion

        #region RemoveNodeAttribute Test Methods
        [Fact]
        public void TestTreeRemoveNodeAttribute1With1AttributeCount()
        {
            // Arrange
            const int attributeCount = 1;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(1);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];

            tree.RemoveAttribute(nodeAttribute1);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeNull();
            tree.LastAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNodeAttribute1With2AttributeCount()
        {
            // Arrange
            const int attributeCount = 2;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(2);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];

            tree.RemoveAttribute(nodeAttribute1);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute2);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeNull();
            nodeAttribute2.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNodeAttribute2With2AttributeCount()
        {
            // Arrange
            const int attributeCount = 2;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(2);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];

            tree.RemoveAttribute(nodeAttribute2);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute1);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNodeAttribute1With3AttributeCount()
        {
            // Arrange
            const int attributeCount = 3;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(3);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];

            tree.RemoveAttribute(nodeAttribute1);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute2);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeNull();
            nodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNodeAttribute2With3AttributeCount()
        {
            // Arrange
            const int attributeCount = 3;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(3);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];

            tree.RemoveAttribute(nodeAttribute2);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute3.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNodeAttribute3With3AttributeCount()
        {
            // Arrange
            const int attributeCount = 3;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(3);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];

            tree.RemoveAttribute(nodeAttribute3);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNodeAttribute1With4AttributeCount()
        {
            // Arrange
            const int attributeCount = 4;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(4);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            nodeAttributeDictionary.Should().ContainKey("Attribute4");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var nodeAttribute4 = nodeAttributeDictionary["Attribute4"];

            tree.RemoveAttribute(nodeAttribute1);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute2);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeNull();
            nodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute4.Name.Should().Be("Attribute4");
            nodeAttribute4.PreviousAttribute.Should().BeSameAs(nodeAttribute3);
            nodeAttribute4.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNodeAttribute2With4AttributeCount()
        {
            // Arrange
            const int attributeCount = 4;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(4);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            nodeAttributeDictionary.Should().ContainKey("Attribute4");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var nodeAttribute4 = nodeAttributeDictionary["Attribute4"];

            tree.RemoveAttribute(nodeAttribute2);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute3.NextAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute4.Name.Should().Be("Attribute4");
            nodeAttribute4.PreviousAttribute.Should().BeSameAs(nodeAttribute3);
            nodeAttribute4.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNodeAttribute3With4AttributeCount()
        {
            // Arrange
            const int attributeCount = 4;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(4);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            nodeAttributeDictionary.Should().ContainKey("Attribute4");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var nodeAttribute4 = nodeAttributeDictionary["Attribute4"];

            tree.RemoveAttribute(nodeAttribute3);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute4.Name.Should().Be("Attribute4");
            nodeAttribute4.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            nodeAttribute4.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeRemoveNodeAttribute4With4AttributeCount()
        {
            // Arrange
            const int attributeCount = 4;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(4);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            nodeAttributeDictionary.Should().ContainKey("Attribute4");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var nodeAttribute4 = nodeAttributeDictionary["Attribute4"];

            tree.RemoveAttribute(nodeAttribute4);

            this.WriteLine();
            this.WriteLine("Tree After RemoveAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeNull();
        }
        #endregion

        #region ReplaceNodeAttribute Test Methods
        [Fact]
        public void TestTreeReplaceNodeAttribute1With1AttributeCount()
        {
            // Arrange
            const int attributeCount = 1;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(1);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            var oldNodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var newNodeAttribute1 = new NodeAttribute<AttributeValue>("NewAttribute1", new AttributeValue(10));

            tree.ReplaceAttribute(oldNodeAttribute1, newNodeAttribute1);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(newNodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(newNodeAttribute1);

            newNodeAttribute1.Name.Should().Be("NewAttribute1");
            newNodeAttribute1.PreviousAttribute.Should().BeNull();
            newNodeAttribute1.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNodeAttribute1With2AttributeCount()
        {
            // Arrange
            const int attributeCount = 2;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(2);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            var oldNodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var newNodeAttribute1 = new NodeAttribute<AttributeValue>("NewAttribute1", new AttributeValue(10));
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];

            tree.ReplaceAttribute(oldNodeAttribute1, newNodeAttribute1);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(newNodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute2);

            newNodeAttribute1.Name.Should().Be("NewAttribute1");
            newNodeAttribute1.PreviousAttribute.Should().BeNull();
            newNodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(newNodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNodeAttribute2With2AttributeCount()
        {
            // Arrange
            const int attributeCount = 2;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(2);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var oldNodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var newNodeAttribute2 = new NodeAttribute<AttributeValue>("NewAttribute2", new AttributeValue(20));

            tree.ReplaceAttribute(oldNodeAttribute2, newNodeAttribute2);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(newNodeAttribute2);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(newNodeAttribute2);

            newNodeAttribute2.Name.Should().Be("NewAttribute2");
            newNodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            newNodeAttribute2.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNodeAttribute1With3AttributeCount()
        {
            // Arrange
            const int attributeCount = 3;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(3);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            var oldNodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var newNodeAttribute1 = new NodeAttribute<AttributeValue>("NewAttribute1", new AttributeValue(10));
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];

            tree.ReplaceAttribute(oldNodeAttribute1, newNodeAttribute1);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(newNodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute3);

            newNodeAttribute1.Name.Should().Be("NewAttribute1");
            newNodeAttribute1.PreviousAttribute.Should().BeNull();
            newNodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(newNodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNodeAttribute2With3AttributeCount()
        {
            // Arrange
            const int attributeCount = 3;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(3);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var oldNodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var newNodeAttribute2 = new NodeAttribute<AttributeValue>("NewAttribute2", new AttributeValue(20));
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];

            tree.ReplaceAttribute(oldNodeAttribute2, newNodeAttribute2);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(newNodeAttribute2);

            newNodeAttribute2.Name.Should().Be("NewAttribute2");
            newNodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            newNodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(newNodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNodeAttribute3With3AttributeCount()
        {
            // Arrange
            const int attributeCount = 3;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(3);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var oldNodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var newNodeAttribute3 = new NodeAttribute<AttributeValue>("NewAttribute3", new AttributeValue(30));

            tree.ReplaceAttribute(oldNodeAttribute3, newNodeAttribute3);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(newNodeAttribute3);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeSameAs(newNodeAttribute3);

            newNodeAttribute3.Name.Should().Be("NewAttribute3");
            newNodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            newNodeAttribute3.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNodeAttribute1With4AttributeCount()
        {
            // Arrange
            const int attributeCount = 4;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(4);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            nodeAttributeDictionary.Should().ContainKey("Attribute4");
            var oldNodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var newNodeAttribute1 = new NodeAttribute<AttributeValue>("NewAttribute1", new AttributeValue(10));
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var nodeAttribute4 = nodeAttributeDictionary["Attribute4"];

            tree.ReplaceAttribute(oldNodeAttribute1, newNodeAttribute1);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(newNodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute4);

            newNodeAttribute1.Name.Should().Be("NewAttribute1");
            newNodeAttribute1.PreviousAttribute.Should().BeNull();
            newNodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(newNodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute4.Name.Should().Be("Attribute4");
            nodeAttribute4.PreviousAttribute.Should().BeSameAs(nodeAttribute3);
            nodeAttribute4.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNodeAttribute2With4AttributeCount()
        {
            // Arrange
            const int attributeCount = 4;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(4);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            nodeAttributeDictionary.Should().ContainKey("Attribute4");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var oldNodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var newNodeAttribute2 = new NodeAttribute<AttributeValue>("NewAttribute2", new AttributeValue(20));
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var nodeAttribute4 = nodeAttributeDictionary["Attribute4"];

            tree.ReplaceAttribute(oldNodeAttribute2, newNodeAttribute2);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(newNodeAttribute2);

            newNodeAttribute2.Name.Should().Be("NewAttribute2");
            newNodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            newNodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(newNodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute4.Name.Should().Be("Attribute4");
            nodeAttribute4.PreviousAttribute.Should().BeSameAs(nodeAttribute3);
            nodeAttribute4.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNodeAttribute3With4AttributeCount()
        {
            // Arrange
            const int attributeCount = 4;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(4);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            nodeAttributeDictionary.Should().ContainKey("Attribute4");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var oldNodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var newNodeAttribute3 = new NodeAttribute<AttributeValue>("NewAttribute3", new AttributeValue(30));
            var nodeAttribute4 = nodeAttributeDictionary["Attribute4"];

            tree.ReplaceAttribute(oldNodeAttribute3, newNodeAttribute3);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeSameAs(newNodeAttribute3);

            newNodeAttribute3.Name.Should().Be("NewAttribute3");
            newNodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            newNodeAttribute3.NextAttribute.Should().BeSameAs(nodeAttribute4);

            nodeAttribute4.Name.Should().Be("Attribute4");
            nodeAttribute4.PreviousAttribute.Should().BeSameAs(newNodeAttribute3);
            nodeAttribute4.NextAttribute.Should().BeNull();
        }

        [Fact]
        public void TestTreeReplaceNodeAttribute4With4AttributeCount()
        {
            // Arrange
            const int attributeCount = 4;
            this.WriteLine("Attribute Count = {0}", attributeCount);

            IDictionary<string, NodeAttribute> nodeAttributeDictionary;

            // Act
            var tree = new Node("1");
            BuildAttributes(attributeCount, tree, out nodeAttributeDictionary);
            this.WriteLine();
            this.WriteLine("Tree Before ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            nodeAttributeDictionary.Should().HaveCount(4);
            nodeAttributeDictionary.Should().ContainKey("Attribute1");
            nodeAttributeDictionary.Should().ContainKey("Attribute2");
            nodeAttributeDictionary.Should().ContainKey("Attribute3");
            nodeAttributeDictionary.Should().ContainKey("Attribute4");
            var nodeAttribute1 = nodeAttributeDictionary["Attribute1"];
            var nodeAttribute2 = nodeAttributeDictionary["Attribute2"];
            var nodeAttribute3 = nodeAttributeDictionary["Attribute3"];
            var oldNodeAttribute4 = nodeAttributeDictionary["Attribute4"];
            var newNodeAttribute4 = new NodeAttribute<AttributeValue>("NewAttribute4", new AttributeValue(40));

            tree.ReplaceAttribute(oldNodeAttribute4, newNodeAttribute4);

            this.WriteLine();
            this.WriteLine("Tree After ReplaceAttribute");
            this.WriteLine(tree.ToTreeString());

            // Assert
            tree.FirstAttribute.Should().BeSameAs(nodeAttribute1);
            tree.LastAttribute.Should().BeSameAs(newNodeAttribute4);

            nodeAttribute1.Name.Should().Be("Attribute1");
            nodeAttribute1.PreviousAttribute.Should().BeNull();
            nodeAttribute1.NextAttribute.Should().BeSameAs(nodeAttribute2);

            nodeAttribute2.Name.Should().Be("Attribute2");
            nodeAttribute2.PreviousAttribute.Should().BeSameAs(nodeAttribute1);
            nodeAttribute2.NextAttribute.Should().BeSameAs(nodeAttribute3);

            nodeAttribute3.Name.Should().Be("Attribute3");
            nodeAttribute3.PreviousAttribute.Should().BeSameAs(nodeAttribute2);
            nodeAttribute3.NextAttribute.Should().BeSameAs(newNodeAttribute4);

            newNodeAttribute4.Name.Should().Be("NewAttribute4");
            newNodeAttribute4.PreviousAttribute.Should().BeSameAs(nodeAttribute3);
            newNodeAttribute4.NextAttribute.Should().BeNull();
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> TestTreeNodesTestData = new[]
            {
                new object[] { "With1MaxDepth", 1, 0 },
                new object[] { "With2MaxDepthAnd1MaxChildren", 2, 1 },
                new object[] { "With2MaxDepthAnd2MaxChildren", 2, 2 },
                new object[] { "With2MaxDepthAnd3MaxChildren", 2, 3 },
                new object[] { "With2MaxDepthAnd4MaxChildren", 2, 4 },
                new object[] { "With3MaxDepthAnd1MaxChildren", 3, 1 },
                new object[] { "With3MaxDepthAnd2MaxChildren", 3, 2 },
                new object[] { "With3MaxDepthAnd3MaxChildren", 3, 3 },
                new object[] { "With3MaxDepthAnd4MaxChildren", 3, 4 },
            };

        public static readonly IEnumerable<object[]> TestTreeDescendentNodesTestData = new[]
            {
                new object[] { "With1MaxDepth", 1, 0 },
                new object[] { "With2MaxDepthAnd1MaxChildren", 2, 1 },
                new object[] { "With2MaxDepthAnd2MaxChildren", 2, 2 },
                new object[] { "With2MaxDepthAnd3MaxChildren", 2, 3 },
                new object[] { "With2MaxDepthAnd4MaxChildren", 2, 4 },
                new object[] { "With3MaxDepthAnd1MaxChildren", 3, 1 },
                new object[] { "With3MaxDepthAnd2MaxChildren", 3, 2 },
                new object[] { "With3MaxDepthAnd3MaxChildren", 3, 3 },
                new object[] { "With3MaxDepthAnd4MaxChildren", 3, 4 },
            };

        public static readonly IEnumerable<object[]> TestTreeAttributesTestData = new[]
            {
                new object[] { "With0AttributeCount", 0 },
                new object[] { "With1AttributeCount", 1 },
                new object[] { "With2AttributeCount", 2 },
                new object[] { "With3AttributeCount", 3 },
                new object[] { "With4AttributeCount", 4 },
            };

        public static readonly IEnumerable<object[]> TestTreeContentTestData = new[]
            {
                new object[] { "WithNonNullContent", new Content("My Content") },
                new object[] { "WithNullContent", null },
            };
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Test Methods
        private static void BuildAttributes(int attributeCount, Node node, out IDictionary<string, NodeAttribute> nodeAttributeDictionary)
        {
            nodeAttributeDictionary = new Dictionary<string, NodeAttribute>();

            if (attributeCount <= 0)
                return;

            for (var i = 1; i <= attributeCount; i++)
            {
                var attributeName = "Attribute{0}".FormatWith(i);
                var attribute = new NodeAttribute<AttributeValue>(attributeName, new AttributeValue(i));
                node.AddAttribute(attribute);

                nodeAttributeDictionary.Add(attribute.Name, attribute);
            }
        }

        private static Node BuildTree(int maxDepth, int maxChildren, out IDictionary<string, Node> nodeDictionary)
        {
            nodeDictionary = new Dictionary<string, Node>();

            if (maxDepth == 0)
                return null;

            var rootNode = new Node("1");
            nodeDictionary.Add(rootNode.Name, rootNode);
            if (maxDepth == 1)
                return rootNode;

            BuildTree(maxDepth, maxChildren, 2, rootNode, nodeDictionary);
            return rootNode;
        }

        private static void BuildTree(int maxDepth, int maxChildren, int currentDepth, Node parentNode, IDictionary<string, Node> nodeDictionary)
        {
            // Create child nodes
            var childNodes = new List<Node>();
            var parentName = parentNode.Name;
            for (var i = 1; i <= maxChildren; ++i)
            {
                var childName = "{0}-{1}".FormatWith(parentName, i);
                var childNode = new Node(childName);
                childNodes.Add(childNode);

                nodeDictionary.Add(childNode.Name, childNode);
                parentNode.AddNode(childNode);
            }

            // If we are at maximum depth, then return.
            if (currentDepth == maxDepth)
                return;

            for (var i = 0; i < maxChildren; ++i)
            {
                var childNode = childNodes[i];
                BuildTree(maxDepth, maxChildren, currentDepth + 1, childNode, nodeDictionary);
            }
        }
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private class AttributeValue
        {
            public AttributeValue(int number)
            {
                this.Number = number;
            }

            public int Number { get; private set; }

            public override string ToString()
            { return this.Number.ToString(CultureInfo.InvariantCulture); }
        }

        public class Content
        {
            public Content(string name)
            {
                this.Name = name;
            }

            public string Name { get; private set; }

            public override string ToString()
            {
                var name = !String.IsNullOrWhiteSpace(this.Name)
                    ? this.Name
                    : "null";

                return "{0} [name={1}]".FormatWith(typeof(Content).Name, name);
            }
        }
        #endregion
    }
}
