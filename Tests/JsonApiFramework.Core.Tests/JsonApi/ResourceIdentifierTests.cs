// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Internal;
using JsonApiFramework.Tests.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class ResourceIdentifierTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceIdentifierTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(ResourceIdentifierTestData))]
        public void TestResourceIdentifierSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(ResourceIdentifierTestData))]
        public void TestResourceIdentifierDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Fact]
        public void TestResourceIdentifierEqualsMethod()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier("people", "24");
            var resourceIdentifier2 = new ResourceIdentifier("people", "42");
            var resourceIdentifier3 = new ResourceIdentifier("people", "24");
            var resourceIdentifier4 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            resourceIdentifier1.Equals(resourceIdentifier0).Should().BeFalse();
            resourceIdentifier1.Equals(resourceIdentifier1).Should().BeTrue();
            resourceIdentifier1.Equals(resourceIdentifier2).Should().BeFalse();
            resourceIdentifier1.Equals(resourceIdentifier3).Should().BeTrue();
            resourceIdentifier1.Equals(resourceIdentifier4).Should().BeFalse();

            resourceIdentifier2.Equals(resourceIdentifier0).Should().BeFalse();
            resourceIdentifier2.Equals(resourceIdentifier1).Should().BeFalse();
            resourceIdentifier2.Equals(resourceIdentifier2).Should().BeTrue();
            resourceIdentifier2.Equals(resourceIdentifier3).Should().BeFalse();
            resourceIdentifier2.Equals(resourceIdentifier4).Should().BeFalse();

            resourceIdentifier3.Equals(resourceIdentifier0).Should().BeFalse();
            resourceIdentifier3.Equals(resourceIdentifier1).Should().BeTrue();
            resourceIdentifier3.Equals(resourceIdentifier2).Should().BeFalse();
            resourceIdentifier3.Equals(resourceIdentifier3).Should().BeTrue();
            resourceIdentifier3.Equals(resourceIdentifier4).Should().BeFalse();

            resourceIdentifier4.Equals(resourceIdentifier0).Should().BeFalse();
            resourceIdentifier4.Equals(resourceIdentifier1).Should().BeFalse();
            resourceIdentifier4.Equals(resourceIdentifier2).Should().BeFalse();
            resourceIdentifier4.Equals(resourceIdentifier3).Should().BeFalse();
            resourceIdentifier4.Equals(resourceIdentifier4).Should().BeTrue();
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierEqualsOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier("people", "24");
            var resourceIdentifier2 = new ResourceIdentifier("people", "42");
            var resourceIdentifier3 = new ResourceIdentifier("people", "24");
            var resourceIdentifier4 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 == resourceIdentifier0).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier1 == resourceIdentifier1).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier1 == resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier1 == resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier1 == resourceIdentifier4).Should().BeFalse();

            (resourceIdentifier2 == resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier2 == resourceIdentifier1).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier2 == resourceIdentifier2).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier2 == resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier2 == resourceIdentifier4).Should().BeFalse();

            (resourceIdentifier3 == resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier3 == resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier3 == resourceIdentifier2).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier3 == resourceIdentifier3).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier3 == resourceIdentifier4).Should().BeFalse();

            (resourceIdentifier4 == resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier4 == resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier4 == resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier4 == resourceIdentifier3).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier4 == resourceIdentifier4).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void TestResourceIdentifierNotEqualsOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier("people", "24");
            var resourceIdentifier2 = new ResourceIdentifier("people", "42");
            var resourceIdentifier3 = new ResourceIdentifier("people", "24");
            var resourceIdentifier4 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 != resourceIdentifier0).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier1 != resourceIdentifier1).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier1 != resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier1 != resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier1 != resourceIdentifier4).Should().BeTrue();

            (resourceIdentifier2 != resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier2 != resourceIdentifier1).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier2 != resourceIdentifier2).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier2 != resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier2 != resourceIdentifier4).Should().BeTrue();

            (resourceIdentifier3 != resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier3 != resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier3 != resourceIdentifier2).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier3 != resourceIdentifier3).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier3 != resourceIdentifier4).Should().BeTrue();

            (resourceIdentifier4 != resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier4 != resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier4 != resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier4 != resourceIdentifier3).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier4 != resourceIdentifier4).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void TestResourceIdentifierCompareToMethod()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier("people", "24");
            var resourceIdentifier2 = new ResourceIdentifier("people", "42");
            var resourceIdentifier3 = new ResourceIdentifier("people", "24");
            var resourceIdentifier4 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            (resourceIdentifier1.CompareTo(resourceIdentifier0) > 0).Should().BeTrue();
            (resourceIdentifier1.CompareTo(resourceIdentifier1) == 0).Should().BeTrue();
            (resourceIdentifier1.CompareTo(resourceIdentifier2) < 0).Should().BeTrue();
            (resourceIdentifier1.CompareTo(resourceIdentifier3) == 0).Should().BeTrue();
            (resourceIdentifier1.CompareTo(resourceIdentifier4) > 0).Should().BeTrue();

            (resourceIdentifier2.CompareTo(resourceIdentifier0) > 0).Should().BeTrue();
            (resourceIdentifier2.CompareTo(resourceIdentifier1) > 0).Should().BeTrue();
            (resourceIdentifier2.CompareTo(resourceIdentifier2) == 0).Should().BeTrue();
            (resourceIdentifier2.CompareTo(resourceIdentifier3) > 0).Should().BeTrue();
            (resourceIdentifier2.CompareTo(resourceIdentifier4) > 0).Should().BeTrue();

            (resourceIdentifier3.CompareTo(resourceIdentifier0) > 0).Should().BeTrue();
            (resourceIdentifier3.CompareTo(resourceIdentifier1) == 0).Should().BeTrue();
            (resourceIdentifier3.CompareTo(resourceIdentifier2) < 0).Should().BeTrue();
            (resourceIdentifier3.CompareTo(resourceIdentifier3) == 0).Should().BeTrue();
            (resourceIdentifier3.CompareTo(resourceIdentifier4) > 0).Should().BeTrue();

            (resourceIdentifier4.CompareTo(resourceIdentifier0) > 0).Should().BeTrue();
            (resourceIdentifier4.CompareTo(resourceIdentifier1) < 0).Should().BeTrue();
            (resourceIdentifier4.CompareTo(resourceIdentifier2) < 0).Should().BeTrue();
            (resourceIdentifier4.CompareTo(resourceIdentifier3) < 0).Should().BeTrue();
            (resourceIdentifier4.CompareTo(resourceIdentifier4) == 0).Should().BeTrue();
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierLessThanOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier("people", "24");
            var resourceIdentifier2 = new ResourceIdentifier("people", "42");
            var resourceIdentifier3 = new ResourceIdentifier("people", "24");
            var resourceIdentifier4 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 < resourceIdentifier0).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier1 < resourceIdentifier1).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier1 < resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier1 < resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier1 < resourceIdentifier4).Should().BeFalse();

            (resourceIdentifier2 < resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier2 < resourceIdentifier1).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier2 < resourceIdentifier2).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier2 < resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier2 < resourceIdentifier4).Should().BeFalse();

            (resourceIdentifier3 < resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier3 < resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier3 < resourceIdentifier2).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier3 < resourceIdentifier3).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier3 < resourceIdentifier4).Should().BeFalse();

            (resourceIdentifier4 < resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier4 < resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier4 < resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier4 < resourceIdentifier3).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier4 < resourceIdentifier4).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierLessThanOrEqualToOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier("people", "24");
            var resourceIdentifier2 = new ResourceIdentifier("people", "42");
            var resourceIdentifier3 = new ResourceIdentifier("people", "24");
            var resourceIdentifier4 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 <= resourceIdentifier0).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier1 <= resourceIdentifier1).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier1 <= resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier1 <= resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier1 <= resourceIdentifier4).Should().BeFalse();

            (resourceIdentifier2 <= resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier2 <= resourceIdentifier1).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier2 <= resourceIdentifier2).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier2 <= resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier2 <= resourceIdentifier4).Should().BeFalse();

            (resourceIdentifier3 <= resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier3 <= resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier3 <= resourceIdentifier2).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier3 <= resourceIdentifier3).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier3 <= resourceIdentifier4).Should().BeFalse();

            (resourceIdentifier4 <= resourceIdentifier0).Should().BeFalse();
            (resourceIdentifier4 <= resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier4 <= resourceIdentifier2).Should().BeTrue();
            (resourceIdentifier4 <= resourceIdentifier3).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier4 <= resourceIdentifier4).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierGreaterThanOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier("people", "24");
            var resourceIdentifier2 = new ResourceIdentifier("people", "42");
            var resourceIdentifier3 = new ResourceIdentifier("people", "24");
            var resourceIdentifier4 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 > resourceIdentifier0).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier1 > resourceIdentifier1).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier1 > resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier1 > resourceIdentifier3).Should().BeFalse();
            (resourceIdentifier1 > resourceIdentifier4).Should().BeTrue();

            (resourceIdentifier2 > resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier2 > resourceIdentifier1).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier2 > resourceIdentifier2).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier2 > resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier2 > resourceIdentifier4).Should().BeTrue();

            (resourceIdentifier3 > resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier3 > resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier3 > resourceIdentifier2).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier3 > resourceIdentifier3).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier3 > resourceIdentifier4).Should().BeTrue();

            (resourceIdentifier4 > resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier4 > resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier4 > resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier4 > resourceIdentifier3).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier4 > resourceIdentifier4).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void TestResourceIdentifierGreaterThanOrEqualToOperator()
        {
            // Arrange
            var resourceIdentifier0 = default(ResourceIdentifier);
            var resourceIdentifier1 = new ResourceIdentifier("people", "24");
            var resourceIdentifier2 = new ResourceIdentifier("people", "42");
            var resourceIdentifier3 = new ResourceIdentifier("people", "24");
            var resourceIdentifier4 = new ResourceIdentifier("comments", "24");

            // Act

            // Assert

            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable EqualExpressionComparison
            (resourceIdentifier1 >= resourceIdentifier0).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier1 >= resourceIdentifier1).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier1 >= resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier1 >= resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier1 >= resourceIdentifier4).Should().BeTrue();

            (resourceIdentifier2 >= resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier2 >= resourceIdentifier1).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier2 >= resourceIdentifier2).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier2 >= resourceIdentifier3).Should().BeTrue();
            (resourceIdentifier2 >= resourceIdentifier4).Should().BeTrue();

            (resourceIdentifier3 >= resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier3 >= resourceIdentifier1).Should().BeTrue();
            (resourceIdentifier3 >= resourceIdentifier2).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier3 >= resourceIdentifier3).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            (resourceIdentifier3 >= resourceIdentifier4).Should().BeTrue();

            (resourceIdentifier4 >= resourceIdentifier0).Should().BeTrue();
            (resourceIdentifier4 >= resourceIdentifier1).Should().BeFalse();
            (resourceIdentifier4 >= resourceIdentifier2).Should().BeFalse();
            (resourceIdentifier4 >= resourceIdentifier3).Should().BeFalse();
