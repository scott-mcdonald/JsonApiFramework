// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class NullableDoubleConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableDoubleConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Double>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<double?, bool>("Nullable<Double>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<double?, bool>("Nullable<Double>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<double?, byte>("Nullable<Double>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<double?, byte>("Nullable<Double>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, byte[]>("Nullable<Double>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<double?, byte[]>("Nullable<Double>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<double?, char>("Nullable<Double>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<double?, char>("Nullable<Double>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<double?, DateTime>("Nullable<Double>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<double?, DateTime>("Nullable<Double>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<double?, DateTimeOffset>("Nullable<Double>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<double?, DateTimeOffset>("Nullable<Double>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<double?, decimal>("Nullable<Double>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<double?, decimal>("Nullable<Double>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, double>("Nullable<Double>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<double?, double>("Nullable<Double>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, PrimaryColor>("Nullable<Double>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<double?, PrimaryColor>("Nullable<Double>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<double?, float>("Nullable<Double>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<double?, float>("Nullable<Double>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, Guid>("Nullable<Double>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<double?, Guid>("Nullable<Double>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<double?, int>("Nullable<Double>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<double?, int>("Nullable<Double>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, long>("Nullable<Double>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<double?, long>("Nullable<Double>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, sbyte>("Nullable<Double>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<double?, sbyte>("Nullable<Double>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, short>("Nullable<Double>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<double?, short>("Nullable<Double>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, string>("Nullable<Double>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<double?, string>("Nullable<Double>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<double?, TimeSpan>("Nullable<Double>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<double?, TimeSpan>("Nullable<Double>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<double?, Type>("Nullable<Double>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<double?, Type>("Nullable<Double>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<double?, uint>("Nullable<Double>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<double?, uint>("Nullable<Double>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, ulong>("Nullable<Double>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<double?, ulong>("Nullable<Double>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, Uri>("Nullable<Double>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<double?, Uri>("Nullable<Double>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<double?, ushort>("Nullable<Double>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<double?, ushort>("Nullable<Double>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<double?, bool?>("Nullable<Double>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<double?, bool?>("Nullable<Double>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<double?, byte?>("Nullable<Double>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<double?, byte?>("Nullable<Double>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, char?>("Nullable<Double>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<double?, char?>("Nullable<Double>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<double?, DateTime?>("Nullable<Double>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<double?, DateTime?>("Nullable<Double>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<double?, DateTimeOffset?>("Nullable<Double>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<double?, DateTimeOffset?>("Nullable<Double>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<double?, decimal?>("Nullable<Double>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<double?, decimal?>("Nullable<Double>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, double?>("Nullable<Double>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<double?, double?>("Nullable<Double>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, PrimaryColor?>("Nullable<Double>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<double?, PrimaryColor?>("Nullable<Double>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<double?, float?>("Nullable<Double>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<double?, float?>("Nullable<Double>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, Guid?>("Nullable<Double>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<double?, Guid?>("Nullable<Double>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<double?, int?>("Nullable<Double>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<double?, int?>("Nullable<Double>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, long?>("Nullable<Double>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<double?, long?>("Nullable<Double>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, sbyte?>("Nullable<Double>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<double?, sbyte?>("Nullable<Double>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, short?>("Nullable<Double>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<double?, short?>("Nullable<Double>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, TimeSpan?>("Nullable<Double>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<double?, TimeSpan?>("Nullable<Double>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<double?, uint?>("Nullable<Double>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<double?, uint?>("Nullable<Double>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, ulong?>("Nullable<Double>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<double?, ulong?>("Nullable<Double>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, ushort?>("Nullable<Double>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<double?, ushort?>("Nullable<Double>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<double?, IInterface>("Nullable<Double>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<double?, IInterface>("Nullable<Double>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<double?, BaseClass>("Nullable<Double>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<double?, BaseClass>("Nullable<Double>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<double?, DerivedClass>("Nullable<Double>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<double?, DerivedClass>("Nullable<Double>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Double>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<double?, bool>("Nullable<Double>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<double?, bool>("Nullable<Double>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<double?, byte>("Nullable<Double>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<double?, byte>("Nullable<Double>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, byte[]>("Nullable<Double>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<double?, byte[]>("Nullable<Double>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<double?, char>("Nullable<Double>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<double?, char>("Nullable<Double>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<double?, DateTime>("Nullable<Double>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<double?, DateTime>("Nullable<Double>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<double?, DateTimeOffset>("Nullable<Double>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<double?, DateTimeOffset>("Nullable<Double>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<double?, decimal>("Nullable<Double>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<double?, decimal>("Nullable<Double>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, double>("Nullable<Double>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<double?, double>("Nullable<Double>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, PrimaryColor>("Nullable<Double>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<double?, PrimaryColor>("Nullable<Double>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<double?, float>("Nullable<Double>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<double?, float>("Nullable<Double>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, Guid>("Nullable<Double>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<double?, Guid>("Nullable<Double>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<double?, int>("Nullable<Double>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<double?, int>("Nullable<Double>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, long>("Nullable<Double>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<double?, long>("Nullable<Double>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, sbyte>("Nullable<Double>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<double?, sbyte>("Nullable<Double>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, short>("Nullable<Double>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<double?, short>("Nullable<Double>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, string>("Nullable<Double>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<double?, string>("Nullable<Double>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<double?, TimeSpan>("Nullable<Double>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<double?, TimeSpan>("Nullable<Double>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<double?, Type>("Nullable<Double>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<double?, Type>("Nullable<Double>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<double?, uint>("Nullable<Double>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<double?, uint>("Nullable<Double>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, ulong>("Nullable<Double>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<double?, ulong>("Nullable<Double>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, Uri>("Nullable<Double>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<double?, Uri>("Nullable<Double>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<double?, ushort>("Nullable<Double>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<double?, ushort>("Nullable<Double>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<double?, bool?>("Nullable<Double>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<double?, bool?>("Nullable<Double>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<double?, byte?>("Nullable<Double>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<double?, byte?>("Nullable<Double>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, char?>("Nullable<Double>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<double?, char?>("Nullable<Double>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<double?, DateTime?>("Nullable<Double>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<double?, DateTime?>("Nullable<Double>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<double?, DateTimeOffset?>("Nullable<Double>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<double?, DateTimeOffset?>("Nullable<Double>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<double?, decimal?>("Nullable<Double>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<double?, decimal?>("Nullable<Double>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, double?>("Nullable<Double>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<double?, double?>("Nullable<Double>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, PrimaryColor?>("Nullable<Double>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<double?, PrimaryColor?>("Nullable<Double>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<double?, float?>("Nullable<Double>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<double?, float?>("Nullable<Double>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, Guid?>("Nullable<Double>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<double?, Guid?>("Nullable<Double>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<double?, int?>("Nullable<Double>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<double?, int?>("Nullable<Double>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, long?>("Nullable<Double>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<double?, long?>("Nullable<Double>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, sbyte?>("Nullable<Double>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<double?, sbyte?>("Nullable<Double>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, short?>("Nullable<Double>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<double?, short?>("Nullable<Double>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, TimeSpan?>("Nullable<Double>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<double?, TimeSpan?>("Nullable<Double>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<double?, uint?>("Nullable<Double>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<double?, uint?>("Nullable<Double>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, ulong?>("Nullable<Double>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<double?, ulong?>("Nullable<Double>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, ushort?>("Nullable<Double>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<double?, ushort?>("Nullable<Double>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<double?, IInterface>("Nullable<Double>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<double?, IInterface>("Nullable<Double>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<double?, BaseClass>("Nullable<Double>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<double?, BaseClass>("Nullable<Double>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<double?, DerivedClass>("Nullable<Double>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<double?, DerivedClass>("Nullable<Double>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
