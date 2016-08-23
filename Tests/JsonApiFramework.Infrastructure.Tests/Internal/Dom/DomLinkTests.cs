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
    public class DomLinkTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomLinkTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomLinkTestData")]
        internal void TestIDomLinkAbstraction(string name, string expectedRel, Link expectedLink, IDomLink domLink)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.Output.WriteLine("Expected Rel:  {0}", expectedRel);
            this.Output.WriteLine("Expected Link: {0}", expectedLink);

            this.Output.WriteLine(String.Empty);

            var actualRel = domLink.Rel;
            var actualLink = domLink.Link;

            this.Output.WriteLine("Actual Rel:    {0}", actualRel);
            this.Output.WriteLine("Actual Link:   {0}", actualLink);

            // Assert
            Assert.Equal(expectedRel, actualRel);
            LinkAssert.Equal(expectedLink, actualLink);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomLinkTestData = new[]
            {
                new object[]
                    {
                        "WithDomReadOnlyLinkAndEmptyObject",
                        Keywords.Self, Link.Empty,
                        DomReadOnlyLink.Create(Keywords.Self, Link.Empty)
                    },

                new object[]
                    {
                        "WithDomReadOnlyLinkAndHRef",
                        Keywords.Self, new Link(ApiSampleData.ArticleHRef),
                        DomReadOnlyLink.Create(Keywords.Self, new Link(ApiSampleData.ArticleHRef))
                    },
                new object[]
                    {
                        "WithDomReadOnlyLinkAndHRefAndMeta",
                        Keywords.Self, 
                        new Link
                            {
                                HRef = ApiSampleData.ArticleHRef,
                                Meta = ApiSampleData.LinkMeta
                            },
                        DomReadOnlyLink.Create(Keywords.Self, new Link
                            {
                                HRef = ApiSampleData.ArticleHRef,
                                Meta = ApiSampleData.LinkMeta
                            })
                    },
                new object[]
                    {
                        "WithDomReadWriteLinkAndEmptyObject",
                        Keywords.Self, Link.Empty,
                        DomReadWriteLink.Create(Keywords.Self)
                    },

                new object[]
                    {
                        "WithDomReadWriteLinkAndHRef",
                        Keywords.Self, new Link(ApiSampleData.ArticleHRef),
                        DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleHRef))
                    },
                new object[]
                    {
                        "WithDomReadWriteLinkAndHRefAndMeta",
                        Keywords.Self, 
                        new Link
                            {
                                HRef = ApiSampleData.ArticleHRef,
                                Meta = ApiSampleData.LinkMeta
                            },
                        DomReadWriteLink.Create(Keywords.Self,
                            DomHRef.Create(ApiSampleData.ArticleHRef),
                            DomReadOnlyMeta.Create(ApiSampleData.LinkMeta))
                    },
            };
        #endregion
    }
}
