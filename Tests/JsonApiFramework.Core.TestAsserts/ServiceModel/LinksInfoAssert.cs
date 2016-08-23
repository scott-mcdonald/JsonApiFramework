// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.ServiceModel;

using Xunit;

namespace JsonApiFramework.TestAsserts.ServiceModel
{
    public static class LinksInfoAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(ILinksInfo expected, ILinksInfo actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            PropertyInfoAssert.Equal(expected, actual);

            var expectedList = expected.Collection.SafeToList();
            var actualList = actual.Collection.SafeToList();
            Assert.Equal(expectedList.Count, actualList.Count);

            var count = expectedList.Count;
            for (var i = 0; i < count; ++i)
            {
                var expectedItem = expectedList[i];
                var actualItem = actualList[i];
                LinkInfoAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion
    }
}