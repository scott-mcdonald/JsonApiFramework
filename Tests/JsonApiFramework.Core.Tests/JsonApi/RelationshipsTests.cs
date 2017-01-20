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
    public class RelationshipsTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipsTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Fact]
        public void TestRelationshipsTryGetRelationshipAndRelationshipExists()
        {
            // Arrange
            var relationships = new Relationships(new Dictionary<string, Relationship>
                {
                    {"author", ArticleToAuthorRelationship},
                    {"comments", ArticleToCommentsRelationship}
                });

            // Act
            var expected = ArticleToCommentsRelationship;
            Relationship actual;
            var actualRelationshipFound = relationships.TryGetRelationship("comments", out actual);

            // Assert
            actualRelationshipFound.Should().BeTrue();
            actual.ShouldBeEquivalentTo(expected);
        }

        [Fact]
        public void TestRelationshipsTryGetRelationshipAndRelationshipNotExists()
        {
            // Arrange
            var relationships = new Relationships(new Dictionary<string, Relationship>
                {
                    {"author", ArticleToAuthorRelationship},
                    {"comments", ArticleToCommentsRelationship}
                });

            // Act
            Relationship actual;
            var actualRelationshipFound = relationships.TryGetRelationship("blogs", out actual);

            // Assert
            actualRelationshipFound.Should().BeFalse();
            actual.Should().BeNull();
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        public const string ArticleToAuthorSelfHRef = "https://api.example.com/articles/42/relationships/author";
        public const string ArticleToAuthorRelatedHRef = "https://api.example.com/articles/42/author";

        public static readonly Link ArticleToAuthorSelfLink = new Link(ArticleToAuthorSelfHRef);
        public static readonly Link ArticleToAuthorRelatedLink = new Link(ArticleToAuthorRelatedHRef);

        public static readonly IReadOnlyDictionary<string, Link> ArticleToAuthorReadOnlyDictionary = new Dictionary<string, Link>
                {
                    {Keywords.Self, ArticleToAuthorSelfLink},
                    {Keywords.Related, ArticleToAuthorRelatedLink}
                };

        public static readonly Links ArticleToAuthorLinks = new Links(ArticleToAuthorReadOnlyDictionary);

        public static readonly ResourceIdentifier ArticleToAuthorResourceIdentifier = new ResourceIdentifier("people", "42");

        public static readonly Relationship ArticleToAuthorRelationship = new ToOneRelationship(ArticleToAuthorLinks, ArticleToAuthorResourceIdentifier);


        public const string ArticleToCommentsSelfHRef = "https://api.example.com/articles/42/relationships/comments";
        public const string ArticleToCommentsRelatedHRef = "https://api.example.com/articles/42/comments";

        public static readonly Link ArticleToCommentsSelfLink = new Link(ArticleToCommentsSelfHRef);
        public static readonly Link ArticleToCommentsRelatedLink = new Link(ArticleToCommentsRelatedHRef);

        public static readonly IReadOnlyDictionary<string, Link> ArticleToCommentsReadOnlyDictionary = new Dictionary<string, Link>
                {
                    {Keywords.Self, ArticleToCommentsSelfLink},
                    {Keywords.Related, ArticleToCommentsRelatedLink}
                };

        public static readonly Links ArticleToCommentsLinks = new Links(ArticleToCommentsReadOnlyDictionary);

        public static readonly ResourceIdentifier ArticleToCommentsResourceIdentifier1 = new ResourceIdentifier("comment", "68");
        public static readonly ResourceIdentifier ArticleToCommentsResourceIdentifier2 = new ResourceIdentifier("comment", "86");
        public static readonly List<ResourceIdentifier> ArticleToCommentsResourceIdentifiers = new List<ResourceIdentifier>
                {
                    ArticleToCommentsResourceIdentifier1,
                    ArticleToCommentsResourceIdentifier2

                };

        public static readonly Relationship ArticleToCommentsRelationship = new ToManyRelationship(ArticleToCommentsLinks, ArticleToCommentsResourceIdentifiers);
        #endregion
    }
}
