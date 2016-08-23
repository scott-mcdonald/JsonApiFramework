// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class ResourceIdentifierAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(ResourceIdentifier expected, string actualJson)
        {
            Assert.NotNull(expected);
            Assert.False(String.IsNullOrEmpty(actualJson));

            var actualJToken = JToken.Parse(actualJson);
            ResourceIdentifierAssert.Equal(expected, actualJToken);
        }

        public static void Equal(ResourceIdentifier expected, JToken actualJToken)
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

            Assert.Equal(expected.Type, (string)actualJObject.SelectToken(Keywords.Type));
            Assert.Equal(expected.Id, (string)actualJObject.SelectToken(Keywords.Id));

            var actualMetaJToken = actualJObject.SelectToken(Keywords.Meta);
            ObjectAssert.Equal(expected.Meta, actualMetaJToken);
        }

        public static void Equal(IReadOnlyList<ResourceIdentifier> expectedCollection, JToken actualJToken)
        {
            // Handle when 'expected' is null.
            if (expectedCollection == null)
            {
                ObjectAssert.IsNull(actualJToken);
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
                var expectedResourceIdentifier = expectedCollection[index];
                var actualResourceIdentifierJToken = actualJArray[index];

                Assert.NotNull(actualResourceIdentifierJToken);
                Assert.Equal(JTokenType.Object, actualResourceIdentifierJToken.Type);

                var actualResourceIdentifierJObject = (JObject)actualResourceIdentifierJToken;
                ResourceIdentifierAssert.Equal(expectedResourceIdentifier, actualResourceIdentifierJObject);
            }
        }

        public static void Equal(ResourceIdentifier expected, ResourceIdentifier actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            Assert.Equal(expected.Type, actual.Type);
            Assert.Equal(expected.Id, actual.Id);
            MetaAssert.Equal(expected.Meta, actual.Meta);
        }

        public static void Equal(IReadOnlyList<ResourceIdentifier> expectedCollection, IReadOnlyList<ResourceIdentifier> actualCollection)
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

                ResourceIdentifierAssert.Equal(expected, actual);
            }
        }
        #endregion
    }
}