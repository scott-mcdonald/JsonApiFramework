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
    public class DomLinksTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomLinksTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomLinksTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomLinksTestData))]
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

        private static readonly JsonSerializerSettings TestJsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                Formatting = Formatting.Indented
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNull = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNull = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };

        public static readonly IEnumerable<object[]> DomLinksTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomLinks>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLinks>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithZeroLinks",
                                TestJsonSerializerSettings,
                                new DomLinks(),
                                @"{}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomLinks>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLinks>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithOneLinkAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomLinks(
                                    new DomProperty(PropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(PropertyType.Meta, "meta")))),
                                new DomLinks(
                                    new DomProperty(PropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42"))))),
@"{
  ""self"":  ""https://api.example.com/articles/42""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomLinks>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLinks>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithOneLinkAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomLinks(
                                    new DomProperty(PropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(PropertyType.Meta, "meta")))),
@"{
  ""self"":  {
    ""href"": ""https://api.example.com/articles/42"",
    ""meta"": null
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomLinks>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLinks>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithOneLinkAndMeta",
                                TestJsonSerializerSettings,
                                new DomLinks(
                                    new DomProperty(PropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(PropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("is-public", new DomValue<bool>(true)),
                                                    new DomProperty("version", new DomValue<string>("2.0"))))))),
@"{
  ""self"":  {
    ""href"": ""https://api.example.com/articles/42"",
    ""meta"": {
      ""is-public"": true,
      ""version"": ""2.0""
    }
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomLinks>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLinks>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithTwoLinksAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomLinks(
                                    new DomProperty(PropertyType.Link, "up",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                            new DomProperty(PropertyType.Meta, "meta"))),
                                    new DomProperty(PropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(PropertyType.Meta, "meta")))),
                                new DomLinks(
                                    new DomProperty(PropertyType.Link, "up",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")))),
                                    new DomProperty(PropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42"))))),
@"{
  ""up"": ""https://api.example.com/articles"",
  ""self"": ""https://api.example.com/articles/42""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomLinks>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLinks>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithTwoLinksAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomLinks(
                                    new DomProperty(PropertyType.Link, "up",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                            new DomProperty(PropertyType.Meta, "meta"))),
                                    new DomProperty(PropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(PropertyType.Meta, "meta")))),
@"{
  ""up"":  {
    ""href"": ""https://api.example.com/articles"",
    ""meta"": null
  },
  ""self"":  {
    ""href"": ""https://api.example.com/articles/42"",
    ""meta"": null
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomLinks>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLinks>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithTwoLinksAndMeta",
                                TestJsonSerializerSettings,
                                new DomLinks(
                                    new DomProperty(PropertyType.Link, "up",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                            new DomProperty(PropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("is-public", new DomValue<bool>(true)),
                                                    new DomProperty("version", new DomValue<string>("2.0")))))),
                                    new DomProperty(PropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(PropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(PropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("is-public", new DomValue<bool>(true)),
                                                    new DomProperty("version", new DomValue<string>("2.0"))))))),
@"{
  ""up"":  {
    ""href"": ""https://api.example.com/articles"",
    ""meta"": {
      ""is-public"": true,
      ""version"": ""2.0""
    }
  },
  ""self"":  {
    ""href"": ""https://api.example.com/articles/42"",
    ""meta"": {
      ""is-public"": true,
      ""version"": ""2.0""
    }
  }
}"))
                    },

        };
        #endregion
    }
}
