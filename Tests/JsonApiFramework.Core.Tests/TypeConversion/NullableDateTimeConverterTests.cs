// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableDateTimeConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableDateTimeConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<DateTime>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<DateTime?, bool>("Nullable<DateTime>ToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTime?, bool>("Nullable<DateTime>ToBool", TestDateTime, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTime?, byte>("Nullable<DateTime>ToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTime?, byte>("Nullable<DateTime>ToByte", TestDateTime, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTime?, byte[]>("Nullable<DateTime>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTime?, byte[]>("Nullable<DateTime>ToByteArray", TestDateTime, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTime?, char>("Nullable<DateTime>ToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTime?, char>("Nullable<DateTime>ToChar", TestDateTime, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTime?, DateTime>("Nullable<DateTime>ToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new ConvertGenericTest<DateTime?, DateTime>("Nullable<DateTime>ToDateTime", TestDateTime, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTime?, DateTimeOffset>("Nullable<DateTime>ToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new ConvertGenericTest<DateTime?, DateTimeOffset>("Nullable<DateTime>ToDateTimeOffset", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTime?, decimal>("Nullable<DateTime>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTime?, decimal>("Nullable<DateTime>ToDecimal", TestDateTime, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTime?, double>("Nullable<DateTime>ToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTime?, double>("Nullable<DateTime>ToDouble", TestDateTime, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTime?, PrimaryColor>("Nullable<DateTime>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTime?, PrimaryColor>("Nullable<DateTime>ToEnum", TestDateTime, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTime?, float>("Nullable<DateTime>ToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTime?, float>("Nullable<DateTime>ToFloat", TestDateTime, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTime?, Guid>("Nullable<DateTime>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTime?, Guid>("Nullable<DateTime>ToGuid", TestDateTime, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTime?, int>("Nullable<DateTime>ToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTime?, int>("Nullable<DateTime>ToInt", TestDateTime, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTime?, long>("Nullable<DateTime>ToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTime?, long>("Nullable<DateTime>ToLong", TestDateTime, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTime?, sbyte>("Nullable<DateTime>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTime?, sbyte>("Nullable<DateTime>ToSByte", TestDateTime, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTime?, short>("Nullable<DateTime>ToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTime?, short>("Nullable<DateTime>ToShort", TestDateTime, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToString", TestDateTime, ConvertResult.Success, TestDateTimeString),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormat", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormat, FormatDateTimeContext),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormatAndFormatProvider", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeContext),
                            new ConvertGenericTest<DateTime?, TimeSpan>("Nullable<DateTime>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTime?, TimeSpan>("Nullable<DateTime>ToTimeSpan", TestDateTime, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTime?, Type>("Nullable<DateTime>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTime?, Type>("Nullable<DateTime>ToType", TestDateTime, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTime?, uint>("Nullable<DateTime>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTime?, uint>("Nullable<DateTime>ToUInt", TestDateTime, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTime?, ulong>("Nullable<DateTime>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTime?, ulong>("Nullable<DateTime>ToULong", TestDateTime, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTime?, Uri>("Nullable<DateTime>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTime?, Uri>("Nullable<DateTime>ToUri", TestDateTime, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTime?, ushort>("Nullable<DateTime>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<DateTime?, ushort>("Nullable<DateTime>ToUShort", TestDateTime, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DateTime?, bool?>("Nullable<DateTime>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTime?, bool?>("Nullable<DateTime>ToNullable<Bool>", TestDateTime, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTime?, byte?>("Nullable<DateTime>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTime?, byte?>("Nullable<DateTime>ToNullable<Byte>", TestDateTime, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTime?, char?>("Nullable<DateTime>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTime?, char?>("Nullable<DateTime>ToNullable<Char>", TestDateTime, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTime?, DateTime?>("Nullable<DateTime>ToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new ConvertGenericTest<DateTime?, DateTime?>("Nullable<DateTime>ToNullable<DateTime>", TestDateTime, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTime?, DateTimeOffset?>("Nullable<DateTime>ToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new ConvertGenericTest<DateTime?, DateTimeOffset?>("Nullable<DateTime>ToNullable<DateTimeOffset>", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTime?, decimal?>("Nullable<DateTime>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTime?, decimal?>("Nullable<DateTime>ToNullable<Decimal>", TestDateTime, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTime?, double?>("Nullable<DateTime>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTime?, double?>("Nullable<DateTime>ToNullable<Double>", TestDateTime, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTime?, PrimaryColor?>("Nullable<DateTime>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTime?, PrimaryColor?>("Nullable<DateTime>ToNullable<Enum>", TestDateTime, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTime?, float?>("Nullable<DateTime>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTime?, float?>("Nullable<DateTime>ToNullable<Float>", TestDateTime, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTime?, Guid?>("Nullable<DateTime>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<DateTime?, Guid?>("Nullable<DateTime>ToNullable<Guid>", TestDateTime, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<DateTime?, int?>("Nullable<DateTime>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTime?, int?>("Nullable<DateTime>ToNullable<Int>", TestDateTime, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTime?, long?>("Nullable<DateTime>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTime?, long?>("Nullable<DateTime>ToNullable<Long>", TestDateTime, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTime?, sbyte?>("Nullable<DateTime>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTime?, sbyte?>("Nullable<DateTime>ToNullable<SByte>", TestDateTime, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTime?, short?>("Nullable<DateTime>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTime?, short?>("Nullable<DateTime>ToNullable<Short>", TestDateTime, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTime?, TimeSpan?>("Nullable<DateTime>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<DateTime?, TimeSpan?>("Nullable<DateTime>ToNullable<TimeSpan>", TestDateTime, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<DateTime?, uint?>("Nullable<DateTime>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTime?, uint?>("Nullable<DateTime>ToNullable<UInt>", TestDateTime, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTime?, ulong?>("Nullable<DateTime>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTime?, ulong?>("Nullable<DateTime>ToNullable<ULong>", TestDateTime, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTime?, ushort?>("Nullable<DateTime>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<DateTime?, ushort?>("Nullable<DateTime>ToNullable<UShort>", TestDateTime, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DateTime?, IInterface>("Nullable<DateTime>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTime?, IInterface>("Nullable<DateTime>ToInterface", TestDateTime, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTime?, BaseClass>("Nullable<DateTime>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTime?, BaseClass>("Nullable<DateTime>ToBaseClass", TestDateTime, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTime?, DerivedClass>("Nullable<DateTime>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<DateTime?, DerivedClass>("Nullable<DateTime>ToDerivedClass", TestDateTime, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<DateTime>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<DateTime?, bool>("Nullable<DateTime>ToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTime?, bool>("Nullable<DateTime>ToBool", TestDateTime, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTime?, byte>("Nullable<DateTime>ToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTime?, byte>("Nullable<DateTime>ToByte", TestDateTime, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTime?, byte[]>("Nullable<DateTime>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTime?, byte[]>("Nullable<DateTime>ToByteArray", TestDateTime, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTime?, char>("Nullable<DateTime>ToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTime?, char>("Nullable<DateTime>ToChar", TestDateTime, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTime?, DateTime>("Nullable<DateTime>ToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new TryConvertGenericTest<DateTime?, DateTime>("Nullable<DateTime>ToDateTime", TestDateTime, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTime?, DateTimeOffset>("Nullable<DateTime>ToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new TryConvertGenericTest<DateTime?, DateTimeOffset>("Nullable<DateTime>ToDateTimeOffset", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTime?, decimal>("Nullable<DateTime>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTime?, decimal>("Nullable<DateTime>ToDecimal", TestDateTime, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTime?, double>("Nullable<DateTime>ToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTime?, double>("Nullable<DateTime>ToDouble", TestDateTime, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTime?, PrimaryColor>("Nullable<DateTime>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTime?, PrimaryColor>("Nullable<DateTime>ToEnum", TestDateTime, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTime?, float>("Nullable<DateTime>ToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTime?, float>("Nullable<DateTime>ToFloat", TestDateTime, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTime?, Guid>("Nullable<DateTime>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTime?, Guid>("Nullable<DateTime>ToGuid", TestDateTime, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTime?, int>("Nullable<DateTime>ToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTime?, int>("Nullable<DateTime>ToInt", TestDateTime, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTime?, long>("Nullable<DateTime>ToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTime?, long>("Nullable<DateTime>ToLong", TestDateTime, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTime?, sbyte>("Nullable<DateTime>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTime?, sbyte>("Nullable<DateTime>ToSByte", TestDateTime, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTime?, short>("Nullable<DateTime>ToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTime?, short>("Nullable<DateTime>ToShort", TestDateTime, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToString", TestDateTime, ConvertResult.Success, TestDateTimeString),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormat", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormat, FormatDateTimeContext),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormatAndFormatProvider", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeContext),
                            new TryConvertGenericTest<DateTime?, TimeSpan>("Nullable<DateTime>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTime?, TimeSpan>("Nullable<DateTime>ToTimeSpan", TestDateTime, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTime?, Type>("Nullable<DateTime>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTime?, Type>("Nullable<DateTime>ToType", TestDateTime, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTime?, uint>("Nullable<DateTime>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTime?, uint>("Nullable<DateTime>ToUInt", TestDateTime, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTime?, ulong>("Nullable<DateTime>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTime?, ulong>("Nullable<DateTime>ToULong", TestDateTime, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTime?, Uri>("Nullable<DateTime>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTime?, Uri>("Nullable<DateTime>ToUri", TestDateTime, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTime?, ushort>("Nullable<DateTime>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<DateTime?, ushort>("Nullable<DateTime>ToUShort", TestDateTime, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DateTime?, bool?>("Nullable<DateTime>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTime?, bool?>("Nullable<DateTime>ToNullable<Bool>", TestDateTime, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTime?, byte?>("Nullable<DateTime>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTime?, byte?>("Nullable<DateTime>ToNullable<Byte>", TestDateTime, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTime?, char?>("Nullable<DateTime>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTime?, char?>("Nullable<DateTime>ToNullable<Char>", TestDateTime, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTime?, DateTime?>("Nullable<DateTime>ToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new TryConvertGenericTest<DateTime?, DateTime?>("Nullable<DateTime>ToNullable<DateTime>", TestDateTime, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTime?, DateTimeOffset?>("Nullable<DateTime>ToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new TryConvertGenericTest<DateTime?, DateTimeOffset?>("Nullable<DateTime>ToNullable<DateTimeOffset>", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTime?, decimal?>("Nullable<DateTime>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTime?, decimal?>("Nullable<DateTime>ToNullable<Decimal>", TestDateTime, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTime?, double?>("Nullable<DateTime>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTime?, double?>("Nullable<DateTime>ToNullable<Double>", TestDateTime, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTime?, PrimaryColor?>("Nullable<DateTime>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTime?, PrimaryColor?>("Nullable<DateTime>ToNullable<Enum>", TestDateTime, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTime?, float?>("Nullable<DateTime>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTime?, float?>("Nullable<DateTime>ToNullable<Float>", TestDateTime, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTime?, Guid?>("Nullable<DateTime>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<DateTime?, Guid?>("Nullable<DateTime>ToNullable<Guid>", TestDateTime, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<DateTime?, int?>("Nullable<DateTime>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTime?, int?>("Nullable<DateTime>ToNullable<Int>", TestDateTime, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTime?, long?>("Nullable<DateTime>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTime?, long?>("Nullable<DateTime>ToNullable<Long>", TestDateTime, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTime?, sbyte?>("Nullable<DateTime>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTime?, sbyte?>("Nullable<DateTime>ToNullable<SByte>", TestDateTime, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTime?, short?>("Nullable<DateTime>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTime?, short?>("Nullable<DateTime>ToNullable<Short>", TestDateTime, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTime?, TimeSpan?>("Nullable<DateTime>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<DateTime?, TimeSpan?>("Nullable<DateTime>ToNullable<TimeSpan>", TestDateTime, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<DateTime?, uint?>("Nullable<DateTime>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTime?, uint?>("Nullable<DateTime>ToNullable<UInt>", TestDateTime, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTime?, ulong?>("Nullable<DateTime>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTime?, ulong?>("Nullable<DateTime>ToNullable<ULong>", TestDateTime, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTime?, ushort?>("Nullable<DateTime>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<DateTime?, ushort?>("Nullable<DateTime>ToNullable<UShort>", TestDateTime, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DateTime?, IInterface>("Nullable<DateTime>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTime?, IInterface>("Nullable<DateTime>ToInterface", TestDateTime, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTime?, BaseClass>("Nullable<DateTime>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTime?, BaseClass>("Nullable<DateTime>ToBaseClass", TestDateTime, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTime?, DerivedClass>("Nullable<DateTime>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<DateTime?, DerivedClass>("Nullable<DateTime>ToDerivedClass", TestDateTime, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
