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

    internal static class DomReadWriteLinksAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Links expected, DomReadWriteLinks actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.Links, actual.NodeType);

            var actualNodes = actual.Nodes()
                                    .ToList();

            var actualDomLinksCount = actualNodes.Count;

            Assert.Equal(expected.Count, actualDomLinksCount);

            foreach (var expectedRelLinkPair in expected)
            {
                var expectedRel = expectedRelLinkPair.Key;
                var expectedLink = expectedRelLinkPair.Value;

                DomNode actualDomLinkNode = null;
                foreach (var actualNode in actualNodes)
                {
                    var domLink = (IDomLink)actualNode;
                    if (domLink.Rel != expectedRel)
                        continue;

                    actualDomLinkNode = actualNode;
                    break;
                }

                Assert.NotNull(actualDomLinkNode);

                DomLinkAssert.Equal(expectedRel, expectedLink, actualDomLinkNode);
            }
        }
        #endregion
    }
}