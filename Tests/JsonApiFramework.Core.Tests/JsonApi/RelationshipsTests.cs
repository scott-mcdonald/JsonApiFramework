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
    public class RelationshipsTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipsTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("RelationshipsTestData")]
        public void TestRelationshipsToJson(string name, Relationships expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            RelationshipsAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("RelationshipsTestData")]
        public void TestRelationshipsParse(string name, Relationships expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Relationships>(json);

            // Assert
            RelationshipsAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestRelationshipsGetRelationshipWithRelationshipThatExists()
        {
            // Arrange
            var relationships = new Relationships
                {
                    { ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship },
                    { ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship }
                };
            var expected = ApiSampleData.ArticleToCommentsToManyRelationship;

            // Act
            var actual = relationships.GetRelationship(ApiSampleData.ArticleToCommentsRel);

            // Assert
            RelationshipAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestRelationshipsGetRelationshipWithRelationshipThatDoesNotExists()
        {
            // Arrange
            var relationships = new Relationships
                {
                    { ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship },
                    { ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship }
                };

            // Act

            // Assert
            Assert.Throws<RelationshipNotFoundException>(() => relationships.GetRelationship(ApiSampleData.ArticleToBlogRel));
        }

        [Fact]
        public void TestRelationshipsTryGetRelationshipWithRelationshipThatExists()
        {
            // Arrange
            var relationships = new Relationships
                {
                    { ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship },
                    { ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship }
                };
            var expected = ApiSampleData.ArticleToCommentsToManyRelationship;

            // Act
            Relationship actual;
            var foundRelationship = relationships.TryGetRelationship(ApiSampleData.ArticleToCommentsRel, out actual);

            // Assert
            Assert.True(foundRelationship);
            RelationshipAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestRelationshipsTryGetRelationshipWithRelationshipThatDoesNotExists()
        {
            // Arrange
            var relationships = new Relationships
                {
                    { ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship },
                    { ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship }
                };

            // Act
            Relationship relationship;
            var foundRelationship = relationships.TryGetRelationship(ApiSampleData.ArticleToBlogRel, out relationship);

            // Assert
            Assert.False(foundRelationship);
            Assert.Null(relationship);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> RelationshipsTestData = new[]
            {
                new object[] {"WithEmptyObject", new Relationships()},
                new object[]
                    {
                        "WithOneRelationship",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship}
                            }
                    },
                new object[]
                    {
                        "WithManyRelationships",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship},
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship}
                            }
                    },
                new object[]
                    {
                        "WithOneToOneRelationship",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship}
                            }
                    },
                new object[]
                    {
                        "WithManyToOneRelationships",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship},
                                {ApiSampleData.ArticleToBlogRel, ApiSampleData.ArticleToBlogToOneRelationship}
                            }
                    },
                new object[]
                    {
                        "WithOneToManyRelationship",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship}
                            }
                    },
                new object[]
                    {
                        "WithManyToManyRelationships",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship},
                                {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesToManyRelationship}
                            }
                    }
            };
        #endregion
    }
}
