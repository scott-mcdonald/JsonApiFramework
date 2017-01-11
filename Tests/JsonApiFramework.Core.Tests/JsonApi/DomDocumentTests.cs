// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Internal;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class DomDocumentTests : XUnitTests
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
        [MemberData(nameof(JsonApiVersionTestData))]
        public void TestJsonSerialize(DomTreeSerializationUnitTestFactory domTreeSerializationUnitTestFactory)
        {
            var data = domTreeSerializationUnitTestFactory.Data;
            var factory = domTreeSerializationUnitTestFactory.DomTreeSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(JsonApiVersionTestData))]
        public void TestJsonDeserialize(DomTreeSerializationUnitTestFactory domTreeSerializationUnitTestFactory)
        {
            var data = domTreeSerializationUnitTestFactory.Data;
            var factory = domTreeSerializationUnitTestFactory.DomTreeDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly IServiceModel TestServiceModel = default(IServiceModel);

        private static readonly JsonSerializerSettings TestSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                    {
                        new DomDocumentConverter(TestServiceModel)
                    },
                Formatting = Formatting.Indented
            };

        private static readonly JsonSerializerSettings TestSettingsIgnoreNull = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
                    {
                        new DomDocumentConverter(TestServiceModel)
                    },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings TestSettingsIncludeNull = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                    {
                        new DomDocumentConverter(TestServiceModel)
                    },
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };

        // ReSharper disable MemberCanBePrivate.Global
        public static readonly IEnumerable<object[]> JsonApiVersionTestData = new[]
            // ReSharper restore MemberCanBePrivate.Global
                {
                    new object[]
                        {
                            new DomTreeSerializationUnitTestFactory(
                                x => new DomTreeSerializeUnitTest<DomReadOnlyDocument>(x),
                                x => new DomTreeDeserializeUnitTest<DomReadOnlyDocument>(x),
                                new DomTreeSerializationUnitTestData(
                                    "WithNull",
                                    TestSettings,
                                    default(DomReadOnlyDocument),
                                    "null"))
                        },

                    new object[]
                        {
                            new DomTreeSerializationUnitTestFactory(
                                x => new DomTreeSerializeUnitTest<DomReadOnlyDocument>(x),
                                x => new DomTreeDeserializeUnitTest<DomReadOnlyDocument>(x, d => d.GetDocumentType().Should().Be(DocumentType.Document)),
                                new DomTreeSerializationUnitTestData(
                                    "WithEmptyObjectAndIgnoreNull",
                                    TestSettingsIgnoreNull,
                                    new DomReadOnlyDocument(TestServiceModel, DocumentType.Document),
                                    "{}"))
                        },

                    new object[]
                        {
                            new DomTreeSerializationUnitTestFactory(
                                x => new DomTreeSerializeUnitTest<DomReadOnlyDocument>(x),
                                x => new DomTreeDeserializeUnitTest<DomReadOnlyDocument>(x, d => d.GetDocumentType().Should().Be(DocumentType.Document)),
                                new DomTreeSerializationUnitTestData(
                                    "WithEmptyObjectAndIncludeNull",
                                    TestSettingsIncludeNull,
                                    new DomReadOnlyDocument(TestServiceModel, DocumentType.Document),
                                    @"{
  ""jsonapi"": null,
  ""meta"": null,
  ""links"": null
}"))
                        },

                    new object[]
                        {
                            new DomTreeSerializationUnitTestFactory(
                                x => new DomTreeSerializeUnitTest<DomReadOnlyDocument>(x),
                                x => new DomTreeDeserializeUnitTest<DomReadOnlyDocument>(x, d => d.GetDocumentType().Should().Be(DocumentType.Document)),
                                new DomTreeSerializationUnitTestData(
                                    "WithJsonApiVersionAndIgnoreNull",
                                    TestSettingsIgnoreNull,
                                    new DomReadOnlyDocument(TestServiceModel, DocumentType.Document,
                                        new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10)),
                                    @"{
  ""jsonapi"": {
    ""version"": ""1.0""
  }
}"))
                        },

                    new object[]
                        {
                            new DomTreeSerializationUnitTestFactory(
                                x => new DomTreeSerializeUnitTest<DomReadOnlyDocument>(x),
                                x => new DomTreeDeserializeUnitTest<DomReadOnlyDocument>(x, d => d.GetDocumentType().Should().Be(DocumentType.Document)),
                                new DomTreeSerializationUnitTestData(
                                    "WithJsonApiVersionAndIncludeNull",
                                    TestSettingsIncludeNull,
                                    new DomReadOnlyDocument(TestServiceModel, DocumentType.Document,
                                        new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10)),
                                    @"{
  ""jsonapi"": {
    ""version"": ""1.0"",
    ""meta"": null
  },
  ""meta"": null,
  ""links"": null
}"))
                        },

                    new object[]
                        {
                            new DomTreeSerializationUnitTestFactory(
                                x => new DomTreeSerializeUnitTest<DomReadOnlyDocument>(x),
                                x => new DomTreeDeserializeUnitTest<DomReadOnlyDocument>(x, d => d.GetDocumentType().Should().Be(DocumentType.Document)),
                                new DomTreeSerializationUnitTestData(
                                    "WithJsonApiVersionAndMetaAndIgnoreNull",
                                    TestSettingsIgnoreNull,
                                    new DomReadOnlyDocument(TestServiceModel, DocumentType.Document,
                                        new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10),
                                        new DomReadOnlyMeta(Meta.Create(new DocumentMeta
                                            {
                                                IsPublic = true,
                                                Version = 2.1m,
                                                Copyright = "Copyright 2015 Example Corporation.",
                                                Authors = new[] {"John Doe", "Jane Doe"}
                                            }))),
                                    @"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""meta"": {
    ""is-public"": true,
    ""version"": 2.1,
    ""copyright"": ""Copyright 2015 Example Corporation."",
    ""authors"": [
      ""John Doe"",
      ""Jane Doe""
    ]
  }
}"))
                        },

                    new object[]
                        {
                            new DomTreeSerializationUnitTestFactory(
                                x => new DomTreeSerializeUnitTest<DomReadOnlyDocument>(x),
                                x => new DomTreeDeserializeUnitTest<DomReadOnlyDocument>(x, d => d.GetDocumentType().Should().Be(DocumentType.Document)),
                                new DomTreeSerializationUnitTestData(
                                    "WithJsonApiVersionAndMetaAndIncludeNull",
                                    TestSettingsIncludeNull,
                                    new DomReadOnlyDocument(TestServiceModel, DocumentType.Document,
                                        new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10),
                                        new DomReadOnlyMeta(Meta.Create(new DocumentMeta
                                            {
                                                IsPublic = true,
                                                Version = 2.1m,
                                                Copyright = "Copyright 2015 Example Corporation.",
                                                Authors = new[] {"John Doe", "Jane Doe"}
                                            }))),
                                    @"{
  ""jsonapi"": {
    ""version"": ""1.0"",
    ""meta"": null
  },
  ""meta"": {
    ""is-public"": true,
    ""version"": 2.1,
    ""copyright"": ""Copyright 2015 Example Corporation."",
    ""authors"": [
      ""John Doe"",
      ""Jane Doe""
    ]
  },
  ""links"": null
}"))
                        },

                    new object[]
                        {
                            new DomTreeSerializationUnitTestFactory(
                                x => new DomTreeSerializeUnitTest<DomReadOnlyDocument>(x),
                                x => new DomTreeDeserializeUnitTest<DomReadOnlyDocument>(x, d => d.GetDocumentType().Should().Be(DocumentType.Document)),
                                new DomTreeSerializationUnitTestData(
                                    "WithJsonApiVersionAndMetaAndLinksAndIgnoreNull",
                                    TestSettingsIgnoreNull,
                                    new DomReadOnlyDocument(TestServiceModel, DocumentType.Document,
                                        new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10),
                                        new DomReadOnlyMeta(Meta.Create(new DocumentMeta
                                            {
                                                IsPublic = true,
                                                Version = 2.1m,
                                                Copyright = "Copyright 2015 Example Corporation.",
                                                Authors = new[] {"John Doe", "Jane Doe"}
                                            })),
                                        new DomReadOnlyLinks(new Links(new Dictionary<string, Link>
                                                {
                                                        {Keywords.Up, "https://api.example.com/articles"},
                                                        {Keywords.Self, "https://api.example.com/articles/24"}
                                                })
                                        )),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  },
  ""meta"": {
    ""is-public"": true,
    ""version"": 2.1,
    ""copyright"": ""Copyright 2015 Example Corporation."",
    ""authors"": [
      ""John Doe"",
      ""Jane Doe""
    ]
  },
  ""links"": {
    ""up"": ""https://api.example.com/articles"",
    ""self"": ""https://api.example.com/articles/24""
  }
}"))
                        },

                    new object[]
                        {
                            new DomTreeSerializationUnitTestFactory(
                                x => new DomTreeSerializeUnitTest<DomReadOnlyDocument>(x),
                                x => new DomTreeDeserializeUnitTest<DomReadOnlyDocument>(x, d => d.GetDocumentType().Should().Be(DocumentType.Document)),
                                new DomTreeSerializationUnitTestData(
                                    "WithJsonApiVersionAndMetaAndLinksAndIncludeNull",
                                    TestSettingsIncludeNull,
                                    new DomReadOnlyDocument(TestServiceModel, DocumentType.Document,
                                        new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10),
                                        new DomReadOnlyMeta(Meta.Create(new DocumentMeta
                                            {
                                                IsPublic = true,
                                                Version = 2.1m,
                                                Copyright = "Copyright 2015 Example Corporation.",
                                                Authors = new[] {"John Doe", "Jane Doe"}
                                            })),
                                        new DomReadOnlyLinks(new Links(new Dictionary<string, Link>
                                                {
                                                        {Keywords.Up, "https://api.example.com/articles"},
                                                        {Keywords.Self, "https://api.example.com/articles/24"}
                                                })
                                        )),
                                    @"{
  ""jsonapi"": {
    ""version"": ""1.0"",
    ""meta"": null
  },
  ""meta"": {
    ""is-public"": true,
    ""version"": 2.1,
    ""copyright"": ""Copyright 2015 Example Corporation."",
    ""authors"": [
      ""John Doe"",
      ""Jane Doe""
    ]
  },
  ""links"": {
    ""up"": {
      ""href"": ""https://api.example.com/articles"",
      ""meta"": null
    },
    ""self"":{
      ""href"": ""https://api.example.com/articles/24"",
      ""meta"": null
    }
  }
}"))
                        },

                };
        #endregion
    }
}
