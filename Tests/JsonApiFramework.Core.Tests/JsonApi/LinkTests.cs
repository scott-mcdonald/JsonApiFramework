// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

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
    public class LinkTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinkTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(LinkTestData))]
        public void TestLinkSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(LinkTestData))]
        public void TestLinkDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Fact]
        public void TestLinkConversionOperatorToLinkFromString()
        {
            // Arrange
            const string json = "https://api.example.com/articles";
            var expected = new Link("https://api.example.com/articles");

            // Act
            this.WriteLine(json);
            var actual = (Link)json;

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void TestLinkConversionOperatorToStringFromLink()
        {
            // Arrange
            var expectedLink = new Link("https://api.example.com/articles");

            // Act
            const string expected = "https://api.example.com/articles";

            var actual = (string)expectedLink;
            this.WriteLine(actual);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void TestLinkUriPropertyWithAbsoluteHRef()
        {
            // Arrange
            const string expectedHRef = "https://api.example.com/articles/1234";
            var expectedUri = new Uri(expectedHRef);
            var expectedScheme = expectedUri.Scheme;
            var expectedHost = expectedUri.Host;
            var expectedAbsolutePath = expectedUri.AbsolutePath;

            var expectedLink = new Link(expectedHRef);

            // Act
            var actualUri = expectedLink.Uri;
            this.WriteLine(actualUri.ToString());

            var actualScheme = actualUri.Scheme;
            var actualHost = actualUri.Host;
            var actualAbsolutePath = actualUri.AbsolutePath;

            // Assert
            actualUri.Should().NotBeNull();
            actualUri.IsAbsoluteUri.Should().BeTrue();
            actualScheme.Should().Be(expectedScheme);
            actualHost.Should().Be(expectedHost);
            actualAbsolutePath.Should().Be(expectedAbsolutePath);
        }

        [Fact]
        public void TestLinkUriPropertyWithRelativeHRef()
        {
            // Arrange
            const string expectedHRef = "articles/1234";
            var expected = new Link(expectedHRef);

            // Act
            var actualUri = expected.Uri;
            this.WriteLine(actualUri.ToString());

            var actualHRef = actualUri.OriginalString;

            // Assert
            actualUri.Should().NotBeNull();
            actualUri.IsAbsoluteUri.Should().BeFalse();
            actualHRef.Should().Be(expectedHRef);
        }

        [Fact]
        public void TestLinkPathSegmentsPropertyWithAbsoluteHRef()
        {
            // Arrange
            const int expectedPathSegmentsCount = 2;
            const string expectedPathSegmentsAtIndex0 = "articles";
            const string expectedPathSegmentsAtIndex1 = "1234";

            var expected = new Link("https://api.example.com/" + expectedPathSegmentsAtIndex0 + "/" + expectedPathSegmentsAtIndex1);

            // Act
            var actual = expected.PathSegments
                                 .ToList();
            this.WriteLine(String.Join(" ", actual));

            // Assert
            actual.Should().NotBeNull();
            actual.Should().HaveCount(expectedPathSegmentsCount);
            actual[0].Should().Be(expectedPathSegmentsAtIndex0);
            actual[1].Should().Be(expectedPathSegmentsAtIndex1);
        }

        [Fact]
        public void TestLinkPathSegmentsPropertyWithRelativeHRef()
        {
            // Arrange
            const int expectedPathSegmentsCount = 2;
            const string expectedPathSegmentsAtIndex0 = "articles";
            const string expectedPathSegmentsAtIndex1 = "1234";
            var expected = new Link(expectedPathSegmentsAtIndex0 + "/" + expectedPathSegmentsAtIndex1);

            // Act
            var actual = expected.PathSegments
                                 .ToList();
            this.WriteLine(String.Join(" ", actual));

            // Assert
            actual.Should().NotBeNull();
            actual.Should().HaveCount(expectedPathSegmentsCount);
            actual[0].Should().Be(expectedPathSegmentsAtIndex0);
            actual[1].Should().Be(expectedPathSegmentsAtIndex1);
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

        private static readonly LinkMeta LinkMetaTestData = new LinkMeta
        {
            IsPublic = true
        };

        public static readonly IEnumerable<object[]> LinkTestData = new[]
        {
            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Link>(x),
                    x => new JsonObjectDeserializeUnitTest<Link>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        default(Link),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Link>(x),
                    x => new JsonObjectDeserializeUnitTest<Link>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        default(Link),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Link>(x),
                    x => new JsonObjectDeserializeUnitTest<Link>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithHRefOnlyAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new Link("https://api.example.com/articles"),
                        "\"https://api.example.com/articles\""))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Link>(x),
                    x => new JsonObjectDeserializeUnitTest<Link>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithHRefOnlyAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new Link("https://api.example.com/articles"),
                        "{\"href\":\"https://api.example.com/articles\",\"meta\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Link>(x),
                    x => new JsonObjectDeserializeUnitTest<Link>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithHRefAndMeta",
                        TestJsonSerializerSettings,
                        new Link("https://api.example.com/articles", new WriteMeta<LinkMeta>(LinkMetaTestData)),
                        new Link("https://api.example.com/articles", new ReadMeta(JObject.FromObject(LinkMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettings)))),
                        "{\"href\":\"https://api.example.com/articles\",\"meta\":{\"is-public\":true}}"))
            },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Meta Types
        [JsonObject(MemberSerialization.OptIn)]
        public class LinkMeta : JsonObject
        {
            [JsonProperty("is-public")] public bool IsPublic { get; set; }
        }
        #endregion
    }
}
