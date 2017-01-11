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
    public class DomJsonApiVersionTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomJsonApiVersionTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomJsonApiVersionTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomJsonApiVersionTestData))]
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
                MetaNullValueHandling = null
            };

        private static readonly JsonSerializerSettings TestJsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                DateParseHandling = DateParseHandling.DateTimeOffset,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented
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

        public static readonly IEnumerable<object[]> DomJsonApiVersionTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomJsonApiVersion>(x),
                            x => new DomJsonDeserializeUnitTest<IDomJsonApiVersion>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithVersionIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomJsonApiVersion(
                                    new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.0"))),
@"{
  ""version"": ""1.0""
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomJsonApiVersion>(x),
                            x => new DomJsonDeserializeUnitTest<IDomJsonApiVersion>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithVersionIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomJsonApiVersion(
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
                            x => new DomJsonSerializeUnitTest<IDomJsonApiVersion>(x),
                            x => new DomJsonDeserializeUnitTest<IDomJsonApiVersion>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithVersionAndMeta",
                                TestJsonSerializerSettings,
                                new DomJsonApiVersion(
                                    new DomProperty(ApiPropertyType.Version, "version", new DomValue<string>("1.1")),
                                    new DomProperty(ApiPropertyType.Meta, "meta", new DomObject(
                                        new DomProperty("website", new DomValue<string>("http://jsonapi.org"))))),
@"{
  ""version"": ""1.1"",
  ""meta"":  {
    ""website"": ""http://jsonapi.org""
  }
}"))
                    },

        };
        #endregion
    }
}
