// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
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
        [MemberData(nameof(CreateDocumentTestData))]
        public void TestCreateDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(CreateResourceDocumentTestData))]
        public void TestCreateResourceDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(CreateResourceIdentifierDocumentTestData))]
        public void TestCreateResourceIdentifierDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
        { this.TestDocumentBuilderWrite(name, expectedDocument, actualDocumentWriter); }

        [Theory]
        [MemberData(nameof(CreateResourceIdentifierCollectionDocumentTestData))]
        public void TestCreateResourceIdentifierCollectionDocument(string name, Document expectedDocument, IDocumentWriter actualDocumentWriter)
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
            public static IDocumentBuilder Create(IServiceModel serviceModel)
            {
                var documentWriter = new DocumentWriter(serviceModel);
                var documentBuilder = new DocumentBuilder(documentWriter);
                return documentBuilder;
            }
        }

        #region CreateDocumentTestData
        public static readonly IEnumerable<object[]> CreateDocumentTestData = new[]
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

        #region CreateResourceDocumentTestData
        public static readonly IEnumerable<object[]> CreateResourceDocumentTestData = new[]
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
                            .Resource(typeof(Article))
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
                            .Resource(typeof(Article))
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
                            .Resource(typeof(Article))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndComplexAttributes",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.StoreConfigurationType,
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
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes)
                            .Resource(typeof(StoreConfiguration))
                                .Attributes()
                                    .AddAttribute(nameof(StoreConfiguration.IsLive), SampleStoreConfigurations.StoreConfiguration.IsLive)
                                    .AddAttribute(nameof(StoreConfiguration.MailingAddress), SampleStoreConfigurations.StoreConfiguration.MailingAddress)
                                    .AddAttribute(nameof(StoreConfiguration.PhoneNumbers), SampleStoreConfigurations.StoreConfiguration.PhoneNumbers)
                                .AttributesEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromAttributeBuilderAndNestedComplexAttributes",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.DrawingType,
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
                                        )
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithDrawingResourceTypes)
                            .Resource(typeof(Drawing))
                                .Attributes()
                                    .AddAttribute(nameof(Drawing.Name), SampleDrawings.Drawing.Name)
                                    .AddAttribute(nameof(Drawing.Lines), SampleDrawings.Drawing.Lines)
                                    .AddAttribute(nameof(Drawing.Polygons), SampleDrawings.Drawing.Polygons)
                                    .AddAttribute(nameof(Drawing.CustomData), SampleDrawings.Drawing.CustomData)
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
                            .Resource(typeof(Article))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.CreateNull())
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToOneResourceLinkage.CreateNull())
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
                            .Resource(typeof(Article))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.Create(ApiSampleData.PersonId))
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToOneResourceLinkage.Create(ApiSampleData.PersonId))
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
                            .Resource(typeof(Article))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.CreateEmpty())
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToManyResourceLinkage.CreateEmpty())
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
                            .Resource(typeof(Article))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
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
                            .Resource(typeof(Article), new Article { Title = "JSON API paints my bikeshed!" })
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
                            .Resource(typeof(Article), new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromObjectAndComplexAttributes",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.StoreConfigurationType,
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
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes)
                            .Resource(typeof(StoreConfiguration), new StoreConfiguration
                                {
                                    IsLive = SampleStoreConfigurations.StoreConfiguration.IsLive,
                                    MailingAddress = SampleStoreConfigurations.StoreConfiguration.MailingAddress,
                                    PhoneNumbers = SampleStoreConfigurations.StoreConfiguration.PhoneNumbers
                                })
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPostWithResourceFromObjectAndNestedComplexAttributes",
                        new ResourceDocument
                            {
                                Data = new Resource
                                    {
                                        Type = ClrSampleData.DrawingType,
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
                                        )
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithDrawingResourceTypes)
                            .Resource(typeof(Drawing), new Drawing
                                {
                                    Name = SampleDrawings.Drawing.Name,
                                    Lines = SampleDrawings.Drawing.Lines,
                                    Polygons = SampleDrawings.Drawing.Polygons,
                                    CustomData = SampleDrawings.Drawing.CustomData
                                })
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
                            .Resource((object)new Article { Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.CreateNull())
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
                            .Resource((object)new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToOneResourceLinkage.CreateNull())
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
                            .Resource((object)new Article { Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.Create(ApiSampleData.PersonId))
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
                            .Resource((object)new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToOneResourceLinkage.Create(ApiSampleData.PersonId))
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
                            .Resource((object)new Article { Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.CreateEmpty())
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
                            .Resource((object)new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToManyResourceLinkage.CreateEmpty())
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
                            .Resource((object)new Article { Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
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
                            .Resource((object)new Article { Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
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
                            .Resource(typeof(Article))
                                .SetId(Id.Create(ApiSampleData.ArticleId))
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(Id.Create(ApiSampleData.ArticleId))
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
                            .Resource(typeof(Article))
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
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
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes)
                            .Resource(typeof(StoreConfiguration))
                                .SetId(Id.Create(SampleStoreConfigurations.StoreConfiguration.StoreConfigurationId))
                                .Attributes()
                                    .AddAttribute(nameof(StoreConfiguration.IsLive), SampleStoreConfigurations.StoreConfiguration.IsLive)
                                    .AddAttribute(nameof(StoreConfiguration.MailingAddress), SampleStoreConfigurations.StoreConfiguration.MailingAddress)
                                    .AddAttribute(nameof(StoreConfiguration.PhoneNumbers), SampleStoreConfigurations.StoreConfiguration.PhoneNumbers)
                                .AttributesEnd()
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPatchWithResourceFromAttributeBuilderAndNestedComplexAttributes",
                        new ResourceDocument
                            {
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
                                        )
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithDrawingResourceTypes)
                            .Resource(typeof(Drawing))
                                .SetId(Id.Create(SampleDrawings.Drawing.Id))
                                .Attributes()
                                    .AddAttribute(nameof(Drawing.Name), SampleDrawings.Drawing.Name)
                                    .AddAttribute(nameof(Drawing.Lines), SampleDrawings.Drawing.Lines)
                                    .AddAttribute(nameof(Drawing.Polygons), SampleDrawings.Drawing.Polygons)
                                    .AddAttribute(nameof(Drawing.CustomData), SampleDrawings.Drawing.CustomData)
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
                            .Resource(typeof(Article))
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.CreateNull())
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToOneResourceLinkage.CreateNull())
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
                            .Resource(typeof(Article))
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.Create(ApiSampleData.PersonId))
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToOneResourceLinkage.Create(ApiSampleData.PersonId))
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
                            .Resource(typeof(Article))
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.CreateEmpty())
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToManyResourceLinkage.CreateEmpty())
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
                            .Resource(typeof(Article))
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
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
                            .Resource(typeof(Article))
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .SetId(Id.Create(ApiSampleData.ArticleId))
                                .Attributes()
                                    .AddAttribute(nameof(Article.Title), "JSON API paints my bikeshed!")
                                .AttributesEnd()
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
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
                            .Resource(typeof(Article), new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
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
                            .Resource(typeof(Article), new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
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
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithOrderResourceTypes)
                            .Resource(typeof(StoreConfiguration), SampleStoreConfigurations.StoreConfiguration)
                            .ResourceEnd()
                    },

                new object[]
                    {
                        "ForPatchWithResourceFromObjectAndNestedComplexAttributes",
                        new ResourceDocument
                            {
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
                                        )
                                    }
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithDrawingResourceTypes)
                            .Resource(typeof(Drawing), SampleDrawings.Drawing)
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
                            .Resource((object)new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.CreateNull())
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
                            .Resource((object)new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToOneResourceLinkage.CreateNull())
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
                            .Resource((object)new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToAuthorRel, ToOneResourceLinkage.Create(ApiSampleData.PersonId))
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
                            .Resource((object)new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToAuthorRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToOneResourceLinkage.Create(ApiSampleData.PersonId))
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
                            .Resource((object)new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.CreateEmpty())
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
                            .Resource((object)new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToManyResourceLinkage.CreateEmpty())
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
                            .Resource((object)new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .Relationships()
                                    .AddRelationship(ApiSampleData.ArticleToCommentsRel, ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
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
                            .Resource((object)new Article { Id = ApiSampleData.ArticleId, Title = "JSON API paints my bikeshed!" })
                                .SetMeta(ApiSampleData.ResourceMeta)
                                .Relationships()
                                    .Relationship(ApiSampleData.ArticleToCommentsRel)
                                        .SetMeta(ApiSampleData.RelationshipMeta)
                                        .SetData(ToManyResourceLinkage.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2, ApiSampleData.CommentId3, ApiSampleData.CommentId4 }))
                                    .RelationshipEnd()
                                .RelationshipsEnd()
                            .ResourceEnd()
                    },
                #endregion
            };
        #endregion

        #region CreateResourceIdentifierDocumentTestData
        public static readonly IEnumerable<object[]> CreateResourceIdentifierDocumentTestData = new[]
            {
                new object[]
                    {
                        "WithNullResourceIdentifier",
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifier(typeof(Person))
                    },
                new object[]
                    {
                        "WithResourceIdentifier",
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .ResourceIdentifier(typeof(Person))
                                .SetId(Id.Create(ApiSampleData.PersonId))
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
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifier(typeof(Person), null)
                    },
                new object[]
                    {
                        "WithEmptyResource",
                        new ResourceIdentifierDocument
                            {
                                Data = null
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifier((object)new Person())
                    },
                new object[]
                    {
                        "WithResource",
                        new ResourceIdentifierDocument
                            {
                                Data = ApiSampleData.PersonResourceIdentifier
                            },
                        DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                            .SetResourceIdentifier((object)SamplePersons.Person)
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
                            .ResourceIdentifier((object)SamplePersons.Person)
                                .SetMeta(ApiSampleData.ResourceMeta)
                            .ResourceIdentifierEnd()
                    },
            };
        #endregion

        #region CreateResourceIdentifierCollectionDocumentTestData
        public static readonly IEnumerable<object[]> CreateResourceIdentifierCollectionDocumentTestData = new[]
        {
            new object[]
            {
                "WithNullResourceIdentifiers",
                new ResourceIdentifierCollectionDocument
                {
                    Data = new List<ResourceIdentifier>()
                },
                DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                                      .SetResourceIdentifierCollection(typeof(Article))
            },
            new object[]
            {
                "WithEmptyResourceIdentifiers",
                new ResourceIdentifierCollectionDocument
                {
                    Data = new List<ResourceIdentifier>()
                },
                DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                                      .SetResourceIdentifierCollection(typeof(Comment), Enumerable.Empty<object>())
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
                        .ResourceIdentifierCollection(typeof(Comment))
                            .SetId(IdCollection.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2 }))
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
                        .ResourceIdentifierCollection(typeof(Comment))
                            .SetMeta(ApiSampleData.ResourceMeta)
                            .SetId(IdCollection.Create(new []{ ApiSampleData.CommentId1, ApiSampleData.CommentId2 }))
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
                        .SetResourceIdentifierCollection(typeof(Comment), new []{ default(object), default(object) })
                },
            new object[]
                {
                    "WithEmptyResources",
                    new ResourceIdentifierCollectionDocument
                        {
                            Data = new List<ResourceIdentifier>()
                        },
                    DocumentBuilderFactory.Create(ClrSampleData.ServiceModelWithBlogResourceTypes)
                        .SetResourceIdentifierCollection(typeof(Comment), new []{ (object)new Comment(), (object)new Comment() })
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
                        .SetResourceIdentifierCollection(typeof(Comment), new []{ (object)SampleComments.Comment1, (object)SampleComments.Comment2 })
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
                        .ResourceIdentifierCollection(typeof(Comment), new []{ (object)SampleComments.Comment1, (object)SampleComments.Comment2 })
                            .SetMeta(new []{ ApiSampleData.ResourceMeta, ApiSampleData.ResourceMeta })
                        .ResourceIdentifierCollectionEnd()
                },
        };
        #endregion

        #endregion
    }
}
