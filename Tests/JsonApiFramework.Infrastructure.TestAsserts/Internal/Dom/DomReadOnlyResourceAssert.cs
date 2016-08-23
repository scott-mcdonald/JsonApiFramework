// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    internal static class DomReadOnlyResourceAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Resource expected, DomReadOnlyResource actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.Resource, actual.NodeType);

            var actualResource = actual.ApiResource;
            ResourceAssert.Equal(expected, actualResource);
        }

        public static void Equal(object expected, DomReadOnlyResource actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }

            Assert.NotNull(actual);
            Assert.Equal(DomNodeType.Resource, actual.NodeType);

            var actualResource = actual.ClrResource;
            ObjectAssert.Equal(expected, actualResource);
        }
        #endregion
    }
}