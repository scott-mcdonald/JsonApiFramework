// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomLinksTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomLinksTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomLinksTestData")]
        internal void TestIDomLinksAbstraction(string name, Links expectedLinks, IDomLinks domLinks)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            foreach (var expectedRelAndLink in expectedLinks)
            {
                var expectedRel = expectedRelAndLink.Key;
                var expectedLink = expectedRelAndLink.Value;

                this.Output.WriteLine("Expected Rel:  {0}", expectedRel);
                this.Output.WriteLine("Expected Link: {0}", expectedLink);
            }

            this.Output.WriteLine(String.Empty);

            var actualLinks = domLinks.Links;

            foreach (var actualRelAndLink in actualLinks)
            {
                var actualRel = actualRelAndLink.Key;
                var actualLink = actualRelAndLink.Value;

                this.Output.WriteLine("Actual Rel:    {0}", actualRel);
                this.Output.WriteLine("Actual Link:   {0}", actualLink);
            }

            // Assert
            LinksAssert.Equal(expectedLinks, actualLinks);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomLinksTestData = new[]
            {
                new object[]
                    {
                        "WithDomReadOnlyLinksAndEmptyObject",
                        new Links(),
                        DomReadOnlyLinks.Create(new Links())
                    },
                new object[]
                    {
                        "WithDomReadOnlyLinksAndOneHRefOnlyLink",
                        new Links
                            {
                                {Keywords.Self, ApiSampleData.ArticleLink}
                            },
                        DomReadOnlyLinks.Create(new Links
                            {
                                {Keywords.Self, ApiSampleData.ArticleLink}
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyLinksAndOneHRefAndMetaLink",
                        new Links
                            {
                                {Keywords.Self, ApiSampleData.ArticleLinkWithMeta}
                            },
                        DomReadOnlyLinks.Create(new Links
                            {
                                {Keywords.Self, ApiSampleData.ArticleLinkWithMeta}
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyLinksAndManyHRefOnlyLinks",
                        new Links
                            {
                                {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                {Keywords.Self, ApiSampleData.ArticleLink}
                            },
                        DomReadOnlyLinks.Create(new Links
                            {
                                {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                {Keywords.Self, ApiSampleData.ArticleLink}
                            })
                    },
                new object[]
                    {
                        "WithDomReadOnlyLinksAndManyHRefAndMetaLinks",
                        new Links
                            {
                                {Keywords.Up, ApiSampleData.ArticleCollectionLinkWithMeta},
                                {Keywords.Self, ApiSampleData.ArticleLinkWithMeta}
                            },
                        DomReadOnlyLinks.Create(new Links
                            {
                                {Keywords.Up, ApiSampleData.ArticleCollectionLinkWithMeta},
                                {Keywords.Self, ApiSampleData.ArticleLinkWithMeta}
                            })
                    },

                new object[]
                    {
                        "WithDomReadWriteLinksAndEmptyObject",
                        new Links(),
                        DomReadWriteLinks.Create()
                    },
                new object[]
                    {
                        "WithDomReadWriteLinksAndOneHRefOnlyLink",
                        new Links
                            {
                                {Keywords.Self, ApiSampleData.ArticleLink}
                            },
                        DomReadWriteLinks.Create(
                            DomReadWriteLink.Create(Keywords.Self,
                                DomHRef.Create(ApiSampleData.ArticleHRef)))
                    },
                new object[]
                    {
                        "WithDomReadWriteLinksAndOneHRefAndMetaLink",
                        new Links
                            {
                                {Keywords.Self, ApiSampleData.ArticleLinkWithMeta}
                            },
                        DomReadWriteLinks.Create(
                            DomReadWriteLink.Create(Keywords.Self,
                                DomHRef.Create(ApiSampleData.ArticleHRef),
                                DomReadOnlyMeta.Create(ApiSampleData.LinkMeta)))
                    },
                new object[]
                    {
                        "WithDomReadWriteLinksAndManyHRefOnlyLinks",
                        new Links
                            {
                                {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                {Keywords.Self, ApiSampleData.ArticleLink}
                            },
                        DomReadWriteLinks.Create(
                            DomReadWriteLink.Create(Keywords.Up,
                                DomHRef.Create(ApiSampleData.ArticleCollectionHRef)),
                            DomReadWriteLink.Create(Keywords.Self,
                                DomHRef.Create(ApiSampleData.ArticleHRef)))
                    },
                new object[]
                    {
                        "WithDomReadWriteLinksAndManyHRefAndMetaLinks",
                        new Links
                            {
                                {Keywords.Up, ApiSampleData.ArticleCollectionLinkWithMeta},
                                {Keywords.Self, ApiSampleData.ArticleLinkWithMeta}
                            },
                        DomReadWriteLinks.Create(
                            DomReadWriteLink.Create(Keywords.Up,
                                DomHRef.Create(ApiSampleData.ArticleCollectionHRef),
                                DomReadOnlyMeta.Create(ApiSampleData.LinkMeta)),
                            DomReadWriteLink.Create(Keywords.Self,
                                DomHRef.Create(ApiSampleData.ArticleHRef),
                                DomReadOnlyMeta.Create(ApiSampleData.LinkMeta)))
                    }
            };
        #endregion
    }
}
