// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Internal;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class DomDocumentTests : XUnitTest
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

        private static readonly JsonSerializerSettings TestSettingsIncludeNull = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                    {
                        new DomDocumentConverter(TestServiceModel)
                    },
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
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

        // ReSharper disable MemberCanBePrivate.Global
        public static readonly IEnumerable<object[]> JsonApiVersionTestData = new[]
        // ReSharper restore MemberCanBePrivate.Global
            {
                new object[]
                    {
                        new DomTreeSerializationUnitTestFactory(
                            x => new DomTreeSerializeUnitTest<DomDocument>(x),
                            x => new DomTreeDeserializeUnitTest<DomDocument>(x),
                            new DomTreeSerializationUnitTestData(
                                "WithNull",
                                TestSettings,
                                default(DomDocument),
                                "null"))
                    },

                new object[]
                    {
                        new DomTreeSerializationUnitTestFactory(
                            x => new DomTreeSerializeUnitTest<DomDocument>(x),
                            x => new DomTreeDeserializeUnitTest<DomDocument>(x),
                            new DomTreeSerializationUnitTestData(
                                "WithEmptyObjectAndIncludeNull",
                                TestSettingsIncludeNull,
                                new DomDocument(TestServiceModel),
@"{
  ""jsonapi"": null,
  ""meta"": null,
  ""links"": null
}"))
                    },

                new object[]
                    {
                        new DomTreeSerializationUnitTestFactory(
                            x => new DomTreeSerializeUnitTest<DomDocument>(x),
                            x => new DomTreeDeserializeUnitTest<DomDocument>(x),
                            new DomTreeSerializationUnitTestData(
                                "WithJspnApiAndIgnoreNull",
                                TestSettingsIgnoreNull,
                                new DomDocument(TestServiceModel),
                                "{}"))
                    },

                new object[]
                    {
                        new DomTreeSerializationUnitTestFactory(
                            x => new DomTreeSerializeUnitTest<DomDocument>(x),
                            x => new DomTreeDeserializeUnitTest<DomDocument>(x),
                            new DomTreeSerializationUnitTestData(
                                "WithJsonApiVersionAndIncludeNull",
                                TestSettingsIncludeNull,
                                new DomDocument(TestServiceModel,
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
                            x => new DomTreeSerializeUnitTest<DomDocument>(x),
                            x => new DomTreeDeserializeUnitTest<DomDocument>(x),
                            new DomTreeSerializationUnitTestData(
                                "WithJsonApiVersionAndIgnoreNull",
                                TestSettingsIgnoreNull,
                                new DomDocument(TestServiceModel,
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
                            x => new DomTreeSerializeUnitTest<DomDocument>(x),
                            x => new DomTreeDeserializeUnitTest<DomDocument>(x),
                            new DomTreeSerializationUnitTestData(
                                "WithJsonApiVersionAndMetaAndIncludeNull",
                                TestSettingsIncludeNull,
                                new DomDocument(TestServiceModel,
                                    new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10),
                                    new DomReadOnlyMeta(JsonApiSampleData.DocumentMeta)),
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
                            x => new DomTreeSerializeUnitTest<DomDocument>(x),
                            x => new DomTreeDeserializeUnitTest<DomDocument>(x),
                            new DomTreeSerializationUnitTestData(
                                "WithJsonApiVersionAndMetaAndIgnoreNull",
                                TestSettingsIgnoreNull,
                                new DomDocument(TestServiceModel,
                                    new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10),
                                    new DomReadOnlyMeta(JsonApiSampleData.DocumentMeta)),
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
                            x => new DomTreeSerializeUnitTest<DomDocument>(x),
                            x => new DomTreeDeserializeUnitTest<DomDocument>(x),
                            new DomTreeSerializationUnitTestData(
                                "WithJsonApiVersionAndMetaAndLinksAndIncludeNull",
                                TestSettingsIncludeNull,
                                new DomDocument(TestServiceModel,
                                    new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10),
                                    new DomReadOnlyMeta(JsonApiSampleData.DocumentMeta),
                                    new DomReadOnlyLinks(new Links(new Dictionary<string, Link>
                                        {
                                            { Keywords.Up, new Link("https://api.example.com/articles") },
                                            { Keywords.Self, new Link("https://api.example.com/articles/24") }
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
    ""up"": ""https://api.example.com/articles"",
    ""self"": ""https://api.example.com/articles/24""
  }
}"))
                    },

                new object[]
                    {
                        new DomTreeSerializationUnitTestFactory(
                            x => new DomTreeSerializeUnitTest<DomDocument>(x),
                            x => new DomTreeDeserializeUnitTest<DomDocument>(x),
                            new DomTreeSerializationUnitTestData(
                                "WithJsonApiVersionAndMetaAndLinksAndIgnoreNull",
                                TestSettingsIgnoreNull,
                                new DomDocument(TestServiceModel,
                                    new DomReadOnlyJsonApiVersion(JsonApiVersion.Version10),
                                    new DomReadOnlyMeta(JsonApiSampleData.DocumentMeta),
                                    new DomReadOnlyLinks(new Links(new Dictionary<string, Link>
                                        {
                                            { Keywords.Up, new Link("https://api.example.com/articles") },
                                            { Keywords.Self, new Link("https://api.example.com/articles/24") }
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

            };


        #endregion
    }
}
