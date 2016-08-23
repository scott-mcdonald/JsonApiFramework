// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    internal static class DomIncludedAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(IEnumerable<Resource> expected, DomNode actual)
        {
            if (expected == null )
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(DomNodeType.Included, actual.NodeType);

            var actualDomIncluded = (DomIncluded)actual;
            var actualDomResources = actualDomIncluded.Nodes()
                                                      .ToList();

            var expectedCollection = expected.SafeToReadOnlyList();

            var expectedCount = expectedCollection.Count();
            var actualCount = actualDomResources.Count();
            Assert.Equal(expectedCount, actualCount);

            var count = expectedCount;
            for (var i = 0; i < count; ++i)
            {
                var expectedResource = expectedCollection[i];
                var actualDomResource = (IDomResource)actualDomResources[i];
                var actualResource = actualDomResource.ApiResource;

                ResourceAssert.Equal(expectedResource, actualResource);
            }
        }
        #endregion
    }
}