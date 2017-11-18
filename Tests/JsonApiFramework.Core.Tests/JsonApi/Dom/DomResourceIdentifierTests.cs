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
    public class DomResourceIdentifierTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomResourceIdentifierTests(ITestOutputHelper output)
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
                            x => new DomJsonSerializeUnitTest<IDomResourceIdentifier>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResourceIdentifier>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithTypeAndIdAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomResourceIdentifier(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
                                new DomResourceIdentifier(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42"))),
@"{
  ""type"": ""articles"",
  ""id"": ""42""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResourceIdentifier>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResourceIdentifier>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithTypeAndIdAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomResourceIdentifier(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Meta, "meta")),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomResourceIdentifier>(x),
                            x => new DomJsonDeserializeUnitTest<IDomResourceIdentifier>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithTypeAndIdAndAndMeta",
                                TestJsonSerializerSettings,
                                new DomResourceIdentifier(
                                    new DomProperty(ApiPropertyType.Type, "type", new DomValue<string>("articles")),
                                    new DomProperty(ApiPropertyType.Id, "id", new DomValue<string>("42")),
                                    new DomProperty(ApiPropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("version", new DomValue<string>("2.0"))))),
@"{
  ""type"": ""articles"",
  ""id"": ""42"",
  ""meta"":  {
    ""version"": ""2.0""
  }
}"))
                    },

        };
        #endregion
    }
}
