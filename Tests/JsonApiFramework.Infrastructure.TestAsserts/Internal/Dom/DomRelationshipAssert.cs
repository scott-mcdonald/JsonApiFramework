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

    internal static class DomRelationshipAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(string expectedRel, Relationship expectedRelationship, DomNode actual)
        {
            Assert.False(String.IsNullOrWhiteSpace(expectedRel));

            if (expectedRelationship == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var actualType = actual.GetType();

            if (actualType == typeof(DomReadOnlyRelationship))
            {
                var actualDomReadOnlyRelationship = (DomReadOnlyRelationship)actual;
                DomReadOnlyRelationshipAssert.Equal(expectedRel, expectedRelationship, actualDomReadOnlyRelationship);
            }
            else if (actualType == typeof(DomReadWriteRelationship))
            {
                var actualDomReadWriteRelationship = (DomReadWriteRelationship)actual;
                DomReadWriteRelationshipAssert.Equal(expectedRel, expectedRelationship, actualDomReadWriteRelationship);
            }
            else
            {
                Assert.True(false, "Unknown actual node type [name={0}].".FormatWith(actualType.Name));
            }
        }
        #endregion
    }
}