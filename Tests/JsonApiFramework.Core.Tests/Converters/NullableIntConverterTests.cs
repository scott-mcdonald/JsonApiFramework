// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class NullableIntConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableIntConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Int>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<int?, bool>("Nullable<Int>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<int?, bool>("Nullable<Int>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<int?, byte>("Nullable<Int>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<int?, byte>("Nullable<Int>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, byte[]>("Nullable<Int>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<int?, byte[]>("Nullable<Int>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<int?, char>("Nullable<Int>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<int?, char>("Nullable<Int>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<int?, DateTime>("Nullable<Int>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<int?, DateTime>("Nullable<Int>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<int?, DateTimeOffset>("Nullable<Int>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<int?, DateTimeOffset>("Nullable<Int>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<int?, decimal>("Nullable<Int>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<int?, decimal>("Nullable<Int>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, double>("Nullable<Int>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<int?, double>("Nullable<Int>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, PrimaryColor>("Nullable<Int>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<int?, PrimaryColor>("Nullable<Int>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<int?, float>("Nullable<Int>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<int?, float>("Nullable<Int>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, Guid>("Nullable<Int>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<int?, Guid>("Nullable<Int>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<int?, int>("Nullable<Int>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<int?, int>("Nullable<Int>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, long>("Nullable<Int>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<int?, long>("Nullable<Int>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, sbyte>("Nullable<Int>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<int?, sbyte>("Nullable<Int>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, short>("Nullable<Int>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<int?, short>("Nullable<Int>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, string>("Nullable<Int>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<int?, string>("Nullable<Int>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<int?, TimeSpan>("Nullable<Int>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<int?, TimeSpan>("Nullable<Int>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<int?, Type>("Nullable<Int>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<int?, Type>("Nullable<Int>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<int?, uint>("Nullable<Int>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<int?, uint>("Nullable<Int>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, ulong>("Nullable<Int>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<int?, ulong>("Nullable<Int>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, Uri>("Nullable<Int>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<int?, Uri>("Nullable<Int>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<int?, ushort>("Nullable<Int>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<int?, ushort>("Nullable<Int>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<int?, bool?>("Nullable<Int>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<int?, bool?>("Nullable<Int>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<int?, byte?>("Nullable<Int>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<int?, byte?>("Nullable<Int>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, char?>("Nullable<Int>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<int?, char?>("Nullable<Int>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<int?, DateTime?>("Nullable<Int>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<int?, DateTime?>("Nullable<Int>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<int?, DateTimeOffset?>("Nullable<Int>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<int?, DateTimeOffset?>("Nullable<Int>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<int?, decimal?>("Nullable<Int>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<int?, decimal?>("Nullable<Int>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, double?>("Nullable<Int>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<int?, double?>("Nullable<Int>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, PrimaryColor?>("Nullable<Int>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<int?, PrimaryColor?>("Nullable<Int>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<int?, float?>("Nullable<Int>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<int?, float?>("Nullable<Int>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, Guid?>("Nullable<Int>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<int?, Guid?>("Nullable<Int>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<int?, int?>("Nullable<Int>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<int?, int?>("Nullable<Int>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, long?>("Nullable<Int>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<int?, long?>("Nullable<Int>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, sbyte?>("Nullable<Int>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<int?, sbyte?>("Nullable<Int>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, short?>("Nullable<Int>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<int?, short?>("Nullable<Int>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, TimeSpan?>("Nullable<Int>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<int?, TimeSpan?>("Nullable<Int>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<int?, uint?>("Nullable<Int>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<int?, uint?>("Nullable<Int>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, ulong?>("Nullable<Int>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<int?, ulong?>("Nullable<Int>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, ushort?>("Nullable<Int>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<int?, ushort?>("Nullable<Int>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<int?, IInterface>("Nullable<Int>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<int?, IInterface>("Nullable<Int>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<int?, BaseClass>("Nullable<Int>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<int?, BaseClass>("Nullable<Int>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<int?, DerivedClass>("Nullable<Int>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<int?, DerivedClass>("Nullable<Int>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Int>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<int?, bool>("Nullable<Int>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<int?, bool>("Nullable<Int>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<int?, byte>("Nullable<Int>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<int?, byte>("Nullable<Int>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, byte[]>("Nullable<Int>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<int?, byte[]>("Nullable<Int>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<int?, char>("Nullable<Int>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<int?, char>("Nullable<Int>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<int?, DateTime>("Nullable<Int>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<int?, DateTime>("Nullable<Int>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<int?, DateTimeOffset>("Nullable<Int>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<int?, DateTimeOffset>("Nullable<Int>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<int?, decimal>("Nullable<Int>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<int?, decimal>("Nullable<Int>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, double>("Nullable<Int>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<int?, double>("Nullable<Int>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, PrimaryColor>("Nullable<Int>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<int?, PrimaryColor>("Nullable<Int>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<int?, float>("Nullable<Int>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<int?, float>("Nullable<Int>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, Guid>("Nullable<Int>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<int?, Guid>("Nullable<Int>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<int?, int>("Nullable<Int>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<int?, int>("Nullable<Int>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, long>("Nullable<Int>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<int?, long>("Nullable<Int>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, sbyte>("Nullable<Int>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<int?, sbyte>("Nullable<Int>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, short>("Nullable<Int>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<int?, short>("Nullable<Int>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, string>("Nullable<Int>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<int?, string>("Nullable<Int>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<int?, TimeSpan>("Nullable<Int>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<int?, TimeSpan>("Nullable<Int>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<int?, Type>("Nullable<Int>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<int?, Type>("Nullable<Int>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<int?, uint>("Nullable<Int>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<int?, uint>("Nullable<Int>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, ulong>("Nullable<Int>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<int?, ulong>("Nullable<Int>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, Uri>("Nullable<Int>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<int?, Uri>("Nullable<Int>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<int?, ushort>("Nullable<Int>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<int?, ushort>("Nullable<Int>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<int?, bool?>("Nullable<Int>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<int?, bool?>("Nullable<Int>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<int?, byte?>("Nullable<Int>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<int?, byte?>("Nullable<Int>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, char?>("Nullable<Int>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<int?, char?>("Nullable<Int>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<int?, DateTime?>("Nullable<Int>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<int?, DateTime?>("Nullable<Int>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<int?, DateTimeOffset?>("Nullable<Int>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<int?, DateTimeOffset?>("Nullable<Int>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<int?, decimal?>("Nullable<Int>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<int?, decimal?>("Nullable<Int>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, double?>("Nullable<Int>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<int?, double?>("Nullable<Int>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, PrimaryColor?>("Nullable<Int>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<int?, PrimaryColor?>("Nullable<Int>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<int?, float?>("Nullable<Int>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<int?, float?>("Nullable<Int>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, Guid?>("Nullable<Int>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<int?, Guid?>("Nullable<Int>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<int?, int?>("Nullable<Int>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<int?, int?>("Nullable<Int>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, long?>("Nullable<Int>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<int?, long?>("Nullable<Int>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, sbyte?>("Nullable<Int>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<int?, sbyte?>("Nullable<Int>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, short?>("Nullable<Int>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<int?, short?>("Nullable<Int>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, TimeSpan?>("Nullable<Int>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<int?, TimeSpan?>("Nullable<Int>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<int?, uint?>("Nullable<Int>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<int?, uint?>("Nullable<Int>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, ulong?>("Nullable<Int>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<int?, ulong?>("Nullable<Int>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, ushort?>("Nullable<Int>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<int?, ushort?>("Nullable<Int>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<int?, IInterface>("Nullable<Int>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<int?, IInterface>("Nullable<Int>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<int?, BaseClass>("Nullable<Int>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<int?, BaseClass>("Nullable<Int>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<int?, DerivedClass>("Nullable<Int>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<int?, DerivedClass>("Nullable<Int>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
