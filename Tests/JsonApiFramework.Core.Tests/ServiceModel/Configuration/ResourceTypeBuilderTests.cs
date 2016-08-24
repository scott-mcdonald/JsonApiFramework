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
                new object[] {"WithArticleResourceTypeWithNullConventionSet", new TestConfigurations.ArticleConfigurationWithNullConventionSet(), null, ClrSampleData.ArticleResourceType},
                new object[] {"WithBlogResourceTypeWithNullConventionSet", new TestConfigurations.BlogConfigurationWithNullConventionSet(), null, ClrSampleData.BlogResourceType},
                new object[] {"WithCommentResourceTypeWithNullConventionSet", new TestConfigurations.CommentConfigurationWithNullConventionSet(), null, ClrSampleData.CommentResourceType},
                new object[] {"WithOrderResourceTypeWithNullConventionSet", new TestConfigurations.OrderConfigurationWithNullConventionSet(), null, ClrSampleData.OrderResourceType},
                new object[] {"WithOrderItemResourceTypeWithNullConventionSet", new TestConfigurations.OrderItemConfigurationWithNullConventionSet(), null, ClrSampleData.OrderItemResourceType},
                new object[] {"WithPaymentResourceTypeWithNullConventionSet", new TestConfigurations.PaymentConfigurationWithNullConventionSet(), null, ClrSampleData.PaymentResourceType},
                new object[] {"WithPersonResourceTypeWithNullConventionSet", new TestConfigurations.PersonConfigurationWithNullConventionSet(), null, ClrSampleData.PersonResourceType},
                new object[] {"WithPosSystemResourceTypeWithNullConventionSet", new TestConfigurations.PosSystemConfigurationWithNullConventionSet(), null, ClrSampleData.PosSystemResourceType},
                new object[] {"WithProductResourceTypeWithNullConventionSet", new TestConfigurations.ProductConfigurationWithNullConventionSet(), null, ClrSampleData.ProductResourceType},
                new object[] {"WithStoreResourceTypeWithNullConventionSet", new TestConfigurations.StoreConfigurationWithNullConventionSet(), null, ClrSampleData.StoreResourceType},
                new object[] {"WithStoreConfigurationResourceTypeWithNullConventionSet", new TestConfigurations.StoreConfigurationConfigurationWithNullConventionSet(), null, ClrSampleData.StoreConfigurationResourceType},

                new object[] {"WithArticleResourceTypeWithConventionSet", new TestConfigurations.ArticleConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.ArticleResourceType},
                new object[] {"WithBlogResourceTypeWithConventionSet", new TestConfigurations.BlogConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.BlogResourceType},
                new object[] {"WithCommentResourceTypeWithConventionSet", new TestConfigurations.CommentConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.CommentResourceType},
                new object[] {"WithOrderResourceTypeWithConventionSet", new TestConfigurations.OrderConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.OrderResourceType},
                new object[] {"WithOrderItemResourceTypeWithConventionSet", new TestConfigurations.OrderItemConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.OrderItemResourceType},
                new object[] {"WithPaymentResourceTypeWithConventionSet", new TestConfigurations.PaymentConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.PaymentResourceType},
                new object[] {"WithPersonResourceTypeWithConventionSet", new TestConfigurations.PersonConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.PersonResourceType},
                new object[] {"WithPosSystemResourceTypeWithConventionSet", new TestConfigurations.PosSystemConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.PosSystemResourceType},
                new object[] {"WithProductResourceTypeWithConventionSet", new TestConfigurations.ProductConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.ProductResourceType},
                new object[] {"WithStoreResourceTypeWithConventionSet", new TestConfigurations.StoreConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.StoreResourceType},
                new object[] {"WithStoreConfigurationResourceTypeWithConventionSet", new TestConfigurations.StoreConfigurationConfigurationWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.StoreConfigurationResourceType},
            };
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
