// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    internal static class DomReadOnlyResourceIdentifierAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(ResourceIdentifier expectedApiResourceIdentifier, DomReadOnlyResourceIdentifier actual)
        {
            if (expectedApiResourceIdentifier == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.ResourceIdentifier, actual.NodeType);

            var actualResourceIdentifier = actual.ApiResourceIdentifier;
            ResourceIdentifierAssert.Equal(expectedApiResourceIdentifier, actualResourceIdentifier);
        }
        #endregion
    }
}