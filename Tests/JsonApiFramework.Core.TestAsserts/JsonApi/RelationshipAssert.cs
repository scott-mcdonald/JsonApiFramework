// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Reflection;
using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class RelationshipAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(Relationship expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJsonElement = JsonSerializer.SerializeToElement(actualJson);
        RelationshipAssert.Equal(expected, actualJsonElement);
    }

    public static void Equal(Relationship expected, JsonElement actualJsonElement)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            ClrObjectAssert.IsNull(actualJsonElement);
            return;
        }

        // Handle when 'expected' is not null.
        Assert.NotNull(actualJsonElement);

        var actualJsonElementValueKind = actualJsonElement.ValueKind;
        Assert.Equal(JsonValueKind.Object, actualJsonElementValueKind);

        // Links
        var actualLinksJsonElement = actualJsonElement.GetProperty(Keywords.Links);
        LinksAssert.Equal(expected.Links, actualLinksJsonElement);

        // Data

        // Relationship types can be the following:
        // 1. Relationship (Base with no Data)
        // 2. ToOneRelationship (Derived from Relationship with one resource identifier (0 or 1))
        // 3. ToManyRelationship (Derived from Relationship with many resource identifiers (N))
        var expectedType = expected.GetType();
        var actualDataJsonElement = actualJsonElement.GetProperty(Keywords.Data);
        var actualDataJsonValueKind = actualDataJsonElement.ValueKind;
        switch (actualDataJsonValueKind)
        {
            // ToOneRelationship (empty to-one relationship)
            case JsonValueKind.Undefined:
            case JsonValueKind.Null:
                {
                    Assert.Equal(ToOneRelationshipTypeInfo, expectedType);
                    var expectedToOneRelationship = (ToOneRelationship)expected;
                    var expectedResourceIdentifier = expectedToOneRelationship.Data;
                    Assert.Null(expectedResourceIdentifier);
                }
                break;

            // ToOneRelationship (non-empty to-one relationship)
            case JsonValueKind.Object:
                {
                    Assert.Equal(ToOneRelationshipTypeInfo, expectedType);

                    var expectedToOneRelationship = (ToOneRelationship)expected;
                    var expectedResourceIdentifier = expectedToOneRelationship.Data;

                    var actualResourceIdentifierJsonElement = actualDataJsonElement;
                    ResourceIdentifierAssert.Equal(expectedResourceIdentifier, actualResourceIdentifierJsonElement);
                }
                break;

            // ToManyRelationship
            case JsonValueKind.Array:
                {
                    Assert.Equal(ToManyRelationshipTypeInfo, expectedType);

                    var expectedToManyRelationship = (ToManyRelationship)expected;
                    var expectedResourceIdentifierCollection = expectedToManyRelationship.Data;

                    if (expectedResourceIdentifierCollection.Any())
                    {
                        // ToManyRelationship (non-empty to-many relationship)
                        Assert.True(actualDataJsonElement.EnumerateArray().Any());

                        var actualResourceIdentifierJsonElement = actualDataJsonElement;
                        ResourceIdentifierAssert.Equal(expectedResourceIdentifierCollection, actualResourceIdentifierJsonElement);
                    }
                    else
                    {
                        // ToManyRelationship (empty to-many relationship)
                        Assert.False(actualDataJsonElement.EnumerateArray().Any());
                    }
                }
                break;

            default:
                Assert.True(false, string.Format("Invalid JsonElement [type={0}] for relationship.", actualJsonElementValueKind));
                break;
        }

        // Meta
        var actualMetaJsonElement = actualJsonElement.GetProperty(Keywords.Meta);
        MetaAssert.Equal(expected.Meta, actualMetaJsonElement);
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
            Assert.True(false, string.Format("Unknown relationship type={0}", relationshipTypeInfo));
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