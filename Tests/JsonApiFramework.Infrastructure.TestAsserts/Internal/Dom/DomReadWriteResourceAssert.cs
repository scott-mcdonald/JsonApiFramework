// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    internal static class DomReadWriteResourceAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Resource expected, DomReadWriteResource actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.Resource, actual.NodeType);

            // Type
            var domType = actual.GetNode<DomNodeType, DomType>(DomNodeType.Type);
            DomTypeAssert.Equal(expected.Type, domType);

            // Id
            var domId = actual.GetNode<DomNodeType, DomId>(DomNodeType.Id);
            DomIdAssert.Equal(expected.Id, domId);

            // Attributes
            var domAttributes = actual.GetNode<DomNodeType, DomAttributes>(DomNodeType.Attributes);
            DomAttributesAssert.Equal(expected.Attributes, domAttributes);

            // Relationships
            var domRelationships = actual.GetNode(DomNodeType.Relationships);
            DomRelationshipsAssert.Equal(expected.Relationships, domRelationships);

            // Links
            var domLinks = actual.GetNode(DomNodeType.Links);
            DomLinksAssert.Equal(expected.Links, domLinks);

            // Meta
            var domMeta = actual.GetNode(DomNodeType.Meta);
            DomMetaAssert.Equal(expected.Meta, domMeta);
        }
        #endregion
    }
}