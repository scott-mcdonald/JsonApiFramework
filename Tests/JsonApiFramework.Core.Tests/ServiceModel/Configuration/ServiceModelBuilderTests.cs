﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;
using JsonApiFramework.ServiceModel.Conventions;
using JsonApiFramework.TestAsserts.ServiceModel;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.ServiceModel.Configuration
{
    public class ServiceModelBuilderTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ServiceModelBuilderTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("CreateServiceModelTestData")]
        public void TestServiceModelBuilderCreateServiceModel(string name, IServiceModelFactory serviceModelFactory, ConventionSet conventionSet, IServiceModel expectedServiceModel)
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

            var expectedJson = expectedServiceModel.ToJson(serializerSettings);
            this.Output.WriteLine("Expected ServiceModel");
            this.Output.WriteLine(expectedJson);

            // Act
            var actualServiceModel = serviceModelFactory.Create(conventionSet);

            this.Output.WriteLine(String.Empty);

            var actualJson = actualServiceModel.ToJson(serializerSettings);
            this.Output.WriteLine("Actual ServiceModel");
            this.Output.WriteLine(actualJson);

            // Assert
            ServiceModelAssert.Equal(expectedServiceModel, actualServiceModel);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable UnusedMember.Global
        public static readonly IEnumerable<object[]> CreateServiceModelTestData = new[]
            {
                new object[] {"WithEmptyServiceModel", new ServiceModelBuilder(), null, ClrSampleData.ServiceModelEmpty},

                new object[] {"WithDirectConfigurationOfServiceModelWithOnlyArticleResourceTypeWithNullConventionSet", new WithDirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventionSet(), null, ClrSampleData.ServiceModelWithOnlyArticleResourceType},
                new object[] {"WithDirectConfigurationOfServiceModelWithBlogResourceTypesWithNullConventionSet", new WithDirectConfigurationServiceModelWithBlogResourceTypesWithNullConventionSet(), null, ClrSampleData.ServiceModelWithBlogResourceTypes},

                new object[] {"WithIndirectConfigurationOfServiceModelWithOnlyArticleResourceTypeWithNullConventionSet", new WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventionSet(), null, ClrSampleData.ServiceModelWithOnlyArticleResourceType},
                new object[] {"WithIndirectConfigurationOfServiceModelWithBlogResourceTypesWithNullConventionSet", new WithIndirectConfigurationServiceModelWithBlogResourceTypesWithNullConventionSet(), null, ClrSampleData.ServiceModelWithBlogResourceTypes},
                new object[] {"WithIndirectConfigurationOfServiceModelWithOrderResourceTypesWithNullConventionSet", new WithIndirectConfigurationServiceModelWithOrderResourceTypesWithNullConventionSet(), null, ClrSampleData.ServiceModelWithOrderResourceTypes},

                new object[] {"WithIndirectConfigurationOfServiceModelWithOnlyArticleResourceTypeWithConventionSet", new WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.ServiceModelWithOnlyArticleResourceType},
                new object[] {"WithIndirectConfigurationOfServiceModelWithBlogResourceTypesWithConventionSet", new WithIndirectConfigurationServiceModelWithBlogResourceTypesWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.ServiceModelWithBlogResourceTypes},
                new object[] {"WithIndirectConfigurationOfServiceModelWithOrderResourceTypesWithConventionSet", new WithIndirectConfigurationServiceModelWithOrderResourceTypesWithConventionSet(), TestConfigurations.CreateConventionSet(), ClrSampleData.ServiceModelWithOrderResourceTypes},
            };
        // ReSharper restore UnusedMember.Global
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Configurations With Null ConventionSet
        private class WithDirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventionSet : ServiceModelBuilder
        {
            public WithDirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventionSet()
            {
                // Hypermedia
                this.Resource<Article>()
                    .Hypermedia()
                    .SetApiCollectionPathSegment(ApiSampleData.ArticleCollectionPathSegment);

                // ResourceIdentity
                this.Resource<Article>()
                    .ResourceIdentity(x => x.Id)
                    .SetApiType(ApiSampleData.ArticleType);

                // Attributes
                this.Resource<Article>()
                    .Attribute(x => x.Title)
                    .SetApiPropertyName(ApiSampleData.ArticleTitlePropertyName);

                // Relationships
                this.Resource<Article>()
                    .Relationships(x => x.Relationships);

                this.Resource<Article>()
                    .ToOneRelationship<Person>(ApiSampleData.ArticleToAuthorRel)
                    .SetApiRelPathSegment(ApiSampleData.ArticleToAuthorRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                this.Resource<Article>()
                    .ToManyRelationship<Comment>(ApiSampleData.ArticleToCommentsRel)
                    .SetApiRelPathSegment(ApiSampleData.ArticleToCommentsRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Resource<Article>()
                    .Links(x => x.Links);
                this.Resource<Article>()
                    .Link(Keywords.Canonical);
                this.Resource<Article>()
                    .Link(Keywords.Self);

                // Meta
                this.Resource<Article>()
                    .Meta(x => x.Meta);
            }
        }

        private class WithDirectConfigurationServiceModelWithBlogResourceTypesWithNullConventionSet : ServiceModelBuilder
        {
            public WithDirectConfigurationServiceModelWithBlogResourceTypesWithNullConventionSet()
            {
                this.ConfigureArticleResourceType();
                this.ConfigureBlogResourceType();
                this.ConfigureCommentResourceType();
                this.ConfigurePersonResourceType();
            }

            private void ConfigureArticleResourceType()
            {
                // Hypermedia
                this.Resource<Article>()
                    .Hypermedia()
                    .SetApiCollectionPathSegment(ApiSampleData.ArticleCollectionPathSegment);

                // ResourceIdentity
                this.Resource<Article>()
                    .ResourceIdentity(x => x.Id)
                    .SetApiType(ApiSampleData.ArticleType);

                // Attributes
                this.Resource<Article>()
                    .Attribute(x => x.Title)
                    .SetApiPropertyName(ApiSampleData.ArticleTitlePropertyName);

                // Relationships
                this.Resource<Article>()
                    .Relationships(x => x.Relationships);

                this.Resource<Article>()
                    .ToOneRelationship<Person>(ApiSampleData.ArticleToAuthorRel)
                    .SetApiRelPathSegment(ApiSampleData.ArticleToAuthorRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                this.Resource<Article>()
                    .ToManyRelationship<Comment>(ApiSampleData.ArticleToCommentsRel)
                    .SetApiRelPathSegment(ApiSampleData.ArticleToCommentsRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Resource<Article>()
                    .Links(x => x.Links);
                this.Resource<Article>()
                    .Link(Keywords.Canonical);
                this.Resource<Article>()
                    .Link(Keywords.Self);

                // Meta
                this.Resource<Article>()
                    .Meta(x => x.Meta);
            }

            private void ConfigureBlogResourceType()
            {
                // Hypermedia
                this.Resource<Blog>()
                    .Hypermedia()
                    .SetApiCollectionPathSegment(ApiSampleData.BlogCollectionPathSegment);

                // ResourceIdentity
                this.Resource<Blog>()
                    .ResourceIdentity(x => x.Id)
                    .SetApiType(ApiSampleData.BlogType);

                // Attributes
                this.Resource<Blog>()
                    .Attribute(x => x.Name)
                    .SetApiPropertyName(ApiSampleData.BlogNamePropertyName);

                // Relationships
                this.Resource<Blog>()
                    .Relationships(x => x.Relationships);

                this.Resource<Blog>()
                    .ToManyRelationship<Article>(ApiSampleData.BlogToArticlesRel)
                    .SetApiRelPathSegment(ApiSampleData.BlogToArticlesRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Resource<Blog>()
                    .Links(x => x.Links);
                this.Resource<Blog>()
                    .Link(Keywords.Self);

                // Meta
                this.Resource<Blog>()
                    .Meta(x => x.Meta);
            }

            private void ConfigureCommentResourceType()
            {
                // Hypermedia
                this.Resource<Comment>()
                    .Hypermedia()
                    .SetApiCollectionPathSegment(ApiSampleData.CommentCollectionPathSegment);

                // ResourceIdentity
                this.Resource<Comment>()
                    .ResourceIdentity(x => x.Id)
                    .SetApiType(ApiSampleData.CommentType);

                // Attributes
                this.Resource<Comment>()
                    .Attribute(x => x.Body)
                    .SetApiPropertyName(ApiSampleData.CommentBodyPropertyName);

                // Relationships
                this.Resource<Comment>()
                    .Relationships(x => x.Relationships);

                this.Resource<Comment>()
                    .ToOneRelationship<Person>(ApiSampleData.CommentToAuthorRel)
                    .SetApiRelPathSegment(ApiSampleData.CommentToAuthorRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Resource<Comment>()
                    .Links(x => x.Links);
                this.Resource<Comment>()
                    .Link(Keywords.Self);

                // Meta
                this.Resource<Comment>()
                    .Meta(x => x.Meta);
            }

            private void ConfigurePersonResourceType()
            {
                // Hypermedia
                this.Resource<Person>()
                    .Hypermedia()
                    .SetApiCollectionPathSegment(ApiSampleData.PersonCollectionPathSegment);

                // ResourceIdentity
                this.Resource<Person>()
                    .ResourceIdentity(x => x.Id)
                    .SetApiType(ApiSampleData.PersonType);

                // Attributes
                this.Resource<Person>()
                    .Attribute(x => x.FirstName)
                    .SetApiPropertyName(ApiSampleData.PersonFirstNamePropertyName);

                this.Resource<Person>()
                    .Attribute(x => x.LastName)
                    .SetApiPropertyName(ApiSampleData.PersonLastNamePropertyName);

                this.Resource<Person>()
                    .Attribute(x => x.Twitter)
                    .SetApiPropertyName(ApiSampleData.PersonTwitterPropertyName);

                // Relationships
                this.Resource<Person>()
                    .Relationships(x => x.Relationships);

                this.Resource<Person>()
                    .ToManyRelationship<Comment>(ApiSampleData.PersonToCommentsRel)
                    .SetApiRelPathSegment(ApiSampleData.PersonToCommentsRelPathSegment)
                    .SetCanonicalRelPathMode(RelationshipCanonicalRelPathMode.DropPreviousPathSegments);

                // Links
                this.Resource<Person>()
                    .Links(x => x.Links);
                this.Resource<Person>()
                    .Link(Keywords.Self);

                // Meta
                this.Resource<Person>()
                    .Meta(x => x.Meta);
            }
        }

        private class WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventionSet : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventionSet()
            {
                this.Configurations.Add(new TestConfigurations.ArticleConfigurationWithNullConventionSet());
            }
        }

        private class WithIndirectConfigurationServiceModelWithBlogResourceTypesWithNullConventionSet : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithBlogResourceTypesWithNullConventionSet()
            {
                this.Configurations.Add(new TestConfigurations.ArticleConfigurationWithNullConventionSet());
                this.Configurations.Add(new TestConfigurations.BlogConfigurationWithNullConventionSet());
                this.Configurations.Add(new TestConfigurations.CommentConfigurationWithNullConventionSet());
                this.Configurations.Add(new TestConfigurations.PersonConfigurationWithNullConventionSet());
            }
        }

        private class WithIndirectConfigurationServiceModelWithOrderResourceTypesWithNullConventionSet : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithOrderResourceTypesWithNullConventionSet()
            {
                this.Configurations.Add(new TestConfigurations.OrderConfigurationWithNullConventionSet());
                this.Configurations.Add(new TestConfigurations.OrderItemConfigurationWithNullConventionSet());
                this.Configurations.Add(new TestConfigurations.PaymentConfigurationWithNullConventionSet());
                this.Configurations.Add(new TestConfigurations.PosSystemConfigurationWithNullConventionSet());
                this.Configurations.Add(new TestConfigurations.ProductConfigurationWithNullConventionSet());
                this.Configurations.Add(new TestConfigurations.StoreConfigurationWithNullConventionSet());
                this.Configurations.Add(new TestConfigurations.StoreConfigurationConfigurationWithNullConventionSet());
            }
        }
        #endregion

        #region Configurations With ConventionSet
        private class WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithConventionSet : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithConventionSet()
            {
                this.Configurations.Add(new TestConfigurations.ArticleConfigurationWithConventionSet());
            }
        }

        private class WithIndirectConfigurationServiceModelWithBlogResourceTypesWithConventionSet : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithBlogResourceTypesWithConventionSet()
            {
                this.Configurations.Add(new TestConfigurations.ArticleConfigurationWithConventionSet());
                this.Configurations.Add(new TestConfigurations.BlogConfigurationWithConventionSet());
                this.Configurations.Add(new TestConfigurations.CommentConfigurationWithConventionSet());
                this.Configurations.Add(new TestConfigurations.PersonConfigurationWithConventionSet());
            }
        }

        private class WithIndirectConfigurationServiceModelWithOrderResourceTypesWithConventionSet : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithOrderResourceTypesWithConventionSet()
            {
                this.Configurations.Add(new TestConfigurations.OrderConfigurationWithConventionSet());
                this.Configurations.Add(new TestConfigurations.OrderItemConfigurationWithConventionSet());
                this.Configurations.Add(new TestConfigurations.PaymentConfigurationWithConventionSet());
                this.Configurations.Add(new TestConfigurations.PosSystemConfigurationWithConventionSet());
                this.Configurations.Add(new TestConfigurations.ProductConfigurationWithConventionSet());
                this.Configurations.Add(new TestConfigurations.StoreConfigurationWithConventionSet());
                this.Configurations.Add(new TestConfigurations.StoreConfigurationConfigurationWithConventionSet());
            }
        }
        #endregion
    }
}
