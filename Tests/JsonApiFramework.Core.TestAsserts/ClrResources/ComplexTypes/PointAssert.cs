// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.TestData.ClrResources.ComplexTypes;

using Xunit;

namespace JsonApiFramework.TestAsserts.ClrResources.ComplexTypes
{
    public static class PointAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Point expected, Point actual)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.X, actual.X);
            Assert.Equal(expected.Y, actual.Y);
            CustomDataAssert.Equal(expected.CustomData, actual.CustomData);
        }

        public static void Equal(IEnumerable<Point> expected, IEnumerable<Point> actual)
        {
            // Handle when 'expected' is null.
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

                PointAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion
    }
}