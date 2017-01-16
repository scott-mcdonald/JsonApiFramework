// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.XUnit;

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
        [Fact]
        public void TestLinksTryGetLinkWithLinkThatExists()
        {
            // Arrange
            var links = new Links(
                new Dictionary<string, Link>
                {
                    {Keywords.Up, JsonApiSampleData.ArticleCollectionLink},
                    {Keywords.Self, JsonApiSampleData.ArticleLink}
                });

            // Act
            var expected = JsonApiSampleData.ArticleLink;
            Link actual;
            var actualLinkFound = links.TryGetLink(Keywords.Self, out actual);

            // Assert
            actualLinkFound.Should().BeTrue();
            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void TestLinksTryGetLinkWithLinkThatDoesNotExists()
        {
            // Arrange
            var links = new Links(
                new Dictionary<string, Link>
                    {
                        {Keywords.Up, JsonApiSampleData.ArticleCollectionLink},
                        {Keywords.Self, JsonApiSampleData.ArticleLink}
                    });

            // Act
            Link actual;
            var actualLinkFound = links.TryGetLink(Keywords.Next, out actual);

            // Assert
            actualLinkFound.Should().BeFalse();
            actual.Should().BeNull();
        }
        #endregion
    }
}
