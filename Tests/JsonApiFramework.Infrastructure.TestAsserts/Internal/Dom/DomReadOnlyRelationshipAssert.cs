// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    internal static class DomReadOnlyRelationshipAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(string expectedRel, Relationship expectedRelationship, DomReadOnlyRelationship actual)
        {
            if (String.IsNullOrWhiteSpace(expectedRel))
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);
            Assert.NotNull(expectedRelationship);

            Assert.Equal(DomNodeType.Relationship, actual.NodeType);

            // Rel
            var actualRel = actual.Rel;
            Assert.Equal(expectedRel, actualRel);

            // Relationship
            var actualRelationship = actual.Relationship;
            RelationshipAssert.Equal(expectedRelationship, actualRelationship);
        }
        #endregion
    }
}