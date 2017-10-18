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
    public class DomJsonSerializerSettingsTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomJsonSerializerSettingsTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomJsonSerializerSettingsTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomJsonSerializerSettingsTestData))]
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
        private static readonly DomJsonSerializerSettings TestDomJsonSerializerSettingsMetaIgnoreNullOverride = new DomJsonSerializerSettings
            {
                NullValueHandlingOverrides = new Dictionary<ApiPropertyType, NullValueHandling>
                    {
                        {ApiPropertyType.Meta, NullValueHandling.Ignore}
                    }
            };

        private static readonly DomJsonSerializerSettings TestDomJsonSerializerSettingsMetaIncludeNullOverride = new DomJsonSerializerSettings
            {
                NullValueHandlingOverrides = new Dictionary<ApiPropertyType, NullValueHandling>
                    {
                        {ApiPropertyType.Meta, NullValueHandling.Include}
                    }
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNullAndMetaIgnoreNullOverride = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettingsMetaIgnoreNullOverride),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNullAndMetaIncludeNullOverride = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettingsMetaIncludeNullOverride),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

        public static readonly IEnumerable<object[]> DomJsonSerializerSettingsTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomJsonApi>(x),
                            x => new DomJsonDeserializeUnitTest<IDomJsonApi>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithJsonApiAndIncludeNullAndMetaIgnoreNullOverride",
                                TestJsonSerializerSettingsIncludeNullAndMetaIgnoreNullOverride,
                                new DomJsonApi(
                                    new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomJsonApi(
                                    new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0"))),
@"{
  ""version"": ""1.0""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomJsonApi>(x),
                            x => new DomJsonDeserializeUnitTest<IDomJsonApi>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithJsonApiAndIgnoreNullAndMetaIncludeNullOverride",
                                TestJsonSerializerSettingsIgnoreNullAndMetaIncludeNullOverride,
                                new DomJsonApi(
                                    new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""version"": ""1.0"",
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomLink>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLink>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithLinkAndIncludeNullAndMetaIgnoreNullOverride",
                                TestJsonSerializerSettingsIncludeNullAndMetaIgnoreNullOverride,
                                new DomLink(
                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomLink(
                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles"))),
@"""https://api.example.com/articles"""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomLink>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLink>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithLinkAndIgnoreNullAndMetaIncludeNullOverride",
                                TestJsonSerializerSettingsIgnoreNullAndMetaIncludeNullOverride,
                                new DomLink(
                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""href"": ""https://api.example.com/articles"",
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomDocument>(x),
                            x => new DomJsonDeserializeUnitTest<IDomDocument>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithDocumentAndIncludeNullAndMetaIgnoreNullOverride",
                                TestJsonSerializerSettingsIncludeNullAndMetaIgnoreNullOverride,
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")),
                                            new DomProperty(ApiPropertyType.Meta, "meta"))),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links")),
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")))),
                                    new DomProperty(ApiPropertyType.Links, "links")),
@"{
  ""jsonapi"": {
    ""version"": ""1.0""
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
                                "WithDocumentAndIgnoreNullAndMetaIncludeNullOverride",
                                TestJsonSerializerSettingsIgnoreNullAndMetaIncludeNullOverride,
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")),
                                            new DomProperty(ApiPropertyType.Meta, "meta"))),
                                    new DomProperty(ApiPropertyType.Meta, "meta"),
                                    new DomProperty(ApiPropertyType.Links, "links")),
                                new DomDocument(ApiDocumentType.Document,
                                    new DomProperty(ApiPropertyType.JsonApi, "jsonapi",
                                        new DomJsonApi(
                                            new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0")),
                                            new DomProperty(ApiPropertyType.Meta, "meta"))),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""jsonapi"": {
    ""version"": ""1.0"",
    ""meta"": null
  },
  ""meta"": null
}"))
                    },

        };
        #endregion
    }
}
