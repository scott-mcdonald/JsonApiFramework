// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

using FluentAssertions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit;

namespace JsonApiFramework.TestAsserts.JsonApi
{
    public static class ClrObjectAssert
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Assert Methods
        public static void Equal(object expected, JToken actualJToken)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                ClrObjectAssert.IsNull(actualJToken);
                return;
            }

            // Handle when 'expected' is not null.
            Assert.NotNull(actualJToken);

            var actualJTokenType = actualJToken.Type;
            switch (actualJTokenType)
            {
                case JTokenType.Array:
                    {
                        var expectedArray = ((IEnumerable<object>)expected).ToList();
                        var actualJArray = (JArray)actualJToken;

                        Assert.Equal(expectedArray.Count, actualJArray.Count);
                        var count = expectedArray.Count;
                        for (var i = 0; i < count; ++i)
                        {
                            var expectedItem = expectedArray[i];
                            var actualJTokenItem = actualJArray[i];
                            ClrObjectAssert.Equal(expectedItem, actualJTokenItem);
                        }
                    }
                    break;

                case JTokenType.Object:
                    {
                        var actualJObject = (JObject)actualJToken;
                        ClrObjectAssert.Equal(expected, actualJObject);
                    }
                    break;

                case JTokenType.Integer:
                    {
                        var expectedInteger = Convert.ToInt64(expected);
                        var actualInteger = (long)actualJToken;
                        Assert.Equal(expectedInteger, actualInteger);
                    }
                    break;

                case JTokenType.Float:
                    {
                        var expectedFloat = Convert.ToDecimal(expected);
                        var actualFloat = (decimal)actualJToken;
                        Assert.Equal(expectedFloat, actualFloat);
                    }
                    break;

                case JTokenType.String:
                    {
                        // Special case if expected is or derives from JToken.
                        var expectedType = expected.GetType();
                        if (typeof(JToken).IsAssignableFrom(expectedType))
                        {
                            Assert.Equal(expected, actualJToken);
                            return;
                        }

                        var actualString = (string)actualJToken;
                        var actual = TypeConverter.Convert(actualString, expectedType);
                        Assert.Equal(expected, actual);
                    }
                    break;

                case JTokenType.Boolean:
                    {
                        var expectedBool = Convert.ToBoolean(expected);
                        var actualBool = (bool)actualJToken;
                        Assert.Equal(expectedBool, actualBool);
                    }
                    break;

                case JTokenType.Date:
                    {
                        var expectedType = expected.GetType();
                        if (expectedType == typeof(DateTime))
                        {
                            var expectedDateTime = (DateTime)expected;
                            var jsonSerializerSettings = new JsonSerializerSettings
                            {
                                DateParseHandling = DateParseHandling.DateTime
                            };
                            var jsonSerializer = JsonSerializer.Create(jsonSerializerSettings);
                            var actualDateTime = actualJToken.ToObject<DateTime>(jsonSerializer);
                            Assert.Equal(expectedDateTime, actualDateTime);
                        }
                        else if (expectedType == typeof(DateTimeOffset))
                        {
                            var expectedDateTimeOffset = (DateTimeOffset)expected;
                            var jsonSerializerSettings = new JsonSerializerSettings
                                {
                                    DateParseHandling = DateParseHandling.DateTimeOffset
                                };
                            var jsonSerializer = JsonSerializer.Create(jsonSerializerSettings);
                            var actualDateTimeOffset = actualJToken.ToObject<DateTimeOffset>(jsonSerializer);
                            Assert.Equal(expectedDateTimeOffset, actualDateTimeOffset);
                        }
                        else
                        {
                            Assert.True(false, "Expected date type is not DateTime or DateTimeOffset.");
                        }
                    }
                    break;

                default:
                    Assert.True(false, String.Format("Unknown actual JTokenType [value={0}] in expected object to actual JToken assert method.", actualJTokenType));
                    break;
            }
        }

        public static void Equal(object expected, JObject actualJObject)
        {
            // Handle when 'expected' is null.
            if (expected == null)
            {
                ClrObjectAssert.IsNull(actualJObject);
                return;
            }

            // Handle when 'expected' is not null.
            Assert.NotNull(actualJObject);

            var expectedTypeInfo = expected.GetType().GetTypeInfo();

            // .. Handle special case when expected type is 'Resource'
            if (expectedTypeInfo == ResourceTypeInfo)
            {
                var expectedResource = (Resource)expected;
                ResourceAssert.Equal(expectedResource, actualJObject);
            }
            // .. Handle special case when expected type is 'ResourceIdentifier'
            else if (expectedTypeInfo == ResourceIdentifierTypeInfo)
            {
                var expectedResourceIdentifier = (ResourceIdentifier)expected;
                ResourceIdentifierAssert.Equal(expectedResourceIdentifier, actualJObject);
            }
            // .. Handle special case when expected type is 'JObject'
            else if (expectedTypeInfo == JObjectTypeInfo)
            {
                var expectedJObject = (JObject)expected;
                Assert.Equal(expectedJObject, actualJObject);
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
                    var actualJToken = actualJObject.SelectToken(actualPropertyName);

                    ClrObjectAssert.Equal(expectedPropertyValue, actualJToken);
                }
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
                if (actualType1.IsSubclassOf(typeof(JToken)))
                {
                    var actualJToken = (JToken)actual;
                    ClrObjectAssert.IsNull(actualJToken);
                    return;
                }
            }

            // Handle when 'expected' is not null.
            Assert.NotNull(actual);

            var actualType2 = actual.GetType();
            if (actualType2.IsSubclassOf(typeof(JToken)))
            {
                var actualJToken = (JToken)actual;
                ClrObjectAssert.Equal(expected, actualJToken);
            }
            else
            {
                // Use FluentAssertions to compare for attribute equality
                // instead of reference equality in Assert.Equal(expectetd, actual)
                actual.ShouldBeEquivalentTo(expected);
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

        public static void IsNull(JToken actualJToken)
        {
            if (actualJToken == null)
                return;

            var actualJTokenType = actualJToken.Type;
            switch (actualJTokenType)
            {
                case JTokenType.None:
                case JTokenType.Null:
                    return;
            }

            Assert.True(false, String.Format("Invalid JToken [type={0}] for IsNull test.", actualJTokenType));
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

            var jsonPropertyCustomAttribute = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
            if (jsonPropertyCustomAttribute == null)
                return dotNetProperName;

            var jsonApiPropertyName = jsonPropertyCustomAttribute.PropertyName;
            return String.IsNullOrWhiteSpace(jsonApiPropertyName) == false
                ? jsonApiPropertyName
                : dotNetProperName;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly TypeInfo JObjectTypeInfo = typeof(JObject).GetTypeInfo();
        private static readonly TypeInfo ResourceTypeInfo = typeof(Resource).GetTypeInfo();
        private static readonly TypeInfo ResourceIdentifierTypeInfo = typeof(ResourceIdentifier).GetTypeInfo();
        #endregion
    }
}