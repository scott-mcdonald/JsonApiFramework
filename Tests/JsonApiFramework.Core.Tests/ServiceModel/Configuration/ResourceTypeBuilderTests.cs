// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System;
using System.Collections.Generic;

using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;
using JsonApiFramework.ServiceModel.Conventions;
using JsonApiFramework.TestAsserts.ServiceModel;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.ServiceModel.Configuration
{
    public class ResourceTypeBuilderTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceTypeBuilderTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("CreateResourceTypeTestData")]
        public void TestResourceTypeBuilderCreateResourceType(string name, IResourceTypeFactory resourceTypeFactory, ConventionSet conventionSet, IResourceType expectedResourceType)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange
            var serializerSettings = new JsonSerializerSettings
                {
                    Converters = new[]
                        {
                            (JsonConverter)new StringEnumConverter()
                        },
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                };

            var expectedJson = expectedResourceType.ToJson(serializerSettings);
            this.Output.WriteLine("Expected ResourceType");
            this.Output.WriteLine(expectedJson);

            // Act
            var actualResourceType = resourceTypeFactory.Create(conventionSet);

            this.Output.WriteLine(String.Empty);

            var actualJson = actualResourceType.ToJson(serializerSettings);
            this.Output.WriteLine("Actual ResourceType");
            this.Output.WriteLine(actualJson);

            // Assert
            ResourceTypeAssert.Equal(expectedResourceType, actualResourceType);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable UnusedMember.Global
        public static readonly IEnumerable<object[]> CreateResourceTypeTestData = new[]
            {
                new object[] {"WithArticleResourceTypeWithNullConventionSet", new TestConfigurations.ArticleConfigurationWithNullConventionSet(), null, SampleMetadata.ArticleResourceType},
                new object[] {"WithBlogResourceTypeWithNullConventionSet", new TestConfigurations.BlogConfigurationWithNullConventionSet(), null, SampleMetadata.BlogResourceType},
                new object[] {"WithCommentResourceTypeWithNullConventionSet", new TestConfigurations.CommentConfigurationWithNullConventionSet(), null, SampleMetadata.CommentResourceType},
                new object[] {"WithOrderResourceTypeWithNullConventionSet", new TestConfigurations.OrderConfigurationWithNullConventionSet(), null, SampleMetadata.OrderResourceType},
                new object[] {"WithOrderItemResourceTypeWithNullConventionSet", new TestConfigurations.OrderItemConfigurationWithNullConventionSet(), null, SampleMetadata.OrderItemResourceType},
                new object[] {"WithPaymentResourceTypeWithNullConventionSet", new TestConfigurations.PaymentConfigurationWithNullConventionSet(), null, SampleMetadata.PaymentResourceType},
                new object[] {"WithPersonResourceTypeWithNullConventionSet", new TestConfigurations.PersonConfigurationWithNullConventionSet(), null, SampleMetadata.PersonResourceType},
                new object[] {"WithPosSystemResourceTypeWithNullConventionSet", new TestConfigurations.PosSystemConfigurationWithNullConventionSet(), null, SampleMetadata.PosSystemResourceType},
                new object[] {"WithProductResourceTypeWithNullConventionSet", new TestConfigurations.ProductConfigurationWithNullConventionSet(), null, SampleMetadata.ProductResourceType},
                new object[] {"WithStoreResourceTypeWithNullConventionSet", new TestConfigurations.StoreConfigurationWithNullConventionSet(), null, SampleMetadata.StoreResourceType},
                new object[] {"WithStoreConfigurationResourceTypeWithNullConventionSet", new TestConfigurations.StoreConfigurationConfigurationWithNullConventionSet(), null, SampleMetadata.StoreConfigurationResourceType},

                new object[] {"WithArticleResourceTypeWithConventionSet", new TestConfigurations.ArticleConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.ArticleResourceType},
                new object[] {"WithBlogResourceTypeWithConventionSet", new TestConfigurations.BlogConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.BlogResourceType},
                new object[] {"WithCommentResourceTypeWithConventionSet", new TestConfigurations.CommentConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.CommentResourceType},
                new object[] {"WithOrderResourceTypeWithConventionSet", new TestConfigurations.OrderConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.OrderResourceType},
                new object[] {"WithOrderItemResourceTypeWithConventionSet", new TestConfigurations.OrderItemConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.OrderItemResourceType},
                new object[] {"WithPaymentResourceTypeWithConventionSet", new TestConfigurations.PaymentConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.PaymentResourceType},
                new object[] {"WithPersonResourceTypeWithConventionSet", new TestConfigurations.PersonConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.PersonResourceType},
                new object[] {"WithPosSystemResourceTypeWithConventionSet", new TestConfigurations.PosSystemConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.PosSystemResourceType},
                new object[] {"WithProductResourceTypeWithConventionSet", new TestConfigurations.ProductConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.ProductResourceType},
                new object[] {"WithStoreResourceTypeWithConventionSet", new TestConfigurations.StoreConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.StoreResourceType},
                new object[] {"WithStoreConfigurationResourceTypeWithConventionSet", new TestConfigurations.StoreConfigurationConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), SampleMetadata.StoreConfigurationResourceType},
            };
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
