// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class ResourceAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(Resource expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJsonElement = JsonSerializer.SerializeToElement(actualJson);
        ResourceAssert.Equal(expected, actualJsonElement);
    }

    public static void Equal(Resource expected, JsonElement actualJsonElement)
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

        // Type
        Assert.Equal(expected.Type, actualJsonElement.GetProperty(Keywords.Type).GetString());

        // Id
        Assert.Equal(expected.Id, actualJsonElement.GetProperty(Keywords.Id).GetString());

        // Attributes
        var actualAttributesJsonElement = actualJsonElement.GetProperty(Keywords.Attributes);
        ApiObjectAssert.Equal(expected.Attributes, actualAttributesJsonElement);

        // Relationships
        var actualRelationshipsJsonElement = actualJsonElement.GetProperty(Keywords.Relationships);
        RelationshipsAssert.Equal(expected.Relationships, actualRelationshipsJsonElement);

        // Links
        var actualLinksJsonElement = actualJsonElement.GetProperty(Keywords.Links);
        LinksAssert.Equal(expected.Links, actualLinksJsonElement);

        // Meta
        var actualMetaJsonElement = actualJsonElement.GetProperty(Keywords.Meta);
        MetaAssert.Equal(expected.Meta, actualMetaJsonElement);
    }

    public static void Equal(IEnumerable<Resource> expected, JsonElement actualJsonElement)
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
        Assert.Equal(JsonValueKind.Array, actualJsonElementValueKind);

        var expectedCollection = expected.SafeToReadOnlyList();

        Assert.Equal(expectedCollection.Count, actualJsonElement.EnumerateArray().Count());
        var count = expectedCollection.Count;

        for (var index = 0; index < count; ++index)
        {
            var expectedResource = expectedCollection[index];
            var actualResourceJsonElement = actualJsonElement[index];

            Assert.NotNull(actualResourceJsonElement);
            Assert.Equal(JsonValueKind.Object, actualResourceJsonElement.ValueKind);

            ResourceAssert.Equal(expectedResource, actualResourceJsonElement);
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