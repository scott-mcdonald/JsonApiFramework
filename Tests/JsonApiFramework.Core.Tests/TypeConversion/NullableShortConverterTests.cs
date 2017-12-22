// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableShortConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableShortConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Short>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<short?, bool>("Nullable<Short>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<short?, bool>("Nullable<Short>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<short?, byte>("Nullable<Short>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<short?, byte>("Nullable<Short>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, byte[]>("Nullable<Short>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<short?, byte[]>("Nullable<Short>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<short?, char>("Nullable<Short>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<short?, char>("Nullable<Short>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<short?, DateTime>("Nullable<Short>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<short?, DateTime>("Nullable<Short>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<short?, DateTimeOffset>("Nullable<Short>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<short?, DateTimeOffset>("Nullable<Short>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<short?, decimal>("Nullable<Short>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<short?, decimal>("Nullable<Short>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, double>("Nullable<Short>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<short?, double>("Nullable<Short>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, PrimaryColor>("Nullable<Short>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<short?, PrimaryColor>("Nullable<Short>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<short?, float>("Nullable<Short>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<short?, float>("Nullable<Short>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, Guid>("Nullable<Short>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<short?, Guid>("Nullable<Short>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<short?, int>("Nullable<Short>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<short?, int>("Nullable<Short>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, long>("Nullable<Short>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<short?, long>("Nullable<Short>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, sbyte>("Nullable<Short>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<short?, sbyte>("Nullable<Short>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, short>("Nullable<Short>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<short?, short>("Nullable<Short>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, string>("Nullable<Short>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<short?, string>("Nullable<Short>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<short?, TimeSpan>("Nullable<Short>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<short?, TimeSpan>("Nullable<Short>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<short?, Type>("Nullable<Short>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<short?, Type>("Nullable<Short>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<short?, uint>("Nullable<Short>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<short?, uint>("Nullable<Short>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, ulong>("Nullable<Short>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<short?, ulong>("Nullable<Short>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, Uri>("Nullable<Short>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<short?, Uri>("Nullable<Short>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<short?, ushort>("Nullable<Short>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<short?, ushort>("Nullable<Short>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<short?, bool?>("Nullable<Short>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<short?, bool?>("Nullable<Short>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<short?, byte?>("Nullable<Short>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<short?, byte?>("Nullable<Short>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, char?>("Nullable<Short>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<short?, char?>("Nullable<Short>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<short?, DateTime?>("Nullable<Short>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<short?, DateTime?>("Nullable<Short>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<short?, DateTimeOffset?>("Nullable<Short>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<short?, DateTimeOffset?>("Nullable<Short>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<short?, decimal?>("Nullable<Short>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<short?, decimal?>("Nullable<Short>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, double?>("Nullable<Short>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<short?, double?>("Nullable<Short>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, PrimaryColor?>("Nullable<Short>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<short?, PrimaryColor?>("Nullable<Short>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<short?, float?>("Nullable<Short>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<short?, float?>("Nullable<Short>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, Guid?>("Nullable<Short>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<short?, Guid?>("Nullable<Short>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<short?, int?>("Nullable<Short>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<short?, int?>("Nullable<Short>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, long?>("Nullable<Short>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<short?, long?>("Nullable<Short>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, sbyte?>("Nullable<Short>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<short?, sbyte?>("Nullable<Short>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, short?>("Nullable<Short>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<short?, short?>("Nullable<Short>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, TimeSpan?>("Nullable<Short>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<short?, TimeSpan?>("Nullable<Short>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<short?, uint?>("Nullable<Short>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<short?, uint?>("Nullable<Short>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, ulong?>("Nullable<Short>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<short?, ulong?>("Nullable<Short>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, ushort?>("Nullable<Short>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<short?, ushort?>("Nullable<Short>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<short?, IInterface>("Nullable<Short>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<short?, IInterface>("Nullable<Short>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<short?, BaseClass>("Nullable<Short>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<short?, BaseClass>("Nullable<Short>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<short?, DerivedClass>("Nullable<Short>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<short?, DerivedClass>("Nullable<Short>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Short>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<short?, bool>("Nullable<Short>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<short?, bool>("Nullable<Short>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<short?, byte>("Nullable<Short>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<short?, byte>("Nullable<Short>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, byte[]>("Nullable<Short>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<short?, byte[]>("Nullable<Short>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<short?, char>("Nullable<Short>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<short?, char>("Nullable<Short>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<short?, DateTime>("Nullable<Short>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<short?, DateTime>("Nullable<Short>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<short?, DateTimeOffset>("Nullable<Short>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<short?, DateTimeOffset>("Nullable<Short>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<short?, decimal>("Nullable<Short>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<short?, decimal>("Nullable<Short>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, double>("Nullable<Short>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<short?, double>("Nullable<Short>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, PrimaryColor>("Nullable<Short>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<short?, PrimaryColor>("Nullable<Short>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<short?, float>("Nullable<Short>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<short?, float>("Nullable<Short>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, Guid>("Nullable<Short>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<short?, Guid>("Nullable<Short>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<short?, int>("Nullable<Short>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<short?, int>("Nullable<Short>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, long>("Nullable<Short>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<short?, long>("Nullable<Short>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, sbyte>("Nullable<Short>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<short?, sbyte>("Nullable<Short>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, short>("Nullable<Short>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<short?, short>("Nullable<Short>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, string>("Nullable<Short>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<short?, string>("Nullable<Short>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<short?, TimeSpan>("Nullable<Short>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<short?, TimeSpan>("Nullable<Short>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<short?, Type>("Nullable<Short>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<short?, Type>("Nullable<Short>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<short?, uint>("Nullable<Short>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<short?, uint>("Nullable<Short>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, ulong>("Nullable<Short>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<short?, ulong>("Nullable<Short>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, Uri>("Nullable<Short>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<short?, Uri>("Nullable<Short>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<short?, ushort>("Nullable<Short>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<short?, ushort>("Nullable<Short>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<short?, bool?>("Nullable<Short>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<short?, bool?>("Nullable<Short>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<short?, byte?>("Nullable<Short>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<short?, byte?>("Nullable<Short>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, char?>("Nullable<Short>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<short?, char?>("Nullable<Short>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<short?, DateTime?>("Nullable<Short>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<short?, DateTime?>("Nullable<Short>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<short?, DateTimeOffset?>("Nullable<Short>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<short?, DateTimeOffset?>("Nullable<Short>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<short?, decimal?>("Nullable<Short>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<short?, decimal?>("Nullable<Short>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, double?>("Nullable<Short>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<short?, double?>("Nullable<Short>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, PrimaryColor?>("Nullable<Short>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<short?, PrimaryColor?>("Nullable<Short>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<short?, float?>("Nullable<Short>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<short?, float?>("Nullable<Short>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, Guid?>("Nullable<Short>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<short?, Guid?>("Nullable<Short>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<short?, int?>("Nullable<Short>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<short?, int?>("Nullable<Short>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, long?>("Nullable<Short>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<short?, long?>("Nullable<Short>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, sbyte?>("Nullable<Short>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<short?, sbyte?>("Nullable<Short>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, short?>("Nullable<Short>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<short?, short?>("Nullable<Short>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, TimeSpan?>("Nullable<Short>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<short?, TimeSpan?>("Nullable<Short>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<short?, uint?>("Nullable<Short>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<short?, uint?>("Nullable<Short>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, ulong?>("Nullable<Short>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<short?, ulong?>("Nullable<Short>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, ushort?>("Nullable<Short>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<short?, ushort?>("Nullable<Short>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<short?, IInterface>("Nullable<Short>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<short?, IInterface>("Nullable<Short>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<short?, BaseClass>("Nullable<Short>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<short?, BaseClass>("Nullable<Short>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<short?, DerivedClass>("Nullable<Short>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<short?, DerivedClass>("Nullable<Short>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
