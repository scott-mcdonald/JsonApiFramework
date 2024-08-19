// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class ErrorAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(Error expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJsonElement = JsonSerializer.SerializeToElement(actualJson);
        ErrorAssert.Equal(expected, actualJsonElement);
    }

    public static void Equal(Error expected, JsonElement actualJsonElement)
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

        // Id
        Assert.Equal(expected.Id, actualJsonElement.GetProperty(Keywords.Id).GetString());

        // Status
        Assert.Equal(expected.Status, actualJsonElement.GetProperty(Keywords.Status).GetString());

        // Code
        Assert.Equal(expected.Code, actualJsonElement.GetProperty(Keywords.Code).GetString());

        // Title
        Assert.Equal(expected.Title, actualJsonElement.GetProperty(Keywords.Title).GetString());

        // Detail
        Assert.Equal(expected.Detail, actualJsonElement.GetProperty(Keywords.Detail).GetString());

        // Source
        var actualSourceJsonElement = actualJsonElement.GetProperty(Keywords.Source);
        ClrObjectAssert.Equal(expected.Source, actualSourceJsonElement);

        // Links
        var actualLinksJsonElement = actualJsonElement.GetProperty(Keywords.Links);
        LinksAssert.Equal(expected.Links, actualLinksJsonElement);

        // Meta
        var actualMetaJsonElement = actualJsonElement.GetProperty(Keywords.Meta);
        MetaAssert.Equal(expected.Meta, actualMetaJsonElement);
    }

    public static void Equal(IEnumerable<Error> expected, JsonElement actualJsonElement)
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
        Assert.Equal(JsonValueKind.Array, actualJsonValueKind);

        var expectedCollection = expected.SafeToReadOnlyList();

        Assert.Equal(expectedCollection.Count, actualJsonElement.EnumerateArray().Count());
        var count = expectedCollection.Count;

        for (var index = 0; index < count; ++index)
        {
            var expectedError = expectedCollection[index];
            var actualErrorJsonElement = actualJsonElement[index];

            Assert.NotNull(actualErrorJsonElement);
            Assert.Equal(JsonValueKind.Object, actualErrorJsonElement.ValueKind);

            ErrorAssert.Equal(expectedError, actualErrorJsonElement);
        }
    }

    public static void Equal(Error expected, Error actual)
    {
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        // Id
        Assert.Equal(expected.Id, actual.Id);

        // Status
        Assert.Equal(expected.Status, actual.Status);

        // Code
        Assert.Equal(expected.Code, actual.Code);

        // Title
        Assert.Equal(expected.Title, actual.Title);

        // Detail
        Assert.Equal(expected.Detail, actual.Detail);

        // Source
        ClrObjectAssert.Equal(expected.Source, actual.Source);

        // Links
        LinksAssert.Equal(expected.Links, actual.Links);

        // Meta
        MetaAssert.Equal(expected.Meta, actual.Meta);
    }

    public static void Equal(IEnumerable<Error> expected, IEnumerable<Error> actual)
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
            var expectedError = expectedCollection[index];
            var actualError = actualCollection[index];

            ErrorAssert.Equal(expectedError, actualError);
        }
    }
    #endregion
}