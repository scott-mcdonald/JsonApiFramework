// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.Internal.Dom;
using JsonApiFramework.TestData.ApiResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomReadOnlyRelationshipsTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyRelationshipsTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadOnlyRelationshipsTestData")]
        public void TestDomReadOnlyRelationshipsCreate(string name, Relationships expected)
        {
            // Arrange

            // Act
            var actual = DomReadOnlyRelationships.Create(expected);

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyRelationshipsAssert.Equal(expected, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadOnlyRelationshipsTestData = new[]
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
