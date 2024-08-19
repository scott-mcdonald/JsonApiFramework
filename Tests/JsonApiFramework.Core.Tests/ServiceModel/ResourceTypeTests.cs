﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System.Text.Json;
using JsonApiFramework.Extension;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Internal;
using JsonApiFramework.TestAsserts.ClrResources;
using JsonApiFramework.TestAsserts.ServiceModel;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Xunit.Abstractions;

namespace JsonApiFramework.Tests.ServiceModel;

public class ResourceTypeTests : XUnitTest
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public ResourceTypeTests(ITestOutputHelper output)
        : base(output)
    { }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Test Methods
    [Theory]
    [MemberData(nameof(AttributeTestData))]
    public void TestResourceTypeGetAttributeInfo(string name, bool attributeExists, IResourceType resourceType, string apiPropertyName, string clrPropertyName, JsonApiFramework.ServiceModel.IAttributeInfo expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        if (!attributeExists)
        {
            Assert.Throws<ServiceModelException>(() => resourceType.GetApiAttributeInfo(apiPropertyName));
            Assert.Throws<ServiceModelException>(() => resourceType.GetClrAttributeInfo(clrPropertyName));
            return;
        }

        var actualApiAttributeInfo = resourceType.GetApiAttributeInfo(apiPropertyName);
        var actualClrAttributeInfo = resourceType.GetClrAttributeInfo(clrPropertyName);

        // Assert
        AttributeInfoAssert.Equal(expected, actualApiAttributeInfo);
        AttributeInfoAssert.Equal(expected, actualClrAttributeInfo);
    }

    [Theory]
    [MemberData(nameof(RelationshipTestData))]
    public void TestResourceTypeGetRelationshipInfo(string name, bool relationshipExists, IResourceType resourceType, string rel, IRelationshipInfo expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        if (!relationshipExists)
        {
            Assert.Throws<ServiceModelException>(() => resourceType.GetRelationshipInfo(rel));
            return;
        }
        var actual = resourceType.GetRelationshipInfo(rel);

        // Assert
        RelationshipInfoAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(LinkTestData))]
    public void TestResourceTypeGetLinkInfo(string name, bool linkExists, IResourceType resourceType, string rel, ILinkInfo expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        if (!linkExists)
        {
            Assert.Throws<ServiceModelException>(() => resourceType.GetLinkInfo(rel));
            return;
        }
        var actual = resourceType.GetLinkInfo(rel);

        // Assert
        LinkInfoAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(AttributeTestData))]
    public void TestResourceTypeTryGetAttributeInfo(string name, bool attributeExists, IResourceType resourceType, string apiPropertyName, string clrPropertyName, JsonApiFramework.ServiceModel.IAttributeInfo expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        JsonApiFramework.ServiceModel.IAttributeInfo actualApiAttributeInfo;
        var actualApiAttributeInfoExists = resourceType.TryGetApiAttributeInfo(apiPropertyName, out actualApiAttributeInfo);

        JsonApiFramework.ServiceModel.IAttributeInfo actualClrAttributeInfo;
        var actualClrAttributeInfoExists = resourceType.TryGetClrAttributeInfo(clrPropertyName, out actualClrAttributeInfo);

        // Assert
        if (!attributeExists)
        {
            Assert.False(actualApiAttributeInfoExists);
            Assert.Null(actualApiAttributeInfo);

            Assert.False(actualClrAttributeInfoExists);
            Assert.Null(actualClrAttributeInfo);

            return;
        }

        Assert.True(actualApiAttributeInfoExists);
        Assert.NotNull(actualApiAttributeInfo);

        AttributeInfoAssert.Equal(expected, actualApiAttributeInfo);

        Assert.True(actualClrAttributeInfoExists);
        Assert.NotNull(actualClrAttributeInfo);

        AttributeInfoAssert.Equal(expected, actualClrAttributeInfo);
    }

    [Theory]
    [MemberData(nameof(RelationshipTestData))]
    public void TestResourceTypeTryGetRelationshipInfo(string name, bool attributeExists, IResourceType resourceType, string clrPropertyName, IRelationshipInfo expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        IRelationshipInfo actual;
        var actualExists = resourceType.TryGetRelationshipInfo(clrPropertyName, out actual);

        // Assert
        if (!attributeExists)
        {
            Assert.False(actualExists);
            Assert.Null(actual);
            return;
        }

        Assert.True(actualExists);
        Assert.NotNull(actual);
        RelationshipInfoAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(LinkTestData))]
    public void TestResourceTypeTryGetLinkInfo(string name, bool attributeExists, IResourceType resourceType, string clrPropertyName, ILinkInfo expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        ILinkInfo actual;
        var actualExists = resourceType.TryGetLinkInfo(clrPropertyName, out actual);

        // Assert
        if (!attributeExists)
        {
            Assert.False(actualExists);
            Assert.Null(actual);
            return;
        }

        Assert.True(actualExists);
        Assert.NotNull(actual);
        LinkInfoAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(FactoryTestData))]
    public void TestResourceTypeCreateClrResourceObject(string name, IResourceType resourceType)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange
        var clrResourceType = resourceType.ClrType;

        // Act
        var clrResource = resourceType.CreateClrObject();

        // Assert
        Assert.NotNull(clrResource);
        Assert.IsType(clrResourceType, clrResource);
    }

    /// <summary>Unit test that reproduces the GitHub issue #13.</summary>
    [Theory]
    [MemberData(nameof(MapApiAttributesToClrResourceTestData))]
    public void TestResourceTypeMapApiAttributesToClrResource(string name, object expected, object actual, Type clrResourceType, Resource apiResource)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange
        var resourceType = ClrSampleData.ServiceModelWithOrderResourceTypes.GetResourceType(clrResourceType);

        // Act
        resourceType.MapApiAttributesToClrResource(actual, apiResource, ClrSampleData.ServiceModelWithOrderResourceTypes);

        // Assert
        ClrResourceAssert.Equal(expected, actual);
    }

    [Fact]
    public void TestResourceTypeAsExtensibleObject()
    {
        this.Output.WriteLine("Test Name: {0}", nameof(this.TestResourceTypeAsExtensibleObject));
        this.Output.WriteLine(string.Empty);

        // Arrange
        var articleResourceType = new ResourceType(ClrSampleData.ArticleClrType,
                                                   ClrSampleData.ArticleHypermediaInfo,
                                                   ClrSampleData.ArticleResourceIdentityInfo,
                                                   ClrSampleData.ArticleAttributesInfo,
                                                   ClrSampleData.ArticleRelationshipsInfo,
                                                   ClrSampleData.ArticleLinksInfo,
                                                   ClrSampleData.ArticleMetaInfo);

        // Act
        articleResourceType.ModifyExtension<IResourceType, ResourceTypeExtension>(x => { x.ExtendedApiName = nameof(x.ExtendedApiName); });
        var articleResourceTypeExtensions = articleResourceType.Extensions.Cast<ResourceTypeExtension>().ToList();
        var actualExtensionCount = articleResourceTypeExtensions.Count;

        var actualExtension = articleResourceTypeExtensions.Single();
        var actualExtendedApiName = actualExtension.ExtendedApiName;

        // Assert
        Assert.Equal(1, actualExtensionCount);
        Assert.Equal(nameof(ResourceTypeExtension.ExtendedApiName), actualExtendedApiName);
    }
    #endregion

    // PUBLIC FIELDS ////////////////////////////////////////////////////
    #region Test Data
    // ReSharper disable UnusedMember.Global
    public static readonly IEnumerable<object[]> AttributeTestData = new[]
        {
            new object[] {"WithNullPropertyName", false, ClrSampleData.ArticleResourceType, null, null, null},
            new object[] {"WithEmptyPropertyName", false, ClrSampleData.ArticleResourceType, string.Empty, string.Empty, null},
            new object[] {"WithInvalidPropertyName", false, ClrSampleData.ArticleResourceType, "foo-bar", "FooBar", null},
            new object[] {"WithValidPropertyName", true, ClrSampleData.ArticleResourceType, ApiSampleData.ArticleTitlePropertyName, StaticReflection.GetMemberName<Article>(x => x.Title), ClrSampleData.ArticleTitleAttributeInfo}
        };

    public static readonly IEnumerable<object[]> RelationshipTestData = new[]
        {
            new object[] {"WithNullPropertyName", false, ClrSampleData.ArticleResourceType, null, null},
            new object[] {"WithEmptyPropertyName", false, ClrSampleData.ArticleResourceType, string.Empty, null},
            new object[] {"WithInvalidPropertyName", false, ClrSampleData.ArticleResourceType, "FooBar", null},
            new object[] {"WithValidPropertyName", true, ClrSampleData.ArticleResourceType, ApiSampleData.ArticleToAuthorRel, ClrSampleData.ArticleToAuthorRelationshipInfo}
        };

    public static readonly IEnumerable<object[]> LinkTestData = new[]
        {
            new object[] {"WithNullPropertyName", false, ClrSampleData.ArticleResourceType, null, null},
            new object[] {"WithEmptyPropertyName", false, ClrSampleData.ArticleResourceType, string.Empty, null},
            new object[] {"WithInvalidPropertyName", false, ClrSampleData.ArticleResourceType, "FooBar", null},
            new object[] {"WithValidPropertyName", true, ClrSampleData.ArticleResourceType, Keywords.Self, ClrSampleData.ArticleSelfLinkInfo}
        };

    public static readonly IEnumerable<object[]> FactoryTestData = new[]
        {
            new object[] {"WithArticleResourceType", ClrSampleData.ArticleResourceType},
            new object[] {"WithBlogResourceType", ClrSampleData.BlogResourceType},
            new object[] {"WithCommentResourceType", ClrSampleData.CommentResourceType},
            new object[] {"WithPersonResourceType", ClrSampleData.PersonResourceType}
        };

    public static readonly IEnumerable<object[]> MapApiAttributesToClrResourceTestData = new[]
        {
            new object[] {
                "WithProduct",
                SampleProducts.Product,
                new Product { ProductId = SampleProducts.Product.ProductId },
                typeof(Product),
                new Resource
                    {
                        Type = ClrSampleData.ProductType,
                        Id = Convert.ToString(SampleProducts.Product.ProductId),
                        Attributes = new ApiObject(
                            new ApiReadProperty("name", JsonSerializer.SerializeToElement("Widget A")),
                            new ApiReadProperty("unitPrice", JsonSerializer.SerializeToElement(25.0m)))
                    }},
            new object[] {
                "WithStoreConfiguration",
                SampleStoreConfigurations.StoreConfiguration,
                new StoreConfiguration { StoreConfigurationId = SampleStoreConfigurations.StoreConfiguration.StoreConfigurationId },
                typeof(StoreConfiguration),
                new Resource
                    {
                        Type = ClrSampleData.StoreConfigurationType,
                        Id = SampleStoreConfigurations.StoreConfiguration.StoreConfigurationId,
                        Attributes = new ApiObject(
                            ApiProperty.Create("isLive", SampleStoreConfigurations.StoreConfiguration.IsLive),
                            ApiProperty.Create("mailingAddress", new ApiObject(
                                ApiProperty.Create("address", SampleStoreConfigurations.StoreConfiguration.MailingAddress.Address),
                                ApiProperty.Create("city", SampleStoreConfigurations.StoreConfiguration.MailingAddress.City),
                                ApiProperty.Create("state", SampleStoreConfigurations.StoreConfiguration.MailingAddress.State),
                                ApiProperty.Create("zipCode", SampleStoreConfigurations.StoreConfiguration.MailingAddress.ZipCode))),
                            ApiProperty.Create("phoneNumbers", SampleStoreConfigurations.StoreConfiguration.PhoneNumbers
                                .Select(x =>
                                    {
                                        var apiObject = new ApiObject(
                                            ApiProperty.Create("areaCode", x.AreaCode),
                                            ApiProperty.Create("number", x.Number));
                                        return apiObject;
                                    })
                                .ToArray()))
                    }},
        };

    // ReSharper restore UnusedMember.Global
    #endregion

    // PRIVATE TYPES ////////////////////////////////////////////////////
    #region Test Types
    private class ResourceTypeExtension : Extension<IResourceType>
    {
        public string ExtendedApiName { get; set; }
    }
    #endregion
}
