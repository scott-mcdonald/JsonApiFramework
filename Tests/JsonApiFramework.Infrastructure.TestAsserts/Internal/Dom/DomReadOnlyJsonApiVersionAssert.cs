// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    internal static class DomReadOnlyJsonApiVersionAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(JsonApiVersion expected, DomReadOnlyJsonApiVersion actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.JsonApiVersion, actual.NodeType);

            var actualJsonApiVersion = actual.JsonApiVersion;
            JsonApiVersionAssert.Equal(expected, actualJsonApiVersion);
        }
        #endregion
    }
}