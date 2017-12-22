// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableTimeSpanConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableTimeSpanConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<TimeSpan>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<TimeSpan?, bool>("Nullable<TimeSpan>ToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<TimeSpan?, bool>("Nullable<TimeSpan>ToBool", TestTimeSpan, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<TimeSpan?, byte>("Nullable<TimeSpan>ToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<TimeSpan?, byte>("Nullable<TimeSpan>ToByte", TestTimeSpan, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<TimeSpan?, byte[]>("Nullable<TimeSpan>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<TimeSpan?, byte[]>("Nullable<TimeSpan>ToByteArray", TestTimeSpan, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<TimeSpan?, char>("Nullable<TimeSpan>ToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<TimeSpan?, char>("Nullable<TimeSpan>ToChar", TestTimeSpan, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<TimeSpan?, DateTime>("Nullable<TimeSpan>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<TimeSpan?, DateTime>("Nullable<TimeSpan>ToDateTime", TestTimeSpan, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<TimeSpan?, DateTimeOffset>("Nullable<TimeSpan>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<TimeSpan?, DateTimeOffset>("Nullable<TimeSpan>ToDateTimeOffset", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<TimeSpan?, decimal>("Nullable<TimeSpan>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<TimeSpan?, decimal>("Nullable<TimeSpan>ToDecimal", TestTimeSpan, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<TimeSpan?, double>("Nullable<TimeSpan>ToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<TimeSpan?, double>("Nullable<TimeSpan>ToDouble", TestTimeSpan, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<TimeSpan?, PrimaryColor>("Nullable<TimeSpan>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<TimeSpan?, PrimaryColor>("Nullable<TimeSpan>ToEnum", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<TimeSpan?, float>("Nullable<TimeSpan>ToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<TimeSpan?, float>("Nullable<TimeSpan>ToFloat", TestTimeSpan, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<TimeSpan?, Guid>("Nullable<TimeSpan>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<TimeSpan?, Guid>("Nullable<TimeSpan>ToGuid", TestTimeSpan, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<TimeSpan?, int>("Nullable<TimeSpan>ToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<TimeSpan?, int>("Nullable<TimeSpan>ToInt", TestTimeSpan, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<TimeSpan?, long>("Nullable<TimeSpan>ToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<TimeSpan?, long>("Nullable<TimeSpan>ToLong", TestTimeSpan, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<TimeSpan?, sbyte>("Nullable<TimeSpan>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<TimeSpan?, sbyte>("Nullable<TimeSpan>ToSByte", TestTimeSpan, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<TimeSpan?, short>("Nullable<TimeSpan>ToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<TimeSpan?, short>("Nullable<TimeSpan>ToShort", TestTimeSpan, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToString", TestTimeSpan, ConvertResult.Success, TestTimeSpanString),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormat", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormat, FormatTimeSpanContext),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormatAndFormatProvider", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormatAndFormatProvider, FormatAndFormatProviderTimeSpanContext),
                            new ConvertGenericTest<TimeSpan?, TimeSpan>("Nullable<TimeSpan>ToTimeSpan", null, ConvertResult.Success, default(TimeSpan)),
                            new ConvertGenericTest<TimeSpan?, TimeSpan>("Nullable<TimeSpan>ToTimeSpan", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<TimeSpan?, Type>("Nullable<TimeSpan>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<TimeSpan?, Type>("Nullable<TimeSpan>ToType", TestTimeSpan, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<TimeSpan?, uint>("Nullable<TimeSpan>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<TimeSpan?, uint>("Nullable<TimeSpan>ToUInt", TestTimeSpan, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<TimeSpan?, ulong>("Nullable<TimeSpan>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<TimeSpan?, ulong>("Nullable<TimeSpan>ToULong", TestTimeSpan, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<TimeSpan?, Uri>("Nullable<TimeSpan>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<TimeSpan?, Uri>("Nullable<TimeSpan>ToUri", TestTimeSpan, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<TimeSpan?, ushort>("Nullable<TimeSpan>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<TimeSpan?, ushort>("Nullable<TimeSpan>ToUShort", TestTimeSpan, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<TimeSpan?, bool?>("Nullable<TimeSpan>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<TimeSpan?, bool?>("Nullable<TimeSpan>ToNullable<Bool>", TestTimeSpan, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<TimeSpan?, byte?>("Nullable<TimeSpan>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<TimeSpan?, byte?>("Nullable<TimeSpan>ToNullable<Byte>", TestTimeSpan, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<TimeSpan?, char?>("Nullable<TimeSpan>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<TimeSpan?, char?>("Nullable<TimeSpan>ToNullable<Char>", TestTimeSpan, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<TimeSpan?, DateTime?>("Nullable<TimeSpan>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<TimeSpan?, DateTime?>("Nullable<TimeSpan>ToNullable<DateTime>", TestTimeSpan, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<TimeSpan?, DateTimeOffset?>("Nullable<TimeSpan>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<TimeSpan?, DateTimeOffset?>("Nullable<TimeSpan>ToNullable<DateTimeOffset>", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<TimeSpan?, decimal?>("Nullable<TimeSpan>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<TimeSpan?, decimal?>("Nullable<TimeSpan>ToNullable<Decimal>", TestTimeSpan, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<TimeSpan?, double?>("Nullable<TimeSpan>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<TimeSpan?, double?>("Nullable<TimeSpan>ToNullable<Double>", TestTimeSpan, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<TimeSpan?, PrimaryColor?>("Nullable<TimeSpan>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<TimeSpan?, PrimaryColor?>("Nullable<TimeSpan>ToNullable<Enum>", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<TimeSpan?, float?>("Nullable<TimeSpan>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<TimeSpan?, float?>("Nullable<TimeSpan>ToNullable<Float>", TestTimeSpan, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<TimeSpan?, Guid?>("Nullable<TimeSpan>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<TimeSpan?, Guid?>("Nullable<TimeSpan>ToNullable<Guid>", TestTimeSpan, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<TimeSpan?, int?>("Nullable<TimeSpan>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<TimeSpan?, int?>("Nullable<TimeSpan>ToNullable<Int>", TestTimeSpan, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<TimeSpan?, long?>("Nullable<TimeSpan>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<TimeSpan?, long?>("Nullable<TimeSpan>ToNullable<Long>", TestTimeSpan, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<TimeSpan?, sbyte?>("Nullable<TimeSpan>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<TimeSpan?, sbyte?>("Nullable<TimeSpan>ToNullable<SByte>", TestTimeSpan, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<TimeSpan?, short?>("Nullable<TimeSpan>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<TimeSpan?, short?>("Nullable<TimeSpan>ToNullable<Short>", TestTimeSpan, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<TimeSpan?, TimeSpan?>("Nullable<TimeSpan>ToNullable<TimeSpan>", null, ConvertResult.Success, new TimeSpan?()),
                            new ConvertGenericTest<TimeSpan?, TimeSpan?>("Nullable<TimeSpan>ToNullable<TimeSpan>", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<TimeSpan?, uint?>("Nullable<TimeSpan>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<TimeSpan?, uint?>("Nullable<TimeSpan>ToNullable<UInt>", TestTimeSpan, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<TimeSpan?, ulong?>("Nullable<TimeSpan>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<TimeSpan?, ulong?>("Nullable<TimeSpan>ToNullable<ULong>", TestTimeSpan, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<TimeSpan?, ushort?>("Nullable<TimeSpan>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<TimeSpan?, ushort?>("Nullable<TimeSpan>ToNullable<UShort>", TestTimeSpan, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<TimeSpan?, IInterface>("Nullable<TimeSpan>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<TimeSpan?, IInterface>("Nullable<TimeSpan>ToInterface", TestTimeSpan, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<TimeSpan?, BaseClass>("Nullable<TimeSpan>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<TimeSpan?, BaseClass>("Nullable<TimeSpan>ToBaseClass", TestTimeSpan, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<TimeSpan?, DerivedClass>("Nullable<TimeSpan>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<TimeSpan?, DerivedClass>("Nullable<TimeSpan>ToDerivedClass", TestTimeSpan, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<TimeSpan>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<TimeSpan?, bool>("Nullable<TimeSpan>ToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<TimeSpan?, bool>("Nullable<TimeSpan>ToBool", TestTimeSpan, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<TimeSpan?, byte>("Nullable<TimeSpan>ToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<TimeSpan?, byte>("Nullable<TimeSpan>ToByte", TestTimeSpan, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<TimeSpan?, byte[]>("Nullable<TimeSpan>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<TimeSpan?, byte[]>("Nullable<TimeSpan>ToByteArray", TestTimeSpan, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<TimeSpan?, char>("Nullable<TimeSpan>ToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<TimeSpan?, char>("Nullable<TimeSpan>ToChar", TestTimeSpan, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<TimeSpan?, DateTime>("Nullable<TimeSpan>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<TimeSpan?, DateTime>("Nullable<TimeSpan>ToDateTime", TestTimeSpan, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<TimeSpan?, DateTimeOffset>("Nullable<TimeSpan>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<TimeSpan?, DateTimeOffset>("Nullable<TimeSpan>ToDateTimeOffset", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<TimeSpan?, decimal>("Nullable<TimeSpan>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<TimeSpan?, decimal>("Nullable<TimeSpan>ToDecimal", TestTimeSpan, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<TimeSpan?, double>("Nullable<TimeSpan>ToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<TimeSpan?, double>("Nullable<TimeSpan>ToDouble", TestTimeSpan, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<TimeSpan?, PrimaryColor>("Nullable<TimeSpan>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<TimeSpan?, PrimaryColor>("Nullable<TimeSpan>ToEnum", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<TimeSpan?, float>("Nullable<TimeSpan>ToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<TimeSpan?, float>("Nullable<TimeSpan>ToFloat", TestTimeSpan, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<TimeSpan?, Guid>("Nullable<TimeSpan>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<TimeSpan?, Guid>("Nullable<TimeSpan>ToGuid", TestTimeSpan, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<TimeSpan?, int>("Nullable<TimeSpan>ToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<TimeSpan?, int>("Nullable<TimeSpan>ToInt", TestTimeSpan, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<TimeSpan?, long>("Nullable<TimeSpan>ToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<TimeSpan?, long>("Nullable<TimeSpan>ToLong", TestTimeSpan, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<TimeSpan?, sbyte>("Nullable<TimeSpan>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<TimeSpan?, sbyte>("Nullable<TimeSpan>ToSByte", TestTimeSpan, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<TimeSpan?, short>("Nullable<TimeSpan>ToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<TimeSpan?, short>("Nullable<TimeSpan>ToShort", TestTimeSpan, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToString", TestTimeSpan, ConvertResult.Success, TestTimeSpanString),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormat", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormat, FormatTimeSpanContext),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormatAndFormatProvider", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormatAndFormatProvider, FormatAndFormatProviderTimeSpanContext),
                            new TryConvertGenericTest<TimeSpan?, TimeSpan>("Nullable<TimeSpan>ToTimeSpan", null, ConvertResult.Success, default(TimeSpan)),
                            new TryConvertGenericTest<TimeSpan?, TimeSpan>("Nullable<TimeSpan>ToTimeSpan", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<TimeSpan?, Type>("Nullable<TimeSpan>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<TimeSpan?, Type>("Nullable<TimeSpan>ToType", TestTimeSpan, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<TimeSpan?, uint>("Nullable<TimeSpan>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<TimeSpan?, uint>("Nullable<TimeSpan>ToUInt", TestTimeSpan, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<TimeSpan?, ulong>("Nullable<TimeSpan>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<TimeSpan?, ulong>("Nullable<TimeSpan>ToULong", TestTimeSpan, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<TimeSpan?, Uri>("Nullable<TimeSpan>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<TimeSpan?, Uri>("Nullable<TimeSpan>ToUri", TestTimeSpan, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<TimeSpan?, ushort>("Nullable<TimeSpan>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<TimeSpan?, ushort>("Nullable<TimeSpan>ToUShort", TestTimeSpan, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<TimeSpan?, bool?>("Nullable<TimeSpan>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<TimeSpan?, bool?>("Nullable<TimeSpan>ToNullable<Bool>", TestTimeSpan, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<TimeSpan?, byte?>("Nullable<TimeSpan>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<TimeSpan?, byte?>("Nullable<TimeSpan>ToNullable<Byte>", TestTimeSpan, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<TimeSpan?, char?>("Nullable<TimeSpan>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<TimeSpan?, char?>("Nullable<TimeSpan>ToNullable<Char>", TestTimeSpan, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<TimeSpan?, DateTime?>("Nullable<TimeSpan>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<TimeSpan?, DateTime?>("Nullable<TimeSpan>ToNullable<DateTime>", TestTimeSpan, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<TimeSpan?, DateTimeOffset?>("Nullable<TimeSpan>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<TimeSpan?, DateTimeOffset?>("Nullable<TimeSpan>ToNullable<DateTimeOffset>", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<TimeSpan?, decimal?>("Nullable<TimeSpan>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<TimeSpan?, decimal?>("Nullable<TimeSpan>ToNullable<Decimal>", TestTimeSpan, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<TimeSpan?, double?>("Nullable<TimeSpan>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<TimeSpan?, double?>("Nullable<TimeSpan>ToNullable<Double>", TestTimeSpan, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<TimeSpan?, PrimaryColor?>("Nullable<TimeSpan>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<TimeSpan?, PrimaryColor?>("Nullable<TimeSpan>ToNullable<Enum>", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<TimeSpan?, float?>("Nullable<TimeSpan>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<TimeSpan?, float?>("Nullable<TimeSpan>ToNullable<Float>", TestTimeSpan, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<TimeSpan?, Guid?>("Nullable<TimeSpan>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<TimeSpan?, Guid?>("Nullable<TimeSpan>ToNullable<Guid>", TestTimeSpan, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<TimeSpan?, int?>("Nullable<TimeSpan>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<TimeSpan?, int?>("Nullable<TimeSpan>ToNullable<Int>", TestTimeSpan, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<TimeSpan?, long?>("Nullable<TimeSpan>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<TimeSpan?, long?>("Nullable<TimeSpan>ToNullable<Long>", TestTimeSpan, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<TimeSpan?, sbyte?>("Nullable<TimeSpan>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<TimeSpan?, sbyte?>("Nullable<TimeSpan>ToNullable<SByte>", TestTimeSpan, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<TimeSpan?, short?>("Nullable<TimeSpan>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<TimeSpan?, short?>("Nullable<TimeSpan>ToNullable<Short>", TestTimeSpan, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<TimeSpan?, TimeSpan?>("Nullable<TimeSpan>ToNullable<TimeSpan>", null, ConvertResult.Success, new TimeSpan?()),
                            new TryConvertGenericTest<TimeSpan?, TimeSpan?>("Nullable<TimeSpan>ToNullable<TimeSpan>", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<TimeSpan?, uint?>("Nullable<TimeSpan>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<TimeSpan?, uint?>("Nullable<TimeSpan>ToNullable<UInt>", TestTimeSpan, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<TimeSpan?, ulong?>("Nullable<TimeSpan>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<TimeSpan?, ulong?>("Nullable<TimeSpan>ToNullable<ULong>", TestTimeSpan, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<TimeSpan?, ushort?>("Nullable<TimeSpan>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<TimeSpan?, ushort?>("Nullable<TimeSpan>ToNullable<UShort>", TestTimeSpan, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<TimeSpan?, IInterface>("Nullable<TimeSpan>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<TimeSpan?, IInterface>("Nullable<TimeSpan>ToInterface", TestTimeSpan, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<TimeSpan?, BaseClass>("Nullable<TimeSpan>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<TimeSpan?, BaseClass>("Nullable<TimeSpan>ToBaseClass", TestTimeSpan, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<TimeSpan?, DerivedClass>("Nullable<TimeSpan>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<TimeSpan?, DerivedClass>("Nullable<TimeSpan>ToDerivedClass", TestTimeSpan, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
