// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class DateTimeOffsetConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DateTimeOffsetConverterTests(ITestOutputHelper output)
            : base(output, true)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(ConvertGenericTestData))]
        public void TestConveterConvertGeneric(IUnitTest unitTest)
        {
            unitTest.Execute(this);
            this.WriteBuffer();
        }

        [Theory]
        [MemberData(nameof(TryConvertGenericTestData))]   
        public void TestConveterTryConvertGeneric(IUnitTest unitTest)
        {
            unitTest.Execute(this);
            this.WriteBuffer();
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region ConvertGenericTestData
        public static readonly IEnumerable<object[]> ConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("DateTimeOffsetToXXX",
                            // Simple Types
                            new ConvertGenericTest<DateTimeOffset, bool>("DateTimeOffsetToBool", TestDateTimeOffset, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTimeOffset, byte>("DateTimeOffsetToByte", TestDateTimeOffset, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTimeOffset, byte[]>("DateTimeOffsetToByteArray", TestDateTimeOffset, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTimeOffset, char>("DateTimeOffsetToChar", TestDateTimeOffset, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTimeOffset, DateTime>("DateTimeOffsetToDateTime", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTimeOffset, DateTimeOffset>("DateTimeOffsetToDateTimeOffset", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTimeOffset, decimal>("DateTimeOffsetToDecimal", TestDateTimeOffset, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTimeOffset, double>("DateTimeOffsetToDouble", TestDateTimeOffset, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTimeOffset, PrimaryColor>("DateTimeOffsetToEnum", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTimeOffset, float>("DateTimeOffsetToFloat", TestDateTimeOffset, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTimeOffset, Guid>("DateTimeOffsetToGuid", TestDateTimeOffset, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTimeOffset, int>("DateTimeOffsetToInt", TestDateTimeOffset, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTimeOffset, long>("DateTimeOffsetToLong", TestDateTimeOffset, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTimeOffset, sbyte>("DateTimeOffsetToSByte", TestDateTimeOffset, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTimeOffset, short>("DateTimeOffsetToShort", TestDateTimeOffset, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToString", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetString),
                            new ConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToStringWithFormat", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormat, FormatDateTimeOffsetContext),
                            new ConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToStringWithFormatAndFormatProvider", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeOffsetContext),
                            new ConvertGenericTest<DateTimeOffset, TimeSpan>("DateTimeOffsetToTimeSpan", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTimeOffset, Type>("DateTimeOffsetToType", TestDateTimeOffset, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTimeOffset, uint>("DateTimeOffsetToUInt", TestDateTimeOffset, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTimeOffset, ulong>("DateTimeOffsetToULong", TestDateTimeOffset, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTimeOffset, Uri>("DateTimeOffsetToUri", TestDateTimeOffset, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTimeOffset, ushort>("DateTimeOffsetToUShort", TestDateTimeOffset, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DateTimeOffset, bool?>("DateTimeOffsetToNullable<Bool>", TestDateTimeOffset, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTimeOffset, byte?>("DateTimeOffsetToNullable<Byte>", TestDateTimeOffset, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTimeOffset, char?>("DateTimeOffsetToNullable<Char>", TestDateTimeOffset, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTimeOffset, DateTime?>("DateTimeOffsetToNullable<DateTime>", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTimeOffset, DateTimeOffset?>("DateTimeOffsetToNullable<DateTimeOffset>", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTimeOffset, decimal?>("DateTimeOffsetToNullable<Decimal>", TestDateTimeOffset, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTimeOffset, double?>("DateTimeOffsetToNullable<Double>", TestDateTimeOffset, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTimeOffset, PrimaryColor?>("DateTimeOffsetToNullable<Enum>", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTimeOffset, float?>("DateTimeOffsetToNullable<Float>", TestDateTimeOffset, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTimeOffset, Guid?>("DateTimeOffsetToNullable<Guid>", TestDateTimeOffset, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<DateTimeOffset, int?>("DateTimeOffsetToNullable<Int>", TestDateTimeOffset, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTimeOffset, long?>("DateTimeOffsetToNullable<Long>", TestDateTimeOffset, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTimeOffset, sbyte?>("DateTimeOffsetToNullable<SByte>", TestDateTimeOffset, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTimeOffset, short?>("DateTimeOffsetToNullable<Short>", TestDateTimeOffset, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTimeOffset, TimeSpan?>("DateTimeOffsetToNullable<TimeSpan>", TestDateTimeOffset, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<DateTimeOffset, uint?>("DateTimeOffsetToNullable<UInt>", TestDateTimeOffset, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTimeOffset, ulong?>("DateTimeOffsetToNullable<ULong>", TestDateTimeOffset, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTimeOffset, ushort?>("DateTimeOffsetToNullable<UShort>", TestDateTimeOffset, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DateTimeOffset, IInterface>("DateTimeOffsetToInterface", TestDateTimeOffset, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTimeOffset, BaseClass>("DateTimeOffsetToBaseClass", TestDateTimeOffset, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTimeOffset, DerivedClass>("DateTimeOffsetToDerivedClass", TestDateTimeOffset, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("DateTimeOffsetToXXX",
                            // Simple Types
                            new TryConvertGenericTest<DateTimeOffset, bool>("DateTimeOffsetToBool", TestDateTimeOffset, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTimeOffset, byte>("DateTimeOffsetToByte", TestDateTimeOffset, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTimeOffset, byte[]>("DateTimeOffsetToByteArray", TestDateTimeOffset, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTimeOffset, char>("DateTimeOffsetToChar", TestDateTimeOffset, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTimeOffset, DateTime>("DateTimeOffsetToDateTime", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTimeOffset, DateTimeOffset>("DateTimeOffsetToDateTimeOffset", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTimeOffset, decimal>("DateTimeOffsetToDecimal", TestDateTimeOffset, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTimeOffset, double>("DateTimeOffsetToDouble", TestDateTimeOffset, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTimeOffset, PrimaryColor>("DateTimeOffsetToEnum", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTimeOffset, float>("DateTimeOffsetToFloat", TestDateTimeOffset, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTimeOffset, Guid>("DateTimeOffsetToGuid", TestDateTimeOffset, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTimeOffset, int>("DateTimeOffsetToInt", TestDateTimeOffset, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTimeOffset, long>("DateTimeOffsetToLong", TestDateTimeOffset, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTimeOffset, sbyte>("DateTimeOffsetToSByte", TestDateTimeOffset, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTimeOffset, short>("DateTimeOffsetToShort", TestDateTimeOffset, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToString", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetString),
                            new TryConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToStringWithFormat", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormat, FormatDateTimeOffsetContext),
                            new TryConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToStringWithFormatAndFormatProvider", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeOffsetContext),
                            new TryConvertGenericTest<DateTimeOffset, TimeSpan>("DateTimeOffsetToTimeSpan", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTimeOffset, Type>("DateTimeOffsetToType", TestDateTimeOffset, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTimeOffset, uint>("DateTimeOffsetToUInt", TestDateTimeOffset, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTimeOffset, ulong>("DateTimeOffsetToULong", TestDateTimeOffset, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTimeOffset, Uri>("DateTimeOffsetToUri", TestDateTimeOffset, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTimeOffset, ushort>("DateTimeOffsetToUShort", TestDateTimeOffset, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DateTimeOffset, bool?>("DateTimeOffsetToNullable<Bool>", TestDateTimeOffset, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTimeOffset, byte?>("DateTimeOffsetToNullable<Byte>", TestDateTimeOffset, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTimeOffset, char?>("DateTimeOffsetToNullable<Char>", TestDateTimeOffset, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTimeOffset, DateTime?>("DateTimeOffsetToNullable<DateTime>", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTimeOffset, DateTimeOffset?>("DateTimeOffsetToNullable<DateTimeOffset>", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTimeOffset, decimal?>("DateTimeOffsetToNullable<Decimal>", TestDateTimeOffset, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTimeOffset, double?>("DateTimeOffsetToNullable<Double>", TestDateTimeOffset, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTimeOffset, PrimaryColor?>("DateTimeOffsetToNullable<Enum>", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTimeOffset, float?>("DateTimeOffsetToNullable<Float>", TestDateTimeOffset, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTimeOffset, Guid?>("DateTimeOffsetToNullable<Guid>", TestDateTimeOffset, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<DateTimeOffset, int?>("DateTimeOffsetToNullable<Int>", TestDateTimeOffset, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTimeOffset, long?>("DateTimeOffsetToNullable<Long>", TestDateTimeOffset, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTimeOffset, sbyte?>("DateTimeOffsetToNullable<SByte>", TestDateTimeOffset, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTimeOffset, short?>("DateTimeOffsetToNullable<Short>", TestDateTimeOffset, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTimeOffset, TimeSpan?>("DateTimeOffsetToNullable<TimeSpan>", TestDateTimeOffset, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<DateTimeOffset, uint?>("DateTimeOffsetToNullable<UInt>", TestDateTimeOffset, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTimeOffset, ulong?>("DateTimeOffsetToNullable<ULong>", TestDateTimeOffset, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTimeOffset, ushort?>("DateTimeOffsetToNullable<UShort>", TestDateTimeOffset, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DateTimeOffset, IInterface>("DateTimeOffsetToInterface", TestDateTimeOffset, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTimeOffset, BaseClass>("DateTimeOffsetToBaseClass", TestDateTimeOffset, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTimeOffset, DerivedClass>("DateTimeOffsetToDerivedClass", TestDateTimeOffset, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
