// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class NullableLongConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableLongConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Long>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<long?, bool>("Nullable<Long>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<long?, bool>("Nullable<Long>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<long?, byte>("Nullable<Long>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<long?, byte>("Nullable<Long>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, byte[]>("Nullable<Long>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<long?, byte[]>("Nullable<Long>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<long?, char>("Nullable<Long>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<long?, char>("Nullable<Long>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<long?, DateTime>("Nullable<Long>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<long?, DateTime>("Nullable<Long>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<long?, DateTimeOffset>("Nullable<Long>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<long?, DateTimeOffset>("Nullable<Long>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<long?, decimal>("Nullable<Long>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<long?, decimal>("Nullable<Long>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, double>("Nullable<Long>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<long?, double>("Nullable<Long>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, PrimaryColor>("Nullable<Long>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<long?, PrimaryColor>("Nullable<Long>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<long?, float>("Nullable<Long>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<long?, float>("Nullable<Long>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, Guid>("Nullable<Long>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<long?, Guid>("Nullable<Long>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<long?, int>("Nullable<Long>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<long?, int>("Nullable<Long>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, long>("Nullable<Long>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<long?, long>("Nullable<Long>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, sbyte>("Nullable<Long>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<long?, sbyte>("Nullable<Long>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, short>("Nullable<Long>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<long?, short>("Nullable<Long>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, string>("Nullable<Long>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<long?, string>("Nullable<Long>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<long?, TimeSpan>("Nullable<Long>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<long?, TimeSpan>("Nullable<Long>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<long?, Type>("Nullable<Long>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<long?, Type>("Nullable<Long>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<long?, uint>("Nullable<Long>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<long?, uint>("Nullable<Long>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, ulong>("Nullable<Long>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<long?, ulong>("Nullable<Long>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, Uri>("Nullable<Long>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<long?, Uri>("Nullable<Long>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<long?, ushort>("Nullable<Long>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<long?, ushort>("Nullable<Long>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<long?, bool?>("Nullable<Long>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<long?, bool?>("Nullable<Long>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<long?, byte?>("Nullable<Long>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<long?, byte?>("Nullable<Long>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, char?>("Nullable<Long>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<long?, char?>("Nullable<Long>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<long?, DateTime?>("Nullable<Long>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<long?, DateTime?>("Nullable<Long>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<long?, DateTimeOffset?>("Nullable<Long>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<long?, DateTimeOffset?>("Nullable<Long>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<long?, decimal?>("Nullable<Long>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<long?, decimal?>("Nullable<Long>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, double?>("Nullable<Long>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<long?, double?>("Nullable<Long>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, PrimaryColor?>("Nullable<Long>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<long?, PrimaryColor?>("Nullable<Long>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<long?, float?>("Nullable<Long>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<long?, float?>("Nullable<Long>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, Guid?>("Nullable<Long>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<long?, Guid?>("Nullable<Long>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<long?, int?>("Nullable<Long>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<long?, int?>("Nullable<Long>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, long?>("Nullable<Long>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<long?, long?>("Nullable<Long>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, sbyte?>("Nullable<Long>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<long?, sbyte?>("Nullable<Long>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, short?>("Nullable<Long>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<long?, short?>("Nullable<Long>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, TimeSpan?>("Nullable<Long>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<long?, TimeSpan?>("Nullable<Long>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<long?, uint?>("Nullable<Long>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<long?, uint?>("Nullable<Long>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, ulong?>("Nullable<Long>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<long?, ulong?>("Nullable<Long>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, ushort?>("Nullable<Long>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<long?, ushort?>("Nullable<Long>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<long?, IInterface>("Nullable<Long>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<long?, IInterface>("Nullable<Long>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<long?, BaseClass>("Nullable<Long>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<long?, BaseClass>("Nullable<Long>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<long?, DerivedClass>("Nullable<Long>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<long?, DerivedClass>("Nullable<Long>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Long>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<long?, bool>("Nullable<Long>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<long?, bool>("Nullable<Long>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<long?, byte>("Nullable<Long>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<long?, byte>("Nullable<Long>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, byte[]>("Nullable<Long>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<long?, byte[]>("Nullable<Long>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<long?, char>("Nullable<Long>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<long?, char>("Nullable<Long>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<long?, DateTime>("Nullable<Long>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<long?, DateTime>("Nullable<Long>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<long?, DateTimeOffset>("Nullable<Long>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<long?, DateTimeOffset>("Nullable<Long>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<long?, decimal>("Nullable<Long>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<long?, decimal>("Nullable<Long>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, double>("Nullable<Long>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<long?, double>("Nullable<Long>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, PrimaryColor>("Nullable<Long>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<long?, PrimaryColor>("Nullable<Long>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<long?, float>("Nullable<Long>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<long?, float>("Nullable<Long>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, Guid>("Nullable<Long>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<long?, Guid>("Nullable<Long>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<long?, int>("Nullable<Long>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<long?, int>("Nullable<Long>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, long>("Nullable<Long>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<long?, long>("Nullable<Long>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, sbyte>("Nullable<Long>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<long?, sbyte>("Nullable<Long>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, short>("Nullable<Long>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<long?, short>("Nullable<Long>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, string>("Nullable<Long>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<long?, string>("Nullable<Long>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<long?, TimeSpan>("Nullable<Long>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<long?, TimeSpan>("Nullable<Long>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<long?, Type>("Nullable<Long>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<long?, Type>("Nullable<Long>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<long?, uint>("Nullable<Long>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<long?, uint>("Nullable<Long>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, ulong>("Nullable<Long>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<long?, ulong>("Nullable<Long>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, Uri>("Nullable<Long>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<long?, Uri>("Nullable<Long>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<long?, ushort>("Nullable<Long>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<long?, ushort>("Nullable<Long>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<long?, bool?>("Nullable<Long>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<long?, bool?>("Nullable<Long>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<long?, byte?>("Nullable<Long>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<long?, byte?>("Nullable<Long>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, char?>("Nullable<Long>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<long?, char?>("Nullable<Long>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<long?, DateTime?>("Nullable<Long>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<long?, DateTime?>("Nullable<Long>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<long?, DateTimeOffset?>("Nullable<Long>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<long?, DateTimeOffset?>("Nullable<Long>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<long?, decimal?>("Nullable<Long>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<long?, decimal?>("Nullable<Long>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, double?>("Nullable<Long>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<long?, double?>("Nullable<Long>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, PrimaryColor?>("Nullable<Long>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<long?, PrimaryColor?>("Nullable<Long>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<long?, float?>("Nullable<Long>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<long?, float?>("Nullable<Long>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, Guid?>("Nullable<Long>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<long?, Guid?>("Nullable<Long>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<long?, int?>("Nullable<Long>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<long?, int?>("Nullable<Long>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, long?>("Nullable<Long>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<long?, long?>("Nullable<Long>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, sbyte?>("Nullable<Long>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<long?, sbyte?>("Nullable<Long>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, short?>("Nullable<Long>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<long?, short?>("Nullable<Long>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, TimeSpan?>("Nullable<Long>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<long?, TimeSpan?>("Nullable<Long>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<long?, uint?>("Nullable<Long>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<long?, uint?>("Nullable<Long>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, ulong?>("Nullable<Long>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<long?, ulong?>("Nullable<Long>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, ushort?>("Nullable<Long>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<long?, ushort?>("Nullable<Long>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<long?, IInterface>("Nullable<Long>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<long?, IInterface>("Nullable<Long>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<long?, BaseClass>("Nullable<Long>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<long?, BaseClass>("Nullable<Long>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<long?, DerivedClass>("Nullable<Long>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<long?, DerivedClass>("Nullable<Long>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
