// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class DocumentTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DocumentTestData")]
        public void TestDocumentToJson(string name, Document expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("DocumentTestData")]
        public void TestDocumentParse(string name, Document expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Document>(json);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("EmptyDocumentTestData")]
        public void TestEmptyDocumentToJson(string name, Document expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("EmptyDocumentTestData")]
        public void TestEmptyDocumentParse(string name, Document expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Document>(json);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ErrorsDocumentTestData")]
        public void TestErrorsDocumentToJson(string name, Document expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ErrorsDocumentTestData")]
        public void TestErrorsDocumentParse(string name, Document expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Document>(json);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("NullDocumentTestData")]
        public void TestNullDocumentToJson(string name, Document expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("NullDocumentTestData")]
        public void TestNullDocumentParse(string name, Document expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Document>(json);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceDocumentTestData")]
        public void TestResourceDocumentToJson(string name, Document expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceDocumentTestData")]
        public void TestResourceDocumentParse(string name, Document expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Document>(json);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceIdentifierDocumentTestData")]
        public void TestResourceIdentifierDocumentToJson(string name, Document expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceIdentifierDocumentTestData")]
        public void TestResourceIdentifierDocumentParse(string name, Document expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Document>(json);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceCollectionDocumentTestData")]
        public void TestResourceCollectionDocumentToJson(string name, Document expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceCollectionDocumentTestData")]
        public void TestResourceCollectionDocumentParse(string name, Document expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Document>(json);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceIdentifierCollectionDocumentTestData")]
        public void TestResourceIdentifierCollectionDocumentToJson(string name, Document expected)
        {
            // Arrange

            // Act
            var actual = expected.ToJson();
            this.Output.WriteLine(actual);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }

        [Theory]
        [MemberData("ResourceIdentifierCollectionDocumentTestData")]
        public void TestResourceIdentifierCollectionDocumentParse(string name, Document expected)
        {
            // Arrange
            var json = expected.ToJson();

            // Act
            this.Output.WriteLine(json);
            var actual = JsonObject.Parse<Document>(json);

            // Assert
            DocumentAssert.Equal(expected, actual);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable once UnusedMember.Global
        public static readonly IEnumerable<object[]> DocumentTestData = new[]
            {
                new object[] {"WithEmptyObject", Document.Empty},
                new object[]
                    {
                        "WithMeta", new Document
                            {
                                Meta = SampleData.DocumentMeta
                            }
                    },
                new object[]
                    {
                        "WithMetaAndLinks", new Document
                            {
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleLink}
                                    },
                            }
                    },
                new object[]
                    {
                        "WithJsonApiAndMetaAndLinks", new Document
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleLink}
                                    },
                            }
                    }
            };

        public static readonly IEnumerable<object[]> EmptyDocumentTestData = new[]
            {
                new object[] {"WithEmptyObject", EmptyDocument.Empty},
                new object[]
                    {
                        "WithEmptyDocument", new EmptyDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToCommentsLink}
                                    }
                            }
                    }
            };

        public static readonly IEnumerable<object[]> ErrorsDocumentTestData = new[]
            {
                new object[] {"WithEmptyObject", ErrorsDocument.Empty},
                new object[]
                    {
                        "WithEmptyErrors", new ErrorsDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleLink}
                                    },
                                Errors = new List<Error>()
                            }
                    },
                new object[]
                    {
                        "WithErrors", new ErrorsDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleLink}
                                    },
                                Errors = new List<Error>
                                    {
                                        SampleData.Error1,
                                        SampleData.Error2
                                    }
                            }
                    }
            };

        public static readonly IEnumerable<object[]> NullDocumentTestData = new[]
            {
                new object[] {"WithEmptyObject", NullDocument.Empty},
                new object[]
                    {
                        "WithNullDocument", new NullDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToAuthorLink}
                                    }
                            }
                    }
            };

        public static readonly IEnumerable<object[]> ResourceDocumentTestData = new[]
            {
                new object[] {"WithEmptyObject", ResourceDocument.Empty},
                new object[]
                    {
                        "WithNullResource", new ResourceDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToAuthorLink}
                                    },
                                Data = null
                            }
                    },
                new object[]
                    {
                        "WithResource", new ResourceDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleLink}
                                    },
                                Data = SampleData.ArticleResource
                            }
                    },
                new object[]
                    {
                        "WithResourceAndIncludedResources", new ResourceDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleLink}
                                    },
                                Data = SampleData.ArticleResourceWithResourceLinkage,
                                Included = new List<Resource>
                                    {
                                        SampleData.PersonResource,
                                        SampleData.CommentResource1,
                                        SampleData.CommentResource2
                                    }
                            }
                    }
            };

        public static readonly IEnumerable<object[]> ResourceIdentifierDocumentTestData = new[]
            {
                new object[] {"WithEmptyObject", ResourceIdentifierDocument.Empty},
                new object[]
                    {
                        "WithNullResourceIdentifier", new ResourceIdentifierDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToAuthorLink}
                                    },
                                Data = null
                            }
                    },
                new object[]
                    {
                        "WithResourceIdentifier", new ResourceIdentifierDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToAuthorLink}
                                    },
                                Data = SampleData.PersonResourceIdentifier
                            }
                    }
            };

        public static readonly IEnumerable<object[]> ResourceCollectionDocumentTestData = new[]
            {
                new object[] {"WithEmptyObject", ResourceCollectionDocument.Empty},
                new object[]
                    {
                        "WithEmptyResources", new ResourceCollectionDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleCollectionLink}
                                    },
                                Data = Enumerable.Empty<Resource>()
                                                 .ToList()
                            }
                    },
                new object[]
                    {
                        "WithResources", new ResourceCollectionDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>
                                    {
                                        SampleData.ArticleResource1,
                                        SampleData.ArticleResource2
                                    }
                            }
                    },
                new object[]
                    {
                        "WithResourcesAndIncludedResources", new ResourceCollectionDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleCollectionLink}
                                    },
                                Data = new List<Resource>
                                    {
                                        SampleData.ArticleResourceWithResourceLinkage1,
                                        SampleData.ArticleResourceWithResourceLinkage2
                                    },
                                Included = new List<Resource>
                                    {
                                        SampleData.PersonResource1,
                                        SampleData.PersonResource2,
                                        SampleData.CommentResource1,
                                        SampleData.CommentResource2,
                                        SampleData.CommentResource3,
                                        SampleData.CommentResource4
                                    }
                            }
                    }
            };

        public static readonly IEnumerable<object[]> ResourceIdentifierCollectionDocumentTestData = new[]
            {
                new object[] {"WithEmptyObject", ResourceIdentifierCollectionDocument.Empty},
                new object[]
                    {
                        "WithEmptyResourceIdentifiers", new ResourceIdentifierCollectionDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToCommentsLink}
                                    },
                                Data = Enumerable.Empty<ResourceIdentifier>()
                                                 .ToList()
                            }
                    },
                new object[]
                    {
                        "WithResourceIdentifiers", new ResourceIdentifierCollectionDocument
                            {
                                JsonApiVersion = SampleData.JsonApiVersionAndMeta,
                                Meta = SampleData.DocumentMeta,
                                Links = new Links
                                    {
                                        {Keywords.Self, SampleData.ArticleToRelationshipsToCommentsLink}
                                    },
                                Data = new List<ResourceIdentifier>
                                    {
                                        SampleData.CommentResourceIdentifier1,
                                        SampleData.CommentResourceIdentifier2
                                    }
                            }
                    }
            };
        #endregion
    }
}
