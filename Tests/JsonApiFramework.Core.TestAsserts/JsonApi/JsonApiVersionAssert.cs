// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class JsonApiVersionAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(JsonApiVersion expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJsonElement = JsonSerializer.SerializeToElement(actualJson);
        JsonApiVersionAssert.Equal(expected, actualJsonElement);
    }

    public static void Equal(JsonApiVersion expected, JsonElement actualJsonElement)
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

        // Version
        Assert.Equal(expected.Version, actualJsonElement.GetProperty(Keywords.Version).GetString());

        // Meta
        var actualMetaJsonElement = actualJsonElement.GetProperty(Keywords.Meta);
        MetaAssert.Equal(expected.Meta, actualMetaJsonElement);
    }

    public static void Equal(JsonApiVersion expected, JsonApiVersion actual)
    {
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        // Version
        Assert.Equal(expected.Version, actual.Version);

        // Meta
        MetaAssert.Equal(expected.Meta, actual.Meta);
    }
    #endregion
}