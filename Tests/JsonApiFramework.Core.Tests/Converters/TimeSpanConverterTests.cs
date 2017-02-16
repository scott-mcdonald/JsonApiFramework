// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class TimeSpanConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TimeSpanConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("TimeSpanToXXX",
                            // Simple Types
                            new ConvertGenericTest<TimeSpan, bool>("TimeSpanToBool", TestTimeSpan, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<TimeSpan, byte>("TimeSpanToByte", TestTimeSpan, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<TimeSpan, byte[]>("TimeSpanToByteArray", TestTimeSpan, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<TimeSpan, char>("TimeSpanToChar", TestTimeSpan, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<TimeSpan, DateTime>("TimeSpanToDateTime", TestTimeSpan, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<TimeSpan, DateTimeOffset>("TimeSpanToDateTimeOffset", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<TimeSpan, decimal>("TimeSpanToDecimal", TestTimeSpan, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<TimeSpan, double>("TimeSpanToDouble", TestTimeSpan, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<TimeSpan, PrimaryColor>("TimeSpanToEnum", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<TimeSpan, float>("TimeSpanToFloat", TestTimeSpan, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<TimeSpan, Guid>("TimeSpanToGuid", TestTimeSpan, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<TimeSpan, int>("TimeSpanToInt", TestTimeSpan, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<TimeSpan, long>("TimeSpanToLong", TestTimeSpan, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<TimeSpan, sbyte>("TimeSpanToSByte", TestTimeSpan, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<TimeSpan, short>("TimeSpanToShort", TestTimeSpan, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<TimeSpan, string>("TimeSpanToString", TestTimeSpan, ConvertResult.Success, TestTimeSpanString),
                            new ConvertGenericTest<TimeSpan, string>("TimeSpanToStringWithFormat", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormat, FormatTimeSpanContext),
                            new ConvertGenericTest<TimeSpan, string>("TimeSpanToStringWithFormatAndFormatProvider", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormatAndFormatProvider, FormatAndFormatProviderTimeSpanContext),
                            new ConvertGenericTest<TimeSpan, TimeSpan>("TimeSpanToTimeSpan", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<TimeSpan, Type>("TimeSpanToType", TestTimeSpan, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<TimeSpan, uint>("TimeSpanToUInt", TestTimeSpan, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<TimeSpan, ulong>("TimeSpanToULong", TestTimeSpan, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<TimeSpan, Uri>("TimeSpanToUri", TestTimeSpan, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<TimeSpan, ushort>("TimeSpanToUShort", TestTimeSpan, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<TimeSpan, bool?>("TimeSpanToNullable<Bool>", TestTimeSpan, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<TimeSpan, byte?>("TimeSpanToNullable<Byte>", TestTimeSpan, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<TimeSpan, char?>("TimeSpanToNullable<Char>", TestTimeSpan, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<TimeSpan, DateTime?>("TimeSpanToNullable<DateTime>", TestTimeSpan, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<TimeSpan, DateTimeOffset?>("TimeSpanToNullable<DateTimeOffset>", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<TimeSpan, decimal?>("TimeSpanToNullable<Decimal>", TestTimeSpan, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<TimeSpan, double?>("TimeSpanToNullable<Double>", TestTimeSpan, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<TimeSpan, PrimaryColor?>("TimeSpanToNullable<Enum>", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<TimeSpan, float?>("TimeSpanToNullable<Float>", TestTimeSpan, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<TimeSpan, Guid?>("TimeSpanToNullable<Guid>", TestTimeSpan, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<TimeSpan, int?>("TimeSpanToNullable<Int>", TestTimeSpan, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<TimeSpan, long?>("TimeSpanToNullable<Long>", TestTimeSpan, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<TimeSpan, sbyte?>("TimeSpanToNullable<SByte>", TestTimeSpan, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<TimeSpan, short?>("TimeSpanToNullable<Short>", TestTimeSpan, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<TimeSpan, TimeSpan?>("TimeSpanToNullable<TimeSpan>", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<TimeSpan, uint?>("TimeSpanToNullable<UInt>", TestTimeSpan, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<TimeSpan, ulong?>("TimeSpanToNullable<ULong>", TestTimeSpan, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<TimeSpan, ushort?>("TimeSpanToNullable<UShort>", TestTimeSpan, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<TimeSpan, IInterface>("TimeSpanToInterface", TestTimeSpan, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<TimeSpan, BaseClass>("TimeSpanToBaseClass", TestTimeSpan, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<TimeSpan, DerivedClass>("TimeSpanToDerivedClass", TestTimeSpan, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("TimeSpanToXXX",
                            // Simple Types
                            new TryConvertGenericTest<TimeSpan, bool>("TimeSpanToBool", TestTimeSpan, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<TimeSpan, byte>("TimeSpanToByte", TestTimeSpan, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<TimeSpan, byte[]>("TimeSpanToByteArray", TestTimeSpan, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<TimeSpan, char>("TimeSpanToChar", TestTimeSpan, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<TimeSpan, DateTime>("TimeSpanToDateTime", TestTimeSpan, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<TimeSpan, DateTimeOffset>("TimeSpanToDateTimeOffset", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<TimeSpan, decimal>("TimeSpanToDecimal", TestTimeSpan, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<TimeSpan, double>("TimeSpanToDouble", TestTimeSpan, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<TimeSpan, PrimaryColor>("TimeSpanToEnum", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<TimeSpan, float>("TimeSpanToFloat", TestTimeSpan, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<TimeSpan, Guid>("TimeSpanToGuid", TestTimeSpan, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<TimeSpan, int>("TimeSpanToInt", TestTimeSpan, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<TimeSpan, long>("TimeSpanToLong", TestTimeSpan, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<TimeSpan, sbyte>("TimeSpanToSByte", TestTimeSpan, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<TimeSpan, short>("TimeSpanToShort", TestTimeSpan, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<TimeSpan, string>("TimeSpanToString", TestTimeSpan, ConvertResult.Success, TestTimeSpanString),
                            new TryConvertGenericTest<TimeSpan, string>("TimeSpanToStringWithFormat", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormat, FormatTimeSpanContext),
                            new TryConvertGenericTest<TimeSpan, string>("TimeSpanToStringWithFormatAndFormatProvider", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormatAndFormatProvider, FormatAndFormatProviderTimeSpanContext),
                            new TryConvertGenericTest<TimeSpan, TimeSpan>("TimeSpanToTimeSpan", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<TimeSpan, Type>("TimeSpanToType", TestTimeSpan, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<TimeSpan, uint>("TimeSpanToUInt", TestTimeSpan, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<TimeSpan, ulong>("TimeSpanToULong", TestTimeSpan, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<TimeSpan, Uri>("TimeSpanToUri", TestTimeSpan, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<TimeSpan, ushort>("TimeSpanToUShort", TestTimeSpan, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<TimeSpan, bool?>("TimeSpanToNullable<Bool>", TestTimeSpan, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<TimeSpan, byte?>("TimeSpanToNullable<Byte>", TestTimeSpan, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<TimeSpan, char?>("TimeSpanToNullable<Char>", TestTimeSpan, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<TimeSpan, DateTime?>("TimeSpanToNullable<DateTime>", TestTimeSpan, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<TimeSpan, DateTimeOffset?>("TimeSpanToNullable<DateTimeOffset>", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<TimeSpan, decimal?>("TimeSpanToNullable<Decimal>", TestTimeSpan, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<TimeSpan, double?>("TimeSpanToNullable<Double>", TestTimeSpan, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<TimeSpan, PrimaryColor?>("TimeSpanToNullable<Enum>", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<TimeSpan, float?>("TimeSpanToNullable<Float>", TestTimeSpan, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<TimeSpan, Guid?>("TimeSpanToNullable<Guid>", TestTimeSpan, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<TimeSpan, int?>("TimeSpanToNullable<Int>", TestTimeSpan, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<TimeSpan, long?>("TimeSpanToNullable<Long>", TestTimeSpan, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<TimeSpan, sbyte?>("TimeSpanToNullable<SByte>", TestTimeSpan, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<TimeSpan, short?>("TimeSpanToNullable<Short>", TestTimeSpan, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<TimeSpan, TimeSpan?>("TimeSpanToNullable<TimeSpan>", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<TimeSpan, uint?>("TimeSpanToNullable<UInt>", TestTimeSpan, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<TimeSpan, ulong?>("TimeSpanToNullable<ULong>", TestTimeSpan, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<TimeSpan, ushort?>("TimeSpanToNullable<UShort>", TestTimeSpan, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<TimeSpan, IInterface>("TimeSpanToInterface", TestTimeSpan, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<TimeSpan, BaseClass>("TimeSpanToBaseClass", TestTimeSpan, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<TimeSpan, DerivedClass>("TimeSpanToDerivedClass", TestTimeSpan, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
