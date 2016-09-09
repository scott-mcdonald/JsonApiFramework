// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System;
using System.Collections.Generic;

using JsonApiFramework.Conventions;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;
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
        public void TestResourceTypeBuilderCreateResourceType(string name, IResourceTypeFactory resourceTypeFactory, IConventions conventions, IResourceType expectedResourceType)
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
            var actualResourceType = resourceTypeFactory.Create(conventions);

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
                new object[] {"WithArticleResourceTypeWithNullConventions", new TestConfigurations.ArticleConfigurationWithNullConventions(), null, ClrSampleData.ArticleResourceType},
                new object[] {"WithBlogResourceTypeWithNullConventions", new TestConfigurations.BlogConfigurationWithNullConventions(), null, ClrSampleData.BlogResourceType},
                new object[] {"WithCommentResourceTypeWithNullConventions", new TestConfigurations.CommentConfigurationWithNullConventions(), null, ClrSampleData.CommentResourceType},
                new object[] {"WithOrderResourceTypeWithNullConventions", new TestConfigurations.OrderConfigurationWithNullConventions(), null, ClrSampleData.OrderResourceType},
                new object[] {"WithOrderItemResourceTypeWithNullConventions", new TestConfigurations.OrderItemConfigurationWithNullConventions(), null, ClrSampleData.OrderItemResourceType},
                new object[] {"WithPaymentResourceTypeWithNullConventions", new TestConfigurations.PaymentConfigurationWithNullConventions(), null, ClrSampleData.PaymentResourceType},
                new object[] {"WithPersonResourceTypeWithNullConventions", new TestConfigurations.PersonConfigurationWithNullConventions(), null, ClrSampleData.PersonResourceType},
                new object[] {"WithPosSystemResourceTypeWithNullConventions", new TestConfigurations.PosSystemConfigurationWithNullConventions(), null, ClrSampleData.PosSystemResourceType},
                new object[] {"WithProductResourceTypeWithNullConventions", new TestConfigurations.ProductConfigurationWithNullConventions(), null, ClrSampleData.ProductResourceType},
                new object[] {"WithStoreResourceTypeWithNullConventions", new TestConfigurations.StoreConfigurationWithNullConventions(), null, ClrSampleData.StoreResourceType},
                new object[] {"WithStoreConfigurationResourceTypeWithNullConventions", new TestConfigurations.StoreConfigurationConfigurationWithNullConventions(), null, ClrSampleData.StoreConfigurationResourceType},

                new object[] {"WithArticleResourceTypeWithConventions", new TestConfigurations.ArticleConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.ArticleResourceType},
                new object[] {"WithBlogResourceTypeWithConventions", new TestConfigurations.BlogConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.BlogResourceType},
                new object[] {"WithCommentResourceTypeWithConventions", new TestConfigurations.CommentConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.CommentResourceType},
                new object[] {"WithOrderResourceTypeWithConventions", new TestConfigurations.OrderConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.OrderResourceType},
                new object[] {"WithOrderItemResourceTypeWithConventions", new TestConfigurations.OrderItemConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.OrderItemResourceType},
                new object[] {"WithPaymentResourceTypeWithConventions", new TestConfigurations.PaymentConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.PaymentResourceType},
                new object[] {"WithPersonResourceTypeWithConventions", new TestConfigurations.PersonConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.PersonResourceType},
                new object[] {"WithPosSystemResourceTypeWithConventions", new TestConfigurations.PosSystemConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.PosSystemResourceType},
                new object[] {"WithProductResourceTypeWithConventions", new TestConfigurations.ProductConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.ProductResourceType},
                new object[] {"WithStoreResourceTypeWithConventions", new TestConfigurations.StoreConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.StoreResourceType},
                new object[] {"WithStoreConfigurationResourceTypeWithConventions", new TestConfigurations.StoreConfigurationConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.StoreConfigurationResourceType},
            };
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
