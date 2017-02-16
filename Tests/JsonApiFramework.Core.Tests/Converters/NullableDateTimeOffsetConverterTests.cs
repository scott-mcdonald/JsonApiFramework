// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class NullableDateTimeOffsetConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableDateTimeOffsetConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<DateTimeOffset>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<DateTimeOffset?, bool>("Nullable<DateTimeOffset>ToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTimeOffset?, bool>("Nullable<DateTimeOffset>ToBool", TestDateTimeOffset, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTimeOffset?, byte>("Nullable<DateTimeOffset>ToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTimeOffset?, byte>("Nullable<DateTimeOffset>ToByte", TestDateTimeOffset, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTimeOffset?, byte[]>("Nullable<DateTimeOffset>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTimeOffset?, byte[]>("Nullable<DateTimeOffset>ToByteArray", TestDateTimeOffset, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTimeOffset?, char>("Nullable<DateTimeOffset>ToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTimeOffset?, char>("Nullable<DateTimeOffset>ToChar", TestDateTimeOffset, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTimeOffset?, DateTime>("Nullable<DateTimeOffset>ToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new ConvertGenericTest<DateTimeOffset?, DateTime>("Nullable<DateTimeOffset>ToDateTime", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTimeOffset?, DateTimeOffset>("Nullable<DateTimeOffset>ToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new ConvertGenericTest<DateTimeOffset?, DateTimeOffset>("Nullable<DateTimeOffset>ToDateTimeOffset", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTimeOffset?, decimal>("Nullable<DateTimeOffset>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTimeOffset?, decimal>("Nullable<DateTimeOffset>ToDecimal", TestDateTimeOffset, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTimeOffset?, double>("Nullable<DateTimeOffset>ToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTimeOffset?, double>("Nullable<DateTimeOffset>ToDouble", TestDateTimeOffset, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTimeOffset?, PrimaryColor>("Nullable<DateTimeOffset>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTimeOffset?, PrimaryColor>("Nullable<DateTimeOffset>ToEnum", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTimeOffset?, float>("Nullable<DateTimeOffset>ToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTimeOffset?, float>("Nullable<DateTimeOffset>ToFloat", TestDateTimeOffset, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTimeOffset?, Guid>("Nullable<DateTimeOffset>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTimeOffset?, Guid>("Nullable<DateTimeOffset>ToGuid", TestDateTimeOffset, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTimeOffset?, int>("Nullable<DateTimeOffset>ToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTimeOffset?, int>("Nullable<DateTimeOffset>ToInt", TestDateTimeOffset, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTimeOffset?, long>("Nullable<DateTimeOffset>ToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTimeOffset?, long>("Nullable<DateTimeOffset>ToLong", TestDateTimeOffset, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTimeOffset?, sbyte>("Nullable<DateTimeOffset>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTimeOffset?, sbyte>("Nullable<DateTimeOffset>ToSByte", TestDateTimeOffset, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTimeOffset?, short>("Nullable<DateTimeOffset>ToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTimeOffset?, short>("Nullable<DateTimeOffset>ToShort", TestDateTimeOffset, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToString", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetString),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormat", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormat, FormatDateTimeOffsetContext),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormatAndFormatProvider", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeOffsetContext),
                            new ConvertGenericTest<DateTimeOffset?, TimeSpan>("Nullable<DateTimeOffset>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTimeOffset?, TimeSpan>("Nullable<DateTimeOffset>ToTimeSpan", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTimeOffset?, Type>("Nullable<DateTimeOffset>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTimeOffset?, Type>("Nullable<DateTimeOffset>ToType", TestDateTimeOffset, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTimeOffset?, uint>("Nullable<DateTimeOffset>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTimeOffset?, uint>("Nullable<DateTimeOffset>ToUInt", TestDateTimeOffset, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTimeOffset?, ulong>("Nullable<DateTimeOffset>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTimeOffset?, ulong>("Nullable<DateTimeOffset>ToULong", TestDateTimeOffset, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTimeOffset?, Uri>("Nullable<DateTimeOffset>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTimeOffset?, Uri>("Nullable<DateTimeOffset>ToUri", TestDateTimeOffset, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTimeOffset?, ushort>("Nullable<DateTimeOffset>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<DateTimeOffset?, ushort>("Nullable<DateTimeOffset>ToUShort", TestDateTimeOffset, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DateTimeOffset?, bool?>("Nullable<DateTimeOffset>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTimeOffset?, bool?>("Nullable<DateTimeOffset>ToNullable<Bool>", TestDateTimeOffset, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTimeOffset?, byte?>("Nullable<DateTimeOffset>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTimeOffset?, byte?>("Nullable<DateTimeOffset>ToNullable<Byte>", TestDateTimeOffset, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTimeOffset?, char?>("Nullable<DateTimeOffset>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTimeOffset?, char?>("Nullable<DateTimeOffset>ToNullable<Char>", TestDateTimeOffset, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTimeOffset?, DateTime?>("Nullable<DateTimeOffset>ToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new ConvertGenericTest<DateTimeOffset?, DateTime?>("Nullable<DateTimeOffset>ToNullable<DateTime>", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTimeOffset?, DateTimeOffset?>("Nullable<DateTimeOffset>ToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new ConvertGenericTest<DateTimeOffset?, DateTimeOffset?>("Nullable<DateTimeOffset>ToNullable<DateTimeOffset>", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTimeOffset?, decimal?>("Nullable<DateTimeOffset>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTimeOffset?, decimal?>("Nullable<DateTimeOffset>ToNullable<Decimal>", TestDateTimeOffset, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTimeOffset?, double?>("Nullable<DateTimeOffset>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTimeOffset?, double?>("Nullable<DateTimeOffset>ToNullable<Double>", TestDateTimeOffset, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTimeOffset?, PrimaryColor?>("Nullable<DateTimeOffset>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTimeOffset?, PrimaryColor?>("Nullable<DateTimeOffset>ToNullable<Enum>", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTimeOffset?, float?>("Nullable<DateTimeOffset>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTimeOffset?, float?>("Nullable<DateTimeOffset>ToNullable<Float>", TestDateTimeOffset, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTimeOffset?, Guid?>("Nullable<DateTimeOffset>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<DateTimeOffset?, Guid?>("Nullable<DateTimeOffset>ToNullable<Guid>", TestDateTimeOffset, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<DateTimeOffset?, int?>("Nullable<DateTimeOffset>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTimeOffset?, int?>("Nullable<DateTimeOffset>ToNullable<Int>", TestDateTimeOffset, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTimeOffset?, long?>("Nullable<DateTimeOffset>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTimeOffset?, long?>("Nullable<DateTimeOffset>ToNullable<Long>", TestDateTimeOffset, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTimeOffset?, sbyte?>("Nullable<DateTimeOffset>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTimeOffset?, sbyte?>("Nullable<DateTimeOffset>ToNullable<SByte>", TestDateTimeOffset, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTimeOffset?, short?>("Nullable<DateTimeOffset>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTimeOffset?, short?>("Nullable<DateTimeOffset>ToNullable<Short>", TestDateTimeOffset, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTimeOffset?, TimeSpan?>("Nullable<DateTimeOffset>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<DateTimeOffset?, TimeSpan?>("Nullable<DateTimeOffset>ToNullable<TimeSpan>", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<DateTimeOffset?, uint?>("Nullable<DateTimeOffset>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTimeOffset?, uint?>("Nullable<DateTimeOffset>ToNullable<UInt>", TestDateTimeOffset, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTimeOffset?, ulong?>("Nullable<DateTimeOffset>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTimeOffset?, ulong?>("Nullable<DateTimeOffset>ToNullable<ULong>", TestDateTimeOffset, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTimeOffset?, ushort?>("Nullable<DateTimeOffset>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<DateTimeOffset?, ushort?>("Nullable<DateTimeOffset>ToNullable<UShort>", TestDateTimeOffset, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DateTimeOffset?, IInterface>("Nullable<DateTimeOffset>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTimeOffset?, IInterface>("Nullable<DateTimeOffset>ToInterface", TestDateTimeOffset, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTimeOffset?, BaseClass>("Nullable<DateTimeOffset>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTimeOffset?, BaseClass>("Nullable<DateTimeOffset>ToBaseClass", TestDateTimeOffset, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTimeOffset?, DerivedClass>("Nullable<DateTimeOffset>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<DateTimeOffset?, DerivedClass>("Nullable<DateTimeOffset>ToDerivedClass", TestDateTimeOffset, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<DateTimeOffset>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<DateTimeOffset?, bool>("Nullable<DateTimeOffset>ToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTimeOffset?, bool>("Nullable<DateTimeOffset>ToBool", TestDateTimeOffset, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTimeOffset?, byte>("Nullable<DateTimeOffset>ToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTimeOffset?, byte>("Nullable<DateTimeOffset>ToByte", TestDateTimeOffset, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTimeOffset?, byte[]>("Nullable<DateTimeOffset>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTimeOffset?, byte[]>("Nullable<DateTimeOffset>ToByteArray", TestDateTimeOffset, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTimeOffset?, char>("Nullable<DateTimeOffset>ToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTimeOffset?, char>("Nullable<DateTimeOffset>ToChar", TestDateTimeOffset, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTimeOffset?, DateTime>("Nullable<DateTimeOffset>ToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new TryConvertGenericTest<DateTimeOffset?, DateTime>("Nullable<DateTimeOffset>ToDateTime", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTimeOffset?, DateTimeOffset>("Nullable<DateTimeOffset>ToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new TryConvertGenericTest<DateTimeOffset?, DateTimeOffset>("Nullable<DateTimeOffset>ToDateTimeOffset", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTimeOffset?, decimal>("Nullable<DateTimeOffset>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTimeOffset?, decimal>("Nullable<DateTimeOffset>ToDecimal", TestDateTimeOffset, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTimeOffset?, double>("Nullable<DateTimeOffset>ToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTimeOffset?, double>("Nullable<DateTimeOffset>ToDouble", TestDateTimeOffset, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTimeOffset?, PrimaryColor>("Nullable<DateTimeOffset>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTimeOffset?, PrimaryColor>("Nullable<DateTimeOffset>ToEnum", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTimeOffset?, float>("Nullable<DateTimeOffset>ToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTimeOffset?, float>("Nullable<DateTimeOffset>ToFloat", TestDateTimeOffset, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTimeOffset?, Guid>("Nullable<DateTimeOffset>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTimeOffset?, Guid>("Nullable<DateTimeOffset>ToGuid", TestDateTimeOffset, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTimeOffset?, int>("Nullable<DateTimeOffset>ToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTimeOffset?, int>("Nullable<DateTimeOffset>ToInt", TestDateTimeOffset, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTimeOffset?, long>("Nullable<DateTimeOffset>ToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTimeOffset?, long>("Nullable<DateTimeOffset>ToLong", TestDateTimeOffset, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTimeOffset?, sbyte>("Nullable<DateTimeOffset>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTimeOffset?, sbyte>("Nullable<DateTimeOffset>ToSByte", TestDateTimeOffset, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTimeOffset?, short>("Nullable<DateTimeOffset>ToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTimeOffset?, short>("Nullable<DateTimeOffset>ToShort", TestDateTimeOffset, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToString", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetString),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormat", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormat, FormatDateTimeOffsetContext),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormatAndFormatProvider", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeOffsetContext),
                            new TryConvertGenericTest<DateTimeOffset?, TimeSpan>("Nullable<DateTimeOffset>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTimeOffset?, TimeSpan>("Nullable<DateTimeOffset>ToTimeSpan", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTimeOffset?, Type>("Nullable<DateTimeOffset>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTimeOffset?, Type>("Nullable<DateTimeOffset>ToType", TestDateTimeOffset, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTimeOffset?, uint>("Nullable<DateTimeOffset>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTimeOffset?, uint>("Nullable<DateTimeOffset>ToUInt", TestDateTimeOffset, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTimeOffset?, ulong>("Nullable<DateTimeOffset>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTimeOffset?, ulong>("Nullable<DateTimeOffset>ToULong", TestDateTimeOffset, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTimeOffset?, Uri>("Nullable<DateTimeOffset>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTimeOffset?, Uri>("Nullable<DateTimeOffset>ToUri", TestDateTimeOffset, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTimeOffset?, ushort>("Nullable<DateTimeOffset>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<DateTimeOffset?, ushort>("Nullable<DateTimeOffset>ToUShort", TestDateTimeOffset, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DateTimeOffset?, bool?>("Nullable<DateTimeOffset>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTimeOffset?, bool?>("Nullable<DateTimeOffset>ToNullable<Bool>", TestDateTimeOffset, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTimeOffset?, byte?>("Nullable<DateTimeOffset>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTimeOffset?, byte?>("Nullable<DateTimeOffset>ToNullable<Byte>", TestDateTimeOffset, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTimeOffset?, char?>("Nullable<DateTimeOffset>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTimeOffset?, char?>("Nullable<DateTimeOffset>ToNullable<Char>", TestDateTimeOffset, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTimeOffset?, DateTime?>("Nullable<DateTimeOffset>ToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new TryConvertGenericTest<DateTimeOffset?, DateTime?>("Nullable<DateTimeOffset>ToNullable<DateTime>", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTimeOffset?, DateTimeOffset?>("Nullable<DateTimeOffset>ToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new TryConvertGenericTest<DateTimeOffset?, DateTimeOffset?>("Nullable<DateTimeOffset>ToNullable<DateTimeOffset>", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTimeOffset?, decimal?>("Nullable<DateTimeOffset>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTimeOffset?, decimal?>("Nullable<DateTimeOffset>ToNullable<Decimal>", TestDateTimeOffset, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTimeOffset?, double?>("Nullable<DateTimeOffset>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTimeOffset?, double?>("Nullable<DateTimeOffset>ToNullable<Double>", TestDateTimeOffset, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTimeOffset?, PrimaryColor?>("Nullable<DateTimeOffset>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTimeOffset?, PrimaryColor?>("Nullable<DateTimeOffset>ToNullable<Enum>", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTimeOffset?, float?>("Nullable<DateTimeOffset>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTimeOffset?, float?>("Nullable<DateTimeOffset>ToNullable<Float>", TestDateTimeOffset, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTimeOffset?, Guid?>("Nullable<DateTimeOffset>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<DateTimeOffset?, Guid?>("Nullable<DateTimeOffset>ToNullable<Guid>", TestDateTimeOffset, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<DateTimeOffset?, int?>("Nullable<DateTimeOffset>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTimeOffset?, int?>("Nullable<DateTimeOffset>ToNullable<Int>", TestDateTimeOffset, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTimeOffset?, long?>("Nullable<DateTimeOffset>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTimeOffset?, long?>("Nullable<DateTimeOffset>ToNullable<Long>", TestDateTimeOffset, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTimeOffset?, sbyte?>("Nullable<DateTimeOffset>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTimeOffset?, sbyte?>("Nullable<DateTimeOffset>ToNullable<SByte>", TestDateTimeOffset, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTimeOffset?, short?>("Nullable<DateTimeOffset>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTimeOffset?, short?>("Nullable<DateTimeOffset>ToNullable<Short>", TestDateTimeOffset, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTimeOffset?, TimeSpan?>("Nullable<DateTimeOffset>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<DateTimeOffset?, TimeSpan?>("Nullable<DateTimeOffset>ToNullable<TimeSpan>", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<DateTimeOffset?, uint?>("Nullable<DateTimeOffset>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTimeOffset?, uint?>("Nullable<DateTimeOffset>ToNullable<UInt>", TestDateTimeOffset, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTimeOffset?, ulong?>("Nullable<DateTimeOffset>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTimeOffset?, ulong?>("Nullable<DateTimeOffset>ToNullable<ULong>", TestDateTimeOffset, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTimeOffset?, ushort?>("Nullable<DateTimeOffset>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<DateTimeOffset?, ushort?>("Nullable<DateTimeOffset>ToNullable<UShort>", TestDateTimeOffset, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DateTimeOffset?, IInterface>("Nullable<DateTimeOffset>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTimeOffset?, IInterface>("Nullable<DateTimeOffset>ToInterface", TestDateTimeOffset, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTimeOffset?, BaseClass>("Nullable<DateTimeOffset>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTimeOffset?, BaseClass>("Nullable<DateTimeOffset>ToBaseClass", TestDateTimeOffset, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTimeOffset?, DerivedClass>("Nullable<DateTimeOffset>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<DateTimeOffset?, DerivedClass>("Nullable<DateTimeOffset>ToDerivedClass", TestDateTimeOffset, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
