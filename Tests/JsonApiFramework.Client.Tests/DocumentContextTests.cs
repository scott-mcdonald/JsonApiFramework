// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;


using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Client.Tests
{
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
        [MemberData("DocumentContextResourceDocumentBuildingForCreatingResourcesTestData")]
        public void TestDocumentContextResourceDocumentBuildingForCreatingResources(string name, IDocumentContextOptions documentContextOptions, Document expectedApiDocument, Func<DocumentContext, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Theory]
        [MemberData("DocumentContextResourceDocumentBuildingForUpdatingResourcesTestData")]
        public void TestDocumentContextResourceDocumentBuildingForUpdatingResources(string name, IDocumentContextOptions documentContextOptions, Document expectedApiDocument, Func<DocumentContext, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Theory]
        [MemberData("DocumentContextResourceIdentifierDocumentBuildingForUpdatingRelationshipsTestData")]
        public void TestDocumentContextResourceIdentifierDocumentBuildingForUpdatingRelationships(string name, IDocumentContextOptions documentContextOptions, Document expectedApiDocument, Func<DocumentContext, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Theory]
        [MemberData("DocumentContextResourceIdentifierCollectionDocumentBuildingForUpdatingRelationshipsTestData")]
        public void TestDocumentContextResourceIdentifierCollectionDocumentBuildingForUpdatingRelationships(string name, IDocumentContextOptions documentContextOptions, Document expectedApiDocument, Func<DocumentContext, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        { this.TestDocumentContextBuilding(name, documentContextOptions, expectedApiDocument, actualApiDocumentNewDocumentFunctor); }

        [Fact]
        public void TestDocumentContextImplementsIDisposable()
        {
            // Arrange
            var documentContextOptions = CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes);

            // Act
            var documentContext = new DocumentContext(documentContextOptions);

            // Assert
            Assert.IsAssignableFrom<IDisposable>(documentContext);
        }

        [Fact]
        public void TestDocumentContextThrowObjectDisposedExceptionAfterBeingDisposed()
        {
            // Arrange
            var documentContextOptions = CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes);
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

        #region DocumentContextResourceDocumentBuildingForCreatingResourcesTestData
        public static readonly IEnumerable<object[]> DocumentContextResourceDocumentBuildingForCreatingResourcesTestData = new[]
            {
                // ResourceDocument
                new object[]
                    {
                        "ArticleResourceDocumentWithNoAttributes",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithMetaOnly",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttribute",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                    .AttributesEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttributeAndToOneRelationship",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
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
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                    .AttributesEnd()
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.Create(ApiSampleData.PersonId))
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttributeAndToOneRelationshipNull",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship()
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                    .AttributesEnd()
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.CreateNull())
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttributeAndToManyRelationship",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
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
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                    .AttributesEnd()
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.Create(new List<string>{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttributeAndToManyRelationshipEmpty",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
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
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my bikeshed!")
                                    .AttributesEnd()
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.CreateEmpty())
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObject",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my bikeshed!")),
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Title = "JSON API paints my bikeshed!" })
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObjectAndToOneRelationship",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId2)
                                                        }
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Title = "JSON API paints my house!" })
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.Create(ApiSampleData.PersonId2))
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObjectAndToOneRelationshipNull",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship()
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Title = "JSON API paints my house!" })
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.CreateNull())
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObjectAndToManyRelationship",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Title = "JSON API paints my house!" })
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId2, ApiSampleData.CommentId4 }))
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObjectAndToManyRelationshipEmpty",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship()
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Title = "JSON API paints my house!" })
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.CreateEmpty())
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },
            };
        #endregion

        #region DocumentContextResourceDocumentBuildingForUpdatingResourcesTestData
        public static readonly IEnumerable<object[]> DocumentContextResourceDocumentBuildingForUpdatingResourcesTestData = new[]
            {
                // ResourceDocument
                new object[]
                    {
                        "ArticleResourceDocumentWithNoAttributes",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .SetId(Id.Create(ApiSampleData.ArticleId))
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithMetaOnly",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Meta = ApiSampleData.ResourceMeta,
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .SetMeta(ApiSampleData.ResourceMeta)
                                    .SetId(Id.Create(ApiSampleData.ArticleId))
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttribute",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .SetId(Id.Create(ApiSampleData.ArticleId))
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my house!")
                                    .AttributesEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttributeAndToOneRelationship",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId2)
                                                        }
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .SetId(Id.Create(ApiSampleData.ArticleId))
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my house!")
                                    .AttributesEnd()
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.Create(ApiSampleData.PersonId2))
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttributeAndToOneRelationshipNull",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship()
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .SetId(Id.Create(ApiSampleData.ArticleId))
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my house!")
                                    .AttributesEnd()
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.CreateNull())
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttributeAndToManyRelationship",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .SetId(Id.Create(ApiSampleData.ArticleId))
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my house!")
                                    .AttributesEnd()
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId2, ApiSampleData.CommentId4 }))
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentWithTitleAttributeAndToManyRelationshipEmpty",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship()
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource<Article>()
                                    .SetId(Id.Create(ApiSampleData.ArticleId))
                                    .Attributes()
                                        .AddAttribute(x => x.Title, "JSON API paints my house!")
                                    .AttributesEnd()
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.CreateEmpty())
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObject",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my house!" })
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObjectAndToOneRelationship",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship
                                                        {
                                                            Data = new ResourceIdentifier(ApiSampleData.PersonType, ApiSampleData.PersonId2)
                                                        }
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my house!" })
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.Create(ApiSampleData.PersonId2))
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObjectAndToOneRelationshipNull",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToAuthorRel, new ToOneRelationship()
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my house!" })
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.CreateNull())
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObjectAndToManyRelationship",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship
                                                        {
                                                            Data = new List<ResourceIdentifier>
                                                                {
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId2),
                                                                    new ResourceIdentifier(ApiSampleData.CommentType, ApiSampleData.CommentId4)
                                                                }
                                                        }
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my house!" })
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId2, ApiSampleData.CommentId4 }))
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },

                new object[]
                    {
                        "ArticleResourceDocumentFromArticleObjectAndToManyRelationshipEmpty",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ApiSampleData.ArticleType,
                                        Id = ApiSampleData.ArticleId,
                                        Attributes = new ApiObject(ApiProperty.Create("title", "JSON API paints my house!")),
                                        Relationships = new Relationships
                                            {
                                                {
                                                    ApiSampleData.ArticleToCommentsRel, new ToManyRelationship()
                                                },
                                            },
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .Resource(new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my house!" })
                                    .Relationships()
                                        .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.CreateEmpty())
                                    .RelationshipsEnd()
                                .ResourceEnd())
                    },
            };
        #endregion

        #region DocumentContextResourceIdentifierDocumentBuildingForUpdatingRelationshipsTestData
        public static readonly IEnumerable<object[]> DocumentContextResourceIdentifierDocumentBuildingForUpdatingRelationshipsTestData = new[]
            {
                new object[]
                    {
                        "ArticleToRelationshipsToAuthorResourceIdentifierDocumentWithNullResourceIdentifier",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifier<Person>()
                                .ResourceIdentifierEnd())
                    },

                new object[]
                    {
                        "ArticleToRelationshipsToAuthorResourceIdentifierDocumentWithResourceIdentifier",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifier<Person>()
                                    .SetId(Id.Create(ApiSampleData.PersonId))
                                .ResourceIdentifierEnd())
                    },

                new object[]
                    {
                        "ArticleToRelationshipsToAuthorResourceIdentifierDocumentFromNullPersonResource",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifier<Person>()
                                .ResourceIdentifierEnd())
                    },

                new object[]
                    {
                        "ArticleToRelationshipsToAuthorResourceIdentifierDocumentFromEmptyPersonResource",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifier(new Person())
                                .ResourceIdentifierEnd())
                    },

                new object[]
                    {
                        "ArticleToRelationshipsToAuthorResourceIdentifierDocumentFromPersonResource",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifier(SamplePersons.Person)
                                .ResourceIdentifierEnd())
                    },
            };
        #endregion

        #region DocumentContextResourceIdentifierCollectionDocumentBuildingForUpdatingRelationshipsTestData
        public static readonly IEnumerable<object[]> DocumentContextResourceIdentifierCollectionDocumentBuildingForUpdatingRelationshipsTestData = new[]
            {
                new object[]
                    {
                        "ArticleToRelationshipsToCommentsResourceIdentifierCollectionDocumentWithEmptyResourceIdentifier",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>()
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifierCollection<Comment>()
                                .ResourceIdentifierCollectionEnd())
                    },

                new object[]
                    {
                        "ArticleToRelationshipsToCommentsResourceIdentifierCollectionDocumentWithResourceIdentifiers",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>
                                    {
                                        ApiSampleData.CommentResourceIdentifier1,
                                        ApiSampleData.CommentResourceIdentifier2,
                                        ApiSampleData.CommentResourceIdentifier3,
                                        ApiSampleData.CommentResourceIdentifier4,
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifierCollection<Comment>()
                                    .SetId(IdCollection.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
                                .ResourceIdentifierCollectionEnd())
                    },

                new object[]
                    {
                        "ArticleToRelationshipsToCommentsResourceIdentifierCollectionDocumentFromNullCommentResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>()
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifierCollection<Comment>()
                                .ResourceIdentifierCollectionEnd())
                    },

                new object[]
                    {
                        "ArticleToRelationshipsToCommentsResourceIdentifierCollectionDocumentFromEmptyCommentResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>()
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifierCollection(new List<Comment>())
                                .ResourceIdentifierCollectionEnd())
                    },

                new object[]
                    {
                        "ArticleToRelationshipsToCommentsResourceIdentifierCollectionDocumentFromCommentResources",
                        CreateDocumentContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes),
                        new ResourceIdentifierCollectionDocument
                            {
                                Data = new List<ResourceIdentifier>
                                    {
                                        ApiSampleData.CommentResourceIdentifier1,
                                        ApiSampleData.CommentResourceIdentifier2,
                                        ApiSampleData.CommentResourceIdentifier3,
                                        ApiSampleData.CommentResourceIdentifier4,
                                    }
                            },
                        new Func<DocumentContext, IDocumentWriter>(documentContext => documentContext
                            .NewDocument()
                                .ResourceIdentifierCollection(new []{ SampleComments.Comment1, SampleComments.Comment2, SampleComments.Comment3, SampleComments.Comment4 })
                                .ResourceIdentifierCollectionEnd())
                    },

            };
        #endregion

        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IDocumentContextOptions CreateDocumentContextOptions(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            var documentContextOptions = new DocumentContextOptions<DocumentContext>();
            var documentContextOptionsBuilder = new DocumentContextOptionsBuilder(documentContextOptions);

            documentContextOptionsBuilder.UseServiceModel(serviceModel);
            return documentContextOptions;
        }

        private void TestDocumentContextBuilding(string name, IDocumentContextOptions documentContextOptions, Document expectedApiDocument, Func<DocumentContext, IDocumentWriter> actualApiDocumentNewDocumentFunctor)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.OutputEmptyLine();

            // Arrange
            var documentContext = new DocumentContext(documentContextOptions);

            // Act
            var actualApiDocumentWriter = actualApiDocumentNewDocumentFunctor(documentContext);

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
