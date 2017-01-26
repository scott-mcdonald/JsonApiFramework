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
    public class DomJsonApiTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomJsonApiTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(DomJsonApiTestData))]
        public void TestJsonSerialize(DomJsonSerializationUnitTestFactory domJsonSerializationUnitTestFactory)
        {
            var data = domJsonSerializationUnitTestFactory.Data;
            var factory = domJsonSerializationUnitTestFactory.DomJsonSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(DomJsonApiTestData))]
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

        public static readonly IEnumerable<object[]> DomJsonApiTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomJsonApi>(x),
                            x => new DomJsonDeserializeUnitTest<IDomJsonApi>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithVersionAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomJsonApi(
                                    new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")),
                                    new DomProperty(PropertyType.Meta, "meta")),
                                new DomJsonApi(
                                    new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0"))),
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
                                "WithVersionAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomJsonApi(
                                    new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.0")),
                                    new DomProperty(PropertyType.Meta, "meta")),
@"{
  ""version"": ""1.0"",
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomJsonApi>(x),
                            x => new DomJsonDeserializeUnitTest<IDomJsonApi>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithVersionAndMeta",
                                TestJsonSerializerSettings,
                                new DomJsonApi(
                                    new DomProperty(PropertyType.Version, "version", new DomValue<string>("1.1")),
                                    new DomProperty(PropertyType.Meta, "meta", new DomObject(
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
