// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Dom;
using JsonApiFramework.Dom.Internal;
using JsonApiFramework.Tests.JsonApi.Dom;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Dom
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
        [MemberData(nameof(DomJsonSerializationTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationTest)
        {
            var data = domJsonSerializationTest.Data;
            var factory = domJsonSerializationTest.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomJsonSerializationTestData))]
        public void TestJsonDeserialize(DomJsonSerializationUnitTestFactory domJsonSerializationTest)
        {
            var data = domJsonSerializationTest.Data;
            var factory = domJsonSerializationTest.DomJsonDeserializeUnitTestFactory;
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

        public static readonly IEnumerable<object[]> DomJsonSerializationTestData = new[]
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
                                "WithOneLink",
                                TestJsonSerializerSettings,
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
                                "WithOneLinkAndMeta",
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
                                "WithTwoLinks",
                                TestJsonSerializerSettings,
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
                                "WithTwoLinksAndMeta",
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
