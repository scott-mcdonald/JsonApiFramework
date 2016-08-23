// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.Internal.Dom;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomReadWriteRelationshipsTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadWriteRelationshipsTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadWriteRelationshipsTestData")]
        internal void TestDomReadWriteRelationshipsCreate(string name, Relationships expected, DomReadWriteRelationships actual)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadWriteRelationshipsAssert.Equal(expected, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadWriteRelationshipsTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyObject",
                        new Relationships(),
                        DomReadWriteRelationships.Create()
                    },
                new object[]
                    {
                        "WithOneRelationship",
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
                        "WithManyRelationships",
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
                        "WithOneToOneRelationship",
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
                        "WithManyToOneRelationships",
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
                        "WithOneToManyRelationship",
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
                        "WithManyToManyRelationships",
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
