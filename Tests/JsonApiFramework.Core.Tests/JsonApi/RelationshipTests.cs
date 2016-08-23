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
    public class RelationshipTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public RelationshipTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("RelationshipTestData")]
        public void TestRelationshipToJson(string name, Relationship expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            RelationshipAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("RelationshipTestData")]
        public void TestRelationshipParse(string name, Relationship expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Relationship>(json);

            // Assert
            RelationshipAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ToOneRelationshipTestData")]
        public void TestToOneRelationshipToJson(string name, Relationship expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            RelationshipAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ToOneRelationshipTestData")]
        public void TestToOneRelationshipParse(string name, Relationship expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Relationship>(json);

            // Assert
            RelationshipAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ToManyRelationshipTestData")]
        public void TestToManyRelationshipToJson(string name, Relationship expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            RelationshipAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ToManyRelationshipTestData")]
        public void TestToManyRelationshipParse(string name, Relationship expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Relationship>(json);

            // Assert
            RelationshipAssert.Equal(expected, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> RelationshipTestData = new[]
            {
                new object[] {"WithEmptyObject", Relationship.Empty},
                new object[]
                    {
                        "WithLinks", new Relationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, SampleData.ArticleToAuthorLink}
                                    }
                            }
                    },
                new object[]
                    {
                        "WithLinksAndMeta",
                        new Relationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, SampleData.ArticleToAuthorLink}
                                    },
                                Meta = SampleData.RelationshipMeta
                            }
                    }
            };

        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> ToOneRelationshipTestData = new[]
            {
                new object[] {"WithEmptyLinkage", ToOneRelationship.Empty},
                new object[]
                    {
                        "WithEmptyLinkageAndLinks", new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, SampleData.ArticleToAuthorLink}
                                    },
                                Data = null
                            }
                    },
                new object[]
                    {
                        "WithEmptyLinkageAndLinksAndMeta",
                        new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, SampleData.ArticleToAuthorLink}
                                    },
                                Data = null,
                                Meta = SampleData.RelationshipMeta
                            }
                    },
                new object[]
                    {
                        "WithNonEmptyLinkage",
                        new ToOneRelationship
                            {
                                Data = SampleData.PersonResourceIdentifier
                            }
                    },
                new object[]
                    {
                        "WithNonEmptyLinkageAndLinks",
                        new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, SampleData.ArticleToAuthorLink}
                                    },
                                Data = SampleData.PersonResourceIdentifier
                            }
                    },
                new object[]
                    {
                        "WithNonEmptyLinkageAndLinksAndMeta",
                        new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, SampleData.ArticleToAuthorLink}
                                    },
                                Data = SampleData.PersonResourceIdentifier,
                                Meta = SampleData.RelationshipMeta
                            }
                    }
            };

        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> ToManyRelationshipTestData = new[]
            {
                new object[] {"WithEmptyLinkage", ToManyRelationship.Empty},
                new object[]
                    {
                        "WithEmptyLinkageAndLinks", new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, SampleData.ArticleToCommentsLink}
                                    },
                                Data = SampleData.EmptyResourceIdentifiers
                            }
                    },
                new object[]
                    {
                        "WithEmptyLinkageAndLinksAndMeta", new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, SampleData.ArticleToCommentsLink}
                                    },
                                Data = SampleData.EmptyResourceIdentifiers,
                                Meta = Meta.Create(new RelationshipMeta
                                    {
                                        CascadeDelete = true
                                    })
                            }
                    },
                new object[]
                    {
                        "WithNonEmptyLinkage", new ToManyRelationship
                            {
                                Data = SampleData.CommentResourceIdentifiers
                            }
                    },
                new object[]
                    {
                        "WithNonEmptyLinkageAndLinks", new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, SampleData.ArticleToCommentsLink}
                                    },
                                Data = SampleData.CommentResourceIdentifiers
                            }
                    },
                new object[]
                    {
                        "WithNonEmptyLinkageAndLinksAndMeta", new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, SampleData.ArticleToCommentsLink}
                                    },
                                Data = SampleData.CommentResourceIdentifiers,
                                Meta = SampleData.RelationshipMeta
                            }
                    }
            };
        #endregion
    }
}
