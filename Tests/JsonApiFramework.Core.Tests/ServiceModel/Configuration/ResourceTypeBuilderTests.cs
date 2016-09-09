// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

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
        public void TestResourceTypeBuilderCreateResourceType(string name, IResourceTypeFactory resourceTypeFactory, IConventions conventions, IResourceType expectedResourceType, IEnumerable<IComplexType> complexTypes)
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
                    NullValueHandling = NullValueHandling.Include
                };

            var expectedJson = expectedResourceType.ToJson(serializerSettings);
            this.Output.WriteLine("Expected ResourceType");
            this.Output.WriteLine(expectedJson);

            // Act
            var actualResourceType = resourceTypeFactory.Create(conventions);
            if (complexTypes != null)
            {
                var clrTypeToComplexTypeDictionary = complexTypes.ToDictionary(x => x.ClrType);
                actualResourceType.Initialize(clrTypeToComplexTypeDictionary);
            }

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
                new object[] {"WithArticleResourceTypeWithNullConventions", new TestConfigurations.ArticleConfigurationWithNullConventions(), null, ClrSampleData.ArticleResourceType, null},
                new object[] {"WithBlogResourceTypeWithNullConventions", new TestConfigurations.BlogConfigurationWithNullConventions(), null, ClrSampleData.BlogResourceType, null},
                new object[] {"WithCommentResourceTypeWithNullConventions", new TestConfigurations.CommentConfigurationWithNullConventions(), null, ClrSampleData.CommentResourceType, null},
                new object[] {"WithOrderResourceTypeWithNullConventions", new TestConfigurations.OrderConfigurationWithNullConventions(), null, ClrSampleData.OrderResourceType, null},
                new object[] {"WithOrderItemResourceTypeWithNullConventions", new TestConfigurations.OrderItemConfigurationWithNullConventions(), null, ClrSampleData.OrderItemResourceType, null},
                new object[] {"WithPaymentResourceTypeWithNullConventions", new TestConfigurations.PaymentConfigurationWithNullConventions(), null, ClrSampleData.PaymentResourceType, null},
                new object[] {"WithPersonResourceTypeWithNullConventions", new TestConfigurations.PersonConfigurationWithNullConventions(), null, ClrSampleData.PersonResourceType, null},
                new object[] {"WithPosSystemResourceTypeWithNullConventions", new TestConfigurations.PosSystemConfigurationWithNullConventions(), null, ClrSampleData.PosSystemResourceType, null},
                new object[] {"WithProductResourceTypeWithNullConventions", new TestConfigurations.ProductConfigurationWithNullConventions(), null, ClrSampleData.ProductResourceType, null},
                new object[] {"WithStoreResourceTypeWithNullConventions", new TestConfigurations.StoreConfigurationWithNullConventions(), null, ClrSampleData.StoreResourceType, null},
                new object[] {"WithStoreConfigurationResourceTypeWithNullConventions", new TestConfigurations.StoreConfigurationConfigurationWithNullConventions(), null, ClrSampleData.StoreConfigurationResourceType, new[]{ClrSampleData.MailingAddressComplexType, ClrSampleData.PhoneNumberComplexType}},

                new object[] {"WithArticleResourceTypeWithConventions", new TestConfigurations.ArticleConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.ArticleResourceType, null},
                new object[] {"WithBlogResourceTypeWithConventions", new TestConfigurations.BlogConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.BlogResourceType, null},
                new object[] {"WithCommentResourceTypeWithConventions", new TestConfigurations.CommentConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.CommentResourceType, null},
                new object[] {"WithOrderResourceTypeWithConventions", new TestConfigurations.OrderConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.OrderResourceType, null},
                new object[] {"WithOrderItemResourceTypeWithConventions", new TestConfigurations.OrderItemConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.OrderItemResourceType, null},
                new object[] {"WithPaymentResourceTypeWithConventions", new TestConfigurations.PaymentConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.PaymentResourceType, null},
                new object[] {"WithPersonResourceTypeWithConventions", new TestConfigurations.PersonConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.PersonResourceType, null},
                new object[] {"WithPosSystemResourceTypeWithConventions", new TestConfigurations.PosSystemConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.PosSystemResourceType, null},
                new object[] {"WithProductResourceTypeWithConventions", new TestConfigurations.ProductConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.ProductResourceType, null},
                new object[] {"WithStoreResourceTypeWithConventions", new TestConfigurations.StoreConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.StoreResourceType, null},
                new object[] {"WithStoreConfigurationResourceTypeWithConventions", new TestConfigurations.StoreConfigurationConfigurationWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.StoreConfigurationResourceType, new[]{ClrSampleData.MailingAddressComplexType, ClrSampleData.PhoneNumberComplexType}},
            };
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
