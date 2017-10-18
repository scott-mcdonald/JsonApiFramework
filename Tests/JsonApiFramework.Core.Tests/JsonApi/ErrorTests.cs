// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Net;

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
    public class ErrorTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ErrorTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(ErrorTestData))]
        public void TestErrorSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(ErrorTestData))]
        public void TestErrorDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
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

        public static readonly Links LinksTestData = new Links(new Dictionary<string, Link>
        {
            {Keywords.About, new Link("https://api.example.com/about/first-name-minimum-requirement")}
        });

        private static readonly ErrorMeta ErrorMetaTestData = new ErrorMeta
        {
            StackTrace = "Foo.Method1 line 42\nFoo.Method2 line 24\nBar.Method1 line 86\nBar.Method2 line 68"
        };

        public static readonly IEnumerable<object[]> ErrorTestData = new[]
        {
            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Error>(x),
                    x => new JsonObjectDeserializeUnitTest<Error>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        default(Error),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Error>(x),
                    x => new JsonObjectDeserializeUnitTest<Error>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        default(Error),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Error>(x),
                    x => new JsonObjectDeserializeUnitTest<Error>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithBareErrorAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new Error("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2", null, null, null, null, null, null, null),
                        "{\"id\":\"a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2\"}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Error>(x),
                    x => new JsonObjectDeserializeUnitTest<Error>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithBareErrorAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new Error("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2", null, null, null, null, null, null, null),
                        "{\"id\":\"a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2\",\"links\":null,\"status\":null,\"code\":null,\"title\":null,\"detail\":null,\"source\":null,\"meta\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Error>(x),
                    x => new JsonObjectDeserializeUnitTest<Error>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithBasicErrorAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new Error("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2", null, HttpStatusCode.NotFound, null, "Invalid Attribute", "First name must contain at least three characters.", new ErrorSource(null, "/data/attributes/first-name"), null),
                        "{\"id\":\"a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2\",\"status\":\"404\",\"title\":\"Invalid Attribute\",\"detail\":\"First name must contain at least three characters.\",\"source\":{\"pointer\":\"/data/attributes/first-name\"}}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Error>(x),
                    x => new JsonObjectDeserializeUnitTest<Error>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithBasicErrorAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new Error("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2", null, HttpStatusCode.NotFound, null, "Invalid Attribute", "First name must contain at least three characters.", new ErrorSource(null, "/data/attributes/first-name"), null),
                        "{\"id\":\"a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2\",\"links\":null,\"status\":\"404\",\"code\":null,\"title\":\"Invalid Attribute\",\"detail\":\"First name must contain at least three characters.\",\"source\":{\"parameter\":null,\"pointer\":\"/data/attributes/first-name\"},\"meta\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Error>(x),
                    x => new JsonObjectDeserializeUnitTest<Error>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithCompleteErrorAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new Error("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2", LinksTestData, HttpStatusCode.NotFound, "24", "Invalid Attribute", "First name must contain at least three characters.", new ErrorSource(null, "/data/attributes/first-name"), new WriteMeta<ErrorMeta>(ErrorMetaTestData)),
                        new Error("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2", LinksTestData, HttpStatusCode.NotFound, "24", "Invalid Attribute", "First name must contain at least three characters.", new ErrorSource(null, "/data/attributes/first-name"), new ReadMeta(JObject.FromObject(ErrorMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettingsIgnoreNull)))),
                        "{\"id\":\"a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2\",\"links\":{\"about\":\"https://api.example.com/about/first-name-minimum-requirement\"},\"status\":\"404\",\"code\":\"24\",\"title\":\"Invalid Attribute\",\"detail\":\"First name must contain at least three characters.\",\"source\":{\"pointer\":\"/data/attributes/first-name\"},\"meta\":{\"stack-trace\":\"Foo.Method1 line 42\\nFoo.Method2 line 24\\nBar.Method1 line 86\\nBar.Method2 line 68\"}}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<Error>(x),
                    x => new JsonObjectDeserializeUnitTest<Error>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithCompleteErrorAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new Error("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2", LinksTestData, HttpStatusCode.NotFound, "24", "Invalid Attribute", "First name must contain at least three characters.", new ErrorSource(null, "/data/attributes/first-name"), new WriteMeta<ErrorMeta>(ErrorMetaTestData)),
                        new Error("a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2", LinksTestData, HttpStatusCode.NotFound, "24", "Invalid Attribute", "First name must contain at least three characters.", new ErrorSource(null, "/data/attributes/first-name"), new ReadMeta(JObject.FromObject(ErrorMetaTestData, JsonSerializer.CreateDefault(TestJsonSerializerSettingsIncludeNull)))),
                        "{\"id\":\"a9ee6d4c-4a24-40e1-ba46-ce0189ca73f2\",\"links\":{\"about\":{\"href\":\"https://api.example.com/about/first-name-minimum-requirement\",\"meta\":null}},\"status\":\"404\",\"code\":\"24\",\"title\":\"Invalid Attribute\",\"detail\":\"First name must contain at least three characters.\",\"source\":{\"parameter\":null,\"pointer\":\"/data/attributes/first-name\"},\"meta\":{\"stack-trace\":\"Foo.Method1 line 42\\nFoo.Method2 line 24\\nBar.Method1 line 86\\nBar.Method2 line 68\"}}"))
            },

        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Meta Types
        [JsonObject(MemberSerialization.OptIn)]
        public class ErrorMeta : JsonObject
        {
            [JsonProperty("stack-trace")] public string StackTrace { get; set; }
        }
        #endregion
    }
}
