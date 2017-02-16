// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class NullableFloatConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableFloatConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Float>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<float?, bool>("Nullable<Float>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<float?, bool>("Nullable<Float>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<float?, byte>("Nullable<Float>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<float?, byte>("Nullable<Float>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, byte[]>("Nullable<Float>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<float?, byte[]>("Nullable<Float>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<float?, char>("Nullable<Float>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<float?, char>("Nullable<Float>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<float?, DateTime>("Nullable<Float>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<float?, DateTime>("Nullable<Float>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<float?, DateTimeOffset>("Nullable<Float>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<float?, DateTimeOffset>("Nullable<Float>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<float?, decimal>("Nullable<Float>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<float?, decimal>("Nullable<Float>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, double>("Nullable<Float>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<float?, double>("Nullable<Float>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, PrimaryColor>("Nullable<Float>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<float?, PrimaryColor>("Nullable<Float>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<float?, float>("Nullable<Float>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<float?, float>("Nullable<Float>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, Guid>("Nullable<Float>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<float?, Guid>("Nullable<Float>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<float?, int>("Nullable<Float>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<float?, int>("Nullable<Float>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, long>("Nullable<Float>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<float?, long>("Nullable<Float>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, sbyte>("Nullable<Float>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<float?, sbyte>("Nullable<Float>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, short>("Nullable<Float>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<float?, short>("Nullable<Float>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, string>("Nullable<Float>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<float?, string>("Nullable<Float>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<float?, TimeSpan>("Nullable<Float>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<float?, TimeSpan>("Nullable<Float>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<float?, Type>("Nullable<Float>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<float?, Type>("Nullable<Float>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<float?, uint>("Nullable<Float>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<float?, uint>("Nullable<Float>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, ulong>("Nullable<Float>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<float?, ulong>("Nullable<Float>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, Uri>("Nullable<Float>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<float?, Uri>("Nullable<Float>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<float?, ushort>("Nullable<Float>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<float?, ushort>("Nullable<Float>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<float?, bool?>("Nullable<Float>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<float?, bool?>("Nullable<Float>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<float?, byte?>("Nullable<Float>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<float?, byte?>("Nullable<Float>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, char?>("Nullable<Float>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<float?, char?>("Nullable<Float>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<float?, DateTime?>("Nullable<Float>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<float?, DateTime?>("Nullable<Float>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<float?, DateTimeOffset?>("Nullable<Float>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<float?, DateTimeOffset?>("Nullable<Float>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<float?, decimal?>("Nullable<Float>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<float?, decimal?>("Nullable<Float>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, double?>("Nullable<Float>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<float?, double?>("Nullable<Float>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, PrimaryColor?>("Nullable<Float>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<float?, PrimaryColor?>("Nullable<Float>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<float?, float?>("Nullable<Float>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<float?, float?>("Nullable<Float>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, Guid?>("Nullable<Float>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<float?, Guid?>("Nullable<Float>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<float?, int?>("Nullable<Float>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<float?, int?>("Nullable<Float>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, long?>("Nullable<Float>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<float?, long?>("Nullable<Float>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, sbyte?>("Nullable<Float>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<float?, sbyte?>("Nullable<Float>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, short?>("Nullable<Float>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<float?, short?>("Nullable<Float>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, TimeSpan?>("Nullable<Float>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<float?, TimeSpan?>("Nullable<Float>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<float?, uint?>("Nullable<Float>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<float?, uint?>("Nullable<Float>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, ulong?>("Nullable<Float>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<float?, ulong?>("Nullable<Float>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, ushort?>("Nullable<Float>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<float?, ushort?>("Nullable<Float>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<float?, IInterface>("Nullable<Float>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<float?, IInterface>("Nullable<Float>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<float?, BaseClass>("Nullable<Float>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<float?, BaseClass>("Nullable<Float>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<float?, DerivedClass>("Nullable<Float>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<float?, DerivedClass>("Nullable<Float>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Float>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<float?, bool>("Nullable<Float>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<float?, bool>("Nullable<Float>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<float?, byte>("Nullable<Float>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<float?, byte>("Nullable<Float>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, byte[]>("Nullable<Float>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<float?, byte[]>("Nullable<Float>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<float?, char>("Nullable<Float>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<float?, char>("Nullable<Float>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<float?, DateTime>("Nullable<Float>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<float?, DateTime>("Nullable<Float>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<float?, DateTimeOffset>("Nullable<Float>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<float?, DateTimeOffset>("Nullable<Float>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<float?, decimal>("Nullable<Float>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<float?, decimal>("Nullable<Float>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, double>("Nullable<Float>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<float?, double>("Nullable<Float>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, PrimaryColor>("Nullable<Float>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<float?, PrimaryColor>("Nullable<Float>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<float?, float>("Nullable<Float>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<float?, float>("Nullable<Float>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, Guid>("Nullable<Float>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<float?, Guid>("Nullable<Float>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<float?, int>("Nullable<Float>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<float?, int>("Nullable<Float>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, long>("Nullable<Float>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<float?, long>("Nullable<Float>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, sbyte>("Nullable<Float>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<float?, sbyte>("Nullable<Float>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, short>("Nullable<Float>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<float?, short>("Nullable<Float>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, string>("Nullable<Float>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<float?, string>("Nullable<Float>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<float?, TimeSpan>("Nullable<Float>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<float?, TimeSpan>("Nullable<Float>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<float?, Type>("Nullable<Float>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<float?, Type>("Nullable<Float>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<float?, uint>("Nullable<Float>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<float?, uint>("Nullable<Float>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, ulong>("Nullable<Float>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<float?, ulong>("Nullable<Float>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, Uri>("Nullable<Float>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<float?, Uri>("Nullable<Float>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<float?, ushort>("Nullable<Float>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<float?, ushort>("Nullable<Float>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<float?, bool?>("Nullable<Float>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<float?, bool?>("Nullable<Float>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<float?, byte?>("Nullable<Float>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<float?, byte?>("Nullable<Float>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, char?>("Nullable<Float>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<float?, char?>("Nullable<Float>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<float?, DateTime?>("Nullable<Float>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<float?, DateTime?>("Nullable<Float>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<float?, DateTimeOffset?>("Nullable<Float>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<float?, DateTimeOffset?>("Nullable<Float>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<float?, decimal?>("Nullable<Float>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<float?, decimal?>("Nullable<Float>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, double?>("Nullable<Float>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<float?, double?>("Nullable<Float>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, PrimaryColor?>("Nullable<Float>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<float?, PrimaryColor?>("Nullable<Float>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<float?, float?>("Nullable<Float>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<float?, float?>("Nullable<Float>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, Guid?>("Nullable<Float>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<float?, Guid?>("Nullable<Float>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<float?, int?>("Nullable<Float>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<float?, int?>("Nullable<Float>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, long?>("Nullable<Float>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<float?, long?>("Nullable<Float>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, sbyte?>("Nullable<Float>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<float?, sbyte?>("Nullable<Float>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, short?>("Nullable<Float>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<float?, short?>("Nullable<Float>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, TimeSpan?>("Nullable<Float>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<float?, TimeSpan?>("Nullable<Float>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<float?, uint?>("Nullable<Float>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<float?, uint?>("Nullable<Float>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, ulong?>("Nullable<Float>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<float?, ulong?>("Nullable<Float>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, ushort?>("Nullable<Float>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<float?, ushort?>("Nullable<Float>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<float?, IInterface>("Nullable<Float>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<float?, IInterface>("Nullable<Float>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<float?, BaseClass>("Nullable<Float>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<float?, BaseClass>("Nullable<Float>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<float?, DerivedClass>("Nullable<Float>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<float?, DerivedClass>("Nullable<Float>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
