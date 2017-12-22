// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableULongConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableULongConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<ULong>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<ulong?, bool>("Nullable<ULong>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<ulong?, bool>("Nullable<ULong>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ulong?, byte>("Nullable<ULong>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<ulong?, byte>("Nullable<ULong>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, byte[]>("Nullable<ULong>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ulong?, byte[]>("Nullable<ULong>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ulong?, char>("Nullable<ULong>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<ulong?, char>("Nullable<ULong>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ulong?, DateTime>("Nullable<ULong>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ulong?, DateTime>("Nullable<ULong>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ulong?, DateTimeOffset>("Nullable<ULong>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ulong?, DateTimeOffset>("Nullable<ULong>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ulong?, decimal>("Nullable<ULong>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<ulong?, decimal>("Nullable<ULong>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, double>("Nullable<ULong>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<ulong?, double>("Nullable<ULong>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, PrimaryColor>("Nullable<ULong>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<ulong?, PrimaryColor>("Nullable<ULong>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ulong?, float>("Nullable<ULong>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<ulong?, float>("Nullable<ULong>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, Guid>("Nullable<ULong>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ulong?, Guid>("Nullable<ULong>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ulong?, int>("Nullable<ULong>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<ulong?, int>("Nullable<ULong>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, long>("Nullable<ULong>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<ulong?, long>("Nullable<ULong>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, sbyte>("Nullable<ULong>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<ulong?, sbyte>("Nullable<ULong>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, short>("Nullable<ULong>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<ulong?, short>("Nullable<ULong>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, string>("Nullable<ULong>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<ulong?, string>("Nullable<ULong>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<ulong?, TimeSpan>("Nullable<ULong>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ulong?, TimeSpan>("Nullable<ULong>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ulong?, Type>("Nullable<ULong>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ulong?, Type>("Nullable<ULong>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ulong?, uint>("Nullable<ULong>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<ulong?, uint>("Nullable<ULong>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, ulong>("Nullable<ULong>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<ulong?, ulong>("Nullable<ULong>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, Uri>("Nullable<ULong>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ulong?, Uri>("Nullable<ULong>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ulong?, ushort>("Nullable<ULong>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<ulong?, ushort>("Nullable<ULong>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<ulong?, bool?>("Nullable<ULong>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<ulong?, bool?>("Nullable<ULong>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ulong?, byte?>("Nullable<ULong>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<ulong?, byte?>("Nullable<ULong>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, char?>("Nullable<ULong>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<ulong?, char?>("Nullable<ULong>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ulong?, DateTime?>("Nullable<ULong>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ulong?, DateTime?>("Nullable<ULong>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ulong?, DateTimeOffset?>("Nullable<ULong>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ulong?, DateTimeOffset?>("Nullable<ULong>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ulong?, decimal?>("Nullable<ULong>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<ulong?, decimal?>("Nullable<ULong>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, double?>("Nullable<ULong>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<ulong?, double?>("Nullable<ULong>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, PrimaryColor?>("Nullable<ULong>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<ulong?, PrimaryColor?>("Nullable<ULong>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ulong?, float?>("Nullable<ULong>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<ulong?, float?>("Nullable<ULong>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, Guid?>("Nullable<ULong>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ulong?, Guid?>("Nullable<ULong>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ulong?, int?>("Nullable<ULong>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<ulong?, int?>("Nullable<ULong>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, long?>("Nullable<ULong>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<ulong?, long?>("Nullable<ULong>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, sbyte?>("Nullable<ULong>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<ulong?, sbyte?>("Nullable<ULong>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, short?>("Nullable<ULong>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<ulong?, short?>("Nullable<ULong>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, TimeSpan?>("Nullable<ULong>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ulong?, TimeSpan?>("Nullable<ULong>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ulong?, uint?>("Nullable<ULong>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<ulong?, uint?>("Nullable<ULong>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, ulong?>("Nullable<ULong>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<ulong?, ulong?>("Nullable<ULong>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, ushort?>("Nullable<ULong>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<ulong?, ushort?>("Nullable<ULong>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<ulong?, IInterface>("Nullable<ULong>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ulong?, IInterface>("Nullable<ULong>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ulong?, BaseClass>("Nullable<ULong>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ulong?, BaseClass>("Nullable<ULong>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ulong?, DerivedClass>("Nullable<ULong>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<ulong?, DerivedClass>("Nullable<ULong>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<ULong>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<ulong?, bool>("Nullable<ULong>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<ulong?, bool>("Nullable<ULong>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ulong?, byte>("Nullable<ULong>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<ulong?, byte>("Nullable<ULong>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, byte[]>("Nullable<ULong>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ulong?, byte[]>("Nullable<ULong>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ulong?, char>("Nullable<ULong>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<ulong?, char>("Nullable<ULong>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ulong?, DateTime>("Nullable<ULong>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ulong?, DateTime>("Nullable<ULong>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ulong?, DateTimeOffset>("Nullable<ULong>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ulong?, DateTimeOffset>("Nullable<ULong>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ulong?, decimal>("Nullable<ULong>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<ulong?, decimal>("Nullable<ULong>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, double>("Nullable<ULong>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<ulong?, double>("Nullable<ULong>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, PrimaryColor>("Nullable<ULong>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<ulong?, PrimaryColor>("Nullable<ULong>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ulong?, float>("Nullable<ULong>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<ulong?, float>("Nullable<ULong>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, Guid>("Nullable<ULong>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ulong?, Guid>("Nullable<ULong>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ulong?, int>("Nullable<ULong>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<ulong?, int>("Nullable<ULong>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, long>("Nullable<ULong>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<ulong?, long>("Nullable<ULong>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, sbyte>("Nullable<ULong>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<ulong?, sbyte>("Nullable<ULong>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, short>("Nullable<ULong>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<ulong?, short>("Nullable<ULong>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, string>("Nullable<ULong>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<ulong?, string>("Nullable<ULong>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<ulong?, TimeSpan>("Nullable<ULong>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ulong?, TimeSpan>("Nullable<ULong>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ulong?, Type>("Nullable<ULong>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ulong?, Type>("Nullable<ULong>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ulong?, uint>("Nullable<ULong>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<ulong?, uint>("Nullable<ULong>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, ulong>("Nullable<ULong>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<ulong?, ulong>("Nullable<ULong>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, Uri>("Nullable<ULong>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ulong?, Uri>("Nullable<ULong>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ulong?, ushort>("Nullable<ULong>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<ulong?, ushort>("Nullable<ULong>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<ulong?, bool?>("Nullable<ULong>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<ulong?, bool?>("Nullable<ULong>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ulong?, byte?>("Nullable<ULong>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<ulong?, byte?>("Nullable<ULong>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, char?>("Nullable<ULong>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<ulong?, char?>("Nullable<ULong>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ulong?, DateTime?>("Nullable<ULong>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ulong?, DateTime?>("Nullable<ULong>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ulong?, DateTimeOffset?>("Nullable<ULong>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ulong?, DateTimeOffset?>("Nullable<ULong>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ulong?, decimal?>("Nullable<ULong>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<ulong?, decimal?>("Nullable<ULong>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, double?>("Nullable<ULong>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<ulong?, double?>("Nullable<ULong>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, PrimaryColor?>("Nullable<ULong>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<ulong?, PrimaryColor?>("Nullable<ULong>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ulong?, float?>("Nullable<ULong>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<ulong?, float?>("Nullable<ULong>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, Guid?>("Nullable<ULong>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ulong?, Guid?>("Nullable<ULong>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ulong?, int?>("Nullable<ULong>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<ulong?, int?>("Nullable<ULong>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, long?>("Nullable<ULong>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<ulong?, long?>("Nullable<ULong>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, sbyte?>("Nullable<ULong>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<ulong?, sbyte?>("Nullable<ULong>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, short?>("Nullable<ULong>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<ulong?, short?>("Nullable<ULong>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, TimeSpan?>("Nullable<ULong>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ulong?, TimeSpan?>("Nullable<ULong>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ulong?, uint?>("Nullable<ULong>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<ulong?, uint?>("Nullable<ULong>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, ulong?>("Nullable<ULong>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<ulong?, ulong?>("Nullable<ULong>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, ushort?>("Nullable<ULong>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<ulong?, ushort?>("Nullable<ULong>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<ulong?, IInterface>("Nullable<ULong>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ulong?, IInterface>("Nullable<ULong>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ulong?, BaseClass>("Nullable<ULong>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ulong?, BaseClass>("Nullable<ULong>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ulong?, DerivedClass>("Nullable<ULong>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<ulong?, DerivedClass>("Nullable<ULong>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
