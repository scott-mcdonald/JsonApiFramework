// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    internal static class DomRelationshipsAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Relationships expected, DomNode actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var actualType = actual.GetType();

            if (actualType == typeof(DomReadOnlyRelationships))
            {
                var actualDomReadOnlyRelationships = (DomReadOnlyRelationships)actual;
                DomReadOnlyRelationshipsAssert.Equal(expected, actualDomReadOnlyRelationships);
            }
            else if (actualType == typeof(DomReadWriteRelationships))
            {
                var actualDomReadWriteRelationships = (DomReadWriteRelationships)actual;
                DomReadWriteRelationshipsAssert.Equal(expected, actualDomReadWriteRelationships);
            }
            else
            {
                Assert.True(false, "Unknown actual node type [name={0}].".FormatWith(actualType.Name));
            }
        }
        #endregion
    }
}