// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{

    internal static class DomReadWriteResourceIdentifierAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(ResourceIdentifier expectedApiResourceIdentifier, DomReadWriteResourceIdentifier actual)
        {
            if (expectedApiResourceIdentifier == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.ResourceIdentifier, actual.NodeType);

            // Type
            var domType = actual.GetNode<DomNodeType, DomType>(DomNodeType.Type);
            DomTypeAssert.Equal(expectedApiResourceIdentifier.Type, domType);

            // Id
            var domId = actual.GetNode<DomNodeType, DomId>(DomNodeType.Id);
            DomIdAssert.Equal(expectedApiResourceIdentifier.Id, domId);

            // Meta
            var domMeta = actual.GetNode(DomNodeType.Meta);
            DomMetaAssert.Equal(expectedApiResourceIdentifier.Meta, domMeta);
        }
        #endregion
    }
}