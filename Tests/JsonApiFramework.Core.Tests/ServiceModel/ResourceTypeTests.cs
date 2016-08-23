// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.TestAsserts.ServiceModel;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.ServiceModel
{
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
        [MemberData("AttributeTestData")]
        public void TestResourceTypeGetAttribute(string name, bool attributeExists, IResourceType resourceType, string apiPropertyName, string clrPropertyName, JsonApiFramework.ServiceModel.IAttributeInfo expected)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange

            // Act
            if (!attributeExists)
            {
                Assert.Throws<ServiceModelException>(() => resourceType.GetApiAttribute(apiPropertyName));
                Assert.Throws<ServiceModelException>(() => resourceType.GetClrAttribute(clrPropertyName));
                return;
            }

            var actualApiAttribute = resourceType.GetApiAttribute(apiPropertyName);
            var actualClrAttribute = resourceType.GetClrAttribute(clrPropertyName);

            // Assert
            AttributeInfoAssert.Equal(expected, actualApiAttribute);
            AttributeInfoAssert.Equal(expected, actualClrAttribute);
        }

        [Theory]
        [MemberData("RelationshipTestData")]
        public void TestResourceTypeGetRelationship(string name, bool relationshipExists, IResourceType resourceType, string rel, IRelationshipInfo expected)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange

            // Act
            if (!relationshipExists)
            {
                Assert.Throws<ServiceModelException>(() => resourceType.GetRelationship(rel));
                return;
            }
            var actual = resourceType.GetRelationship(rel);

            // Assert
            RelationshipInfoAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("LinkTestData")]
        public void TestResourceTypeGetLink(string name, bool linkExists, IResourceType resourceType, string rel, ILinkInfo expected)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange

            // Act
            if (!linkExists)
            {
                Assert.Throws<ServiceModelException>(() => resourceType.GetLink(rel));
                return;
            }
            var actual = resourceType.GetLink(rel);

            // Assert
            LinkInfoAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("AttributeTestData")]
        public void TestResourceTypeTryGetAttribute(string name, bool attributeExists, IResourceType resourceType, string apiPropertyName, string clrPropertyName, JsonApiFramework.ServiceModel.IAttributeInfo expected)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange

            // Act
            JsonApiFramework.ServiceModel.IAttributeInfo actualApiAttribute;
            var actualApiAttributeExists = resourceType.TryGetApiAttribute(apiPropertyName, out actualApiAttribute);

            JsonApiFramework.ServiceModel.IAttributeInfo actualClrAttribute;
            var actualClrAttributeExists = resourceType.TryGetClrAttribute(clrPropertyName, out actualClrAttribute);

            // Assert
            if (!attributeExists)
            {
                Assert.False(actualApiAttributeExists);
                Assert.Null(actualApiAttribute);

                Assert.False(actualClrAttributeExists);
                Assert.Null(actualClrAttribute);

                return;
            }

            Assert.True(actualApiAttributeExists);
            Assert.NotNull(actualApiAttribute);

            AttributeInfoAssert.Equal(expected, actualApiAttribute);

            Assert.True(actualClrAttributeExists);
            Assert.NotNull(actualClrAttribute);

            AttributeInfoAssert.Equal(expected, actualClrAttribute);
        }

        [Theory]
        [MemberData("RelationshipTestData")]
        public void TestResourceTypeTryGetRelationship(string name, bool attributeExists, IResourceType resourceType, string clrPropertyName, IRelationshipInfo expected)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange

            // Act
            IRelationshipInfo actual;
            var actualExists = resourceType.TryGetRelationship(clrPropertyName, out actual);

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
        [MemberData("LinkTestData")]
        public void TestResourceTypeTryGetLink(string name, bool attributeExists, IResourceType resourceType, string clrPropertyName, ILinkInfo expected)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange

            // Act
            ILinkInfo actual;
            var actualExists = resourceType.TryGetLink(clrPropertyName, out actual);

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
        [MemberData("FactoryTestData")]
        public void TestResourceTypeCreateClrResource(string name, IResourceType resourceType)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange
            var clrResourceType = resourceType.ClrResourceType;

            // Act
            var clrResource = resourceType.CreateClrResource();

            // Assert
            Assert.NotNull(clrResource);
            Assert.IsType(clrResourceType, clrResource);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable UnusedMember.Global
        public static readonly IEnumerable<object[]> AttributeTestData = new[]
            {
                new object[] {"WithNullPropertyName", false, SampleMetadata.ArticleResourceType, null, null, null},
                new object[] {"WithEmptyPropertyName", false, SampleMetadata.ArticleResourceType, String.Empty, String.Empty, null},
                new object[] {"WithInvalidPropertyName", false, SampleMetadata.ArticleResourceType, "foo-bar", "FooBar", null},
                new object[] {"WithValidPropertyName", true, SampleMetadata.ArticleResourceType, SampleData.ArticleTitlePropertyName, StaticReflection.GetMemberName<Article>(x => x.Title), SampleMetadata.ArticleTitleAttribute}
            };

        public static readonly IEnumerable<object[]> RelationshipTestData = new[]
            {
                new object[] {"WithNullPropertyName", false, SampleMetadata.ArticleResourceType, null, null},
                new object[] {"WithEmptyPropertyName", false, SampleMetadata.ArticleResourceType, String.Empty, null},
                new object[] {"WithInvalidPropertyName", false, SampleMetadata.ArticleResourceType, "FooBar", null},
                new object[] {"WithValidPropertyName", true, SampleMetadata.ArticleResourceType, SampleData.ArticleToAuthorRel, SampleMetadata.ArticleToAuthorRelationship}
            };

        public static readonly IEnumerable<object[]> LinkTestData = new[]
            {
                new object[] {"WithNullPropertyName", false, SampleMetadata.ArticleResourceType, null, null},
                new object[] {"WithEmptyPropertyName", false, SampleMetadata.ArticleResourceType, String.Empty, null},
                new object[] {"WithInvalidPropertyName", false, SampleMetadata.ArticleResourceType, "FooBar", null},
                new object[] {"WithValidPropertyName", true, SampleMetadata.ArticleResourceType, Keywords.Self, SampleMetadata.ArticleSelfLink}
            };

        public static readonly IEnumerable<object[]> FactoryTestData = new[]
            {
                new object[] {"WithArticleResourceType", SampleMetadata.ArticleResourceType},
                new object[] {"WithBlogResourceType", SampleMetadata.BlogResourceType},
                new object[] {"WithCommentResourceType", SampleMetadata.CommentResourceType},
                new object[] {"WithPersonResourceType", SampleMetadata.PersonResourceType}
            };
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
