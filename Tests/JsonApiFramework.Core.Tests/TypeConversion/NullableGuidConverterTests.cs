// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class NullableGuidConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableGuidConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Guid>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<Guid?, bool>("Nullable<Guid>ToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Guid?, bool>("Nullable<Guid>ToBool", TestGuid, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Guid?, byte>("Nullable<Guid>ToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Guid?, byte>("Nullable<Guid>ToByte", TestGuid, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Guid?, byte[]>("Nullable<Guid>ToByteArray", null, ConvertResult.Success, default(byte[])),
                            new ConvertGenericTest<Guid?, byte[]>("Nullable<Guid>ToByteArray", TestGuid, ConvertResult.Success, TestGuidByteArray),
                            new ConvertGenericTest<Guid?, char>("Nullable<Guid>ToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Guid?, char>("Nullable<Guid>ToChar", TestGuid, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Guid?, DateTime>("Nullable<Guid>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Guid?, DateTime>("Nullable<Guid>ToDateTime", TestGuid, ConvertResult.Failure, TestDateTime),
                            new ConvertGenericTest<Guid?, DateTimeOffset>("Nullable<Guid>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Guid?, DateTimeOffset>("Nullable<Guid>ToDateTimeOffset", TestGuid, ConvertResult.Failure, TestDateTimeOffset),
                            new ConvertGenericTest<Guid?, decimal>("Nullable<Guid>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Guid?, decimal>("Nullable<Guid>ToDecimal", TestGuid, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Guid?, double>("Nullable<Guid>ToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Guid?, double>("Nullable<Guid>ToDouble", TestGuid, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Guid?, PrimaryColor>("Nullable<Guid>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Guid?, PrimaryColor>("Nullable<Guid>ToEnum", TestGuid, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Guid?, float>("Nullable<Guid>ToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Guid?, float>("Nullable<Guid>ToFloat", TestGuid, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Guid?, Guid>("Nullable<Guid>ToGuid", null, ConvertResult.Success, default(Guid)),
                            new ConvertGenericTest<Guid?, Guid>("Nullable<Guid>ToGuid", TestGuid, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<Guid?, int>("Nullable<Guid>ToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Guid?, int>("Nullable<Guid>ToInt", TestGuid, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Guid?, long>("Nullable<Guid>ToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Guid?, long>("Nullable<Guid>ToLong", TestGuid, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Guid?, sbyte>("Nullable<Guid>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Guid?, sbyte>("Nullable<Guid>ToSByte", TestGuid, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Guid?, short>("Nullable<Guid>ToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Guid?, short>("Nullable<Guid>ToShort", TestGuid, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Guid?, string>("Nullable<Guid>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<Guid?, string>("Nullable<Guid>ToString", TestGuid, ConvertResult.Success, TestGuidString),
                            new ConvertGenericTest<Guid?, TimeSpan>("Nullable<Guid>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Guid?, TimeSpan>("Nullable<Guid>ToTimeSpan", TestGuid, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Guid?, Type>("Nullable<Guid>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Guid?, Type>("Nullable<Guid>ToType", TestGuid, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Guid?, uint>("Nullable<Guid>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Guid?, uint>("Nullable<Guid>ToUInt", TestGuid, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Guid?, ulong>("Nullable<Guid>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Guid?, ulong>("Nullable<Guid>ToULong", TestGuid, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Guid?, Uri>("Nullable<Guid>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Guid?, Uri>("Nullable<Guid>ToUri", TestGuid, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Guid?, ushort>("Nullable<Guid>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<Guid?, ushort>("Nullable<Guid>ToUShort", TestGuid, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<Guid?, bool?>("Nullable<Guid>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Guid?, bool?>("Nullable<Guid>ToNullable<Bool>", TestGuid, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Guid?, byte?>("Nullable<Guid>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Guid?, byte?>("Nullable<Guid>ToNullable<Byte>", TestGuid, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Guid?, char?>("Nullable<Guid>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Guid?, char?>("Nullable<Guid>ToNullable<Char>", TestGuid, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Guid?, DateTime?>("Nullable<Guid>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<Guid?, DateTime?>("Nullable<Guid>ToNullable<DateTime>", TestGuid, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<Guid?, DateTimeOffset?>("Nullable<Guid>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<Guid?, DateTimeOffset?>("Nullable<Guid>ToNullable<DateTimeOffset>", TestGuid, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<Guid?, decimal?>("Nullable<Guid>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Guid?, decimal?>("Nullable<Guid>ToNullable<Decimal>", TestGuid, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Guid?, double?>("Nullable<Guid>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Guid?, double?>("Nullable<Guid>ToNullable<Double>", TestGuid, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Guid?, PrimaryColor?>("Nullable<Guid>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Guid?, PrimaryColor?>("Nullable<Guid>ToNullable<Enum>", TestGuid, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Guid?, float?>("Nullable<Guid>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Guid?, float?>("Nullable<Guid>ToNullable<Float>", TestGuid, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Guid?, Guid?>("Nullable<Guid>ToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new ConvertGenericTest<Guid?, Guid?>("Nullable<Guid>ToNullable<Guid>", TestGuid, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<Guid?, int?>("Nullable<Guid>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Guid?, int?>("Nullable<Guid>ToNullable<Int>", TestGuid, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Guid?, long?>("Nullable<Guid>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Guid?, long?>("Nullable<Guid>ToNullable<Long>", TestGuid, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Guid?, sbyte?>("Nullable<Guid>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Guid?, sbyte?>("Nullable<Guid>ToNullable<SByte>", TestGuid, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Guid?, short?>("Nullable<Guid>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Guid?, short?>("Nullable<Guid>ToNullable<Short>", TestGuid, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Guid?, TimeSpan?>("Nullable<Guid>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<Guid?, TimeSpan?>("Nullable<Guid>ToNullable<TimeSpan>", TestGuid, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<Guid?, uint?>("Nullable<Guid>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Guid?, uint?>("Nullable<Guid>ToNullable<UInt>", TestGuid, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Guid?, ulong?>("Nullable<Guid>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Guid?, ulong?>("Nullable<Guid>ToNullable<ULong>", TestGuid, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Guid?, ushort?>("Nullable<Guid>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<Guid?, ushort?>("Nullable<Guid>ToNullable<UShort>", TestGuid, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<Guid?, IInterface>("Nullable<Guid>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Guid?, IInterface>("Nullable<Guid>ToInterface", TestGuid, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Guid?, BaseClass>("Nullable<Guid>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Guid?, BaseClass>("Nullable<Guid>ToBaseClass", TestGuid, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Guid?, DerivedClass>("Nullable<Guid>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<Guid?, DerivedClass>("Nullable<Guid>ToDerivedClass", TestGuid, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Guid>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<Guid?, bool>("Nullable<Guid>ToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Guid?, bool>("Nullable<Guid>ToBool", TestGuid, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Guid?, byte>("Nullable<Guid>ToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Guid?, byte>("Nullable<Guid>ToByte", TestGuid, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Guid?, byte[]>("Nullable<Guid>ToByteArray", null, ConvertResult.Success, default(byte[])),
                            new TryConvertGenericTest<Guid?, byte[]>("Nullable<Guid>ToByteArray", TestGuid, ConvertResult.Success, TestGuidByteArray),
                            new TryConvertGenericTest<Guid?, char>("Nullable<Guid>ToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Guid?, char>("Nullable<Guid>ToChar", TestGuid, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Guid?, DateTime>("Nullable<Guid>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Guid?, DateTime>("Nullable<Guid>ToDateTime", TestGuid, ConvertResult.Failure, TestDateTime),
                            new TryConvertGenericTest<Guid?, DateTimeOffset>("Nullable<Guid>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Guid?, DateTimeOffset>("Nullable<Guid>ToDateTimeOffset", TestGuid, ConvertResult.Failure, TestDateTimeOffset),
                            new TryConvertGenericTest<Guid?, decimal>("Nullable<Guid>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Guid?, decimal>("Nullable<Guid>ToDecimal", TestGuid, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Guid?, double>("Nullable<Guid>ToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Guid?, double>("Nullable<Guid>ToDouble", TestGuid, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Guid?, PrimaryColor>("Nullable<Guid>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Guid?, PrimaryColor>("Nullable<Guid>ToEnum", TestGuid, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Guid?, float>("Nullable<Guid>ToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Guid?, float>("Nullable<Guid>ToFloat", TestGuid, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Guid?, Guid>("Nullable<Guid>ToGuid", null, ConvertResult.Success, default(Guid)),
                            new TryConvertGenericTest<Guid?, Guid>("Nullable<Guid>ToGuid", TestGuid, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<Guid?, int>("Nullable<Guid>ToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Guid?, int>("Nullable<Guid>ToInt", TestGuid, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Guid?, long>("Nullable<Guid>ToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Guid?, long>("Nullable<Guid>ToLong", TestGuid, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Guid?, sbyte>("Nullable<Guid>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Guid?, sbyte>("Nullable<Guid>ToSByte", TestGuid, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Guid?, short>("Nullable<Guid>ToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Guid?, short>("Nullable<Guid>ToShort", TestGuid, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Guid?, string>("Nullable<Guid>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<Guid?, string>("Nullable<Guid>ToString", TestGuid, ConvertResult.Success, TestGuidString),
                            new TryConvertGenericTest<Guid?, TimeSpan>("Nullable<Guid>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Guid?, TimeSpan>("Nullable<Guid>ToTimeSpan", TestGuid, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Guid?, Type>("Nullable<Guid>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Guid?, Type>("Nullable<Guid>ToType", TestGuid, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Guid?, uint>("Nullable<Guid>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Guid?, uint>("Nullable<Guid>ToUInt", TestGuid, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Guid?, ulong>("Nullable<Guid>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Guid?, ulong>("Nullable<Guid>ToULong", TestGuid, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Guid?, Uri>("Nullable<Guid>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Guid?, Uri>("Nullable<Guid>ToUri", TestGuid, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Guid?, ushort>("Nullable<Guid>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<Guid?, ushort>("Nullable<Guid>ToUShort", TestGuid, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<Guid?, bool?>("Nullable<Guid>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Guid?, bool?>("Nullable<Guid>ToNullable<Bool>", TestGuid, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Guid?, byte?>("Nullable<Guid>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Guid?, byte?>("Nullable<Guid>ToNullable<Byte>", TestGuid, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Guid?, char?>("Nullable<Guid>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Guid?, char?>("Nullable<Guid>ToNullable<Char>", TestGuid, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Guid?, DateTime?>("Nullable<Guid>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<Guid?, DateTime?>("Nullable<Guid>ToNullable<DateTime>", TestGuid, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<Guid?, DateTimeOffset?>("Nullable<Guid>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<Guid?, DateTimeOffset?>("Nullable<Guid>ToNullable<DateTimeOffset>", TestGuid, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<Guid?, decimal?>("Nullable<Guid>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Guid?, decimal?>("Nullable<Guid>ToNullable<Decimal>", TestGuid, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Guid?, double?>("Nullable<Guid>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Guid?, double?>("Nullable<Guid>ToNullable<Double>", TestGuid, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Guid?, PrimaryColor?>("Nullable<Guid>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Guid?, PrimaryColor?>("Nullable<Guid>ToNullable<Enum>", TestGuid, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Guid?, float?>("Nullable<Guid>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Guid?, float?>("Nullable<Guid>ToNullable<Float>", TestGuid, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Guid?, Guid?>("Nullable<Guid>ToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new TryConvertGenericTest<Guid?, Guid?>("Nullable<Guid>ToNullable<Guid>", TestGuid, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<Guid?, int?>("Nullable<Guid>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Guid?, int?>("Nullable<Guid>ToNullable<Int>", TestGuid, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Guid?, long?>("Nullable<Guid>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Guid?, long?>("Nullable<Guid>ToNullable<Long>", TestGuid, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Guid?, sbyte?>("Nullable<Guid>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Guid?, sbyte?>("Nullable<Guid>ToNullable<SByte>", TestGuid, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Guid?, short?>("Nullable<Guid>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Guid?, short?>("Nullable<Guid>ToNullable<Short>", TestGuid, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Guid?, TimeSpan?>("Nullable<Guid>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<Guid?, TimeSpan?>("Nullable<Guid>ToNullable<TimeSpan>", TestGuid, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<Guid?, uint?>("Nullable<Guid>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Guid?, uint?>("Nullable<Guid>ToNullable<UInt>", TestGuid, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Guid?, ulong?>("Nullable<Guid>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Guid?, ulong?>("Nullable<Guid>ToNullable<ULong>", TestGuid, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Guid?, ushort?>("Nullable<Guid>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<Guid?, ushort?>("Nullable<Guid>ToNullable<UShort>", TestGuid, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<Guid?, IInterface>("Nullable<Guid>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Guid?, IInterface>("Nullable<Guid>ToInterface", TestGuid, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Guid?, BaseClass>("Nullable<Guid>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Guid?, BaseClass>("Nullable<Guid>ToBaseClass", TestGuid, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Guid?, DerivedClass>("Nullable<Guid>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<Guid?, DerivedClass>("Nullable<Guid>ToDerivedClass", TestGuid, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
