// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableDecimalConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableDecimalConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Decimal>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<decimal?, bool>("Nullable<Decimal>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<decimal?, bool>("Nullable<Decimal>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<decimal?, byte>("Nullable<Decimal>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<decimal?, byte>("Nullable<Decimal>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, byte[]>("Nullable<Decimal>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<decimal?, byte[]>("Nullable<Decimal>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<decimal?, char>("Nullable<Decimal>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<decimal?, char>("Nullable<Decimal>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<decimal?, DateTime>("Nullable<Decimal>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<decimal?, DateTime>("Nullable<Decimal>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<decimal?, DateTimeOffset>("Nullable<Decimal>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<decimal?, DateTimeOffset>("Nullable<Decimal>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<decimal?, decimal>("Nullable<Decimal>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<decimal?, decimal>("Nullable<Decimal>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, double>("Nullable<Decimal>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<decimal?, double>("Nullable<Decimal>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, PrimaryColor>("Nullable<Decimal>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<decimal?, PrimaryColor>("Nullable<Decimal>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<decimal?, float>("Nullable<Decimal>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<decimal?, float>("Nullable<Decimal>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, Guid>("Nullable<Decimal>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<decimal?, Guid>("Nullable<Decimal>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<decimal?, int>("Nullable<Decimal>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<decimal?, int>("Nullable<Decimal>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, long>("Nullable<Decimal>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<decimal?, long>("Nullable<Decimal>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, sbyte>("Nullable<Decimal>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<decimal?, sbyte>("Nullable<Decimal>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, short>("Nullable<Decimal>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<decimal?, short>("Nullable<Decimal>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, string>("Nullable<Decimal>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<decimal?, string>("Nullable<Decimal>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<decimal?, TimeSpan>("Nullable<Decimal>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<decimal?, TimeSpan>("Nullable<Decimal>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<decimal?, Type>("Nullable<Decimal>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<decimal?, Type>("Nullable<Decimal>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<decimal?, uint>("Nullable<Decimal>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<decimal?, uint>("Nullable<Decimal>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, ulong>("Nullable<Decimal>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<decimal?, ulong>("Nullable<Decimal>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, Uri>("Nullable<Decimal>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<decimal?, Uri>("Nullable<Decimal>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<decimal?, ushort>("Nullable<Decimal>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<decimal?, ushort>("Nullable<Decimal>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<decimal?, bool?>("Nullable<Decimal>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<decimal?, bool?>("Nullable<Decimal>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<decimal?, byte?>("Nullable<Decimal>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<decimal?, byte?>("Nullable<Decimal>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, char?>("Nullable<Decimal>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<decimal?, char?>("Nullable<Decimal>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<decimal?, DateTime?>("Nullable<Decimal>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<decimal?, DateTime?>("Nullable<Decimal>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<decimal?, DateTimeOffset?>("Nullable<Decimal>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<decimal?, DateTimeOffset?>("Nullable<Decimal>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<decimal?, decimal?>("Nullable<Decimal>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<decimal?, decimal?>("Nullable<Decimal>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, double?>("Nullable<Decimal>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<decimal?, double?>("Nullable<Decimal>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, PrimaryColor?>("Nullable<Decimal>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<decimal?, PrimaryColor?>("Nullable<Decimal>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<decimal?, float?>("Nullable<Decimal>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<decimal?, float?>("Nullable<Decimal>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, Guid?>("Nullable<Decimal>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<decimal?, Guid?>("Nullable<Decimal>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<decimal?, int?>("Nullable<Decimal>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<decimal?, int?>("Nullable<Decimal>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, long?>("Nullable<Decimal>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<decimal?, long?>("Nullable<Decimal>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, sbyte?>("Nullable<Decimal>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<decimal?, sbyte?>("Nullable<Decimal>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, short?>("Nullable<Decimal>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<decimal?, short?>("Nullable<Decimal>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, TimeSpan?>("Nullable<Decimal>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<decimal?, TimeSpan?>("Nullable<Decimal>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<decimal?, uint?>("Nullable<Decimal>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<decimal?, uint?>("Nullable<Decimal>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, ulong?>("Nullable<Decimal>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<decimal?, ulong?>("Nullable<Decimal>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, ushort?>("Nullable<Decimal>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<decimal?, ushort?>("Nullable<Decimal>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<decimal?, IInterface>("Nullable<Decimal>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<decimal?, IInterface>("Nullable<Decimal>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<decimal?, BaseClass>("Nullable<Decimal>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<decimal?, BaseClass>("Nullable<Decimal>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<decimal?, DerivedClass>("Nullable<Decimal>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<decimal?, DerivedClass>("Nullable<Decimal>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Decimal>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<decimal?, bool>("Nullable<Decimal>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<decimal?, bool>("Nullable<Decimal>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<decimal?, byte>("Nullable<Decimal>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<decimal?, byte>("Nullable<Decimal>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, byte[]>("Nullable<Decimal>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<decimal?, byte[]>("Nullable<Decimal>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<decimal?, char>("Nullable<Decimal>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<decimal?, char>("Nullable<Decimal>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<decimal?, DateTime>("Nullable<Decimal>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<decimal?, DateTime>("Nullable<Decimal>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<decimal?, DateTimeOffset>("Nullable<Decimal>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<decimal?, DateTimeOffset>("Nullable<Decimal>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<decimal?, decimal>("Nullable<Decimal>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<decimal?, decimal>("Nullable<Decimal>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, double>("Nullable<Decimal>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<decimal?, double>("Nullable<Decimal>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, PrimaryColor>("Nullable<Decimal>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<decimal?, PrimaryColor>("Nullable<Decimal>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<decimal?, float>("Nullable<Decimal>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<decimal?, float>("Nullable<Decimal>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, Guid>("Nullable<Decimal>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<decimal?, Guid>("Nullable<Decimal>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<decimal?, int>("Nullable<Decimal>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<decimal?, int>("Nullable<Decimal>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, long>("Nullable<Decimal>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<decimal?, long>("Nullable<Decimal>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, sbyte>("Nullable<Decimal>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<decimal?, sbyte>("Nullable<Decimal>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, short>("Nullable<Decimal>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<decimal?, short>("Nullable<Decimal>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, string>("Nullable<Decimal>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<decimal?, string>("Nullable<Decimal>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<decimal?, TimeSpan>("Nullable<Decimal>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<decimal?, TimeSpan>("Nullable<Decimal>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<decimal?, Type>("Nullable<Decimal>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<decimal?, Type>("Nullable<Decimal>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<decimal?, uint>("Nullable<Decimal>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<decimal?, uint>("Nullable<Decimal>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, ulong>("Nullable<Decimal>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<decimal?, ulong>("Nullable<Decimal>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, Uri>("Nullable<Decimal>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<decimal?, Uri>("Nullable<Decimal>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<decimal?, ushort>("Nullable<Decimal>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<decimal?, ushort>("Nullable<Decimal>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<decimal?, bool?>("Nullable<Decimal>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<decimal?, bool?>("Nullable<Decimal>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<decimal?, byte?>("Nullable<Decimal>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<decimal?, byte?>("Nullable<Decimal>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, char?>("Nullable<Decimal>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<decimal?, char?>("Nullable<Decimal>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<decimal?, DateTime?>("Nullable<Decimal>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<decimal?, DateTime?>("Nullable<Decimal>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<decimal?, DateTimeOffset?>("Nullable<Decimal>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<decimal?, DateTimeOffset?>("Nullable<Decimal>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<decimal?, decimal?>("Nullable<Decimal>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<decimal?, decimal?>("Nullable<Decimal>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, double?>("Nullable<Decimal>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<decimal?, double?>("Nullable<Decimal>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, PrimaryColor?>("Nullable<Decimal>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<decimal?, PrimaryColor?>("Nullable<Decimal>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<decimal?, float?>("Nullable<Decimal>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<decimal?, float?>("Nullable<Decimal>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, Guid?>("Nullable<Decimal>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<decimal?, Guid?>("Nullable<Decimal>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<decimal?, int?>("Nullable<Decimal>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<decimal?, int?>("Nullable<Decimal>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, long?>("Nullable<Decimal>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<decimal?, long?>("Nullable<Decimal>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, sbyte?>("Nullable<Decimal>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<decimal?, sbyte?>("Nullable<Decimal>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, short?>("Nullable<Decimal>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<decimal?, short?>("Nullable<Decimal>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, TimeSpan?>("Nullable<Decimal>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<decimal?, TimeSpan?>("Nullable<Decimal>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<decimal?, uint?>("Nullable<Decimal>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<decimal?, uint?>("Nullable<Decimal>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, ulong?>("Nullable<Decimal>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<decimal?, ulong?>("Nullable<Decimal>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, ushort?>("Nullable<Decimal>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<decimal?, ushort?>("Nullable<Decimal>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<decimal?, IInterface>("Nullable<Decimal>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<decimal?, IInterface>("Nullable<Decimal>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<decimal?, BaseClass>("Nullable<Decimal>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<decimal?, BaseClass>("Nullable<Decimal>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<decimal?, DerivedClass>("Nullable<Decimal>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<decimal?, DerivedClass>("Nullable<Decimal>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
