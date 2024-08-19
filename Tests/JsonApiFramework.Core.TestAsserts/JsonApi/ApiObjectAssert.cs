﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class ApiObjectAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(ApiObject expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJsonElement = JsonSerializer.SerializeToElement(actualJson);
        ApiObjectAssert.Equal(expected, actualJsonElement);
    }

    public static void Equal(ApiObject expected, JsonElement actualJsonElement)
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

        var expectedCollection = expected.ToList();
        var actualCollection = actualJsonElement.EnumerateObject().ToList();
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