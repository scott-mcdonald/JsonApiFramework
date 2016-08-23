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
                    { SampleData.ArticleToAuthorRel, SampleData.ArticleToAuthorToOneRelationship },
                    { SampleData.ArticleToCommentsRel, SampleData.ArticleToCommentsToManyRelationship }
                };
            var expected = SampleData.ArticleToCommentsToManyRelationship;

            // Act
            var actual = relationships.GetRelationship(SampleData.ArticleToCommentsRel);

            // Assert
            RelationshipAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestRelationshipsGetRelationshipWithRelationshipThatDoesNotExists()
        {
            // Arrange
            var relationships = new Relationships
                {
                    { SampleData.ArticleToAuthorRel, SampleData.ArticleToAuthorToOneRelationship },
                    { SampleData.ArticleToCommentsRel, SampleData.ArticleToCommentsToManyRelationship }
                };

            // Act

            // Assert
            Assert.Throws<RelationshipNotFoundException>(() => relationships.GetRelationship(SampleData.ArticleToBlogRel));
        }

        [Fact]
        public void TestRelationshipsTryGetRelationshipWithRelationshipThatExists()
        {
            // Arrange
            var relationships = new Relationships
                {
                    { SampleData.ArticleToAuthorRel, SampleData.ArticleToAuthorToOneRelationship },
                    { SampleData.ArticleToCommentsRel, SampleData.ArticleToCommentsToManyRelationship }
                };
            var expected = SampleData.ArticleToCommentsToManyRelationship;

            // Act
            Relationship actual;
            var foundRelationship = relationships.TryGetRelationship(SampleData.ArticleToCommentsRel, out actual);

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
                    { SampleData.ArticleToAuthorRel, SampleData.ArticleToAuthorToOneRelationship },
                    { SampleData.ArticleToCommentsRel, SampleData.ArticleToCommentsToManyRelationship }
                };

            // Act
            Relationship relationship;
            var foundRelationship = relationships.TryGetRelationship(SampleData.ArticleToBlogRel, out relationship);

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
                                {SampleData.ArticleToAuthorRel, SampleData.ArticleToAuthorRelationship}
                            }
                    },
                new object[]
                    {
                        "WithManyRelationships",
                        new Relationships
                            {
                                {SampleData.ArticleToAuthorRel, SampleData.ArticleToAuthorRelationship},
                                {SampleData.ArticleToCommentsRel, SampleData.ArticleToCommentsRelationship}
                            }
                    },
                new object[]
                    {
                        "WithOneToOneRelationship",
                        new Relationships
                            {
                                {SampleData.ArticleToAuthorRel, SampleData.ArticleToAuthorToOneRelationship}
                            }
                    },
                new object[]
                    {
                        "WithManyToOneRelationships",
                        new Relationships
                            {
                                {SampleData.ArticleToAuthorRel, SampleData.ArticleToAuthorToOneRelationship},
                                {SampleData.ArticleToBlogRel, SampleData.ArticleToBlogToOneRelationship}
                            }
                    },
                new object[]
                    {
                        "WithOneToManyRelationship",
                        new Relationships
                            {
                                {SampleData.ArticleToCommentsRel, SampleData.ArticleToCommentsToManyRelationship}
                            }
                    },
                new object[]
                    {
                        "WithManyToManyRelationships",
                        new Relationships
                            {
                                {SampleData.ArticleToCommentsRel, SampleData.ArticleToCommentsToManyRelationship},
                                {SampleData.BlogToArticlesRel, SampleData.BlogToArticlesToManyRelationship}
                            }
                    }
            };
        #endregion
    }
}
