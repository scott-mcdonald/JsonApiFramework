// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomResourceIdentifierTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomResourceIdentifierTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomResourceIdentifierTestData")]
        internal void TestIDomResourceIdentifierAbstraction(string name, ResourceIdentifier expectedApiResourceIdentifier, Type expectedClrResourceType, IDomResourceIdentifier actual)
        {
            // Arrange

            // Act
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            this.Output.WriteLine("Expected ResourceIdentifier: {0}", expectedApiResourceIdentifier);

            this.Output.WriteLine(String.Empty);

            var actualApiResourceIdentifier = actual.ApiResourceIdentifier;
            var actualClrResourceType = actual.ClrResourceType;

            this.Output.WriteLine("Actual ResourceIdentifier:   {0}", actualApiResourceIdentifier);

            // Assert
            ResourceIdentifierAssert.Equal(expectedApiResourceIdentifier, actualApiResourceIdentifier);
            Assert.Equal(expectedClrResourceType, actualClrResourceType);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DomResourceIdentifierTestData = new[]
            {
                new object[]
                    {
                        "WithDomReadOnlyResourceIdentifierAndEmptyObject",
                        ResourceIdentifier.Empty, null,
                        DomReadOnlyResourceIdentifier.Create(ResourceIdentifier.Empty, null)
                    },
                new object[]
                    {
                        "WithDomReadOnlyResourceIdentifierAndNonEmptyObject",
                        ApiSampleData.ArticleResourceIdentifier, typeof(Article),
                        DomReadOnlyResourceIdentifier.Create(ApiSampleData.ArticleResourceIdentifier, typeof(Article))
                    },
                new object[]
                    {
                        "WithDomReadOnlyResourceIdentifierAndNonEmptyObjectAndMeta",
                        ApiSampleData.ArticleResourceIdentifierWithMeta, typeof(Article),
                        DomReadOnlyResourceIdentifier.Create(ApiSampleData.ArticleResourceIdentifierWithMeta, typeof(Article))
                    },

                new object[]
                    {
                        "WithDomReadWriteResourceIdentifierAndEmptyObject",
                        ResourceIdentifier.Empty, null,
                        DomReadWriteResourceIdentifier.Create()
                    },
                new object[]
                    {
                        "WithDomReadWriteResourceIdentifierAndNonEmptyObject",
                        ApiSampleData.ArticleResourceIdentifier, typeof(Article),
                        DomReadWriteResourceIdentifier.Create(
                            DomType.CreateFromResourceType(ClrSampleData.ArticleResourceType),
                            DomId.CreateFromApiResourceIdentity(ClrSampleData.ArticleResourceType, ApiSampleData.ArticleResourceIdentifier))
                    },
                new object[]
                    {
                        "WithDomReadWriteResourceIdentifierAndNonEmptyObjectAndMeta",
                        ApiSampleData.ArticleResourceIdentifierWithMeta, typeof(Article),
                        DomReadWriteResourceIdentifier.Create(
                            DomType.CreateFromResourceType(ClrSampleData.ArticleResourceType),
                            DomId.CreateFromApiResourceIdentity(ClrSampleData.ArticleResourceType, ApiSampleData.ArticleResourceIdentifier),
                            DomReadOnlyMeta.Create(ApiSampleData.ResourceMeta))
                    }
            };
        #endregion
    }
}
