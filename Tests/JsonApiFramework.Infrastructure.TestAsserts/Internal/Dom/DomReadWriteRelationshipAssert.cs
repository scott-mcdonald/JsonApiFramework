// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    internal static class DomReadWriteRelationshipAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(string expectedRel, Relationship expectedRelationship, DomReadWriteRelationship actual)
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

            // Links
            var domLinks = actual.GetNode(DomNodeType.Links);
            DomLinksAssert.Equal(expectedRelationship.Links, domLinks);

            // Data
            var expectedRelationshipType = expectedRelationship.GetRelationshipType();
            switch (expectedRelationshipType)
            {
                case RelationshipType.Relationship:
                    break;

                case RelationshipType.ToOneRelationship:
                    {
                        var expectedToOneResourceLinkage = expectedRelationship.GetToOneResourceLinkage();
                        var domData = actual.GetNode(DomNodeType.Data);
                        DomDataAssert.Equal(expectedToOneResourceLinkage, domData);
                    }
                    break;

                case RelationshipType.ToManyRelationship:
                    {
                        var expectedToManyResourceLinkage = expectedRelationship.GetToManyResourceLinkage();
                        var domDataCollection = actual.GetNode(DomNodeType.DataCollection);
                        DomDataCollectionAssert.Equal(expectedToManyResourceLinkage, domDataCollection);
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Meta
            var domMeta = actual.GetNode(DomNodeType.Meta);
            DomMetaAssert.Equal(expectedRelationship.Meta, domMeta);
        }
        #endregion
    }
}