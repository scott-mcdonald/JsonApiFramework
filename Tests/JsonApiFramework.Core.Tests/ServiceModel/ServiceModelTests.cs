// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonApiFramework.Json;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Converters;
using JsonApiFramework.TestAsserts.ServiceModel;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Xunit.Abstractions;

namespace JsonApiFramework.Tests.ServiceModel;

public class ServiceModelTests : XUnitTest
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public ServiceModelTests(ITestOutputHelper output)
        : base(output)
    { }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Test Methods
    [Theory]
    [MemberData(nameof(ServiceModelTestData))]
    public void TestServiceModelToJson(string name, IServiceModel expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange
            var serializerOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = 
                    {
                        new JsonStringEnumConverter()
                    }
                };

        // Act
        var actual = expected.ToJson(serializerOptions);
        this.Output.WriteLine(actual);

        // Assert
        Assert.NotNull(actual);
    }

    [Theory]
    [MemberData(nameof(ServiceModelTestData))]
    public void TestServiceModelParse(string name, IServiceModel expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange
        var serializerOptions = new JsonSerializerOptions
            {
                Converters =
                    {
                        new JsonStringEnumConverter(),

                        // Metadata Converters
                        new AttributeInfoConverter(),
                        new AttributesInfoConverter(),
                        new ComplexTypeConverter(), 
                        new HypermediaInfoConverter(),
                        new LinkInfoConverter(),
                        new LinksInfoConverter(),
                        new MetaInfoConverter(),
                        new RelationshipInfoConverter(),
                        new RelationshipsInfoConverter(),
                        new ResourceIdentityInfoConverter(),
                        new ResourceTypeConverter(),
                        new ServiceModelConverter(),
                        new PropertyInfoConverter() // needs to be last in the order to avoid casting exceptions
                    },
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        var json = expected.ToJson(serializerOptions);

        // Act
        this.Output.WriteLine(json);
        var actual = JsonObject.Parse<IServiceModel>(json, serializerOptions);

        // Assert
        ServiceModelAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ClrTypeTestData))]
    public void TestServiceModelNonGenericGetResourceTypeByClrType(string name, bool resourceTypeExists, IServiceModel serviceModel, Type clrResourceType, IResourceType expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        if (!resourceTypeExists)
        {
            Assert.Throws<ServiceModelException>(() => serviceModel.GetResourceType(clrResourceType));
            return;
        }
        var actual = serviceModel.GetResourceType(clrResourceType);

        // Assert
        ResourceTypeAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ClrTypeTestData))]
    public void TestServiceModelNonGenericTryGetResourceTypeByClrType(string name, bool resourceTypeExists, IServiceModel serviceModel, Type clrResourceType, IResourceType expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        IResourceType actual;
        var actualExists = serviceModel.TryGetResourceType(clrResourceType, out actual);

        // Assert
        if (!resourceTypeExists)
        {
            Assert.False(actualExists);
            Assert.Null(actual);
            return;
        }

        Assert.True(actualExists);
        Assert.NotNull(actual);
        ResourceTypeAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ApiTypeTestData))]
    public void TestServiceModelNonGenericGetResourceTypeByApiType(string name, bool resourceTypeExists, IServiceModel serviceModel, string apiResourceType, IResourceType expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        if (!resourceTypeExists)
        {
            Assert.Throws<ServiceModelException>(() => serviceModel.GetResourceType(apiResourceType));
            return;
        }
        var actual = serviceModel.GetResourceType(apiResourceType);

        // Assert
        ResourceTypeAssert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ApiTypeTestData))]
    public void TestServiceModelNonGenericTryGetResourceTypeByApiType(string name, bool resourceTypeExists, IServiceModel serviceModel, string apiResourceType, IResourceType expected)
    {
        this.Output.WriteLine("Test Name: {0}", name);
        this.Output.WriteLine(string.Empty);

        // Arrange

        // Act
        IResourceType actual;
        var actualExists = serviceModel.TryGetResourceType(apiResourceType, out actual);

        // Assert
        if (!resourceTypeExists)
        {
            Assert.False(actualExists);
            Assert.Null(actual);
            return;
        }

        Assert.True(actualExists);
        Assert.NotNull(actual);
        ResourceTypeAssert.Equal(expected, actual);
    }

    [Fact]
    public void TestServiceModelGenericGetResourceTypeWithInvalidClrType()
    {
        // Arrange
        var serviceModel = ClrSampleData.ServiceModelWithOnlyArticleResourceType;

        // Act
        Assert.Throws<ServiceModelException>(() => serviceModel.GetResourceType<Blog>());

        // Assert
    }

    [Fact]
    public void TestServiceModelGenericGetResourceTypeWithValidClrType()
    {
        // Arrange
        var serviceModel = ClrSampleData.ServiceModelWithOnlyArticleResourceType;
        var expected = ClrSampleData.ArticleResourceType;

        // Act
        var actual = serviceModel.GetResourceType<Article>();

        // Assert
        ResourceTypeAssert.Equal(expected, actual);
    }

    [Fact]
    public void TestServiceModelGenericTryGetResourceTypeWithInvalidClrType()
    {
        // Arrange
        var serviceModel = ClrSampleData.ServiceModelWithOnlyArticleResourceType;

        // Act
        IResourceType actual;
        var actualExists = serviceModel.TryGetResourceType<Blog>(out actual);

        // Assert
        Assert.False(actualExists);
        Assert.Null(actual);
    }

    [Fact]
    public void TestServiceModelGenericTryGetResourceTypeWithValidClrType()
    {
        // Arrange
        var serviceModel = ClrSampleData.ServiceModelWithOnlyArticleResourceType;
        var expected = ClrSampleData.ArticleResourceType;

        // Act
        IResourceType actual;
        var actualExists = serviceModel.TryGetResourceType<Article>(out actual);

        // Assert
        Assert.True(actualExists);
        Assert.NotNull(actual);
        ResourceTypeAssert.Equal(expected, actual);
    }
    #endregion

    // PUBLIC FIELDS ////////////////////////////////////////////////////
    #region Test Data
    // ReSharper disable UnusedMember.Global
    public static readonly IEnumerable<object[]> ServiceModelTestData = new[]
        {
            new object[] {"WithEmptyObject", ClrSampleData.ServiceModelEmpty},
            new object[] {"WithResourceTypesAndMappedRelationshipsLinksAndMetaClrProperties", ClrSampleData.ServiceModelWithBlogResourceTypes},
            new object[] {"WithResourceTypesAndNoMappedRelationshipsLinksAndMetaClrProperties", ClrSampleData.ServiceModelWithOrderResourceTypes}
        };

    public static readonly IEnumerable<object[]> ClrTypeTestData = new[]
        {
            new object[] {"WithNullClrType", false, ClrSampleData.ServiceModelWithOnlyArticleResourceType, null, null},
            new object[] {"WithInvalidClrType", false, ClrSampleData.ServiceModelWithOnlyArticleResourceType, typeof(Blog), null},
            new object[] {"WithValidClrType", true, ClrSampleData.ServiceModelWithOnlyArticleResourceType, typeof(Article), ClrSampleData.ArticleResourceType}
        };

    public static readonly IEnumerable<object[]> ApiTypeTestData = new[]
        {
            new object[] {"WithNullApiType", false, ClrSampleData.ServiceModelWithOnlyArticleResourceType, null, null},
            new object[] {"WithInvalidApiType", false, ClrSampleData.ServiceModelWithOnlyArticleResourceType, ApiSampleData.BlogType, null},
            new object[] {"WithValidApiType", true, ClrSampleData.ServiceModelWithOnlyArticleResourceType, ApiSampleData.ArticleType, ClrSampleData.ArticleResourceType}
        };
    // ReSharper restore UnusedMember.Global
    #endregion
}
