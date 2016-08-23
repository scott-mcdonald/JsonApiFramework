// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    internal static class DomLinkAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(string expectedRel, Link expectedLink, DomNode actual)
        {
            Assert.False(String.IsNullOrWhiteSpace(expectedRel));

            if (expectedLink == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var actualType = actual.GetType();

            if (actualType == typeof(DomReadOnlyLink))
            {
                var actualDomReadOnlyLink = (DomReadOnlyLink)actual;
                DomReadOnlyLinkAssert.Equal(expectedRel, expectedLink, actualDomReadOnlyLink);
            }
            else if (actualType == typeof(DomReadWriteLink))
            {
                var actualDomReadWriteLink = (DomReadWriteLink)actual;
                DomReadWriteLinkAssert.Equal(expectedRel, expectedLink, actualDomReadWriteLink);
            }
            else
            {
                Assert.True(false, "Unknown actual node type [name={0}].".FormatWith(actualType.Name));
            }
        }
        #endregion
    }
}