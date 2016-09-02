// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Configuration;
using JsonApiFramework.ServiceModel.Conventions;
using JsonApiFramework.TestAsserts.ClrResources;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestAsserts.ServiceModel;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests
{
    public class DocumentContextBaseTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentContextBaseTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DocumentContextBaseServiceModelConstructionTestData")]
        public void TestDocumentContextBaseServiceModelConstruction(string name, Func<DocumentContextBase> documentContextBaseFactory, IServiceModel expectedServiceModel, bool expectedServiceModelReferenceEquals)
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
            var documentContextBase = documentContextBaseFactory();
            var actualServiceModel = documentContextBase.ServiceModel;

            this.Output.WriteLine(String.Empty);

            var actualJson = actualServiceModel.ToJson(serializerSettings);
            this.Output.WriteLine("Actual ServiceModel");
            this.Output.WriteLine(actualJson);

            // Assert
            Assert.NotNull(documentContextBase);
            Assert.Equal(expectedServiceModelReferenceEquals, Object.ReferenceEquals(expectedServiceModel, actualServiceModel));
            ServiceModelAssert.Equal(expectedServiceModel, actualServiceModel);
        }

        [Theory]
        [MemberData("DocumentContextBaseReadingAndWritingTestData")]
        public void TestDocumentContextBaseReadingAndWriting(string name, IDocumentContextBaseTest test)
        {
            this.OutputTestName(name);
            this.OutputEmptyLine();

            test.Arrange();
            test.Act();
            test.Output(this);
            test.Assert();
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data

        #region DocumentContextBaseServiceModelConstructionTestData
        public static readonly IEnumerable<object[]> DocumentContextBaseServiceModelConstructionTestData = new[]
            {
                new object[]
                    {
                        "WithExternalBuiltBlogServiceModel",
                        (Func<DocumentContextBase>)(() => new DocumentContextBase(CreateExternalBuiltContextOptions(ClrSampleData.ServiceModelWithBlogResourceTypes))),
                        ClrSampleData.ServiceModelWithBlogResourceTypes,
                        true
                    },

                new object[]
                    {
                        "WithExternalBuiltOrderServiceModel",
                        (Func<DocumentContextBase>)(() => new DocumentContextBase(CreateExternalBuiltContextOptions(ClrSampleData.ServiceModelWithOrderResourceTypes))),
                        ClrSampleData.ServiceModelWithOrderResourceTypes,
                        true
                    },

                new object[]
                    {
                        "WithInternalBuiltBlogServiceModelWithConventionSet",
                        (Func<DocumentContextBase>)(() => new BlogServiceModelWithConventionSetDocumentContext()),
                        ClrSampleData.ServiceModelWithBlogResourceTypes,
                        false
                    },

                new object[]
                    {
                        "WithInternalBuiltBlogServiceModelWithNullConventionSet",
                        (Func<DocumentContextBase>)(() => new BlogServiceModelWithNullConventionSetDocumentContext()),
                        ClrSampleData.ServiceModelWithBlogResourceTypes,
                        false
                    },

                new object[]
                    {
                        "WithExternalBuiltInternalConfiguredBlogServiceModel",
                        (Func<DocumentContextBase>)(() => new BlogServiceModelWithPrebuiltServiceModelDocumentContext()),
                        ClrSampleData.ServiceModelWithBlogResourceTypes,
                        true
                    },

                new object[]
                    {
                        "WithInternalBuiltOrderServiceModelWithConventionSet",
                        (Func<DocumentContextBase>)(() => new OrderServiceModelWithConventionSetDocumentContext()),
                        ClrSampleData.ServiceModelWithOrderResourceTypes,
                        false
                    },

                new object[]
                    {
                        "WithInternalBuiltOrderServiceModelWithNullConventionSet",
                        (Func<DocumentContextBase>)(() => new OrderServiceModelWithNullConventionSetDocumentContext()),
                        ClrSampleData.ServiceModelWithOrderResourceTypes,
                        false
                    },

                new object[]
                    {
                        "WithExternalBuiltInternalConfiguredOrderServiceModel",
                        (Func<DocumentContextBase>)(() => new OrderServiceModelWithPrebuiltServiceModelDocumentContext()),
                        ClrSampleData.ServiceModelWithOrderResourceTypes,
                        true
                    },
            };
        #endregion

        #region DocumentContextBaseReadingAndWritingTestData
        public static readonly IEnumerable<object[]> DocumentContextBaseReadingAndWritingTestData = new[]
            {
                // Document
                new object[]
                    {
                        "WithExistingArticleDocument", new DocumentContextBaseWithExistingDocumentTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            Document.Empty)
                    },

                new object[]
                    {
                        "WithExistingArticleDocumentWithJsonApiAndMetaAndLinks", new DocumentContextBaseWithExistingDocumentTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new Document
                                {
                                    JsonApiVersion = ApiSampleData.JsonApiVersionAndMeta,
                                    Meta = ApiSampleData.DocumentMeta,
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                })
                    },

                // NullDocument
                new object[]
                    {
                        "WithExistingArticleNullDocument", new DocumentContextBaseWithExistingResourceDocumentTest<Article>(
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

                // ResourceDocument
                new object[]
                    {
                        "WithExistingArticleResourceDocumentWithNullResource", new DocumentContextBaseWithExistingResourceDocumentTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleLink}
                                        },
                                    Data = null
                                },
                            null)
                    },

                new object[]
                    {
                        "WithExistingArticleResourceDocument", new DocumentContextBaseWithExistingResourceDocumentTest<Article>(
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
                        "WithExistingArticleResourceDocumentWithIncludedToOnePersonResource", new DocumentContextBaseWithExistingResourceDocumentWithIncludesTest<Article, Person>(
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
                            SampleArticles.ArticleWithResourceLinkage,
                            ApiSampleData.ArticleToAuthorRel,
                            SamplePersons.Person)
                    },

                new object[]
                    {
                        "WithExistingArticleResourceDocumentWithIncludedToManyCommentResourceCollection", new DocumentContextBaseWithExistingResourceDocumentWithIncludesTest<Article, Comment>(
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
                            SampleArticles.ArticleWithResourceLinkage,
                            ApiSampleData.ArticleToCommentsRel,
                            new [] { SampleComments.Comment1, SampleComments.Comment2 })
                    },

                // ResourceIdentifierDocument
                new object[]
                    {
                        "WithExistingArticleToRelationshipsToAuthorResourceIdentifierDocumentWithNullResourceIdentifier", new DocumentContextBaseWithExistingResourceIdentifierDocumentTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink}
                                        },
                                    Data = null
                                },
                            null)
                    },

                new object[]
                    {
                        "WithExistingArticleToRelationshipsToAuthorResourceIdentifierDocument", new DocumentContextBaseWithExistingResourceIdentifierDocumentTest<Person, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleToRelationshipsToAuthorLink}
                                        },
                                    Data = ApiSampleData.PersonResourceIdentifier
                                },
                            ApiSampleData.PersonId)
                    },
                    
                // EmptyDocument
                new object[]
                    {
                        "WithExistingArticleEmptyDocument", new DocumentContextBaseWithExistingResourceCollectionDocumentTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new EmptyDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        }
                                },
                            Enumerable.Empty<Article>())
                    },

                // ResourceCollectionDocument
                new object[]
                    {
                        "WithExistingArticleResourceCollectionDocumentWithEmptyResources", new DocumentContextBaseWithExistingResourceCollectionDocumentTest<Article>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        },
                                    Data = new List<Resource>()
                                },
                            Enumerable.Empty<Article>())
                    },

                new object[]
                    {
                        "WithExistingArticleResourceCollectionDocument", new DocumentContextBaseWithExistingResourceCollectionDocumentTest<Article>(
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
                            new[]{ SampleArticles.Article1, SampleArticles.Article2 })
                    },

                new object[]
                    {
                        "WithExistingArticleResourceCollectionDocumentWithIncludedToOnePersonResourceCollection", new DocumentContextBaseWithExistingResourceCollectionDocumentWithIncludesTest<Article, Person>(
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
                            new[]{ SampleArticles.ArticleWithResourceLinkage1, SampleArticles.ArticleWithResourceLinkage2 },
                            ApiSampleData.ArticleToAuthorRel,
                            new [] { SamplePersons.Person1, SamplePersons.Person2 })
                    },

                new object[]
                    {
                        "WithExistingArticleResourceCollectionDocumentWithIncludedToManyCommentResourceCollectionCollection", new DocumentContextBaseWithExistingResourceCollectionDocumentWithIncludesTest<Article, Comment>(
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
                            new[]{ SampleArticles.ArticleWithResourceLinkage1, SampleArticles.ArticleWithResourceLinkage2 },
                            ApiSampleData.ArticleToCommentsRel,
                            new []{
                                new [] { SampleComments.Comment1, SampleComments.Comment2 },
                                new [] { SampleComments.Comment3, SampleComments.Comment4 }
                            })
                    },

                // ResourceIdentifierCollectionDocument
                new object[]
                    {
                        "WithExistingArticleToRelationshipsToCommentsResourceIdentifierDocumentWithEmptyResourceIdentifiers", new DocumentContextBaseWithExistingResourceIdentifierCollectionDocumentTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleToRelationshipsToCommentsLink}
                                        },
                                    Data = Enumerable.Empty<ResourceIdentifier>()
                                                     .ToList()
                                },
                            Enumerable.Empty<string>())
                    },

                new object[]
                    {
                        "WithExistingArticleToRelationshipsToCommentsResourceIdentifierDocument", new DocumentContextBaseWithExistingResourceIdentifierCollectionDocumentTest<Comment, string>(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ResourceIdentifierCollectionDocument
                                {
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
                            new [] { ApiSampleData.CommentId1, ApiSampleData.CommentId2 })
                    },

                // ErrorsDocument
                new object[]
                    {
                        "WithExistingErrorsDocumentWithEmptyErrors", new DocumentContextBaseWithExistingErrorCollectionDocumentTest(
                            ClrSampleData.ServiceModelWithBlogResourceTypes,
                            new ErrorsDocument
                                {
                                    Links = new Links
                                        {
                                            {Keywords.Self, ApiSampleData.ArticleCollectionLink}
                                        },
                                    Errors = new List<Error>()
                                },
                            Enumerable.Empty<Error>())
                    },

                new object[]
                    {
                        "WithExistingErrorsDocument", new DocumentContextBaseWithExistingErrorCollectionDocumentTest(
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
                            new List<Error>()
                                {
                                    ApiSampleData.Error1,
                                    ApiSampleData.Error2
                                })
                    },
            };
        #endregion

        #endregion

        #region Test Types
        public interface IDocumentContextBaseTest
        {
            #region Methods
            void Arrange();
            void Act();
            void Output(DocumentContextBaseTests parent);
            void Assert();
            #endregion
        }

        public class DocumentContextBaseWithExistingDocumentTest : IDocumentContextBaseTest
        {
            #region Constructors
            public DocumentContextBaseWithExistingDocumentTest(IServiceModel serviceModel, Document expectedApiDocument)
            {
                this.ServiceModel = serviceModel;
                this.ExpectedApiDocument = expectedApiDocument;
            }
            #endregion

            #region IDocumentContextBaseTest Implementation
            public virtual void Arrange()
            { }

            public virtual void Act()
            {
                var documentContextOptions = CreateExternalBuiltContextOptions(this.ServiceModel);
                var documentContextBase = new DocumentContextBase(documentContextOptions, this.ExpectedApiDocument);
                this.DocumentContextBase = documentContextBase;

                // Reading
                this.ActualJsonApiVersion = this.DocumentContextBase.GetJsonApiVersion();
                this.ActualDocumentMeta = this.DocumentContextBase.GetDocumentMeta();
                this.ActualDocumentLinks = this.DocumentContextBase.GetDocumentLinks();

                // Writing
                this.ActualApiDocument = this.DocumentContextBase.WriteDocument();
            }

            public virtual void Output(DocumentContextBaseTests parent)
            {
                var output = parent.Output;

                output.WriteLine("Parsed DOM Tree");
                output.WriteLine(String.Empty);

                var domTreeString = this.DocumentContextBase.ToDomTreeString();
                output.WriteLine(domTreeString);

                output.WriteLine(String.Empty);
                output.WriteLine("Read Document JSON");

                var readDocumentJson = this.ExpectedApiDocument.ToJson();
                output.WriteLine(readDocumentJson);

                output.WriteLine(String.Empty);
                output.WriteLine("Write Document JSON");

                var writeDocumentJson = this.ActualApiDocument.ToJson();
                output.WriteLine(writeDocumentJson);
            }

            public virtual void Assert()
            {
                // Reading
                JsonApiVersionAssert.Equal(this.ExpectedJsonApiVersion, this.ActualJsonApiVersion);
                MetaAssert.Equal(this.ExpectedDocumentMeta, this.ActualDocumentMeta);
                LinksAssert.Equal(this.ExpectedDocumentLinks, this.ActualDocumentLinks);

                // Writing
                DocumentAssert.Equal(this.ExpectedApiDocument, this.ActualApiDocument);
            }
            #endregion

            // ReSharper disable MemberCanBePrivate.Global
            #region User Supplied Properties
            protected IServiceModel ServiceModel { get; private set; }
            protected Document ExpectedApiDocument { get; private set; }
            #endregion

            #region Calculated Properties
            protected DocumentContextBase DocumentContextBase { get; private set; }

            protected JsonApiVersion ExpectedJsonApiVersion { get { return this.ExpectedApiDocument.JsonApiVersion; } }
            protected Meta ExpectedDocumentMeta { get { return this.ExpectedApiDocument.Meta; } }
            protected Links ExpectedDocumentLinks { get { return this.ExpectedApiDocument.Links; } }

            protected Document ActualApiDocument { get; private set; }

            protected JsonApiVersion ActualJsonApiVersion { get; private set; }
            protected Meta ActualDocumentMeta { get; private set; }
            protected Links ActualDocumentLinks { get; private set; }
            #endregion
            // ReSharper restore MemberCanBePrivate.Global
        }

        public class DocumentContextBaseWithExistingResourceDocumentTest<TResource> : DocumentContextBaseWithExistingDocumentTest
            where TResource : class, IResource
        {
            #region Constructors
            public DocumentContextBaseWithExistingResourceDocumentTest(IServiceModel serviceModel, Document expectedApiDocument, TResource expectedResource)
                : base(serviceModel, expectedApiDocument)
            {
                this.ExpectedResource = expectedResource;
            }
            #endregion

            #region IDocumentContextBaseTest Overrides
            public override void Act()
            {
                base.Act();

                // Reading
                this.ActualResource = this.DocumentContextBase.GetResource<TResource>();
            }

            public override void Assert()
            {
                base.Assert();

                // Reading
                ClrResourceAssert.Equal(this.ExpectedResource, this.ActualResource);
            }
            #endregion

            // ReSharper disable MemberCanBePrivate.Global
            #region User Supplied Properties
            protected TResource ExpectedResource { get; private set; }
            #endregion

            #region Calculated Properties
            protected TResource ActualResource { get; private set; }
            #endregion
            // ReSharper restore MemberCanBePrivate.Global
        }

        public class DocumentContextBaseWithExistingResourceDocumentWithIncludesTest<TResource, TRelatedResource> : DocumentContextBaseWithExistingResourceDocumentTest<TResource>
            where TResource : class, IResource
            where TRelatedResource : class, IResource
        {
            #region Constructors
            public DocumentContextBaseWithExistingResourceDocumentWithIncludesTest(IServiceModel serviceModel, Document expectedApiDocument, TResource expectedResource, string expectedRelatedRel, TRelatedResource expectedRelatedResource)
                : base(serviceModel, expectedApiDocument, expectedResource)
            {
                this.ExpectedRelatedRel = expectedRelatedRel;
                this.ExpectedRelatedResource = expectedRelatedResource;
            }

            public DocumentContextBaseWithExistingResourceDocumentWithIncludesTest(IServiceModel serviceModel, Document expectedApiDocument, TResource expectedResource, string expectedRelatedRel, IEnumerable<TRelatedResource> expectedRelatedResourceCollection)
                : base(serviceModel, expectedApiDocument, expectedResource)
            {
                this.ExpectedRelatedRel = expectedRelatedRel;
                this.ExpectedRelatedResourceCollection = expectedRelatedResourceCollection;
            }
            #endregion

            #region IDocumentContextBaseTest Overrides
            public override void Act()
            {
                base.Act();

                // Reading
                var expectedResource = this.ExpectedResource;
                var expectedRelatedRelationships = this.DocumentContextBase.GetResourceRelationships(expectedResource);
                var expectedRelatedRelationship = expectedRelatedRelationships.GetRelationship(this.ExpectedRelatedRel);
                var expectedRelatedRelationshipRelationshipType = expectedRelatedRelationship.GetRelationshipType();
                switch (expectedRelatedRelationshipRelationshipType)
                {
                    case RelationshipType.ToOneRelationship:
                        {
                            var actualRelatedResource = this.DocumentContextBase.GetRelatedResource<TRelatedResource>(expectedRelatedRelationship);
                            this.ActualRelatedResource = actualRelatedResource;
                        }
                        break;

                    case RelationshipType.ToManyRelationship:
                        {
                            var actualRelatedResourceCollection = this.DocumentContextBase.GetRelatedResourceCollection<TRelatedResource>(expectedRelatedRelationship);
                            this.ActualRelatedResourceCollection = actualRelatedResourceCollection;
                        }
                        break;
                }
            }

            public override void Assert()
            {
                base.Assert();

                // Reading
                ClrResourceAssert.Equal(this.ExpectedRelatedResource, this.ActualRelatedResource);
                ClrResourceAssert.Equal(this.ExpectedRelatedResourceCollection, this.ActualRelatedResourceCollection);
            }
            #endregion

            // ReSharper disable MemberCanBePrivate.Global
            #region User Supplied Properties
            private string ExpectedRelatedRel { get; set; }
            private TRelatedResource ExpectedRelatedResource { get; set; }
            private IEnumerable<TRelatedResource> ExpectedRelatedResourceCollection { get; set; }
            #endregion

            #region Calculated Properties
            private TRelatedResource ActualRelatedResource { get; set; }
            private IEnumerable<TRelatedResource> ActualRelatedResourceCollection { get; set; }
            #endregion
            // ReSharper restore MemberCanBePrivate.Global
        }

        public class DocumentContextBaseWithExistingResourceIdentifierDocumentTest<TResource, TResourceId> : DocumentContextBaseWithExistingDocumentTest
            where TResource : class, IResource
        {
            #region Constructors
            public DocumentContextBaseWithExistingResourceIdentifierDocumentTest(IServiceModel serviceModel, Document expectedApiDocument, TResourceId expectedResourceId)
                : base(serviceModel, expectedApiDocument)
            {
                this.ExpectedResourceId = expectedResourceId;
            }
            #endregion

            #region IDocumentContextBaseTest Overrides
            public override void Act()
            {
                base.Act();

                // Reading
                this.ActualResourceId = this.DocumentContextBase.GetResourceId<TResource, TResourceId>();
            }

            public override void Assert()
            {
                base.Assert();

                // Reading
                Xunit.Assert.Equal(this.ExpectedResourceId, this.ActualResourceId);
            }
            #endregion

            // ReSharper disable MemberCanBePrivate.Global
            #region User Supplied Properties
            protected TResourceId ExpectedResourceId { get; private set; }
            #endregion

            #region Calculated Properties
            protected TResourceId ActualResourceId { get; private set; }
            #endregion
            // ReSharper restore MemberCanBePrivate.Global
        }

        public class DocumentContextBaseWithExistingResourceCollectionDocumentTest<TResource> : DocumentContextBaseWithExistingDocumentTest
            where TResource : class, IResource
        {
            #region Constructors
            public DocumentContextBaseWithExistingResourceCollectionDocumentTest(IServiceModel serviceModel, Document expectedApiDocument, IEnumerable<TResource> expectedResourceCollection)
                : base(serviceModel, expectedApiDocument)
            {
                this.ExpectedResourceCollection = expectedResourceCollection;
            }
            #endregion

            #region IDocumentContextBaseTest Overrides
            public override void Act()
            {
                base.Act();

                // Reading
                this.ActualResourceCollection = this.DocumentContextBase.GetResourceCollection<TResource>();
            }

            public override void Assert()
            {
                base.Assert();

                // Reading
                ClrResourceAssert.Equal(this.ExpectedResourceCollection, this.ActualResourceCollection);
            }
            #endregion

            // ReSharper disable MemberCanBePrivate.Global
            #region User Supplied Properties
            protected IEnumerable<TResource> ExpectedResourceCollection { get; private set; }
            #endregion

            #region Calculated Properties
            protected IEnumerable<TResource> ActualResourceCollection { get; private set; }
            #endregion
            // ReSharper restore MemberCanBePrivate.Global
        }

        public class DocumentContextBaseWithExistingResourceCollectionDocumentWithIncludesTest<TResource, TRelatedResource> : DocumentContextBaseWithExistingResourceCollectionDocumentTest<TResource>
            where TResource : class, IResource
            where TRelatedResource : class, IResource
        {
            #region Constructors
            public DocumentContextBaseWithExistingResourceCollectionDocumentWithIncludesTest(IServiceModel serviceModel, Document expectedApiDocument, IEnumerable<TResource> expectedResourceCollection, string expectedRelatedRel, IEnumerable<TRelatedResource> expectedRelatedResourceCollection)
                : base(serviceModel, expectedApiDocument, expectedResourceCollection)
            {
                this.ExpectedRelatedRel = expectedRelatedRel;
                this.ExpectedRelatedResourceCollection = expectedRelatedResourceCollection;
            }

            public DocumentContextBaseWithExistingResourceCollectionDocumentWithIncludesTest(IServiceModel serviceModel, Document expectedApiDocument, IEnumerable<TResource> expectedResourceCollection, string expectedRelatedRel, IEnumerable<IEnumerable<TRelatedResource>> expectedRelatedResourceCollectionCollection)
                : base(serviceModel, expectedApiDocument, expectedResourceCollection)
            {
                this.ExpectedRelatedRel = expectedRelatedRel;
                this.ExpectedRelatedResourceCollectionCollection = expectedRelatedResourceCollectionCollection;
            }
            #endregion

            #region IDocumentContextBaseTest Overrides
            public override void Act()
            {
                base.Act();

                // Reading
                var rel = this.ExpectedRelatedRel;
                var serviceModel = this.ServiceModel;

                var fromResourceType = serviceModel.GetResourceType<TResource>();
                var fromRelationship = fromResourceType.GetRelationship(rel);
                var fromRelationshipToCardinality = fromRelationship.ToCardinality;
                switch (fromRelationshipToCardinality)
                {
                    case RelationshipCardinality.ToOne:
                        {
                            var actualRelatedToOneResourceCollection = this
                                .ExpectedResourceCollection
                                .Select(x =>
                                {
                                    var expectedResource = x;
                                    var expectedRelatedRelationships = this.DocumentContextBase.GetResourceRelationships(expectedResource);
                                    var expectedRelatedRelationship = expectedRelatedRelationships.GetRelationship(this.ExpectedRelatedRel);
                                    var actualRelatedResource = this.DocumentContextBase.GetRelatedResource<TRelatedResource>(expectedRelatedRelationship);
                                    return actualRelatedResource;
                                })
                                .ToList();
                            this.ActualRelatedResourceCollection = actualRelatedToOneResourceCollection;
                        }
                        break;

                    case RelationshipCardinality.ToMany:
                        {
                            var actualRelatedToManyResourceCollectionCollection = this
                                .ExpectedResourceCollection
                                .Select(x =>
                                {
                                    var expectedResource = x;
                                    var expectedRelatedRelationships = this.DocumentContextBase.GetResourceRelationships(expectedResource);
                                    var expectedRelatedRelationship = expectedRelatedRelationships.GetRelationship(this.ExpectedRelatedRel);
                                    var actualRelatedResourceCollection = this.DocumentContextBase.GetRelatedResourceCollection<TRelatedResource>(expectedRelatedRelationship);
                                    return actualRelatedResourceCollection;
                                })
                                .ToList();
                            this.ActualRelatedResourceCollectionCollection = actualRelatedToManyResourceCollectionCollection;
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            public override void Assert()
            {
                base.Assert();

                // Reading
                ClrResourceAssert.Equal(this.ExpectedRelatedResourceCollection, this.ActualRelatedResourceCollection);

                if (this.ExpectedRelatedResourceCollectionCollection == null)
                {
                    Xunit.Assert.Null(this.ActualRelatedResourceCollectionCollection);
                    return;
                }
                Xunit.Assert.NotNull(this.ActualRelatedResourceCollectionCollection);

                var expectedRelatedResourceCollectionList = this.ExpectedRelatedResourceCollectionCollection.SafeToList();
                var actualRelatedResourceCollectionList = this.ActualRelatedResourceCollectionCollection.SafeToList();
                Xunit.Assert.Equal(expectedRelatedResourceCollectionList.Count, actualRelatedResourceCollectionList.Count);

                var count = expectedRelatedResourceCollectionList.Count;
                for (var index = 0; index < count; ++index)
                {
                    var expectedRelatedResourceCollection = expectedRelatedResourceCollectionList[index];
                    var actualRelatedResourceCollection = actualRelatedResourceCollectionList[index];

                    ClrResourceAssert.Equal(expectedRelatedResourceCollection, actualRelatedResourceCollection);
                }
            }
            #endregion

            // ReSharper disable MemberCanBePrivate.Global
            #region User Supplied Properties
            private string ExpectedRelatedRel { get; set; }
            private IEnumerable<TRelatedResource> ExpectedRelatedResourceCollection { get; set; }
            private IEnumerable<IEnumerable<TRelatedResource>> ExpectedRelatedResourceCollectionCollection { get; set; }
            #endregion

            #region Calculated Properties
            private IEnumerable<TRelatedResource> ActualRelatedResourceCollection { get; set; }
            private IEnumerable<IEnumerable<TRelatedResource>> ActualRelatedResourceCollectionCollection { get; set; }
            #endregion
            // ReSharper restore MemberCanBePrivate.Global
        }

        public class DocumentContextBaseWithExistingResourceIdentifierCollectionDocumentTest<TResource, TResourceId> : DocumentContextBaseWithExistingDocumentTest
            where TResource : class, IResource
        {
            #region Constructors
            public DocumentContextBaseWithExistingResourceIdentifierCollectionDocumentTest(IServiceModel serviceModel, Document expectedApiDocument, IEnumerable<TResourceId> expectedResourceIdCollection)
                : base(serviceModel, expectedApiDocument)
            {
                this.ExpectedResourceIdCollection = expectedResourceIdCollection;
            }
            #endregion

            #region IDocumentContextBaseTest Overrides
            public override void Act()
            {
                base.Act();

                // Reading
                this.ActualResourceIdCollection = this.DocumentContextBase.GetResourceIdCollection<TResource, TResourceId>();
            }

            public override void Assert()
            {
                base.Assert();

                // Reading
                Xunit.Assert.Equal(this.ExpectedResourceIdCollection, this.ActualResourceIdCollection);
            }
            #endregion

            // ReSharper disable MemberCanBePrivate.Global
            #region User Supplied Properties
            protected IEnumerable<TResourceId> ExpectedResourceIdCollection { get; private set; }
            #endregion

            #region Calculated Properties
            protected IEnumerable<TResourceId> ActualResourceIdCollection { get; private set; }
            #endregion
            // ReSharper restore MemberCanBePrivate.Global
        }

        public class DocumentContextBaseWithExistingErrorCollectionDocumentTest : DocumentContextBaseWithExistingDocumentTest
        {
            #region Constructors
            public DocumentContextBaseWithExistingErrorCollectionDocumentTest(IServiceModel serviceModel, Document expectedApiDocument, IEnumerable<Error> expectedErrorCollection)
                : base(serviceModel, expectedApiDocument)
            {
                this.ExpectedErrorCollection = expectedErrorCollection;
            }
            #endregion

            #region IDocumentContextBaseTest Overrides
            public override void Act()
            {
                base.Act();

                // Reading
                this.ActualErrorCollection = this.DocumentContextBase.GetErrorCollection();
            }

            public override void Assert()
            {
                base.Assert();

                // Reading
                ErrorAssert.Equal(this.ExpectedErrorCollection, this.ActualErrorCollection);
            }
            #endregion

            // ReSharper disable MemberCanBePrivate.Global
            #region User Supplied Properties
            protected IEnumerable<Error> ExpectedErrorCollection { get; private set; }
            #endregion

            #region Calculated Properties
            protected IEnumerable<Error> ActualErrorCollection { get; private set; }
            #endregion
            // ReSharper restore MemberCanBePrivate.Global
        }

        private class BlogServiceModelWithNullConventionSetDocumentContext : DocumentContextBase
        {
            #region Constructors
            public BlogServiceModelWithNullConventionSetDocumentContext()
                : base(new DocumentContextOptions<BlogServiceModelWithNullConventionSetDocumentContext>())
            { }

            public BlogServiceModelWithNullConventionSetDocumentContext(Document apiDocument)
                : base(new DocumentContextOptions<BlogServiceModelWithNullConventionSetDocumentContext>(), apiDocument)
            { }
            #endregion

            #region DocumentContextBase Overrides
            protected internal override void OnServiceModelCreating(IServiceModelBuilder serviceModelBuilder)
            {
                Contract.Requires(serviceModelBuilder != null);

                serviceModelBuilder.Configurations.Add(new TestConfigurations.ArticleConfigurationWithNullConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.BlogConfigurationWithNullConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.CommentConfigurationWithNullConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.PersonConfigurationWithNullConventionSet());
            }
            #endregion
        }

        private class BlogServiceModelWithConventionSetDocumentContext : DocumentContextBase
        {
            #region Constructors
            public BlogServiceModelWithConventionSetDocumentContext()
                : base(new DocumentContextOptions<BlogServiceModelWithConventionSetDocumentContext>())
            { }

            public BlogServiceModelWithConventionSetDocumentContext(Document apiDocument)
                : base(new DocumentContextOptions<BlogServiceModelWithConventionSetDocumentContext>(), apiDocument)
            { }
            #endregion

            #region DocumentContextBase Overrides
            protected internal override void OnConventionSetCreating(IConventionSetBuilder conventionSetBuilder)
            {
                Contract.Requires(conventionSetBuilder != null);

                conventionSetBuilder.ApiAttributeNamingConventions()
                                    .AddStandardMemberNamingConvention();

                conventionSetBuilder.ApiTypeNamingConventions()
                                    .AddPluralNamingConvention()
                                    .AddStandardMemberNamingConvention();

                conventionSetBuilder.ResourceTypeConventions()
                                    .AddPropertyDiscoveryConvention();
            }

            protected internal override void OnServiceModelCreating(IServiceModelBuilder serviceModelBuilder)
            {
                Contract.Requires(serviceModelBuilder != null);

                serviceModelBuilder.Configurations.Add(new TestConfigurations.ArticleConfigurationWithConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.BlogConfigurationWithConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.CommentConfigurationWithConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.PersonConfigurationWithConventionSet());
            }
            #endregion
        }

        private class BlogServiceModelWithPrebuiltServiceModelDocumentContext : DocumentContextBase
        {
            #region Constructors
            public BlogServiceModelWithPrebuiltServiceModelDocumentContext()
                : base(new DocumentContextOptions<BlogServiceModelWithPrebuiltServiceModelDocumentContext>())
            { }

            public BlogServiceModelWithPrebuiltServiceModelDocumentContext(Document apiDocument)
                : base(new DocumentContextOptions<BlogServiceModelWithPrebuiltServiceModelDocumentContext>(), apiDocument)
            { }
            #endregion

            #region DocumentContextBase Overrides
            protected internal override void OnConfiguring(IDocumentContextOptionsBuilder optionsBuilder)
            {
                Contract.Requires(optionsBuilder != null);

                optionsBuilder.UseServiceModel(ClrSampleData.ServiceModelWithBlogResourceTypes);
            }
            #endregion
        }

        private class OrderServiceModelWithNullConventionSetDocumentContext : DocumentContextBase
        {
            #region Constructors
            public OrderServiceModelWithNullConventionSetDocumentContext()
                : base(new DocumentContextOptions<OrderServiceModelWithNullConventionSetDocumentContext>())
            { }

            public OrderServiceModelWithNullConventionSetDocumentContext(Document apiDocument)
                : base(new DocumentContextOptions<OrderServiceModelWithNullConventionSetDocumentContext>(), apiDocument)
            { }
            #endregion

            #region DocumentContextBase Overrides
            protected internal override void OnServiceModelCreating(IServiceModelBuilder serviceModelBuilder)
            {
                Contract.Requires(serviceModelBuilder != null);

                serviceModelBuilder.Configurations.Add(new TestConfigurations.OrderConfigurationWithNullConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.OrderItemConfigurationWithNullConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.PaymentConfigurationWithNullConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.PosSystemConfigurationWithNullConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.ProductConfigurationWithNullConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.StoreConfigurationWithNullConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.StoreConfigurationConfigurationWithNullConventionSet());
            }
            #endregion
        }

        private class OrderServiceModelWithConventionSetDocumentContext : DocumentContextBase
        {
            #region Constructors
            public OrderServiceModelWithConventionSetDocumentContext()
                : base(new DocumentContextOptions<OrderServiceModelWithConventionSetDocumentContext>())
            { }

            public OrderServiceModelWithConventionSetDocumentContext(Document apiDocument)
                : base(new DocumentContextOptions<OrderServiceModelWithConventionSetDocumentContext>(), apiDocument)
            { }
            #endregion

            #region DocumentContextBase Overrides
            protected internal override void OnConventionSetCreating(IConventionSetBuilder conventionSetBuilder)
            {
                Contract.Requires(conventionSetBuilder != null);

                conventionSetBuilder.ApiAttributeNamingConventions()
                                    .AddStandardMemberNamingConvention();

                conventionSetBuilder.ApiTypeNamingConventions()
                                    .AddPluralNamingConvention()
                                    .AddStandardMemberNamingConvention();

                conventionSetBuilder.ResourceTypeConventions()
                                    .AddPropertyDiscoveryConvention();
            }

            protected internal override void OnServiceModelCreating(IServiceModelBuilder serviceModelBuilder)
            {
                Contract.Requires(serviceModelBuilder != null);

                serviceModelBuilder.Configurations.Add(new TestConfigurations.OrderConfigurationWithConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.OrderItemConfigurationWithConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.PaymentConfigurationWithConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.PosSystemConfigurationWithConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.ProductConfigurationWithConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.StoreConfigurationWithConventionSet());
                serviceModelBuilder.Configurations.Add(new TestConfigurations.StoreConfigurationConfigurationWithConventionSet());
            }
            #endregion
        }

        private class OrderServiceModelWithPrebuiltServiceModelDocumentContext : DocumentContextBase
        {
            #region Constructors
            public OrderServiceModelWithPrebuiltServiceModelDocumentContext()
                : base(new DocumentContextOptions<OrderServiceModelWithPrebuiltServiceModelDocumentContext>())
            { }

            public OrderServiceModelWithPrebuiltServiceModelDocumentContext(Document apiDocument)
                : base(new DocumentContextOptions<OrderServiceModelWithPrebuiltServiceModelDocumentContext>(), apiDocument)
            { }
            #endregion

            #region DocumentContextBase Overrides
            protected internal override void OnConfiguring(IDocumentContextOptionsBuilder optionsBuilder)
            {
                Contract.Requires(optionsBuilder != null);

                optionsBuilder.UseServiceModel(ClrSampleData.ServiceModelWithOrderResourceTypes);
            }
            #endregion
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IDocumentContextOptions CreateExternalBuiltContextOptions(IServiceModel serviceModel)
        {
            Contract.Requires(serviceModel != null);

            var documentContextOptions = new DocumentContextOptions<DocumentContextBase>();
            var documentContextOptionsBuilder = new DocumentContextOptionsBuilder(documentContextOptions);

            documentContextOptionsBuilder.UseServiceModel(serviceModel);
            return documentContextOptions;
        }

        private void OutputEmptyLine()
        {
            this.Output.WriteLine(String.Empty);
        }

        private void OutputTestName(string testName)
        {
            this.Output.WriteLine("Test Name: {0}", testName);
        }
        #endregion
    }
}
