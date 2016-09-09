// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.TestData.ClrResources.ComplexTypes;

using Xunit;

namespace JsonApiFramework.TestAsserts.ClrResources.ComplexTypes
{
    public static class CustomPropertyAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(CustomProperty expected, CustomProperty actual)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Value, actual.Value);
        }

        public static void Equal(IEnumerable<CustomProperty> expected, IEnumerable<CustomProperty> actual)
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

                CustomPropertyAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion
    }
}