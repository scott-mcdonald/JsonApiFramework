// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableByteConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableByteConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Byte>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<byte?, bool>("Nullable<Byte>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<byte?, bool>("Nullable<Byte>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<byte?, byte>("Nullable<Byte>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<byte?, byte>("Nullable<Byte>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, byte[]>("Nullable<Byte>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<byte?, byte[]>("Nullable<Byte>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<byte?, char>("Nullable<Byte>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<byte?, char>("Nullable<Byte>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<byte?, DateTime>("Nullable<Byte>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte?, DateTime>("Nullable<Byte>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte?, DateTimeOffset>("Nullable<Byte>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<byte?, DateTimeOffset>("Nullable<Byte>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<byte?, decimal>("Nullable<Byte>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<byte?, decimal>("Nullable<Byte>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, double>("Nullable<Byte>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<byte?, double>("Nullable<Byte>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, PrimaryColor>("Nullable<Byte>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<byte?, PrimaryColor>("Nullable<Byte>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<byte?, float>("Nullable<Byte>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<byte?, float>("Nullable<Byte>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, Guid>("Nullable<Byte>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<byte?, Guid>("Nullable<Byte>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<byte?, int>("Nullable<Byte>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<byte?, int>("Nullable<Byte>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, long>("Nullable<Byte>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<byte?, long>("Nullable<Byte>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, sbyte>("Nullable<Byte>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<byte?, sbyte>("Nullable<Byte>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, short>("Nullable<Byte>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<byte?, short>("Nullable<Byte>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, string>("Nullable<Byte>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<byte?, string>("Nullable<Byte>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<byte?, TimeSpan>("Nullable<Byte>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<byte?, TimeSpan>("Nullable<Byte>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<byte?, Type>("Nullable<Byte>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<byte?, Type>("Nullable<Byte>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<byte?, uint>("Nullable<Byte>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<byte?, uint>("Nullable<Byte>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, ulong>("Nullable<Byte>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<byte?, ulong>("Nullable<Byte>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, Uri>("Nullable<Byte>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<byte?, Uri>("Nullable<Byte>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<byte?, ushort>("Nullable<Byte>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<byte?, ushort>("Nullable<Byte>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<byte?, bool?>("Nullable<Byte>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<byte?, bool?>("Nullable<Byte>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<byte?, byte?>("Nullable<Byte>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<byte?, byte?>("Nullable<Byte>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, char?>("Nullable<Byte>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<byte?, char?>("Nullable<Byte>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<byte?, DateTime?>("Nullable<Byte>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<byte?, DateTime?>("Nullable<Byte>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<byte?, DateTimeOffset?>("Nullable<Byte>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<byte?, DateTimeOffset?>("Nullable<Byte>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<byte?, decimal?>("Nullable<Byte>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<byte?, decimal?>("Nullable<Byte>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, double?>("Nullable<Byte>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<byte?, double?>("Nullable<Byte>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, PrimaryColor?>("Nullable<Byte>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<byte?, PrimaryColor?>("Nullable<Byte>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<byte?, float?>("Nullable<Byte>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<byte?, float?>("Nullable<Byte>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, Guid?>("Nullable<Byte>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<byte?, Guid?>("Nullable<Byte>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<byte?, int?>("Nullable<Byte>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<byte?, int?>("Nullable<Byte>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, long?>("Nullable<Byte>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<byte?, long?>("Nullable<Byte>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, sbyte?>("Nullable<Byte>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<byte?, sbyte?>("Nullable<Byte>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, short?>("Nullable<Byte>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<byte?, short?>("Nullable<Byte>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, TimeSpan?>("Nullable<Byte>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<byte?, TimeSpan?>("Nullable<Byte>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<byte?, uint?>("Nullable<Byte>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<byte?, uint?>("Nullable<Byte>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, ulong?>("Nullable<Byte>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<byte?, ulong?>("Nullable<Byte>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, ushort?>("Nullable<Byte>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<byte?, ushort?>("Nullable<Byte>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<byte?, IInterface>("Nullable<Byte>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<byte?, IInterface>("Nullable<Byte>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<byte?, BaseClass>("Nullable<Byte>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<byte?, BaseClass>("Nullable<Byte>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<byte?, DerivedClass>("Nullable<Byte>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<byte?, DerivedClass>("Nullable<Byte>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Byte>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<byte?, bool>("Nullable<Byte>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<byte?, bool>("Nullable<Byte>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<byte?, byte>("Nullable<Byte>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<byte?, byte>("Nullable<Byte>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, byte[]>("Nullable<Byte>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<byte?, byte[]>("Nullable<Byte>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<byte?, char>("Nullable<Byte>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<byte?, char>("Nullable<Byte>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<byte?, DateTime>("Nullable<Byte>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte?, DateTime>("Nullable<Byte>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte?, DateTimeOffset>("Nullable<Byte>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<byte?, DateTimeOffset>("Nullable<Byte>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<byte?, decimal>("Nullable<Byte>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<byte?, decimal>("Nullable<Byte>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, double>("Nullable<Byte>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<byte?, double>("Nullable<Byte>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, PrimaryColor>("Nullable<Byte>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<byte?, PrimaryColor>("Nullable<Byte>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<byte?, float>("Nullable<Byte>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<byte?, float>("Nullable<Byte>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, Guid>("Nullable<Byte>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<byte?, Guid>("Nullable<Byte>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<byte?, int>("Nullable<Byte>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<byte?, int>("Nullable<Byte>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, long>("Nullable<Byte>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<byte?, long>("Nullable<Byte>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, sbyte>("Nullable<Byte>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<byte?, sbyte>("Nullable<Byte>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, short>("Nullable<Byte>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<byte?, short>("Nullable<Byte>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, string>("Nullable<Byte>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<byte?, string>("Nullable<Byte>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<byte?, TimeSpan>("Nullable<Byte>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<byte?, TimeSpan>("Nullable<Byte>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<byte?, Type>("Nullable<Byte>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<byte?, Type>("Nullable<Byte>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<byte?, uint>("Nullable<Byte>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<byte?, uint>("Nullable<Byte>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, ulong>("Nullable<Byte>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<byte?, ulong>("Nullable<Byte>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, Uri>("Nullable<Byte>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<byte?, Uri>("Nullable<Byte>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<byte?, ushort>("Nullable<Byte>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<byte?, ushort>("Nullable<Byte>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<byte?, bool?>("Nullable<Byte>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<byte?, bool?>("Nullable<Byte>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<byte?, byte?>("Nullable<Byte>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<byte?, byte?>("Nullable<Byte>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, char?>("Nullable<Byte>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<byte?, char?>("Nullable<Byte>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<byte?, DateTime?>("Nullable<Byte>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<byte?, DateTime?>("Nullable<Byte>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<byte?, DateTimeOffset?>("Nullable<Byte>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<byte?, DateTimeOffset?>("Nullable<Byte>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<byte?, decimal?>("Nullable<Byte>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<byte?, decimal?>("Nullable<Byte>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, double?>("Nullable<Byte>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<byte?, double?>("Nullable<Byte>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, PrimaryColor?>("Nullable<Byte>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<byte?, PrimaryColor?>("Nullable<Byte>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<byte?, float?>("Nullable<Byte>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<byte?, float?>("Nullable<Byte>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, Guid?>("Nullable<Byte>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<byte?, Guid?>("Nullable<Byte>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<byte?, int?>("Nullable<Byte>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<byte?, int?>("Nullable<Byte>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, long?>("Nullable<Byte>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<byte?, long?>("Nullable<Byte>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, sbyte?>("Nullable<Byte>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<byte?, sbyte?>("Nullable<Byte>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, short?>("Nullable<Byte>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<byte?, short?>("Nullable<Byte>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, TimeSpan?>("Nullable<Byte>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<byte?, TimeSpan?>("Nullable<Byte>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<byte?, uint?>("Nullable<Byte>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<byte?, uint?>("Nullable<Byte>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, ulong?>("Nullable<Byte>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<byte?, ulong?>("Nullable<Byte>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, ushort?>("Nullable<Byte>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<byte?, ushort?>("Nullable<Byte>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<byte?, IInterface>("Nullable<Byte>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<byte?, IInterface>("Nullable<Byte>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<byte?, BaseClass>("Nullable<Byte>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<byte?, BaseClass>("Nullable<Byte>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<byte?, DerivedClass>("Nullable<Byte>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<byte?, DerivedClass>("Nullable<Byte>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
