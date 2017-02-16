// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class DateTimeConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DateTimeConverterTests(ITestOutputHelper output)
            : base(output, true)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(ConvertGenericTestData))]
        public void TestTypeConveterConvertGeneric(IUnitTest unitTest)
        {
            unitTest.Execute(this);
            this.WriteBuffer();
        }

        [Theory]
        [MemberData(nameof(TryConvertGenericTestData))]   
        public void TestTypeConveterTryConvertGeneric(IUnitTest unitTest)
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
                        new AggregateUnitTest("DateTimeToXXX",
                            // Simple Types
                            new ConvertGenericTest<DateTime, bool>("DateTimeToBool", TestDateTime, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTime, byte>("DateTimeToByte", TestDateTime, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTime, byte[]>("DateTimeToByteArray", TestDateTime, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTime, char>("DateTimeToChar", TestDateTime, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTime, DateTime>("DateTimeToDateTime", TestDateTime, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTime, DateTimeOffset>("DateTimeToDateTimeOffset", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTime, decimal>("DateTimeToDecimal", TestDateTime, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTime, double>("DateTimeToDouble", TestDateTime, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTime, PrimaryColor>("DateTimeToEnum", TestDateTime, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTime, float>("DateTimeToFloat", TestDateTime, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTime, Guid>("DateTimeToGuid", TestDateTime, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTime, int>("DateTimeToInt", TestDateTime, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTime, long>("DateTimeToLong", TestDateTime, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTime, sbyte>("DateTimeToSByte", TestDateTime, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTime, short>("DateTimeToShort", TestDateTime, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTime, string>("DateTimeToString", TestDateTime, ConvertResult.Success, TestDateTimeString),
                            new ConvertGenericTest<DateTime, string>("DateTimeToStringWithFormat", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormat, FormatDateTimeContext),
                            new ConvertGenericTest<DateTime, string>("DateTimeToStringWithFormatAndFormatProvider", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeContext),
                            new ConvertGenericTest<DateTime, TimeSpan>("DateTimeToTimeSpan", TestDateTime, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTime, Type>("DateTimeToType", TestDateTime, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTime, uint>("DateTimeToUInt", TestDateTime, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTime, ulong>("DateTimeToULong", TestDateTime, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTime, Uri>("DateTimeToUri", TestDateTime, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTime, ushort>("DateTimeToUShort", TestDateTime, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DateTime, bool?>("DateTimeToNullable<Bool>", TestDateTime, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTime, byte?>("DateTimeToNullable<Byte>", TestDateTime, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTime, char?>("DateTimeToNullable<Char>", TestDateTime, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTime, DateTime?>("DateTimeToNullable<DateTime>", TestDateTime, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTime, DateTimeOffset?>("DateTimeToNullable<DateTimeOffset>", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTime, decimal?>("DateTimeToNullable<Decimal>", TestDateTime, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTime, double?>("DateTimeToNullable<Double>", TestDateTime, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTime, PrimaryColor?>("DateTimeToNullable<Enum>", TestDateTime, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTime, float?>("DateTimeToNullable<Float>", TestDateTime, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTime, Guid?>("DateTimeToNullable<Guid>", TestDateTime, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<DateTime, int?>("DateTimeToNullable<Int>", TestDateTime, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTime, long?>("DateTimeToNullable<Long>", TestDateTime, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTime, sbyte?>("DateTimeToNullable<SByte>", TestDateTime, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTime, short?>("DateTimeToNullable<Short>", TestDateTime, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTime, TimeSpan?>("DateTimeToNullable<TimeSpan>", TestDateTime, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<DateTime, uint?>("DateTimeToNullable<UInt>", TestDateTime, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTime, ulong?>("DateTimeToNullable<ULong>", TestDateTime, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTime, ushort?>("DateTimeToNullable<UShort>", TestDateTime, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DateTime, IInterface>("DateTimeToInterface", TestDateTime, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTime, BaseClass>("DateTimeToBaseClass", TestDateTime, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTime, DerivedClass>("DateTimeToDerivedClass", TestDateTime, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("DateTimeToXXX",
                            // Simple Types
                            new TryConvertGenericTest<DateTime, bool>("DateTimeToBool", TestDateTime, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTime, byte>("DateTimeToByte", TestDateTime, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTime, byte[]>("DateTimeToByteArray", TestDateTime, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTime, char>("DateTimeToChar", TestDateTime, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTime, DateTime>("DateTimeToDateTime", TestDateTime, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTime, DateTimeOffset>("DateTimeToDateTimeOffset", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTime, decimal>("DateTimeToDecimal", TestDateTime, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTime, double>("DateTimeToDouble", TestDateTime, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTime, PrimaryColor>("DateTimeToEnum", TestDateTime, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTime, float>("DateTimeToFloat", TestDateTime, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTime, Guid>("DateTimeToGuid", TestDateTime, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTime, int>("DateTimeToInt", TestDateTime, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTime, long>("DateTimeToLong", TestDateTime, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTime, sbyte>("DateTimeToSByte", TestDateTime, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTime, short>("DateTimeToShort", TestDateTime, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTime, string>("DateTimeToString", TestDateTime, ConvertResult.Success, TestDateTimeString),
                            new TryConvertGenericTest<DateTime, string>("DateTimeToStringWithFormat", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormat, FormatDateTimeContext),
                            new TryConvertGenericTest<DateTime, string>("DateTimeToStringWithFormatAndFormatProvider", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeContext),
                            new TryConvertGenericTest<DateTime, TimeSpan>("DateTimeToTimeSpan", TestDateTime, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTime, Type>("DateTimeToType", TestDateTime, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTime, uint>("DateTimeToUInt", TestDateTime, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTime, ulong>("DateTimeToULong", TestDateTime, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTime, Uri>("DateTimeToUri", TestDateTime, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTime, ushort>("DateTimeToUShort", TestDateTime, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DateTime, bool?>("DateTimeToNullable<Bool>", TestDateTime, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTime, byte?>("DateTimeToNullable<Byte>", TestDateTime, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTime, char?>("DateTimeToNullable<Char>", TestDateTime, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTime, DateTime?>("DateTimeToNullable<DateTime>", TestDateTime, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTime, DateTimeOffset?>("DateTimeToNullable<DateTimeOffset>", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTime, decimal?>("DateTimeToNullable<Decimal>", TestDateTime, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTime, double?>("DateTimeToNullable<Double>", TestDateTime, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTime, PrimaryColor?>("DateTimeToNullable<Enum>", TestDateTime, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTime, float?>("DateTimeToNullable<Float>", TestDateTime, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTime, Guid?>("DateTimeToNullable<Guid>", TestDateTime, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<DateTime, int?>("DateTimeToNullable<Int>", TestDateTime, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTime, long?>("DateTimeToNullable<Long>", TestDateTime, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTime, sbyte?>("DateTimeToNullable<SByte>", TestDateTime, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTime, short?>("DateTimeToNullable<Short>", TestDateTime, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTime, TimeSpan?>("DateTimeToNullable<TimeSpan>", TestDateTime, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<DateTime, uint?>("DateTimeToNullable<UInt>", TestDateTime, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTime, ulong?>("DateTimeToNullable<ULong>", TestDateTime, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTime, ushort?>("DateTimeToNullable<UShort>", TestDateTime, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DateTime, IInterface>("DateTimeToInterface", TestDateTime, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTime, BaseClass>("DateTimeToBaseClass", TestDateTime, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTime, DerivedClass>("DateTimeToDerivedClass", TestDateTime, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