#pragma warning disable CS1718 // Comparison made to same variable
            (resourceIdentifier4 >= resourceIdentifier4).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
            // ReSharper restore EqualExpressionComparison
            // ReSharper restore ExpressionIsAlwaysNull
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly JsonSerializerSettings TestJsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None
        };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNull = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNull = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Include
        };

        private static readonly ResourceIdentifierMeta ResourceIdentifierMetaTestData = new ResourceIdentifierMeta
        {
            Version = 1.23m
        };

        public static readonly IEnumerable<object[]> ResourceIdentifierTestData = new[]
        {
            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ResourceIdentifier>(x),
                    x => new JsonObjectDeserializeUnitTest<ResourceIdentifier>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        default(ResourceIdentifier),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ResourceIdentifier>(x),
                    x => new JsonObjectDeserializeUnitTest<ResourceIdentifier>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        default(ResourceIdentifier),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ResourceIdentifier>(x),
                    x => new JsonObjectDeserializeUnitTest<ResourceIdentifier>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithTypeAndIdAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new ResourceIdentifier("articles", "42"),
                        "{\"type\":\"articles\",\"id\":\"42\"}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ResourceIdentifier>(x),
                    x => new JsonObjectDeserializeUnitTest<ResourceIdentifier>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithTypeAndIdAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new ResourceIdentifier("articles", "42"),
                        "{\"type\":\"articles\",\"id\":\"42\",\"meta\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ResourceIdentifier>(x),
                    x => new JsonObjectDeserializeUnitTest<ResourceIdentifier>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithTypeAndIdAndMeta",
                        TestJsonSerializerSettings,
                        new ResourceIdentifier("articles", "42", new WriteMeta<ResourceIdentifierMeta>(ResourceIdentifierMetaTestData)),
                        new ResourceIdentifier("articles", "42", new ReadMeta(JObject.FromObject(ResourceIdentifierMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                        "{\"type\":\"articles\",\"id\":\"42\",\"meta\":{\"version\":1.23}}"))
            },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Meta Types
        [JsonObject(MemberSerialization.OptIn)]
        public class ResourceIdentifierMeta : JsonObject
        {
            [JsonProperty("version")] public decimal Version { get; set; }
        }
        #endregion
    }
}
