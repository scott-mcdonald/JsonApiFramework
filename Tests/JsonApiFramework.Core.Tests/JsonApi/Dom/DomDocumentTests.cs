// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Dom;
using JsonApiFramework.JsonApi.Dom.Internal;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi.Dom
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
        [MemberData(nameof(DomDocumentTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomDocumentTestData))]
        public void TestJsonDeserialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly DomJsonSerializerSettings TestDomJsonSerializerSettings = new DomJsonSerializerSettings
            {
                NullValueHandlingOverrides = null
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNull = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNull = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };

        public static readonly IEnumerable<object[]> DomDocumentTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithEmptyObjectAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links")),
                                new DomDocument(DocumentType.Document),
                            @"{}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithEmptyObjectAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi"),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links")),
@"{
  ""jsonapi"": null,
  ""meta"": null,
  ""links"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithJsonApiAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApiVersion(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")),
                                            new DomProperty(PropertyType.Meta, "meta"))),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links")),
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApiVersion(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0"))))),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithJsonApiAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApiVersion(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")),
                                            new DomProperty(PropertyType.Meta, "meta"))),
                                    new DomProperty(PropertyType.Meta, "meta"),
                                    new DomProperty(PropertyType.Links, "links")),
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
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithJsonApiAndMetaAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApiVersion(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")),
                                            new DomProperty(PropertyType.Meta, "meta"))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright", new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(PropertyType.Links, "links")),
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApiVersion(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright", new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe"))))))),
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
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithJsonApiAndMetaAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApiVersion(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")),
                                            new DomProperty(PropertyType.Meta, "meta"))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright", new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(PropertyType.Links, "links")),
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
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithJsonApiAndMetaAndLinksAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApiVersion(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")),
                                            new DomProperty(PropertyType.Meta, "meta"))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright", new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(PropertyType.Links, "links", new DomLinks(
                                        new DomProperty(PropertyType.Link, "up",
                                            new DomLink(
                                                new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                                new DomProperty(PropertyType.Meta, "meta"))),
                                        new DomProperty(PropertyType.Link, "self",
                                            new DomLink(
                                                new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                                new DomProperty(PropertyType.Meta, "meta")))))),
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApiVersion(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright", new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(PropertyType.Links, "links", new DomLinks(
                                        new DomProperty(PropertyType.Link, "up",
                                            new DomLink(
                                                new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")))),
                                        new DomProperty(PropertyType.Link, "self",
                                            new DomLink(
                                                new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42"))))))),
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
    ""self"": ""https://api.example.com/articles/42""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithJsonApiAndMetaAndLinksAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomDocument(DocumentType.Document,
                                    new DomProperty(PropertyType.JsonApi, "jsonapi",
                                        new DomJsonApiVersion(
                                            new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")),
                                            new DomProperty(PropertyType.Meta, "meta"))),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<decimal>(2.1m)),
                                        new DomProperty("copyright", new DomValue<string>("Copyright 2015 Example Corporation.")),
                                        new DomProperty("authors", new DomArray(
                                            new DomItem(0, new DomValue<string>("John Doe")),
                                            new DomItem(1, new DomValue<string>("Jane Doe")))))),
                                    new DomProperty(PropertyType.Links, "links", new DomLinks(
                                        new DomProperty(PropertyType.Link, "up",
                                            new DomLink(
                                                new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                                new DomProperty(PropertyType.Meta, "meta"))),
                                        new DomProperty(PropertyType.Link, "self",
                                            new DomLink(
                                                new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                                new DomProperty(PropertyType.Meta, "meta")))))),
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
    ""self"": {
      ""href"": ""https://api.example.com/articles/42"",
      ""meta"": null
    }
  }
}"))
                    },

        };
        #endregion
    }
}
