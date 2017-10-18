// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.Http;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Tests.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class LinksTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LinksTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(LinksTestData))]
        public void TestLinksSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(LinksTestData))]
        public void TestLinksDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Fact]
        public void TestLinksTryGetLinkAndLinkExists()
        {
            // Arrange
            var links = new Links(
                new Dictionary<string, Link>
                {
                    {Keywords.Up, ArticleCollectionLink},
                    {Keywords.Self, ArticleLink}
                });

            // Act
            var expected = ArticleLink;
            Link actual;
            var actualLinkFound = links.TryGetLink(Keywords.Self, out actual);

            // Assert
            actualLinkFound.Should().BeTrue();
            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void TestLinksTryGetLinkAndLinkNotExists()
        {
            // Arrange
            var links = new Links(
                new Dictionary<string, Link>
                    {
                        {Keywords.Up, ArticleCollectionLink},
                        {Keywords.Self, ArticleLink}
                    });

            // Act
            Link actual;
            var actualLinkFound = links.TryGetLink(Keywords.Next, out actual);

            // Assert
            actualLinkFound.Should().BeFalse();
            actual.Should().BeNull();
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public static readonly IUrlBuilderConfiguration UrlBuilderConfiguration = new UrlBuilderConfiguration("https", "api.example.com");

        public static readonly string ArticleCollectionHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path("articles").Build();
        public static readonly Link ArticleCollectionLink = new Link(ArticleCollectionHRef);

        public static readonly string ArticleHRef = UrlBuilder.Create(UrlBuilderConfiguration).Path("articles").Path("42").Build();
        public static readonly Link ArticleLink = new Link(ArticleHRef);

        private static readonly JsonSerializerSettings TestJsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static readonly IEnumerable<object[]> LinksTestData = new[]
        {
            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Links>(x),
                    x => new JsonObjectDeserializeUnitTest<Links>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObject",
                        TestJsonSerializerSettings,
                        default(Links),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Links>(x),
                    x => new JsonObjectDeserializeUnitTest<Links>(x),
                    new JsonObjectSerializationUnitTestData(
                        "With0Links",
                        TestJsonSerializerSettings,
                        new Links(),
                        "{}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Links>(x),
                    x => new JsonObjectDeserializeUnitTest<Links>(x),
                    new JsonObjectSerializationUnitTestData(
                        "With1Links",
                        TestJsonSerializerSettings,
                        new Links(new Dictionary<string, Link>
                        {
                            {Keywords.Self, new Link("https://api.example.com/articles/42")}
                        }),
                        "{\"self\":\"https://api.example.com/articles/42\"}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Links>(x),
                    x => new JsonObjectDeserializeUnitTest<Links>(x),
                    new JsonObjectSerializationUnitTestData(
                        "With2Links",
                        TestJsonSerializerSettings,
                        new Links(new Dictionary<string, Link>
                        {
                            {Keywords.Up, new Link("https://api.example.com/articles")},
                            {Keywords.Self, new Link("https://api.example.com/articles/42")}
                        }),
                        "{\"up\":\"https://api.example.com/articles\",\"self\":\"https://api.example.com/articles/42\"}"))
            },
        };
        #endregion
    }
}
