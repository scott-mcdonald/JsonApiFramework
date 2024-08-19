// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi;

public static class ClrObjectAssert
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Assert Methods
    public static void Equal(object expected, JsonElement actualJsonElement)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            ClrObjectAssert.IsNull(actualJsonElement);
            return;
        }

        // Handle when 'expected' is not null.
        Assert.NotNull(actualJsonElement);

        var expectedTypeInfo = expected.GetType().GetTypeInfo();

        // .. Handle special case when expected type is 'Resource'
        if (expectedTypeInfo == ResourceTypeInfo)
        {
            var expectedResource = (Resource)expected;
            ResourceAssert.Equal(expectedResource, actualJsonElement);
        }
        // .. Handle special case when expected type is 'ResourceIdentifier'
        else if (expectedTypeInfo == ResourceIdentifierTypeInfo)
        {
            var expectedResourceIdentifier = (ResourceIdentifier)expected;
            ResourceIdentifierAssert.Equal(expectedResourceIdentifier, actualJsonElement);
        }
        // .. Handle special case when expected type is 'JsonElement'
        else if (expectedTypeInfo == JsonElementTypeInfo)
        {
            var expectedJsonElement = (JsonElement)expected;
            Assert.Equal(expectedJsonElement, actualJsonElement);
        }
        // .. Handle normal case when expected type is an object that is
        // .. not 'Resource' or 'ResourceIdentifier'.
        else
        {
            var expectedProperties = expectedTypeInfo.GetProperties();
            foreach (var expectedProperty in expectedProperties)
            {
                // ReSharper disable once UnusedVariable
                var expectedPropertyName = GetDotNetPropertyName(expectedProperty);
                var expectedPropertyValue = expectedProperty.GetValue(expected, null);

                var actualPropertyName = GetJsonApiPropertyName(expectedProperty);
                var actualJsonSubElement = actualJsonElement.GetProperty(actualPropertyName);

                ClrObjectAssert.Equal(expectedPropertyValue, actualJsonSubElement);
            }
        }

        var actualJsonValueKind = actualJsonElement.ValueKind;
        switch (actualJsonValueKind)
        {
            case JsonValueKind.Array:
                {
                    var expectedArray = ((IEnumerable<object>)expected).ToList();
                    var actualJsonArray = actualJsonElement.EnumerateArray();

                    Assert.Equal(expectedArray.Count, actualJsonArray.Count());
                    var count = expectedArray.Count;
                    for (var i = 0; i < count; ++i)
                    {
                        var expectedItem = expectedArray[i];
                        var actualJsonElementItem = actualJsonArray.ElementAt(i);
                        ClrObjectAssert.Equal(expectedItem, actualJsonElementItem);
                    }
                }
                break;

            case JsonValueKind.Object:
                {
                    ClrObjectAssert.Equal(expected, actualJsonElement);
                }
                break;

            case JsonValueKind.Number:
                {
                    var expectedFloat = Convert.ToDecimal(expected);
                    var actualFloat = actualJsonElement.GetDecimal();
                    Assert.Equal(expectedFloat, actualFloat);
                }
                break;

            case JsonValueKind.String:
                {
                    // Special case if expected is or derives from JsonElement.
                    var expectedType = expected.GetType();
                    if (typeof(JsonElement).IsAssignableFrom(expectedType))
                    {
                        Assert.Equal(expected, actualJsonElement);
                        return;
                    }

                    var actualString = actualJsonElement.GetString();
                    var actual = TypeConverter.Convert(actualString, expectedType);
                    Assert.Equal(expected, actual);
                }
                break;

            case JsonValueKind.True:
            case JsonValueKind.False:
                {
                    var expectedBool = Convert.ToBoolean(expected);
                    var actualBool = actualJsonElement.GetBoolean();
                    Assert.Equal(expectedBool, actualBool);
                }
                break;

            default:
                Assert.True(false, string.Format("Unknown actual JsonValueKind [value={0}] in expected object to actual JsonElement assert method.", actualJsonValueKind));
                break;
        }
    }

    public static void Equal(object expected, object actual)
    {
        // Handle when 'expected' is null.
        if (expected == null)
        {
            if (actual == null)
                return;

            var actualType1 = actual.GetType();
            if (actualType1.IsSubclassOf(typeof(JsonElement)))
            {
                var actualJsonElement = (JsonElement)actual;
                ClrObjectAssert.IsNull(actualJsonElement);
                return;
            }
        }

        // Handle when 'expected' is not null.
        Assert.NotNull(actual);

        var actualType2 = actual.GetType();
        if (actualType2.IsSubclassOf(typeof(JsonElement)))
        {
            var actualJsonElement = (JsonElement)actual;
            ClrObjectAssert.Equal(expected, actualJsonElement);
        }
        else
        {
            // Use FluentAssertions to compare for attribute equality
            // instead of reference equality in Assert.Equal(expectetd, actual)
            actual.Should().BeEquivalentTo(expected);
        }
    }

    public static void Equal(IReadOnlyList<object> expectedCollection, IReadOnlyList<object> actualCollection)
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

            ClrObjectAssert.Equal(expected, actual);
        }
    }

    public static void IsNull(JsonElement actualJsonElement)
    {
        var actualJsonValueKind = actualJsonElement.ValueKind;
        switch (actualJsonValueKind)
        {
            case JsonValueKind.Undefined:
            case JsonValueKind.Null:
                return;
        }

        Assert.True(false, string.Format("Invalid JsonElement [type={0}] for IsNull test.", actualJsonValueKind));
    }
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Methods
    private static string GetDotNetPropertyName(PropertyInfo propertyInfo)
    {
        Contract.Requires(propertyInfo != null);

        var dotNetPropertyName = propertyInfo.Name;
        return dotNetPropertyName;
    }

    private static string GetJsonApiPropertyName(PropertyInfo propertyInfo)
    {
        Contract.Requires(propertyInfo != null);

        var dotNetProperName = GetDotNetPropertyName(propertyInfo);

        var jsonPropertyCustomAttribute = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
        if (jsonPropertyCustomAttribute == null)
            return dotNetProperName;

        var jsonApiPropertyName = jsonPropertyCustomAttribute.Name;
        return string.IsNullOrWhiteSpace(jsonApiPropertyName) == false
            ? jsonApiPropertyName
            : dotNetProperName;
    }
    #endregion

    // PRIVATE FIELDS ///////////////////////////////////////////////////
    #region Constants
    private static readonly TypeInfo JsonElementTypeInfo = typeof(JsonElement).GetTypeInfo();
    private static readonly TypeInfo ResourceTypeInfo = typeof(Resource).GetTypeInfo();
    private static readonly TypeInfo ResourceIdentifierTypeInfo = typeof(ResourceIdentifier).GetTypeInfo();
    #endregion
}