// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Http;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Server.Hypermedia;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Server.Tests
{
    using Attribute = JsonApiFramework.JsonApi.ApiProperty;
    using Attributes = JsonApiFramework.JsonApi.ApiObject;

    public class DocumentContextTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentContextTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(TestDocumentContextDocumentBuildingTestData))]
        public void TestDocumentContextDocumentBuilding(string name, IDocumentContextOptions documentContextOptions, string currentUrlRequest, Document expectedApiDocument, Func<DocumentContext, string, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, currentUrlRequest, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Theory]
        [MemberData(nameof(TestDocumentContextResourceDocumentBuildingTestData))]
        public void TestDocumentContextResourceDocumentBuilding(string name, IDocumentContextOptions documentContextOptions, string currentUrlRequest, Document expectedApiDocument, Func<DocumentContext, string, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, currentUrlRequest, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Theory]
        [MemberData(nameof(TestDocumentContextResourceCollectionDocumentBuildingTestData))]
        public void TestDocumentContextResourceCollectionDocumentBuilding(string name, IDocumentContextOptions documentContextOptions, string currentUrlRequest, Document expectedApiDocument, Func<DocumentContext, string, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, currentUrlRequest, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Theory]
        [MemberData(nameof(TestDocumentContextResourceIdentifierDocumentBuildingTestData))]
        public void TestDocumentContextResourceIdentifierDocumentBuilding(string name, IDocumentContextOptions documentContextOptions, string currentUrlRequest, Document expectedApiDocument, Func<DocumentContext, string, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, currentUrlRequest, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Theory]
        [MemberData(nameof(TestDocumentContextResourceIdentifierCollectionDocumentBuildingTestData))]
        public void TestDocumentContextResourceIdentifierCollectionDocumentBuilding(string name, IDocumentContextOptions documentContextOptions, string currentUrlRequest, Document expectedApiDocument, Func<DocumentContext, string, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, currentUrlRequest, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Theory]
        [MemberData(nameof(TestDocumentContextHypermediaPredicateBuildingTestData))]
        public void TestDocumentContextHypermediaPredicateBuilding(string name, IDocumentContextOptions documentContextOptions, string currentUrlRequest, Document expectedApiDocument, Func<DocumentContext, string, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, currentUrlRequest, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Theory]
        [MemberData(nameof(TestDocumentContextErrorsDocumentBuildingTestData))]
        public void TestDocumentContextErrorsDocumentBuilding(string name, IDocumentContextOptions documentContextOptions, string currentUrlRequest, Document expectedApiDocument, Func<DocumentContext, string, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, currentUrlRequest, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Fact]
        public void TestDocumentContextImplementsIDisposable()
        {
            // Arrange
            var documentContextOptions = CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments);

            // Act
            var documentContext = new DocumentContext(documentContextOptions);

            // Assert
            Assert.IsAssignableFrom<IDisposable>(documentContext);
        }

        [Fact]
        public void TestDocumentContextThrowObjectDisposedExceptionAfterBeingDisposed()
        {
            // Arrange
            var documentContextOptions = CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments);
            var documentContext = new DocumentContext(documentContextOptions);
            var documentReader = (IDocumentReader)documentContext;
            var documentWriter = (IDocumentWriter)documentContext;

            // Act
            documentContext.Dispose();

            // Assert
            Assert.Throws<ObjectDisposedException>(() => documentReader.GetJsonApiVersion());
            Assert.Throws<ObjectDisposedException>(() => documentWriter.WriteDocument());
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly UrlBuilderConfiguration UrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
            {
                Scheme = "http",
                Host = "api.example.com"
            };

        #region TestDocumentContextDocumentBuildingTestData
        public static readonly IEnumerable<object[]> TestDocumentContextDocumentBuildingTestData = new[]
            {
                new object[]
                    {
                        "WithNothing",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef,
                        new Document(),
                        new Func<DocumentContext, string, IDocumentWriter>((documentContext, currentRequestUrl) => documentContext
                            .NewDocument(currentRequestUrl))
                    },

                new object[]
                    {
                        "WithMetaOnly",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef,
                        new Document
                            {
                                Meta = ApiSampleData.DocumentMeta
                            },
                        new Func<DocumentContext, string, IDocumentWriter>((documentContext, currentRequestUrl) => documentContext
                            .NewDocument(currentRequestUrl)
                                .SetMeta(ApiSampleData.DocumentMeta))
                    },

                new object[]
                    {
                        "WithMetaAndLinks",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef,
                        new Document
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                            },
                        new Func<DocumentContext, string, IDocumentWriter>((documentContext, currentRequestUrl) => documentContext
                            .NewDocument(currentRequestUrl)
                                .SetMeta(ApiSampleData.DocumentMeta)
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd())
                    },

                new object[]
                    {
                        "WithJsonApiAndMetaAndLinks",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef,
                        new Document
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                            },
                        new Func<DocumentContext, string, IDocumentWriter>((documentContext, currentRequestUrl) => documentContext
                            .NewDocument(currentRequestUrl)
                                .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                .SetMeta(ApiSampleData.DocumentMeta)
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd())
                    },
            };
        #endregion

        #region TestDocumentContextResourceDocumentBuildingTestData
        public static readonly IEnumerable<object[]> TestDocumentContextResourceDocumentBuildingTestData = new[]
            {
                new object[]
                    {
                        "WithNullArticleResource",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef,
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = null
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Up)
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .Resource(default(Article))
                                    .ResourceEnd())
                    },

                new object[]
                    {
                        "WithArticleResource",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef,
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = ApiSampleData.ArticleResource
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Up)
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .Resource(SampleArticles.Article)
                                        .SetMeta(ApiSampleData.ResourceMeta)
                                        .Relationships()
                                            .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                            .AddLink(Keywords.Canonical)
                                        .LinksEnd()
                                    .ResourceEnd())
                    },

                new object[]
                    {
                        "WithArticleResourceAndSparseFieldsets",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef + "?fields[articles]=",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                        {Keywords.Self, new Link(ApiSampleData.ArticleHRef + "?fields[articles]=")}
                                    },
                                Data = new Resource
                                       {
                                           Type = ApiSampleData.ArticleType,
                                           Id   = ApiSampleData.ArticleId,
                                           Relationships = new Relationships(),
                                           Links = new Links
                                                   {
                                                       {Keywords.Self, ApiSampleData.ArticleLink},
                                                       {Keywords.Canonical, ApiSampleData.ArticleLink}
                                                   },
                                           Meta = ApiSampleData.ResourceMeta
                                       }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Up)
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .Resource(SampleArticles.Article)
                                        .SetMeta(ApiSampleData.ResourceMeta)
                                        .Relationships()
                                            .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                            .AddLink(Keywords.Canonical)
                                        .LinksEnd()
                                    .ResourceEnd())
                    },

                new object[]
                    {
                        "WithArticleResourceAndIncludedResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef,
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                Included = new List<Resource>
                                    {
                                        ApiSampleData.PersonResource,
                                        ApiSampleData.CommentResource1,
                                        ApiSampleData.CommentResource2
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Up)
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .Resource(SampleArticles.Article)
                                        .SetMeta(ApiSampleData.ResourceMeta)
                                        .Relationships()
                                            .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                            .AddLink(Keywords.Canonical)
                                        .LinksEnd()
                                    .ResourceEnd()
                                    .Included()
                                        .Include(ToOneIncludedResource.Create(SampleArticles.Article, ApiSampleData.ArticleToAuthorRel, SamplePersons.Person))
                                            .SetMeta(ApiSampleData.ResourceMeta)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                        .Include(ToManyIncludedResources.Create(SampleArticles.Article, ApiSampleData.ArticleToCommentsRel, new []{ SampleComments.Comment1, SampleComments.Comment2 }))
                                            .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                    .IncludedEnd())
                    },

                new object[]
                    {
                        "WithArticleResourceAndIncludedResourcesAndSparseFieldsets",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef + "?fields[articles]=title&fields[people]=first-name,last-name&fields[comments]=",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                        {Keywords.Self, new Link(ApiSampleData.ArticleHRef + "?fields[articles]=title&fields[people]=first-name,last-name&fields[comments]=")}
                                    },
                                Data = new Resource
                                       {
                                           Type       = ApiSampleData.ArticleType,
                                           Id         = ApiSampleData.ArticleId,
                                           Attributes = new Attributes(Attribute.Create("title", "JSON API paints my bikeshed!")),
                                           Relationships = new Relationships
                                                           {
                                                               {ApiSampleData.ArticleToAuthorRel, new ToOneRelationship { Data = ApiSampleData.PersonResourceIdentifier }},
                                                               {ApiSampleData.ArticleToCommentsRel, new ToManyRelationship { Data = ApiSampleData.CommentResourceIdentifiers }}
                                                           },
                                           Links = new Links
                                                   {
                                                       {Keywords.Self, ApiSampleData.ArticleLink},
                                                       {Keywords.Canonical, ApiSampleData.ArticleLink}
                                                   },
                                           Meta = ApiSampleData.ResourceMeta
                                       },
                                Included = new List<Resource>
                                    {
                                        new Resource
                                        {
                                            Type = ApiSampleData.PersonType,
                                            Id   = ApiSampleData.PersonId,
                                            Attributes = new Attributes(
                                                Attribute.Create("first-name", "John"),
                                                Attribute.Create("last-name",  "Doe")),
                                            Relationships = new Relationships(),
                                            Links = new Links
                                                    {
                                                        {Keywords.Self, ApiSampleData.PersonLink}
                                                    },
                                            Meta = ApiSampleData.ResourceMeta
                                        },
                                        new Resource
                                        {
                                            Type       = ApiSampleData.CommentType,
                                            Id         = ApiSampleData.CommentId1,
                                            Relationships = new Relationships(),
                                            Links = new Links
                                                    {
                                                        {Keywords.Self, ApiSampleData.CommentLink1}
                                                    },
                                            Meta = ApiSampleData.ResourceMeta1
                                        },
                                        new Resource
                                        {
                                            Type       = ApiSampleData.CommentType,
                                            Id         = ApiSampleData.CommentId2,
                                            Relationships = new Relationships(),
                                            Links = new Links
                                                    {
                                                        {Keywords.Self, ApiSampleData.CommentLink2}
                                                    },
                                            Meta = ApiSampleData.ResourceMeta2
                                        }
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Up)
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .Resource(SampleArticles.Article)
                                        .SetMeta(ApiSampleData.ResourceMeta)
                                        .Relationships()
                                            .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                            .AddLink(Keywords.Canonical)
                                        .LinksEnd()
                                    .ResourceEnd()
                                    .Included()
                                        .Include(ToOneIncludedResource.Create(SampleArticles.Article, ApiSampleData.ArticleToAuthorRel, SamplePersons.Person))
                                            .SetMeta(ApiSampleData.ResourceMeta)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                        .Include(ToManyIncludedResources.Create(SampleArticles.Article, ApiSampleData.ArticleToCommentsRel, new []{ SampleComments.Comment1, SampleComments.Comment2 }))
                                            .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                    .IncludedEnd())
                    },

                new object[]
                    {
                        "WithArticleResourceAndIncludedResourcesIncludingMoreResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef,
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                Included = new List<Resource>
                                    {
                                        ApiSampleData.PersonResource1,
                                        ApiSampleData.PersonResource2,
                                        ApiSampleData.CommentResourceWithResourceLinkage1,
                                        ApiSampleData.CommentResourceWithResourceLinkage2,
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Up)
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .Resource(SampleArticles.Article)
                                        .SetMeta(ApiSampleData.ResourceMeta)
                                        .Relationships()
                                            .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                            .AddLink(Keywords.Canonical)
                                        .LinksEnd()
                                    .ResourceEnd()
                                    .Included()
                                        .Include(ToOneIncludedResource.Create(SampleArticles.Article, ApiSampleData.ArticleToAuthorRel, SamplePersons.Person))
                                            .SetMeta(ApiSampleData.ResourceMeta)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                        .Include(ToManyIncludedResources.Create(SampleArticles.Article, ApiSampleData.ArticleToCommentsRel, new []{ SampleComments.Comment1, SampleComments.Comment2 }))
                                            .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                        .Include(ToOneIncludedResource.Create(SampleComments.Comment1, ApiSampleData.CommentToAuthorRel, SamplePersons.Person1),
                                                 ToOneIncludedResource.Create(SampleComments.Comment2, ApiSampleData.CommentToAuthorRel, SamplePersons.Person2))
                                            .SetMeta(ApiSampleData.ResourceMeta)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                    .IncludedEnd())
                    },

                new object[]
                    {
                        "WithArticleResourceAndIncludedResourcesIncludingMoreResourcesAndSparseFieldsets",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleHRef + "?fields[articles]=title&fields[people]=first-name,last-name&fields[comments]=",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Up, ApiSampleData.ArticleCollectionLink},
                                        {Keywords.Self, new Link(ApiSampleData.ArticleHRef + "?fields[articles]=title&fields[people]=first-name,last-name&fields[comments]=")}
                                    },
                                Data = new Resource
                                       {
                                           Type       = ApiSampleData.ArticleType,
                                           Id         = ApiSampleData.ArticleId,
                                           Attributes = new Attributes(Attribute.Create("title", "JSON API paints my bikeshed!")),
                                           Relationships = new Relationships
                                                           {
                                                               {ApiSampleData.ArticleToAuthorRel, new ToOneRelationship { Data    = ApiSampleData.PersonResourceIdentifier }},
                                                               {ApiSampleData.ArticleToCommentsRel, new ToManyRelationship { Data = ApiSampleData.CommentResourceIdentifiers }}
                                                           },
                                           Links = new Links
                                                   {
                                                       {Keywords.Self, ApiSampleData.ArticleLink},
                                                       {Keywords.Canonical, ApiSampleData.ArticleLink}
                                                   },
                                           Meta = ApiSampleData.ResourceMeta
                                       },
                                Included = new List<Resource>
                                    {
                                        new Resource
                                        {
                                            Type = ApiSampleData.PersonType,
                                            Id   = ApiSampleData.PersonId,
                                            Attributes = new Attributes(
                                                Attribute.Create("first-name", "John"),
                                                Attribute.Create("last-name",  "Doe")),
                                            Relationships = new Relationships(),
                                            Links = new Links
                                                    {
                                                        {Keywords.Self, ApiSampleData.PersonLink}
                                                    },
                                            Meta = ApiSampleData.ResourceMeta
                                        },
                                        new Resource
                                        {
                                            Type       = ApiSampleData.CommentType,
                                            Id         = ApiSampleData.CommentId1,
                                            Relationships = new Relationships
                                                            {
                                                                {ApiSampleData.ArticleToAuthorRel, new ToOneRelationship { Data    = ApiSampleData.PersonResourceIdentifier }}
                                                            },
                                            Links = new Links
                                                    {
                                                        {Keywords.Self, ApiSampleData.CommentLink1}
                                                    },
                                            Meta = ApiSampleData.ResourceMeta1
                                        },
                                        new Resource
                                        {
                                            Type       = ApiSampleData.CommentType,
                                            Id         = ApiSampleData.CommentId2,
                                            Relationships = new Relationships
                                                            {
                                                                {ApiSampleData.ArticleToAuthorRel, new ToOneRelationship { Data = ApiSampleData.PersonResourceIdentifier2 }}
                                                            },
                                            Links = new Links
                                                    {
                                                        {Keywords.Self, ApiSampleData.CommentLink2}
                                                    },
                                            Meta = ApiSampleData.ResourceMeta2
                                        },
                                        new Resource
                                        {
                                            Type = ApiSampleData.PersonType,
                                            Id   = ApiSampleData.PersonId2,
                                            Attributes = new Attributes(
                                                Attribute.Create("first-name", "Jane"),
                                                Attribute.Create("last-name",  "Doe")),
                                            Relationships = new Relationships(),
                                            Links = new Links
                                                    {
                                                        {Keywords.Self, ApiSampleData.PersonLink2}
                                                    },
                                            Meta = ApiSampleData.ResourceMeta
                                        }
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Up)
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .Resource(SampleArticles.Article)
                                        .SetMeta(ApiSampleData.ResourceMeta)
                                        .Relationships()
                                            .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                            .AddLink(Keywords.Canonical)
                                        .LinksEnd()
                                    .ResourceEnd()
                                    .Included()
                                        .Include(ToOneIncludedResource.Create(SampleArticles.Article, ApiSampleData.ArticleToAuthorRel, SamplePersons.Person))
                                            .SetMeta(ApiSampleData.ResourceMeta)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                        .Include(ToManyIncludedResources.Create(SampleArticles.Article, ApiSampleData.ArticleToCommentsRel, new []{ SampleComments.Comment1, SampleComments.Comment2 }))
                                            .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                        .Include(ToOneIncludedResource.Create(SampleComments.Comment1, ApiSampleData.CommentToAuthorRel, SamplePersons.Person1),
                                                 ToOneIncludedResource.Create(SampleComments.Comment2, ApiSampleData.CommentToAuthorRel, SamplePersons.Person2))
                                            .SetMeta(ApiSampleData.ResourceMeta)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                    .IncludedEnd())
                    },
            };
        #endregion

        #region TestDocumentContextResourceCollectionDocumentBuildingTestData
        public static readonly IEnumerable<object[]> TestDocumentContextResourceCollectionDocumentBuildingTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyArticleResourceCollection",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleCollectionHRef,
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = Enumerable.Empty<Resource>()
                                                 .ToList()
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(Enumerable.Empty<Article>())
                                    .ResourceCollectionEnd())
                    },

                new object[]
                    {
                        "WithArticleResourceCollection",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleCollectionHRef,
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>
                                    {
                                        ApiSampleData.ArticleResource1,
                                        ApiSampleData.ArticleResource2
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(new []{ SampleArticles.Article1, SampleArticles.Article2 })
                                        .SetMeta(ApiSampleData.ResourceMeta)
                                        .Relationships()
                                            .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                            .AddLink(Keywords.Canonical)
                                        .LinksEnd()
                                    .ResourceCollectionEnd())
                    },

                new object[]
                    {
                        "WithArticleResourceCollectionAndIncludedResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleCollectionHRef,
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>
                                    {
                                        ApiSampleData.ArticleResourceWithResourceLinkage1,
                                        ApiSampleData.ArticleResourceWithResourceLinkage2
                                    },
                                Included = new List<Resource>
                                    {
                                        ApiSampleData.PersonResource1,
                                        ApiSampleData.PersonResource2,
                                        ApiSampleData.CommentResource1,
                                        ApiSampleData.CommentResource2,
                                        ApiSampleData.CommentResource3,
                                        ApiSampleData.CommentResource4
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(new []{ SampleArticles.Article1, SampleArticles.Article2 })
                                        .SetMeta(ApiSampleData.ResourceMeta)
                                        .Relationships()
                                            .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                            .AddLink(Keywords.Canonical)
                                        .LinksEnd()
                                    .ResourceCollectionEnd()
                                    .Included()
                                        .Include(ToOneIncludedResource.Create(SampleArticles.Article1, ApiSampleData.ArticleToAuthorRel, SamplePersons.Person1),
                                                 ToOneIncludedResource.Create(SampleArticles.Article2, ApiSampleData.ArticleToAuthorRel, SamplePersons.Person2))
                                            .SetMeta(ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                        .Include(ToManyIncludedResources.Create(SampleArticles.Article1, ApiSampleData.ArticleToCommentsRel, new []{ SampleComments.Comment1, SampleComments.Comment2 }),
                                                 ToManyIncludedResources.Create(SampleArticles.Article2, ApiSampleData.ArticleToCommentsRel, new []{ SampleComments.Comment3, SampleComments.Comment4 }))
                                            .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2, ApiSampleData.ResourceMeta3, ApiSampleData.ResourceMeta4)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                    .IncludedEnd())
                    },

                new object[]
                    {
                        "WithArticleResourceCollectionAndIncludedResourcesIncludingMoreResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleCollectionHRef,
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>
                                    {
                                        ApiSampleData.ArticleResourceWithResourceLinkage1,
                                        ApiSampleData.ArticleResourceWithResourceLinkage2
                                    },
                                Included = new List<Resource>
                                    {
                                        ApiSampleData.PersonResource1,
                                        ApiSampleData.PersonResource2,
                                        ApiSampleData.PersonResource3,
                                        ApiSampleData.PersonResource4,
                                        ApiSampleData.CommentResourceWithResourceLinkage1,
                                        ApiSampleData.CommentResourceWithResourceLinkage2,
                                        ApiSampleData.CommentResourceWithResourceLinkage3,
                                        ApiSampleData.CommentResourceWithResourceLinkage4
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(new []{ SampleArticles.Article1, SampleArticles.Article2 })
                                        .SetMeta(ApiSampleData.ResourceMeta)
                                        .Relationships()
                                            .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                            .AddLink(Keywords.Canonical)
                                        .LinksEnd()
                                    .ResourceCollectionEnd()
                                    .Included()
                                        .Include(ToOneIncludedResource.Create(SampleArticles.Article1, ApiSampleData.ArticleToAuthorRel, SamplePersons.Person1),
                                                 ToOneIncludedResource.Create(SampleArticles.Article2, ApiSampleData.ArticleToAuthorRel, SamplePersons.Person2))
                                            .SetMeta(ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                        .Include(ToManyIncludedResources.Create(SampleArticles.Article1, ApiSampleData.ArticleToCommentsRel, new []{ SampleComments.Comment1, SampleComments.Comment2 }),
                                                 ToManyIncludedResources.Create(SampleArticles.Article2, ApiSampleData.ArticleToCommentsRel, new []{ SampleComments.Comment3, SampleComments.Comment4 }))
                                            .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2, ApiSampleData.ResourceMeta3, ApiSampleData.ResourceMeta4)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                        .Include(ToOneIncludedResource.Create(SampleComments.Comment1, ApiSampleData.CommentToAuthorRel, SamplePersons.Person1),
                                                 ToOneIncludedResource.Create(SampleComments.Comment2, ApiSampleData.CommentToAuthorRel, SamplePersons.Person2),
                                                 ToOneIncludedResource.Create(SampleComments.Comment3, ApiSampleData.CommentToAuthorRel, SamplePersons.Person3),
                                                 ToOneIncludedResource.Create(SampleComments.Comment4, ApiSampleData.CommentToAuthorRel, SamplePersons.Person4))
                                            .SetMeta(ApiSampleData.ResourceMeta)
                                            .Relationships()
                                                .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                            .RelationshipsEnd()
                                            .Links()
                                                .AddLink(Keywords.Self)
                                            .LinksEnd()
                                        .IncludeEnd()
                                    .IncludedEnd())
                    },
            };
        #endregion

        #region TestDocumentContextResourceIdentifierDocumentBuildingTestData
        public static readonly IEnumerable<object[]> TestDocumentContextResourceIdentifierDocumentBuildingTestData = new[]
            {
                new object[]
                    {
                        "WithNullPersonResourceIdentifier",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToAuthorHRef,
                        new ResourceIdentifierDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink}
                                    },
                                Data = null
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifier<Person>()
                                        .SetId(Id.Create(default(string)))
                                    .ResourceIdentifierEnd())
                    },

                new object[]
                    {
                        "WithNullPersonResource",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToAuthorHRef,
                        new ResourceIdentifierDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink}
                                    },
                                Data = null
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifier(default(Person))
                                    .ResourceIdentifierEnd())
                    },

                new object[]
                    {
                        "WithEmptyPersonResource",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToAuthorHRef,
                        new ResourceIdentifierDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink}
                                    },
                                Data = null
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifier(new Person())
                                    .ResourceIdentifierEnd())
                    },

                new object[]
                    {
                        "WithPersonResourceIdentifier",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToAuthorHRef,
                        new ResourceIdentifierDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink}
                                    },
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifier<Person>()
                                        .SetId(Id.Create(ApiSampleData.PersonId))
                                    .ResourceIdentifierEnd())
                    },

                new object[]
                    {
                        "WithPersonResource",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToAuthorHRef,
                        new ResourceIdentifierDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink}
                                    },
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifier(SamplePersons.Person)
                                    .ResourceIdentifierEnd())
                    },
            };
        #endregion

        #region TestDocumentContextResourceIdentifierCollectionDocumentBuildingTestData
        public static readonly IEnumerable<object[]> TestDocumentContextResourceIdentifierCollectionDocumentBuildingTestData = new[]
            {
                new object[]
                    {
                        "WithNullCommentResourceIdentifiers",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToCommentsHRef,
                        new ResourceIdentifierCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink}
                                    },
                                Data = new List<ResourceIdentifier>()
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifierCollection<Comment>()
                                        .SetId(IdCollection.Create(default(IEnumerable<string>)))
                                    .ResourceIdentifierCollectionEnd())
                    },

                new object[]
                    {
                        "WithNullCommentResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToCommentsHRef,
                        new ResourceIdentifierCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink}
                                    },
                                Data = new List<ResourceIdentifier>()
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifierCollection(default(IEnumerable<Comment>))
                                    .ResourceIdentifierCollectionEnd())
                    },

                new object[]
                    {
                        "WithEmptyCommentResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToCommentsHRef,
                        new ResourceIdentifierCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink}
                                    },
                                Data = new List<ResourceIdentifier>()
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifierCollection(Enumerable.Empty<Comment>())
                                    .ResourceIdentifierCollectionEnd())
                    },

                new object[]
                    {
                        "WithCommentResourceIdentifiers",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToCommentsHRef,
                        new ResourceIdentifierCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink}
                                    },
                                Data = new List<ResourceIdentifier>
                                    {
                                        ApiSampleData.CommentResourceIdentifier1,
                                        ApiSampleData.CommentResourceIdentifier2
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifierCollection<Comment>()
                                        .SetId(IdCollection.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2 }))
                                    .ResourceIdentifierCollectionEnd())
                    },

                new object[]
                    {
                        "WithCommentResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleToRelationshipsToCommentsHRef,
                        new ResourceIdentifierCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink}
                                    },
                                Data = new List<ResourceIdentifier>
                                    {
                                        ApiSampleData.CommentResourceIdentifier1,
                                        ApiSampleData.CommentResourceIdentifier2
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceIdentifierCollection(new []{ SampleComments.Comment1, SampleComments.Comment2 })
                                    .ResourceIdentifierCollectionEnd())
                    },
            };
        #endregion

        #region TestDocumentContextHypermediaPredicateBuildingTestData
        public static readonly IEnumerable<object[]> TestDocumentContextHypermediaPredicateBuildingTestData = new[]
            {
                new object[]
                    {
                        "WithPaymentResourceCollectionAndNoRelationshipsSupressed",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        "http://api.example.com/payments",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/payments"}
                                    },
                                Data = new List<Resource>
                                    {
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "101",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 75.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links 
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/payments/101/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/payments/101/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/payments/101"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "102",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 25.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links 
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/payments/102/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/payments/102/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/payments/102"},
                                                    },
                                            }
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(new []{ SamplePayments.Payment101, SamplePayments.Payment102 })
                                        .Relationships()
                                            .Relationship(ClrSampleData.PaymentToOrderRel, payment => payment.Amount > 0.0m)
                                                .Links()
                                                    .AddLink(Keywords.Self)
                                                    .AddLink(Keywords.Related)
                                                .LinksEnd()
                                            .RelationshipEnd()
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                        .LinksEnd()
                                    .ResourceCollectionEnd())
                    },

                new object[]
                    {
                        "WithPaymentResourceCollectionAndSomeRelationshipsSupressed",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        "http://api.example.com/payments",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/payments"}
                                    },
                                Data = new List<Resource>
                                    {
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "101",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 75.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links 
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/payments/101/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/payments/101/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/payments/101"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "102",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 25.0m)),
                                                Relationships = new Relationships(),
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/payments/102"},
                                                    },
                                            }
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(new []{ SamplePayments.Payment101, SamplePayments.Payment102 })
                                        .Relationships()
                                            .Relationship(ClrSampleData.PaymentToOrderRel, payment => payment.Amount > 50.0m)
                                                .Links()
                                                    .AddLink(Keywords.Self)
                                                    .AddLink(Keywords.Related)
                                                .LinksEnd()
                                            .RelationshipEnd()
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                        .LinksEnd()
                                    .ResourceCollectionEnd())
                    },

                new object[]
                    {
                        "WithPaymentResourceCollectionAndAllRelationshipsSupressed",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        "http://api.example.com/payments",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/payments"}
                                    },
                                Data = new List<Resource>
                                    {
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "101",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 75.0m)),
                                                Relationships = new Relationships(),
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/payments/101"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "102",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 25.0m)),
                                                Relationships = new Relationships(),
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/payments/102"},
                                                    },
                                            }
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(new []{ SamplePayments.Payment101, SamplePayments.Payment102 })
                                        .Relationships()
                                            .Relationship(ClrSampleData.PaymentToOrderRel, payment => payment.Amount > 100.0m)
                                                .Links()
                                                    .AddLink(Keywords.Self)
                                                    .AddLink(Keywords.Related)
                                                .LinksEnd()
                                            .RelationshipEnd()
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self)
                                        .LinksEnd()
                                    .ResourceCollectionEnd())
                    },

                new object[]
                    {
                        "WithPaymentResourceCollectionAndNoLinksSupressed",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        "http://api.example.com/payments",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/payments"}
                                    },
                                Data = new List<Resource>
                                    {
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "101",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 75.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links 
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/payments/101/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/payments/101/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/payments/101"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "102",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 25.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links 
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/payments/102/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/payments/102/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/payments/102"},
                                                    },
                                            }
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(new []{ SamplePayments.Payment101, SamplePayments.Payment102 })
                                        .Relationships()
                                            .Relationship(ClrSampleData.PaymentToOrderRel)
                                                .Links()
                                                    .AddLink(Keywords.Self)
                                                    .AddLink(Keywords.Related)
                                                .LinksEnd()
                                            .RelationshipEnd()
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self, payment => payment.Amount > 0.0m)
                                        .LinksEnd()
                                    .ResourceCollectionEnd())
                    },

                new object[]
                    {
                        "WithPaymentResourceCollectionAndSomeLinksSupressed",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        "http://api.example.com/payments",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/payments"}
                                    },
                                Data = new List<Resource>
                                    {
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "101",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 75.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links 
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/payments/101/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/payments/101/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/payments/101"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "102",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 25.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links 
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/payments/102/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/payments/102/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links(),
                                            }
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(new []{ SamplePayments.Payment101, SamplePayments.Payment102 })
                                        .Relationships()
                                            .Relationship(ClrSampleData.PaymentToOrderRel)
                                                .Links()
                                                    .AddLink(Keywords.Self)
                                                    .AddLink(Keywords.Related)
                                                .LinksEnd()
                                            .RelationshipEnd()
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self, payment => payment.Amount > 50.0m)
                                        .LinksEnd()
                                    .ResourceCollectionEnd())
                    },

                new object[]
                    {
                        "WithPaymentResourceCollectionAndAllLinksSupressed",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithOrderResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        "http://api.example.com/payments",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/payments"}
                                    },
                                Data = new List<Resource>
                                    {
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "101",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 75.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links 
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/payments/101/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/payments/101/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links(),
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "102",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 25.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links 
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/payments/102/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/payments/102/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links(),
                                            }
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .ResourceCollection(new []{ SamplePayments.Payment101, SamplePayments.Payment102 })
                                        .Relationships()
                                            .Relationship(ClrSampleData.PaymentToOrderRel)
                                                .Links()
                                                    .AddLink(Keywords.Self)
                                                    .AddLink(Keywords.Related)
                                                .LinksEnd()
                                            .RelationshipEnd()
                                        .RelationshipsEnd()
                                        .Links()
                                            .AddLink(Keywords.Self, payment => payment.Amount > 100.0m)
                                        .LinksEnd()
                                    .ResourceCollectionEnd())
                    },
            };
        #endregion

        #region TestDocumentContextErrorsDocumentBuildingTestData
        public static readonly IEnumerable<object[]> TestDocumentContextErrorsDocumentBuildingTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyErrors",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleCollectionHRef,
                        new ErrorsDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Errors = new List<Error>()
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .Errors()
                                    .ErrorsEnd())
                    },

                new object[]
                    {
                        "WithReadOnlyErrors",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes, UrlBuilderConfigurationWithoutRootPathSegments),
                        ApiSampleData.ArticleCollectionHRef,
                        new ErrorsDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Errors = new List<Error>
                                    {
                                        ApiSampleData.Error1,
                                        ApiSampleData.Error2
                                    }
                            },
                        new Func<DocumentContext, string, IDocumentWriter>(
                            (documentContext, currentRequestUrl) => documentContext
                                .NewDocument(currentRequestUrl)
                                    .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                    .Errors()
                                        .AddError(ApiSampleData.Error1)
                                        .AddError(ApiSampleData.Error2)
                                    .ErrorsEnd())
                    },
            };
        #endregion

        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IDocumentContextOptions CreateDocumentContextOptions(IServiceModel serviceModel, IUrlBuilderConfiguration urlBuilderConfiguration, IHypermediaAssemblerRegistry hypermediaAssemblerRegistry = default(IHypermediaAssemblerRegistry))
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(urlBuilderConfiguration != null);

            var options = new DocumentContextOptions<DocumentContext>();
            var optionsBuilder = new DocumentContextOptionsBuilder(options);

            optionsBuilder.UseServiceModel(serviceModel);
            optionsBuilder.UseUrlBuilderConfiguration(urlBuilderConfiguration);
            optionsBuilder.UseHypermediaAssemblerRegistry(hypermediaAssemblerRegistry);

            return options;
        }

        private void TestDocumentContextBuilding(string name, IDocumentContextOptions documentContextOptions, string currentUrlRequest, Document expectedApiDocument, Func<DocumentContext, string, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.OutputEmptyLine();

            // Arrange
            var documentContext = new DocumentContext(documentContextOptions);

            // Act
            var actualApiDocumentWriter = actualApiDocumentNewDocumentFunctor(documentContext, currentUrlRequest);

            this.Output.WriteLine("Before WriteDocument Method DOM Tree");
            this.OutputEmptyLine();
            this.Output.WriteLine(documentContext.ToDomTreeString());
            this.OutputEmptyLine();

            var actualApiDocument = actualApiDocumentWriter.WriteDocument();

            this.Output.WriteLine("After WriteDocument Method DOM Tree");
            this.OutputEmptyLine();
            this.Output.WriteLine(documentContext.ToDomTreeString());
            this.OutputEmptyLine();

            this.OutputJson("Expected Document JSON", expectedApiDocument);
            this.OutputEmptyLine();

            this.OutputJson("Actual Document JSON", actualApiDocument);

            // Assert
            DocumentAssert.Equal(expectedApiDocument, actualApiDocument);
        }

        private void OutputEmptyLine()
        {
            this.Output.WriteLine(String.Empty);
        }

        private void OutputJson(string header, IJsonObject jsonObject)
        {
            var json = jsonObject.ToJson();

            this.Output.WriteLine(header);
            this.Output.WriteLine(String.Empty);
            this.Output.WriteLine(json);
        }
        #endregion
    }
}
