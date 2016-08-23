// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    internal static class DomReadWriteRelationshipsAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Relationships expected, DomReadWriteRelationships actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.Relationships, actual.NodeType);

            var actualNodes = actual.Nodes()
                                    .ToList();

            var actualDomRelationshipsCount = actualNodes.Count;

            Assert.Equal(expected.Count, actualDomRelationshipsCount);

            foreach (var expectedRelRelationshipPair in expected)
            {
                var expectedRel = expectedRelRelationshipPair.Key;
                var expectedRelationship = expectedRelRelationshipPair.Value;

                DomNode actualDomRelationshipNode = null;
                foreach (var actualNode in actualNodes)
                {
                    var domRelationship = (IDomRelationship)actualNode;
                    if (domRelationship.Rel != expectedRel)
                        continue;

                    actualDomRelationshipNode = actualNode;
                    break;
                }

                Assert.NotNull(actualDomRelationshipNode);

                DomRelationshipAssert.Equal(expectedRel, expectedRelationship, actualDomRelationshipNode);
            }
        }
        #endregion
    }
}