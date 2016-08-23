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
    public class DomReadWriteLinkTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadWriteLinkTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadWriteLinkTestData")]
        internal void TestDomReadWriteLinkCreate(string name, string expectedRel, Link expectedLink, DomReadWriteLink actual)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadWriteLinkAssert.Equal(expectedRel, expectedLink, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadWriteLinkTestData = new[]
            {
                new object[] {"WithEmptyObject", Keywords.Self, Link.Empty, DomReadWriteLink.Create(Keywords.Self)},
                new object[] {"WithHRef", Keywords.Self, new Link(ApiSampleData.ArticleHRef), DomReadWriteLink.Create(Keywords.Self, DomHRef.Create(ApiSampleData.ArticleHRef))},
                new object[]
                    {
                        "WithHRefAndMeta",
                        Keywords.Self, 
                        new Link
                            {
                                HRef = ApiSampleData.ArticleHRef,
                                Meta = ApiSampleData.LinkMeta
                            },
                        DomReadWriteLink.Create(Keywords.Self,
                            DomHRef.Create(ApiSampleData.ArticleHRef),
                            DomReadOnlyMeta.Create(ApiSampleData.LinkMeta))
                    }
            };
        #endregion
    }
}
