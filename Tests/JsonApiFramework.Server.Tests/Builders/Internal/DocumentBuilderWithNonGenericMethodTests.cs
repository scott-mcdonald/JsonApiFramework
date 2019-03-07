// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using JsonApiFramework.Http;
using JsonApiFramework.Internal;
using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Server.Hypermedia;
using JsonApiFramework.Server.Hypermedia.Internal;
using JsonApiFramework.Server.Internal;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Server.Tests.Internal
{
    using Attribute  = ApiProperty;
    using Attributes = ApiObject;

    public class DocumentBuilderWithNonGenericMethodTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentBuilderWithNonGenericMethodTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DocumentBuilderWriteDocumentTestData))]
        public void TestDocumentBuilderWriteDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(DocumentBuilderWriteEmptyDocumentTestData))]
        public void TestDocumentBuilderWriteEmptyDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(DocumentBuilderWriteNullDocumentTestData))]
        public void TestDocumentBuilderWriteNullDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(DocumentBuilderWriteResourceCollectionDocumentTestData))]
        public void TestDocumentBuilderWriteResourceCollectionDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(DocumentBuilderWriteResourceDocumentTestData))]
        public void TestDocumentBuilderWriteResourceDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(DocumentBuilderWriteResourceIdentifierCollectionDocumentTestData))]
        public void TestDocumentBuilderWriteResourceIdentifierCollectionDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(DocumentBuilderWriteResourceIdentifierDocumentTestData))]
        public void TestDocumentBuilderWriteResourceIdentifierDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(DocumentBuilderWriteWithFrameworkHypermediaTestData))]
        public void TestDocumentBuilderWriteWithFrameworkHypermedia(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(DocumentBuilderWriteErrorsDocumentTestData))]
        public void TestDocumentBuilderWriteErrorsDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
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

        private void TestDocumentBuilderWrite(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        {
            // Arrange

            // Act
            var hasDomDocument = (IGetDomDocument)actualDocumentWriter;

            this.Output.WriteLine("Test Name: {0}", name);
            this.OutputEmptyLine();
            this.Output.WriteLine("Before WriteDocument Method DOM Tree");
            this.OutputEmptyLine();
            this.Output.WriteLine(hasDomDocument.DomDocument.ToTreeString());
            this.OutputEmptyLine();

            var actualDocument = actualDocumentWriter.WriteDocument();

            this.Output.WriteLine("After WriteDocument Method DOM Tree");
            this.OutputEmptyLine();
            this.Output.WriteLine(hasDomDocument.DomDocument.ToTreeString());
            this.OutputEmptyLine();

            this.OutputJson("Expected Document JSON", expectedDocument);
            this.OutputEmptyLine();

            this.OutputJson("Actual Document JSON", actualDocument);

            // Assert
            DocumentAssert.Equal(expectedDocument, actualDocument);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly UrlBuilderConfiguration UrlBuilderConfigurationWithRootPathSegments = new UrlBuilderConfiguration
            {
                Scheme = "http",
                Host = "api.example.com",
                RootPathSegments = new[]
                    {
                        "api",
                        "v2"
                    }
            };

        public static readonly UrlBuilderConfiguration UrlBuilderConfigurationWithoutRootPathSegments = new UrlBuilderConfiguration
            {
                Scheme = "http",
                Host = "api.example.com"
            };

        public static readonly IHypermediaAssemblerRegistry HypermediaAssemblerRegistry = default(IHypermediaAssemblerRegistry);

        private static class DocumentBuilderFactory
        {
            public static IDocumentBuilder Create(IServiceModel serviceModel, IHypermediaAssemblerRegistry hypermediaAssemblerRegistry, IUrlBuilderConfiguration urlBuilderConfiguration, string currentRequestUrl)
            {
                var documentWriter         = new DocumentWriter(serviceModel);
                var hypermediaContext      = new HypermediaContext(serviceModel, urlBuilderConfiguration, null);
                var documentBuilderContext = new DocumentBuilderContext(currentRequestUrl, QueryParameters.Empty, false);
                var documentBuilder        = new DocumentBuilder(documentWriter, hypermediaAssemblerRegistry, hypermediaContext, documentBuilderContext);
                return documentBuilder;
            }
        }

        #region DocumentBuilderWriteDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithNothing",
                        Document.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com")
                    },
                new object[]
                    {
                        "WithMeta",
                        new Document
                            {
                                Meta = ApiSampleData.DocumentMeta
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com")
                            .SetMeta(ApiSampleData.DocumentMeta)
                    },
                new object[]
                    {
                        "WithMetaAndLinks",
                        new Document
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com")
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddSelfLink(ApiSampleData.ArticleLink)
                            .LinksEnd()
                    },
                new object[]
                    {
                        "WithJsonApiAndMetaAndLinks",
                        new Document
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com")
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddSelfLink(ApiSampleData.ArticleLink)
                            .LinksEnd()
                    },
            };
        #endregion

        #region DocumentBuilderWriteEmptyDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteEmptyDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyResources",
                        EmptyDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/articles")
                            .ResourceCollection(Enumerable.Empty<object>())
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithEmptyResourcesAndJsonApiAndMetaAndLinks",
                        new EmptyDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/articles")
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self, ApiSampleData.ArticleCollectionLink)
                            .LinksEnd()
                            .ResourceCollection(Enumerable.Empty<object>())
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithEmptyResourcesAndUserBuiltHypermedia",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self, ApiSampleData.ArticleCollectionLink)
                            .LinksEnd()
                            .ResourceCollection(Enumerable.Empty<object>())
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { ApiSampleData.ArticleToAuthorRelationship1, ApiSampleData.ArticleToAuthorRelationship2 })
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { ApiSampleData.ArticleToCommentsRelationship1, ApiSampleData.ArticleToCommentsRelationship2 })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self, ApiSampleData.ArticleLink1, ApiSampleData.ArticleLink2)
                                    .AddLink(Keywords.Canonical, ApiSampleData.ArticleLink1, ApiSampleData.ArticleLink2)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithEmptyResourcesAndFrameworkBuiltHypermedia",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(Enumerable.Empty<object>())
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
                    },
            };
        #endregion

        #region DocumentBuilderWriteNullDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteNullDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithNullResource",
                        NullDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com")
                            .Resource(default(object))
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithNullResourceAndJsonApiAndMetaAndLinks",
                        new NullDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com")
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self, ApiSampleData.ArticleLink)
                            .LinksEnd()
                            .Resource(default(object))
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithNullResourceAndUserBuiltHypermedia",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = null
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self, ApiSampleData.ArticleLink)
                            .LinksEnd()
                            .Resource(default(object))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship)
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship)
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self, ApiSampleData.ArticleLink)
                                    .AddLink(Keywords.Canonical, ApiSampleData.ArticleLink)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithNullResourceAndFrameworkBuiltHypermedia",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = null
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource(default(object))
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
                    },
            };
        #endregion

        #region DocumentBuilderWriteResourceCollectionDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteResourceCollectionDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithResourcesAndUserBuiltHypermedia",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self, ApiSampleData.ArticleCollectionLink)
                            .LinksEnd()
                            .ResourceCollection(new object[] { SampleArticles.Article1, SampleArticles.Article2 })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, new []{ ApiSampleData.ArticleToAuthorRelationship1, ApiSampleData.ArticleToAuthorRelationship2 })
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, new []{ ApiSampleData.ArticleToCommentsRelationship1, ApiSampleData.ArticleToCommentsRelationship2 })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self, ApiSampleData.ArticleLink1, ApiSampleData.ArticleLink2)
                                    .AddLink(Keywords.Canonical, ApiSampleData.ArticleLink1, ApiSampleData.ArticleLink2)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithResourcesAndFrameworkBuiltHypermedia",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object[] { SampleArticles.Article1, SampleArticles.Article2 })
                                .SetMeta(ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                    .AddLink(Keywords.Canonical)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithResourcesAndIncludedResourcesAndUserBuiltHypermedia",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object[] { SampleArticles.Article1, SampleArticles.Article2 })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, new [] { ApiSampleData.ArticleToAuthorToOneRelationship1, ApiSampleData.ArticleToAuthorToOneRelationship2 })
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, new [] { ApiSampleData.ArticleToCommentsToManyRelationship1, ApiSampleData.ArticleToCommentsToManyRelationship2 })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self, ApiSampleData.ArticleLink1, ApiSampleData.ArticleLink2)
                                    .AddLink(Keywords.Canonical, ApiSampleData.ArticleLink1, ApiSampleData.ArticleLink2)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                            .Included()
                                .Include(ToOneIncludedResource.Create((object)SampleArticles.Article1, ApiSampleData.ArticleToAuthorRel, typeof(Person), (object)SamplePersons.Person1),
                                         ToOneIncludedResource.Create((object)SampleArticles.Article2, ApiSampleData.ArticleToAuthorRel, typeof(Person), (object)SamplePersons.Person2))
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { ApiSampleData.PersonToCommentsRelationship1, ApiSampleData.PersonToCommentsRelationship2 })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self, ApiSampleData.PersonLink1, ApiSampleData.PersonLink2)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToManyIncludedResources.Create((object)SampleArticles.Article1, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new object []{ SampleComments.Comment1, SampleComments.Comment2 }),
                                         ToManyIncludedResources.Create((object)SampleArticles.Article2, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new object []{ SampleComments.Comment3, SampleComments.Comment4 }))
                                    .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2, ApiSampleData.ResourceMeta3, ApiSampleData.ResourceMeta4)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.CommentToAuthorRel, new []{ ApiSampleData.CommentToAuthorRelationship1, ApiSampleData.CommentToAuthorRelationship2, ApiSampleData.CommentToAuthorRelationship3, ApiSampleData.CommentToAuthorRelationship4 })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self, ApiSampleData.CommentLink1, ApiSampleData.CommentLink2, ApiSampleData.CommentLink3, ApiSampleData.CommentLink4)
                                    .LinksEnd()
                                .IncludeEnd()
                            .IncludedEnd()
                    },
                new object[]
                    {
                        "WithResourcesAndIncludedResourcesAndFrameworkBuiltHypermedia",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object[] { SampleArticles.Article1, SampleArticles.Article2 })
                                .SetMeta(ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta)
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
                                .Include(ToOneIncludedResource.Create((object)SampleArticles.Article1, ApiSampleData.ArticleToAuthorRel, typeof(Person), (object)SamplePersons.Person1),
                                         ToOneIncludedResource.Create((object)SampleArticles.Article2, ApiSampleData.ArticleToAuthorRel, typeof(Person), (object)SamplePersons.Person2))
                                    .SetMeta(ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToManyIncludedResources.Create((object)SampleArticles.Article1, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new object []{ SampleComments.Comment1, SampleComments.Comment2 }),
                                         ToManyIncludedResources.Create((object)SampleArticles.Article2, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new object []{ SampleComments.Comment3, SampleComments.Comment4 }))
                                    .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2, ApiSampleData.ResourceMeta3, ApiSampleData.ResourceMeta4)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                            .IncludedEnd()
                    },

                new object[]
                    {
                        "WithResourcesAndIncludedResourceLinkageAndFrameworkBuiltHypermedia",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>
                                    {
                                        ApiSampleData.ArticleResourceWithResourceLinkage1,
                                        ApiSampleData.ArticleResourceWithResourceLinkage2
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object[] { SampleArticles.Article1, SampleArticles.Article2 })
                                .SetMeta(ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .AddRelationship(rel: ApiSampleData.ArticleToAuthorRel, linkRelCollection: new []{ Keywords.Self, Keywords.Related}, toOneResourceLinkageCollection: new [] { ToOneResourceLinkage.Create(ApiSampleData.PersonId1), ToOneResourceLinkage.Create(ApiSampleData.PersonId2) })
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .Links()
                                            .AddSelfLink()
                                            .AddRelatedLink()
                                        .LinksEnd()
                                        .SetData(new [] { ToManyResourceLinkage.Create(new[] { ApiSampleData.CommentId1, ApiSampleData.CommentId2 }), ToManyResourceLinkage.Create(new[] { ApiSampleData.CommentId3, ApiSampleData.CommentId4 }) })
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                    .AddLink(Keywords.Canonical)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },

                new object[]
                    {
                        "WithResourcesAndIncludedNullAndEmptyResourceLinkageAndFrameworkBuiltHypermedia",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>
                                    {
                                        ApiSampleData.ArticleResourceWithNullAndEmptyResourceLinkage1,
                                        ApiSampleData.ArticleResourceWithNullAndEmptyResourceLinkage2
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object[] { SampleArticles.Article1, SampleArticles.Article2 })
                                .SetMeta(ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .Links()
                                            .AddSelfLink()
                                            .AddRelatedLink()
                                        .LinksEnd()
                                        .SetData(ToOneResourceLinkage.CreateNull())
                                    .RelationshipEnd()
                                    .AddRelationship(rel: ApiSampleData.ArticleToCommentsRel, linkRelCollection: new []{ Keywords.Self, Keywords.Related}, toManyResourceLinkage: ToManyResourceLinkage.CreateEmpty())
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                    .AddLink(Keywords.Canonical)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },

                new object[]
                    {
                        "WithResourcesAndIncludedDuplicateResourcesAndFrameworkBuiltHypermedia",
                        new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>
                                    {
                                        ApiSampleData.ArticleResourceWithResourceLinkage1,
                                        new Resource
                                            {
                                                Type = ApiSampleData.ArticleType,
                                                Id = ApiSampleData.ArticleId2,
                                                Attributes = new Attributes(Attribute.Create("title", "JSON API paints my house!")),
                                                Relationships = new Relationships
                                                    {
                                                        {ApiSampleData.ArticleToAuthorRel,
                                                            new ToOneRelationship
                                                                {
                                                                    Links = new Links
                                                                            {
                                                                                {Keywords.Self, new Link(UrlBuilder.Create(ApiSampleData.UrlBuilderConfiguration).Path(ApiSampleData.ArticleCollectionPathSegment).Path(ApiSampleData.ArticleId2).Path(Keywords.Relationships).Path(ApiSampleData.ArticleToAuthorRel).Build())},
                                                                                {Keywords.Related, new Link(UrlBuilder.Create(ApiSampleData.UrlBuilderConfiguration).Path(ApiSampleData.ArticleCollectionPathSegment).Path(ApiSampleData.ArticleId2).Path(ApiSampleData.ArticleToAuthorRel).Build())}
                                                                            },
                                                                    Data = ApiSampleData.PersonResourceIdentifier1
                                                                }},
                                                        {ApiSampleData.ArticleToCommentsRel,
                                                            new ToManyRelationship
                                                                {
                                                                    Links = new Links
                                                                            {
                                                                                {Keywords.Self, new Link(UrlBuilder.Create(ApiSampleData.UrlBuilderConfiguration).Path(ApiSampleData.ArticleCollectionPathSegment).Path(ApiSampleData.ArticleId2).Path(Keywords.Relationships).Path(ApiSampleData.CommentCollectionPathSegment).Build())},
                                                                                {Keywords.Related, new Link(UrlBuilder.Create(ApiSampleData.UrlBuilderConfiguration).Path(ApiSampleData.ArticleCollectionPathSegment).Path(ApiSampleData.ArticleId2).Path(ApiSampleData.CommentCollectionPathSegment).Build())}
                                                                            },
                                                                    Data = ApiSampleData.ArticleToCommentResourceIdentifiers1
                                                                }}
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, ApiSampleData.ArticleLink2},
                                                        {Keywords.Canonical, ApiSampleData.ArticleLink2}
                                                    },
                                                Meta = ApiSampleData.ResourceMeta
                                            }
                                    },
                                Included = new List<Resource>
                                    {
                                        ApiSampleData.PersonResource1,
                                        ApiSampleData.CommentResource1,
                                        ApiSampleData.CommentResource2,
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object[]{ SampleArticles.Article1, SampleArticles.Article2 })
                                .SetMeta(ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta)
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
                                .Include(ToOneIncludedResource.Create((object)SampleArticles.Article1, ApiSampleData.ArticleToAuthorRel, typeof(Person), (object)SamplePersons.Person1),
                                         ToOneIncludedResource.Create((object)SampleArticles.Article2, ApiSampleData.ArticleToAuthorRel, typeof(Person), (object)SamplePersons.Person1))
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToManyIncludedResources.Create((object)SampleArticles.Article1, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new object []{ SampleComments.Comment1 }),
                                         ToManyIncludedResources.Create((object)SampleArticles.Article2, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new object []{ SampleComments.Comment1 }))
                                    .SetMeta(ApiSampleData.ResourceMeta1)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToManyIncludedResources.Create((object)SampleArticles.Article1, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new object []{ SampleComments.Comment2 }),
                                         ToManyIncludedResources.Create((object)SampleArticles.Article2, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new object []{ SampleComments.Comment2 }))
                                    .SetMeta(ApiSampleData.ResourceMeta2)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                            .IncludedEnd()
                    },
            };
        #endregion

        #region DocumentBuilderWriteResourceDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteResourceDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithResourceAndUserBuiltHypermedia",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = ApiSampleData.ArticleResource
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self, ApiSampleData.ArticleLink)
                            .LinksEnd()
                            .Resource((object)SampleArticles.Article)
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorRelationship)
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsRelationship)
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self, ApiSampleData.ArticleLink)
                                    .AddLink(Keywords.Canonical, ApiSampleData.ArticleLink)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithResourceAndFrameworkBuiltHypermedia",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = ApiSampleData.ArticleResource
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleArticles.Article)
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
                    },
                new object[]
                    {
                        "WithResourceAndIncludedResourcesAndUserBuiltHypermedia",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self, ApiSampleData.ArticleLink)
                            .LinksEnd()
                            .Resource((object)SampleArticles.Article)
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship)
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ApiSampleData.ArticleToCommentsToManyRelationship)
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self, ApiSampleData.ArticleLink)
                                    .AddLink(Keywords.Canonical, ApiSampleData.ArticleLink)
                                .LinksEnd()
                            .ResourceEnd()
                            .Included()
                                .Include(ToOneIncludedResource.Create((object)SampleArticles.Article, ApiSampleData.ArticleToAuthorRel, typeof(Person), (object)SamplePersons.Person))
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.PersonToCommentsRel, ApiSampleData.PersonToCommentsRelationship)
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self, ApiSampleData.PersonLink)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToManyIncludedResources.Create((object)SampleArticles.Article, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new []{ SampleComments.Comment1, SampleComments.Comment2 }))
                                    .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { ApiSampleData.CommentToAuthorRelationship1, ApiSampleData.CommentToAuthorRelationship2 })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self, ApiSampleData.CommentLink1, ApiSampleData.CommentLink2)
                                    .LinksEnd()
                                .IncludeEnd()
                            .IncludedEnd()
                    },
                new object[]
                    {
                        "WithResourceAndIncludedResourcesAndFrameworkBuiltHypermedia",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleArticles.Article)
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
                                .Include(ToOneIncludedResource.Create((object)SampleArticles.Article, ApiSampleData.ArticleToAuthorRel, typeof(Person), SamplePersons.Person))
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToManyIncludedResources.Create((object)SampleArticles.Article, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new []{ SampleComments.Comment1, SampleComments.Comment2 }))
                                    .SetMeta(ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                            .IncludedEnd()
                    },
                new object[]
                    {
                        "WithResourceAndIncludedResourceLinkageAndFrameworkBuiltHypermedia",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = ApiSampleData.ArticleResourceWithResourceLinkage
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleArticles.Article)
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .Links()
                                            .AddSelfLink()
                                            .AddRelatedLink()
                                        .LinksEnd()
                                        .SetData(ToOneResourceLinkage.Create(ApiSampleData.PersonId))
                                    .RelationshipEnd()
                                    .AddRelationship(rel: ApiSampleData.ArticleToCommentsRel, linkRelCollection: new []{ Keywords.Self, Keywords.Related }, toManyResourceLinkage: ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2 }))
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                    .AddLink(Keywords.Canonical)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithResourceAndIncludedNullAndEmptyResourceLinkageAndFrameworkBuiltHypermedia",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = ApiSampleData.ArticleResourceWithNullAndEmptyResourceLinkage
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleArticles.Article)
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .AddRelationship(rel: ApiSampleData.ArticleToAuthorRel, linkRelCollection: new []{Keywords.Self, Keywords.Related}, toOneResourceLinkage: ToOneResourceLinkage.CreateNull())
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .Links()
                                            .AddSelfLink()
                                            .AddRelatedLink()
                                        .LinksEnd()
                                        .SetData(ToManyResourceLinkage.CreateEmpty())
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                    .AddLink(Keywords.Canonical)
                                .LinksEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "WithResourceAndIncludedDuplicateResourcesAndFrameworkBuiltHypermedia",
                        new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new Attributes(Attribute.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {ApiSampleData.ArticleToAuthorRel, ApiSampleData.ArticleToAuthorToOneRelationship},
                                                {ApiSampleData.ArticleToCommentsRel,
                                                    new ToManyRelationship
                                                        {
                                                            Links = new Links
                                                                    {
                                                                        {Keywords.Self, new Link(UrlBuilder.Create(ApiSampleData.UrlBuilderConfiguration).Path(ApiSampleData.ArticleCollectionPathSegment).Path(ApiSampleData.ArticleId).Path(Keywords.Relationships).Path(ApiSampleData.CommentCollectionPathSegment).Build())},
                                                                        {Keywords.Related, new Link(UrlBuilder.Create(ApiSampleData.UrlBuilderConfiguration).Path(ApiSampleData.ArticleCollectionPathSegment).Path(ApiSampleData.ArticleId).Path(ApiSampleData.CommentCollectionPathSegment).Build())}
                                                                    },
                                                            Data = new List<ResourceIdentifier> { ApiSampleData.CommentResourceIdentifier1 }
                                                        }}
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
                                        ApiSampleData.PersonResource,
                                        ApiSampleData.CommentResource1
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleArticles.Article)
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
                                .Include(ToOneIncludedResource.Create((object)SampleArticles.Article, ApiSampleData.ArticleToAuthorRel, typeof(Person), SamplePersons.Person))
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToOneIncludedResource.Create((object)SampleArticles.Article, ApiSampleData.ArticleToAuthorRel, typeof(Person), SamplePersons.Person))
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.PersonToCommentsRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToManyIncludedResources.Create((object)SampleArticles.Article, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new []{ SampleComments.Comment1 }))
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToManyIncludedResources.Create((object)SampleArticles.Article, ApiSampleData.ArticleToCommentsRel, typeof(Comment), new []{ SampleComments.Comment1 }))
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.CommentToAuthorRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                            .IncludedEnd()
                    },

                new object[]
                    {
                        "WithNestedComplexTypes",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, UrlBuilder.Create(UrlBuilderConfigurationWithoutRootPathSegments).Path(ClrSampleData.DrawingCollectionPathSegment).Path(SampleDrawings.Drawing.Id).Build()}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.DrawingType,
                                        Id = SampleDrawings.Drawing.Id.ToString(CultureInfo.InvariantCulture),
                                        Attributes = new ApiObject(
                                            ApiProperty.Create("name", SampleDrawings.Drawing.Name),
                                            ApiProperty.Create("lines", SampleDrawings.Drawing.Lines
                                                .Select(x =>
                                                    {
                                                        var point1CustomData = ApiProperty.Create("custom-data",
                                                            x.Point1.CustomData != null
                                                                ? new ApiObject(ApiProperty.Create("collection",
                                                                    x.Point1.CustomData.Collection.EmptyIfNull()
                                                                     .Select(y =>
                                                                         {
                                                                             var apiObject2 = new ApiObject(ApiProperty.Create("name", y.Name), ApiProperty.Create("value", y.Value));
                                                                             return apiObject2;
                                                                         })
                                                                     .ToArray()))
                                                                : null);
                                                        var point1 = ApiProperty.Create("point1", new ApiObject(ApiProperty.Create("x", x.Point1.X), ApiProperty.Create("y", x.Point1.Y), point1CustomData));

                                                        var point2CustomData = ApiProperty.Create("custom-data",
                                                            x.Point2.CustomData != null
                                                                ? new ApiObject(ApiProperty.Create("collection",
                                                                    x.Point2.CustomData.Collection.EmptyIfNull()
                                                                     .Select(y =>
                                                                         {
                                                                             var apiObject2 = new ApiObject(ApiProperty.Create("name", y.Name), ApiProperty.Create("value", y.Value));
                                                                             return apiObject2;
                                                                         })
                                                                     .ToArray()))
                                                                : null);
                                                        var point2 = ApiProperty.Create("point2", new ApiObject(ApiProperty.Create("x", x.Point2.X), ApiProperty.Create("y", x.Point2.Y), point2CustomData));

                                                        var customData = ApiProperty.Create("custom-data",
                                                            x.CustomData != null
                                                                ? new ApiObject(ApiProperty.Create("collection",
                                                                    x.CustomData.Collection.EmptyIfNull()
                                                                     .Select(y =>
                                                                     {
                                                                         var apiObject2 = new ApiObject(ApiProperty.Create("name", y.Name), ApiProperty.Create("value", y.Value));
                                                                         return apiObject2;
                                                                     })
                                                                     .ToArray()))
                                                                : null);
                                                        return new ApiObject(point1, point2, customData);
                                                    })
                                                .ToArray()),
                                            ApiProperty.Create("polygons", SampleDrawings.Drawing.Polygons
                                                .Select(x =>
                                                    {
                                                        var points = ApiProperty.Create("points",
                                                            x.Points.Select(y =>
                                                                {
                                                                    var pointCustomData = ApiProperty.Create("custom-data",
                                                                        y.CustomData != null
                                                                            ? new ApiObject(ApiProperty.Create("collection",
                                                                                y.CustomData.Collection.EmptyIfNull()
                                                                                 .Select(z =>
                                                                                     {
                                                                                         var apiObject3 = new ApiObject(ApiProperty.Create("name", z.Name), ApiProperty.Create("value", z.Value));
                                                                                         return apiObject3;
                                                                                     })
                                                                                 .ToArray()))
                                                                            : null);
                                                                    var apiObject2 = new ApiObject(ApiProperty.Create("x", y.X), ApiProperty.Create("y", y.Y), pointCustomData);
                                                                    return apiObject2;
                                                                })
                                                             .ToArray());
                                                        var customData = ApiProperty.Create("custom-data",
                                                            x.CustomData != null
                                                                ? new ApiObject(ApiProperty.Create("collection",
                                                                    x.CustomData.Collection.EmptyIfNull()
                                                                     .Select(y =>
                                                                     {
                                                                         var apiObject2 = new ApiObject(ApiProperty.Create("name", y.Name), ApiProperty.Create("value", y.Value));
                                                                         return apiObject2;
                                                                     })
                                                                     .ToArray()))
                                                                : null);
                                                        return new ApiObject(points, customData);
                                                    })
                                                .ToArray()),
                                            ApiProperty.Create("custom-data", SampleDrawings.Drawing.CustomData != null
                                                ? new ApiObject(ApiProperty.Create("collection", SampleDrawings.Drawing.CustomData.Collection.EmptyIfNull().Select(x =>
                                                            {
                                                                var apiObject = new ApiObject(ApiProperty.Create("name", x.Name), ApiProperty.Create("value", x.Value));
                                                                return apiObject;
                                                            })
                                                        .ToArray()))
                                                : null)
                                        ),
                                        Links = new Links
                                            {
                                                {Keywords.Self, UrlBuilder.Create(UrlBuilderConfigurationWithoutRootPathSegments).Path(ClrSampleData.DrawingCollectionPathSegment).Path(SampleDrawings.Drawing.Id).Build()}
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithDrawingResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, UrlBuilder.Create(UrlBuilderConfigurationWithoutRootPathSegments).Path(ClrSampleData.DrawingCollectionPathSegment).Path(SampleDrawings.Drawing.Id).Build())
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleDrawings.Drawing)
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                    },

                new object[]
                {
                    "WithSingletonObjectAndFrameworkBuiltHypermedia",
                    new ResourceDocument
                    {
                        JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                        Meta           = ApiSampleData.DocumentMeta,
                        Links = new Links
                                {
                                    {Keywords.Self, new Link("http://api.example.com")}
                                },
                        Data = new Resource
                               {
                                   Type       = ClrSampleData.HomeType,
                                   Attributes = new Attributes(Attribute.Create("message", "This is the home document singleton!")),
                                   Links      = new Links {{Keywords.Self, new Link("http://api.example.com")}}
                               }
                    },
                    DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com")
                        .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                        .SetMeta(ApiSampleData.DocumentMeta)
                        .Links()
                            .AddLink(Keywords.Self)
                        .LinksEnd()
                        .Resource((object)(new Home { Message = "This is the home document singleton!", }))
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                        .ResourceEnd()
                },
            };
        #endregion

        #region DocumentBuilderWriteResourceIdentifierCollectionDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteResourceIdentifierCollectionDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithNullResourceIdentifiers",
                        ResourceIdentifierCollectionDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetResourceIdentifierCollection(typeof(Article))
                    },
                new object[]
                    {
                        "WithNullResourceIdentifiersAndMeta",
                        ResourceIdentifierCollectionDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .ResourceIdentifierCollection(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierCollectionEnd()
                    },
                new object[]
                    {
                        "WithResourceIdentifiers",
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>
                                    {
                                        ApiSampleData.CommentResourceIdentifier1,
                                        ApiSampleData.CommentResourceIdentifier2
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToCommentsHRef)
                            .ResourceIdentifierCollection(typeof(Comment))
                                .SetId(IdCollection.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2 }))
                            .ResourceIdentifierCollectionEnd()
                    },
                new object[]
                    {
                        "WithResourceIdentifiersAndMeta",
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>
                                    {
                                        ApiSampleData.CommentResourceIdentifierWithMeta1,
                                        ApiSampleData.CommentResourceIdentifierWithMeta2
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToCommentsHRef)
                            .ResourceIdentifierCollection(typeof(Comment))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(IdCollection.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2 }))
                            .ResourceIdentifierCollectionEnd()
                    },
                new object[]
                    {
                        "WithNullResources",
                        ResourceIdentifierCollectionDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetResourceIdentifierCollection(default(IEnumerable<object>))
                    },
                new object[]
                    {
                        "WithNullResourcesAndMeta",
                        ResourceIdentifierCollectionDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .ResourceIdentifierCollection(default(IEnumerable<object>))
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierCollectionEnd()
                    },
                new object[]
                    {
                        "WithEmptyResources",
                        ResourceIdentifierCollectionDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetResourceIdentifierCollection(Enumerable.Empty<object>())
                    },
                new object[]
                    {
                        "WithEmptyResourcesAndMeta",
                        ResourceIdentifierCollectionDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .ResourceIdentifierCollection(Enumerable.Empty<object>())
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierCollectionEnd()
                    },
                new object[]
                    {
                        "WithResources",
                        new ResourceIdentifierCollectionDocument
                        {
                            Data = new List<ResourceIdentifier>
                            {
                                ApiSampleData.CommentResourceIdentifier1,
                                ApiSampleData.CommentResourceIdentifier2
                            }
                        },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetResourceIdentifierCollection(new object []{ SampleComments.Comment1, SampleComments.Comment2 })
                    },
                new object[]
                    {
                        "WithResourcesAndMeta",
                        new ResourceIdentifierCollectionDocument
                        {
                            Data = new List<ResourceIdentifier>
                            {
                                ApiSampleData.CommentResourceIdentifierWithMeta1,
                                ApiSampleData.CommentResourceIdentifierWithMeta2
                            }
                        },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .ResourceIdentifierCollection(new object []{ SampleComments.Comment1, SampleComments.Comment2 })
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierCollectionEnd()
                    },
            };
        #endregion

        #region DocumentBuilderWriteResourceIdentifierDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteResourceIdentifierDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithNullResourceIdentifier",
                        ResourceIdentifierDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .SetResourceIdentifier(typeof(Article))
                    },
                new object[]
                    {
                        "WithNullResourceIdentifierAndMeta",
                        ResourceIdentifierDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleHRef)
                            .ResourceIdentifier(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierEnd()
                    },
                new object[]
                    {
                        "WithResourceIdentifier",
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToAuthorHRef)
                            .ResourceIdentifier(typeof(Person))
                                .SetId(Id.Create(ApiSampleData.PersonId))
                            .ResourceIdentifierEnd()
                    },
                new object[]
                    {
                        "WithResourceIdentifierAndMeta",
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifierWithMeta
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToAuthorHRef)
                            .ResourceIdentifier(typeof(Person))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(Id.Create(ApiSampleData.PersonId))
                            .ResourceIdentifierEnd()
                    },
                new object[]
                    {
                        "WithNullResource",
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToAuthorHRef)
                            .SetResourceIdentifier(default(object))
                    },
                new object[]
                    {
                        "WithNullResourceAndMeta",
                        new ResourceIdentifierDocument
                        {
                            Data = null
                        },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToAuthorHRef)
                            .ResourceIdentifier(default(object))
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierEnd()
                    },
                new object[]
                    {
                        "WithEmptyResource",
                        new ResourceIdentifierDocument
                        {
                            Data = null
                        },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToAuthorHRef)
                            .SetResourceIdentifier((object)new Person())
                    },
                new object[]
                    {
                        "WithEmptyResourceAndMeta",
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToAuthorHRef)
                            .ResourceIdentifier((object)new Person())
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierEnd()
                    },
                new object[]
                    {
                        "WithResource",
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToAuthorHRef)
                            .SetResourceIdentifier((object)SamplePersons.Person)
                    },
                new object[]
                    {
                        "WithResourceAndMeta",
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifierWithMeta
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleToRelationshipsToAuthorHRef)
                            .ResourceIdentifier((object)SamplePersons.Person)
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierEnd()
                    },
            };
        #endregion

        #region DocumentBuilderWriteWithFrameworkHypermediaTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteWithFrameworkHypermediaTestData = new[]
            {
                new object[]
                    {
                        "WithResourceCollectionPath",
                        new ResourceCollectionDocument
                            {
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/payments")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object []{ (object)SamplePayments.Payment101, (object)SamplePayments.Payment102 })
                                .Relationships()
                                    .AddRelationship(ClrSampleData.PaymentToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithResourcePath",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/payments/101"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.PaymentType,
                                        Id = "101",
                                        Attributes = new ApiObject(ApiProperty.Create("amount", 100.0m)),
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
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/payments/101")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SamplePayments.Payment)
                                .Relationships()
                                    .AddRelationship(ClrSampleData.PaymentToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithResourcePathAndToOneResourcePathCanonical",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/payments/101/order"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.OrderType,
                                        Id = "1",
                                        Attributes = new ApiObject(ApiProperty.Create("total-price", 100.0m)),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.OrderToOrderItemsRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/orders/1/relationships/line-items"},
                                                                    {Keywords.Related, "http://api.example.com/orders/1/line-items"}
                                                                }
                                                        }
                                                },
                                                {
                                                    ClrSampleData.OrderToPaymentsRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/orders/1/relationships/payments"},
                                                                    {Keywords.Related, "http://api.example.com/orders/1/payments"}
                                                                }
                                                        }
                                                },
                                                {
                                                    ClrSampleData.OrderToStoreRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/orders/1/relationships/store"},
                                                                    {Keywords.Related, "http://api.example.com/orders/1/store"}
                                                                }
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/orders/1"},
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/payments/101/order")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleOrders.Order)
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderToOrderItemsRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderToPaymentsRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderToStoreRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithResourcePathAndToOneResourcePathHierarchical",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/stores/50/configuration"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.StoreConfigurationType,
                                        Id = "50-Configuration",
                                        Attributes = new ApiObject(
                                            ApiProperty.Create("is-live", SampleStoreConfigurations.StoreConfiguration.IsLive),
                                            ApiProperty.Create("mailing-address", new ApiObject(
                                                ApiProperty.Create("address", SampleStoreConfigurations.StoreConfiguration.MailingAddress.Address),
                                                ApiProperty.Create("city", SampleStoreConfigurations.StoreConfiguration.MailingAddress.City),
                                                ApiProperty.Create("state", SampleStoreConfigurations.StoreConfiguration.MailingAddress.State),
                                                ApiProperty.Create("zip-code", SampleStoreConfigurations.StoreConfiguration.MailingAddress.ZipCode))),
                                            ApiProperty.Create("phone-numbers", SampleStoreConfigurations.StoreConfiguration.PhoneNumbers
                                                .Select(x =>
                                                    {
                                                        var apiObject = new ApiObject(
                                                            ApiProperty.Create("area-code", x.AreaCode),
                                                            ApiProperty.Create("number", x.Number));
                                                        return apiObject;
                                                    })
                                                .ToArray())),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/stores/50/configuration/relationships/pos"},
                                                                    {Keywords.Related, "http://api.example.com/stores/50/configuration/pos"}
                                                                }
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/stores/50/configuration"},
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/stores/50/configuration")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleStoreConfigurations.StoreConfiguration)
                                .Paths()
                                    .AddPath(SampleStores.Store, ClrSampleData.StoreToStoreConfigurationRel)
                                .PathsEnd()
                                .Relationships()
                                    .AddRelationship(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithResourcePathAndToOneResourcePathHierarchicalToOneResourcePathCanonical",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/stores/50/configuration/pos"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.PosSystemType,
                                        Id = "RadiantWcf",
                                        Attributes = new ApiObject(
                                            ApiProperty.Create("pos-system-name", SamplePosSystems.PosSystem.PosSystemName),
                                            ApiProperty.Create("end-of-life-date", SamplePosSystems.PosSystem.EndOfLifeDate)),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.PosSystemToStoreConfigurationsRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/pos-systems/RadiantWcf/relationships/store-configurations"},
                                                                    {Keywords.Related, "http://api.example.com/pos-systems/RadiantWcf/store-configurations"}
                                                                }
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/pos-systems/RadiantWcf"},
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/stores/50/configuration/pos")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SamplePosSystems.PosSystem)
                                .Relationships()
                                    .AddRelationship(ClrSampleData.PosSystemToStoreConfigurationsRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithResourcePathAndToManyResourceCollectionPath",
                        new ResourceCollectionDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/orders/1/payments"}
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/orders/1/payments")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object []{ (object)SamplePayments.Payment101, (object)SamplePayments.Payment102 })
                                .Relationships()
                                    .AddRelationship(ClrSampleData.PaymentToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithResourcePathAndToManyResourceCollectionPathWithDifferentSelfAndCanonicalLinks",
                        new ResourceCollectionDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/orders/1/payments"}
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
                                                                            {Keywords.Self, "http://api.example.com/orders/1/payments/101/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/orders/1/payments/101/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/orders/1/payments/101"},
                                                        {Keywords.Canonical, "http://api.example.com/payments/101"},
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
                                                                            {Keywords.Self, "http://api.example.com/orders/1/payments/102/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/orders/1/payments/102/order"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/orders/1/payments/102"},
                                                        {Keywords.Canonical, "http://api.example.com/payments/102"},
                                                    },
                                            }
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/orders/1/payments")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object []{ (object)SamplePayments.Payment101, (object)SamplePayments.Payment102 })
                                .Paths()
                                    .AddPath((x, y) => { x.AddPath(SampleOrders.Order, ClrSampleData.OrderToPaymentsRel); })
                                .PathsEnd()
                                .Relationships()
                                    .AddRelationship(ClrSampleData.PaymentToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                    .AddLink(Keywords.Canonical)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithRootPathSegmentsAndResourceCollectionPath",
                        new ResourceCollectionDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/api/v2/orders"}
                                    },
                                Data = new List<Resource>
                                    {
                                        new Resource
                                            {
                                                Type = ClrSampleData.OrderType,
                                                Id = "1",
                                                Attributes = new ApiObject(ApiProperty.Create("total-price", 100.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.OrderToOrderItemsRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/1/relationships/line-items"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/1/line-items"}
                                                                        }
                                                                }
                                                        },
                                                        {
                                                            ClrSampleData.OrderToPaymentsRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/1/relationships/payments"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/1/payments"}
                                                                        }
                                                                }
                                                        },
                                                        {
                                                            ClrSampleData.OrderToStoreRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/1/relationships/store"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/1/store"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/api/v2/orders/1"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.OrderType,
                                                Id = "2",
                                                Attributes = new ApiObject(ApiProperty.Create("total-price", 200.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.OrderToOrderItemsRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/2/relationships/line-items"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/2/line-items"}
                                                                        }
                                                                }
                                                        },
                                                        {
                                                            ClrSampleData.OrderToPaymentsRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/2/relationships/payments"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/2/payments"}
                                                                        }
                                                                }
                                                        },
                                                        {
                                                            ClrSampleData.OrderToStoreRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/2/relationships/store"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/2/store"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/api/v2/orders/2"},
                                                    },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithRootPathSegments, "http://api.example.com/api/v2/orders")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object []{ (object)SampleOrders.Order1, (object)SampleOrders.Order2 })
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderToOrderItemsRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderToPaymentsRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderToStoreRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithRootPathSegmentsAndResourcePath",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/api/v2/orders/1"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.OrderType,
                                        Id = "1",
                                        Attributes = new ApiObject(ApiProperty.Create("total-price", 100.0m)),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.OrderToOrderItemsRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/api/v2/orders/1/relationships/line-items"},
                                                                    {Keywords.Related, "http://api.example.com/api/v2/orders/1/line-items"}
                                                                }
                                                        }
                                                },
                                                {
                                                    ClrSampleData.OrderToPaymentsRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/api/v2/orders/1/relationships/payments"},
                                                                    {Keywords.Related, "http://api.example.com/api/v2/orders/1/payments"}
                                                                }
                                                        }
                                                },
                                                {
                                                    ClrSampleData.OrderToStoreRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/api/v2/orders/1/relationships/store"},
                                                                    {Keywords.Related, "http://api.example.com/api/v2/orders/1/store"}
                                                                }
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/api/v2/orders/1"},
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithRootPathSegments, "http://api.example.com/api/v2/orders/1")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleOrders.Order)
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderToOrderItemsRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderToPaymentsRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderToStoreRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithRootPathSegmentsAndResourcePathAndToManyResourceCollectionPath",
                        new ResourceCollectionDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items"}
                                    },
                                Data = new List<Resource>
                                    {
                                        new Resource
                                            {
                                                Type = ClrSampleData.OrderItemType,
                                                Id = "1001",
                                                Attributes = new ApiObject(
                                                    ApiProperty.Create("product-name", "Widget A"),
                                                    ApiProperty.Create("quantity", 2m),
                                                    ApiProperty.Create("unit-price", 25.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.OrderItemToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1001/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/1/line-items/1001/order"}
                                                                        }
                                                                }
                                                        },
                                                        {
                                                            ClrSampleData.OrderItemToProductRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1001/relationships/product"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/1/line-items/1001/product"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1001"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.OrderItemType,
                                                Id = "1002",
                                                Attributes = new ApiObject(
                                                    ApiProperty.Create("product-name", "Widget B"),
                                                    ApiProperty.Create("quantity", 1m),
                                                    ApiProperty.Create("unit-price", 50.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.OrderItemToOrderRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1002/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/1/line-items/1002/order"}
                                                                        }
                                                                }
                                                        },
                                                        {
                                                            ClrSampleData.OrderItemToProductRel, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1002/relationships/product"},
                                                                            {Keywords.Related, "http://api.example.com/api/v2/orders/1/line-items/1002/product"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1002"},
                                                    },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithRootPathSegments, "http://api.example.com/api/v2/orders/1/line-items")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .ResourceCollection(new object []{ (object)SampleOrderItems.OrderItem1001, (object)SampleOrderItems.OrderItem1002 })
                                .Paths()
                                    .AddPath(SampleOrders.Order, ClrSampleData.OrderToOrderItemsRel)
                                .PathsEnd()
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderItemToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderItemToProductRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceCollectionEnd()
                    },
                new object[]
                    {
                        "WithRootPathSegmentsAndResourcePathAndToManyResourcePath",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1001"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.OrderItemType,
                                        Id = "1001",
                                        Attributes = new ApiObject(
                                            ApiProperty.Create("product-name", "Widget A"),
                                            ApiProperty.Create("quantity", 2m),
                                            ApiProperty.Create("unit-price", 25.0m)),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.OrderItemToOrderRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1001/relationships/order"},
                                                                    {Keywords.Related, "http://api.example.com/api/v2/orders/1/line-items/1001/order"}
                                                                }
                                                        }
                                                },
                                                {
                                                    ClrSampleData.OrderItemToProductRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1001/relationships/product"},
                                                                    {Keywords.Related, "http://api.example.com/api/v2/orders/1/line-items/1001/product"}
                                                                }
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/api/v2/orders/1/line-items/1001"},
                                            },
                                    },
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithRootPathSegments, "http://api.example.com/api/v2/orders/1/line-items/1001")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleOrderItems.OrderItem1001)
                                .Paths()
                                    .AddPath(SampleOrders.Order, ClrSampleData.OrderToOrderItemsRel)
                                .PathsEnd()
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderItemToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderItemToProductRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithRootPathSegmentsAndNonResourcePathAndResourcePathAndToManyResourcePath",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items/1001"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.OrderItemType,
                                        Id = "1001",
                                        Attributes = new ApiObject(
                                            ApiProperty.Create("product-name", "Widget A"),
                                            ApiProperty.Create("quantity", 2m),
                                            ApiProperty.Create("unit-price", 25.0m)),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.OrderItemToOrderRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items/1001/relationships/order"},
                                                                    {Keywords.Related, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items/1001/order"}
                                                                }
                                                        }
                                                },
                                                {
                                                    ClrSampleData.OrderItemToProductRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items/1001/relationships/product"},
                                                                    {Keywords.Related, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items/1001/product"}
                                                                }
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items/1001"},
                                            },
                                    },
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithRootPathSegments, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/line-items/1001")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleOrderItems.OrderItem1001)
                                .Paths()
                                    .AddPath("nrp-1")
                                    .AddPath("nrp-2")
                                    .AddPath(SampleOrders.Order, ClrSampleData.OrderToOrderItemsRel)
                                .PathsEnd()
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderItemToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderItemToProductRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithRootPathSegmentsAndNonResourcePathAndResourcePathAndNonResourcePathAndToManyResourcePath",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items/1001"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.OrderItemType,
                                        Id = "1001",
                                        Attributes = new ApiObject(
                                            ApiProperty.Create("product-name", "Widget A"),
                                            ApiProperty.Create("quantity", 2m),
                                            ApiProperty.Create("unit-price", 25.0m)),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.OrderItemToOrderRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items/1001/relationships/order"},
                                                                    {Keywords.Related, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items/1001/order"}
                                                                }
                                                        }
                                                },
                                                {
                                                    ClrSampleData.OrderItemToProductRel, new Relationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items/1001/relationships/product"},
                                                                    {Keywords.Related, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items/1001/product"}
                                                                }
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items/1001"},
                                            },
                                    },
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithRootPathSegments, "http://api.example.com/api/v2/nrp-1/nrp-2/orders/1/nrp-3/nrp-4/line-items/1001")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleOrderItems.OrderItem1001)
                                .Paths()
                                    .AddPath("nrp-1")
                                    .AddPath("nrp-2")
                                    .AddPath(SampleOrders.Order, ClrSampleData.OrderToOrderItemsRel)
                                    .AddPath("nrp-3")
                                    .AddPath("nrp-4")
                                .PathsEnd()
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderItemToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderItemToProductRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "WithNonResourcePathAndResourcePathAndAllIncludedResources",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/en-us/orders/1"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.OrderType,
                                        Id = "1",
                                        Attributes = new ApiObject(ApiProperty.Create("total-price", 100.0m)),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.OrderToOrderItemsRel, new ToManyRelationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/en-us/orders/1/relationships/line-items"},
                                                                    {Keywords.Related, "http://api.example.com/en-us/orders/1/line-items"}
                                                                },
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1001"),
                                                                    new ResourceIdentifier(ClrSampleData.OrderItemType, "1002")
                                                                }
                                                        }
                                                },
                                                {
                                                    ClrSampleData.OrderToPaymentsRel, new ToManyRelationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/en-us/orders/1/relationships/payments"},
                                                                    {Keywords.Related, "http://api.example.com/en-us/orders/1/payments"}
                                                                },
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ClrSampleData.PaymentType, "101"),
                                                                    new ResourceIdentifier(ClrSampleData.PaymentType, "102")
                                                                }
                                                        }
                                                },
                                                {
                                                    ClrSampleData.OrderToStoreRel, new ToOneRelationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/en-us/orders/1/relationships/store"},
                                                                    {Keywords.Related, "http://api.example.com/en-us/orders/1/store"}
                                                                },
                                                            Data = new ResourceIdentifier(ClrSampleData.StoreType, "50")
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/en-us/orders/1"},
                                            },
                                    },
                                Included = new List<Resource>
                                    {
                                        new Resource
                                            {
                                                Type = ClrSampleData.OrderItemType,
                                                Id = "1001",
                                                Attributes = new ApiObject(
                                                    ApiProperty.Create("product-name", "Widget A"),
                                                    ApiProperty.Create("quantity", 2m),
                                                    ApiProperty.Create("unit-price", 25.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.OrderItemToOrderRel, new ToOneRelationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/en-us/orders/1/line-items/1001/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/en-us/orders/1/line-items/1001/order"}
                                                                        },
                                                                    Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                                                                }
                                                        },
                                                        {
                                                            ClrSampleData.OrderItemToProductRel, new ToOneRelationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/en-us/orders/1/line-items/1001/relationships/product"},
                                                                            {Keywords.Related, "http://api.example.com/en-us/orders/1/line-items/1001/product"}
                                                                        },
                                                                    Data = new ResourceIdentifier(ClrSampleData.ProductType, "501")
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/en-us/orders/1/line-items/1001"},
                                                    },
                                             },
                                        new Resource
                                            {
                                                Type = ClrSampleData.OrderItemType,
                                                Id = "1002",
                                                Attributes = new ApiObject(
                                                    ApiProperty.Create("product-name", "Widget B"),
                                                    ApiProperty.Create("quantity", 1m),
                                                    ApiProperty.Create("unit-price", 50.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.OrderItemToOrderRel, new ToOneRelationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/en-us/orders/1/line-items/1002/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/en-us/orders/1/line-items/1002/order"}
                                                                        },
                                                                    Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                                                                }
                                                        },
                                                        {
                                                            ClrSampleData.OrderItemToProductRel, new ToOneRelationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/en-us/orders/1/line-items/1002/relationships/product"},
                                                                            {Keywords.Related, "http://api.example.com/en-us/orders/1/line-items/1002/product"}
                                                                        },
                                                                    Data = new ResourceIdentifier(ClrSampleData.ProductType, "502")
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/en-us/orders/1/line-items/1002"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.PaymentType,
                                                Id = "101",
                                                Attributes = new ApiObject(ApiProperty.Create("amount", 75.0m)),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PaymentToOrderRel, new ToOneRelationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/en-us/payments/101/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/en-us/payments/101/order"}
                                                                        },
                                                                    Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/en-us/payments/101"},
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
                                                            ClrSampleData.PaymentToOrderRel, new ToOneRelationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/en-us/payments/102/relationships/order"},
                                                                            {Keywords.Related, "http://api.example.com/en-us/payments/102/order"}
                                                                        },
                                                                    Data = new ResourceIdentifier(ClrSampleData.OrderType, "1")
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/en-us/payments/102"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.StoreType,
                                                Id = "50",
                                                Attributes = new ApiObject(
                                                    ApiProperty.Create("store-name", "Store 50"),
                                                    ApiProperty.Create("address", "1234 Main Street"),
                                                    ApiProperty.Create("city", "Boynton Beach"),
                                                    ApiProperty.Create("state", "FL"),
                                                    ApiProperty.Create("zip-code", "33472")),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.StoreToStoreConfigurationRel, new ToOneRelationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/en-us/stores/50/relationships/configuration"},
                                                                            {Keywords.Related, "http://api.example.com/en-us/stores/50/configuration"}
                                                                        },
                                                                    Data = new ResourceIdentifier(ClrSampleData.StoreConfigurationType, "50-Configuration")
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/en-us/stores/50"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.ProductType,
                                                Id = "501",
                                                Attributes = new ApiObject(
                                                    ApiProperty.Create("name", "Widget A"),
                                                    ApiProperty.Create("unit-price", 25.0m)),
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/en-us/products/501"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.ProductType,
                                                Id = "502",
                                                Attributes = new ApiObject(
                                                    ApiProperty.Create("name", "Widget B"),
                                                    ApiProperty.Create("unit-price", 50.0m)),
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/en-us/products/502"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.StoreConfigurationType,
                                                Id = "50-Configuration",
                                                Attributes = new ApiObject(
                                                    ApiProperty.Create("is-live", SampleStoreConfigurations.StoreConfiguration.IsLive),
                                                    ApiProperty.Create("mailing-address", new ApiObject(
                                                        ApiProperty.Create("address", SampleStoreConfigurations.StoreConfiguration.MailingAddress.Address),
                                                        ApiProperty.Create("city", SampleStoreConfigurations.StoreConfiguration.MailingAddress.City),
                                                        ApiProperty.Create("state", SampleStoreConfigurations.StoreConfiguration.MailingAddress.State),
                                                        ApiProperty.Create("zip-code", SampleStoreConfigurations.StoreConfiguration.MailingAddress.ZipCode))),
                                                    ApiProperty.Create("phone-numbers", SampleStoreConfigurations.StoreConfiguration.PhoneNumbers
                                                        .Select(x =>
                                                            {
                                                                var apiObject = new ApiObject(
                                                                    ApiProperty.Create("area-code", x.AreaCode),
                                                                    ApiProperty.Create("number", x.Number));
                                                                return apiObject;
                                                            })
                                                        .ToArray())),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new ToOneRelationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/en-us/stores/50/configuration/relationships/pos"},
                                                                            {Keywords.Related, "http://api.example.com/en-us/stores/50/configuration/pos"}
                                                                        },
                                                                    Data = new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantWcf")
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/en-us/stores/50/configuration"},
                                                    },
                                            },
                                        new Resource
                                            {
                                                Type = ClrSampleData.PosSystemType,
                                                Id = "RadiantWcf",
                                                Attributes = new ApiObject(
                                                    ApiProperty.Create("pos-system-name", "Radiant WCF-Based Api"),
                                                    ApiProperty.Create("end-of-life-date", new DateTime(1999, 12, 31))),
                                                Relationships = new Relationships
                                                    {
                                                        {
                                                            ClrSampleData.PosSystemToStoreConfigurationsRelPathSegment, new Relationship
                                                                {
                                                                    Links = new Links
                                                                        {
                                                                            {Keywords.Self, "http://api.example.com/en-us/pos-systems/RadiantWcf/relationships/store-configurations"},
                                                                            {Keywords.Related, "http://api.example.com/en-us/pos-systems/RadiantWcf/store-configurations"}
                                                                        }
                                                                }
                                                        },
                                                    },
                                                Links = new Links
                                                    {
                                                        {Keywords.Self, "http://api.example.com/en-us/pos-systems/RadiantWcf"},
                                                    },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/en-us/orders/1")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleOrders.Order)
                                .Paths()
                                    .AddPath("en-us")
                                .PathsEnd()
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderToOrderItemsRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderToPaymentsRel, new [] { Keywords.Self, Keywords.Related })
                                    .AddRelationship(ClrSampleData.OrderToStoreRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                            .Included()
                                .Include(ToManyIncludedResources.Create((object)SampleOrders.Order, ClrSampleData.OrderToOrderItemsRel, typeof(OrderItem), new object []{ (object)SampleOrderItems.OrderItem1001, (object)SampleOrderItems.OrderItem1002 }))
                                    .Paths()
                                        .AddPath("en-us")
                                        .AddPath(SampleOrders.Order, ClrSampleData.OrderToOrderItemsRel)
                                    .PathsEnd()
                                    .Relationships()
                                        .AddRelationship(ClrSampleData.OrderItemToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                        .AddRelationship(ClrSampleData.OrderItemToProductRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToManyIncludedResources.Create((object)SampleOrders.Order, ClrSampleData.OrderToPaymentsRel, typeof(Payment), new object []{ (object)SamplePayments.Payment101, (object)SamplePayments.Payment102 }))
                                    .Paths()
                                        .AddPath("en-us")
                                    .PathsEnd()
                                    .Relationships()
                                        .AddRelationship(ClrSampleData.PaymentToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                                .Include(ToOneIncludedResource.Create((object)SampleOrders.Order, ClrSampleData.OrderToStoreRel, typeof(Store), (object)SampleStores.Store50))
                                    .Paths()
                                        .AddPath("en-us")
                                    .PathsEnd()
                                    .Relationships()
                                        .AddRelationship(ClrSampleData.StoreToStoreConfigurationRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()

                                .AddInclude(ToOneIncludedResource.Create((object)SampleOrderItems.OrderItem1001, ClrSampleData.OrderItemToOrderRel, typeof(Order), (object)SampleOrders.Order),
                                            ToOneIncludedResource.Create((object)SampleOrderItems.OrderItem1002, ClrSampleData.OrderItemToOrderRel, typeof(Order), (object)SampleOrders.Order))
                                .AddInclude(ToOneIncludedResource.Create((object)SamplePayments.Payment101, ClrSampleData.PaymentToOrderRel, typeof(Order), (object)SampleOrders.Order),
                                            ToOneIncludedResource.Create((object)SamplePayments.Payment102, ClrSampleData.PaymentToOrderRel, typeof(Order), (object)SampleOrders.Order))

                                .Include(ToOneIncludedResource.Create((object)SampleOrderItems.OrderItem1001, ClrSampleData.OrderItemToProductRel, typeof(Product), (object)SampleProducts.Product501))
                                    .Paths()
                                        .AddPath("en-us")
                                    .PathsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()

                                .Include(ToOneIncludedResource.Create((object)SampleOrderItems.OrderItem1002, ClrSampleData.OrderItemToProductRel, typeof(Product), (object)SampleProducts.Product502))
                                    .Paths()
                                        .AddPath("en-us")
                                    .PathsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()

                                .Include(ToOneIncludedResource.Create((object)SampleStores.Store50, ClrSampleData.StoreToStoreConfigurationRel, typeof(StoreConfiguration), (object)SampleStoreConfigurations.Store50Configuration))
                                    .Paths()
                                        .AddPath("en-us")
                                        .AddPath<Store, long>(50, ClrSampleData.StoreToStoreConfigurationRel)
                                    .PathsEnd()
                                    .Relationships()
                                        .AddRelationship(ClrSampleData.StoreToStoreConfigurationToPosSystemRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()

                                .Include(ToOneIncludedResource.Create((object)SampleStoreConfigurations.Store50Configuration, ClrSampleData.StoreToStoreConfigurationToPosSystemRel, typeof(PosSystem), (object)SamplePosSystems.PosSystemRadiantWcf))
                                    .Paths()
                                        .AddPath("en-us")
                                    .PathsEnd()
                                    .Relationships()
                                        .AddRelationship(ClrSampleData.PosSystemToStoreConfigurationsRelPathSegment, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()

                            .IncludedEnd()
                    },
                new object[]
                    {
                        "WithResourcePathAndEmptyToManyIncludedResources",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/orders/1"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.OrderType,
                                        Id = "1",
                                        Attributes = new ApiObject(ApiProperty.Create("total-price", 100.0m)),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.OrderToPaymentsRel, new ToManyRelationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/orders/1/relationships/payments"},
                                                                    {Keywords.Related, "http://api.example.com/orders/1/payments"}
                                                                },
                                                            Data = new List<ResourceIdentifier>()
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/orders/1"},
                                            },
                                    },
                                Included = new List<Resource>()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/orders/1")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleOrders.Order)
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderToPaymentsRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                            .Included()
                                .Include(ToManyIncludedResources.Create((object)SampleOrders.Order, ClrSampleData.OrderToPaymentsRel,  typeof(Payment), default(IEnumerable<object>)))
                                    .Relationships()
                                        .AddRelationship(ClrSampleData.PaymentToOrderRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                            .IncludedEnd()
                    },
                new object[]
                    {
                        "WithResourcePathAndNullToOneIncludedResource",
                        new ResourceDocument
                            {
                                Links = new Links
                                    {
                                        {Keywords.Self, "http://api.example.com/orders/1"}
                                    },
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.OrderType,
                                        Id = "1",
                                        Attributes = new ApiObject(ApiProperty.Create("total-price", 100.0m)),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ClrSampleData.OrderToStoreRel, new ToOneRelationship
                                                        {
                                                            Links = new Links
                                                                {
                                                                    {Keywords.Self, "http://api.example.com/orders/1/relationships/store"},
                                                                    {Keywords.Related, "http://api.example.com/orders/1/store"}
                                                                },
                                                            Data = default(ResourceIdentifier)
                                                        }
                                                },
                                            },
                                        Links = new Links
                                            {
                                                {Keywords.Self, "http://api.example.com/orders/1"},
                                            },
                                    },
                                Included = new List<Resource>()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, "http://api.example.com/orders/1")
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Resource((object)SampleOrders.Order)
                                .Relationships()
                                    .AddRelationship(ClrSampleData.OrderToStoreRel, new [] { Keywords.Self, Keywords.Related })
                                .RelationshipsEnd()
                                .Links()
                                    .AddLink(Keywords.Self)
                                .LinksEnd()
                            .ResourceEnd()
                            .Included()
                                .Include(ToOneIncludedResource.Create((object)SampleOrders.Order, ClrSampleData.OrderToStoreRel,  typeof(Store), default(object)))
                                    .Relationships()
                                        .AddRelationship(ClrSampleData.StoreToStoreConfigurationRel, new [] { Keywords.Self, Keywords.Related })
                                    .RelationshipsEnd()
                                    .Links()
                                        .AddLink(Keywords.Self)
                                    .LinksEnd()
                                .IncludeEnd()
                            .IncludedEnd()
                    },
            };
        #endregion

        #region DocumentBuilderWriteErrorsDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteErrorsDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyErrors",
                        ErrorsDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .Errors()
                            .ErrorsEnd()
                    },
                new object[]
                    {
                        "WithEmptyErrorsAndJsonApiAndMetaAndLinksAndUserBuiltHypermedia",
                        new ErrorsDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Errors = Enumerable.Empty<Error>()
                                                   .ToList()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self, ApiSampleData.ArticleCollectionLink)
                            .LinksEnd()
                            .Errors()
                            .ErrorsEnd()
                    },
                new object[]
                    {
                        "WithEmptyErrorsAndJsonApiAndMetaAndLinksAndFrameworkBuiltHypermedia",
                        new ErrorsDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Errors = Enumerable.Empty<Error>()
                                                   .ToList()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Errors()
                            .ErrorsEnd()
                    },
                new object[]
                    {
                        "WithReadOnlyErrors",
                        new ErrorsDocument
                            {
                                Errors = new List<Error>
                                    {
                                        ApiSampleData.Error1,
                                        ApiSampleData.Error2
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .Errors()
                                .AddError(ApiSampleData.Error1)
                                .AddError(ApiSampleData.Error2)
                            .ErrorsEnd()
                    },
                new object[]
                    {
                        "WithReadOnlyErrorsAndJsonApiAndMetaAndLinksAndUserBuiltHypermedia",
                        new ErrorsDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self, ApiSampleData.ArticleCollectionLink)
                            .LinksEnd()
                            .Errors()
                                .AddError(ApiSampleData.Error1, ApiSampleData.Error2)
                            .ErrorsEnd()
                    },
                new object[]
                    {
                        "WithReadOnlyErrorsAndJsonApiAndMetaAndLinksAndFrameworkBuiltHypermedia",
                        new ErrorsDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes, HypermediaAssemblerRegistry, UrlBuilderConfigurationWithoutRootPathSegments, ApiSampleData.ArticleCollectionHRef)
                            .SetJsonApiVersion(ApiSampleData.JsonApiVersionAndMeta)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Links()
                                .AddLink(Keywords.Self)
                            .LinksEnd()
                            .Errors()
                                .AddError(ApiSampleData.Error1, ApiSampleData.Error2)
                            .ErrorsEnd()
                    },
            };
        #endregion

        #endregion
    }
}
