// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
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
            var resource = ApiSampleData.ArticleResource;
            var expected = ApiSampleData.ArticleResourceIdentifier;

            // Act
            var actual = (ResourceIdentifier)resource;

            // Assert
            ResourceIdentifierAssert.Equal(actual, expected);
        }

        [Fact]
        public void TestResourceToJsonWithEnumeration()
        {
            // Arrange
            var serializerSettings = JsonObject.DefaultToJsonSerializerSettings;
            serializerSettings.Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                };
            var jsonSerializer = JsonSerializer.Create(serializerSettings);

            const Enum openEnum = Enum.Open;
            var expectedSubstring = openEnum.ToString();
            var expectedAttributes = new EnumAttributes
                {
                    Enum = openEnum
                };

            var expectedResource = new Resource
                {
                    Type = "resource",
                    Id = "123",
                    Attributes = JObject.FromObject(expectedAttributes, jsonSerializer),
                };

            // Act
            var actualString = expectedResource.ToJson(serializerSettings);
            this.Output.WriteLine("Resource with Enum");
            this.Output.WriteLine(actualString);

            // Assert
            Assert.Contains(expectedSubstring, actualString);
        }

        [Fact]
        public void TestResourceParseWithEnumeration()
        {
            // Arrange
            var serializerSettings = JsonObject.DefaultToJsonSerializerSettings;
            serializerSettings.Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                };
            var jsonSerializer = JsonSerializer.Create(serializerSettings);

            const Enum expectedEnum = Enum.Open;
            var expected = new EnumAttributes
                {
                    Enum = expectedEnum
                };
            var expectedResource = new Resource
                {
                    Type = "resource",
                    Id = "123",
                    Attributes = JObject.FromObject(expected, jsonSerializer),
                };
            var expectedResourceJson = expectedResource.ToJson(serializerSettings);

            // Act
            this.Output.WriteLine("Resource with Enum");
            this.Output.WriteLine(expectedResourceJson);

            var actualResource = JsonObject.Parse<Resource>(expectedResourceJson);
            var actual = actualResource.Attributes.ToObject<EnumAttributes>();
            var actualEnum = actual.Enum;

            // Assert
            Assert.Equal(expectedEnum, actualEnum);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> ResourceTestData = new[]
            {
                new object[] {"WithEmptyObject", Resource.Empty},
                new object[] {"WithIdentityOnly", ApiSampleData.ArticleResourceWithIdentityOnly},
                new object[] {"WithJsonDataTypesAttributes", ApiSampleData.ArticleResourceWithJsonDataTypesAttributes},
                new object[] {"WithCompleteObject", ApiSampleData.ArticleResource}
            };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        public enum Enum
        {
            Closed,
            Open
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class EnumAttributes : JsonObject
        {
            [JsonProperty("enum")] public Enum Enum { get; set; }
        }
        #endregion
    }
}
