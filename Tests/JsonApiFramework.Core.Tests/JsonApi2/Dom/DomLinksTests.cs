// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi2.Dom;
using JsonApiFramework.JsonApi2.Dom.Internal;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi2.Dom
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
                                "WithOneLinkAndHRefAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomLinks(
                                    new DomProperty(ApiPropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(ApiPropertyType.Meta, "meta")))),
                                new DomLinks(
                                    new DomProperty(ApiPropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42"))))),
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
                                "WithOneLinkAndHRefAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomLinks(
                                    new DomProperty(ApiPropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(ApiPropertyType.Meta, "meta")))),
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
                                "WithOneLinkAndHRefAndMeta",
                                TestJsonSerializerSettings,
                                new DomLinks(
                                    new DomProperty(ApiPropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(ApiPropertyType.Meta, "meta",
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
                                "WithTwoLinksAndHRefAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomLinks(
                                    new DomProperty(ApiPropertyType.Link, "up",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                            new DomProperty(ApiPropertyType.Meta, "meta"))),
                                    new DomProperty(ApiPropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(ApiPropertyType.Meta, "meta")))),
                                new DomLinks(
                                    new DomProperty(ApiPropertyType.Link, "up",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")))),
                                    new DomProperty(ApiPropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42"))))),
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
                                "WithTwoLinksAndHRefAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomLinks(
                                    new DomProperty(ApiPropertyType.Link, "up",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                            new DomProperty(ApiPropertyType.Meta, "meta"))),
                                    new DomProperty(ApiPropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(ApiPropertyType.Meta, "meta")))),
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
                                "WithTwoLinksAndHRefAndMeta",
                                TestJsonSerializerSettings,
                                new DomLinks(
                                    new DomProperty(ApiPropertyType.Link, "up",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                            new DomProperty(ApiPropertyType.Meta, "meta",
                                                new DomObject(
                                                    new DomProperty("is-public", new DomValue<bool>(true)),
                                                    new DomProperty("version", new DomValue<string>("2.0")))))),
                                    new DomProperty(ApiPropertyType.Link, "self",
                                        new DomLink(
                                            new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles/42")),
                                            new DomProperty(ApiPropertyType.Meta, "meta",
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
