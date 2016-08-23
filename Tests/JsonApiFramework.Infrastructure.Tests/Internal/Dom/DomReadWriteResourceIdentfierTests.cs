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
    public class DomReadWriteResourceIdentifierTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomReadWriteResourceIdentifierTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomReadWriteResourceIdentifierTestData")]
        internal void TestDomReadWriteResourceIdentifierCreate(string name, ResourceIdentifier expected, DomReadWriteResourceIdentifier actual)
        {

            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(actual);

            // Assert
            DomReadWriteResourceIdentifierAssert.Equal(expected, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomReadWriteResourceIdentifierTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyObject",
                        ResourceIdentifier.Empty,
                        DomReadWriteResourceIdentifier.Create()
                    },
                new object[]
                    {
                        "WithNonEmptyObject",
                        ApiSampleData.ArticleResourceIdentifier,
                        DomReadWriteResourceIdentifier.Create(
                            DomType.CreateFromResourceType(ClrSampleData.ArticleResourceType),
                            DomId.CreateFromApiResourceIdentity(ClrSampleData.ArticleResourceType, ApiSampleData.ArticleResourceIdentifier))
                    },
                new object[]
                    {
                        "WithNonEmptyObjectAndMeta",
                        ApiSampleData.ArticleResourceIdentifierWithMeta,
                        DomReadWriteResourceIdentifier.Create(
                            DomType.CreateFromResourceType(ClrSampleData.ArticleResourceType),
                            DomId.CreateFromApiResourceIdentity(ClrSampleData.ArticleResourceType, ApiSampleData.ArticleResourceIdentifier),
                            DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta))
                    }
            };
        #endregion
    }
}
