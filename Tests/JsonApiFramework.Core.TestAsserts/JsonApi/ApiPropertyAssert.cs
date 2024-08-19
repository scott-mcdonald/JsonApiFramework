// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class ApiPropertyAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(ApiProperty expected, JsonProperty actualJsonProperty)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            ClrObjectAssert.IsNull(actualJsonProperty.Value);
            return;
        }

        // Handle when 'expected' is not null.
        Assert.NotNull(actualJsonProperty);

        Assert.Equal(expected.Name, actualJsonProperty.Name);
        ClrObjectAssert.Equal(expected.ValueAsObject(), actualJsonProperty.Value);
    }

    public static void Equal(IReadOnlyList<ApiProperty> expectedCollection, JsonElement actualJsonElement)
    {
        // Handle when 'expected' is null.
        if (expectedCollection == null)
        {
            ClrObjectAssert.IsNull(actualJsonElement);
            return;
        }

        // Handle when 'expected' is not null.
        Assert.NotNull(actualJsonElement);

        var actualJsonElementValueKind = actualJsonElement.ValueKind;
        Assert.Equal(JsonValueKind.Array, actualJsonElementValueKind);

        Assert.Equal(expectedCollection.Count, actualJsonElement.EnumerateArray().Count());
        var count = expectedCollection.Count;

        for (var index = 0; index < count; ++index)
        {
            var expectedObjectProperty = expectedCollection[index];
            var actualObjectProperty = actualJsonElement.EnumerateObject().ElementAt(index);

            Assert.NotNull(actualObjectProperty);
            Assert.Equal(JsonValueKind.Object, actualObjectProperty.Value.ValueKind);

            ApiPropertyAssert.Equal(expectedObjectProperty, actualObjectProperty);
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
