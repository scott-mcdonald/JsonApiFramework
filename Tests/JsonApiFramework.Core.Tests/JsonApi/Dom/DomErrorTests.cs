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
    public class DomErrorTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomErrorTests(ITestOutputHelper output)
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

        public static readonly IEnumerable<object[]> DomJsonSerializationTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomError>(x),
                            x => new DomJsonDeserializeUnitTest<IDomError>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithBareErrorAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomError(
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2")),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Status, "status"),
                                    new DomProperty(ApiPropertyType.Code, "code"),
                                    new DomProperty(ApiPropertyType.Title, "title"),
                                    new DomProperty(ApiPropertyType.Detail, "detail"),
                                    new DomProperty(ApiPropertyType.Source, "source"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomError(
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2"))),
@"{
  ""id"": ""a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomError>(x),
                            x => new DomJsonDeserializeUnitTest<IDomError>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithBareErrorAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomError(
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2")),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Status, "status"),
                                    new DomProperty(ApiPropertyType.Code, "code"),
                                    new DomProperty(ApiPropertyType.Title, "title"),
                                    new DomProperty(ApiPropertyType.Detail, "detail"),
                                    new DomProperty(ApiPropertyType.Source, "source"),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""id"": ""a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2"",
  ""links"": null,
  ""status"": null,
  ""code"": null,
  ""title"": null,
  ""detail"": null,
  ""source"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomError>(x),
                            x => new DomJsonDeserializeUnitTest<IDomError>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithBasicErrorAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomError(
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2")),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Status, "status", new DomValue<string>("404")),
                                    new DomProperty(ApiPropertyType.Code, "code"),
                                    new DomProperty(ApiPropertyType.Title, "title", new DomValue<string>("Invalid Attribute")),
                                    new DomProperty(ApiPropertyType.Detail, "detail", new DomValue<string>("First name must contain at least three characters.")),
                                    new DomProperty(ApiPropertyType.Source, "source",
                                        new DomObject(
                                            new DomProperty("pointer", new DomValue<string>("/data/attributes/first-name")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomError(
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2")),
                                    new DomProperty(ApiPropertyType.Status, "status", new DomValue<string>("404")),
                                    new DomProperty(ApiPropertyType.Title, "title", new DomValue<string>("Invalid Attribute")),
                                    new DomProperty(ApiPropertyType.Detail, "detail", new DomValue<string>("First name must contain at least three characters.")),
                                    new DomProperty(ApiPropertyType.Source, "source",
                                        new DomObject(
                                            new DomProperty("pointer", new DomValue<string>("/data/attributes/first-name"))))),
@"{
  ""id"": ""a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2"",
  ""status"": ""404"",
  ""title"": ""Invalid Attribute"",
  ""detail"": ""First name must contain at least three characters."",
  ""source"": {
    ""pointer"": ""/data/attributes/first-name""
  }
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomError>(x),
                            x => new DomJsonDeserializeUnitTest<IDomError>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithBasicErrorAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomError(
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2")),
                                    new DomProperty(ApiPropertyType.Links, "links"),
                                    new DomProperty(ApiPropertyType.Status, "status", new DomValue<string>("404")),
                                    new DomProperty(ApiPropertyType.Code, "code"),
                                    new DomProperty(ApiPropertyType.Title, "title", new DomValue<string>("Invalid Attribute")),
                                    new DomProperty(ApiPropertyType.Detail, "detail", new DomValue<string>("First name must contain at least three characters.")),
                                    new DomProperty(ApiPropertyType.Source, "source",
                                        new DomObject(
                                            new DomProperty("pointer", new DomValue<string>("/data/attributes/first-name")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""id"": ""a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2"",
  ""links"": null,
  ""status"": ""404"",
  ""code"": null,
  ""title"": ""Invalid Attribute"",
  ""detail"": ""First name must contain at least three characters."",
  ""source"": {
    ""pointer"": ""/data/attributes/first-name""
  },
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomError>(x),
                            x => new DomJsonDeserializeUnitTest<IDomError>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithCompleteError",
                                TestJsonSerializerSettings,
                                new DomError(
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2")),
                                    new DomProperty(ApiPropertyType.Links, "links",
                                        new DomLinks(
                                            new DomProperty(ApiPropertyType.Link, "about",
                                                new DomLink(
                                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/about/first-name-minimum-requirement")))))),
                                    new DomProperty(ApiPropertyType.Status, "status", new DomValue<string>("404")),
                                    new DomProperty(ApiPropertyType.Code, "code", new DomValue<string>("24")),
                                    new DomProperty(ApiPropertyType.Title, "title", new DomValue<string>("Invalid Attribute")),
                                    new DomProperty(ApiPropertyType.Detail, "detail", new DomValue<string>("First name must contain at least three characters.")),
                                    new DomProperty(ApiPropertyType.Source, "source",
                                        new DomObject(
                                            new DomProperty("pointer", new DomValue<string>("/data/attributes/first-name")))),
                                    new DomProperty(ApiPropertyType.Meta, "meta",
                                        new DomObject(
                                            new DomProperty("stack-trace", new DomValue<string>("Foo.Method1 line 42\nFoo.Method2 line 24\nBar.Method1 line 86\nBar.Method2 line 68"))))),
@"{
  ""id"": ""a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2"",
  ""links"": {
    ""about"": ""https://api.example.com/about/first-name-minimum-requirement""
  },
  ""status"": ""404"",
  ""code"": ""24"",
  ""title"": ""Invalid Attribute"",
  ""detail"": ""First name must contain at least three characters."",
  ""source"": {
    ""pointer"": ""/data/attributes/first-name""
  },
  ""meta"": {
    ""stack-trace"": ""Foo.Method1 line 42\nFoo.Method2 line 24\nBar.Method1 line 86\nBar.Method2 line 68""
  }
}"))
                    },

        };
        #endregion
    }
}
