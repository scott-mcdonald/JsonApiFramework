// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.Internal.Dom;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;

using Newtonsoft.Json.Linq;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomReadWriteRelationshipTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadWriteRelationshipTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadWriteRelationshipTestData")]
        internal void TestDomReadWriteRelationshipCreate(string name, string expectedRel, Relationship expectedRelationship, DomReadWriteRelationship actual)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadWriteRelationshipAssert.Equal(expectedRel, expectedRelationship, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadWriteRelationshipTestData = new[]
            {
                new object[]
                    {
                        "RelationshipWithEmptyObject",
                        ApiSampleData.ArticleToAuthorRel,
                        Relationship.Empty,
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel)
                    },
                new object[]
                    {
                        "RelationshipWithLinks",
                        ApiSampleData.ArticleToAuthorRel,
                        new Relationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    }
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))))
                    },
                new object[]
                    {
                        "RelationshipWithLinksAndMeta",
                        ApiSampleData.ArticleToAuthorRel,
                        new Relationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Meta = ApiSampleData.RelationshipMeta
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomReadOnlyMeta.Create(ApiSampleData.RelationshipMeta))
                    },
                new object[]
                    {
                        "ToOneRelationshipWithEmptyLinkage",
                        ApiSampleData.ArticleToAuthorRel,
                        ToOneRelationship.Empty,
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            DomData.Create())
                    },
                new object[]
                    {
                        "ToOneRelationshipWithEmptyLinkageAndLinks",
                        ApiSampleData.ArticleToAuthorRel,
                        new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Data = null
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomData.Create())
                    },
                new object[]
                    {
                        "ToOneRelationshipWithEmptyLinkageAndLinksAndMeta",
                        ApiSampleData.ArticleToAuthorRel,
                        new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Data = null,
                                Meta = ApiSampleData.RelationshipMeta
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomData.Create(),
                            DomReadOnlyMeta.Create(ApiSampleData.RelationshipMeta))
                    },
                new object[]
                    {
                        "ToOneRelationshipWithNonEmptyLinkage",
                        ApiSampleData.ArticleToAuthorRel,
                        new ToOneRelationship
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            DomData.CreateFromResourceIdentifier(
                                DomReadWriteResourceIdentifier.Create(
                                    DomType.CreateFromResourceType(ClrSampleData.PersonResourceType),
                                    DomId.CreateFromClrResource(ClrSampleData.PersonResourceType, SamplePersons.Person))))
                    },
                new object[]
                    {
                        "ToOneRelationshipWithNonEmptyLinkageAndLinks",
                        ApiSampleData.ArticleToAuthorRel,
                        new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomData.CreateFromResourceIdentifier(
                                DomReadWriteResourceIdentifier.Create(
                                    DomType.CreateFromResourceType(ClrSampleData.PersonResourceType),
                                    DomId.CreateFromClrResource(ClrSampleData.PersonResourceType, SamplePersons.Person))))
                    },
                new object[]
                    {
                        "ToOneRelationshipWithNonEmptyLinkageAndLinksAndMeta",
                        ApiSampleData.ArticleToAuthorRel,
                        new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Data = ApiSampleData.PersonResourceIdentifier,
                                Meta = ApiSampleData.RelationshipMeta
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomData.CreateFromResourceIdentifier(
                                DomReadWriteResourceIdentifier.Create(
                                    DomType.CreateFromResourceType(ClrSampleData.PersonResourceType),
                                    DomId.CreateFromClrResource(ClrSampleData.PersonResourceType, SamplePersons.Person))),
                            DomReadOnlyMeta.Create(ApiSampleData.RelationshipMeta))
                    },
                new object[]
                    {
                        "ToManyRelationshipWithEmptyLinkage",
                        ApiSampleData.ArticleToCommentsRel,
                        ToManyRelationship.Empty,
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                            DomDataCollection.Create())
                    },
                new object[]
                    {
                        "ToManyRelationshipWithEmptyLinkageAndLinks",
                        ApiSampleData.ArticleToCommentsRel,
                        new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
                                    },
                                Data = ApiSampleData.EmptyResourceIdentifiers
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef))),
                            DomDataCollection.Create())
                    },
                new object[]
                    {
                        "ToManyRelationshipWithEmptyLinkageAndLinksAndMeta",
                        ApiSampleData.ArticleToCommentsRel,
                        new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
                                    },
                                Data = ApiSampleData.EmptyResourceIdentifiers,
                                Meta = JObject.FromObject(new RelationshipMeta
                                    {
                                        CascadeDelete = true
                                    })
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                            DomReadWriteLinks.Create(
                                DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef))),
                            DomDataCollection.Create(),
                            DomReadOnlyMeta.Create(ApiSampleData.RelationshipMeta))
                    },
                new object[]
                    {
                        "ToManyRelationshipWithNonEmptyLinkage",
                        ApiSampleData.ArticleToCommentsRel,
                        new ToManyRelationship
                            {
                                Data = ApiSampleData.CommentResourceIdentifiers
                            },
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                            DomDataCollection.CreateFromResourceIdentifiers(
                                DomReadWriteResourceIdentifier.Create(
                                    DomType.CreateFromResourceType(ClrSampleData.CommentResourceType),
                                    DomId.CreateFromClrResource(ClrSampleData.CommentResourceType, SampleComments.Comment1)),
                                DomReadWriteResourceIdentifier.Create(
                                    DomType.CreateFromResourceType(ClrSampleData.CommentResourceType),
                                    DomId.CreateFromClrResource(ClrSampleData.CommentResourceType, SampleComments.Comment2))))
                    },
                new object[]
                    {
                        "ToManyRelationshipWithNonEmptyLinkageAndLinks",
                        ApiSampleData.ArticleToCommentsRel,
                        new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
                                    },
                                Data = ApiSampleData.CommentResourceIdentifiers
                            },
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
                                    DomId.CreateFromClrResource(ClrSampleData.CommentResourceType, SampleComments.Comment2))))
                    },
                new object[]
                    {
                        "ToManyRelationshipWithNonEmptyLinkageAndLinksAndMeta",
                        ApiSampleData.ArticleToCommentsRel,
                        new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
                                    },
                                Data = ApiSampleData.CommentResourceIdentifiers,
                                Meta = ApiSampleData.RelationshipMeta
                            },
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
                                    DomId.CreateFromClrResource(ClrSampleData.CommentResourceType, SampleComments.Comment2))),
                            DomReadOnlyMeta.Create(ApiSampleData.RelationshipMeta))
                    }
            };
        #endregion
    }
}
