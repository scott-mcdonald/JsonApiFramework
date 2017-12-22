// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableUIntConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableUIntConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<UInt>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<uint?, bool>("Nullable<UInt>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<uint?, bool>("Nullable<UInt>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<uint?, byte>("Nullable<UInt>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<uint?, byte>("Nullable<UInt>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, byte[]>("Nullable<UInt>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<uint?, byte[]>("Nullable<UInt>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<uint?, char>("Nullable<UInt>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<uint?, char>("Nullable<UInt>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<uint?, DateTime>("Nullable<UInt>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<uint?, DateTime>("Nullable<UInt>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<uint?, DateTimeOffset>("Nullable<UInt>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<uint?, DateTimeOffset>("Nullable<UInt>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<uint?, decimal>("Nullable<UInt>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<uint?, decimal>("Nullable<UInt>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, double>("Nullable<UInt>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<uint?, double>("Nullable<UInt>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, PrimaryColor>("Nullable<UInt>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<uint?, PrimaryColor>("Nullable<UInt>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<uint?, float>("Nullable<UInt>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<uint?, float>("Nullable<UInt>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, Guid>("Nullable<UInt>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<uint?, Guid>("Nullable<UInt>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<uint?, int>("Nullable<UInt>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<uint?, int>("Nullable<UInt>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, long>("Nullable<UInt>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<uint?, long>("Nullable<UInt>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, sbyte>("Nullable<UInt>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<uint?, sbyte>("Nullable<UInt>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, short>("Nullable<UInt>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<uint?, short>("Nullable<UInt>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, string>("Nullable<UInt>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<uint?, string>("Nullable<UInt>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<uint?, TimeSpan>("Nullable<UInt>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<uint?, TimeSpan>("Nullable<UInt>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<uint?, Type>("Nullable<UInt>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<uint?, Type>("Nullable<UInt>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<uint?, uint>("Nullable<UInt>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<uint?, uint>("Nullable<UInt>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, ulong>("Nullable<UInt>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<uint?, ulong>("Nullable<UInt>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, Uri>("Nullable<UInt>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<uint?, Uri>("Nullable<UInt>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<uint?, ushort>("Nullable<UInt>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<uint?, ushort>("Nullable<UInt>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<uint?, bool?>("Nullable<UInt>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<uint?, bool?>("Nullable<UInt>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<uint?, byte?>("Nullable<UInt>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<uint?, byte?>("Nullable<UInt>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, char?>("Nullable<UInt>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<uint?, char?>("Nullable<UInt>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<uint?, DateTime?>("Nullable<UInt>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<uint?, DateTime?>("Nullable<UInt>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<uint?, DateTimeOffset?>("Nullable<UInt>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<uint?, DateTimeOffset?>("Nullable<UInt>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<uint?, decimal?>("Nullable<UInt>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<uint?, decimal?>("Nullable<UInt>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, double?>("Nullable<UInt>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<uint?, double?>("Nullable<UInt>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, PrimaryColor?>("Nullable<UInt>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<uint?, PrimaryColor?>("Nullable<UInt>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<uint?, float?>("Nullable<UInt>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<uint?, float?>("Nullable<UInt>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, Guid?>("Nullable<UInt>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<uint?, Guid?>("Nullable<UInt>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<uint?, int?>("Nullable<UInt>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<uint?, int?>("Nullable<UInt>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, long?>("Nullable<UInt>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<uint?, long?>("Nullable<UInt>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, sbyte?>("Nullable<UInt>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<uint?, sbyte?>("Nullable<UInt>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, short?>("Nullable<UInt>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<uint?, short?>("Nullable<UInt>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, TimeSpan?>("Nullable<UInt>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<uint?, TimeSpan?>("Nullable<UInt>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<uint?, uint?>("Nullable<UInt>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<uint?, uint?>("Nullable<UInt>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, ulong?>("Nullable<UInt>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<uint?, ulong?>("Nullable<UInt>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, ushort?>("Nullable<UInt>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<uint?, ushort?>("Nullable<UInt>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<uint?, IInterface>("Nullable<UInt>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<uint?, IInterface>("Nullable<UInt>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<uint?, BaseClass>("Nullable<UInt>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<uint?, BaseClass>("Nullable<UInt>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<uint?, DerivedClass>("Nullable<UInt>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<uint?, DerivedClass>("Nullable<UInt>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<UInt>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<uint?, bool>("Nullable<UInt>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<uint?, bool>("Nullable<UInt>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<uint?, byte>("Nullable<UInt>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<uint?, byte>("Nullable<UInt>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, byte[]>("Nullable<UInt>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<uint?, byte[]>("Nullable<UInt>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<uint?, char>("Nullable<UInt>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<uint?, char>("Nullable<UInt>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<uint?, DateTime>("Nullable<UInt>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<uint?, DateTime>("Nullable<UInt>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<uint?, DateTimeOffset>("Nullable<UInt>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<uint?, DateTimeOffset>("Nullable<UInt>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<uint?, decimal>("Nullable<UInt>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<uint?, decimal>("Nullable<UInt>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, double>("Nullable<UInt>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<uint?, double>("Nullable<UInt>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, PrimaryColor>("Nullable<UInt>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<uint?, PrimaryColor>("Nullable<UInt>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<uint?, float>("Nullable<UInt>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<uint?, float>("Nullable<UInt>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, Guid>("Nullable<UInt>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<uint?, Guid>("Nullable<UInt>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<uint?, int>("Nullable<UInt>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<uint?, int>("Nullable<UInt>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, long>("Nullable<UInt>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<uint?, long>("Nullable<UInt>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, sbyte>("Nullable<UInt>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<uint?, sbyte>("Nullable<UInt>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, short>("Nullable<UInt>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<uint?, short>("Nullable<UInt>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, string>("Nullable<UInt>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<uint?, string>("Nullable<UInt>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<uint?, TimeSpan>("Nullable<UInt>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<uint?, TimeSpan>("Nullable<UInt>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<uint?, Type>("Nullable<UInt>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<uint?, Type>("Nullable<UInt>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<uint?, uint>("Nullable<UInt>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<uint?, uint>("Nullable<UInt>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, ulong>("Nullable<UInt>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<uint?, ulong>("Nullable<UInt>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, Uri>("Nullable<UInt>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<uint?, Uri>("Nullable<UInt>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<uint?, ushort>("Nullable<UInt>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<uint?, ushort>("Nullable<UInt>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<uint?, bool?>("Nullable<UInt>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<uint?, bool?>("Nullable<UInt>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<uint?, byte?>("Nullable<UInt>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<uint?, byte?>("Nullable<UInt>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, char?>("Nullable<UInt>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<uint?, char?>("Nullable<UInt>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<uint?, DateTime?>("Nullable<UInt>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<uint?, DateTime?>("Nullable<UInt>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<uint?, DateTimeOffset?>("Nullable<UInt>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<uint?, DateTimeOffset?>("Nullable<UInt>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<uint?, decimal?>("Nullable<UInt>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<uint?, decimal?>("Nullable<UInt>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, double?>("Nullable<UInt>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<uint?, double?>("Nullable<UInt>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, PrimaryColor?>("Nullable<UInt>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<uint?, PrimaryColor?>("Nullable<UInt>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<uint?, float?>("Nullable<UInt>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<uint?, float?>("Nullable<UInt>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, Guid?>("Nullable<UInt>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<uint?, Guid?>("Nullable<UInt>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<uint?, int?>("Nullable<UInt>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<uint?, int?>("Nullable<UInt>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, long?>("Nullable<UInt>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<uint?, long?>("Nullable<UInt>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, sbyte?>("Nullable<UInt>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<uint?, sbyte?>("Nullable<UInt>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, short?>("Nullable<UInt>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<uint?, short?>("Nullable<UInt>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, TimeSpan?>("Nullable<UInt>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<uint?, TimeSpan?>("Nullable<UInt>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<uint?, uint?>("Nullable<UInt>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<uint?, uint?>("Nullable<UInt>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, ulong?>("Nullable<UInt>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<uint?, ulong?>("Nullable<UInt>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, ushort?>("Nullable<UInt>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<uint?, ushort?>("Nullable<UInt>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<uint?, IInterface>("Nullable<UInt>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<uint?, IInterface>("Nullable<UInt>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<uint?, BaseClass>("Nullable<UInt>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<uint?, BaseClass>("Nullable<UInt>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<uint?, DerivedClass>("Nullable<UInt>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<uint?, DerivedClass>("Nullable<UInt>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
