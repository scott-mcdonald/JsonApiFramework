// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;
    using DomNodeContainer = NodeContainer<DomNodeType>;

    internal static class DomDataAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Resource expected, DomNode actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(DomNodeType.Data, actual.NodeType);

            if (expected == null)
            {
                Assert.True(((DomNodeContainer)actual).Node == null);
                return;
            }
            Assert.NotNull(actual);

            var actualDomData = (DomData)actual;
            var actualDomDataNode = actualDomData.Node;

            DomResourceAssert.Equal(expected, actualDomDataNode);
        }

        public static void Equal(ResourceIdentifier expected, DomNode actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(DomNodeType.Data, actual.NodeType);

            if (expected == null)
            {
                Assert.True(((DomNodeContainer)actual).Node == null);
                return;
            }
            Assert.NotNull(actual);

            var actualDomData = (DomData)actual;
            var actualDomDataNode = actualDomData.Node;

            DomResourceIdentifierAssert.Equal(expected, actualDomDataNode);
        }

        public static void Null(DomNode actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(DomNodeType.Data, actual.NodeType);

            Assert.True(((DomNodeContainer)actual).Node == null);
        }
        #endregion
    }
}