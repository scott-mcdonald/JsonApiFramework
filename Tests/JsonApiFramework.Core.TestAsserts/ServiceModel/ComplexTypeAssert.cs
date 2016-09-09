// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.ServiceModel;

using Xunit;

namespace JsonApiFramework.TestAsserts.ServiceModel
{
    public static class ComplexTypeAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(IComplexType expected, IComplexType actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.ClrType, actual.ClrType);
            AttributesInfoAssert.Equal(expected.AttributesInfo, actual.AttributesInfo);
        }

        public static void Equal(IEnumerable<IComplexType> expected, IEnumerable<IComplexType> actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var expectedList = expected.SafeToList();
            var actualList = actual.SafeToList();
            Assert.Equal(expectedList.Count, actualList.Count);

            var count = expectedList.Count;
            for (var i = 0; i < count; ++i)
            {
                var expectedItem = expectedList[i];
                var actualItem = actualList[i];
                ComplexTypeAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion
    }
}