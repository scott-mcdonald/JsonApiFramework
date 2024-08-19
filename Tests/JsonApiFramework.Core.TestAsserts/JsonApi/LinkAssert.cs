// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class LinkAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(Link expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJsonElement = JsonSerializer.SerializeToElement(actualJson);
        LinkAssert.Equal(expected, actualJsonElement);
    }

    public static void Equal(Link expected, JsonElement actualJsonElement)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            ClrObjectAssert.IsNull(actualJsonElement);
            return;
        }

        // Handle when 'expected' is not null.
        var expectedHRef = expected.HRef;
        var expectedMeta = expected.Meta;

        Assert.NotNull(actualJsonElement);

        var actualJsonElementValueKind = actualJsonElement.ValueKind;
        switch (actualJsonElementValueKind)
        {
            case JsonValueKind.String:
                {
                    var actualHRef = actualJsonElement.GetString();
                    Assert.Equal(expectedHRef, actualHRef);

                    Assert.Null(expectedMeta);
                }
                break;

            case JsonValueKind.Object:
                {
                    // HRef String
                    JsonElement actualHRefJsonElement;
                    try
                    {
                        actualHRefJsonElement = actualJsonElement.GetProperty(Keywords.HRef);
                    }
                    catch (KeyNotFoundException)
                    {
                        if (expectedHRef == null)
                        {
                            return;
                        }
                        throw;
                    }

                    if (expectedHRef == null)
                    {
                        var actualHRefJsonValueKind = actualHRefJsonElement.ValueKind;
                        Assert.Equal(JsonValueKind.Null, actualHRefJsonValueKind);
                    }
                    else
                    {
                        Assert.Equal(JsonValueKind.String, actualHRefJsonElement.ValueKind);

                        var actualHRef = actualHRefJsonElement.GetString();
                        Assert.Equal(expectedHRef, actualHRef);
                    }

                    // Meta Object
                    var actualMetaJsonElement = actualJsonElement.GetProperty(Keywords.Meta);
                    ClrObjectAssert.Equal(expectedMeta, actualMetaJsonElement);
                }
                break;

            case JsonValueKind.Undefined:
            case JsonValueKind.Null:
                {
                    Assert.Null(expectedHRef);
                    Assert.Null(expectedMeta);
                }
                break;

            default:
                Assert.True(false, string.Format("Invalid JsonElement [type={0}] for link.", actualJsonElementValueKind));
                return;
        }
    }

    public static void Equal(Link expected, Link actual)
    {
        if (expected == null)
        {
            Assert.Null(actual);
            return;
        }
        Assert.NotNull(actual);

        // HRef
        Assert.Equal(expected.HRef, actual.HRef);

        // Meta
        MetaAssert.Equal(expected.Meta, actual.Meta);
    }

    public static void Equal(IEnumerable<Link> expected, IEnumerable<Link> actual)
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
            var expectedLink = expectedCollection[index];
            var actualLink = actualCollection[index];

            LinkAssert.Equal(expectedLink, actualLink);
        }
    }
    #endregion
}