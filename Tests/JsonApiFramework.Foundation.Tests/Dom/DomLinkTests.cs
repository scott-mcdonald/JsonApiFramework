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
    public class DomLinkTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomLinkTests(ITestOutputHelper output)
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
                            x => new DomJsonSerializeUnitTest<IDomLink>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLink>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithHRefAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
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
                                "WithHRefAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
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
                            x => new DomJsonSerializeUnitTest<IDomLink>(x),
                            x => new DomJsonDeserializeUnitTest<IDomLink>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithHRefAndMeta",
                                TestJsonSerializerSettings,
                                new DomLink(
                                    new DomProperty(ApiPropertyType.HRef, "href", new DomValue<string>("https://api.example.com/articles")),
                                    new DomProperty(ApiPropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("is-public", new DomValue<bool>(true)),
                                        new DomProperty("version", new DomValue<string>("2.0"))))),
@"{
  ""href"": ""https://api.example.com/articles"",
  ""meta"":  {
    ""is-public"": true,
    ""version"": ""2.0""
  }
}"))
                    },

        };
        #endregion
    }
}
