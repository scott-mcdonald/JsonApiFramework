// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Metadata;
using JsonApiFramework.Metadata.Configuration;
using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable ExpressionIsAlwaysNull
namespace JsonApiFramework.Tests.Metadata.Configuration
{
    public class ResourceTypeConfigurationTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceTypeConfigurationTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(CreateResourceTypeTestData))]
        public void TestCreateResourceType(IUnitTest unitTest)
        { unitTest.Execute(this); }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        private static readonly IResourceType ArticleResourceTypeAndDefaultOptions = new ResourceType<Article>(
            // ResourceIdentity
            Factory<Article>.CreateResourceIdentityInfo("Article", x => x.ArticleId),

            // Attributes
            Factory.CreateAttributesInfo(
                Factory<Article>.CreateAttributeInfo("Title", x => x.Title),
                Factory<Article>.CreateAttributeInfo("Body", x => x.Body)),

            // Relationships
            Factory<Article>.CreateRelationshipsInfo(x => x.Relationships,
                Factory<Article>.CreateRelationshipInfo("Author", x => x.Author),
                Factory<Article>.CreateRelationshipInfo("Comments", x => x.Comments)),

            // Links
            Factory<Article>.CreateLinksInfo(x => x.Links)
        );

        private static readonly IResourceType ArticleResourceTypeAndStandardNamingOptions = new ResourceType<Article>(
            // ResourceIdentity
            Factory<Article>.CreateResourceIdentityInfo("articles", x => x.ArticleId),

            // Attributes
            Factory.CreateAttributesInfo(
                Factory<Article>.CreateAttributeInfo("title", x => x.Title),
                Factory<Article>.CreateAttributeInfo("body", x => x.Body)),

            // Relationships
            Factory<Article>.CreateRelationshipsInfo(x => x.Relationships,
                Factory<Article>.CreateRelationshipInfo("author", x => x.Author),
                Factory<Article>.CreateRelationshipInfo("comments", x => x.Comments)),

            // Links
            Factory<Article>.CreateLinksInfo(x => x.Links)
        );

        private static readonly IResourceType PersonResourceTypeAndDefaultOptions = new ResourceType<Person>(
            // ResourceIdentity
            Factory<Person>.CreateResourceIdentityInfo("Person", x => x.PersonId),

            // Attributes
            Factory.CreateAttributesInfo(
                Factory<Person>.CreateAttributeInfo("FirstName", x => x.FirstName),
                Factory<Person>.CreateAttributeInfo("LastName", x => x.LastName),
                Factory<Person>.CreateAttributeInfo("Twitter", x => x.Twitter),
                Factory<Person>.CreateAttributeInfo("MainPhoneNumber", x => x.MainPhoneNumber),
                Factory<Person>.CreateAttributeInfo("AlternatePhoneNumbers", x => x.AlternatePhoneNumbers)),

            // Relationships
            Factory.CreateRelationshipsInfo(
                Factory.CreateRelationshipInfo<Article>("Article", RelationshipCardinality.ToMany),
                Factory.CreateRelationshipInfo<Comment>("Comment", RelationshipCardinality.ToMany)),

            // Links
            null
        );

        private static readonly IResourceType PersonResourceTypeAndStandardNamingOptions = new ResourceType<Person>(
            // ResourceIdentity
            Factory<Person>.CreateResourceIdentityInfo("people", x => x.PersonId),

            // Attributes
            Factory.CreateAttributesInfo(
                Factory<Person>.CreateAttributeInfo("first-name", x => x.FirstName),
                Factory<Person>.CreateAttributeInfo("last-name", x => x.LastName),
                Factory<Person>.CreateAttributeInfo("twitter", x => x.Twitter),
                Factory<Person>.CreateAttributeInfo("main-phone-number", x => x.MainPhoneNumber),
                Factory<Person>.CreateAttributeInfo("alternate-phone-numbers", x => x.AlternatePhoneNumbers)),

            // Relationships
            Factory.CreateRelationshipsInfo(
                Factory.CreateRelationshipInfo<Article>("articles", RelationshipCardinality.ToMany),
                Factory.CreateRelationshipInfo<Comment>("comments", RelationshipCardinality.ToMany)),

            // Links
            null
        );

        private static readonly IResourceType CommentResourceTypeAndDefaultOptions = new ResourceType<Comment>(
            // ResourceIdentity
            Factory<Comment>.CreateResourceIdentityInfo("Comment", x => x.CommentId),

            // Attributes
            Factory.CreateAttributesInfo(
                Factory<Comment>.CreateAttributeInfo("Text", x => x.Text)),

            // Relationships
            Factory.CreateRelationshipsInfo(
                Factory.CreateRelationshipInfo<Article>("Article", RelationshipCardinality.ToOne),
                Factory.CreateRelationshipInfo<Person>("Person", RelationshipCardinality.ToOne)),

            // Links
            null
        );

        private static readonly IResourceType CommentResourceTypeAndStandardNamingOptions = new ResourceType<Comment>(
            // ResourceIdentity
            Factory<Comment>.CreateResourceIdentityInfo("comments", x => x.CommentId),

            // Attributes
            Factory.CreateAttributesInfo(
                Factory<Comment>.CreateAttributeInfo("text", x => x.Text)),

            // Relationships
            Factory.CreateRelationshipsInfo(
                Factory.CreateRelationshipInfo<Article>("article", RelationshipCardinality.ToOne),
                Factory.CreateRelationshipInfo<Person>("author", RelationshipCardinality.ToOne)),

            // Links
            null
        );

        public static readonly IEnumerable<object[]> CreateResourceTypeTestData = new[]
        {
            new object[]
            {
                new CreateResourceTypeUnitTest<Article>(
                    "WithArticleAndNullConventionsAndDefaultOptions",
                    () =>
                    {
                        var resourceTypeConfiguration = new ResourceTypeConfiguration<Article>();

                        resourceTypeConfiguration
                            .ResourceIdentity(x => x.ArticleId);

                        resourceTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.Title)
                                .Attribute(x => x.Body);

                        resourceTypeConfiguration
                            .Relationships(x => x.Relationships)
                                .Relationship(x => x.Author)
                                .Relationship(x => x.Comments);

                        resourceTypeConfiguration
                            .Links(x => x.Links);

                        return resourceTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    ArticleResourceTypeAndDefaultOptions)
            },

            new object[]
            {
                new CreateResourceTypeUnitTest<Article>(
                    "WithArticleAndNullConventionsAndStandardNamingOptions",
                    () =>
                    {
                        var resourceTypeConfiguration = new ResourceTypeConfiguration<Article>();

                        resourceTypeConfiguration
                            .ResourceIdentity(x => x.ArticleId, config => config.SetApiType("articles"));

                        resourceTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.Title, config => config.SetApiAttributeName("title"))
                                .Attribute(x => x.Body, config => config.SetApiAttributeName("body"));

                        resourceTypeConfiguration
                            .Relationships(x => x.Relationships)
                                .Relationship(x => x.Author, config => config.SetApiRel("author"))
                                .Relationship(x => x.Comments, config => config.SetApiRel("comments"));

                        resourceTypeConfiguration
                            .Links(x => x.Links);

                        return resourceTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    ArticleResourceTypeAndStandardNamingOptions)
            },

            new object[]
            {
                new CreateResourceTypeUnitTest<Person>(
                    "WithPersonAndNullConventionsAndDefaultOptions",
                    () =>
                    {
                        var resourceTypeConfiguration = new ResourceTypeConfiguration<Person>();

                        resourceTypeConfiguration
                            .ResourceIdentity(x => x.PersonId);

                        resourceTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.FirstName)
                                .Attribute(x => x.LastName)
                                .Attribute(x => x.Twitter)
                                .Attribute(x => x.MainPhoneNumber)
                                .Attribute(x => x.AlternatePhoneNumbers);

                        resourceTypeConfiguration
                            .Relationships()
                                .ToManyRelationship<Article>()
                                .ToManyRelationship<Comment>();

                        return resourceTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    PersonResourceTypeAndDefaultOptions)
            },

            new object[]
            {
                new CreateResourceTypeUnitTest<Person>(
                    "WithPersonAndNullConventionsAndStandardNamingOptions",
                    () =>
                    {
                        var resourceTypeConfiguration = new ResourceTypeConfiguration<Person>();

                        resourceTypeConfiguration
                            .ResourceIdentity(x => x.PersonId, config => config.SetApiType("people"));

                        resourceTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.FirstName, config => config.SetApiAttributeName("first-name"))
                                .Attribute(x => x.LastName, config => config.SetApiAttributeName("last-name"))
                                .Attribute(x => x.Twitter, config => config.SetApiAttributeName("twitter"))
                                .Attribute(x => x.MainPhoneNumber, config => config.SetApiAttributeName("main-phone-number"))
                                .Attribute(x => x.AlternatePhoneNumbers, config => config.SetApiAttributeName("alternate-phone-numbers"));

                        resourceTypeConfiguration
                            .Relationships()
                                .ToManyRelationship<Article>(config => config.SetApiRel("articles"))
                                .ToManyRelationship<Comment>(config => config.SetApiRel("comments"));

                        return resourceTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    PersonResourceTypeAndStandardNamingOptions)
            },

            new object[]
            {
                new CreateResourceTypeUnitTest<Comment>(
                    "WithCommentAndNullConventionsAndDefaultOptions",
                    () =>
                    {
                        var resourceTypeConfiguration = new ResourceTypeConfiguration<Comment>();

                        resourceTypeConfiguration
                            .ResourceIdentity(x => x.CommentId);

                        resourceTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.Text)
                                ;
                        resourceTypeConfiguration
                            .Relationships()
                                .ToOneRelationship<Article>()
                                .ToOneRelationship<Person>();

                        return resourceTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    CommentResourceTypeAndDefaultOptions)
            },

            new object[]
            {
                new CreateResourceTypeUnitTest<Comment>(
                    "WithCommentAndNullConventionsAndStandardNamingOptions",
                    () =>
                    {
                        var resourceTypeConfiguration = new ResourceTypeConfiguration<Comment>();

                        resourceTypeConfiguration
                            .ResourceIdentity(x => x.CommentId, config => config.SetApiType("comments"));

                        resourceTypeConfiguration
                            .Attributes()
                                .Attribute(x => x.Text, config => config.SetApiAttributeName("text"));

                        resourceTypeConfiguration
                            .Relationships()
                                .ToOneRelationship<Article>(config => config.SetApiRel("article"))
                                .ToOneRelationship<Person>(config => config.SetApiRel("author"));

                        return resourceTypeConfiguration;
                    },
                    default(IMetadataConventions),
                    CommentResourceTypeAndStandardNamingOptions)
            },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region ComplexType Types
        private class PhoneNumber
        {
            // ReSharper disable UnusedMember.Local
            public string AreaCode { get; set; }
            public string Number { get; set; }
            // ReSharper restore UnusedMember.Local
        }
        #endregion

        #region ResourceType Types
        private class Article
        {
            // ReSharper disable UnusedMember.Local
            public Guid ArticleId { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }

            public Person Author { get; set; }
            public List<Comment> Comments { get; set; }

            public Relationships Relationships { get; set; }
            public Links Links { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class Person
        {
            // ReSharper disable UnusedMember.Local
            public Guid PersonId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Twitter { get; set; }

            public PhoneNumber MainPhoneNumber { get; set; }
            public List<PhoneNumber> AlternatePhoneNumbers { get; set; }
            // ReSharper restore UnusedMember.Local
        }

        private class Comment
        {
            // ReSharper disable UnusedMember.Local
            public Guid CommentId { get; set; }
            public string Text { get; set; }
            // ReSharper restore UnusedMember.Local
        }
        #endregion

        #region Unit Tests
        private class CreateResourceTypeUnitTest<T> : UnitTest
        {
            #region Constructors
            public CreateResourceTypeUnitTest(string name, Func<ResourceTypeConfiguration<T>> factoryMethod, IMetadataConventions metadataConventions, IResourceType expectedResourceType)
                : base(name)
            {
                this.FactoryMethod = factoryMethod;
                this.MetadataConventions = metadataConventions;
                this.ExpectedResourceType = expectedResourceType;
            }
            #endregion

            #region UnitTest Overrides
            protected override void Arrange()
            {
                var resourceTypeConfiguration = this.FactoryMethod();
                this.ResourceTypeConfiguration = resourceTypeConfiguration;

                var clrTypeName = typeof(T).Name;
                this.WriteLine("CLR Type : {0}", clrTypeName);
                this.WriteLine();

                this.WriteLine("Expected ResourceType");
                this.WriteLine(this.ExpectedResourceType.ToJson());
                this.WriteLine();
            }

            protected override void Act()
            {
                var metadataConventions = this.MetadataConventions;
                var resourceTypeConfiguration = this.ResourceTypeConfiguration;
                var actualResourceType = resourceTypeConfiguration.Create(metadataConventions);
                this.ActualResourceType = actualResourceType;

                this.WriteLine("Actual ResourceType");
                this.WriteLine(this.ActualResourceType.ToJson());
            }

            protected override void Assert()
            {
                this.ActualResourceType.ShouldBeEquivalentTo(this.ExpectedResourceType);
            }
            #endregion

            #region User Supplied Properties
            private Func<ResourceTypeConfiguration<T>> FactoryMethod { get; }
            private IMetadataConventions MetadataConventions { get; }
            private IResourceType ExpectedResourceType { get; }
            #endregion

            #region Calculated Properties
            private ResourceTypeConfiguration<T> ResourceTypeConfiguration { get; set; }
            private IResourceType ActualResourceType { get; set; }
            #endregion
        }
        #endregion
    }
}
