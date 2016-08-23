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
    public class DomReadOnlyLinksTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyLinksTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadOnlyLinksTestData")]
        public void TestDomReadOnlyLinksCreate(string name, Links expected)
        {
            // Arrange

            // Act
            var actual = DomReadOnlyLinks.Create(expected);

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyLinksAssert.Equal(expected, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadOnlyLinksTestData = new[]
            {
                new object[] {"WithEmptyObject", new Links()},
                new object[]
                    {
                        "WithOneHRefOnlyLink",
                        new Links
                            {
                                {Keywords.Self, ApiSampleData.ArticleLink}
                            }
                    },
                new object[]
                    {
                        "WithOneHRefAndMetaLink",
                        new Links
                            {
                                {Keywords.Self, ApiSampleData.ArticleLinkWithMeta}
                            }
                    },
                new object[]
                    {
                        "WithManyHRefOnlyLinks",
                        new Links
                            {
                                {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                {Keywords.Self, ApiSampleData.ArticleLink}
                            }
                    },
                new object[]
                    {
                        "WithManyHRefAndMetaLinks",
                        new Links
                            {
                                {Keywords.Up, ApiSampleData.ArticleCollectionLinkWithMeta},
                                {Keywords.Self, ApiSampleData.ArticleLinkWithMeta}
                            }
                    }
            };
        #endregion
    }
}
