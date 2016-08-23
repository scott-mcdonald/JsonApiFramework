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
    public class ResourceTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("ResourceTestData")]
        public void TestResourceToJson(string name, Resource expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            ResourceAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceTestData")]
        public void TestResourceParse(string name, Resource expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Resource>(json);

            // Assert
            ResourceAssert.Equal(expected, actual);
        }

        [Fact]
        public void TestResourceResourceIdentifierConversionOperator()
        {
            // Arrange
            var resource = SampleData.ArticleResource;
            var expected = SampleData.ArticleResourceIdentifier;

            // Act
            var actual = (ResourceIdentifier)resource;

            // Assert
            ResourceIdentifierAssert.Equal(actual, expected);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> ResourceTestData = new[]
            {
                new object[] {"WithEmptyObject", Resource.Empty},
                new object[] {"WithIdentityOnly", SampleData.ArticleResourceWithIdentityOnly},
                new object[] {"WithJsonDataTypesAttributes", SampleData.ArticleResourceWithJsonDataTypesAttributes},
                new object[] {"WithCompleteObject", SampleData.ArticleResource}
            };
        #endregion
    }
}
