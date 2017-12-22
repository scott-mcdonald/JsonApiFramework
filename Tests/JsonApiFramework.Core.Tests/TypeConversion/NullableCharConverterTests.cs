// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableCharConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableCharConverterTests(ITestOutputHelper output)
            : base(output, false)
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
                        new AggregateUnitTest("Nullable<Char>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<char?, bool>("Nullable<Char>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<char?, bool>("Nullable<Char>ToBool", '*', ConvertResult.Success, true),
                            new ConvertGenericTest<char?, byte>("Nullable<Char>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<char?, byte>("Nullable<Char>ToByte", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, byte[]>("Nullable<Char>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<char?, byte[]>("Nullable<Char>ToByteArray", '*', ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<char?, char>("Nullable<Char>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<char?, char>("Nullable<Char>ToChar", '*', ConvertResult.Success, '*'),
                            new ConvertGenericTest<char?, DateTime>("Nullable<Char>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<char?, DateTime>("Nullable<Char>ToDateTime", '*', ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<char?, DateTimeOffset>("Nullable<Char>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<char?, DateTimeOffset>("Nullable<Char>ToDateTimeOffset", '*', ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<char?, decimal>("Nullable<Char>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<char?, decimal>("Nullable<Char>ToDecimal", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, double>("Nullable<Char>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<char?, double>("Nullable<Char>ToDouble", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, PrimaryColor>("Nullable<Char>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<char?, PrimaryColor>("Nullable<Char>ToEnum", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<char?, float>("Nullable<Char>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<char?, float>("Nullable<Char>ToFloat", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, Guid>("Nullable<Char>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<char?, Guid>("Nullable<Char>ToGuid", '*', ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<char?, int>("Nullable<Char>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<char?, int>("Nullable<Char>ToInt", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, long>("Nullable<Char>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<char?, long>("Nullable<Char>ToLong", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, sbyte>("Nullable<Char>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<char?, sbyte>("Nullable<Char>ToSByte", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, short>("Nullable<Char>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<char?, short>("Nullable<Char>ToShort", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, string>("Nullable<Char>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<char?, string>("Nullable<Char>ToString", '*', ConvertResult.Success, "*"),
                            new ConvertGenericTest<char?, TimeSpan>("Nullable<Char>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<char?, TimeSpan>("Nullable<Char>ToTimeSpan", '*', ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<char?, Type>("Nullable<Char>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<char?, Type>("Nullable<Char>ToType", '*', ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<char?, uint>("Nullable<Char>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<char?, uint>("Nullable<Char>ToUInt", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, ulong>("Nullable<Char>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<char?, ulong>("Nullable<Char>ToULong", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, Uri>("Nullable<Char>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<char?, Uri>("Nullable<Char>ToUri", '*', ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<char?, ushort>("Nullable<Char>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<char?, ushort>("Nullable<Char>ToUShort", '*', ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<char?, bool?>("Nullable<Char>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<char?, bool?>("Nullable<Char>ToNullable<Bool>", '*', ConvertResult.Success, true),
                            new ConvertGenericTest<char?, byte?>("Nullable<Char>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<char?, byte?>("Nullable<Char>ToNullable<Byte>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, char?>("Nullable<Char>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<char?, char?>("Nullable<Char>ToNullable<Char>", '*', ConvertResult.Success, '*'),
                            new ConvertGenericTest<char?, DateTime?>("Nullable<Char>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<char?, DateTime?>("Nullable<Char>ToNullable<DateTime>", '*', ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<char?, DateTimeOffset?>("Nullable<Char>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<char?, DateTimeOffset?>("Nullable<Char>ToNullable<DateTimeOffset>", '*', ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<char?, decimal?>("Nullable<Char>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<char?, decimal?>("Nullable<Char>ToNullable<Decimal>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, double?>("Nullable<Char>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<char?, double?>("Nullable<Char>ToNullable<Double>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, PrimaryColor?>("Nullable<Char>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<char?, PrimaryColor?>("Nullable<Char>ToNullable<Enum>", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<char?, float?>("Nullable<Char>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<char?, float?>("Nullable<Char>ToNullable<Float>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, Guid?>("Nullable<Char>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<char?, Guid?>("Nullable<Char>ToNullable<Guid>", '*', ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<char?, int?>("Nullable<Char>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<char?, int?>("Nullable<Char>ToNullable<Int>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, long?>("Nullable<Char>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<char?, long?>("Nullable<Char>ToNullable<Long>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, sbyte?>("Nullable<Char>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<char?, sbyte?>("Nullable<Char>ToNullable<SByte>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, short?>("Nullable<Char>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<char?, short?>("Nullable<Char>ToNullable<Short>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, TimeSpan?>("Nullable<Char>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<char?, TimeSpan?>("Nullable<Char>ToNullable<TimeSpan>", '*', ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<char?, uint?>("Nullable<Char>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<char?, uint?>("Nullable<Char>ToNullable<UInt>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, ulong?>("Nullable<Char>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<char?, ulong?>("Nullable<Char>ToNullable<ULong>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, ushort?>("Nullable<Char>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<char?, ushort?>("Nullable<Char>ToNullable<UShort>", '*', ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<char?, IInterface>("Nullable<Char>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<char?, IInterface>("Nullable<Char>ToInterface", '*', ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<char?, BaseClass>("Nullable<Char>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<char?, BaseClass>("Nullable<Char>ToBaseClass", '*', ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<char?, DerivedClass>("Nullable<Char>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<char?, DerivedClass>("Nullable<Char>ToDerivedClass", '*', ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Char>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<char?, bool>("Nullable<Char>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<char?, bool>("Nullable<Char>ToBool", '*', ConvertResult.Success, true),
                            new TryConvertGenericTest<char?, byte>("Nullable<Char>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<char?, byte>("Nullable<Char>ToByte", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, byte[]>("Nullable<Char>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<char?, byte[]>("Nullable<Char>ToByteArray", '*', ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<char?, char>("Nullable<Char>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<char?, char>("Nullable<Char>ToChar", '*', ConvertResult.Success, '*'),
                            new TryConvertGenericTest<char?, DateTime>("Nullable<Char>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<char?, DateTime>("Nullable<Char>ToDateTime", '*', ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<char?, DateTimeOffset>("Nullable<Char>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<char?, DateTimeOffset>("Nullable<Char>ToDateTimeOffset", '*', ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<char?, decimal>("Nullable<Char>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<char?, decimal>("Nullable<Char>ToDecimal", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, double>("Nullable<Char>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<char?, double>("Nullable<Char>ToDouble", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, PrimaryColor>("Nullable<Char>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<char?, PrimaryColor>("Nullable<Char>ToEnum", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<char?, float>("Nullable<Char>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<char?, float>("Nullable<Char>ToFloat", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, Guid>("Nullable<Char>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<char?, Guid>("Nullable<Char>ToGuid", '*', ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<char?, int>("Nullable<Char>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<char?, int>("Nullable<Char>ToInt", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, long>("Nullable<Char>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<char?, long>("Nullable<Char>ToLong", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, sbyte>("Nullable<Char>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<char?, sbyte>("Nullable<Char>ToSByte", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, short>("Nullable<Char>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<char?, short>("Nullable<Char>ToShort", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, string>("Nullable<Char>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<char?, string>("Nullable<Char>ToString", '*', ConvertResult.Success, "*"),
                            new TryConvertGenericTest<char?, TimeSpan>("Nullable<Char>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<char?, TimeSpan>("Nullable<Char>ToTimeSpan", '*', ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<char?, Type>("Nullable<Char>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<char?, Type>("Nullable<Char>ToType", '*', ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<char?, uint>("Nullable<Char>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<char?, uint>("Nullable<Char>ToUInt", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, ulong>("Nullable<Char>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<char?, ulong>("Nullable<Char>ToULong", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, Uri>("Nullable<Char>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<char?, Uri>("Nullable<Char>ToUri", '*', ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<char?, ushort>("Nullable<Char>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<char?, ushort>("Nullable<Char>ToUShort", '*', ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<char?, bool?>("Nullable<Char>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<char?, bool?>("Nullable<Char>ToNullable<Bool>", '*', ConvertResult.Success, true),
                            new TryConvertGenericTest<char?, byte?>("Nullable<Char>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<char?, byte?>("Nullable<Char>ToNullable<Byte>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, char?>("Nullable<Char>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<char?, char?>("Nullable<Char>ToNullable<Char>", '*', ConvertResult.Success, '*'),
                            new TryConvertGenericTest<char?, DateTime?>("Nullable<Char>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<char?, DateTime?>("Nullable<Char>ToNullable<DateTime>", '*', ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<char?, DateTimeOffset?>("Nullable<Char>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<char?, DateTimeOffset?>("Nullable<Char>ToNullable<DateTimeOffset>", '*', ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<char?, decimal?>("Nullable<Char>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<char?, decimal?>("Nullable<Char>ToNullable<Decimal>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, double?>("Nullable<Char>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<char?, double?>("Nullable<Char>ToNullable<Double>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, PrimaryColor?>("Nullable<Char>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<char?, PrimaryColor?>("Nullable<Char>ToNullable<Enum>", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<char?, float?>("Nullable<Char>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<char?, float?>("Nullable<Char>ToNullable<Float>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, Guid?>("Nullable<Char>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<char?, Guid?>("Nullable<Char>ToNullable<Guid>", '*', ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<char?, int?>("Nullable<Char>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<char?, int?>("Nullable<Char>ToNullable<Int>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, long?>("Nullable<Char>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<char?, long?>("Nullable<Char>ToNullable<Long>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, sbyte?>("Nullable<Char>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<char?, sbyte?>("Nullable<Char>ToNullable<SByte>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, short?>("Nullable<Char>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<char?, short?>("Nullable<Char>ToNullable<Short>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, TimeSpan?>("Nullable<Char>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<char?, TimeSpan?>("Nullable<Char>ToNullable<TimeSpan>", '*', ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<char?, uint?>("Nullable<Char>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<char?, uint?>("Nullable<Char>ToNullable<UInt>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, ulong?>("Nullable<Char>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<char?, ulong?>("Nullable<Char>ToNullable<ULong>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, ushort?>("Nullable<Char>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<char?, ushort?>("Nullable<Char>ToNullable<UShort>", '*', ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<char?, IInterface>("Nullable<Char>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<char?, IInterface>("Nullable<Char>ToInterface", '*', ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<char?, BaseClass>("Nullable<Char>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<char?, BaseClass>("Nullable<Char>ToBaseClass", '*', ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<char?, DerivedClass>("Nullable<Char>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<char?, DerivedClass>("Nullable<Char>ToDerivedClass", '*', ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
