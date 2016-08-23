// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.XUnit;

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
        [MemberData("LinkTestData")]
        public void TestLinkToJson(string name, Link expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            LinkAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("LinkTestData")]
        public void TestLinkParse(string name, Link expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Link>(json);

            // Assert
            LinkAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestLinkConversionOperatorToLinkFromString()
        {
            // Arrange
            var json = SampleData.ArticleCollectionHRef;
            var expected = new Link(SampleData.ArticleCollectionHRef);

            // Act
            this.Output.WriteLine(json);
            var actual = (Link)json;

            // Assert
            LinkAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestLinkConversionOperatorToStringFromLink()
        {
            // Arrange
            var expectedLink = new Link(SampleData.ArticleCollectionHRef);

            // Act
            var expected = SampleData.ArticleCollectionHRef;

            var actual = (string)expectedLink;
            this.Output.WriteLine(actual);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestLinkUriPropertyWithNullHRef()
        {
            // Arrange
            var expected = new Link();

            // Act
            var uri = expected.Uri;

            // Assert
            Assert.Null(uri);
        }

        [Fact]
        public void TestLinkUriPropertyWithAbsoluteHRef()
        {
            // Arrange
            const string expectedHRef = "http://api.example.com/articles/1234";
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
            this.Output.WriteLine(actualUri.ToString());

            var actualScheme = actualUri.Scheme;
            var actualHost = actualUri.Host;
            var actualAbsolutePath = actualUri.AbsolutePath;

            // Assert
            Assert.NotNull(actualUri);
            Assert.True(actualUri.IsAbsoluteUri);
            Assert.Equal(expectedScheme, actualScheme);
            Assert.Equal(expectedHost, actualHost);
            Assert.Equal(expectedAbsolutePath, actualAbsolutePath);
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
            this.Output.WriteLine(actualUri.ToString());

            var actualHRef = actualUri.OriginalString;

            // Assert
            Assert.NotNull(actualUri);
            Assert.False(actualUri.IsAbsoluteUri);
            Assert.Equal(expectedHRef, actualHRef);
        }

        [Fact]
        public void TestLinkPathSegmentsPropertyWithNullHRef()
        {
            // Arrange
            var expected = new Link();

            // Act
            var pathSegments = expected.PathSegments
                                       .ToList();
            this.Output.WriteLine(pathSegments.Count > 0
                ? pathSegments.Aggregate((current, next) => current + " " + next)
                : String.Empty);

            // Assert
            Assert.NotNull(pathSegments);
            Assert.False(pathSegments.Any());
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
                    HRef = "http://api.example.com/" + expectedPathSegmentsAtIndex0 + "/" + expectedPathSegmentsAtIndex1
                };

            // Act
            var actual = expected.PathSegments
                                 .ToList();
            this.Output.WriteLine(actual.Count > 0
                ? actual.Aggregate((current, next) => current + " " + next)
                : String.Empty);

            // Assert
            Assert.NotNull(actual);

            var actualPathSegmentsCount = actual.Count;
            var actualPathSegmentsAtIndex0 = actual[0];
            var actualPathSegmentsAtIndex1 = actual[1];

            Assert.Equal(expectedPathSegmentsCount, actualPathSegmentsCount);
            Assert.Equal(expectedPathSegmentsAtIndex0, actualPathSegmentsAtIndex0);
            Assert.Equal(expectedPathSegmentsAtIndex1, actualPathSegmentsAtIndex1);
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
            this.Output.WriteLine(actual.Count > 0
                ? actual.Aggregate((current, next) => current + " " + next)
                : String.Empty);

            // Assert
            Assert.NotNull(actual);

            var actualPathSegmentsCount = actual.Count;
            var actualPathSegmentsAtIndex0 = actual[0];
            var actualPathSegmentsAtIndex1 = actual[1];

            Assert.Equal(expectedPathSegmentsCount, actualPathSegmentsCount);
            Assert.Equal(expectedPathSegmentsAtIndex0, actualPathSegmentsAtIndex0);
            Assert.Equal(expectedPathSegmentsAtIndex1, actualPathSegmentsAtIndex1);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> LinkTestData = new[]
            {
                new object[] {"WithEmptyObject", new Link()},
                new object[] {"WithHRef", new Link(SampleData.ArticleCollectionHRef)},
                new object[]
                    {
                        "WithHRefAndMeta",
                        new Link
                            {
                                HRef = SampleData.ArticleCollectionHRef,
                                Meta = SampleData.LinkMeta
                            }
                    }
            };
        #endregion
    }
}
