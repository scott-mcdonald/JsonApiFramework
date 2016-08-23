// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Server.Hypermedia;

using Xunit;

namespace JsonApiFramework.Server.TestAsserts.Hypermedia
{
    public static class HypermediaPathAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(IHypermediaPath expected, IHypermediaPath actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.PathSegments, actual.PathSegments);
            Assert.Equal(expected.HypermediaPathType, actual.HypermediaPathType);
        }

        public static void Equal(IEnumerable<IHypermediaPath> expected, IEnumerable<IHypermediaPath> actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var expectedCollection = expected.SafeToList();
            var actualCollection = actual.SafeToList();

            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expectedItem = expectedCollection[index];
                var actualItem = actualCollection[index];

                HypermediaPathAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion
    }
}