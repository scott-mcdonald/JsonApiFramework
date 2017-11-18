// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi.Dom;
using JsonApiFramework.JsonApi.Dom.Internal;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi.Dom
{
    public class DomArrayTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomArrayTests(ITestOutputHelper output)
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
                DateParseHandling = DateParseHandling.DateTimeOffset,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented
            };

        public static readonly IEnumerable<object[]> DomJsonSerializationTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithEmptyArray",
                                TestJsonSerializerSettings,
                                new DomArray(),
                                @"[]"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "With1StringValueItem",
                                TestJsonSerializerSettings,
                                new DomArray(
                                    new DomItem(0, new DomValue<string>("zero"))),
@"[
  ""zero""
]"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "With2StringValueItems",
                                TestJsonSerializerSettings,
                                new DomArray(
                                    new DomItem(0, new DomValue<string>("zero")),
                                    new DomItem(1, new DomValue<string>("one"))),
@"[
  ""zero"",
  ""one""
]"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "With3StringValueItems",
                                TestJsonSerializerSettings,
                                new DomArray(
                                    new DomItem(0, new DomValue<string>("zero")),
                                    new DomItem(1, new DomValue<string>("one")),
                                    new DomItem(2, new DomValue<string>("two"))),
@"[
  ""zero"",
  ""one"",
  ""two""
]"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "With1PointObjectItem",
                                TestJsonSerializerSettings,
                                new DomArray(
                                    new DomItem(0,
                                        new DomObject(
                                            new DomProperty("x-coordinate", new DomValue<long>(24)),
                                            new DomProperty("y-coordinate", new DomValue<long>(42))))),
@"[
  {
    ""x-coordinate"": 24,
    ""y-coordinate"": 42
  }
]"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "With2PointObjectItems",
                                TestJsonSerializerSettings,
                                new DomArray(
                                    new DomItem(0,
                                        new DomObject(
                                            new DomProperty("x-coordinate", new DomValue<long>(24)),
                                            new DomProperty("y-coordinate", new DomValue<long>(42)))),
                                    new DomItem(1,
                                        new DomObject(
                                            new DomProperty("x-coordinate", new DomValue<long>(68)),
                                            new DomProperty("y-coordinate", new DomValue<long>(86))))),
@"[
  {
    ""x-coordinate"": 24,
    ""y-coordinate"": 42
  },
  {
    ""x-coordinate"": 68,
    ""y-coordinate"": 86
  }
]"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "With3PointObjectItems",
                                TestJsonSerializerSettings,
                                new DomArray(
                                    new DomItem(0,
                                        new DomObject(
                                            new DomProperty("x-coordinate", new DomValue<long>(24)),
                                            new DomProperty("y-coordinate", new DomValue<long>(42)))),
                                    new DomItem(1,
                                        new DomObject(
                                            new DomProperty("x-coordinate", new DomValue<long>(68)),
                                            new DomProperty("y-coordinate", new DomValue<long>(86)))),
                                    new DomItem(2,
                                        new DomObject(
                                            new DomProperty("x-coordinate", new DomValue<long>(46)),
                                            new DomProperty("y-coordinate", new DomValue<long>(64))))),
@"[
  {
    ""x-coordinate"": 24,
    ""y-coordinate"": 42
  },
  {
    ""x-coordinate"": 68,
    ""y-coordinate"": 86
  },
  {
    ""x-coordinate"": 46,
    ""y-coordinate"": 64
  }
]"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "With1NullItem",
                                TestJsonSerializerSettings,
                                new DomArray(
                                    new DomItem(0)),
@"[
  null
]"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "With2NullItems",
                                TestJsonSerializerSettings,
                                new DomArray(
                                    new DomItem(0),
                                    new DomItem(1)),
@"[
  null,
  null
]"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomArray>(x),
                            x => new DomJsonDeserializeUnitTest<IDomArray>(x),
                            new DomJsonSerializationUnitTestData(
                                "With3NullItems",
                                TestJsonSerializerSettings,
                                new DomArray(
                                    new DomItem(0),
                                    new DomItem(1),
                                    new DomItem(2)),
@"[
  null,
  null,
  null
]"))
                    },

        };
        #endregion
    }
}
