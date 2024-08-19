// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class LinksAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(Links expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJsonElement = JsonSerializer.SerializeToElement(actualJson);
        LinksAssert.Equal(expected, actualJsonElement);
    }

    public static void Equal(Links expected, JsonElement actualJsonElement)
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

        var expectedLinksCount = expected.Count;
        var actualLinksCount = actualJsonElement.EnumerateObject().Count();
        Assert.Equal(expectedLinksCount, actualLinksCount);

        foreach (var key in expected.Keys)
        {
            var expectedLink = expected[key];
            Assert.IsType<Link>(expectedLink);

            var actualLinkJsonElement = actualJsonElement.GetProperty(key);
            LinkAssert.Equal(expectedLink, actualLinkJsonElement);
        }
    }

    public static void Equal(Links expected, Links actual)
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

            var expectedLink = expected[key];
            Assert.IsType<Link>(expectedLink);

            var actualLink = actual[key];
            Assert.IsType<Link>(actualLink);

            LinkAssert.Equal(expectedLink, actualLink);
        }
    }

    public static void Equal(IEnumerable<Links> expected, IEnumerable<Links> actual)
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
            var expectedLinks = expectedCollection[index];
            var actualLinks = actualCollection[index];

            LinksAssert.Equal(expectedLinks, actualLinks);
        }
    }
    #endregion
}