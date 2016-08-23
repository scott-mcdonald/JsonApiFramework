// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ClrResources;

using Xunit;

namespace JsonApiFramework.TestAsserts.ClrResources
{
    public static class ArticleAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Article expected, Article actual)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            RelationshipsAssert.Equal(expected.Relationships, actual.Relationships);
            LinksAssert.Equal(expected.Links, actual.Links);
            ObjectAssert.Equal(expected.Meta, actual.Meta);
        }

        public static void Equal(IEnumerable<Article> expected, IEnumerable<Article> actual)
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

                ArticleAssert.Equal(expectedItem, actualItem);
            }
        }
        #endregion
    }
}