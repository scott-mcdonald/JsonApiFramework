// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class ApiPropertyAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(ApiProperty expected, JToken actualJToken)
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
            Assert.Equal(JTokenType.Property, actualJTokenType);

            var actualJProperty = (JProperty)actualJToken;

            Assert.Equal(expected.Name, actualJProperty.Name);
            ClrObjectAssert.Equal(expected.ValueAsObject(), actualJProperty.Value);
        }

        public static void Equal(IReadOnlyList<ApiProperty> expectedCollection, JToken actualJToken)
        {
            // Handle when 'expected' is null.
            if (expectedCollection == null)
            {
                ClrObjectAssert.IsNull(actualJToken);
                return;
            }

            // Handle when 'expected' is not null.
            Assert.NotNull(actualJToken);

            var actualJTokenType = actualJToken.Type;
            Assert.Equal(JTokenType.Array, actualJTokenType);

            var actualJArray = (JArray)actualJToken;

            Assert.Equal(expectedCollection.Count, actualJArray.Count);
            var count = expectedCollection.Count;

            for (var index = 0; index < count; ++index)
            {
                var expectedObjectProperty = expectedCollection[index];
                var actualObjectPropertyJToken = actualJArray[index];

                Assert.NotNull(actualObjectPropertyJToken);
                Assert.Equal(JTokenType.Object, actualObjectPropertyJToken.Type);

                var actualObjectPropertyJObject = (JObject)actualObjectPropertyJToken;
                ApiPropertyAssert.Equal(expectedObjectProperty, actualObjectPropertyJObject);
            }
        }

        public static void Equal(ApiProperty expected, ApiProperty actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.Name, actual.Name);
            ClrObjectAssert.Equal(expected.ValueAsObject(), actual.ValueAsObject());
        }

        public static void Equal(IReadOnlyList<ApiProperty> expectedCollection, IReadOnlyList<ApiProperty> actualCollection)
        {
            if (expectedCollection == null)
            {
                Assert.Null(actualCollection);
                return;
            }

            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expected = expectedCollection[index];
                var actual = actualCollection[index];

                ApiPropertyAssert.Equal(expected, actual);
            }
        }
        #endregion
    }
}
