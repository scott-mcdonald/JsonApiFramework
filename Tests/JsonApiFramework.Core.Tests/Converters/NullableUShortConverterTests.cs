// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class NullableUShortConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableUShortConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<UShort>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<ushort?, bool>("Nullable<UShort>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<ushort?, bool>("Nullable<UShort>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ushort?, byte>("Nullable<UShort>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<ushort?, byte>("Nullable<UShort>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, byte[]>("Nullable<UShort>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ushort?, byte[]>("Nullable<UShort>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ushort?, char>("Nullable<UShort>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<ushort?, char>("Nullable<UShort>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ushort?, DateTime>("Nullable<UShort>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ushort?, DateTime>("Nullable<UShort>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ushort?, DateTimeOffset>("Nullable<UShort>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ushort?, DateTimeOffset>("Nullable<UShort>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ushort?, decimal>("Nullable<UShort>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<ushort?, decimal>("Nullable<UShort>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, double>("Nullable<UShort>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<ushort?, double>("Nullable<UShort>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, PrimaryColor>("Nullable<UShort>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<ushort?, PrimaryColor>("Nullable<UShort>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ushort?, float>("Nullable<UShort>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<ushort?, float>("Nullable<UShort>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, Guid>("Nullable<UShort>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ushort?, Guid>("Nullable<UShort>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ushort?, int>("Nullable<UShort>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<ushort?, int>("Nullable<UShort>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, long>("Nullable<UShort>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<ushort?, long>("Nullable<UShort>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, sbyte>("Nullable<UShort>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<ushort?, sbyte>("Nullable<UShort>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, short>("Nullable<UShort>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<ushort?, short>("Nullable<UShort>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, string>("Nullable<UShort>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<ushort?, string>("Nullable<UShort>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<ushort?, TimeSpan>("Nullable<UShort>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ushort?, TimeSpan>("Nullable<UShort>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ushort?, Type>("Nullable<UShort>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ushort?, Type>("Nullable<UShort>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ushort?, uint>("Nullable<UShort>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<ushort?, uint>("Nullable<UShort>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, ulong>("Nullable<UShort>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<ushort?, ulong>("Nullable<UShort>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, Uri>("Nullable<UShort>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ushort?, Uri>("Nullable<UShort>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ushort?, ushort>("Nullable<UShort>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<ushort?, ushort>("Nullable<UShort>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<ushort?, bool?>("Nullable<UShort>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<ushort?, bool?>("Nullable<UShort>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ushort?, byte?>("Nullable<UShort>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<ushort?, byte?>("Nullable<UShort>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, char?>("Nullable<UShort>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<ushort?, char?>("Nullable<UShort>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ushort?, DateTime?>("Nullable<UShort>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ushort?, DateTime?>("Nullable<UShort>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ushort?, DateTimeOffset?>("Nullable<UShort>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ushort?, DateTimeOffset?>("Nullable<UShort>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ushort?, decimal?>("Nullable<UShort>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<ushort?, decimal?>("Nullable<UShort>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, double?>("Nullable<UShort>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<ushort?, double?>("Nullable<UShort>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, PrimaryColor?>("Nullable<UShort>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<ushort?, PrimaryColor?>("Nullable<UShort>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ushort?, float?>("Nullable<UShort>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<ushort?, float?>("Nullable<UShort>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, Guid?>("Nullable<UShort>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ushort?, Guid?>("Nullable<UShort>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ushort?, int?>("Nullable<UShort>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<ushort?, int?>("Nullable<UShort>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, long?>("Nullable<UShort>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<ushort?, long?>("Nullable<UShort>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, sbyte?>("Nullable<UShort>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<ushort?, sbyte?>("Nullable<UShort>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, short?>("Nullable<UShort>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<ushort?, short?>("Nullable<UShort>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, TimeSpan?>("Nullable<UShort>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ushort?, TimeSpan?>("Nullable<UShort>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ushort?, uint?>("Nullable<UShort>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<ushort?, uint?>("Nullable<UShort>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, ulong?>("Nullable<UShort>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<ushort?, ulong?>("Nullable<UShort>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, ushort?>("Nullable<UShort>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<ushort?, ushort?>("Nullable<UShort>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<ushort?, IInterface>("Nullable<UShort>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ushort?, IInterface>("Nullable<UShort>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ushort?, BaseClass>("Nullable<UShort>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ushort?, BaseClass>("Nullable<UShort>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ushort?, DerivedClass>("Nullable<UShort>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<ushort?, DerivedClass>("Nullable<UShort>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<UShort>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<ushort?, bool>("Nullable<UShort>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<ushort?, bool>("Nullable<UShort>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ushort?, byte>("Nullable<UShort>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<ushort?, byte>("Nullable<UShort>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, byte[]>("Nullable<UShort>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ushort?, byte[]>("Nullable<UShort>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ushort?, char>("Nullable<UShort>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<ushort?, char>("Nullable<UShort>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ushort?, DateTime>("Nullable<UShort>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ushort?, DateTime>("Nullable<UShort>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ushort?, DateTimeOffset>("Nullable<UShort>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ushort?, DateTimeOffset>("Nullable<UShort>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ushort?, decimal>("Nullable<UShort>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<ushort?, decimal>("Nullable<UShort>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, double>("Nullable<UShort>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<ushort?, double>("Nullable<UShort>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, PrimaryColor>("Nullable<UShort>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<ushort?, PrimaryColor>("Nullable<UShort>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ushort?, float>("Nullable<UShort>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<ushort?, float>("Nullable<UShort>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, Guid>("Nullable<UShort>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ushort?, Guid>("Nullable<UShort>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ushort?, int>("Nullable<UShort>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<ushort?, int>("Nullable<UShort>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, long>("Nullable<UShort>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<ushort?, long>("Nullable<UShort>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, sbyte>("Nullable<UShort>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<ushort?, sbyte>("Nullable<UShort>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, short>("Nullable<UShort>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<ushort?, short>("Nullable<UShort>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, string>("Nullable<UShort>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<ushort?, string>("Nullable<UShort>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<ushort?, TimeSpan>("Nullable<UShort>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ushort?, TimeSpan>("Nullable<UShort>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ushort?, Type>("Nullable<UShort>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ushort?, Type>("Nullable<UShort>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ushort?, uint>("Nullable<UShort>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<ushort?, uint>("Nullable<UShort>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, ulong>("Nullable<UShort>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<ushort?, ulong>("Nullable<UShort>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, Uri>("Nullable<UShort>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ushort?, Uri>("Nullable<UShort>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ushort?, ushort>("Nullable<UShort>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<ushort?, ushort>("Nullable<UShort>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<ushort?, bool?>("Nullable<UShort>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<ushort?, bool?>("Nullable<UShort>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ushort?, byte?>("Nullable<UShort>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<ushort?, byte?>("Nullable<UShort>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, char?>("Nullable<UShort>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<ushort?, char?>("Nullable<UShort>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ushort?, DateTime?>("Nullable<UShort>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ushort?, DateTime?>("Nullable<UShort>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ushort?, DateTimeOffset?>("Nullable<UShort>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ushort?, DateTimeOffset?>("Nullable<UShort>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ushort?, decimal?>("Nullable<UShort>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<ushort?, decimal?>("Nullable<UShort>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, double?>("Nullable<UShort>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<ushort?, double?>("Nullable<UShort>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, PrimaryColor?>("Nullable<UShort>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<ushort?, PrimaryColor?>("Nullable<UShort>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ushort?, float?>("Nullable<UShort>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<ushort?, float?>("Nullable<UShort>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, Guid?>("Nullable<UShort>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ushort?, Guid?>("Nullable<UShort>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ushort?, int?>("Nullable<UShort>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<ushort?, int?>("Nullable<UShort>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, long?>("Nullable<UShort>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<ushort?, long?>("Nullable<UShort>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, sbyte?>("Nullable<UShort>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<ushort?, sbyte?>("Nullable<UShort>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, short?>("Nullable<UShort>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<ushort?, short?>("Nullable<UShort>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, TimeSpan?>("Nullable<UShort>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ushort?, TimeSpan?>("Nullable<UShort>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ushort?, uint?>("Nullable<UShort>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<ushort?, uint?>("Nullable<UShort>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, ulong?>("Nullable<UShort>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<ushort?, ulong?>("Nullable<UShort>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, ushort?>("Nullable<UShort>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<ushort?, ushort?>("Nullable<UShort>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<ushort?, IInterface>("Nullable<UShort>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ushort?, IInterface>("Nullable<UShort>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ushort?, BaseClass>("Nullable<UShort>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ushort?, BaseClass>("Nullable<UShort>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ushort?, DerivedClass>("Nullable<UShort>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<ushort?, DerivedClass>("Nullable<UShort>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
