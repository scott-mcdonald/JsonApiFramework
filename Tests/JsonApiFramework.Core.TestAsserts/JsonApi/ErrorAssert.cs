// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class ErrorAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Error expected, string actualJson)
        {
            Assert.NotNull(expected);
            Assert.False(String.IsNullOrEmpty(actualJson));

            var actualJToken = JToken.Parse(actualJson);
            ErrorAssert.Equal(expected, actualJToken);
        }

        public static void Equal(Error expected, JToken actualJToken)
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

            // Id
            Assert.Equal(expected.Id, (string)actualJObject.SelectToken(Keywords.Id));

            // Status
            Assert.Equal(expected.Status, (string)actualJObject.SelectToken(Keywords.Status));

            // Code
            Assert.Equal(expected.Code, (string)actualJObject.SelectToken(Keywords.Code));

            // Title
            Assert.Equal(expected.Title, (string)actualJObject.SelectToken(Keywords.Title));

            // Detail
            Assert.Equal(expected.Detail, (string)actualJObject.SelectToken(Keywords.Detail));

            // Source
            var actualSourceJToken = actualJObject.SelectToken(Keywords.Source);
            ObjectAssert.Equal(expected.Source, actualSourceJToken);

            // Links
            var actualLinksJToken = actualJObject.SelectToken(Keywords.Links);
            LinksAssert.Equal(expected.Links, actualLinksJToken);

            // Meta
            var actualMetaJToken = actualJObject.SelectToken(Keywords.Meta);
            MetaAssert.Equal(expected.Meta, actualMetaJToken);
        }

        public static void Equal(IEnumerable<Error> expected, JToken actualJToken)
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
            Assert.Equal(JTokenType.Array, actualJTokenType);

            var actualJArray = (JArray)actualJToken;

            var expectedCollection = expected.SafeToReadOnlyList();

            Assert.Equal(expectedCollection.Count, actualJArray.Count);
            var count = expectedCollection.Count;

            for (var index = 0; index < count; ++index)
            {
                var expectedError = expectedCollection[index];
                var actualErrorJToken = actualJArray[index];

                Assert.NotNull(actualErrorJToken);
                Assert.Equal(JTokenType.Object, actualErrorJToken.Type);

                var actualErrorJObject = (JObject)actualErrorJToken;
                ErrorAssert.Equal(expectedError, actualErrorJObject);
            }
        }

        public static void Equal(Error expected, Error actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            // Id
            Assert.Equal(expected.Id, actual.Id);

            // Status
            Assert.Equal(expected.Status, actual.Status);

            // Code
            Assert.Equal(expected.Code, actual.Code);

            // Title
            Assert.Equal(expected.Title, actual.Title);

            // Detail
            Assert.Equal(expected.Detail, actual.Detail);

            // Source
            ObjectAssert.Equal(expected.Source, actual.Source);

            // Links
            LinksAssert.Equal(expected.Links, actual.Links);

            // Meta
            MetaAssert.Equal(expected.Meta, actual.Meta);
        }

        public static void Equal(IEnumerable<Error> expected, IEnumerable<Error> actual)
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
                var expectedError = expectedCollection[index];
                var actualError = actualCollection[index];

                ErrorAssert.Equal(expectedError, actualError);
            }
        }
        #endregion
    }
}