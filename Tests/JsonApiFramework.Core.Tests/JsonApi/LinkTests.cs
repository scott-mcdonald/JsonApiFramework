// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Tests.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class LinkTests : XUnitTest
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
        public void TestJsonSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(LinkTestData))]
        public void TestJsonDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
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
        public void TestLinkUriPropertyWithNullHRef()
        {
            // Arrange
            var expected = new Link();

            // Act
            var uri = expected.Uri;

            // Assert
            uri.Should().BeNull();
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

            var expectedLink = new Link
                {
                    HRef = expectedHRef
                };

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
            var expected = new Link
                {
                    HRef = expectedHRef
                };

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
        public void TestLinkPathSegmentsPropertyWithNullHRef()
        {
            // Arrange
            var expected = new Link();

            // Act
            var pathSegments = expected.PathSegments
                                       .ToList();
            this.WriteLine(pathSegments.Count > 0
                ? pathSegments.Aggregate((current, next) => current + " " + next)
                : String.Empty);

            // Assert
            pathSegments.Should().NotBeNull();
            pathSegments.Should().BeEmpty();
        }

        [Fact]
        public void TestLinkPathSegmentsPropertyWithAbsoluteHRef()
        {
            // Arrange
            const int expectedPathSegmentsCount = 2;
            const string expectedPathSegmentsAtIndex0 = "articles";
            const string expectedPathSegmentsAtIndex1 = "1234";

            var expected = new Link
                {
                    HRef = "https://api.example.com/" + expectedPathSegmentsAtIndex0 + "/" + expectedPathSegmentsAtIndex1
                };

            // Act
            var actual = expected.PathSegments
                                 .ToList();
            this.WriteLine(actual.Count > 0
                ? actual.Aggregate((current, next) => current + " " + next)
                : String.Empty);

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
            var expected = new Link
                {
                    HRef = expectedPathSegmentsAtIndex0 + "/" + expectedPathSegmentsAtIndex1
                };

            // Act
            var actual = expected.PathSegments
                                 .ToList();
            this.WriteLine(actual.Count > 0
                ? actual.Aggregate((current, next) => current + " " + next)
                : String.Empty);

            // Assert
            actual.Should().NotBeNull();
            actual.Should().HaveCount(expectedPathSegmentsCount);
            actual[0].Should().Be(expectedPathSegmentsAtIndex0);
            actual[1].Should().Be(expectedPathSegmentsAtIndex1);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly JsonSerializerSettings TestSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

        private static readonly JsonSerializerSettings TestSettingsIncludeNull = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };

        private static readonly JsonSerializerSettings TestSettingsIgnoreNull = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

        // ReSharper disable MemberCanBePrivate.Global
        public static readonly IEnumerable<object[]> LinkTestData = new[]
        // ReSharper restore MemberCanBePrivate.Global
            {
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Link>(x),
                            x => new JsonObjectDeserializeUnitTest<Link>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithNull",
                                TestSettings,
                                default(Link),
                                "null"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Link>(x),
                            x => new JsonObjectDeserializeUnitTest<Link>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithEmptyObjectAndIncludeNull",
                                TestSettingsIncludeNull,
                                new Link(),
@"{
  ""href"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Link>(x),
                            x => new JsonObjectDeserializeUnitTest<Link>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithEmptyObjectAndIgnoreNull",
                                TestSettingsIgnoreNull,
                                new Link(),
                                "{}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Link>(x),
                            x => new JsonObjectDeserializeUnitTest<Link>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithHRefOnlyAsEmptyString",
                                TestSettings,
                                new Link(String.Empty),
                                "\"\""))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Link>(x),
                            x => new JsonObjectDeserializeUnitTest<Link>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithHRefOnlyAsValidUrl",
                                TestSettings,
                                new Link("https://api.example.com/articles"),
                                "\"https://api.example.com/articles\""))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<Link>(x),
                            x => new JsonObjectDeserializeUnitTest<Link>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithHRefAndMeta",
                                TestSettings,
                                new Link("https://api.example.com/articles", JsonApiSampleData.LinkMeta),
@"{
  ""href"": ""https://api.example.com/articles"",
  ""meta"": {
    ""is-public"": true,
    ""version"": ""2.0""
    }
}"))
                    },
            };
        #endregion
    }
}
