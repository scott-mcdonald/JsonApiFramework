// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;

using Newtonsoft.Json.Linq;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomRelationshipTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomRelationshipTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomRelationshipTestData")]
        internal void TestIDomRelationshipAbstraction(string name, string expectedRel, Relationship expectedRelationship, IDomRelationship domRelationship)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.Output.WriteLine("Expected Rel:          {0}", expectedRel);
            this.Output.WriteLine("Expected Relationship: {0}", expectedRelationship);

            this.Output.WriteLine(String.Empty);

            var actualRel = domRelationship.Rel;
            var actualRelationship = domRelationship.Relationship;

            this.Output.WriteLine("Actual Rel:            {0}", actualRel);
            this.Output.WriteLine("Actual Relationship:   {0}", actualRelationship);

            // Assert
            Assert.Equal(expectedRel, actualRel);
            RelationshipAssert.Equal(expectedRelationship, actualRelationship);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomRelationshipTestData = new[]
            {
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndRelationshipWithEmptyObject",
                        ApiSampleData.ArticleToAuthorRel,
                        Relationship.Empty,
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToAuthorRel, Relationship.Empty)
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndRelationshipWithLinks",
                        ApiSampleData.ArticleToAuthorRel,
                        new Relationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    }
                            },
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            new Relationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    }
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndRelationshipWithLinksAndMeta",
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
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            new Relationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToOneRelationshipWithEmptyLinkage",
                        ApiSampleData.ArticleToAuthorRel,
                        ToOneRelationship.Empty,
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToAuthorRel, ToOneRelationship.Empty)
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToOneRelationshipWithEmptyLinkageAndLinks",
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
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Data = null
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToOneRelationshipWithEmptyLinkageAndLinksAndMeta",
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
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Data = null,
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    },

                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToOneRelationshipWithNonEmptyLinkage",
                        ApiSampleData.ArticleToAuthorRel,
                        new ToOneRelationship
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            new ToOneRelationship
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToOneRelationshipWithNonEmptyLinkageAndLinks",
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
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Data = ApiSampleData.PersonResourceIdentifier
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToOneRelationshipWithNonEmptyLinkageAndLinksAndMeta",
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
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            new ToOneRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink},
                                        {Keywords.Related, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Data = ApiSampleData.PersonResourceIdentifier,
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToManyRelationshipWithEmptyLinkage",
                        ApiSampleData.ArticleToCommentsRel,
                        ToManyRelationship.Empty,
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToCommentsRel, ToManyRelationship.Empty)
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToManyRelationshipWithEmptyLinkageAndLinks",
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
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                            new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
                                    },
                                Data = ApiSampleData.EmptyResourceIdentifiers
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToManyRelationshipWithEmptyLinkageAndLinksAndMeta",
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
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToCommentsRel,
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
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToManyRelationshipWithNonEmptyLinkage",
                        ApiSampleData.ArticleToCommentsRel,
                        new ToManyRelationship
                            {
                                Data = ApiSampleData.CommentResourceIdentifiers
                            },
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                            new ToManyRelationship
                            {
                                Data = ApiSampleData.CommentResourceIdentifiers
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToManyRelationshipWithNonEmptyLinkageAndLinks",
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
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                            new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
                                    },
                                Data = ApiSampleData.CommentResourceIdentifiers
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyRelationshipAndToManyRelationshipWithNonEmptyLinkageAndLinksAndMeta",
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
                        DomReadOnlyRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                            new ToManyRelationship
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink},
                                        {Keywords.Related, ApiSampleData.ArticleToCommentsLink}
                                    },
                                Data = ApiSampleData.CommentResourceIdentifiers,
                                Meta = ApiSampleData.RelationshipMeta
                            })
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndRelationshipWithEmptyObject",
                        ApiSampleData.ArticleToAuthorRel,
                        Relationship.Empty,
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel)
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndRelationshipWithLinks",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndRelationshipWithLinksAndMeta",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomReadOnlyMeta.Create(ApiSampleData.RelationshipMeta))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndToOneRelationshipWithEmptyLinkage",
                        ApiSampleData.ArticleToAuthorRel,
                        ToOneRelationship.Empty,
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToAuthorRel,
                            DomData.Create())
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndToOneRelationshipWithEmptyLinkageAndLinks",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomData.Create())
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndToOneRelationshipWithEmptyLinkageAndLinksAndMeta",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomData.Create(),
                            DomReadOnlyMeta.Create(ApiSampleData.RelationshipMeta))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndToOneRelationshipWithNonEmptyLinkage",
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
                        "WithDomReadWriteRelationshipAndToOneRelationshipWithNonEmptyLinkageAndLinks",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomData.CreateFromResourceIdentifier(
                                DomReadWriteResourceIdentifier.Create(
                                    DomType.CreateFromResourceType(ClrSampleData.PersonResourceType),
                                    DomId.CreateFromClrResource(ClrSampleData.PersonResourceType, SamplePersons.Person))))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndToOneRelationshipWithNonEmptyLinkageAndLinksAndMeta",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToAuthorHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToAuthorHRef))),
                            DomData.CreateFromResourceIdentifier(
                                DomReadWriteResourceIdentifier.Create(
                                    DomType.CreateFromResourceType(ClrSampleData.PersonResourceType),
                                    DomId.CreateFromClrResource(ClrSampleData.PersonResourceType, SamplePersons.Person))),
                            DomReadOnlyMeta.Create(ApiSampleData.RelationshipMeta))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndToManyRelationshipWithEmptyLinkage",
                        ApiSampleData.ArticleToCommentsRel,
                        ToManyRelationship.Empty,
                        DomReadWriteRelationship.Create(ApiSampleData.ArticleToCommentsRel,
                            DomDataCollection.Create())
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndToManyRelationshipWithEmptyLinkageAndLinks",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef))),
                            DomDataCollection.Create())
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndToManyRelationshipWithEmptyLinkageAndLinksAndMeta",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
                                DomReadWriteLink.Create(Keywords.Related, DomHRef.Create(ApiSampleData.ArticleToCommentsHRef))),
                            DomDataCollection.Create(),
                            DomReadOnlyMeta.Create(ApiSampleData.RelationshipMeta))
                    },
                new object[]
                    {
                        "WithDomReadWriteRelationshipAndToManyRelationshipWithNonEmptyLinkage",
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
                        "WithDomReadWriteRelationshipAndToManyRelationshipWithNonEmptyLinkageAndLinks",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
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
                        "WithDomReadWriteRelationshipAndToManyRelationshipWithNonEmptyLinkageAndLinksAndMeta",
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
                                DomReadWriteLink.Create(Keywords.Self,
                                    DomHRef.Create(ApiSampleData.ArticleToRelationshipsToCommentsHRef)),
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
