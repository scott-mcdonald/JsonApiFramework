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
    public class DomReadOnlyResourceIdentifierTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadOnlyResourceIdentifierTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadOnlyResourceIdentifierTestData")]
        public void TestDomReadOnlyResourceIdentifierCreate(string name, ResourceIdentifier apiResourceIdentifier, Type clrResourceType)
        {
            // Arrange

            // Act
            var actual = DomReadOnlyResourceIdentifier.Create(apiResourceIdentifier, clrResourceType);

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadOnlyResourceIdentifierAssert.Equal(apiResourceIdentifier, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadOnlyResourceIdentifierTestData = new[]
            {
                new object[] {"WithEmptyObject", ResourceIdentifier.Empty, typeof(Article)},
                new object[] {"WithNonEmptyObject", ApiSampleData.ArticleResourceIdentifier, typeof(Article)},
                new object[] {"WithNonEmptyObjectAndMeta", ApiSampleData.ArticleResourceIdentifierWithMeta, typeof(Article)}
            };
        #endregion
    }
}
