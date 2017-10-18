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
    public class ErrorSourceTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ErrorSourceTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(ErrorSourceTestData))]
        public void TestErrorSourceSerialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
        {
            var data = jsonObjectSerializationUnitTestFactory.Data;
            var factory = jsonObjectSerializationUnitTestFactory.JsonObjectSerializeUnitTestFactory;
            var unitTest = factory(data);
            unitTest.Execute(this);
        }

        [Theory]
        [MemberData(nameof(ErrorSourceTestData))]
        public void TestErrorSourceDeserialize(JsonObjectSerializationUnitTestFactory jsonObjectSerializationUnitTestFactory)
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

        public static readonly IEnumerable<object[]> ErrorSourceTestData = new[]
        {
            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ErrorSource>(x),
                    x => new JsonObjectDeserializeUnitTest<ErrorSource>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        default(ErrorSource),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ErrorSource>(x),
                    x => new JsonObjectDeserializeUnitTest<ErrorSource>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullObjectAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        default(ErrorSource),
                        "null"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ErrorSource>(x),
                    x => new JsonObjectDeserializeUnitTest<ErrorSource>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullParameterAndNullPointerAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new ErrorSource(null, null),
                        "{}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ErrorSource>(x),
                    x => new JsonObjectDeserializeUnitTest<ErrorSource>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullParameterAndNullPointerAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new ErrorSource(null, null),
                        "{\"parameter\":null,\"pointer\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ErrorSource>(x),
                    x => new JsonObjectDeserializeUnitTest<ErrorSource>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithParameterAndNullPointerAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new ErrorSource("filter", null),
                        "{\"parameter\":\"filter\"}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ErrorSource>(x),
                    x => new JsonObjectDeserializeUnitTest<ErrorSource>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithParameterAndNullPointerAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new ErrorSource("filter", null),
                        "{\"parameter\":\"filter\",\"pointer\":null}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ErrorSource>(x),
                    x => new JsonObjectDeserializeUnitTest<ErrorSource>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullParameterAndPointerAndIgnoreNull",
                        TestJsonSerializerSettingsIgnoreNull,
                        new ErrorSource(null, "/data/attributes/name"),
                        "{\"pointer\":\"/data/attributes/name\"}"))
            },

            new object[]
            {
                new JsonObjectSerializationUnitTestFactory(
                    x => new JsonObjectSerializeUnitTest<ErrorSource>(x),
                    x => new JsonObjectDeserializeUnitTest<ErrorSource>(x),
                    new JsonObjectSerializationUnitTestData(
                        "WithNullParameterAndPointerAndIncludeNull",
                        TestJsonSerializerSettingsIncludeNull,
                        new ErrorSource(null, "/data/attributes/name"),
                        "{\"parameter\":null,\"pointer\":\"/data/attributes/name\"}"))
            },
        };
        #endregion
    }
}
