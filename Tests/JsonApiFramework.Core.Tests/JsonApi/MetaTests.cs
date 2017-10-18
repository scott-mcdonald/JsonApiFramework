// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Json;
using JsonApiFramework.JsonApi;
using JsonApiFramework.JsonApi.Internal;
using JsonApiFramework.Tests.Json;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class MetaTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public MetaTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(MetaTestData))]
        public void TestMetaSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(MetaTestData))]
        public void TestMetaDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectDeserializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Test Data
        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIgnoreNull = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings TestJsonSerializerSettingsIncludeNull = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Include
        };

        private static readonly DocumentMeta DocumentMetaTestData = new DocumentMeta
        {
            IsPublic = true,
            Version = 2.1m,
            Copyright = "Copyright 2015 Example Corporation.",
            Authors = new[] {"John Doe", "Jane Doe"}
        };

        public static readonly IEnumerable<object[]> MetaTestData = new[]
        {
            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Meta>(x),
                    x => new JsonObjectDeserializeUnitTest<Meta>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        default(Meta),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Meta>(x),
                    x => new JsonObjectDeserializeUnitTest<Meta>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        default(Meta),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Meta>(x),
                    x => new JsonObjectDeserializeUnitTest<Meta>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithDocumentMetaAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new WriteMeta<DocumentMeta>(DocumentMetaTestData),
                        new ReadMeta(JObject.FromObject(DocumentMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettingsIgnoreNull))),
                        "{\"is-public\":true,\"version\":2.1,\"copyright\":\"Copyright 2015 Example Corporation.\",\"authors\":[\"John Doe\",\"Jane Doe\"]}\r\n"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Meta>(x),
                    x => new JsonObjectDeserializeUnitTest<Meta>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithDocumentMetaAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new WriteMeta<DocumentMeta>(DocumentMetaTestData),
                        new ReadMeta(JObject.FromObject(DocumentMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettingsIncludeNull))),
                        "{\"is-public\":true,\"version\":2.1,\"copyright\":\"Copyright 2015 Example Corporation.\",\"authors\":[\"John Doe\",\"Jane Doe\"],\"extra\":null}"))
            },
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Meta Types
        [JsonObject(MemberSerialization.OptIn)]
        public class DocumentMeta : JsonObject
        {
            [JsonProperty("is-public")] public bool IsPublic { get; set; }
            [JsonProperty("version")]   public decimal Version { get; set; }
            [JsonProperty("copyright")] public string Copyright { get; set; }
            [JsonProperty("authors")]   public string[] Authors { get; set; }
            [JsonProperty("extra")]     public string Extra { get; set; }
        }
        #endregion
    }
}
