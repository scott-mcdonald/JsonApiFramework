// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class LinksAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Links expected, string actualJson)
        {
            Assert.NotNull(expected);
            Assert.False(String.IsNullOrEmpty(actualJson));

            var actualJToken = JToken.Parse(actualJson);
            LinksAssert.Equal(expected, actualJToken);
        }

        public static void Equal(Links expected, JToken actualJToken)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                ObjectAssert.IsNull(actualJToken);
                return;
            }

            // Handle when 'expected' is not null.
            Assert.NotNull(actualJToken);

            var actualJTokenType = actualJToken.Type;
            Assert.Equal(JTokenType.Object, actualJTokenType);

            var actualJObject = (JObject)actualJToken;

            var expectedLinksCount = expected.Count;
            var actualLinksCount = actualJObject.Count;
            Assert.Equal(expectedLinksCount, actualLinksCount);

            foreach (var key in expected.Keys)
            {
                var expectedLink = expected[key];
                Assert.IsType<Link>(expectedLink);

                var actualLinkJToken = actualJObject[key];
                LinkAssert.Equal(expectedLink, actualLinkJToken);
            }
        }

        public static void Equal(Links expected, Links actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }

            Assert.NotNull(actual);
            Assert.Equal(expected.Count, actual.Count);

            foreach (var key in expected.Keys)
            {
                Assert.True(actual.ContainsKey(key));

                var expectedLink = expected[key];
                Assert.IsType<Link>(expectedLink);

                var actualLink = actual[key];
                Assert.IsType<Link>(actualLink);

                LinkAssert.Equal(expectedLink, actualLink);
            }
        }

        public static void Equal(IEnumerable<Links> expected, IEnumerable<Links> actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            var expectedCollection = expected.SafeToReadOnlyList();
            var actualCollection = actual.SafeToReadOnlyList();

            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expectedLinks = expectedCollection[index];
                var actualLinks = actualCollection[index];

                LinksAssert.Equal(expectedLinks, actualLinks);
            }
        }
        #endregion
    }
}