// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.ServiceModel;

using Xunit;

namespace JsonApiFramework.TestAsserts.ServiceModel
{
    public static class ResourceTypeAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(IResourceType expected, IResourceType actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.ClrType, actual.ClrType);
            HypermediaInfoAssert.Equal(expected.HypermediaInfo, actual.HypermediaInfo);
            ResourceIdentityInfoAssert.Equal(expected.ResourceIdentityInfo, actual.ResourceIdentityInfo);
            AttributesInfoAssert.Equal(expected.AttributesInfo, actual.AttributesInfo);
            RelationshipsInfoAssert.Equal(expected.RelationshipsInfo, actual.RelationshipsInfo);
            LinksInfoAssert.Equal(expected.LinksInfo, actual.LinksInfo);
            MetaInfoAssert.Equal(expected.MetaInfo, actual.MetaInfo);
        }

        public static void Equal(IEnumerable<IResourceType> expected, IEnumerable<IResourceType> actual)
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
                ResourceTypeAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion
    }
}