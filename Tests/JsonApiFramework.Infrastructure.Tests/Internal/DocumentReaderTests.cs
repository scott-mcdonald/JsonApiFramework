// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Internal;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.TestAsserts.ClrResources;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal
{
    public class DocumentReaderTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentReaderTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DocumentReaderTestData")]
        public void TestDocumentReaderGetDocumentType(string name, IServiceModel serviceModel, Document apiDocument)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();

            // Arrange
            var apiDocumentJson = apiDocument.ToJson();
            apiDocument = JsonObject.Parse<Document>(apiDocumentJson);

            var documentReader = new DocumentReader(apiDocument, serviceModel);
            this.OutputDomDocument(documentReader);

            this.OutputEmptyLine();
            this.OutputJson("Document JSON", apiDocument);

            var expectedDocumentType = apiDocument.GetDocumentType();

            // Act
            var actualDocumentType = documentReader.GetDocumentType();

            // Assert
            Assert.Equal(expectedDocumentType, actualDocumentType);
        }

        [Theory]
        [MemberData("DocumentReaderTestData")]
        public void TestDocumentReaderIsDataDocument(string name, IServiceModel serviceModel, Document apiDocument)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", apiDocument);

            // Arrange
            var apiDocumentJson = apiDocument.ToJson();
            apiDocument = JsonObject.Parse<Document>(apiDocumentJson);

            var documentReader = new DocumentReader(apiDocument, serviceModel);
            this.OutputDomDocument(documentReader);
            var expectedIsDataDocument = apiDocument.IsDataDocument();

            this.OutputEmptyLine();
            this.Output.WriteLine("Expected IsDataDocument: {0}", expectedIsDataDocument);

            // Act
            var actualIsDataDocument = documentReader.IsDataDocument();

            this.OutputEmptyLine();
            this.Output.WriteLine("Actual   IsDataDocument: {0}", actualIsDataDocument);

            // Assert
            Assert.Equal(expectedIsDataDocument, actualIsDataDocument);
        }

        [Theory]
        [MemberData("DocumentReaderTestData")]
        public void TestDocumentReaderIsErrorsDocument(string name, IServiceModel serviceModel, Document apiDocument)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", apiDocument);

            // Arrange
            var apiDocumentJson = apiDocument.ToJson();
            apiDocument = JsonObject.Parse<Document>(apiDocumentJson);

            var documentReader = new DocumentReader(apiDocument, serviceModel);
            this.OutputDomDocument(documentReader);
            var expectedIsErrorsDocument = apiDocument.IsErrorsDocument();


            this.OutputEmptyLine();
            this.Output.WriteLine("Expected IsErrorsDocument: {0}", expectedIsErrorsDocument);

            // Act
            var actualIsErrorsDocument = documentReader.IsErrorsDocument();

            this.OutputEmptyLine();
            this.Output.WriteLine("Actual   IsErrorsDocument: {0}", actualIsErrorsDocument);

            // Assert
            Assert.Equal(expectedIsErrorsDocument, actualIsErrorsDocument);
        }

        [Theory]
        [MemberData("DocumentReaderTestData")]
        public void TestDocumentReaderIsMetaOnlyDocument(string name, IServiceModel serviceModel, Document apiDocument)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", apiDocument);

            // Arrange
            var apiDocumentJson = apiDocument.ToJson();
            apiDocument = JsonObject.Parse<Document>(apiDocumentJson);

            var documentReader = new DocumentReader(apiDocument, serviceModel);
            this.OutputDomDocument(documentReader);
            var expectedIsMetaOnlyDocument = apiDocument.IsMetaOnlyDocument();


            this.OutputEmptyLine();
            this.Output.WriteLine("Expected IsMetaOnlyDocument: {0}", expectedIsMetaOnlyDocument);

            // Act
            var actualIsMetaOnlyDocument = documentReader.IsMetaOnlyDocument();

            this.OutputEmptyLine();
            this.Output.WriteLine("Actual   IsMetaOnlyDocument: {0}", actualIsMetaOnlyDocument);

            // Assert
            Assert.Equal(expectedIsMetaOnlyDocument, actualIsMetaOnlyDocument);
        }

        [Theory]
        [MemberData("DocumentReaderTestData")]
        public void TestDocumentReaderIsValidDocument(string name, IServiceModel serviceModel, Document apiDocument)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", apiDocument);

            // Arrange
            var apiDocumentJson = apiDocument.ToJson();
            apiDocument = JsonObject.Parse<Document>(apiDocumentJson);

            var documentReader = new DocumentReader(apiDocument, serviceModel);
            this.OutputDomDocument(documentReader);
            var expectedIsValidDocument = apiDocument.IsValidDocument();


            this.OutputEmptyLine();
            this.Output.WriteLine("Expected IsValidDocument: {0}", expectedIsValidDocument);

            // Act
            var actualIsValidDocument = documentReader.IsValidDocument();

            this.OutputEmptyLine();
            this.Output.WriteLine("Actual   IsValidDocument: {0}", actualIsValidDocument);

            // Assert
            Assert.Equal(expectedIsValidDocument, actualIsValidDocument);
        }

        [Theory]
        [MemberData("DocumentReaderTestData")]
        public void TestDocumentReaderGetDocumentJsonApi(string name, IServiceModel serviceModel, Document apiDocument)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", apiDocument);

            // Arrange
            var apiDocumentJson = apiDocument.ToJson();
            apiDocument = JsonObject.Parse<Document>(apiDocumentJson);

            var documentReader = new DocumentReader(apiDocument, serviceModel);
            this.OutputDomDocument(documentReader);
            var expectedJsonApiVersion = apiDocument.JsonApiVersion;

            // Act
            var actualJsonApiVersion = documentReader.GetJsonApiVersion();

            // Assert
            JsonApiVersionAssert.Equal(expectedJsonApiVersion, actualJsonApiVersion);
        }

        [Theory]
        [MemberData("DocumentReaderTestData")]
        public void TestDocumentReaderGetDocumentMeta(string name, IServiceModel serviceModel, Document apiDocument)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", apiDocument);

            // Arrange
            var apiDocumentJson = apiDocument.ToJson();
            apiDocument = JsonObject.Parse<Document>(apiDocumentJson);

            var documentReader = new DocumentReader(apiDocument, serviceModel);
            this.OutputDomDocument(documentReader);
            var expectedMeta = apiDocument.Meta;

            // Act
            var actualMeta = documentReader.GetDocumentMeta();

            // Assert
            MetaAssert.Equal(expectedMeta, actualMeta);
        }

        [Theory]
        [MemberData("DocumentReaderTestData")]
        public void TestDocumentReaderGetDocumentLinks(string name, IServiceModel serviceModel, Document apiDocument)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", apiDocument);

            // Arrange
            var apiDocumentJson = apiDocument.ToJson();
            apiDocument = JsonObject.Parse<Document>(apiDocumentJson);

            var documentReader = new DocumentReader(apiDocument, serviceModel);
            this.OutputDomDocument(documentReader);
            var expectedLinks = apiDocument.Links;

            // Act
            var actualLinks = documentReader.GetDocumentLinks();

            // Assert
            LinksAssert.Equal(expectedLinks, actualLinks);
        }

        [Theory]
        [MemberData("GetRelatedResourceTestData")]
        public void TestDocumentReaderGetRelatedToOneResource(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetRelatedResourceCollectionTestData")]
        public void TestDocumentReaderGetRelatedToManyResourceCollection(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceTestData")]
        public void TestDocumentReaderGetResource(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceByResourceIdTestData")]
        public void TestDocumentReaderGetResourceByResourceId(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceCollectionTestData")]
        public void TestDocumentReaderGetResourceCollection(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceIdTestData")]
        public void TestDocumentReaderGetResourceId(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceIdCollectionTestData")]
        public void TestDocumentReaderGetResourceIdCollection(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceMetaTestData")]
        public void TestDocumentReaderGetResourceMeta(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceMetaByResourceTestData")]
        public void TestDocumentReaderGetResourceMetaByResource(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceMetaByResourceIdTestData")]
        public void TestDocumentReaderGetResourceMetaByResourceId(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceMetaCollectionTestData")]
        public void TestDocumentReaderGetResourceMetaCollection(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceLinksTestData")]
        public void TestDocumentReaderGetResourceLinks(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceLinksByResourceTestData")]
        public void TestDocumentReaderGetResourceLinksByResource(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceLinksByResourceIdTestData")]
        public void TestDocumentReaderGetResourceLinksByResourceId(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceLinksCollectionTestData")]
        public void TestDocumentReaderGetResourceLinksCollection(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceRelationshipsTestData")]
        public void TestDocumentReaderGetResourceRelationships(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceRelationshipsByResourceTestData")]
        public void TestDocumentReaderGetResourceRelationshipsByResource(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceRelationshipsByResourceIdTestData")]
        public void TestDocumentReaderGetResourceRelationshipsByResourceId(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceRelationshipsCollectionTestData")]
        public void TestDocumentReaderGetResourceRelationshipsCollection(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetResourceTypeCollectionTestData")]
        public void TestDocumentReaderGetResourceTypeCollection(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Theory]
        [MemberData("GetErrorCollectionTestData")]
        public void TestDocumentReaderGetErrorCollection(string name, IDocumentReaderTest test)
        { this.TestDocumentReaderTest(name, test); }

        [Fact]
        public void TestDocumentReaderGetResourceThrowsExceptionWithManyResources()
        {
            // Arrange
            var serviceModel = ClrSampleData.ServiceModelWithBlogResourceTypes;
            var document = new ResourceCollectionDocument
            {
                Links = new Links
                        {
                            {Keywords.Self, ApiSampleData.BlogToArticlesLink}
                        },
                Data = new List<Resource>
                    {
                            ApiSampleData.ArticleResource1,
                            ApiSampleData.ArticleResource2
                        }
            };
            var documentJson = document.ToJson();
            document = JsonObject.Parse<ResourceCollectionDocument>(documentJson);

            var documentReader = new DocumentReader(document, serviceModel);
            this.OutputDomDocument(documentReader);

            // Act
            this.OutputTestName(StaticReflection.GetMemberName<DocumentReaderTests>(x => x.TestDocumentReaderGetResourceThrowsExceptionWithManyResources()));
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", document);

            Func<object> methodCall = documentReader.GetResource<Article>;

            // Assert
            Assert.Throws<DocumentReadException>(methodCall);
        }

        [Fact]
        public void TestDocumentReaderGetResourceIdThrowsExceptionWithManyResources()
        {
            // Arrange
            var serviceModel = ClrSampleData.ServiceModelWithBlogResourceTypes;
            var document = new ResourceCollectionDocument
                {
                    Links = new Links
                        {
                            {Keywords.Self, ApiSampleData.BlogToArticlesLink}
                        },
                    Data = new List<Resource>
                        {
                            ApiSampleData.ArticleResource1,
                            ApiSampleData.ArticleResource2
                        }
                };
            var documentJson = document.ToJson();
            document = JsonObject.Parse<ResourceCollectionDocument>(documentJson);

            var documentReader = new DocumentReader(document, serviceModel);
            this.OutputDomDocument(documentReader);

            // Act
            this.OutputTestName(StaticReflection.GetMemberName<DocumentReaderTests>(x => x.TestDocumentReaderGetResourceIdThrowsExceptionWithManyResources()));
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", document);

            Func<object> methodCall = documentReader.GetResourceId<Article, string>;

            // Assert
            Assert.Throws<DocumentReadException>(methodCall);
        }

        [Fact]
        public void TestDocumentReaderGetResourceIdThrowsExceptionWithManyResourceIdentifiers()
        {
            // Arrange
            var serviceModel = ClrSampleData.ServiceModelWithBlogResourceTypes;
            var document = new ResourceIdentifierCollectionDocument
            {
                Links = new Links
                        {
                            {Keywords.Self, ApiSampleData.BlogToArticlesLink}
                        },
                Data = new List<ResourceIdentifier>
                        {
                            ApiSampleData.ArticleResourceIdentifier1,
                            ApiSampleData.ArticleResourceIdentifier2
                        }
            };
            var documentJson = document.ToJson();
            document = JsonObject.Parse<ResourceIdentifierCollectionDocument>(documentJson);

            var documentReader = new DocumentReader(document, serviceModel);
            this.OutputDomDocument(documentReader);

            // Act
            this.OutputTestName(StaticReflection.GetMemberName<DocumentReaderTests>(x => x.TestDocumentReaderGetResourceIdThrowsExceptionWithManyResourceIdentifiers()));
            this.OutputEmptyLine();
            this.OutputJson("Document JSON", document);

            Func<object> methodCall = documentReader.GetResourceId<Article, string>;

            // Assert
            Assert.Throws<DocumentReadException>(methodCall);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        #region DocumentReaderTestData
        public static readonly IEnumerable<object[]> DocumentReaderTestData = new[]
            {
                new object[] {"DocumentWithEmptyObject", ClrSampleData.ServiceModelWithBlogResourceTypes, Document.Empty},
                new object[]
                    {
                        "DocumentWithMeta", ClrSampleData.ServiceModelWithBlogResourceTypes, new Document
                            {
                                Meta = ApiSampleData.DocumentMeta
                            }
                    },
                new object[]
                    {
                        "DocumentWithMetaAndLinks", ClrSampleData.ServiceModelWithBlogResourceTypes, new Document
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                            }
                    },
                new object[]
                    {
                        "DocumentWithJsonApiAndMetaAndLinks", ClrSampleData.ServiceModelWithBlogResourceTypes, new Document
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                            }
                    },

                new object[] {"EmptyDocumentWithEmptyObject", ClrSampleData.ServiceModelWithBlogResourceTypes, EmptyDocument.Empty},
                new object[]
                    {
                        "EmptyDocumentWithJsonApiAndMetaAndLinks", ClrSampleData.ServiceModelWithBlogResourceTypes, new EmptyDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                            }
                    },

                new object[] {"ErrorsDocumentWithEmptyObject", ClrSampleData.ServiceModelWithBlogResourceTypes, ErrorsDocument.Empty},
                new object[]
                    {
                        "ErrorsDocumentWithErrors", ClrSampleData.ServiceModelWithBlogResourceTypes, new ErrorsDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Errors = new List<Error>
                                    {
                                        ApiSampleData.Error1,
                                        ApiSampleData.Error2
                                    }
                            }
                    },

                new object[] {"NullDocumentWithEmptyObject", ClrSampleData.ServiceModelWithBlogResourceTypes, NullDocument.Empty},
                new object[]
                    {
                        "NullDocumentWithJsonApiAndMetaAndLinks", ClrSampleData.ServiceModelWithBlogResourceTypes, new NullDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToAuthorLink}
                                    }
                            }
                    },

                new object[] {"ResourceDocumentWithEmptyObject", ClrSampleData.ServiceModelWithBlogResourceTypes, ResourceDocument.Empty},
                new object[]
                    {
                        "ResourceDocumentWithNullResource", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToAuthorLink}
                                    },
                                Data = null
                            }
                    },
                new object[]
                    {
                        "ResourceDocumentWithResource", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleLink}
                                    },
                                Data = ApiSampleData.ArticleResource
                            }
                    },
                new object[]
                    {
                        "ResourceDocumentWithResourceAndIncludedResources", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceDocument
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
                            }
                    },

                new object[] {"ResourceIdentifierDocumentWithEmptyObject", ClrSampleData.ServiceModelWithBlogResourceTypes, ResourceIdentifierDocument.Empty},
                new object[]
                    {
                        "ResourceIdentifierDocumentWithNullResourceIdentifier", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceIdentifierDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink}
                                    },
                                Data = null
                            }
                    },
                new object[]
                    {
                        "ResourceIdentifierDocumentWithResourceIdentifier", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceIdentifierDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink}
                                    },
                                Data = ApiSampleData.PersonResourceIdentifier
                            }
                    },

                new object[] {"ResourceCollectionDocumentWithEmptyObject", ClrSampleData.ServiceModelWithBlogResourceTypes, ResourceCollectionDocument.Empty},
                new object[]
                    {
                        "ResourceCollectionDocumentWithEmptyResources", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                    },
                                Data = Enumerable.Empty<Resource>()
                                                 .ToList()
                            }
                    },
                new object[]
                    {
                        "ResourceCollectionDocumentWithResources", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceCollectionDocument
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
                            }
                    },
                new object[]
                    {
                        "ResourceCollectionDocumentWithResourcesAndIncludedResources", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceCollectionDocument
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
                            }
                    },

                new object[] {"ResourceIdentifierCollectionDocumentWithEmptyObject", ClrSampleData.ServiceModelWithBlogResourceTypes, ResourceIdentifierCollectionDocument.Empty},
                new object[]
                    {
                        "ResourceIdentifierCollectionDocumentWithEmptyResourceIdentifiers", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceIdentifierCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink}
                                    },
                                Data = Enumerable.Empty<ResourceIdentifier>()
                                                 .ToList()
                            }
                    },
                new object[]
                    {
                        "ResourceIdentifierCollectionDocumentWithResourceIdentifiers", ClrSampleData.ServiceModelWithBlogResourceTypes, new ResourceIdentifierCollectionDocument
                            {
                                JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                Meta = ApiSampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink}
                                    },
                                Data = new List<ResourceIdentifier>
                                    {
                                        ApiSampleData.CommentResourceIdentifier1,
                                        ApiSampleData.CommentResourceIdentifier2
                                    }
                            }
                    },
            };
        #endregion

        #region GetRelatedResource TestData
        public static readonly IEnumerable<object[]> GetRelatedResourceTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyResourceDocument",
                        new GetRelatedResourceTest<Article, Person>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        }
                                },
                            ApiSampleData.ArticleToAuthorRel,
                            Enumerable.Empty<Tuple<Article, Person>>())
                    },
                new object[]
                    {
                        "WithEmptyResourceCollectionDocument",
                        new GetRelatedResourceTest<Article, Person>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        }
                                },
                            ApiSampleData.ArticleToAuthorRel,
                            Enumerable.Empty<Tuple<Article, Person>>())
                    },
                new object[]
                    {
                        "WithArticleResourceDocument",
                        new GetRelatedResourceTest<Article, Person>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
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
                            ApiSampleData.ArticleToAuthorRel,
                            new[]
                                {
                                    new Tuple<Article, Person>(SampleArticles.ArticleWithResourceLinkage, SamplePersons.Person)
                                })
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocument",
                        new GetRelatedResourceTest<Article, Person>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            ApiSampleData.ArticleToAuthorRel,
                            new[]
                                {
                                    new Tuple<Article, Person>(SampleArticles.ArticleWithResourceLinkage1, SamplePersons.Person1),
                                    new Tuple<Article, Person>(SampleArticles.ArticleWithResourceLinkage2, SamplePersons.Person2),
                                })
                    },
            };
        #endregion

        #region GetRelatedResourceCollection TestData
        public static readonly IEnumerable<object[]> GetRelatedResourceCollectionTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyResourceDocument",
                        new GetRelatedResourceCollectionTest<Article, Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        }
                                },
                            ApiSampleData.ArticleToCommentsRel,
                            Enumerable.Empty<Tuple<Article, IEnumerable<Comment>>>())
                    },
                new object[]
                    {
                        "WithEmptyResourceCollectionDocument",
                        new GetRelatedResourceCollectionTest<Article, Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        }
                                },
                            ApiSampleData.ArticleToCommentsRel,
                            Enumerable.Empty<Tuple<Article, IEnumerable<Comment>>>())
                    },
                new object[]
                    {
                        "WithArticleResourceDocument",
                        new GetRelatedResourceCollectionTest<Article, Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
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
                            ApiSampleData.ArticleToCommentsRel,
                            new[]
                                {
                                    new Tuple<Article, IEnumerable<Comment>>(SampleArticles.ArticleWithResourceLinkage, new [] {SampleComments.Comment1, SampleComments.Comment2})
                                })
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocument",
                        new GetRelatedResourceCollectionTest<Article, Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            ApiSampleData.ArticleToCommentsRel,
                            new[]
                                {
                                    new Tuple<Article, IEnumerable<Comment>>(SampleArticles.ArticleWithResourceLinkage1, new [] {SampleComments.Comment1, SampleComments.Comment2}),
                                    new Tuple<Article, IEnumerable<Comment>>(SampleArticles.ArticleWithResourceLinkage2, new [] {SampleComments.Comment3, SampleComments.Comment4})
                                })
                    },
            };
        #endregion

        #region GetResource TestData
        public static readonly IEnumerable<object[]> GetResourceTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new Document
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithNullDocument",
                        new GetResourceTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new NullDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithArticleResourceDocument",
                        new GetResourceTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResource
                                },
                            SampleArticles.Article)
                    },
                new object[]
                    {
                        "WithArticleResourceDocumentAndIncludedResourcesGetArticle",
                        new GetResourceTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
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
                            SampleArticles.ArticleWithResourceLinkage)
                    },
                new object[]
                    {
                        "WithArticleResourceDocumentAndIncludedResourcesGetPerson",
                        new GetResourceTest<Person>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
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
                            SamplePersons.Person)
                    },
                new object[]
                    {
                        "WithBlogResourceDocument",
                        new GetResourceTest<Blog>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.BlogLink}
                                        },
                                    Data = ApiSampleData.BlogResource
                                },
                            SampleBlogs.Blog)
                    },
                new object[]
                    {
                        "WithCommentResourceDocument",
                        new GetResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.CommentLink}
                                        },
                                    Data = ApiSampleData.CommentResource
                                },
                            SampleComments.Comment)
                    },
                new object[]
                    {
                        "WithPersonResourceDocument",
                        new GetResourceTest<Person>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.PersonLink}
                                        },
                                    Data = ApiSampleData.PersonResource
                                },
                            SamplePersons.Person)
                    },
                new object[]
                    {
                        "WithPaymentResourceDocument",
                        new GetResourceTest<Payment>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
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
                                        }
                                },
                            SamplePayments.Payment101)
                    },
                new object[]
                    {
                        "WithPaymentResourceDocumentGetOrder",
                        new GetResourceTest<Order>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
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
                                        }
                                },
                            default(Order))
                    },
                new object[]
                    {
                        "WithOrderResourceDocumentWithAllIncludedResources",
                        new GetResourceTest<Order>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            SampleDocuments.OrderResourceDocumentWithAllIncludedResources,
                            SampleOrders.Order)
                    },
                new object[]
                    {
                        "WithOrderResourceDocumentWithAllIncludedResourcesGetStore",
                        new GetResourceTest<Store>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            SampleDocuments.OrderResourceDocumentWithAllIncludedResources,
                            SampleStores.Store)
                    },
                new object[]
                    {
                        "WithOrderResourceDocumentWithAllIncludedResourcesGetStoreConfiguration",
                        new GetResourceTest<StoreConfiguration>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            SampleDocuments.OrderResourceDocumentWithAllIncludedResources,
                            SampleStoreConfigurations.StoreConfiguration)
                    },
                new object[]
                    {
                        "WithOrderResourceDocumentWithAllIncludedResourcesGetPosSystem",
                        new GetResourceTest<PosSystem>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            SampleDocuments.OrderResourceDocumentWithAllIncludedResources,
                            SamplePosSystems.PosSystem)
                    },

                new object[]
                    {
                        "WithStoreConfigurationResourceDocument",
                        new GetResourceTest<StoreConfiguration>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
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
                                                        ClrSampleData.StoreToStoreConfigurationToPosSystemRel,
                                                        new ToOneRelationship
                                                            {
                                                                Links = new Links
                                                                    {
                                                                        {
                                                                            Keywords.Self, "http://api.example.com/stores/50/configuration/relationships/pos"
                                                                        },
                                                                        {
                                                                            Keywords.Related, "http://api.example.com/stores/50/configuration/pos"
                                                                        }
                                                                    },
                                                                Data =
                                                                    new ResourceIdentifier(ClrSampleData.PosSystemType, "RadiantRest")
                                                            }
                                                    },
                                                },
                                            Links = new Links
                                                {
                                                    {Keywords.Self, "http://api.example.com/stores/50/configuration"},
                                                },
                                        }
                                },
                            SampleStoreConfigurations.StoreConfiguration)
                    },

                new object[]
                    {
                        "WithDrawingResourceDocument",
                        new GetResourceTest<Drawing>(
                            ClrSampleData.ServiceModelWithDrawingResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/drawings/1"}
                                        },
                                    Data = new Resource
                                        {
                                            Type = ClrSampleData.DrawingType,
                                            Id = "1",
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
                                                    {Keywords.Self, "http://api.example.com/drawings/1"},
                                                },
                                        }
                                },
                            SampleDrawings.Drawing)
                    },

                new object[]
                    {
                        "WithPosSystemAndNonNullEndOfLifeDateResourceDocument",
                        new GetResourceTest<PosSystem>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/pos-systems/RadiantWcf"}
                                        },
                                    Data = new Resource
                                        {
                                            Type = ClrSampleData.PosSystemType,
                                            Id = "RadiantWcf",
                                            Attributes = new ApiObject(
                                                ApiProperty.Create("pos-system-name", "Radiant WCF-Based Api"),
                                                ApiProperty.Create("end-of-life-date", new DateTime(1999, 12, 31))),
                                            Links = new Links
                                                {
                                                    {Keywords.Self, "http://api.example.com/pos-systems/RadiantWcf"},
                                                },
                                        }
                                },
                            SamplePosSystems.PosSystemRadiantWcf)
                    },

                new object[]
                    {
                        "WithPosSystemAndNullEndOfLifeDateResourceDocument",
                        new GetResourceTest<PosSystem>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, "http://api.example.com/pos-systems/RadiantRest"}
                                        },
                                    Data = new Resource
                                        {
                                            Type = ClrSampleData.PosSystemType,
                                            Id = "RadiantRest",
                                            Attributes = new ApiObject(
                                                ApiProperty.Create("pos-system-name", "Radiant REST-Based Api"),
                                                ApiProperty.Create("end-of-life-date", new DateTime?())),
                                            Links = new Links
                                                {
                                                    {Keywords.Self, "http://api.example.com/pos-systems/RadiantRest"},
                                                },
                                        }
                                },
                            SamplePosSystems.PosSystemRadiantRest)
                    },
            };
        #endregion

        #region GetResourceByResourceId TestData
        public static readonly IEnumerable<object[]> GetResourceByResourceIdTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceByResourceIdTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new Document
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        }
                                },
                            ApiSampleData.ArticleId,
                            null)
                    },
                new object[]
                    {
                        "WithNullDocument",
                        new GetResourceByResourceIdTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new NullDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        }
                                },
                            ApiSampleData.ArticleId,
                            null)
                    },
                new object[]
                    {
                        "WithArticleResourceDocument",
                        new GetResourceByResourceIdTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResource
                                },
                            ApiSampleData.ArticleId,
                            SampleArticles.Article)
                    },
                new object[]
                    {
                        "WithArticleResourceDocumentAndIncludedResourcesForArticle",
                        new GetResourceByResourceIdTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
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
                            ApiSampleData.ArticleId,
                            SampleArticles.ArticleWithResourceLinkage)
                    },
                new object[]
                    {
                        "WithArticleResourceDocumentAndIncludedResourcesForPerson",
                        new GetResourceByResourceIdTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
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
                            ApiSampleData.PersonId,
                            SamplePersons.Person)
                    },
                new object[]
                    {
                        "WithArticleResourceDocumentAndIncludedResourcesForComment",
                        new GetResourceByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
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
                            ApiSampleData.CommentId2,
                            SampleComments.Comment2)
                    },
                new object[]
                    {
                        "WithBlogResourceDocument",
                        new GetResourceByResourceIdTest<Blog, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.BlogLink}
                                        },
                                    Data = ApiSampleData.BlogResource
                                },
                            ApiSampleData.BlogId,
                            SampleBlogs.Blog)
                    },
                new object[]
                    {
                        "WithCommentResourceDocument",
                        new GetResourceByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.CommentLink}
                                        },
                                    Data = ApiSampleData.CommentResource
                                },
                            ApiSampleData.CommentId,
                            SampleComments.Comment)
                    },
                new object[]
                    {
                        "WithPersonResourceDocument",
                        new GetResourceByResourceIdTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.PersonLink}
                                        },
                                    Data = ApiSampleData.PersonResource
                                },
                            ApiSampleData.PersonId,
                            SamplePersons.Person)
                    },
            };
        #endregion

        #region GetResourceCollection TestData
        public static readonly IEnumerable<object[]> GetResourceCollectionTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceCollectionTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new Document
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceCollectionTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new EmptyDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocument",
                        new GetResourceCollectionTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new List<Article> {SampleArticles.Article1, SampleArticles.Article2})
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocumentAndIncludedResourcesGetArticles",
                        new GetResourceCollectionTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new List<Article> {SampleArticles.ArticleWithResourceLinkage1, SampleArticles.ArticleWithResourceLinkage2})
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocumentAndIncludedResourcesGetComments",
                        new GetResourceCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new List<Comment> {SampleComments.Comment1, SampleComments.Comment2, SampleComments.Comment3, SampleComments.Comment4})
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocumentAndIncludedResourcesGetPersons",
                        new GetResourceCollectionTest<Person>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new List<Person> {SamplePersons.Person1, SamplePersons.Person2})
                    },
                new object[]
                    {
                        "WithBlogResourceCollectionDocument",
                        new GetResourceCollectionTest<Blog>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.BlogCollectionLink}
                                        },
                                    Data = new List<Resource>
                                        {
                                            ApiSampleData.BlogResource1,
                                            ApiSampleData.BlogResource2
                                        }
                                },
                            new List<Blog> {SampleBlogs.Blog1, SampleBlogs.Blog2})
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentForComment",
                        new GetResourceCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.CommentCollectionLink}
                                        },
                                    Data = new List<Resource>
                                        {
                                            ApiSampleData.CommentResource1,
                                            ApiSampleData.CommentResource2
                                        }
                                },
                            new List<Comment> {SampleComments.Comment1, SampleComments.Comment2})
                    },
                new object[]
                    {
                        "WithPersonResourceCollectionDocument",
                        new GetResourceCollectionTest<Person>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.PersonCollectionLink}
                                        },
                                    Data = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource1,
                                            ApiSampleData.PersonResource2
                                        }
                                },
                            new List<Person> {SamplePersons.Person1, SamplePersons.Person2})
                    },
                new object[]
                    {
                        "WithPaymentResourceCollectionDocument",
                        new GetResourceCollectionTest<Payment>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
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
                            new List<Payment> {SamplePayments.Payment101, SamplePayments.Payment102})
                    },
                new object[]
                    {
                        "WithPaymentResourceCollectionDocumentGetOrders",
                        new GetResourceCollectionTest<Order>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
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
                            new List<Order>())
                    },
                new object[]
                    {
                        "WithOrderResourceDocumentWithAllIncludedResourcesGetOrderItems",
                        new GetResourceCollectionTest<OrderItem>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            SampleDocuments.OrderResourceDocumentWithAllIncludedResources,
                            new List<OrderItem> { SampleOrderItems.OrderItem1001, SampleOrderItems.OrderItem1002 })
                    },
                new object[]
                    {
                        "WithOrderResourceDocumentWithAllIncludedResourcesGetPayments",
                        new GetResourceCollectionTest<Payment>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            SampleDocuments.OrderResourceDocumentWithAllIncludedResources,
                            new List<Payment> { SamplePayments.Payment101, SamplePayments.Payment102 })
                    },
                new object[]
                    {
                        "WithOrderResourceDocumentWithAllIncludedResourcesGetProducts",
                        new GetResourceCollectionTest<Product>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            SampleDocuments.OrderResourceDocumentWithAllIncludedResources,
                            new List<Product> { SampleProducts.Product501, SampleProducts.Product502 })
                    },
                new object[]
                    {
                        "WithOrderResourceDocumentWithAllIncludedResourcesGetStoreConfiguration",
                        new GetResourceCollectionTest<StoreConfiguration>(
                            ClrSampleData.ServiceModelWithOrderResourceTypes,
                            SampleDocuments.OrderResourceDocumentWithAllIncludedResources,
                            new List<StoreConfiguration> { SampleStoreConfigurations.StoreConfiguration })
                    },
            };
        #endregion

        #region GetResourceId TestData
        public static readonly IEnumerable<object[]> GetResourceIdTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceIdTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new Document
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithNullDocument",
                        new GetResourceIdTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new NullDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithArticleResourceDocument",
                        new GetResourceIdTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResource
                                },
                            ApiSampleData.ArticleId)
                    },
                new object[]
                    {
                        "WithArticleResourceDocumentAndIncludedResourcesForArticle",
                        new GetResourceIdTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
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
                            ApiSampleData.ArticleId)
                    },
                new object[]
                    {
                        "WithArticleResourceDocumentAndIncludedResourcesForPerson",
                        new GetResourceIdTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
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
                            ApiSampleData.PersonId)
                    },
                new object[]
                    {
                        "WithArticleResourceIdentifierDocument",
                        new GetResourceIdTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResourceIdentifier
                                },
                            ApiSampleData.ArticleId)
                    },
                new object[]
                    {
                        "WithBlogResourceDocument",
                        new GetResourceIdTest<Blog, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.BlogLink}
                                        },
                                    Data = ApiSampleData.BlogResource
                                },
                            ApiSampleData.BlogId)
                    },
                new object[]
                    {
                        "WithBlogResourceIdentifierDocument",
                        new GetResourceIdTest<Blog, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.BlogLink}
                                        },
                                    Data = ApiSampleData.BlogResourceIdentifier
                                },
                            ApiSampleData.BlogId)
                    },
                new object[]
                    {
                        "WithCommentResourceDocument",
                        new GetResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.CommentLink}
                                        },
                                    Data = ApiSampleData.CommentResource
                                },
                            ApiSampleData.CommentId)
                    },
                new object[]
                    {
                        "WithCommentResourceIdentifierDocument",
                        new GetResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.CommentLink}
                                        },
                                    Data = ApiSampleData.CommentResourceIdentifier
                                },
                            ApiSampleData.CommentId)
                    },
                new object[]
                    {
                        "WithPersonResourceDocument",
                        new GetResourceIdTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.PersonLink}
                                        },
                                    Data = ApiSampleData.PersonResource
                                },
                            ApiSampleData.PersonId)
                    },
                new object[]
                    {
                        "WithPersonResourceIdentifierDocument",
                        new GetResourceIdTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.PersonLink}
                                        },
                                    Data = ApiSampleData.PersonResourceIdentifier
                                },
                            ApiSampleData.PersonId)
                    },
            };
        #endregion

        #region GetResourceIdCollection TestData
        public static readonly IEnumerable<object[]> GetResourceIdCollectionTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceIdCollectionTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new Document
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceIdCollectionTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new EmptyDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocument",
                        new GetResourceIdCollectionTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new []{ ApiSampleData.ArticleId1, ApiSampleData.ArticleId2})
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocumentAndIncludedResourcesForArticle",
                        new GetResourceIdCollectionTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new []{ ApiSampleData.ArticleId1, ApiSampleData.ArticleId2})
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocumentAndIncludedResourcesForComment",
                        new GetResourceIdCollectionTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4})
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocumentAndIncludedResourcesForPerson",
                        new GetResourceIdCollectionTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new []{ ApiSampleData.PersonId1, ApiSampleData.PersonId2})
                    },
                new object[]
                    {
                        "WithArticleResourceIdentifierCollectionDocument",
                        new GetResourceIdCollectionTest<Article, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        },
                                    Data = new List<ResourceIdentifier>
                                        {
                                            ApiSampleData.ArticleResourceIdentifier1,
                                            ApiSampleData.ArticleResourceIdentifier2
                                        }
                                },
                            new []{ ApiSampleData.ArticleId1, ApiSampleData.ArticleId2})
                    },
                new object[]
                    {
                        "WithBlogResourceCollectionDocument",
                        new GetResourceIdCollectionTest<Blog, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.BlogCollectionLink}
                                        },
                                    Data = new List<Resource>
                                        {
                                            ApiSampleData.BlogResource1,
                                            ApiSampleData.BlogResource2
                                        }
                                },
                            new []{ ApiSampleData.BlogId1, ApiSampleData.BlogId2})
                    },
                new object[]
                    {
                        "WithBlogsResourceIdentifierCollectionDocument",
                        new GetResourceIdCollectionTest<Blog, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.BlogCollectionLink}
                                        },
                                    Data = new List<ResourceIdentifier>
                                        {
                                            ApiSampleData.BlogResourceIdentifier1,
                                            ApiSampleData.BlogResourceIdentifier2
                                        }
                                },
                            new []{ ApiSampleData.BlogId1, ApiSampleData.BlogId2})
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocument",
                        new GetResourceIdCollectionTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.CommentCollectionLink}
                                        },
                                    Data = new List<Resource>
                                        {
                                            ApiSampleData.CommentResource1,
                                            ApiSampleData.CommentResource2
                                        }
                                },
                            new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2})
                    },
                new object[]
                    {
                        "WithCommentResourceIdentifierCollectionDocument",
                        new GetResourceIdCollectionTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.CommentCollectionLink}
                                        },
                                    Data = new List<ResourceIdentifier>
                                        {
                                            ApiSampleData.CommentResourceIdentifier1,
                                            ApiSampleData.CommentResourceIdentifier2
                                        }
                                },
                            new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2})
                    },
                new object[]
                    {
                        "WithPersonResourceCollectionDocument",
                        new GetResourceIdCollectionTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.PersonCollectionLink}
                                        },
                                    Data = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource1,
                                            ApiSampleData.PersonResource2
                                        }
                                },
                            new []{ ApiSampleData.PersonId1, ApiSampleData.PersonId2})
                    },
                new object[]
                    {
                        "WithPersonsResourceIdentifierCollectionDocument",
                        new GetResourceIdCollectionTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.PersonCollectionLink}
                                        },
                                    Data = new List<ResourceIdentifier>
                                        {
                                            ApiSampleData.PersonResourceIdentifier1,
                                            ApiSampleData.PersonResourceIdentifier2
                                        }
                                },
                            new []{ ApiSampleData.PersonId1, ApiSampleData.PersonId2})
                    },
            };
        #endregion

        #region GetResourceMeta TestData
        public static readonly IEnumerable<object[]> GetResourceMetaTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceMetaTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            null)
                    },
                new object[]
                    {
                        "WithNullDocument",
                        new GetResourceMetaTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            NullDocument.Empty,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceDocument",
                        new GetResourceMetaTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = new Resource
                                        {
                                            Type = ApiSampleData.CommentType,
                                            Id = ApiSampleData.CommentId,
                                            Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely."))
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceDocumentAndMeta",
                        new GetResourceMetaTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = new Resource
                                        {
                                            Type = ApiSampleData.CommentType,
                                            Id = ApiSampleData.CommentId,
                                            Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")),
                                            Meta = ApiSampleData.ResourceMeta
                                        }
                                },
                            ApiSampleData.ResourceMeta)
                    },
                new object[]
                    {
                        "WithCommentResourceIdentifierDocument",
                        new GetResourceMetaTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierDocument
                                {
                                    Data = new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceIdentifierDocumentAndMeta",
                        new GetResourceMetaTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierDocument
                                {
                                    Data = new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId, Meta = ApiSampleData.ResourceMeta }
                                },
                            ApiSampleData.ResourceMeta)
                    },
            };
        #endregion

        #region GetResourceMetaByResource TestData
        public static readonly IEnumerable<object[]> GetResourceMetaByResourceTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceMetaByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            SampleComments.Comment4,
                            null)
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceMetaByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            EmptyDocument.Empty,
                            SampleComments.Comment4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocument",
                        new GetResourceMetaByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")) },
                                        }
                                },
                            SampleComments.Comment4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndMeta",
                        new GetResourceMetaByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Meta = ApiSampleData.ResourceMeta1 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Meta = ApiSampleData.ResourceMeta2 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")), Meta = ApiSampleData.ResourceMeta3 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Meta = ApiSampleData.ResourceMeta4 },
                                        }
                                },
                            SampleComments.Comment4,
                            ApiSampleData.ResourceMeta4)
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocument",
                        new GetResourceMetaByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Meta = ApiSampleData.DocumentMeta,
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                        }
                                },
                            SampleComments.Comment2,
                            null)
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocumentAndMeta",
                        new GetResourceMetaByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Meta = ApiSampleData.DocumentMeta,
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Meta = ApiSampleData.ResourceMeta1 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Meta = ApiSampleData.ResourceMeta2 },
                                        }
                                },
                            SampleComments.Comment2,
                            ApiSampleData.ResourceMeta2)
                    },
            };
        #endregion

        #region GetResourceMetaByResourceId TestData
        public static readonly IEnumerable<object[]> GetResourceMetaByResourceIdTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceMetaByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceMetaByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            EmptyDocument.Empty,
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocument",
                        new GetResourceMetaByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")) },
                                        }
                                },
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndMeta",
                        new GetResourceMetaByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Meta = ApiSampleData.ResourceMeta1 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Meta = ApiSampleData.ResourceMeta2 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")), Meta = ApiSampleData.ResourceMeta3 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Meta = ApiSampleData.ResourceMeta4 },
                                        }
                                },
                            ApiSampleData.CommentId4,
                            ApiSampleData.ResourceMeta4)
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocument",
                        new GetResourceMetaByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Meta = ApiSampleData.DocumentMeta,
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                        }
                                },
                            ApiSampleData.CommentId2,
                            null)
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocumentAndMeta",
                        new GetResourceMetaByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Meta = ApiSampleData.DocumentMeta,
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Meta = ApiSampleData.ResourceMeta1 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Meta = ApiSampleData.ResourceMeta2 },
                                        }
                                },
                            ApiSampleData.CommentId2,
                            ApiSampleData.ResourceMeta2)
                    },
                new object[]
                    {
                        "WithCommentResourceIdentifierCollectionDocument",
                        new GetResourceMetaByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Data = new List<ResourceIdentifier>
                                        {
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4 },
                                        }
                                },
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceIdentifierCollectionDocumentAndMeta",
                        new GetResourceMetaByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Data = new List<ResourceIdentifier>
                                        {
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Meta = ApiSampleData.ResourceMeta1 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Meta = ApiSampleData.ResourceMeta2 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Meta = ApiSampleData.ResourceMeta3 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Meta = ApiSampleData.ResourceMeta4 },
                                        }
                                },
                            ApiSampleData.CommentId4,
                            ApiSampleData.ResourceMeta4)
                    },
            };
        #endregion

        #region GetResourceMetaCollection TestData
        public static readonly IEnumerable<object[]> GetResourceMetaCollectionTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceMetaCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            Enumerable.Empty<Meta>())
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceMetaCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            EmptyDocument.Empty,
                            Enumerable.Empty<Meta>())
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndNoMeta",
                        new GetResourceMetaCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")) },
                                        }
                                },
                            new Meta[]{ null, null, null, null})
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndSomeMeta",
                        new GetResourceMetaCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Meta = ApiSampleData.ResourceMeta2 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Meta = ApiSampleData.ResourceMeta4 },
                                        }
                                },
                            new []{ null, ApiSampleData.ResourceMeta2, null, ApiSampleData.ResourceMeta4})
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndAllMeta",
                        new GetResourceMetaCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Meta = ApiSampleData.ResourceMeta1 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Meta = ApiSampleData.ResourceMeta2 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")), Meta = ApiSampleData.ResourceMeta3 },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Meta = ApiSampleData.ResourceMeta4 },
                                        }
                                },
                            new []{ ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2, ApiSampleData.ResourceMeta3, ApiSampleData.ResourceMeta4})
                    },
                new object[]
                    {
                        "WithCommentResourceIdentifierCollectionDocumentAndNoMeta",
                        new GetResourceMetaCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Data = new List<ResourceIdentifier>
                                        {
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4 },
                                        }
                                },
                            new Meta[]{ null, null, null, null})
                    },
                new object[]
                    {
                        "WithCommentResourceIdentifierCollectionDocumentAndSomeMeta",
                        new GetResourceMetaCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Data = new List<ResourceIdentifier>
                                        {
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Meta = ApiSampleData.ResourceMeta1 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Meta = ApiSampleData.ResourceMeta3 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4 },
                                        }
                                },
                            new []{ ApiSampleData.ResourceMeta1, null, ApiSampleData.ResourceMeta3, null})
                    },
                new object[]
                    {
                        "WithCommentResourceIdentifierCollectionDocumentAndAllMeta",
                        new GetResourceMetaCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Data = new List<ResourceIdentifier>
                                        {
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Meta = ApiSampleData.ResourceMeta1 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Meta = ApiSampleData.ResourceMeta2 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Meta = ApiSampleData.ResourceMeta3 },
                                            new ResourceIdentifier { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Meta = ApiSampleData.ResourceMeta4 },
                                        }
                                },
                            new []{ ApiSampleData.ResourceMeta1, ApiSampleData.ResourceMeta2, ApiSampleData.ResourceMeta3, ApiSampleData.ResourceMeta4})
                    },
            };
        #endregion

        #region GetResourceLinks TestData
        public static readonly IEnumerable<object[]> GetResourceLinksTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceLinksTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            null)
                    },
                new object[]
                    {
                        "WithNullDocument",
                        new GetResourceLinksTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            NullDocument.Empty,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceDocument",
                        new GetResourceLinksTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = new Resource
                                        {
                                            Type = ApiSampleData.CommentType,
                                            Id = ApiSampleData.CommentId1,
                                            Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely."))
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceDocumentAndLinks",
                        new GetResourceLinksTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = new Resource
                                        {
                                            Type = ApiSampleData.CommentType,
                                            Id = ApiSampleData.CommentId,
                                            Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")),
                                            Links = new Links { {Keywords.Self, ApiSampleData.CommentLink} }
                                        }
                                },
                            new Links { {Keywords.Self, ApiSampleData.CommentLink} })
                    },
            };
        #endregion

        #region GetResourceLinksByResource TestData
        public static readonly IEnumerable<object[]> GetResourceLinksByResourceTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceLinksByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            SampleComments.Comment4,
                            null)
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceLinksByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            EmptyDocument.Empty,
                            SampleComments.Comment4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocument",
                        new GetResourceLinksByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")) },
                                        }
                                },
                            SampleComments.Comment4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndLinks",
                        new GetResourceLinksByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink1} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink2} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink3} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink4} } },
                                        }
                                },
                            SampleComments.Comment4,
                            new Links { {Keywords.Self, ApiSampleData.CommentLink4} })
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocument",
                        new GetResourceLinksByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Meta = ApiSampleData.DocumentMeta,
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                        }
                                },
                            SampleComments.Comment2,
                            null)
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocumentAndLinks",
                        new GetResourceLinksByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Meta = ApiSampleData.DocumentMeta,
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink1} }  },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink2} } },
                                        }
                                },
                            SampleComments.Comment2,
                            new Links { {Keywords.Self, ApiSampleData.CommentLink2} })
                    },
            };
        #endregion

        #region GetResourceLinksByResourceId TestData
        public static readonly IEnumerable<object[]> GetResourceLinksByResourceIdTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceLinksByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceLinksByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            EmptyDocument.Empty,
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocument",
                        new GetResourceLinksByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")) },
                                        }
                                },
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndLinks",
                        new GetResourceLinksByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink1} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink2} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink3} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink4} } },
                                        }
                                },
                            ApiSampleData.CommentId4,
                            new Links { {Keywords.Self, ApiSampleData.CommentLink4} })
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocument",
                        new GetResourceLinksByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Meta = ApiSampleData.DocumentMeta,
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                        }
                                },
                            ApiSampleData.CommentId2,
                            null)
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocumentAndLinks",
                        new GetResourceLinksByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Meta = ApiSampleData.DocumentMeta,
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink1} }  },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink2} } },
                                        }
                                },
                            ApiSampleData.CommentId2,
                            new Links { {Keywords.Self, ApiSampleData.CommentLink2} })
                    },
            };
        #endregion

        #region GetResourceLinksCollection TestData
        public static readonly IEnumerable<object[]> GetResourceLinksCollectionTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceLinksCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            Enumerable.Empty<Links>())
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceLinksCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            EmptyDocument.Empty,
                            Enumerable.Empty<Links>())
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndNoLinks",
                        new GetResourceLinksCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")) },
                                        }
                                },
                            new Links[]{ null, null, null, null})
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndSomeLinks",
                        new GetResourceLinksCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink2} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink4} } },
                                        }
                                },
                            new []
                                {
                                    null,
                                    new Links { {Keywords.Self, ApiSampleData.CommentLink2} },
                                    null,
                                    new Links { {Keywords.Self, ApiSampleData.CommentLink4} }
                                })
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndAllLinks",
                        new GetResourceLinksCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink1} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink2} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink3} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Links = new Links { {Keywords.Self, ApiSampleData.CommentLink4} } },
                                        }
                                },
                            new []
                                {
                                    new Links { {Keywords.Self, ApiSampleData.CommentLink1} },
                                    new Links { {Keywords.Self, ApiSampleData.CommentLink2} },
                                    new Links { {Keywords.Self, ApiSampleData.CommentLink3} },
                                    new Links { {Keywords.Self, ApiSampleData.CommentLink4} }
                                })
                    },
            };
        #endregion

        #region GetResourceRelationships TestData
        public static readonly IEnumerable<object[]> GetResourceRelationshipsTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceRelationshipsTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            null)
                    },
                new object[]
                    {
                        "WithNullDocument",
                        new GetResourceRelationshipsTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            NullDocument.Empty,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceDocument",
                        new GetResourceRelationshipsTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceDocumentAndRelationships",
                        new GetResourceRelationshipsTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1} } }
                                },
                            new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1} })
                    },
                new object[]
                {
                    "WithCommentResourceDocumentAndRelationshipsMinusId",
                    new GetResourceRelationshipsTest<Comment>(
                        ClrSampleData.ServiceModelWithBlogResourceTypes,
                        new ResourceDocument
                        {
                            Data = new Resource
                            {
                                Type = ApiSampleData.CommentType,
                                Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")),
                                Relationships = new Relationships
                                {
                                    {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1}
                                }
                            }
                        },
                        new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1} })
                },
            };
        #endregion

        #region GetResourceRelationshipsByResource TestData
        public static readonly IEnumerable<object[]> GetResourceRelationshipsByResourceTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceRelationshipsByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            SampleComments.Comment4,
                            null)
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceRelationshipsByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            EmptyDocument.Empty,
                            SampleComments.Comment4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocument",
                        new GetResourceRelationshipsByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")) },
                                        }
                                },
                            SampleComments.Comment4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndRelationships",
                        new GetResourceRelationshipsByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship3} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4} } },
                                        }
                                },
                            SampleComments.Comment4,
                            new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4} })
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocument",
                        new GetResourceRelationshipsByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                        }
                                },
                            SampleComments.Comment2,
                            null)
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocumentAndRelationships",
                        new GetResourceRelationshipsByResourceTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} } },
                                        }
                                },
                            SampleComments.Comment2,
                            new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} })
                    },
            };
        #endregion

        #region GetResourceRelationshipsByResourceId TestData
        public static readonly IEnumerable<object[]> GetResourceRelationshipsByResourceIdTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceRelationshipsByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceRelationshipsByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            EmptyDocument.Empty,
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocument",
                        new GetResourceRelationshipsByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")) },
                                        }
                                },
                            ApiSampleData.CommentId4,
                            null)
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndRelationships",
                        new GetResourceRelationshipsByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship3} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4} } },
                                        }
                                },
                            ApiSampleData.CommentId4,
                            new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4} })
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocument",
                        new GetResourceRelationshipsByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                        }
                                },
                            ApiSampleData.CommentId2,
                            null)
                    },
                new object[]
                    {
                        "WithArticleAndIncludedPersonAndCommentsResourceDocumentAndRelationships",
                        new GetResourceRelationshipsByResourceIdTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Data = ApiSampleData.ArticleResourceWithResourceLinkage,
                                    Included = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource,
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} } },
                                        }
                                },
                            ApiSampleData.CommentId2,
                            new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} })
                    },
            };
        #endregion

        #region GetResourceRelationshipsCollection TestData
        public static readonly IEnumerable<object[]> GetResourceRelationshipsCollectionTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceRelationshipsCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty,
                            Enumerable.Empty<Relationships>())
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceRelationshipsCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            EmptyDocument.Empty,
                            Enumerable.Empty<Relationships>())
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndNoRelationships",
                        new GetResourceRelationshipsCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")) },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")) },
                                        }
                                },
                            new Relationships[]{ null, null, null, null})
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndSomeRelationships",
                        new GetResourceRelationshipsCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely."))},
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?"))},
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4} } },
                                        }
                                },
                            new []
                                {
                                    null,
                                    new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} },
                                    null,
                                    new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4} }
                                })
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocumentAndAllRelationships",
                        new GetResourceRelationshipsCollectionTest<Comment>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Data = new List<Resource>
                                        {
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId1, Attributes = new ApiObject(ApiProperty.Create("body", "I disagree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId2, Attributes = new ApiObject(ApiProperty.Create("body", "I agree completely.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId3, Attributes = new ApiObject(ApiProperty.Create("body", "Is 42 the answer?")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship3} } },
                                            new Resource { Type = ApiSampleData.CommentType, Id = ApiSampleData.CommentId4, Attributes = new ApiObject(ApiProperty.Create("body", "42 is the answer according to 'Hitchhiker's Guide to the Galaxy'.")), Relationships = new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4} } },
                                        }
                                },
                            new []
                                {
                                    new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship1} },
                                    new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship2} },
                                    new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship3} },
                                    new Relationships { {ApiSampleData.CommentToAuthorRel, ApiSampleData.CommentToAuthorRelationship4} }
                                })
                    },
            };
        #endregion

        #region GetResourceTypeCollection TestData
        public static readonly IEnumerable<object[]> GetResourceTypeCollectionTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetResourceTypeCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new Document
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        },
                                },
                            null)
                    },
                new object[]
                    {
                        "WithEmptyDocument",
                        new GetResourceTypeCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new EmptyDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        },
                                },
                            null)
                    },
                new object[]
                    {
                        "WithNullDocument",
                        new GetResourceTypeCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new EmptyDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        },
                                },
                            null)
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocument",
                        new GetResourceTypeCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new List<Type> {typeof(Article)})
                    },
                new object[]
                    {
                        "WithArticleResourceCollectionDocumentAndIncludedResources",
                        new GetResourceTypeCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
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
                            new List<Type> {typeof(Article), typeof(Comment), typeof(Person)})
                    },
                new object[]
                    {
                        "WithBlogResourceCollectionDocument",
                        new GetResourceTypeCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        },
                                    Data = new List<Resource>
                                        {
                                            ApiSampleData.BlogResource1,
                                            ApiSampleData.BlogResource2
                                        }
                                },
                            new List<Type> {typeof(Blog)})
                    },
                new object[]
                    {
                        "WithCommentResourceCollectionDocument",
                        new GetResourceTypeCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.CommentCollectionLink}
                                        },
                                    Data = new List<Resource>
                                        {
                                            ApiSampleData.CommentResource1,
                                            ApiSampleData.CommentResource2
                                        }
                                },
                            new List<Type> {typeof(Comment)})
                    },
                new object[]
                    {
                        "WithPersonResourceCollectionDocument",
                        new GetResourceTypeCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.PersonCollectionLink}
                                        },
                                    Data = new List<Resource>
                                        {
                                            ApiSampleData.PersonResource1,
                                            ApiSampleData.PersonResource2
                                        }
                                },
                            new List<Type> {typeof(Person)})
                    },
            };
        #endregion

        #region GetErrorCollection TestData
        public static readonly IEnumerable<object[]> GetErrorCollectionTestData = new[]
            {
                new object[]
                    {
                        "WithDocument",
                        new GetErrorCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new Document
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        }
                                },
                            null)
                    },
                new object[]
                    {
                        "WithErrorsDocumentAndZeroErrors",
                        new GetErrorCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ErrorsDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        }
                                },
                            Enumerable.Empty<Error>())
                    },
                new object[]
                    {
                        "WithErrorsDocumentAndOneError",
                        new GetErrorCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ErrorsDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        },
                                    Errors = new List<Error>
                                        {
                                            ApiSampleData.Error,
                                        }
                                },
                            new []{ ApiSampleData.Error })
                    },
                new object[]
                    {
                        "WithErrorsDocumentAndTwoErrors",
                        new GetErrorCollectionTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ErrorsDocument
                                {
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
                            new []{ ApiSampleData.Error1, ApiSampleData.Error2})
                    },
            };
        #endregion

        #endregion

        #region Test Types
        public interface IDocumentReaderTest
        {
            void Arrange();
            void Act();
            void OutputTest(DocumentReaderTests parent);
            void AssertTest();
        }

        public class GetRelatedResourceCollectionTest<TResource, TRelatedResource> : IDocumentReaderTest
            where TResource : JsonObject, IGetRelationships
            where TRelatedResource : JsonObject
        {
            #region Constructors
            public GetRelatedResourceCollectionTest(IServiceModel serviceModel, Document document, string relationshipRel, IEnumerable<Tuple<TResource, IEnumerable<TRelatedResource>>> expected)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;
                this.RelationshipRel = relationshipRel;

                this.Expected = expected;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                var actual = this.DocumentReader.GetResourceCollection<TResource>()
                                 .Select(x =>
                                 {
                                     var actualResource = x;
                                     var actualRelationship = actualResource.GetRelationship(this.RelationshipRel);
                                     var actualRelatedResources = this.DocumentReader.GetRelatedResourceCollection<TRelatedResource>(actualRelationship);
                                     return new Tuple<TResource, IEnumerable<TRelatedResource>>(actualResource, actualRelatedResources);
                                 })
                                 .ToList();
                this.Actual = actual;
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;

                foreach (var expected in Expected)
                {
                    output.WriteLine("Expected {0} Resource JSON".FormatWith(typeof(TResource).Name));
                    var expectedResource = expected.Item1;
                    var expectedResourceAsJson = expectedResource.ToJson();
                    output.WriteLine(expectedResourceAsJson);

                    output.WriteLine("Expected Related {0} Resources JSON".FormatWith(typeof(TRelatedResource).Name));
                    var expectedRelatedResources = expected.Item2;
                    foreach (var expectedRelatedResource in expectedRelatedResources)
                    {
                        var expectedRelatedResourceAsJson = expectedRelatedResource.ToJson();
                        output.WriteLine(expectedRelatedResourceAsJson);
                    }
                }

                output.WriteLine(String.Empty);

                foreach (var actual in this.Actual)
                {
                    output.WriteLine("Actual {0} Resource JSON".FormatWith(typeof(TResource).Name));
                    var actualResource = actual.Item1;
                    var actualResourceAsJson = actualResource.ToJson();
                    output.WriteLine(actualResourceAsJson);

                    output.WriteLine("Actual Related {0} Resources JSON".FormatWith(typeof(TRelatedResource).Name));
                    var actualRelatedResources = actual.Item2;
                    foreach (var actualRelatedResource in actualRelatedResources)
                    {
                        var actualRelatedResourceAsJson = actualRelatedResource.ToJson();
                        output.WriteLine(actualRelatedResourceAsJson);
                    }
                }
            }

            public void AssertTest()
            {
                var expected = this.Expected.SafeToList();
                var actual = this.Actual.SafeToList();

                Assert.Equal(expected.Count, actual.Count);

                var count = expected.Count;
                for (var i = 0; i < count; ++i)
                {
                    var expectedResource = expected[i].Item1;
                    var expectedRelatedResources = expected[i].Item2;

                    var actualResource = actual[i].Item1;
                    var actualRelatedResources = actual[i].Item2;

                    ClrResourceAssert.Equal(expectedResource, actualResource);
                    ClrResourceAssert.Equal(expectedRelatedResources, actualRelatedResources);
                }
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }
            private string RelationshipRel { get; set; }

            private IEnumerable<Tuple<TResource, IEnumerable<TRelatedResource>>> Expected { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private IEnumerable<Tuple<TResource, IEnumerable<TRelatedResource>>> Actual { get; set; }
            #endregion
        }

        public class GetRelatedResourceTest<TResource, TRelatedResource> : IDocumentReaderTest
            where TResource : JsonObject, IGetRelationships
            where TRelatedResource : JsonObject
        {
            #region Constructors
            public GetRelatedResourceTest(IServiceModel serviceModel, Document document, string relationshipRel, IEnumerable<Tuple<TResource, TRelatedResource>> expected)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;
                this.RelationshipRel = relationshipRel;

                this.Expected = expected;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                var actual = this.DocumentReader.GetResourceCollection<TResource>()
                                 .Select(x =>
                                     {
                                         var actualResource = x;
                                         var actualRelationship = actualResource.GetRelationship(this.RelationshipRel);
                                         var actualRelatedResource = this.DocumentReader.GetRelatedResource<TRelatedResource>(actualRelationship);
                                         return new Tuple<TResource, TRelatedResource>(actualResource, actualRelatedResource);
                                     })
                                 .ToList();
                this.Actual = actual;
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;

                foreach (var expected in Expected)
                {
                    output.WriteLine("Expected {0} Resource JSON".FormatWith(typeof(TResource).Name));
                    var expectedResource = expected.Item1;
                    var expectedResourceAsJson = expectedResource.ToJson();
                    output.WriteLine(expectedResourceAsJson);

                    output.WriteLine("Expected Related {0} Resource JSON".FormatWith(typeof(TRelatedResource).Name));
                    var expectedRelatedResource = expected.Item2;
                    var expectedRelatedResourceAsJson = expectedRelatedResource.ToJson();
                    output.WriteLine(expectedRelatedResourceAsJson);
                }

                output.WriteLine(String.Empty);

                foreach (var actual in this.Actual)
                {
                    output.WriteLine("Actual {0} Resource JSON".FormatWith(typeof(TResource).Name));
                    var actualResource = actual.Item1;
                    var actualResourceAsJson = actualResource.ToJson();
                    output.WriteLine(actualResourceAsJson);

                    output.WriteLine("Actual Related {0} Resource JSON".FormatWith(typeof(TRelatedResource).Name));
                    var actualRelatedResource = actual.Item2;
                    var actualRelatedResourceAsJson = actualRelatedResource.ToJson();
                    output.WriteLine(actualRelatedResourceAsJson);
                }
            }

            public void AssertTest()
            {
                var expected = this.Expected.SafeToList();
                var actual = this.Actual.SafeToList();

                Assert.Equal(expected.Count, actual.Count);

                var count = expected.Count;
                for (var i = 0; i < count; ++i)
                {
                    var expectedResource = expected[i].Item1;
                    var expectedRelatedResource = expected[i].Item2;

                    var actualResource = actual[i].Item1;
                    var actualRelatedResource = actual[i].Item2;

                    ClrResourceAssert.Equal(expectedResource, actualResource);
                    ClrResourceAssert.Equal(expectedRelatedResource, actualRelatedResource);
                }
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }
            private string RelationshipRel { get; set; }

            private IEnumerable<Tuple<TResource, TRelatedResource>> Expected { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private IEnumerable<Tuple<TResource, TRelatedResource>> Actual { get; set; }
            #endregion
        }

        public class GetResourceTest<TResource> : IDocumentReaderTest
            where TResource : JsonObject
        {
            #region Constructors
            public GetResourceTest(IServiceModel serviceModel, Document document, TResource expectedResource)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;
                this.ExpectedResource = expectedResource;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResource = this.DocumentReader
                                          .GetResource<TResource>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;

                OutputTest(output, this.ExpectedResource, this.ActualResource);
            }

            public void AssertTest()
            {
                ClrResourceAssert.Equal(this.ExpectedResource, this.ActualResource);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private TResource ExpectedResource { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private TResource ActualResource { get; set; }
            #endregion

            #region Private Methods
            private static void OutputTest(ITestOutputHelper output, TResource expectedResource, TResource actualResource)
            {
                var typeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource JSON".FormatWith(typeName));
                if (expectedResource != null)
                {
                    var expectedResourceAsJson = expectedResource.ToJson();
                    output.WriteLine(expectedResourceAsJson);
                }

                output.WriteLine(String.Empty);

                output.WriteLine("Actual {0} Resource JSON".FormatWith(typeName));
                if (actualResource != null)
                {
                    var actualResourceAsJson = actualResource.ToJson();
                    output.WriteLine(actualResourceAsJson);
                }
            }
            #endregion
        }

        public class GetResourceByResourceIdTest<TResource, TResourceId> : IDocumentReaderTest
            where TResource : JsonObject
        {
            #region Constructors
            public GetResourceByResourceIdTest(IServiceModel serviceModel, Document document, TResourceId resourceId, TResource expectedResource)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;
                this.ResourceId = resourceId;
                this.ExpectedResource = expectedResource;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                var resourceId = this.ResourceId;
                this.ActualResource = this.DocumentReader
                                          .GetResource<TResource, TResourceId>(resourceId);
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;

                OutputTest(output, this.ResourceId, this.ExpectedResource, this.ActualResource);
            }

            public void AssertTest()
            {
                ClrResourceAssert.Equal(this.ExpectedResource, this.ActualResource);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private TResourceId ResourceId { get; set; }
            private TResource ExpectedResource { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private TResource ActualResource { get; set; }
            #endregion

            #region Private Methods
            private static void OutputTest(ITestOutputHelper output, TResourceId resourceId, TResource expectedResource, TResource actualResource)
            {
                var typeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource By Identifier {1} JSON".FormatWith(typeName, resourceId));
                if (expectedResource != null)
                {
                    var expectedResourceAsJson = expectedResource.ToJson();
                    output.WriteLine(expectedResourceAsJson);
                }

                output.WriteLine(String.Empty);

                output.WriteLine("Actual {0} Resource By Identifier {1} JSON".FormatWith(typeName, resourceId));
                if (actualResource != null)
                {
                    var actualResourceAsJson = actualResource.ToJson();
                    output.WriteLine(actualResourceAsJson);
                }
            }
            #endregion
        }

        public class GetResourceCollectionTest<TResource> : IDocumentReaderTest
            where TResource : JsonObject
        {
            #region Constructors
            public GetResourceCollectionTest(IServiceModel serviceModel, Document document, IEnumerable<TResource> expectedResourceCollection)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;
                this.ExpectedResourceCollection = expectedResourceCollection.EmptyIfNull();
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceCollection = this.DocumentReader
                                                    .GetResourceCollection<TResource>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;

                OutputTest(output, this.ExpectedResourceCollection, this.ActualResourceCollection);
            }

            public void AssertTest()
            {
                ClrResourceAssert.Equal(this.ExpectedResourceCollection, this.ActualResourceCollection);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private IEnumerable<TResource> ExpectedResourceCollection { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }
            private IEnumerable<TResource> ActualResourceCollection { get; set; }
            #endregion

            #region Private Methods
            private static void OutputTest(ITestOutputHelper output, IEnumerable<TResource> expectedResources, IEnumerable<TResource> actualResources)
            {
                var typeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resources JSON".FormatWith(typeName));
                foreach (var expectedResourceAsJson in expectedResources.Select(expectedResource => expectedResource.ToJson()))
                {
                    output.WriteLine(expectedResourceAsJson);
                }

                output.WriteLine(String.Empty);

                output.WriteLine("Actual {0} Resources JSON".FormatWith(typeName));
                foreach (var actualResourceAsJson in actualResources.Select(actualResource => actualResource.ToJson()))
                {
                    output.WriteLine(actualResourceAsJson);
                }
            }
            #endregion
        }

        public class GetResourceIdTest<TResource, TResourceId> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceIdTest(IServiceModel serviceModel, Document document, TResourceId expectedResourceId)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ExpectedResourceId = expectedResourceId;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceId = this.DocumentReader
                                            .GetResourceId<TResource, TResourceId>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource Identifier".FormatWith(resourceTypeName));

                var expectedIdAsString = !Object.ReferenceEquals(this.ExpectedResourceId, default(TResourceId))
                    ? this.ExpectedResourceId.ToString() : "null";
                output.WriteLine(expectedIdAsString);

                output.WriteLine(String.Empty);

                output.WriteLine("Actual {0} Resource Identifier".FormatWith(resourceTypeName));

                var actualIdAsString = !Object.ReferenceEquals(this.ActualResourceId, default(TResourceId))
                    ? this.ExpectedResourceId.ToString() : "null";
                output.WriteLine(actualIdAsString);
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedResourceId, this.ActualResourceId);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private TResourceId ExpectedResourceId { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private TResourceId ActualResourceId { get; set; }
            #endregion
        }

        public class GetResourceIdCollectionTest<TResource, TResourceId> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceIdCollectionTest(IServiceModel serviceModel, Document document, IEnumerable<TResourceId> expectedResourceIds)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ExpectedResourceIds = expectedResourceIds.EmptyIfNull();
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceIds = this.DocumentReader
                                             .GetResourceIdCollection<TResource, TResourceId>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource Identifiers".FormatWith(resourceTypeName));
                foreach (var expectedResourceIdAsString in this.ExpectedResourceIds.Select(expectedResourceId => Convert.ToString(expectedResourceId)))
                {
                    output.WriteLine(expectedResourceIdAsString);
                }

                output.WriteLine(String.Empty);

                output.WriteLine("Actual {0} Resource Identifiers".FormatWith(resourceTypeName));
                foreach (var actualResourceIdAsString in this.ActualResourceIds.Select(expectedResourceId => Convert.ToString(expectedResourceId)))
                {
                    output.WriteLine(actualResourceIdAsString);
                }
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedResourceIds, this.ActualResourceIds);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private IEnumerable<TResourceId> ExpectedResourceIds { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private IEnumerable<TResourceId> ActualResourceIds { get; set; }
            #endregion
        }

        public class GetResourceMetaTest<TResource> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceMetaTest(IServiceModel serviceModel, Document document, Meta expectedResourceMeta)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ExpectedResourceMeta = expectedResourceMeta;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceMeta = this.DocumentReader
                                              .GetResourceMeta<TResource>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource Meta".FormatWith(resourceTypeName));
                output.WriteLine(this.ExpectedResourceMeta != null ? this.ExpectedResourceMeta.ToJson() : CoreStrings.NullText);

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Meta".FormatWith(resourceTypeName));
                output.WriteLine(this.ActualResourceMeta != null ? this.ActualResourceMeta.ToJson() : CoreStrings.NullText);
            }

            public void AssertTest()
            {
                MetaAssert.Equal(this.ExpectedResourceMeta, this.ActualResourceMeta);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private Meta ExpectedResourceMeta { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private Meta ActualResourceMeta { get; set; }
            #endregion
        }

        public class GetResourceMetaByResourceTest<TResource> : IDocumentReaderTest
            where TResource : class, IJsonObject 
        {
            #region Constructors
            public GetResourceMetaByResourceTest(IServiceModel serviceModel, Document document, TResource clrResourceToLookup, Meta expectedResourceMeta)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ClrResourceToLookup = clrResourceToLookup;
                this.ExpectedResourceMeta = expectedResourceMeta;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceMeta = this.DocumentReader
                                              .GetResourceMeta(this.ClrResourceToLookup);
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("{0} Resource To Lookup".FormatWith(resourceTypeName));
                output.WriteLine(this.ClrResourceToLookup.ToJson());

                parent.OutputEmptyLine();

                output.WriteLine("Expected {0} Resource Meta".FormatWith(resourceTypeName));
                output.WriteLine(this.ExpectedResourceMeta != null ? this.ExpectedResourceMeta.ToJson() : CoreStrings.NullText);

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Meta".FormatWith(resourceTypeName));
                output.WriteLine(this.ActualResourceMeta != null ? this.ActualResourceMeta.ToJson() : CoreStrings.NullText);
            }

            public void AssertTest()
            {
                MetaAssert.Equal(this.ExpectedResourceMeta, this.ActualResourceMeta);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private TResource ClrResourceToLookup { get; set; }
            private Meta ExpectedResourceMeta { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private Meta ActualResourceMeta { get; set; }
            #endregion
        }

        public class GetResourceMetaByResourceIdTest<TResource, TResourceId> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceMetaByResourceIdTest(IServiceModel serviceModel, Document document, TResourceId clrResourceIdToLookup, Meta expectedResourceMeta)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ClrResourceIdToLookup = clrResourceIdToLookup;
                this.ExpectedResourceMeta = expectedResourceMeta;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceMeta = this.DocumentReader
                                              .GetResourceMeta<TResource, TResourceId>(this.ClrResourceIdToLookup);
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("{0} ResourceId To Lookup".FormatWith(resourceTypeName));
                output.WriteLine(this.ClrResourceIdToLookup.ToString());

                parent.OutputEmptyLine();

                output.WriteLine("Expected {0} Resource Meta".FormatWith(resourceTypeName));
                output.WriteLine(this.ExpectedResourceMeta != null ? this.ExpectedResourceMeta.ToJson() : CoreStrings.NullText);

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Meta".FormatWith(resourceTypeName));
                output.WriteLine(this.ActualResourceMeta != null ? this.ActualResourceMeta.ToJson() : CoreStrings.NullText);
            }

            public void AssertTest()
            {
                MetaAssert.Equal(this.ExpectedResourceMeta, this.ActualResourceMeta);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private TResourceId ClrResourceIdToLookup { get; set; }
            private Meta ExpectedResourceMeta { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private Meta ActualResourceMeta { get; set; }
            #endregion
        }

        public class GetResourceMetaCollectionTest<TResource> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceMetaCollectionTest(IServiceModel serviceModel, Document document, IEnumerable<Meta> expectedResourceMetaCollection)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ExpectedResourceMetaCollection = expectedResourceMetaCollection.EmptyIfNull();
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceMetaCollection = this.DocumentReader
                                                        .GetResourceMetaCollection<TResource>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource Meta Collection".FormatWith(resourceTypeName));
                foreach (var resourceMetaJson in this.ExpectedResourceMetaCollection.Select(resourceMeta => resourceMeta != null ? resourceMeta.ToJson() : CoreStrings.NullText))
                {
                    output.WriteLine(resourceMetaJson);
                }

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Meta Collection".FormatWith(resourceTypeName));
                foreach (var resourceMetaJson in this.ActualResourceMetaCollection.Select(resourceMeta => resourceMeta != null ? resourceMeta.ToJson() : CoreStrings.NullText))
                {
                    output.WriteLine(resourceMetaJson);
                }
            }

            public void AssertTest()
            {
                MetaAssert.Equal(this.ExpectedResourceMetaCollection, this.ActualResourceMetaCollection);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private IEnumerable<Meta> ExpectedResourceMetaCollection { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private IEnumerable<Meta> ActualResourceMetaCollection { get; set; }
            #endregion
        }

        public class GetResourceLinksTest<TResource> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceLinksTest(IServiceModel serviceModel, Document document, Links expectedResourceLinks)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ExpectedResourceLinks = expectedResourceLinks;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceLinks = this.DocumentReader
                                               .GetResourceLinks<TResource>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource Links".FormatWith(resourceTypeName));
                output.WriteLine(this.ExpectedResourceLinks != null ? this.ExpectedResourceLinks.ToJson() : CoreStrings.NullText);

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Links".FormatWith(resourceTypeName));
                output.WriteLine(this.ActualResourceLinks != null ? this.ActualResourceLinks.ToJson() : CoreStrings.NullText);
            }

            public void AssertTest()
            {
                LinksAssert.Equal(this.ExpectedResourceLinks, this.ActualResourceLinks);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private Links ExpectedResourceLinks { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private Links ActualResourceLinks { get; set; }
            #endregion
        }

        public class GetResourceLinksByResourceTest<TResource> : IDocumentReaderTest
            where TResource : class, IJsonObject
        {
            #region Constructors
            public GetResourceLinksByResourceTest(IServiceModel serviceModel, Document document, TResource clrResourceToLookup, Links expectedResourceLinks)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ClrResourceToLookup = clrResourceToLookup;
                this.ExpectedResourceLinks = expectedResourceLinks;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceLinks = this.DocumentReader
                                              .GetResourceLinks(this.ClrResourceToLookup);
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("{0} Resource To Lookup".FormatWith(resourceTypeName));
                output.WriteLine(this.ClrResourceToLookup.ToJson());

                parent.OutputEmptyLine();

                output.WriteLine("Expected {0} Resource Links".FormatWith(resourceTypeName));
                output.WriteLine(this.ExpectedResourceLinks != null ? this.ExpectedResourceLinks.ToJson() : CoreStrings.NullText);

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Links".FormatWith(resourceTypeName));
                output.WriteLine(this.ActualResourceLinks != null ? this.ActualResourceLinks.ToJson() : CoreStrings.NullText);
            }

            public void AssertTest()
            {
                LinksAssert.Equal(this.ExpectedResourceLinks, this.ActualResourceLinks);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private TResource ClrResourceToLookup { get; set; }
            private Links ExpectedResourceLinks { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private Links ActualResourceLinks { get; set; }
            #endregion
        }

        public class GetResourceLinksByResourceIdTest<TResource, TResourceId> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceLinksByResourceIdTest(IServiceModel serviceModel, Document document, TResourceId clrResourceIdToLookup, Links expectedResourceLinks)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ClrResourceIdToLookup = clrResourceIdToLookup;
                this.ExpectedResourceLinks = expectedResourceLinks;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceLinks = this.DocumentReader
                                              .GetResourceLinks<TResource, TResourceId>(this.ClrResourceIdToLookup);
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("{0} ResourceId To Lookup".FormatWith(resourceTypeName));
                output.WriteLine(this.ClrResourceIdToLookup.ToString());

                parent.OutputEmptyLine();

                output.WriteLine("Expected {0} Resource Links".FormatWith(resourceTypeName));
                output.WriteLine(this.ExpectedResourceLinks != null ? this.ExpectedResourceLinks.ToJson() : CoreStrings.NullText);

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Links".FormatWith(resourceTypeName));
                output.WriteLine(this.ActualResourceLinks != null ? this.ActualResourceLinks.ToJson() : CoreStrings.NullText);
            }

            public void AssertTest()
            {
                LinksAssert.Equal(this.ExpectedResourceLinks, this.ActualResourceLinks);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private TResourceId ClrResourceIdToLookup { get; set; }
            private Links ExpectedResourceLinks { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private Links ActualResourceLinks { get; set; }
            #endregion
        }

        public class GetResourceLinksCollectionTest<TResource> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceLinksCollectionTest(IServiceModel serviceModel, Document document, IEnumerable<Links> expectedResourceLinksCollection)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ExpectedResourceLinksCollection = expectedResourceLinksCollection.EmptyIfNull();
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceLinksCollection = this.DocumentReader
                                                         .GetResourceLinksCollection<TResource>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource Links Collection".FormatWith(resourceTypeName));
                foreach (var resourceLinksJson in this.ExpectedResourceLinksCollection.Select(resourceLinks => resourceLinks != null ? resourceLinks.ToJson() : CoreStrings.NullText))
                {
                    output.WriteLine(resourceLinksJson);
                }

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Links Collection".FormatWith(resourceTypeName));
                foreach (var resourceLinksJson in this.ActualResourceLinksCollection.Select(resourceLinks => resourceLinks != null ? resourceLinks.ToJson() : CoreStrings.NullText))
                {
                    output.WriteLine(resourceLinksJson);
                }
            }

            public void AssertTest()
            {
                LinksAssert.Equal(this.ExpectedResourceLinksCollection, this.ActualResourceLinksCollection);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private IEnumerable<Links> ExpectedResourceLinksCollection { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private IEnumerable<Links> ActualResourceLinksCollection { get; set; }
            #endregion
        }

        public class GetResourceRelationshipsTest<TResource> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceRelationshipsTest(IServiceModel serviceModel, Document document, Relationships expectedResourceRelationships)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ExpectedResourceRelationships = expectedResourceRelationships;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceRelationships = this.DocumentReader
                                               .GetResourceRelationships<TResource>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource Relationships".FormatWith(resourceTypeName));
                output.WriteLine(this.ExpectedResourceRelationships != null ? this.ExpectedResourceRelationships.ToJson() : CoreStrings.NullText);

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Relationships".FormatWith(resourceTypeName));
                output.WriteLine(this.ActualResourceRelationships != null ? this.ActualResourceRelationships.ToJson() : CoreStrings.NullText);
            }

            public void AssertTest()
            {
                RelationshipsAssert.Equal(this.ExpectedResourceRelationships, this.ActualResourceRelationships);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private Relationships ExpectedResourceRelationships { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private Relationships ActualResourceRelationships { get; set; }
            #endregion
        }

        public class GetResourceRelationshipsByResourceTest<TResource> : IDocumentReaderTest
            where TResource : class, IJsonObject
        {
            #region Constructors
            public GetResourceRelationshipsByResourceTest(IServiceModel serviceModel, Document document, TResource clrResourceToLookup, Relationships expectedResourceRelationships)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ClrResourceToLookup = clrResourceToLookup;
                this.ExpectedResourceRelationships = expectedResourceRelationships;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceRelationships = this.DocumentReader
                                              .GetResourceRelationships(this.ClrResourceToLookup);
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("{0} Resource To Lookup".FormatWith(resourceTypeName));
                output.WriteLine(this.ClrResourceToLookup.ToJson());

                parent.OutputEmptyLine();

                output.WriteLine("Expected {0} Resource Relationships".FormatWith(resourceTypeName));
                output.WriteLine(this.ExpectedResourceRelationships != null ? this.ExpectedResourceRelationships.ToJson() : CoreStrings.NullText);

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Relationships".FormatWith(resourceTypeName));
                output.WriteLine(this.ActualResourceRelationships != null ? this.ActualResourceRelationships.ToJson() : CoreStrings.NullText);
            }

            public void AssertTest()
            {
                RelationshipsAssert.Equal(this.ExpectedResourceRelationships, this.ActualResourceRelationships);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private TResource ClrResourceToLookup { get; set; }
            private Relationships ExpectedResourceRelationships { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private Relationships ActualResourceRelationships { get; set; }
            #endregion
        }

        public class GetResourceRelationshipsByResourceIdTest<TResource, TResourceId> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceRelationshipsByResourceIdTest(IServiceModel serviceModel, Document document, TResourceId clrResourceIdToLookup, Relationships expectedResourceRelationships)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ClrResourceIdToLookup = clrResourceIdToLookup;
                this.ExpectedResourceRelationships = expectedResourceRelationships;
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceRelationships = this.DocumentReader
                                              .GetResourceRelationships<TResource, TResourceId>(this.ClrResourceIdToLookup);
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("{0} ResourceId To Lookup".FormatWith(resourceTypeName));
                output.WriteLine(this.ClrResourceIdToLookup.ToString());

                parent.OutputEmptyLine();

                output.WriteLine("Expected {0} Resource Relationships".FormatWith(resourceTypeName));
                output.WriteLine(this.ExpectedResourceRelationships != null ? this.ExpectedResourceRelationships.ToJson() : CoreStrings.NullText);

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Relationships".FormatWith(resourceTypeName));
                output.WriteLine(this.ActualResourceRelationships != null ? this.ActualResourceRelationships.ToJson() : CoreStrings.NullText);
            }

            public void AssertTest()
            {
                RelationshipsAssert.Equal(this.ExpectedResourceRelationships, this.ActualResourceRelationships);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private TResourceId ClrResourceIdToLookup { get; set; }
            private Relationships ExpectedResourceRelationships { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private Relationships ActualResourceRelationships { get; set; }
            #endregion
        }

        public class GetResourceRelationshipsCollectionTest<TResource> : IDocumentReaderTest
            where TResource : class
        {
            #region Constructors
            public GetResourceRelationshipsCollectionTest(IServiceModel serviceModel, Document document, IEnumerable<Relationships> expectedResourceRelationshipsCollection)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ExpectedResourceRelationshipsCollection = expectedResourceRelationshipsCollection.EmptyIfNull();
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualResourceRelationshipsCollection = this.DocumentReader
                                                         .GetResourceRelationshipsCollection<TResource>();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;
                var resourceTypeName = typeof(TResource).Name;

                output.WriteLine("Expected {0} Resource Relationships Collection".FormatWith(resourceTypeName));
                foreach (var resourceRelationshipsJson in this.ExpectedResourceRelationshipsCollection.Select(resourceRelationships => resourceRelationships != null ? resourceRelationships.ToJson() : CoreStrings.NullText))
                {
                    output.WriteLine(resourceRelationshipsJson);
                }

                parent.OutputEmptyLine();

                output.WriteLine("Actual {0} Resource Relationships Collection".FormatWith(resourceTypeName));
                foreach (var resourceRelationshipsJson in this.ActualResourceRelationshipsCollection.Select(resourceRelationships => resourceRelationships != null ? resourceRelationships.ToJson() : CoreStrings.NullText))
                {
                    output.WriteLine(resourceRelationshipsJson);
                }
            }

            public void AssertTest()
            {
                RelationshipsAssert.Equal(this.ExpectedResourceRelationshipsCollection, this.ActualResourceRelationshipsCollection);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private IEnumerable<Relationships> ExpectedResourceRelationshipsCollection { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private IEnumerable<Relationships> ActualResourceRelationshipsCollection { get; set; }
            #endregion
        }

        public class GetResourceTypeCollectionTest : IDocumentReaderTest
        {
            #region Constructors
            public GetResourceTypeCollectionTest(IServiceModel serviceModel, Document document, IEnumerable<Type> expectedTypeCollection)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;

                this.ExpectedTypeCollection = expectedTypeCollection.EmptyIfNull()
                                                                    .Distinct()
                                                                    .OrderBy(x => x.Name)
                                                                    .ToList();
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualTypeCollection = this.DocumentReader
                                                .GetResourceTypeCollection()
                                                .OrderBy(x => x.Name)
                                                .ToList();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;

                output.WriteLine("Expected Resource Type Collection");
                foreach (var expectedType in this.ExpectedTypeCollection)
                {
                    output.WriteLine(expectedType.Name);
                }

                output.WriteLine(String.Empty);

                output.WriteLine("Actual Resource Type Collection");
                foreach (var actualType in this.ActualTypeCollection)
                {
                    output.WriteLine(actualType.Name);
                }
            }

            public void AssertTest()
            {
                Assert.Equal(this.ExpectedTypeCollection, this.ActualTypeCollection);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }

            private IEnumerable<Type> ExpectedTypeCollection { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }

            private IEnumerable<Type> ActualTypeCollection { get; set; }
            #endregion
        }

        public class GetErrorCollectionTest : IDocumentReaderTest
        {
            #region Constructors
            public GetErrorCollectionTest(IServiceModel serviceModel, Document document, IEnumerable<Error> expectedErrorCollection)
            {
                this.ServiceModel = serviceModel;
                this.Document = document;
                this.ExpectedErrorCollection = expectedErrorCollection.EmptyIfNull();
            }
            #endregion

            #region IDocumentReaderTest Implementation
            public void Arrange()
            {
                var documentJson = this.Document.ToJson();
                this.Document = JsonObject.Parse<Document>(documentJson);

                this.DocumentReader = new DocumentReader(this.Document, this.ServiceModel);
            }

            public void Act()
            {
                this.ActualErrorCollection = this.DocumentReader
                                                 .GetErrorCollection();
            }

            public void OutputTest(DocumentReaderTests parent)
            {
                parent.OutputDomDocument(this.DocumentReader);

                parent.OutputJson("Document JSON", this.Document);
                parent.OutputEmptyLine();

                var output = parent.Output;

                OutputTest(output, this.ExpectedErrorCollection, this.ActualErrorCollection);
            }

            public void AssertTest()
            {
                ErrorAssert.Equal(this.ExpectedErrorCollection, this.ActualErrorCollection);
            }
            #endregion

            #region User Supplied Properties
            private IServiceModel ServiceModel { get; set; }
            private Document Document { get; set; }
            private IEnumerable<Error> ExpectedErrorCollection { get; set; }
            #endregion

            #region Calculated Properties
            private IDocumentReader DocumentReader { get; set; }
            private IEnumerable<Error> ActualErrorCollection { get; set; }
            #endregion

            #region Private Methods
            private static void OutputTest(ITestOutputHelper output, IEnumerable<Error> expectedErrorCollection, IEnumerable<Error> actualErrorCollection)
            {
                output.WriteLine("Expected Error Collection JSON");
                foreach (var expectedErrorAsJson in expectedErrorCollection.Select(expectedError => expectedError.ToJson()))
                {
                    output.WriteLine(expectedErrorAsJson);
                }

                output.WriteLine(String.Empty);

                output.WriteLine("Actual Error Collection JSON");
                foreach (var actualErrorAsJson in actualErrorCollection.Select(actualError => actualError.ToJson()))
                {
                    output.WriteLine(actualErrorAsJson);
                }
            }
            #endregion
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void OutputDomDocument(IDocumentReader documentReader)
        {
            var domDocument = ((DocumentReader)documentReader).DomDocument;
            var domDocumentTreeString = domDocument.ToTreeString();

            this.Output.WriteLine("DOM Tree");
            this.Output.WriteLine(domDocumentTreeString);
            this.OutputEmptyLine();
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

        private void OutputTestName(string testName)
        {
            this.Output.WriteLine("Test Name: {0}", testName);
        }

        private void TestDocumentReaderTest(string name, IDocumentReaderTest test)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();

            // Arrange
            test.Arrange();

            // Act
            test.Act();
            test.OutputTest(this);

            // Assert
            test.AssertTest();
        }
        #endregion
    }
}
