// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class MetaAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(Meta expected, JsonElement actualJsonElement)
    {
        if (expected == null)
        {
            ClrObjectAssert.IsNull(actualJsonElement);
            return;
        }

        // Handle when 'expected' is not null.
        Assert.NotNull(actualJsonElement);

        var actualJsonValueKind = actualJsonElement.ValueKind;
        Assert.Equal(JsonValueKind.Object, actualJsonValueKind);

        var actualMeta = (Meta)actualJsonElement;
        MetaAssert.Equal(expected, actualMeta);
    }

    public static void Equal(Meta expected, Meta actual)
    {
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        var expectedJsonElement = (JsonElement)expected;
        var actualJsonElement = (JsonElement)actual;
        Assert.Equal(expectedJsonElement, actualJsonElement);
    }

    public static void Equal(IEnumerable<Meta> expected, IEnumerable<Meta> actual)
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
            var expectedMeta = expectedCollection[index];
            var actualMeta = actualCollection[index];

            MetaAssert.Equal(expectedMeta, actualMeta);
        }
    }
    #endregion
}