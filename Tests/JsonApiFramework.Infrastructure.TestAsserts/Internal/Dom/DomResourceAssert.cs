// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    internal static class DomResourceAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Resource expected, DomNode actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var actualType = actual.GetType();

            if (actualType == typeof(DomReadOnlyResource))
            {
                var actualDomReadOnlyResource = (DomReadOnlyResource)actual;
                DomReadOnlyResourceAssert.Equal(expected, actualDomReadOnlyResource);
            }
            else if (actualType == typeof(DomReadWriteResource))
            {
                var actualDomReadWriteResource = (DomReadWriteResource)actual;
                DomReadWriteResourceAssert.Equal(expected, actualDomReadWriteResource);
            }
            else
            {
                Assert.True(false, "Unknown actual node type [name={0}].".FormatWith(actualType.Name));
            }
        }
        #endregion
    }
}