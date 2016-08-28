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
    public static class ResourceAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Resource expected, string actualJson)
        {
            Assert.NotNull(expected);
            Assert.False(String.IsNullOrEmpty(actualJson));

            var actualJToken = JToken.Parse(actualJson);
            ResourceAssert.Equal(expected, actualJToken);
        }

        public static void Equal(Resource expected, JToken actualJToken)
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

            // Type
            Assert.Equal(expected.Type, (string)actualJObject.SelectToken(Keywords.Type));

            // Id
            Assert.Equal(expected.Id, (string)actualJObject.SelectToken(Keywords.Id));

            // Attributes
            var actualAttributesJToken = actualJObject.SelectToken(Keywords.Attributes);
            ApiObjectAssert.Equal(expected.Attributes, actualAttributesJToken);

            // Relationships
            var actualRelationshipsJToken = actualJObject.SelectToken(Keywords.Relationships);
            RelationshipsAssert.Equal(expected.Relationships, actualRelationshipsJToken);

            // Links
            var actualLinksJToken = actualJObject.SelectToken(Keywords.Links);
            LinksAssert.Equal(expected.Links, actualLinksJToken);

            // Meta
            var actualMetaJToken = actualJObject.SelectToken(Keywords.Meta);
            MetaAssert.Equal(expected.Meta, actualMetaJToken);
        }

        public static void Equal(IEnumerable<Resource> expected, JToken actualJToken)
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
            Assert.Equal(JTokenType.Array, actualJTokenType);

            var actualJArray = (JArray)actualJToken;

            var expectedCollection = expected.SafeToReadOnlyList();

            Assert.Equal(expectedCollection.Count, actualJArray.Count);
            var count = expectedCollection.Count;

            for (var index = 0; index < count; ++index)
            {
                var expectedResource = expectedCollection[index];
                var actualResourceJToken = actualJArray[index];

                Assert.NotNull(actualResourceJToken);
                Assert.Equal(JTokenType.Object, actualResourceJToken.Type);

                var actualResourceJObject = (JObject)actualResourceJToken;
                ResourceAssert.Equal(expectedResource, actualResourceJObject);
            }
        }

        public static void Equal(Resource expected, Resource actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            // Type
            Assert.Equal(expected.Type, actual.Type);

            // Id
            Assert.Equal(expected.Id, actual.Id);

            // Attributes
            ApiObjectAssert.Equal(expected.Attributes, actual.Attributes);

            // Relationships
            RelationshipsAssert.Equal(expected.Relationships, actual.Relationships);

            // Links
            LinksAssert.Equal(expected.Links, actual.Links);

            // Meta
            MetaAssert.Equal(expected.Meta, actual.Meta);
        }

        public static void Equal(IEnumerable<Resource> expected, IEnumerable<Resource> actual)
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

            // Compare resources independent of order coming into this method.
            var expectedCollectionSorted = expectedCollection.OrderBy(x => x)
                                                             .ToList();
            var actualCollectionSorted = actualCollection.OrderBy(x => x)
                                                         .ToList();

            var count = expectedCollection.Count;
            for (var index = 0; index < count; ++index)
            {
                var expectedResource = expectedCollectionSorted[index];
                var actualResource = actualCollectionSorted[index];

                ResourceAssert.Equal(expectedResource, actualResource);
            }
        }
        #endregion
    }
}