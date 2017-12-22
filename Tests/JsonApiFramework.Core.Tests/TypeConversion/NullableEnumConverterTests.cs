// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableEnumConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableEnumConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Enum>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<PrimaryColor?, bool>("Nullable<Enum>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<PrimaryColor?, bool>("Nullable<Enum>ToBool", PrimaryColor.Blue, ConvertResult.Success, true),
                            new ConvertGenericTest<PrimaryColor?, byte>("Nullable<Enum>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<PrimaryColor?, byte>("Nullable<Enum>ToByte", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, byte[]>("Nullable<Enum>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<PrimaryColor?, byte[]>("Nullable<Enum>ToByteArray", PrimaryColor.Blue, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<PrimaryColor?, char>("Nullable<Enum>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<PrimaryColor?, char>("Nullable<Enum>ToChar", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new ConvertGenericTest<PrimaryColor?, DateTime>("Nullable<Enum>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<PrimaryColor?, DateTime>("Nullable<Enum>ToDateTime", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<PrimaryColor?, DateTimeOffset>("Nullable<Enum>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<PrimaryColor?, DateTimeOffset>("Nullable<Enum>ToDateTimeOffset", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<PrimaryColor?, decimal>("Nullable<Enum>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<PrimaryColor?, decimal>("Nullable<Enum>ToDecimal", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, double>("Nullable<Enum>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<PrimaryColor?, double>("Nullable<Enum>ToDouble", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, PrimaryColor>("Nullable<Enum>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<PrimaryColor?, PrimaryColor>("Nullable<Enum>ToEnum", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<PrimaryColor?, float>("Nullable<Enum>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<PrimaryColor?, float>("Nullable<Enum>ToFloat", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, Guid>("Nullable<Enum>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<PrimaryColor?, Guid>("Nullable<Enum>ToGuid", PrimaryColor.Blue, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<PrimaryColor?, int>("Nullable<Enum>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<PrimaryColor?, int>("Nullable<Enum>ToInt", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, long>("Nullable<Enum>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<PrimaryColor?, long>("Nullable<Enum>ToLong", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, sbyte>("Nullable<Enum>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<PrimaryColor?, sbyte>("Nullable<Enum>ToSByte", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, short>("Nullable<Enum>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<PrimaryColor?, short>("Nullable<Enum>ToShort", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToString", PrimaryColor.Blue, ConvertResult.Success, TestBlueString),
                            new ConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToStringWithFormat", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinalAsString, FormatEnumContext),
                            new ConvertGenericTest<PrimaryColor?, TimeSpan>("Nullable<Enum>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<PrimaryColor?, TimeSpan>("Nullable<Enum>ToTimeSpan", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<PrimaryColor?, Type>("Nullable<Enum>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<PrimaryColor?, Type>("Nullable<Enum>ToType", PrimaryColor.Blue, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<PrimaryColor?, uint>("Nullable<Enum>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<PrimaryColor?, uint>("Nullable<Enum>ToUInt", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, ulong>("Nullable<Enum>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<PrimaryColor?, ulong>("Nullable<Enum>ToULong", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, Uri>("Nullable<Enum>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<PrimaryColor?, Uri>("Nullable<Enum>ToUri", PrimaryColor.Blue, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<PrimaryColor?, ushort>("Nullable<Enum>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<PrimaryColor?, ushort>("Nullable<Enum>ToUShort", PrimaryColor.Blue, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<PrimaryColor?, bool?>("Nullable<Enum>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<PrimaryColor?, bool?>("Nullable<Enum>ToNullable<Bool>", PrimaryColor.Blue, ConvertResult.Success, true),
                            new ConvertGenericTest<PrimaryColor?, byte?>("Nullable<Enum>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<PrimaryColor?, byte?>("Nullable<Enum>ToNullable<Byte>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, char?>("Nullable<Enum>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<PrimaryColor?, char?>("Nullable<Enum>ToNullable<Char>", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new ConvertGenericTest<PrimaryColor?, DateTime?>("Nullable<Enum>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<PrimaryColor?, DateTime?>("Nullable<Enum>ToNullable<DateTime>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<PrimaryColor?, DateTimeOffset?>("Nullable<Enum>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<PrimaryColor?, DateTimeOffset?>("Nullable<Enum>ToNullable<DateTimeOffset>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<PrimaryColor?, decimal?>("Nullable<Enum>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<PrimaryColor?, decimal?>("Nullable<Enum>ToNullable<Decimal>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, double?>("Nullable<Enum>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<PrimaryColor?, double?>("Nullable<Enum>ToNullable<Double>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, PrimaryColor?>("Nullable<Enum>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<PrimaryColor?, PrimaryColor?>("Nullable<Enum>ToNullable<Enum>", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<PrimaryColor?, float?>("Nullable<Enum>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<PrimaryColor?, float?>("Nullable<Enum>ToNullable<Float>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, Guid?>("Nullable<Enum>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<PrimaryColor?, Guid?>("Nullable<Enum>ToNullable<Guid>", PrimaryColor.Blue, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<PrimaryColor?, int?>("Nullable<Enum>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<PrimaryColor?, int?>("Nullable<Enum>ToNullable<Int>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, long?>("Nullable<Enum>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<PrimaryColor?, long?>("Nullable<Enum>ToNullable<Long>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, sbyte?>("Nullable<Enum>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<PrimaryColor?, sbyte?>("Nullable<Enum>ToNullable<SByte>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, short?>("Nullable<Enum>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<PrimaryColor?, short?>("Nullable<Enum>ToNullable<Short>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, TimeSpan?>("Nullable<Enum>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<PrimaryColor?, TimeSpan?>("Nullable<Enum>ToNullable<TimeSpan>", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<PrimaryColor?, uint?>("Nullable<Enum>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<PrimaryColor?, uint?>("Nullable<Enum>ToNullable<UInt>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, ulong?>("Nullable<Enum>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<PrimaryColor?, ulong?>("Nullable<Enum>ToNullable<ULong>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, ushort?>("Nullable<Enum>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<PrimaryColor?, ushort?>("Nullable<Enum>ToNullable<UShort>", PrimaryColor.Blue, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<PrimaryColor?, IInterface>("Nullable<Enum>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<PrimaryColor?, IInterface>("Nullable<Enum>ToInterface", PrimaryColor.Blue, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<PrimaryColor?, BaseClass>("Nullable<Enum>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<PrimaryColor?, BaseClass>("Nullable<Enum>ToBaseClass", PrimaryColor.Blue, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<PrimaryColor?, DerivedClass>("Nullable<Enum>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<PrimaryColor?, DerivedClass>("Nullable<Enum>ToDerivedClass", PrimaryColor.Blue, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Enum>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<PrimaryColor?, bool>("Nullable<Enum>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<PrimaryColor?, bool>("Nullable<Enum>ToBool", PrimaryColor.Blue, ConvertResult.Success, true),
                            new TryConvertGenericTest<PrimaryColor?, byte>("Nullable<Enum>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<PrimaryColor?, byte>("Nullable<Enum>ToByte", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, byte[]>("Nullable<Enum>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<PrimaryColor?, byte[]>("Nullable<Enum>ToByteArray", PrimaryColor.Blue, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<PrimaryColor?, char>("Nullable<Enum>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<PrimaryColor?, char>("Nullable<Enum>ToChar", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<PrimaryColor?, DateTime>("Nullable<Enum>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<PrimaryColor?, DateTime>("Nullable<Enum>ToDateTime", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<PrimaryColor?, DateTimeOffset>("Nullable<Enum>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<PrimaryColor?, DateTimeOffset>("Nullable<Enum>ToDateTimeOffset", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<PrimaryColor?, decimal>("Nullable<Enum>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<PrimaryColor?, decimal>("Nullable<Enum>ToDecimal", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, double>("Nullable<Enum>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<PrimaryColor?, double>("Nullable<Enum>ToDouble", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, PrimaryColor>("Nullable<Enum>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<PrimaryColor?, PrimaryColor>("Nullable<Enum>ToEnum", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<PrimaryColor?, float>("Nullable<Enum>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<PrimaryColor?, float>("Nullable<Enum>ToFloat", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, Guid>("Nullable<Enum>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<PrimaryColor?, Guid>("Nullable<Enum>ToGuid", PrimaryColor.Blue, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<PrimaryColor?, int>("Nullable<Enum>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<PrimaryColor?, int>("Nullable<Enum>ToInt", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, long>("Nullable<Enum>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<PrimaryColor?, long>("Nullable<Enum>ToLong", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, sbyte>("Nullable<Enum>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<PrimaryColor?, sbyte>("Nullable<Enum>ToSByte", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, short>("Nullable<Enum>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<PrimaryColor?, short>("Nullable<Enum>ToShort", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToString", PrimaryColor.Blue, ConvertResult.Success, TestBlueString),
                            new TryConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToStringWithFormat", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinalAsString, FormatEnumContext),
                            new TryConvertGenericTest<PrimaryColor?, TimeSpan>("Nullable<Enum>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<PrimaryColor?, TimeSpan>("Nullable<Enum>ToTimeSpan", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<PrimaryColor?, Type>("Nullable<Enum>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<PrimaryColor?, Type>("Nullable<Enum>ToType", PrimaryColor.Blue, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<PrimaryColor?, uint>("Nullable<Enum>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<PrimaryColor?, uint>("Nullable<Enum>ToUInt", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, ulong>("Nullable<Enum>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<PrimaryColor?, ulong>("Nullable<Enum>ToULong", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, Uri>("Nullable<Enum>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<PrimaryColor?, Uri>("Nullable<Enum>ToUri", PrimaryColor.Blue, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<PrimaryColor?, ushort>("Nullable<Enum>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<PrimaryColor?, ushort>("Nullable<Enum>ToUShort", PrimaryColor.Blue, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<PrimaryColor?, bool?>("Nullable<Enum>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<PrimaryColor?, bool?>("Nullable<Enum>ToNullable<Bool>", PrimaryColor.Blue, ConvertResult.Success, true),
                            new TryConvertGenericTest<PrimaryColor?, byte?>("Nullable<Enum>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<PrimaryColor?, byte?>("Nullable<Enum>ToNullable<Byte>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, char?>("Nullable<Enum>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<PrimaryColor?, char?>("Nullable<Enum>ToNullable<Char>", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<PrimaryColor?, DateTime?>("Nullable<Enum>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<PrimaryColor?, DateTime?>("Nullable<Enum>ToNullable<DateTime>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<PrimaryColor?, DateTimeOffset?>("Nullable<Enum>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<PrimaryColor?, DateTimeOffset?>("Nullable<Enum>ToNullable<DateTimeOffset>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<PrimaryColor?, decimal?>("Nullable<Enum>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<PrimaryColor?, decimal?>("Nullable<Enum>ToNullable<Decimal>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, double?>("Nullable<Enum>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<PrimaryColor?, double?>("Nullable<Enum>ToNullable<Double>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, PrimaryColor?>("Nullable<Enum>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<PrimaryColor?, PrimaryColor?>("Nullable<Enum>ToNullable<Enum>", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<PrimaryColor?, float?>("Nullable<Enum>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<PrimaryColor?, float?>("Nullable<Enum>ToNullable<Float>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, Guid?>("Nullable<Enum>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<PrimaryColor?, Guid?>("Nullable<Enum>ToNullable<Guid>", PrimaryColor.Blue, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<PrimaryColor?, int?>("Nullable<Enum>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<PrimaryColor?, int?>("Nullable<Enum>ToNullable<Int>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, long?>("Nullable<Enum>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<PrimaryColor?, long?>("Nullable<Enum>ToNullable<Long>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, sbyte?>("Nullable<Enum>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<PrimaryColor?, sbyte?>("Nullable<Enum>ToNullable<SByte>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, short?>("Nullable<Enum>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<PrimaryColor?, short?>("Nullable<Enum>ToNullable<Short>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, TimeSpan?>("Nullable<Enum>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<PrimaryColor?, TimeSpan?>("Nullable<Enum>ToNullable<TimeSpan>", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<PrimaryColor?, uint?>("Nullable<Enum>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<PrimaryColor?, uint?>("Nullable<Enum>ToNullable<UInt>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, ulong?>("Nullable<Enum>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<PrimaryColor?, ulong?>("Nullable<Enum>ToNullable<ULong>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, ushort?>("Nullable<Enum>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<PrimaryColor?, ushort?>("Nullable<Enum>ToNullable<UShort>", PrimaryColor.Blue, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<PrimaryColor?, IInterface>("Nullable<Enum>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<PrimaryColor?, IInterface>("Nullable<Enum>ToInterface", PrimaryColor.Blue, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<PrimaryColor?, BaseClass>("Nullable<Enum>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<PrimaryColor?, BaseClass>("Nullable<Enum>ToBaseClass", PrimaryColor.Blue, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<PrimaryColor?, DerivedClass>("Nullable<Enum>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<PrimaryColor?, DerivedClass>("Nullable<Enum>ToDerivedClass", PrimaryColor.Blue, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
