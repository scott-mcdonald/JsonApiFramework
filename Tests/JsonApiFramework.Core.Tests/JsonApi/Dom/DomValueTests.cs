// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using FluentAssertions;

using JsonApiFramework.JsonApi.Dom;
using JsonApiFramework.JsonApi.Dom.Internal;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi.Dom
{
    public class DomValueTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomValueTests(ITestOutputHelper output)
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
                FloatParseHandling = FloatParseHandling.Decimal
            };

        public const string TestDateTimeString = "1968-05-20T20:02:42.123Z";
        public static readonly DateTime TestDateTime = new DateTime(1968, 5, 20, 20, 2, 42, 123, DateTimeKind.Utc);

        public const string TestDateTimeOffsetString = "1968-05-20T20:02:42.123-05:00";
        public static readonly DateTimeOffset TestDateTimeOffset = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 123, new TimeSpan(-5, 0, 0));

        public const string TestTimeSpanString = "42.00:00:00";
        public static readonly TimeSpan TestTimeSpan = new TimeSpan(42, 0, 0, 0, 0);

        public const string TestGuidString = "5167e9e1-a15f-41e1-af46-442ffcd37f1b";
        public static readonly Guid TestGuid = new Guid(TestGuidString);

        public const string TestUriString = "https://api.example.com:8002/api/en-us/articles/42";
        public static readonly Uri TestUri = new Uri(TestUriString);

        public static readonly byte[] TestByteArray = { 42, 24, 48, 84, 12, 21, 68, 86 };
        public const string TestByteArrayString = "KhgwVAwVRFY=";

        public static readonly Type TestType = typeof(DateTimeOffset);
        public static readonly string TestTypeString = TypeReflection.GetCompactQualifiedName(TestType);

        public const int TestRedOrdinalValue0 = 0;
        public const int TestGreenOrdinalValue24 = 24;
        public const int TestBlueOrdinalValue42 = 42;

        // ReSharper disable UnusedMember.Global
        public enum RedGreenOrBlue
        {
            Red = TestRedOrdinalValue0,
            Green = TestGreenOrdinalValue24,
            Blue = TestBlueOrdinalValue42
        };
        // ReSharper restore UnusedMember.Global

        public const int TestEnumOrdinal = TestBlueOrdinalValue42;
        public const string TestEnumString = "Blue";
        public const RedGreenOrBlue TestEnum = RedGreenOrBlue.Blue;

        public const string TestString = "The quick brown fox jumps over the lazy dog";

        public static readonly IEnumerable<object[]> DomJsonSerializationTestData = new[]
            {
                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<bool>().Should().BeFalse()),
                            new DomJsonSerializationUnitTestData(
                                "WithBoolFalse",
                                TestJsonSerializerSettings,
                                new DomValue<bool>(false),
                                new DomValue<bool>(false),
                                "false"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<bool>().Should().BeTrue()),
                            new DomJsonSerializationUnitTestData(
                                "WithBoolTrue",
                                TestJsonSerializerSettings,
                                new DomValue<bool>(true),
                                new DomValue<bool>(true),
                                "true"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<byte>().Should().Be(42)),
                            new DomJsonSerializationUnitTestData(
                                "WithByte",
                                TestJsonSerializerSettings,
                                new DomValue<byte>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<byte[]>().ShouldAllBeEquivalentTo(TestByteArray)),
                            new DomJsonSerializationUnitTestData(
                                "WithByteArray",
                                TestJsonSerializerSettings,
                                new DomValue<byte[]>(TestByteArray),
                                new DomValue<string>(TestByteArrayString),
                                "\"" + TestByteArrayString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<char>().Should().Be('*')),
                            new DomJsonSerializationUnitTestData(
                                "WithChar",
                                TestJsonSerializerSettings,
                                new DomValue<char>('*'),
                                new DomValue<string>("*"),
                                "\"*\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<DateTime>().Should().Be(TestDateTime)),
                            new DomJsonSerializationUnitTestData(
                                "WithDateTime",
                                new JsonSerializerSettings
                                    {
                                        ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                                        DateParseHandling = DateParseHandling.DateTime
                                    },
                                new DomValue<DateTime>(TestDateTime),
                                new DomValue<DateTime>(TestDateTime),
                                "\"" + TestDateTimeString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<DateTimeOffset>().Should().Be(TestDateTimeOffset)),
                            new DomJsonSerializationUnitTestData(
                                "WithDateTimeOffset",
                                TestJsonSerializerSettings,
                                new DomValue<DateTimeOffset>(TestDateTimeOffset),
                                new DomValue<DateTimeOffset>(TestDateTimeOffset),
                                "\"" + TestDateTimeOffsetString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<decimal>().Should().Be((decimal)42.1)),
                            new DomJsonSerializationUnitTestData(
                                "WithDecimal",
                                TestJsonSerializerSettings,
                                new DomValue<decimal>((decimal)42.1),
                                new DomValue<decimal>((decimal)42.1),
                                "42.1"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<double>().Should().Be(42.2)),
                            new DomJsonSerializationUnitTestData(
                                "WithDouble",
                                TestJsonSerializerSettings,
                                new DomValue<double>(42.2),
                                new DomValue<decimal>((decimal)42.2),
                                "42.2"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<RedGreenOrBlue>().Should().Be(TestEnum)),
                            new DomJsonSerializationUnitTestData(
                                "WithEnumOrdinal",
                                TestJsonSerializerSettings,
                                new DomValue<RedGreenOrBlue>(TestEnum),
                                new DomValue<long>(TestEnumOrdinal),
                                TestEnumOrdinal.ToString()))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<RedGreenOrBlue>().Should().Be(TestEnum)),
                            new DomJsonSerializationUnitTestData(
                                "WithEnumString",
                                new JsonSerializerSettings
                                    {
                                        ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                                        Converters = new List<JsonConverter>
                                                {
                                                    new StringEnumConverter()
                                                }
                                    },
                                new DomValue<RedGreenOrBlue>(TestEnum),
                                new DomValue<string>(TestEnumString),
                                "\"" + TestEnumString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<float>().Should().Be((float)42.3)),
                            new DomJsonSerializationUnitTestData(
                                "WithFloat",
                                TestJsonSerializerSettings,
                                new DomValue<float>((float)42.3),
                                new DomValue<decimal>((decimal)42.3),
                                "42.3"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<Guid>().Should().Be(TestGuid)),
                            new DomJsonSerializationUnitTestData(
                                "WithGuid",
                                TestJsonSerializerSettings,
                                new DomValue<Guid>(TestGuid),
                                new DomValue<string>(TestGuidString),
                                "\"" + TestGuidString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<int>().Should().Be(42)),
                            new DomJsonSerializationUnitTestData(
                                "WithInt",
                                TestJsonSerializerSettings,
                                new DomValue<int>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<long>().Should().Be(42)),
                            new DomJsonSerializationUnitTestData(
                                "WithLong",
                                TestJsonSerializerSettings,
                                new DomValue<long>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrUnderlyingValueType.Should().BeNull()),
                            new DomJsonSerializationUnitTestData(
                                "WithNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<bool?>().Should().Be(true)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableBoolAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<bool?>(true),
                                new DomValue<bool>(true),
                                "true"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<bool?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableBoolAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<byte?>().Should().Be((byte)42)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableByteAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<byte?>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<byte?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableByteAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<char?>().Should().Be((char)'*')),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableCharAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<char?>('*'),
                                new DomValue<string>("*"),
                                "\"*\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<char?>().Should().BeNull()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableCharAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<DateTime?>().Should().Be(TestDateTime)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableDateTimeAndValue",
                                new JsonSerializerSettings
                                    {
                                        ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                                        DateParseHandling = DateParseHandling.DateTime
                                    },
                                new DomValue<DateTime?>(TestDateTime),
                                new DomValue<DateTime>(TestDateTime),
                                "\"" + TestDateTimeString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<DateTime?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableDateTimeAndNull",
                                new JsonSerializerSettings
                                    {
                                        ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                                        DateParseHandling = DateParseHandling.DateTime
                                    },
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<DateTimeOffset?>().Should().Be(TestDateTimeOffset)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableDateTimeOffsetAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<DateTimeOffset?>(TestDateTimeOffset),
                                new DomValue<DateTimeOffset>(TestDateTimeOffset),
                                "\"" + TestDateTimeOffsetString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<DateTimeOffset?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableDateTimeOffsetAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<decimal?>().Should().Be((decimal)42.1)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableDecimalAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<decimal?>((decimal)42.1),
                                new DomValue<decimal>((decimal)42.1),
                                "42.1"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<decimal?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableDecimalAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<double?>().Should().Be((double)42.2)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableDoubleAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<double?>(42.2),
                                new DomValue<decimal>((decimal)42.2),
                                "42.2"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<double?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableDoubleAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<RedGreenOrBlue?>().Should().Be(TestEnum)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableEnumOrdinalAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<RedGreenOrBlue?>(TestEnum),
                                new DomValue<long>(TestEnumOrdinal),
                                TestEnumOrdinal.ToString()))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<RedGreenOrBlue?>().Should().Be(TestEnum)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableEnumStringAndValue",
                                new JsonSerializerSettings
                                    {
                                        ContractResolver = new DomContractResolver(TestDomJsonSerializerSettings),
                                        Converters = new List<JsonConverter>
                                            {
                                                new StringEnumConverter()
                                            }
                                    },
                                new DomValue<RedGreenOrBlue?>(TestEnum),
                                new DomValue<string>(TestEnumString),
                                "\"" + TestEnumString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<RedGreenOrBlue?>().Should().BeNull()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableEnumAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<float?>().Should().Be((float)42.3)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableFloatAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<float?>((float)42.3),
                                new DomValue<decimal>((decimal)42.3),
                                "42.3"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<float?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableFloatAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<Guid?>().Should().Be(TestGuid)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableGuidAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<Guid?>(TestGuid),
                                new DomValue<string>(TestGuidString),
                                "\"" + TestGuidString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<Guid?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableGuidAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<int?>().Should().Be((int)42)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableIntAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<int?>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<int?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableIntAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<long?>().Should().Be((long)42)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableLongAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<long?>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<long?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableLongAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<sbyte?>().Should().Be((sbyte)42)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableSByteAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<sbyte?>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<sbyte?>().Should().BeNull()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableSByteAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<short?>().Should().Be((short)42)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableShortAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<short?>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<short?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableShortAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<TimeSpan?>().Should().Be(TestTimeSpan)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableTimeSpanAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<TimeSpan?>(TestTimeSpan),
                                new DomValue<string>(TestTimeSpanString),
                                "\"" + TestTimeSpanString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<TimeSpan?>().Should().NotHaveValue()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableTimeSpanAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<uint?>().Should().Be((uint)42)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableUIntAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<uint?>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<uint?>().Should().BeNull()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableUIntAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<ulong?>().Should().Be((ulong)42)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableULongAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<ulong?>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<ulong?>().Should().BeNull()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableULongAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<ushort?>().Should().Be((ushort)42)),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableUShortAndValue",
                                TestJsonSerializerSettings,
                                new DomValue<ushort?>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<ushort?>().Should().BeNull()),
                            new DomJsonSerializationUnitTestData(
                                "WithNullableUShortAndNull",
                                TestJsonSerializerSettings,
                                null,
                                null,
                                "null"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<sbyte>().Should().Be(42)),
                            new DomJsonSerializationUnitTestData(
                                "WithSByte",
                                TestJsonSerializerSettings,
                                new DomValue<sbyte>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<short>().Should().Be(42)),
                            new DomJsonSerializationUnitTestData(
                                "WithShort",
                                TestJsonSerializerSettings,
                                new DomValue<short>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<string>().Should().Be(TestString)),
                            new DomJsonSerializationUnitTestData(
                                "WithString",
                                TestJsonSerializerSettings,
                                new DomValue<string>(TestString),
                                new DomValue<string>(TestString),
                                "\"" + TestString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<TimeSpan>().Should().Be(TestTimeSpan)),
                            new DomJsonSerializationUnitTestData(
                                "WithTimeSpan",
                                TestJsonSerializerSettings,
                                new DomValue<TimeSpan>(TestTimeSpan),
                                new DomValue<string>(TestTimeSpanString),
                                "\"" + TestTimeSpanString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<uint>().Should().Be(42)),
                            new DomJsonSerializationUnitTestData(
                                "WithUInt",
                                TestJsonSerializerSettings,
                                new DomValue<uint>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<ulong>().Should().Be(42)),
                            new DomJsonSerializationUnitTestData(
                                "WithULong",
                                TestJsonSerializerSettings,
                                new DomValue<ulong>(42),
                                new DomValue<long>(42),
                                "42"))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<Uri>().Should().Be(TestUri)),
                            new DomJsonSerializationUnitTestData(
                                "WithUri",
                                TestJsonSerializerSettings,
                                new DomValue<Uri>(TestUri),
                                new DomValue<string>(TestUriString),
                                "\"" + TestUriString + "\""))
                    },

                new object[]
                    {
                        new DomJsonSerializationUnitTestFactory(
                            x => new DomJsonSerializeUnitTest<IDomValue>(x),
                            x => new DomJsonDeserializeUnitTest<IDomValue>(x, y => y.ClrValue<ushort>().Should().Be(42)),
                            new DomJsonSerializationUnitTestData(
                                "WithUShort",
                                TestJsonSerializerSettings,
                                new DomValue<ushort>(42),
                                new DomValue<long>(42),
                                "42"))
                    },
            };
        #endregion
    }
}
