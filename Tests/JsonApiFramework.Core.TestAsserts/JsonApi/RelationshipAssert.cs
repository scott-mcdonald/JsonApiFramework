// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using JsonApiFramework.JsonApi;

using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class RelationshipAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(Relationship expected, string actualJson)
        {
            Assert.NotNull(expected);
            Assert.False(String.IsNullOrEmpty(actualJson));

            var actualJToken = JToken.Parse(actualJson);
            RelationshipAssert.Equal(expected, actualJToken);
        }

        public static void Equal(Relationship expected, JToken actualJToken)
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

            // Links
            var actualLinksJToken = actualJObject.SelectToken(Keywords.Links);
            LinksAssert.Equal(expected.Links, actualLinksJToken);

            // Data

            // Relationship types can be the following:
            // 1. Relationship (Base with no Data)
            // 2. ToOneRelationship (Derived from Relationship with one resource identifier (0 or 1))
            // 3. ToManyRelationship (Derived from Relationship with many resource identifiers (N))
            var expectedType = expected.GetType();
            var actualDataJToken = actualJObject.SelectToken(Keywords.Data);
            if (actualDataJToken == null)
            {
                Assert.Equal(RelationshipTypeInfo, expectedType);
            }
            else
            {
                var actualDataJTokenType = actualDataJToken.Type;
                switch (actualDataJTokenType)
                {
                    // ToOneRelationship (empty to-one relationship)
                    case JTokenType.None:
                    case JTokenType.Null:
                        {
                            Assert.Equal(ToOneRelationshipTypeInfo, expectedType);
                            var expectedToOneRelationship = (ToOneRelationship)expected;
                            var expectedResourceIdentifier = expectedToOneRelationship.Data;
                            Assert.Null(expectedResourceIdentifier);
                        }
                        break;

                    // ToOneRelationship (non-empty to-one relationship)
                    case JTokenType.Object:
                        {
                            Assert.Equal(ToOneRelationshipTypeInfo, expectedType);

                            var expectedToOneRelationship = (ToOneRelationship)expected;
                            var expectedResourceIdentifier = expectedToOneRelationship.Data;

                            var actualResourceIdentifierJToken = actualDataJToken;
                            ResourceIdentifierAssert.Equal(expectedResourceIdentifier, actualResourceIdentifierJToken);
                        }
                        break;

                    // ToManyRelationship
                    case JTokenType.Array:
                        {
                            Assert.Equal(ToManyRelationshipTypeInfo, expectedType);

                            var expectedToManyRelationship = (ToManyRelationship)expected;
                            var expectedResourceIdentifierCollection = expectedToManyRelationship.Data;

                            if (expectedResourceIdentifierCollection.Any())
                            {
                                // ToManyRelationship (non-empty to-many relationship)
                                Assert.True(actualDataJToken.Any());

                                var actualResourceIdentifierJToken = actualDataJToken;
                                ResourceIdentifierAssert.Equal(expectedResourceIdentifierCollection, actualResourceIdentifierJToken);
                            }
                            else
                            {
                                // ToManyRelationship (empty to-many relationship)
                                Assert.False(actualDataJToken.Any());
                            }
                        }
                        break;

                    default:
                        Assert.True(false, String.Format("Invalid JToken [type={0}] for relationship.", actualJTokenType));
                        break;
                }
            }

            // Meta
            var actualMetaJToken = actualJObject.SelectToken(Keywords.Meta);
            MetaAssert.Equal(expected.Meta, actualMetaJToken);
        }

        public static void Equal(Relationship expected, Relationship actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
                return;
            }
            Assert.NotNull(actual);

            // Links
            LinksAssert.Equal(expected.Links, actual.Links);

            // Data

            // Relationship types can be the following:
            // 1. Relationship (Base with no Data)
            // 2. ToOneRelationship (Derived from Relationship with one resource identifier (0 or 1))
            // 3. ToManyRelationship (Derived from Relationship with many resource identifiers (N))
            var expectedTypeInfo = expected.GetType().GetTypeInfo();
            var actualTypeInfo = actual.GetType().GetTypeInfo();
            Assert.Equal(expectedTypeInfo, actualTypeInfo);

            var relationshipTypeInfo = expectedTypeInfo;
            if (relationshipTypeInfo == RelationshipTypeInfo)
            {
                // NOOP
            }
            else if (relationshipTypeInfo == ToOneRelationshipTypeInfo)
            {
                var expectedToOneRelationship = (ToOneRelationship)expected;
                var actualToOneRelationship = (ToOneRelationship)actual;

                var expectedResourceIdentifier = expectedToOneRelationship.Data;
                var actualResourceIdentifier = actualToOneRelationship.Data;

                ResourceIdentifierAssert.Equal(expectedResourceIdentifier, actualResourceIdentifier);
            }
            else if (relationshipTypeInfo == ToManyRelationshipTypeInfo)
            {
                var expectedToManyRelationship = (ToManyRelationship)expected;
                var actualToManyRelationship = (ToManyRelationship)actual;

                var expectedResourceIdentifierCollection = expectedToManyRelationship.Data;
                var actualResourceIdentifierCollection = actualToManyRelationship.Data;

                ResourceIdentifierAssert.Equal(expectedResourceIdentifierCollection, actualResourceIdentifierCollection);
            }
            else
            {
                Assert.True(false, String.Format("Unknown relationship type={0}", relationshipTypeInfo));
            }

            // Meta
            MetaAssert.Equal(expected.Meta, actual.Meta);
        }

        public static void Equal(IEnumerable<Relationship> expected, IEnumerable<Relationship> actual)
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
                var expectedRelationship = expectedCollection[index];
                var actualRelationship = actualCollection[index];

                RelationshipAssert.Equal(expectedRelationship, actualRelationship);
            }
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly TypeInfo RelationshipTypeInfo = typeof(Relationship).GetTypeInfo();
        private static readonly TypeInfo ToOneRelationshipTypeInfo = typeof(ToOneRelationship).GetTypeInfo();
        private static readonly TypeInfo ToManyRelationshipTypeInfo = typeof(ToManyRelationship).GetTypeInfo();
        #endregion
    }
}