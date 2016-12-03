// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Tests.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class JsonApiVersionTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonApiVersionTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(JsonApiVersionTestData))]
        public void TestJsonSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(JsonApiVersionTestData))]
        public void TestJsonDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly JsonSerializerSettings TestSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

        private static readonly JsonSerializerSettings TestSettingsIncludeNull = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };

        private static readonly JsonSerializerSettings TestSettingsIgnoreNull = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

        // ReSharper disable MemberCanBePrivate.Global
        public static readonly IEnumerable<object[]> JsonApiVersionTestData = new[]
        // ReSharper restore MemberCanBePrivate.Global
            {
                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<JsonApiVersion>(x),
                            x => new JsonObjectDeserializeUnitTest<JsonApiVersion>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithNull",
                                TestSettings,
                                default(JsonApiVersion),
                                "null"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<JsonApiVersion>(x),
                            x => new JsonObjectDeserializeUnitTest<JsonApiVersion>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithEmptyObjectAndIncludeNull",
                                TestSettingsIncludeNull,
                                new JsonApiVersion(),
@"{
  ""version"": null,
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<JsonApiVersion>(x),
                            x => new JsonObjectDeserializeUnitTest<JsonApiVersion>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithEmptyObjectAndIgnoreNull",
                                TestSettingsIgnoreNull,
                                new JsonApiVersion(),
                                "{}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<JsonApiVersion>(x),
                            x => new JsonObjectDeserializeUnitTest<JsonApiVersion>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithVersion",
                                TestSettings,
                                new JsonApiVersion(JsonApiVersion.Version10String),
@"{
  ""version"": ""1.0"",
  ""meta"": null
}"))
                    },

                new object[]
                    {
                        new JsonObjectSerializationUnitTestFactory(
                            x => new JsonObjectSerializeUnitTest<JsonApiVersion>(x),
                            x => new JsonObjectDeserializeUnitTest<JsonApiVersion>(x),
                            new JsonObjectSerializationUnitTestData(
                                "WithVersionAndMeta",
                                TestSettings,
                                new JsonApiVersion(JsonApiVersion.Version10String, JsonApiSampleData.JsonApiVersionMeta),
@"{
  ""version"": ""1.0"",
  ""meta"": {
    ""website"": ""http://jsonapi.org""
    }
}"))
                    },
            };
        #endregion
    }
}
