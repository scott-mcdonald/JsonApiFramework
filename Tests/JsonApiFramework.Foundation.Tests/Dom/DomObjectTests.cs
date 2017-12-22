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
    public class DomObjectTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomObjectTests(ITestOutputHelper output)
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

        public struct Point
        {
            public long XCoordinate { get; set; }
            public long YCoordinate { get; set; }
        }

        public interface IShape
        {
            bool Enabled { get; set; }
            string Name { get; set; }
            string Description { get; set; }
            Point Center { get; set; }
        }

        public class Shape : IShape
        {
            public bool Enabled { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public Point Center { get; set; }
        }

        public class Circle : Shape
        {
            public long Radius { get; set; }
        }

        public static readonly IEnumerable<object[]> DomJsonSerializationTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithEmptyPointObjectAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomObject(),
@"{}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithEmptyPointObjectAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomObject(
                                    new DomProperty("x-coordinate"),
                                    new DomProperty("y-coordinate")),
@"{
  ""x-coordinate"": null,
  ""y-coordinate"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithPartialPointObjectAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomObject(
                                    new DomProperty("x-coordinate", new DomValue<long>(24))),
@"{
  ""x-coordinate"": 24
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithPartialPointObjectAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomObject(
                                    new DomProperty("x-coordinate", new DomValue<long>(24)),
                                    new DomProperty("y-coordinate")),
@"{
  ""x-coordinate"": 24,
  ""y-coordinate"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithCompletePointObject",
                                TestJsonSerializerSettings,
                                new DomObject(
                                    new DomProperty("x-coordinate", new DomValue<long>(24)),
                                    new DomProperty("y-coordinate", new DomValue<long>(42))),
@"{
  ""x-coordinate"": 24,
  ""y-coordinate"": 42
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithEmptyCircleObjectAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomObject(),
@"{}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithEmptyCircleObjectAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomObject(
                                    new DomProperty("enabled"),
                                    new DomProperty("name"),
                                    new DomProperty("description"),
                                    new DomProperty("center"),
                                    new DomProperty("radius")),
@"{
  ""enabled"": null,
  ""name"": null,
  ""description"": null,
  ""center"": null,
  ""radius"": null
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithPartialCircleObjectAndIgnoreNull",
                                TestJsonSerializerSettingsIgnoreNull,
                                new DomObject(
                                    new DomProperty("enabled", new DomValue<bool>(true)),
                                    new DomProperty("name", new DomValue<string>("My Circle")),
                                    new DomProperty("center", new DomObject(
                                        new DomProperty("x-coordinate", new DomValue<long>(24)))),
                                    new DomProperty("radius", new DomValue<long>(123))),
@"{
  ""enabled"": true,
  ""name"": ""My Circle"",
  ""center"": {
    ""x-coordinate"": 24
  },
  ""radius"": 123
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithPartialCircleObjectAndIncludeNull",
                                TestJsonSerializerSettingsIncludeNull,
                                new DomObject(
                                    new DomProperty("enabled", new DomValue<bool>(true)),
                                    new DomProperty("name", new DomValue<string>("My Circle")),
                                    new DomProperty("description"),
                                    new DomProperty("center", new DomObject(
                                        new DomProperty("x-coordinate", new DomValue<long>(24)),
                                        new DomProperty("y-coordinate"))),
                                    new DomProperty("radius", new DomValue<long>(123))),
@"{
  ""enabled"": true,
  ""name"": ""My Circle"",
  ""description"": null,
  ""center"": {
    ""x-coordinate"": 24,
    ""y-coordinate"": null
  },
  ""radius"": 123
}"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomObject>(x),
                            x => new DomJsonDeserializeUnitTest<IDomObject>(x),
                            new DomJsonSerializationUnitTestData(
                                "WithCompleteCircleObject",
                                TestJsonSerializerSettings,
                                new DomObject(
                                    new DomProperty("enabled", new DomValue<bool>(true)),
                                    new DomProperty("name", new DomValue<string>("My Circle")),
                                    new DomProperty("description", new DomValue<string>("This is my circle of trust...")),
                                    new DomProperty("center", new DomObject(
                                        new DomProperty("x-coordinate", new DomValue<long>(24)),
                                        new DomProperty("y-coordinate", new DomValue<long>(42)))),
                                    new DomProperty("radius", new DomValue<long>(123))),
@"{
  ""enabled"": true,
  ""name"": ""My Circle"",
  ""description"": ""This is my circle of trust..."",
  ""center"": {
    ""x-coordinate"": 24,
    ""y-coordinate"": 42
  },
  ""radius"": 123
}"))
                    },

        };
        #endregion
    }
}
