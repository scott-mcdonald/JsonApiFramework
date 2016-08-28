// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Client.Internal;
using JsonApiFramework.Internal;
using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Client.Tests.Internal
{
    public class DocumentBuilderTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentBuilderTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DocumentBuilderWriteDocumentTestData")]
        public void TestDocumentBuilderWriteDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData("DocumentBuilderWriteResourceDocumentTestData")]
        public void TestDocumentBuilderWriteResourceDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData("DocumentBuilderWriteResourceIdentifierCollectionDocumentTestData")]
        public void TestDocumentBuilderWriteResourceIdentifierCollectionDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData("DocumentBuilderWriteResourceIdentifierDocumentTestData")]
        public void TestDocumentBuilderWriteResourceIdentifierDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
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
            var getDomDocument = (IGetDomDocument)actualDocumentWriter;

            this.Output.WriteLine("Test Name: {0}", name);
            this.OutputEmptyLine();
            this.Output.WriteLine("Before WriteDocument Method DOM Tree");
            this.OutputEmptyLine();
            this.Output.WriteLine(getDomDocument.DomDocument.ToTreeString());
            this.OutputEmptyLine();

            var actualDocument = actualDocumentWriter.WriteDocument();

            this.Output.WriteLine("After WriteDocument Method DOM Tree");
            this.OutputEmptyLine();
            this.Output.WriteLine(getDomDocument.DomDocument.ToTreeString());
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
        private static class DocumentBuilderFactory
        {
            public static DocumentBuilder Create(IServiceModel serviceModel)
            {
                var documentWriter = new DocumentWriter(serviceModel);
                var documentBuilder = new DocumentBuilder(documentWriter);
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                    },
                new object[]
                    {
                        "WithMeta",
                        new Document
                            {
                                Meta = ApiSampleData.DocumentMeta
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                    },
            };
        #endregion

        #region DocumentBuilderWriteResourceDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteResourceDocumentTestData = new[]
            {
                // Resources Created for POST
                #region Resources Created for POST
                new object[]
                    {
                        "ForPostWithEmptyResource",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithEmptyResourceAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilder",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndToOneRelationshipNull",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {ApiSampleData.ArticleToAuthorRel, new ToOneRelationship()},
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndToOneRelationshipNullAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId()
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndToOneRelationship",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId)
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ApiSampleData.PersonId)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndToOneRelationshipAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId)
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId(ApiSampleData.PersonId)
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndToManyRelationshipEmpty",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship()
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndToManyRelationshipEmptyAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId()
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndToManyRelationship",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId1),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId3),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndToManyRelationshipAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId1),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId3),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId(ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4)
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromObject",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromObjectAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromObjectAndToOneRelationshipNull",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {ApiSampleData.ArticleToAuthorRel, new ToOneRelationship()},
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromObjectAndToOneRelationshipNullAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId()
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromObjectAndToOneRelationship",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId)
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ApiSampleData.PersonId)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromObjectAndToOneRelationshipAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId)
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId(ApiSampleData.PersonId)
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromObjectAndToManyRelationshipEmpty",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship()
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromObjectAndToManyRelationshipEmptyAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId()
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromObjectAndToManyRelationship",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId1),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId3),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPostWithResourceFromObjectAndToManyRelationshipAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId1),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId3),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId(ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4)
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                #endregion

                // Resources Created for PATCH
                #region Resources Created for PATCH
                new object[]
                    {
                        "ForPatchWithEmptyResource",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .SetId(ApiSampleData.ArticleId)
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithEmptyResourceAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(ApiSampleData.ArticleId)
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilder",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndComplexAttributes",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.StoreConfigurationType,
                                        Id = SampleStoreConfigurations.StoreConfiguration.StoreConfigurationId,
                                        Attributes = new ApiObject(
                                            ApiProperty.Create("is-live", SampleStoreConfigurations.StoreConfiguration.IsLive),
                                            ApiProperty.Create("mailing-address", SampleStoreConfigurations.StoreConfiguration.MailingAddress),
                                            ApiProperty.Create("phone-numbers", SampleStoreConfigurations.StoreConfiguration.PhoneNumbers)),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes)
                            .Resource<StoreConfiguration>()
                                .SetId(SampleStoreConfigurations.StoreConfiguration.StoreConfigurationId)
                                .Attributes()
                                    .AddAttribute(x => x.IsLive, SampleStoreConfigurations.StoreConfiguration.IsLive)
                                    .AddAttribute(x => x.MailingAddress, SampleStoreConfigurations.StoreConfiguration.MailingAddress)
                                    .AddAttribute(x => x.PhoneNumbers, SampleStoreConfigurations.StoreConfiguration.PhoneNumbers)
                                .AttributesEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndToOneRelationshipNull",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {ApiSampleData.ArticleToAuthorRel, new ToOneRelationship()},
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndToOneRelationshipNullAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId()
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndToOneRelationship",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId)
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ApiSampleData.PersonId)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndToOneRelationshipAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId)
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId(ApiSampleData.PersonId)
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndToManyRelationshipEmpty",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship()
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndToManyRelationshipEmptyAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId()
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndToManyRelationship",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId1),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId3),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource<Article>()
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndToManyRelationshipAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId1),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId3),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource<Article>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(ApiSampleData.ArticleId)
                                .Attributes()
                                    .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId(ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4)
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPatchWithResourceFromObject",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndComplexAttributes",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.StoreConfigurationType,
                                        Id = SampleStoreConfigurations.StoreConfiguration.StoreConfigurationId,
                                        Attributes = new ApiObject(
                                            ApiProperty.Create("is-live", SampleStoreConfigurations.StoreConfiguration.IsLive),
                                            ApiProperty.Create("mailing-address", SampleStoreConfigurations.StoreConfiguration.MailingAddress),
                                            ApiProperty.Create("phone-numbers", SampleStoreConfigurations.StoreConfiguration.PhoneNumbers)),
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes)
                            .Resource(SampleStoreConfigurations.StoreConfiguration)
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndToOneRelationshipNull",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {ApiSampleData.ArticleToAuthorRel, new ToOneRelationship()},
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndToOneRelationshipNullAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId()
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndToOneRelationship",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId)
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ApiSampleData.PersonId)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndToOneRelationshipAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId)
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId(ApiSampleData.PersonId)
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndToManyRelationshipEmpty",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship()
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndToManyRelationshipEmptyAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId()
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndToManyRelationship",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId1),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId3),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4)
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndToManyRelationshipAndMeta",
                        new ResourceDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Meta = ApiSampleData.RelationshipMeta,
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId1),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId3),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetId(ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4)
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                #endregion
            };
        #endregion

        #region DocumentBuilderWriteResourceIdentifierCollectionDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteResourceIdentifierCollectionDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyObject",
                        ResourceIdentifierCollectionDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifierCollection<Article>()
                    },
                new object[]
                    {
                        "WithNullResourceIdentifiers",
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifierCollection<Article>()
                    },
                new object[]
                    {
                        "WithEmptyResourceIdentifiers",
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifierCollection(Enumerable.Empty<Comment>())
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .ResourceIdentifierCollection<Comment>()
                                .SetId(ApiSampleData.CommentId1, ApiSampleData.CommentId2)
                            .ResourceIdentifierCollectionEnd()
                    },
                new object[]
                    {
                        "WithResourceIdentifiersAndMeta",
                        new ResourceIdentifierCollectionDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new List<ResourceIdentifier>
                                    {
                                        ApiSampleData.CommentResourceIdentifierWithMeta1,
                                        ApiSampleData.CommentResourceIdentifierWithMeta2
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .ResourceIdentifierCollection<Comment>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(ApiSampleData.CommentId1, ApiSampleData.CommentId2)
                            .ResourceIdentifierCollectionEnd()
                    },
                new object[]
                    {
                        "WithNullResources",
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifierCollection(default(Comment), default(Comment))
                    },
                new object[]
                    {
                        "WithEmptyResources",
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>()
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifierCollection(new Comment(), new Comment())
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifierCollection(SampleComments.Comment1, SampleComments.Comment2)
                    },
                new object[]
                    {
                        "WithResourcesAndMeta",
                        new ResourceIdentifierCollectionDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = new List<ResourceIdentifier>
                                    {
                                        ApiSampleData.CommentResourceIdentifierWithMeta1,
                                        ApiSampleData.CommentResourceIdentifierWithMeta2
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .ResourceIdentifierCollection(SampleComments.Comment1, SampleComments.Comment2)
                                .SetMeta(ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta)
                            .ResourceIdentifierCollectionEnd()
                    },
            };
        #endregion

        #region DocumentBuilderWriteResourceIdentifierDocumentTestData
        public static readonly IEnumerable<object[]> DocumentBuilderWriteResourceIdentifierDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithEmptyObject",
                        ResourceIdentifierDocument.Empty,
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifier<Article>()
                    },
                new object[]
                    {
                        "WithNullResourceIdentifier",
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifier<Person>()
                    },
                new object[]
                    {
                        "WithResourceIdentifier",
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .ResourceIdentifier<Person>()
                                .SetId(ApiSampleData.PersonId)
                            .ResourceIdentifierEnd()
                    },
                new object[]
                    {
                        "WithResourceIdentifierAndMeta",
                        new ResourceIdentifierDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = ApiSampleData.PersonResourceIdentifierWithMeta
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .ResourceIdentifier<Person>()
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(ApiSampleData.PersonId)
                            .ResourceIdentifierEnd()
                    },
                new object[]
                    {
                        "WithNullResource",
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifier(default(Person))
                    },
                new object[]
                    {
                        "WithEmptyResource",
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifier(new Person())
                    },
                new object[]
                    {
                        "WithResource",
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifier(SamplePersons.Person)
                    },
                new object[]
                    {
                        "WithResourceAndMeta",
                        new ResourceIdentifierDocument
                            {
                                Meta = ApiSampleData.DocumentMeta,
                                Data = ApiSampleData.PersonResourceIdentifierWithMeta
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetMeta(ApiSampleData.DocumentMeta)
                            .ResourceIdentifier(SamplePersons.Person)
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierEnd()
                    },
            };
        #endregion

        #endregion
    }
}
