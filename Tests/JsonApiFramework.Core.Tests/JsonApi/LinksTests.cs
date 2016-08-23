// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class LinksTests : XUnitTest
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
        [MemberData("LinksTestData")]
        public void TestLinksToJson(string name, Links expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            LinksAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("LinksTestData")]
        public void TestLinksParse(string name, Links expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Links>(json);

            // Assert
            LinksAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestLinksGetHRefWithLinkThatExists()
        {
            // Arrange
            var links = new Links
                {
                    { Keywords.Up, SampleData.ArticleCollectionLink },
                    { Keywords.Self, SampleData.ArticleLink }
                };

            // Act
            var expected = SampleData.ArticleHRef;
            var actual = links.GetHRef(Keywords.Self);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestLinksGetHRefWithLinkThatDoesNotExists()
        {
            // Arrange
            var links = new Links
                {
                    { Keywords.Up, SampleData.ArticleCollectionLink },
                    { Keywords.Self, SampleData.ArticleLink }
                };

            // Act

            // Assert
            Assert.Throws<LinkNotFoundException>(() => links.GetHRef(Keywords.Next));
        }

        [Fact]
        public void TestLinksTryGetHRefWithLinkThatExists()
        {
            // Arrange
            var links = new Links
                {
                    { Keywords.Up, SampleData.ArticleCollectionLink },
                    { Keywords.Self, SampleData.ArticleLink }
                };

            // Act
            var expected = SampleData.ArticleHRef;
            string actual;
            var foundHRef = links.TryGetHRef(Keywords.Self, out actual);

            // Assert
            Assert.True(foundHRef);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestLinksTryGetHRefWithLinkThatDoesNotExists()
        {
            // Arrange
            var links = new Links
                {
                    { Keywords.Up, SampleData.ArticleCollectionLink },
                    { Keywords.Self, SampleData.ArticleLink }
                };

            // Act
            string actual;
            var foundHRef = links.TryGetHRef(Keywords.Next, out actual);

            // Assert
            Assert.False(foundHRef);
            Assert.Null(actual);
        }


        [Fact]
        public void TestLinksGetLinkWithLinkThatExists()
        {
            // Arrange
            var links = new Links
                {
                    { Keywords.Up, SampleData.ArticleCollectionLink },
                    { Keywords.Self, SampleData.ArticleLink }
                };

            // Act
            var expected = SampleData.ArticleLink;
            var actual = links.GetLink(Keywords.Self);

            // Assert
            LinkAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestLinksGetLinkWithLinkThatDoesNotExists()
        {
            // Arrange
            var links = new Links
                {
                    { Keywords.Up, SampleData.ArticleCollectionLink },
                    { Keywords.Self, SampleData.ArticleLink }
                };

            // Act

            // Assert
            Assert.Throws<LinkNotFoundException>(() => links.GetLink(Keywords.Next));
        }

        [Fact]
        public void TestLinksTryGetLinkWithLinkThatExists()
        {
            // Arrange
            var links = new Links
                {
                    { Keywords.Up, SampleData.ArticleCollectionLink },
                    { Keywords.Self, SampleData.ArticleLink }
                };

            // Act
            var expected = SampleData.ArticleLink;
            Link actual;
            var foundLink = links.TryGetLink(Keywords.Self, out actual);

            // Assert
            Assert.True(foundLink);
            LinkAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestLinksTryGetLinkWithLinkThatDoesNotExists()
        {
            // Arrange
            var links = new Links
                {
                    { Keywords.Up, SampleData.ArticleCollectionLink },
                    { Keywords.Self, SampleData.ArticleLink }
                };

            // Act
            Link actual;
            var foundLink = links.TryGetLink(Keywords.Next, out actual);

            // Assert
            Assert.False(foundLink);
            Assert.Null(actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> LinksTestData = new[]
            {
                new object[] {"WithEmptyObject", new Links()},
                new object[]
                    {
                        "WithOneHRefOnlyLink",
                        new Links
                            {
                                {Keywords.Self, SampleData.ArticleLink}
                            }
                    },
                new object[]
                    {
                        "WithOneHRefAndMetaLink",
                        new Links
                            {
                                {Keywords.Self, SampleData.ArticleLinkWithMeta}
                            }
                    },
                new object[]
                    {
                        "WithManyHRefOnlyLinks",
                        new Links
                            {
                                {Keywords.Up, SampleData.ArticleCollectionLink},
                                {Keywords.Self, SampleData.ArticleLink}
                            }
                    },
                new object[]
                    {
                        "WithManyHRefAndMetaLinks",
                        new Links
                            {
                                {Keywords.Up, SampleData.ArticleCollectionLinkWithMeta},
                                {Keywords.Self, SampleData.ArticleLinkWithMeta}
                            }
                    }
            };
        #endregion
    }
}
