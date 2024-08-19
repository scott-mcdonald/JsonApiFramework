// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Text.Json;
using JsonApiFramework.JsonApi;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class RelationshipsAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(Relationships expected, string actualJson)
    {
        Assert.NotNull(expected);
        Assert.False(string.IsNullOrEmpty(actualJson));

        var actualJsonElement = JsonSerializer.SerializeToElement(actualJson);
        RelationshipsAssert.Equal(expected, actualJsonElement);
    }

    public static void Equal(Relationships expected, JsonElement actualJsonElement)
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

        var expectedRelationshipsCount = expected.Count;
        var actualRelationshipsCount = actualJsonElement.EnumerateObject().Count();
        Assert.Equal(expectedRelationshipsCount, actualRelationshipsCount);

        foreach (var key in expected.Keys)
        {
            var expectedRelationship = expected[key];
            Assert.IsAssignableFrom<Relationship>(expectedRelationship);

            var actualRelationshipJsonElement = actualJsonElement.GetProperty(key);
            RelationshipAssert.Equal(expectedRelationship, actualRelationshipJsonElement);
        }
    }

    public static void Equal(Relationships expected, Relationships actual)
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

            var expectedRelationship = expected[key];
            Assert.IsAssignableFrom<Relationship>(expectedRelationship);

            var actualRelationship = actual[key];
            Assert.IsAssignableFrom<Relationship>(actualRelationship);

            RelationshipAssert.Equal(expectedRelationship, actualRelationship);
        }
    }

    public static void Equal(IEnumerable<Relationships> expected, IEnumerable<Relationships> actual)
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
            var expectedRelationships = expectedCollection[index];
            var actualRelationships = actualCollection[index];

            RelationshipsAssert.Equal(expectedRelationships, actualRelationships);
        }
    }
    #endregion
}