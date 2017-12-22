// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableSByteConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableSByteConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<SByte>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<sbyte?, bool>("Nullable<SByte>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<sbyte?, bool>("Nullable<SByte>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<sbyte?, byte>("Nullable<SByte>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<sbyte?, byte>("Nullable<SByte>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, byte[]>("Nullable<SByte>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<sbyte?, byte[]>("Nullable<SByte>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<sbyte?, char>("Nullable<SByte>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<sbyte?, char>("Nullable<SByte>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<sbyte?, DateTime>("Nullable<SByte>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<sbyte?, DateTime>("Nullable<SByte>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<sbyte?, DateTimeOffset>("Nullable<SByte>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<sbyte?, DateTimeOffset>("Nullable<SByte>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<sbyte?, decimal>("Nullable<SByte>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<sbyte?, decimal>("Nullable<SByte>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, double>("Nullable<SByte>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<sbyte?, double>("Nullable<SByte>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, PrimaryColor>("Nullable<SByte>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<sbyte?, PrimaryColor>("Nullable<SByte>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<sbyte?, float>("Nullable<SByte>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<sbyte?, float>("Nullable<SByte>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, Guid>("Nullable<SByte>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<sbyte?, Guid>("Nullable<SByte>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<sbyte?, int>("Nullable<SByte>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<sbyte?, int>("Nullable<SByte>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, long>("Nullable<SByte>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<sbyte?, long>("Nullable<SByte>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, sbyte>("Nullable<SByte>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<sbyte?, sbyte>("Nullable<SByte>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, short>("Nullable<SByte>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<sbyte?, short>("Nullable<SByte>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, string>("Nullable<SByte>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<sbyte?, string>("Nullable<SByte>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<sbyte?, TimeSpan>("Nullable<SByte>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<sbyte?, TimeSpan>("Nullable<SByte>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<sbyte?, Type>("Nullable<SByte>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<sbyte?, Type>("Nullable<SByte>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<sbyte?, uint>("Nullable<SByte>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<sbyte?, uint>("Nullable<SByte>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, ulong>("Nullable<SByte>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<sbyte?, ulong>("Nullable<SByte>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, Uri>("Nullable<SByte>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<sbyte?, Uri>("Nullable<SByte>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<sbyte?, ushort>("Nullable<SByte>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<sbyte?, ushort>("Nullable<SByte>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<sbyte?, bool?>("Nullable<SByte>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<sbyte?, bool?>("Nullable<SByte>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<sbyte?, byte?>("Nullable<SByte>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<sbyte?, byte?>("Nullable<SByte>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, char?>("Nullable<SByte>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<sbyte?, char?>("Nullable<SByte>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<sbyte?, DateTime?>("Nullable<SByte>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<sbyte?, DateTime?>("Nullable<SByte>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<sbyte?, DateTimeOffset?>("Nullable<SByte>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<sbyte?, DateTimeOffset?>("Nullable<SByte>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<sbyte?, decimal?>("Nullable<SByte>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<sbyte?, decimal?>("Nullable<SByte>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, double?>("Nullable<SByte>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<sbyte?, double?>("Nullable<SByte>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, PrimaryColor?>("Nullable<SByte>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<sbyte?, PrimaryColor?>("Nullable<SByte>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<sbyte?, float?>("Nullable<SByte>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<sbyte?, float?>("Nullable<SByte>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, Guid?>("Nullable<SByte>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<sbyte?, Guid?>("Nullable<SByte>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<sbyte?, int?>("Nullable<SByte>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<sbyte?, int?>("Nullable<SByte>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, long?>("Nullable<SByte>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<sbyte?, long?>("Nullable<SByte>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, sbyte?>("Nullable<SByte>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<sbyte?, sbyte?>("Nullable<SByte>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, short?>("Nullable<SByte>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<sbyte?, short?>("Nullable<SByte>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, TimeSpan?>("Nullable<SByte>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<sbyte?, TimeSpan?>("Nullable<SByte>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<sbyte?, uint?>("Nullable<SByte>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<sbyte?, uint?>("Nullable<SByte>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, ulong?>("Nullable<SByte>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<sbyte?, ulong?>("Nullable<SByte>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, ushort?>("Nullable<SByte>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<sbyte?, ushort?>("Nullable<SByte>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<sbyte?, IInterface>("Nullable<SByte>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<sbyte?, IInterface>("Nullable<SByte>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<sbyte?, BaseClass>("Nullable<SByte>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<sbyte?, BaseClass>("Nullable<SByte>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<sbyte?, DerivedClass>("Nullable<SByte>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<sbyte?, DerivedClass>("Nullable<SByte>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<SByte>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<sbyte?, bool>("Nullable<SByte>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<sbyte?, bool>("Nullable<SByte>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<sbyte?, byte>("Nullable<SByte>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<sbyte?, byte>("Nullable<SByte>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, byte[]>("Nullable<SByte>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<sbyte?, byte[]>("Nullable<SByte>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<sbyte?, char>("Nullable<SByte>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<sbyte?, char>("Nullable<SByte>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<sbyte?, DateTime>("Nullable<SByte>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<sbyte?, DateTime>("Nullable<SByte>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<sbyte?, DateTimeOffset>("Nullable<SByte>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<sbyte?, DateTimeOffset>("Nullable<SByte>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<sbyte?, decimal>("Nullable<SByte>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<sbyte?, decimal>("Nullable<SByte>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, double>("Nullable<SByte>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<sbyte?, double>("Nullable<SByte>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, PrimaryColor>("Nullable<SByte>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<sbyte?, PrimaryColor>("Nullable<SByte>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<sbyte?, float>("Nullable<SByte>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<sbyte?, float>("Nullable<SByte>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, Guid>("Nullable<SByte>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<sbyte?, Guid>("Nullable<SByte>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<sbyte?, int>("Nullable<SByte>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<sbyte?, int>("Nullable<SByte>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, long>("Nullable<SByte>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<sbyte?, long>("Nullable<SByte>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, sbyte>("Nullable<SByte>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<sbyte?, sbyte>("Nullable<SByte>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, short>("Nullable<SByte>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<sbyte?, short>("Nullable<SByte>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, string>("Nullable<SByte>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<sbyte?, string>("Nullable<SByte>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<sbyte?, TimeSpan>("Nullable<SByte>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<sbyte?, TimeSpan>("Nullable<SByte>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<sbyte?, Type>("Nullable<SByte>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<sbyte?, Type>("Nullable<SByte>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<sbyte?, uint>("Nullable<SByte>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<sbyte?, uint>("Nullable<SByte>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, ulong>("Nullable<SByte>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<sbyte?, ulong>("Nullable<SByte>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, Uri>("Nullable<SByte>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<sbyte?, Uri>("Nullable<SByte>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<sbyte?, ushort>("Nullable<SByte>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<sbyte?, ushort>("Nullable<SByte>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<sbyte?, bool?>("Nullable<SByte>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<sbyte?, bool?>("Nullable<SByte>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<sbyte?, byte?>("Nullable<SByte>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<sbyte?, byte?>("Nullable<SByte>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, char?>("Nullable<SByte>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<sbyte?, char?>("Nullable<SByte>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<sbyte?, DateTime?>("Nullable<SByte>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<sbyte?, DateTime?>("Nullable<SByte>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<sbyte?, DateTimeOffset?>("Nullable<SByte>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<sbyte?, DateTimeOffset?>("Nullable<SByte>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<sbyte?, decimal?>("Nullable<SByte>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<sbyte?, decimal?>("Nullable<SByte>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, double?>("Nullable<SByte>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<sbyte?, double?>("Nullable<SByte>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, PrimaryColor?>("Nullable<SByte>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<sbyte?, PrimaryColor?>("Nullable<SByte>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<sbyte?, float?>("Nullable<SByte>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<sbyte?, float?>("Nullable<SByte>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, Guid?>("Nullable<SByte>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<sbyte?, Guid?>("Nullable<SByte>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<sbyte?, int?>("Nullable<SByte>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<sbyte?, int?>("Nullable<SByte>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, long?>("Nullable<SByte>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<sbyte?, long?>("Nullable<SByte>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, sbyte?>("Nullable<SByte>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<sbyte?, sbyte?>("Nullable<SByte>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, short?>("Nullable<SByte>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<sbyte?, short?>("Nullable<SByte>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, TimeSpan?>("Nullable<SByte>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<sbyte?, TimeSpan?>("Nullable<SByte>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<sbyte?, uint?>("Nullable<SByte>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<sbyte?, uint?>("Nullable<SByte>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, ulong?>("Nullable<SByte>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<sbyte?, ulong?>("Nullable<SByte>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, ushort?>("Nullable<SByte>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<sbyte?, ushort?>("Nullable<SByte>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<sbyte?, IInterface>("Nullable<SByte>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<sbyte?, IInterface>("Nullable<SByte>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<sbyte?, BaseClass>("Nullable<SByte>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<sbyte?, BaseClass>("Nullable<SByte>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<sbyte?, DerivedClass>("Nullable<SByte>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<sbyte?, DerivedClass>("Nullable<SByte>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
