// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class RelationshipsAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Relationships expected, string actualJson)
        {
            Assert.NotNull(expected);
            Assert.False(String.IsNullOrEmpty(actualJson));

            var actualJToken = JToken.Parse(actualJson);
            RelationshipsAssert.Equal(expected, actualJToken);
        }

        public static void Equal(Relationships expected, JToken actualJToken)
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

            var expectedRelationshipsCount = expected.Count;
            var actualRelationshipsCount = actualJObject.Count;
            Assert.Equal(expectedRelationshipsCount, actualRelationshipsCount);

            foreach (var key in expected.Keys)
            {
                var expectedRelationship = expected[key];
                Assert.IsAssignableFrom<Relationship>(expectedRelationship);

                var actualRelationshipJToken = actualJObject[key];
                RelationshipAssert.Equal(expectedRelationship, actualRelationshipJToken);
            }
        }

        public static void Equal(Relationships expected, Relationships actual)
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

                var expectedRelationship = expected[key];
                Assert.IsAssignableFrom<Relationship>(expectedRelationship);

                var actualRelationship = actual[key];
                Assert.IsAssignableFrom<Relationship>(actualRelationship);

                RelationshipAssert.Equal(expectedRelationship, actualRelationship);
            }
        }

        public static void Equal(IEnumerable<Relationships> expected, IEnumerable<Relationships> actual)
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
                var expectedRelationships = expectedCollection[index];
                var actualRelationships = actualCollection[index];

                RelationshipsAssert.Equal(expectedRelationships, actualRelationships);
            }
        }
        #endregion
    }
}