// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class ApiObjectAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(ApiObject expected, string actualJson)
        {
            Assert.NotNull(expected);
            Assert.False(String.IsNullOrEmpty(actualJson));

            var actualJToken = JToken.Parse(actualJson);
            ApiObjectAssert.Equal(expected, actualJToken);
        }

        public static void Equal(ApiObject expected, JToken actualJToken)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                ClrObjectAssert.IsNull(actualJToken);
                return;
            }

            // Handle when 'expected' is not null.
            Assert.NotNull(actualJToken);

            var actualJTokenType = actualJToken.Type;
            Assert.Equal(JTokenType.Object, actualJTokenType);

            var actualJObject = (JObject)actualJToken;

            var expectedCollection = expected.ToList();
            var actualCollection = actualJObject.Properties().ToList();
            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expectedObjectProperty = expectedCollection[index];
                var actualJProperty = actualCollection[index];

                ApiPropertyAssert.Equal(expectedObjectProperty, actualJProperty);
            }
        }

        public static void Equal(ApiObject expected, ApiObject actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }

            Assert.NotNull(actual);

            var expectedCollection = expected.ToList();
            var actualCollection = actual.ToList();
            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expectedObjectProperty = expectedCollection[index];
                var actualObjectProperty = actualCollection[index];

                ApiPropertyAssert.Equal(expectedObjectProperty, actualObjectProperty);
            }
        }

        public static void Equal(IEnumerable<ApiObject> expected, IEnumerable<ApiObject> actual)
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
                var expectedObject = expectedCollection[index];
                var actualObject = actualCollection[index];

                ApiObjectAssert.Equal(expectedObject, actualObject);
            }
        }
        #endregion
    }
}