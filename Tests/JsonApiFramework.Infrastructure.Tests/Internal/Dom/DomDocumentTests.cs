// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.TestAsserts.Internal.Dom;
using JsonApiFramework.TestAsserts.JsonApi;
using JsonApiFramework.TestData.ApiResources;
using JsonApiFramework.TestData.ClrResources;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Internal.Dom
{
    public class DomDocumentTests : DomXUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomDocumentTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("DomDocumentTestData")]
        public void TestDomDocumentParse(string name, IServiceModel serviceModel, Document apiDocument)
        {
            // Arrange

            // Act
            var domDocument = DomDocument.Parse(apiDocument, serviceModel);

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(domDocument);
            this.OutputEmptyLine();
            this.OutputJson(apiDocument);

            // Assert
            DomDocumentAssert.Equal(apiDocument, domDocument);
        }

        [Theory]
        [MemberData("DomDocumentTestData")]
        public void TestDomDocumentDomResources(string name, IServiceModel serviceModel, Document apiDocument)
        {
            // Arrange
            var domDocument = DomDocument.Parse(apiDocument, serviceModel);

            var expectedApiResources = new List<Resource>();
            if (!apiDocument.IsDataNullOrEmpty())
            {
                if (apiDocument.IsResourceDocument())
                {
                    var apiResource = apiDocument.GetResource();
                    expectedApiResources.Add(apiResource);
                }
                else if (apiDocument.IsResourceCollectionDocument())
                {
                    var apiResourceCollection = apiDocument.GetResourceCollection();
                    expectedApiResources.AddRange(apiResourceCollection);
                }
            }
            if (!apiDocument.IsIncludedNullOrEmpty())
            {
                var apiIncludedResources = apiDocument.GetIncludedResources();
                expectedApiResources.AddRange(apiIncludedResources);
            }

            // Act
            var actualDomResources = domDocument.DomResources()
                                                .SafeToList();
            var actualApiResources = actualDomResources.Select(x => x.ApiResource)
                                                       .SafeToList();

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(domDocument);
            this.OutputEmptyLine();

            this.Output.WriteLine("Expected Resources");
            foreach (var expectedApiResource in expectedApiResources)
            {
                this.Output.WriteLine(expectedApiResource.ToString());
            }
            this.OutputEmptyLine();
            this.Output.WriteLine("Actual Resources");
            foreach (var actualApiResource in actualApiResources)
            {
                this.Output.WriteLine(actualApiResource.ToString());
            }

            // Assert
            ResourceAssert.Equal(expectedApiResources, actualApiResources);
        }

        [Theory]
        [MemberData("DomDocumentTestData")]
        public void TestDomDocumentDomResourceIdentifiers(string name, IServiceModel serviceModel, Document apiDocument)
        {
            // Arrange
            var domDocument = DomDocument.Parse(apiDocument, serviceModel);

            var expectedApiResourceIdentifiers = new List<ResourceIdentifier>();
            if (!apiDocument.IsDataNullOrEmpty())
            {
                if (apiDocument.IsResourceIdentifierDocument())
                {
                    var apiResourceIdentifier = apiDocument.GetResourceIdentifier();
                    expectedApiResourceIdentifiers.Add(apiResourceIdentifier);
                }
                else if (apiDocument.IsResourceIdentifierCollectionDocument())
                {
                    var apiResourceIdentifierCollection = apiDocument.GetResourceIdentifierCollection();
                    expectedApiResourceIdentifiers.AddRange(apiResourceIdentifierCollection);
                }
            }

            // Act
            var actualDomResourceIdentifiers = domDocument.DomResourceIdentitifiers()
                                                          .SafeToList();
            var actualApiResourceIdentifiers = actualDomResourceIdentifiers.Select(x => x.ApiResourceIdentifier)
                                                                           .SafeToList();

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(domDocument);
            this.OutputEmptyLine();

            this.Output.WriteLine("Expected Resource Identifiers");
            foreach (var expectedApiResourceIdentifier in expectedApiResourceIdentifiers)
            {
                this.Output.WriteLine(expectedApiResourceIdentifier.ToString());
            }
            this.OutputEmptyLine();
            this.Output.WriteLine("Actual Resource Identifiers");
            foreach (var actualApiResourceIdentifier in actualApiResourceIdentifiers)
            {
                this.Output.WriteLine(actualApiResourceIdentifier.ToString());
            }

            // Assert
            ResourceIdentifierAssert.Equal(expectedApiResourceIdentifiers, actualApiResourceIdentifiers);
        }

        [Theory]
        [MemberData("DomDocumentTestData")]
        public void TestDomDocumentDomResourceIdentities(string name, IServiceModel serviceModel, Document apiDocument)
        {
            // Arrange
            var domDocument = DomDocument.Parse(apiDocument, serviceModel);

            var expectedResourceIdentities = new List<ResourceIdentity>();
            if (!apiDocument.IsDataNullOrEmpty())
            {
                if (apiDocument.IsResourceDocument())
                {
                    var apiResource = apiDocument.GetResource();
                    expectedResourceIdentities.Add(new ResourceIdentity(apiResource));
                }
                else if (apiDocument.IsResourceCollectionDocument())
                {
                    var apiResourceCollection = apiDocument.GetResourceCollection();
                    expectedResourceIdentities.AddRange(apiResourceCollection.Select(x => new ResourceIdentity(x)));
                }
                else if (apiDocument.IsResourceIdentifierDocument())
                {
                    var apiResourceIdentifier = apiDocument.GetResourceIdentifier();
                    expectedResourceIdentities.Add(new ResourceIdentity(apiResourceIdentifier));
                }
                else if (apiDocument.IsResourceIdentifierCollectionDocument())
                {
                    var apiResourceIdentifierCollection = apiDocument.GetResourceIdentifierCollection();
                    expectedResourceIdentities.AddRange(apiResourceIdentifierCollection.Select(x => new ResourceIdentity(x)));
                }
            }

            if (!apiDocument.IsIncludedNullOrEmpty())
            {
                var apiIncludedResourceCollection = apiDocument.GetIncludedResources();
                expectedResourceIdentities.AddRange(apiIncludedResourceCollection.Select(x => new ResourceIdentity(x)));
            }

            // Act
            var actualDomResourceIdentities = domDocument.DomResourceIdentities()
                                                         .SafeToList();
            var actualResourceIdentities = actualDomResourceIdentities.Select(x => new ResourceIdentity(x))
                                                                      .SafeToList();

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);
            this.OutputDomTree(domDocument);
            this.OutputEmptyLine();

            this.Output.WriteLine("Expected Resource Identities");
            foreach (var expectedResourceIdentity in expectedResourceIdentities)
            {
                this.Output.WriteLine(expectedResourceIdentity.ToString());
            }
            this.OutputEmptyLine();
            this.Output.WriteLine("Actual Resource Identities");
            foreach (var actualResourceIdentity in actualResourceIdentities)
            {
                this.Output.WriteLine(actualResourceIdentity.ToString());
            }

            // Assert
            var expectedCount = expectedResourceIdentities.Count;
            var actualCount = actualResourceIdentities.Count;
            Assert.Equal(expectedCount, actualCount);

            var count = expectedCount;
            for (var i = 0; i < count; ++i)
            {
                var expectedResourceIdentity = expectedResourceIdentities[i];
                var actualResourceIdentity = actualResourceIdentities[i];

                Assert.Equal(expectedResourceIdentity.ApiResourceType, actualResourceIdentity.ApiResourceType);
                Assert.Equal(expectedResourceIdentity.ApiResourceId, actualResourceIdentity.ApiResourceId);
            }
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> DomDocumentTestData = new[]
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
                                        {Keywords.Self, ApiSampleData.ArticleCollectionLink}
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

        #region Test Types
        private class ResourceIdentity
        {
            public ResourceIdentity(IDomResourceIdentity domResourceIdentity)
            {
                Contract.Requires(domResourceIdentity != null);

                this.ApiResourceType = domResourceIdentity.ApiResourceType;
                this.ApiResourceId = domResourceIdentity.ApiResourceId;
            }

            public ResourceIdentity(IGetResourceIdentity getResourceIdentity)
            {
                Contract.Requires(getResourceIdentity != null);

                this.ApiResourceType = getResourceIdentity.Type;
                this.ApiResourceId = getResourceIdentity.Id;
            }

            public string ApiResourceType { get; private set; }
            public string ApiResourceId { get; private set; }

            public override string ToString()
            {
                var type = this.ApiResourceType ?? JsonConstants.Null;
                var id = this.ApiResourceId ?? JsonConstants.Null;
                return String.Format("{0} [type={1} id={2}]", typeof(ResourceIdentity).Name, type, id);
            }
        }
        #endregion
    }
}
