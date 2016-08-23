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
    public class DomReadWriteLinksTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadWriteLinksTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadWriteLinksTestData")]
        internal void TestDomReadWriteLinksCreate(string name, Links expected, DomReadWriteLinks actual)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadWriteLinksAssert.Equal(expected, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadWriteLinksTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyObject",
                        new Links(),
                        DomReadWriteLinks.Create()
                    },
                new object[]
                    {
                        "WithOneHRefOnlyLink",
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
                        "WithOneHRefAndMetaLink",
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
                        "WithManyHRefOnlyLinks",
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
                        "WithManyHRefAndMetaLinks",
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
