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
    public class ServiceModelConfigurationTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ServiceModelConfigurationTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(CreateServiceModelTestData))]
        public void TestCreateServiceModel(IUnitTest unitTest)
        { unitTest.Execute(this); }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data
        private static readonly IComplexType PhoneNumberComplexTypeAndDefaultOptions = new ComplexType<PhoneNumber>(
            // Attributes
            Factory.CreateAttributesInfo(
                Factory<PhoneNumber>.CreateAttributeInfo("AreaCode", x => x.AreaCode),
                Factory<PhoneNumber>.CreateAttributeInfo("Number", x => x.Number))
        );

        private static readonly IComplexType PhoneNumberComplexTypeAndStandardNamingOptions = new ComplexType<PhoneNumber>(
            // Attributes
            Factory.CreateAttributesInfo(
                Factory<PhoneNumber>.CreateAttributeInfo("area-code", x => x.AreaCode),
                Factory<PhoneNumber>.CreateAttributeInfo("number", x => x.Number))
        );

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

        private static readonly IServiceModel ServiceModelAndDefaultOptions = new ServiceModel(
            nameof(ServiceModelAndDefaultOptions),
            new[]
            {
                PhoneNumberComplexTypeAndDefaultOptions
            },
            new[]
            {
                ArticleResourceTypeAndDefaultOptions,
                PersonResourceTypeAndDefaultOptions,
                CommentResourceTypeAndDefaultOptions
            });

        private static readonly IServiceModel ServiceModelAndStandardNamingOptions = new ServiceModel(
            nameof(ServiceModelAndStandardNamingOptions),
            new[]
            {
                PhoneNumberComplexTypeAndStandardNamingOptions
            },
            new[]
            {
                ArticleResourceTypeAndStandardNamingOptions,
                PersonResourceTypeAndStandardNamingOptions,
                CommentResourceTypeAndStandardNamingOptions
            });

        public static readonly IEnumerable<object[]> CreateServiceModelTestData = new[]
        {
            new object[]
            {
                new CreateServiceModelUnitTest(
                    "WithDirectConfigurationAndNullConventionsAndDefaultOptions",
                    () =>
                    {
                        var serviceModelConfiguration = new ServiceModelConfiguration();

                        serviceModelConfiguration
                            .SetName(nameof(ServiceModelAndDefaultOptions));

                        // PhoneNumber
                        serviceModelConfiguration
                            .ComplexType<PhoneNumber>()
                                .Attributes()
                                    .Attribute(x => x.AreaCode)
                                    .Attribute(x => x.Number);

                        // Article
                        serviceModelConfiguration
                            .ResourceType<Article>()
                                .ResourceIdentity(x => x.ArticleId);

                        serviceModelConfiguration
                            .ResourceType<Article>()
                                .Attributes()
                                    .Attribute(x => x.Title)
                                    .Attribute(x => x.Body);

                        serviceModelConfiguration
                            .ResourceType<Article>()
                                .Relationships(x => x.Relationships)
                                    .Relationship(x => x.Author)
                                    .Relationship(x => x.Comments);

                        serviceModelConfiguration
                            .ResourceType<Article>()
                                .Links(x => x.Links);

                        // Person
                        serviceModelConfiguration
                            .ResourceType<Person>()
                                .ResourceIdentity(x => x.PersonId);

                        serviceModelConfiguration
                            .ResourceType<Person>()
                                .Attributes()
                                    .Attribute(x => x.FirstName)
                                    .Attribute(x => x.LastName)
                                    .Attribute(x => x.Twitter)
                                    .Attribute(x => x.MainPhoneNumber)
                                    .Attribute(x => x.AlternatePhoneNumbers);

                        serviceModelConfiguration
                            .ResourceType<Person>()
                                .Relationships()
                                    .ToManyRelationship<Article>()
                                    .ToManyRelationship<Comment>();

                        // Comment
                        serviceModelConfiguration
                            .ResourceType<Comment>()
                                .ResourceIdentity(x => x.CommentId);

                        serviceModelConfiguration
                            .ResourceType<Comment>()
                                .Attributes()
                                    .Attribute(x => x.Text);

                        serviceModelConfiguration
                            .ResourceType<Comment>()
                                .Relationships()
                                    .ToOneRelationship<Article>()
                                    .ToOneRelationship<Person>();

                        return serviceModelConfiguration;
                    },
                    default(IMetadataConventions),
                    ServiceModelAndDefaultOptions)
            },

            new object[]
            {
                new CreateServiceModelUnitTest(
                    "WithDirectConfigurationAndNullConventionsAndStandardNamingOptions",
                    () =>
                    {
                        var serviceModelConfiguration = new ServiceModelConfiguration();

                        serviceModelConfiguration
                            .SetName(nameof(ServiceModelAndStandardNamingOptions));

                        // PhoneNumber
                        serviceModelConfiguration
                            .ComplexType<PhoneNumber>()
                                .Attributes()
                                    .Attribute(x => x.AreaCode, config => config.SetApiAttributeName("area-code"))
                                    .Attribute(x => x.Number, config => config.SetApiAttributeName("number"));

                        // Article
                        serviceModelConfiguration
                            .ResourceType<Article>()
                                .ResourceIdentity(x => x.ArticleId, config => config.SetApiType("articles"));

                        serviceModelConfiguration
                            .ResourceType<Article>()
                                .Attributes()
                                    .Attribute(x => x.Title, config => config.SetApiAttributeName("title"))
                                    .Attribute(x => x.Body, config => config.SetApiAttributeName("body"));

                        serviceModelConfiguration
                            .ResourceType<Article>()
                                .Relationships(x => x.Relationships)
                                    .Relationship(x => x.Author, config => config.SetApiRel("author"))
                                    .Relationship(x => x.Comments, config => config.SetApiRel("comments"));

                        serviceModelConfiguration
                            .ResourceType<Article>()
                                .Links(x => x.Links);

                        // Person
                        serviceModelConfiguration
                            .ResourceType<Person>()
                                .ResourceIdentity(x => x.PersonId, config => config.SetApiType("people"));

                        serviceModelConfiguration
                            .ResourceType<Person>()
                                .Attributes()
                                    .Attribute(x => x.FirstName, config => config.SetApiAttributeName("first-name"))
                                    .Attribute(x => x.LastName, config => config.SetApiAttributeName("last-name"))
                                    .Attribute(x => x.Twitter, config => config.SetApiAttributeName("twitter"))
                                    .Attribute(x => x.MainPhoneNumber, config => config.SetApiAttributeName("main-phone-number"))
                                    .Attribute(x => x.AlternatePhoneNumbers, config => config.SetApiAttributeName("alternate-phone-numbers"));

                        serviceModelConfiguration
                            .ResourceType<Person>()
                                .Relationships()
                                    .ToManyRelationship<Article>(config => config.SetApiRel("articles"))
                                    .ToManyRelationship<Comment>(config => config.SetApiRel("comments"));

                        // Comment
                        serviceModelConfiguration
                            .ResourceType<Comment>()
                                .ResourceIdentity(x => x.CommentId, config => config.SetApiType("comments"));

                        serviceModelConfiguration
                            .ResourceType<Comment>()
                                .Attributes()
                                    .Attribute(x => x.Text, config => config.SetApiAttributeName("text"));

                        serviceModelConfiguration
                            .ResourceType<Comment>()
                                .Relationships()
                                    .ToOneRelationship<Article>(config => config.SetApiRel("article"))
                                    .ToOneRelationship<Person>(config => config.SetApiRel("author"));

                        return serviceModelConfiguration;
                    },
                    default(IMetadataConventions),
                    ServiceModelAndStandardNamingOptions)
            },

            new object[]
            {
                new CreateServiceModelUnitTest(
                    "WithAddConfigurationAndNullConventionsAndDefaultOptions",
                    () =>
                    {
                        var serviceModelConfiguration = new ServiceModelConfiguration();
                        serviceModelConfiguration.SetName(nameof(ServiceModelAndDefaultOptions));
                        serviceModelConfiguration.Add(new PhoneNumberComplexTypeConfigurationAndDefaultOptions());
                        serviceModelConfiguration.Add(new ArticleResourceTypeConfigurationAndDefaultOptions());
                        serviceModelConfiguration.Add(new PersonResourceTypeConfigurationAndDefaultOptions());
                        serviceModelConfiguration.Add(new CommentResourceTypeConfigurationAndDefaultOptions());
                        return serviceModelConfiguration;
                    },
                    default(IMetadataConventions),
                    ServiceModelAndDefaultOptions)
            },

            new object[]
            {
                new CreateServiceModelUnitTest(
                    "WithAddConfigurationAndNullConventionsAndStandardNamingOptions",
                    () =>
                    {
                        var serviceModelConfiguration = new ServiceModelConfiguration();
                        serviceModelConfiguration.SetName(nameof(ServiceModelAndStandardNamingOptions));
                        serviceModelConfiguration.Add(new PhoneNumberComplexTypeConfigurationAndStandardNamingOptions());
                        serviceModelConfiguration.Add(new ArticleResourceTypeConfigurationAndStandardNamingOptions());
                        serviceModelConfiguration.Add(new PersonResourceTypeConfigurationAndStandardNamingOptions());
                        serviceModelConfiguration.Add(new CommentResourceTypeConfigurationAndStandardNamingOptions());
                        return serviceModelConfiguration;
                    },
                    default(IMetadataConventions),
                    ServiceModelAndStandardNamingOptions)
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

        #region Configuration Types
        private class PhoneNumberComplexTypeConfigurationAndDefaultOptions : ComplexTypeConfiguration<PhoneNumber>
        {
            public PhoneNumberComplexTypeConfigurationAndDefaultOptions()
            {
                this.Attributes()
                        .Attribute(x => x.AreaCode)
                        .Attribute(x => x.Number);
            }
        }

        private class ArticleResourceTypeConfigurationAndDefaultOptions : ResourceTypeConfiguration<Article>
        {
            public ArticleResourceTypeConfigurationAndDefaultOptions()
            {
                this.ResourceIdentity(x => x.ArticleId);

                this.Attributes()
                        .Attribute(x => x.Title)
                        .Attribute(x => x.Body);

                this.Relationships(x => x.Relationships)
                        .Relationship(x => x.Author)
                        .Relationship(x => x.Comments);

                this.Links(x => x.Links);
            }
        }

        private class PersonResourceTypeConfigurationAndDefaultOptions : ResourceTypeConfiguration<Person>
        {
            public PersonResourceTypeConfigurationAndDefaultOptions()
            {
                this.ResourceIdentity(x => x.PersonId);

                this.Attributes()
                        .Attribute(x => x.FirstName)
                        .Attribute(x => x.LastName)
                        .Attribute(x => x.Twitter)
                        .Attribute(x => x.MainPhoneNumber)
                        .Attribute(x => x.AlternatePhoneNumbers);

                this.Relationships()
                        .ToManyRelationship<Article>()
                        .ToManyRelationship<Comment>();
            }
        }

        private class CommentResourceTypeConfigurationAndDefaultOptions : ResourceTypeConfiguration<Comment>
        {
            public CommentResourceTypeConfigurationAndDefaultOptions()
            {
                this.ResourceIdentity(x => x.CommentId);

                this.Attributes()
                        .Attribute(x => x.Text);

                this.Relationships()
                        .ToOneRelationship<Article>()
                        .ToOneRelationship<Person>();
            }
        }


        private class PhoneNumberComplexTypeConfigurationAndStandardNamingOptions : ComplexTypeConfiguration<PhoneNumber>
        {
            public PhoneNumberComplexTypeConfigurationAndStandardNamingOptions()
            {
                this.Attributes()
                        .Attribute(x => x.AreaCode, config => config.SetApiAttributeName("area-code"))
                        .Attribute(x => x.Number, config => config.SetApiAttributeName("number"));
            }
        }

        private class ArticleResourceTypeConfigurationAndStandardNamingOptions : ResourceTypeConfiguration<Article>
        {
            public ArticleResourceTypeConfigurationAndStandardNamingOptions()
            {
                this.ResourceIdentity(x => x.ArticleId, config => config.SetApiType("articles"));

                this.Attributes()
                        .Attribute(x => x.Title, config => config.SetApiAttributeName("title"))
                        .Attribute(x => x.Body, config => config.SetApiAttributeName("body"));

                this.Relationships(x => x.Relationships)
                        .Relationship(x => x.Author, config => config.SetApiRel("author"))
                        .Relationship(x => x.Comments, config => config.SetApiRel("comments"));

                this.Links(x => x.Links);
            }
        }

        private class PersonResourceTypeConfigurationAndStandardNamingOptions : ResourceTypeConfiguration<Person>
        {
            public PersonResourceTypeConfigurationAndStandardNamingOptions()
            {
                this.ResourceIdentity(x => x.PersonId, config => config.SetApiType("people"));

                this.Attributes()
                        .Attribute(x => x.FirstName, config => config.SetApiAttributeName("first-name"))
                        .Attribute(x => x.LastName, config => config.SetApiAttributeName("last-name"))
                        .Attribute(x => x.Twitter, config => config.SetApiAttributeName("twitter"))
                        .Attribute(x => x.MainPhoneNumber, config => config.SetApiAttributeName("main-phone-number"))
                        .Attribute(x => x.AlternatePhoneNumbers, config => config.SetApiAttributeName("alternate-phone-numbers"));

                this.Relationships()
                        .ToManyRelationship<Article>(config => config.SetApiRel("articles"))
                        .ToManyRelationship<Comment>(config => config.SetApiRel("comments"));
            }
        }

        private class CommentResourceTypeConfigurationAndStandardNamingOptions : ResourceTypeConfiguration<Comment>
        {
            public CommentResourceTypeConfigurationAndStandardNamingOptions()
            {
                this.ResourceIdentity(x => x.CommentId, config => config.SetApiType("comments"));

                this.Attributes()
                        .Attribute(x => x.Text, config => config.SetApiAttributeName("text"));

                this.Relationships()
                        .ToOneRelationship<Article>(config => config.SetApiRel("article"))
                        .ToOneRelationship<Person>(config => config.SetApiRel("author"));
            }
        }
        #endregion

        #region Unit Tests
        private class CreateServiceModelUnitTest : UnitTest
        {
            #region Constructors
            public CreateServiceModelUnitTest(string name, Func<ServiceModelConfiguration> factoryMethod, IMetadataConventions metadataConventions, IServiceModel expectedServiceModel)
                : base(name)
            {
                this.FactoryMethod = factoryMethod;
                this.MetadataConventions = metadataConventions;
                this.ExpectedServiceModel = expectedServiceModel;
            }
            #endregion

            #region UnitTest Overrides
            protected override void Arrange()
            {
                var serviceModelConfiguration = this.FactoryMethod();
                this.ServiceModelConfiguration = serviceModelConfiguration;

                this.WriteLine("Expected ServiceModel");
                this.WriteLine(this.ExpectedServiceModel.ToJson());
                this.WriteLine();
            }

            protected override void Act()
            {
                var metadataConventions = this.MetadataConventions;
                var serviceModelConfiguration = this.ServiceModelConfiguration;
                var actualServiceModel = serviceModelConfiguration.Create(metadataConventions);
                this.ActualServiceModel = actualServiceModel;

                this.WriteLine("Actual ServiceModel");
                this.WriteLine(this.ActualServiceModel.ToJson());
            }

            protected override void Assert()
            {
                this.ActualServiceModel.ShouldBeEquivalentTo(this.ExpectedServiceModel);
            }
            #endregion

            #region User Supplied Properties
            private Func<ServiceModelConfiguration> FactoryMethod { get; }
            private IMetadataConventions MetadataConventions { get; }
            private IServiceModel ExpectedServiceModel { get; }
            #endregion

            #region Calculated Properties
            private ServiceModelConfiguration ServiceModelConfiguration { get; set; }
            private IServiceModel ActualServiceModel { get; set; }
            #endregion
        }
        #endregion
    }
}
