// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.TestAsserts.Internal.Dom;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomReadOnlyResourceTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyResourceTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestDomReadOnlyResourceCreateWithArticleResource()
        {
            // Arrange
            var apiResource = ApiSampleData.ArticleResource;
            var clrResource = SampleArticles.Article;

            // Act
            var actual = DomReadOnlyResource.Create(apiResource, clrResource);

            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyResourceAssert.Equal(ApiSampleData.ArticleResource, actual);
            DomReadOnlyResourceAssert.Equal(SampleArticles.Article, actual);
        }

        [Fact]
        public void TestDomReadOnlyResourceCreateWithBlogResource()
        {
            // Arrange
            var apiResource = ApiSampleData.BlogResource;
            var clrResource = SampleBlogs.Blog;

            // Act
            var actual = DomReadOnlyResource.Create(apiResource, clrResource);

            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyResourceAssert.Equal(ApiSampleData.BlogResource, actual);
            DomReadOnlyResourceAssert.Equal(SampleBlogs.Blog, actual);
        }

        [Fact]
        public void TestDomReadOnlyResourceCreateWithCommentResource()
        {
            // Arrange
            var apiResource = ApiSampleData.CommentResource;
            var clrResource = SampleComments.Comment;

            // Act
            var actual = DomReadOnlyResource.Create(apiResource, clrResource);

            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyResourceAssert.Equal(ApiSampleData.CommentResource, actual);
            DomReadOnlyResourceAssert.Equal(SampleComments.Comment, actual);
        }

        [Fact]
        public void TestDomReadOnlyResourceCreateWithPersonResource()
        {
            // Arrange
            var apiResource = ApiSampleData.PersonResource;
            var clrResource = SamplePersons.Person;

            // Act
            var actual = DomReadOnlyResource.Create(apiResource, clrResource);

            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyResourceAssert.Equal(ApiSampleData.PersonResource, actual);
            DomReadOnlyResourceAssert.Equal(SamplePersons.Person, actual);
        }
        #endregion
    }
}
