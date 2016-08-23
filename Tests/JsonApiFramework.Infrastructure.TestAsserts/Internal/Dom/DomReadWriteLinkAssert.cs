// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    internal static class DomReadWriteLinkAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(string expectedRel, Link expectedLink, DomReadWriteLink actual)
        {
            if (String.IsNullOrWhiteSpace(expectedRel))
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);
            Assert.NotNull(expectedLink);

            Assert.Equal(DomNodeType.Link, actual.NodeType);

            // Rel
            var actualRel = actual.Rel;
            Assert.Equal(expectedRel, actualRel);

            // HRef
            var domHRef = actual.GetNode<DomNodeType, DomHRef>(DomNodeType.HRef);
            DomHRefAssert.Equal(expectedLink.HRef, domHRef);

            // Meta
            var domMeta = actual.GetNode(DomNodeType.Meta);
            DomMetaAssert.Equal(expectedLink.Meta, domMeta);
        }
        #endregion
    }
}