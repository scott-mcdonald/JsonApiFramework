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

            Assert.Equal(expected.ClrResourceType, actual.ClrResourceType);
            HypermediaInfoAssert.Equal(expected.Hypermedia, actual.Hypermedia);
            ResourceIdentityInfoAssert.Equal(expected.ResourceIdentity, actual.ResourceIdentity);
            AttributesInfoAssert.Equal(expected.Attributes, actual.Attributes);
            RelationshipsInfoAssert.Equal(expected.Relationships, actual.Relationships);
            LinksInfoAssert.Equal(expected.Links, actual.Links);
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