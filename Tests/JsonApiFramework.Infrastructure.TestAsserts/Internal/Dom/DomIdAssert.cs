// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    internal static class DomIdAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(string expected, DomNode actual)
        {
            if (String.IsNullOrWhiteSpace(expected))
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.Id, actual.NodeType);

            var actualDomId = (DomId)actual;
            var actualApiId = actualDomId.ApiId;
            Assert.Equal(expected, actualApiId);
        }
        #endregion
    }
}