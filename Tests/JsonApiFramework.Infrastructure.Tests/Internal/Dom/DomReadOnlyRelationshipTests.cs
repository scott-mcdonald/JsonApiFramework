// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.Internal.Dom;
using JsonApiFramework.TestData.ApiResources;

using Newtonsoft.Json.Linq;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomReadOnlyRelationshipTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyRelationshipTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadOnlyRelationshipTestData")]
        public void TestDomReadOnlyRelationshipCreate(string name, string expectedRel, Relationship expectedRelationship)
        {
            // Arrange

            // Act
            var actual = DomReadOnlyRelationship.Create(expectedRel, expectedRelationship);

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyRelationshipAssert.Equal(expectedRel, expectedRelationship, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadOnlyRelationshipTestData = new[]
            {
                new object[] {"RelationshipWithEmptyObject", ApiSampleData.ArticleToAuthorRel, Relationship.Empty},
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
                            }
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
                            }
                    },
                new object[] {"ToOneRelationshipWithEmptyLinkage", ApiSampleData.ArticleToAuthorRel, ToOneRelationship.Empty},
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
                            }
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
                            }
                    },
                new object[]
                    {
                        "ToOneRelationshipWithNonEmptyLinkage",
                        ApiSampleData.ArticleToAuthorRel,
                        new ToOneRelationship
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            }
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
                            }
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
                            }
                    },
                new object[] {"ToManyRelationshipWithEmptyLinkage", ApiSampleData.ArticleToCommentsRel, ToManyRelationship.Empty},
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
                            }
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
                            }
                    },
                new object[]
                    {
                        "ToManyRelationshipWithNonEmptyLinkage",
                        ApiSampleData.ArticleToCommentsRel,
                        new ToManyRelationship
                            {
                                Data = ApiSampleData.CommentResourceIdentifiers
                            }
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
                            }
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
                            }
                    }
            };
        #endregion
    }
}
