// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomRelationshipsTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomRelationshipsTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomRelationshipsTestData")]
        internal void TestIDomRelationshipsAbstraction(string name, Relationships expectedRelationships, IDomRelationships domRelationships)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            foreach (var expectedRelAndRelationship in expectedRelationships)
            {
                var expectedRel = expectedRelAndRelationship.Key;
                var expectedRelationship = expectedRelAndRelationship.Value;

                this.Output.WriteLine("Expected Rel:          {0}", expectedRel);
                this.Output.WriteLine("Expected Relationship: {0}", expectedRelationship);
            }

            this.Output.WriteLine(String.Empty);

            var actualRelationships = domRelationships.Relationships;

            foreach (var actualRelAndRelationship in actualRelationships)
            {
                var actualRel = actualRelAndRelationship.Key;
                var actualRelationship = actualRelAndRelationship.Value;

                this.Output.WriteLine("Actual Rel:            {0}", actualRel);
                this.Output.WriteLine("Actual Relationship:   {0}", actualRelationship);
            }

            // Assert
            RelationshipsAssert.Equal(expectedRelationships, actualRelationships);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomRelationshipsTestData = new[]
            {
                new object[]
                    {
                        "WithDomReadOnlyRelationshipsAndEmptyObject",
                        new Relationships(),
                        DomReadOnlyRelationships.Create(new Relationships())
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipsAndOneRelationship",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship}
                            },
                        DomReadOnlyRelationships.Create(
                            new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship}
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipsAndManyRelationships",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship},
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship}
                            },
                        DomReadOnlyRelationships.Create(
                            new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship},
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship}
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipsAndOneToOneRelationship",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship}
                            },
                        DomReadOnlyRelationships.Create(
                            new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship}
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipsAndManyToOneRelationships",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship},
                                {ApiSampleData.ArticleToBlogRel, ApiSampleData.ArticleToBlogToOneRelationship}
                            },
                        DomReadOnlyRelationships.Create(
                            new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship},
                                {ApiSampleData.ArticleToBlogRel, ApiSampleData.ArticleToBlogToOneRelationship}
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipsAndOneToManyRelationship",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship}
                            },
                        DomReadOnlyRelationships.Create(
                            new Relationships
                            {
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship}
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipsAndManyToManyRelationships",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship},
                                {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesToManyRelationship}
                            },
                        DomReadOnlyRelationships.Create(
                            new Relationships
                            {
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship},
                                {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesToManyRelationship}
                            })
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipsAndEmptyObject",
                        new Relationships(),
                        DomReadWriteRelationships.Create()
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipsAndOneRelationship",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship}
                            },
                        DomReadWriteRelationships.Create(
                            DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                                DomReadWriteLinks.Create(
                                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                    DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef)))))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipsAndManyRelationships",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship},
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship}
                            },
                        DomReadWriteRelationships.Create(
                            DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                                DomReadWriteLinks.Create(
                                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                    DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef)))),
                            DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                                DomReadWriteLinks.Create(
                                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                                    DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef)))))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipsAndOneToOneRelationship",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship}
                            },
                        DomReadWriteRelationships.Create(
                            DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                                DomReadWriteLinks.Create(
                                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                    DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                                DomData.CreateFromResourceIdentifier(
                                    DomReadWriteResourceIdentifier.Create(
                                        DomType.CreateFromResourceType(ClrSampleData.PersonResourceType),
                                        DomId.CreateFromClrResource(ClrSampleData.PersonResourceType, SamplePersons.Person)))))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipsAndManyToOneRelationships",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship},
                                {ApiSampleData.ArticleToBlogRel, ApiSampleData.ArticleToBlogToOneRelationship}
                            },
                        DomReadWriteRelationships.Create(
                            DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                                DomReadWriteLinks.Create(
                                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                    DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                                DomData.CreateFromResourceIdentifier(
                                    DomReadWriteResourceIdentifier.Create(
                                        DomType.CreateFromResourceType(ClrSampleData.PersonResourceType),
                                        DomId.CreateFromClrResource(ClrSampleData.PersonResourceType, SamplePersons.Person)))),
                            DomReadWriteRelationship.Create(ApiSampleData.ArticleToBlogRel,
                                DomReadWriteLinks.Create(
                                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToBlogHRef)),
                                    DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToBlogHRef))),
                                DomData.CreateFromResourceIdentifier(
                                    DomReadWriteResourceIdentifier.Create(
                                        DomType.CreateFromResourceType(ClrSampleData.BlogResourceType),
                                        DomId.CreateFromClrResource(ClrSampleData.BlogResourceType, SampleBlogs.Blog)))))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipsAndOneToManyRelationship",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship}
                            },
                        DomReadWriteRelationships.Create(
                            DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                                DomReadWriteLinks.Create(
                                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                                    DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef))),
                                DomDataCollection.CreateFromResourceIdentifiers(
                                    DomReadWriteResourceIdentifier.Create(
                                        DomType.CreateFromResourceType(ClrSampleData.CommentResourceType),
                                        DomId.CreateFromClrResource(ClrSampleData.CommentResourceType, SampleComments.Comment1)),
                                    DomReadWriteResourceIdentifier.Create(
                                        DomType.CreateFromResourceType(ClrSampleData.CommentResourceType),
                                        DomId.CreateFromClrResource(ClrSampleData.CommentResourceType, SampleComments.Comment2)))))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipsAndManyToManyRelationships",
                        new Relationships
                            {
                                {ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship},
                                {ApiSampleData.BlogToArticlesRel, ApiSampleData.BlogToArticlesToManyRelationship}
                            },
                        DomReadWriteRelationships.Create(
                            DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                                DomReadWriteLinks.Create(
                                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                                    DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef))),
                                DomDataCollection.CreateFromResourceIdentifiers(
                                    DomReadWriteResourceIdentifier.Create(
                                        DomType.CreateFromResourceType(ClrSampleData.CommentResourceType),
                                        DomId.CreateFromClrResource(ClrSampleData.CommentResourceType, SampleComments.Comment1)),
                                    DomReadWriteResourceIdentifier.Create(
                                        DomType.CreateFromResourceType(ClrSampleData.CommentResourceType),
                                        DomId.CreateFromClrResource(ClrSampleData.CommentResourceType, SampleComments.Comment2)))),
                            DomReadWriteRelationship.Create(ApiSampleData.BlogToArticlesRel,
                                DomReadWriteLinks.Create(
                                    DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.BlogToRelationshipsToArticlesHRef)),
                                    DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.BlogToArticlesHRef))),
                                DomDataCollection.CreateFromResourceIdentifiers(
                                    DomReadWriteResourceIdentifier.Create(
                                        DomType.CreateFromResourceType(ClrSampleData.ArticleResourceType),
                                        DomId.CreateFromClrResource(ClrSampleData.ArticleResourceType, SampleArticles.Article1)),
                                    DomReadWriteResourceIdentifier.Create(
                                        DomType.CreateFromResourceType(ClrSampleData.ArticleResourceType),
                                        DomId.CreateFromClrResource(ClrSampleData.ArticleResourceType, SampleArticles.Article2)))))
                    }
            };
        #endregion
    }
}
