// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Conventions;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;
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
        public void TestServiceModelBuilderCreateServiceModel(string name, IServiceModelFactory serviceModelFactory, IConventions conventions, IServiceModel expectedServiceModel)
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

            var expectedJson = expectedServiceModel.ToJson(serializerSettings);
            this.Output.WriteLine("Expected ServiceModel");
            this.Output.WriteLine(expectedJson);

            // Act
            var actualServiceModel = serviceModelFactory.Create(conventions);

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

                new object[] {"WithDirectConfigurationOfServiceModelWithOnlyArticleResourceTypeWithNullConventions", new WithDirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventions(), null, ClrSampleData.ServiceModelWithOnlyArticleResourceType},
                new object[] {"WithDirectConfigurationOfServiceModelWithBlogResourceTypesWithNullConventions", new WithDirectConfigurationServiceModelWithBlogResourceTypesWithNullConventions(), null, ClrSampleData.ServiceModelWithBlogResourceTypes},

                new object[] {"WithIndirectConfigurationOfServiceModelWithOnlyArticleResourceTypeWithNullConventions", new WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventions(), null, ClrSampleData.ServiceModelWithOnlyArticleResourceType},
                new object[] {"WithIndirectConfigurationOfServiceModelWithBlogResourceTypesWithNullConventions", new WithIndirectConfigurationServiceModelWithBlogResourceTypesWithNullConventions(), null, ClrSampleData.ServiceModelWithBlogResourceTypes},
                new object[] {"WithIndirectConfigurationOfServiceModelWithOrderResourceTypesWithNullConventions", new WithIndirectConfigurationServiceModelWithOrderResourceTypesWithNullConventions(), null, ClrSampleData.ServiceModelWithOrderResourceTypes},

                new object[] {"WithIndirectConfigurationOfServiceModelWithOnlyArticleResourceTypeWithConventions", new WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.ServiceModelWithOnlyArticleResourceType},
                new object[] {"WithIndirectConfigurationOfServiceModelWithBlogResourceTypesWithConventions", new WithIndirectConfigurationServiceModelWithBlogResourceTypesWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.ServiceModelWithBlogResourceTypes},
                new object[] {"WithIndirectConfigurationOfServiceModelWithOrderResourceTypesWithConventions", new WithIndirectConfigurationServiceModelWithOrderResourceTypesWithConventions(), TestConfigurations.CreateConventions(), ClrSampleData.ServiceModelWithOrderResourceTypes},
            };
        // ReSharper restore UnusedMember.Global
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Configurations With Null Conventions
        private class WithDirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventions : ServiceModelBuilder
        {
            public WithDirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventions()
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

        private class WithDirectConfigurationServiceModelWithBlogResourceTypesWithNullConventions : ServiceModelBuilder
        {
            public WithDirectConfigurationServiceModelWithBlogResourceTypesWithNullConventions()
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

        private class WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventions : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithNullConventions()
            {
                this.Configurations.Add(new TestConfigurations.ArticleConfigurationWithNullConventions());
            }
        }

        private class WithIndirectConfigurationServiceModelWithBlogResourceTypesWithNullConventions : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithBlogResourceTypesWithNullConventions()
            {
                this.Configurations.Add(new TestConfigurations.ArticleConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.BlogConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.CommentConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.PersonConfigurationWithNullConventions());
            }
        }

        private class WithIndirectConfigurationServiceModelWithOrderResourceTypesWithNullConventions : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithOrderResourceTypesWithNullConventions()
            {
                this.Configurations.Add(new TestConfigurations.MailingAddressConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.PhoneNumberConfigurationWithNullConventions());

                this.Configurations.Add(new TestConfigurations.OrderConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.OrderItemConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.PaymentConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.PosSystemConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.ProductConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.StoreConfigurationWithNullConventions());
                this.Configurations.Add(new TestConfigurations.StoreConfigurationConfigurationWithNullConventions());
            }
        }
        #endregion

        #region Configurations With Conventions
        private class WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithConventions : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithOnlyArticleResourceTypeWithConventions()
            {
                this.Configurations.Add(new TestConfigurations.ArticleConfigurationWithConventions());
            }
        }

        private class WithIndirectConfigurationServiceModelWithBlogResourceTypesWithConventions : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithBlogResourceTypesWithConventions()
            {
                this.Configurations.Add(new TestConfigurations.ArticleConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.BlogConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.CommentConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.PersonConfigurationWithConventions());
            }
        }

        private class WithIndirectConfigurationServiceModelWithOrderResourceTypesWithConventions : ServiceModelBuilder
        {
            public WithIndirectConfigurationServiceModelWithOrderResourceTypesWithConventions()
            {
                this.Configurations.Add(new TestConfigurations.MailingAddressConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.PhoneNumberConfigurationWithConventions());

                this.Configurations.Add(new TestConfigurations.OrderConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.OrderItemConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.PaymentConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.PosSystemConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.ProductConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.StoreConfigurationWithConventions());
                this.Configurations.Add(new TestConfigurations.StoreConfigurationConfigurationWithConventions());
            }
        }
        #endregion
    }
}
