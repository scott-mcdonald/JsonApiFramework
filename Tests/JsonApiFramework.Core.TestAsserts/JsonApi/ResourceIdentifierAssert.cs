// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class ResourceIdentifierAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(ResourceIdentifier expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJToken = JsonSerializer.SerializeToElement(actualJson);
        ResourceIdentifierAssert.Equal(expected, actualJToken);
    }

    public static void Equal(ResourceIdentifier expected, JsonElement actualJsonElement)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            ClrObjectAssert.IsNull(actualJsonElement);
            return;
        }

        // Handle when 'expected' is not null.
        Assert.NotNull(actualJsonElement);

        var actualJsonValueKind = actualJsonElement.ValueKind;
        Assert.Equal(JsonValueKind.Object, actualJsonValueKind);

        Assert.Equal(expected.Type, actualJsonElement.GetProperty(Keywords.Type).GetString());
        Assert.Equal(expected.Id, actualJsonElement.GetProperty(Keywords.Id).GetString());

        var actualMetaJToken = actualJsonElement.GetProperty(Keywords.Meta);
        ClrObjectAssert.Equal(expected.Meta, actualMetaJToken);
    }

    public static void Equal(IReadOnlyList<ResourceIdentifier> expectedCollection, JsonElement actualJsonElement)
    {
        // Handle when 'expected' is null.
        if (expectedCollection == null)
        {
            ClrObjectAssert.IsNull(actualJsonElement);
            return;
        }

        // Handle when 'expected' is not null.
        Assert.NotNull(actualJsonElement);

        var actualJsonValueKind = actualJsonElement.ValueKind;
        Assert.Equal(JsonValueKind.Array, actualJsonValueKind);

        Assert.Equal(expectedCollection.Count, actualJsonElement.EnumerateArray().Count());
        var count = expectedCollection.Count;

        for (var index = 0; index < count; ++index)
        {
            var expectedResourceIdentifier = expectedCollection[index];
            var actualResourceIdentifierJsonElement = actualJsonElement[index];

            Assert.NotNull(actualResourceIdentifierJsonElement);
            Assert.Equal(JsonValueKind.Object, actualResourceIdentifierJsonElement.ValueKind);

            ResourceIdentifierAssert.Equal(expectedResourceIdentifier, actualResourceIdentifierJsonElement);
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