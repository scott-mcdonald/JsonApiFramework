// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.Internal.Dom
{
    using DomNode = Node<DomNodeType>;

    internal static class DomDataCollectionAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Empty(DomNode actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(DomNodeType.DataCollection, actual.NodeType);

            var actualDomDataCollection = (DomDataCollection)actual;
            var actualDomResources = actualDomDataCollection.Nodes()
                                                            .ToArray();
            var actualCount = actualDomResources.Count();
            Assert.Equal(0, actualCount);
        }

        public static void Equal(IEnumerable<Resource> expected, DomNode actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(DomNodeType.DataCollection, actual.NodeType);

            var actualDomDataCollection = (DomDataCollection)actual;
            var actualDomResources = actualDomDataCollection.Nodes()
                                                            .ToList();
            var actualCount = actualDomResources.Count();

            var expectedCollection = expected.SafeToReadOnlyList();

            if (expectedCollection.Any() == false)
            {
                Assert.Equal(0, actualCount);
                return;
            }

            var expectedCount = expectedCollection.Count();
            Assert.Equal(expectedCount, actualCount);

            var count = expectedCount;
            for (var i = 0; i < count; ++i)
            {
                var expectedApiResource = expectedCollection[i];
                var actualDomResource = actualDomResources[i];

                DomResourceAssert.Equal(expectedApiResource, actualDomResource);
            }
        }

        public static void Equal(IEnumerable<ResourceIdentifier> expected, DomNode actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(DomNodeType.DataCollection, actual.NodeType);

            var actualDomDataCollection = (DomDataCollection)actual;
            var actualDomResourceIdentifiers = actualDomDataCollection.Nodes()
                                                                      .ToArray();
            var actualCount = actualDomResourceIdentifiers.Count();

            var expectedCollection = expected.SafeToReadOnlyList();

            if (expectedCollection.Any() == false)
            {
                Assert.Equal(0, actualCount);
                return;
            }

            var expectedCount = expectedCollection.Count();
            Assert.Equal(expectedCount, actualCount);

            var count = expectedCount;
            for (var i = 0; i < count; ++i)
            {
                var expectedApiResourceIdentifier = expectedCollection[i];
                var actualDomResourceIdentifier = actualDomResourceIdentifiers[i];

                DomResourceIdentifierAssert.Equal(expectedApiResourceIdentifier, actualDomResourceIdentifier);
            }
        }
        #endregion
    }
}