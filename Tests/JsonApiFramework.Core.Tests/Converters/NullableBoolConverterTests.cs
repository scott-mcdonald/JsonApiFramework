// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class NullableBoolConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullableBoolConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("Nullable<Bool>ToXXX",
                            // Simple Types
                            new ConvertGenericTest<bool?, bool>("Nullable<Bool>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<bool?, bool>("Nullable<Bool>ToBool", true, ConvertResult.Success, true),
                            new ConvertGenericTest<bool?, byte>("Nullable<Bool>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<bool?, byte>("Nullable<Bool>ToByte", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, byte[]>("Nullable<Bool>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<bool?, byte[]>("Nullable<Bool>ToByteArray", true, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<bool?, char>("Nullable<Bool>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<bool?, char>("Nullable<Bool>ToChar", true, ConvertResult.Success, (char)1),
                            new ConvertGenericTest<bool?, DateTime>("Nullable<Bool>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<bool?, DateTime>("Nullable<Bool>ToDateTime", true, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<bool?, DateTimeOffset>("Nullable<Bool>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<bool?, DateTimeOffset>("Nullable<Bool>ToDateTimeOffset", true, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<bool?, decimal>("Nullable<Bool>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<bool?, decimal>("Nullable<Bool>ToDecimal", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, double>("Nullable<Bool>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<bool?, double>("Nullable<Bool>ToDouble", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, PrimaryColor>("Nullable<Bool>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<bool?, PrimaryColor>("Nullable<Bool>ToEnum", true, ConvertResult.Success, PrimaryColor.Green),
                            new ConvertGenericTest<bool?, float>("Nullable<Bool>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<bool?, float>("Nullable<Bool>ToFloat", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, Guid>("Nullable<Bool>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<bool?, Guid>("Nullable<Bool>ToGuid", true, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<bool?, int>("Nullable<Bool>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<bool?, int>("Nullable<Bool>ToInt", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, long>("Nullable<Bool>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<bool?, long>("Nullable<Bool>ToLong", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, sbyte>("Nullable<Bool>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<bool?, sbyte>("Nullable<Bool>ToSByte", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, short>("Nullable<Bool>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<bool?, short>("Nullable<Bool>ToShort", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, string>("Nullable<Bool>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<bool?, string>("Nullable<Bool>ToString", true, ConvertResult.Success, "True"),
                            new ConvertGenericTest<bool?, TimeSpan>("Nullable<Bool>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<bool?, TimeSpan>("Nullable<Bool>ToTimeSpan", true, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<bool?, Type>("Nullable<Bool>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<bool?, Type>("Nullable<Bool>ToType", true, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<bool?, uint>("Nullable<Bool>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<bool?, uint>("Nullable<Bool>ToUInt", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, ulong>("Nullable<Bool>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<bool?, ulong>("Nullable<Bool>ToULong", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, Uri>("Nullable<Bool>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<bool?, Uri>("Nullable<Bool>ToUri", true, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<bool?, ushort>("Nullable<Bool>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<bool?, ushort>("Nullable<Bool>ToUShort", true, ConvertResult.Success, 1),

                            // Nullable Types
                            new ConvertGenericTest<bool?, bool?>("Nullable<Bool>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<bool?, bool?>("Nullable<Bool>ToNullable<Bool>", true, ConvertResult.Success, true),
                            new ConvertGenericTest<bool?, byte?>("Nullable<Bool>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<bool?, byte?>("Nullable<Bool>ToNullable<Byte>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, char?>("Nullable<Bool>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<bool?, char?>("Nullable<Bool>ToNullable<Char>", true, ConvertResult.Success, (char)1),
                            new ConvertGenericTest<bool?, DateTime?>("Nullable<Bool>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<bool?, DateTime?>("Nullable<Bool>ToNullable<DateTime>", true, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<bool?, DateTimeOffset?>("Nullable<Bool>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<bool?, DateTimeOffset?>("Nullable<Bool>ToNullable<DateTimeOffset>", true, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<bool?, decimal?>("Nullable<Bool>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<bool?, decimal?>("Nullable<Bool>ToNullable<Decimal>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, double?>("Nullable<Bool>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<bool?, double?>("Nullable<Bool>ToNullable<Double>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, PrimaryColor?>("Nullable<Bool>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<bool?, PrimaryColor?>("Nullable<Bool>ToNullable<Enum>", true, ConvertResult.Success, PrimaryColor.Green),
                            new ConvertGenericTest<bool?, float?>("Nullable<Bool>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<bool?, float?>("Nullable<Bool>ToNullable<Float>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, Guid?>("Nullable<Bool>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<bool?, Guid?>("Nullable<Bool>ToNullable<Guid>", true, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<bool?, int?>("Nullable<Bool>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<bool?, int?>("Nullable<Bool>ToNullable<Int>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, long?>("Nullable<Bool>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<bool?, long?>("Nullable<Bool>ToNullable<Long>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, sbyte?>("Nullable<Bool>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<bool?, sbyte?>("Nullable<Bool>ToNullable<SByte>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, short?>("Nullable<Bool>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<bool?, short?>("Nullable<Bool>ToNullable<Short>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, TimeSpan?>("Nullable<Bool>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<bool?, TimeSpan?>("Nullable<Bool>ToNullable<TimeSpan>", true, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<bool?, uint?>("Nullable<Bool>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<bool?, uint?>("Nullable<Bool>ToNullable<UInt>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, ulong?>("Nullable<Bool>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<bool?, ulong?>("Nullable<Bool>ToNullable<ULong>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, ushort?>("Nullable<Bool>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<bool?, ushort?>("Nullable<Bool>ToNullable<UShort>", true, ConvertResult.Success, 1),

                            // Interface/Class Types
                            new ConvertGenericTest<bool?, IInterface>("Nullable<Bool>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<bool?, IInterface>("Nullable<Bool>ToInterface", true, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<bool?, BaseClass>("Nullable<Bool>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<bool?, BaseClass>("Nullable<Bool>ToBaseClass", true, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<bool?, DerivedClass>("Nullable<Bool>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<bool?, DerivedClass>("Nullable<Bool>ToDerivedClass", true, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("Nullable<Bool>ToXXX",
                            // Simple Types
                            new TryConvertGenericTest<bool?, bool>("Nullable<Bool>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<bool?, bool>("Nullable<Bool>ToBool", true, ConvertResult.Success, true),
                            new TryConvertGenericTest<bool?, byte>("Nullable<Bool>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<bool?, byte>("Nullable<Bool>ToByte", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, byte[]>("Nullable<Bool>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<bool?, byte[]>("Nullable<Bool>ToByteArray", true, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<bool?, char>("Nullable<Bool>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<bool?, char>("Nullable<Bool>ToChar", true, ConvertResult.Success, (char)1),
                            new TryConvertGenericTest<bool?, DateTime>("Nullable<Bool>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<bool?, DateTime>("Nullable<Bool>ToDateTime", true, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<bool?, DateTimeOffset>("Nullable<Bool>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<bool?, DateTimeOffset>("Nullable<Bool>ToDateTimeOffset", true, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<bool?, decimal>("Nullable<Bool>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<bool?, decimal>("Nullable<Bool>ToDecimal", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, double>("Nullable<Bool>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<bool?, double>("Nullable<Bool>ToDouble", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, PrimaryColor>("Nullable<Bool>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<bool?, PrimaryColor>("Nullable<Bool>ToEnum", true, ConvertResult.Success, PrimaryColor.Green),
                            new TryConvertGenericTest<bool?, float>("Nullable<Bool>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<bool?, float>("Nullable<Bool>ToFloat", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, Guid>("Nullable<Bool>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<bool?, Guid>("Nullable<Bool>ToGuid", true, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<bool?, int>("Nullable<Bool>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<bool?, int>("Nullable<Bool>ToInt", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, long>("Nullable<Bool>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<bool?, long>("Nullable<Bool>ToLong", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, sbyte>("Nullable<Bool>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<bool?, sbyte>("Nullable<Bool>ToSByte", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, short>("Nullable<Bool>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<bool?, short>("Nullable<Bool>ToShort", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, string>("Nullable<Bool>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<bool?, string>("Nullable<Bool>ToString", true, ConvertResult.Success, "True"),
                            new TryConvertGenericTest<bool?, TimeSpan>("Nullable<Bool>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<bool?, TimeSpan>("Nullable<Bool>ToTimeSpan", true, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<bool?, Type>("Nullable<Bool>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<bool?, Type>("Nullable<Bool>ToType", true, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<bool?, uint>("Nullable<Bool>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<bool?, uint>("Nullable<Bool>ToUInt", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, ulong>("Nullable<Bool>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<bool?, ulong>("Nullable<Bool>ToULong", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, Uri>("Nullable<Bool>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<bool?, Uri>("Nullable<Bool>ToUri", true, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<bool?, ushort>("Nullable<Bool>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<bool?, ushort>("Nullable<Bool>ToUShort", true, ConvertResult.Success, 1),

                            // Nullable Types
                            new TryConvertGenericTest<bool?, bool?>("Nullable<Bool>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<bool?, bool?>("Nullable<Bool>ToNullable<Bool>", true, ConvertResult.Success, true),
                            new TryConvertGenericTest<bool?, byte?>("Nullable<Bool>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<bool?, byte?>("Nullable<Bool>ToNullable<Byte>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, char?>("Nullable<Bool>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<bool?, char?>("Nullable<Bool>ToNullable<Char>", true, ConvertResult.Success, (char)1),
                            new TryConvertGenericTest<bool?, DateTime?>("Nullable<Bool>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<bool?, DateTime?>("Nullable<Bool>ToNullable<DateTime>", true, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<bool?, DateTimeOffset?>("Nullable<Bool>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<bool?, DateTimeOffset?>("Nullable<Bool>ToNullable<DateTimeOffset>", true, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<bool?, decimal?>("Nullable<Bool>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<bool?, decimal?>("Nullable<Bool>ToNullable<Decimal>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, double?>("Nullable<Bool>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<bool?, double?>("Nullable<Bool>ToNullable<Double>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, PrimaryColor?>("Nullable<Bool>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<bool?, PrimaryColor?>("Nullable<Bool>ToNullable<Enum>", true, ConvertResult.Success, PrimaryColor.Green),
                            new TryConvertGenericTest<bool?, float?>("Nullable<Bool>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<bool?, float?>("Nullable<Bool>ToNullable<Float>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, Guid?>("Nullable<Bool>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<bool?, Guid?>("Nullable<Bool>ToNullable<Guid>", true, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<bool?, int?>("Nullable<Bool>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<bool?, int?>("Nullable<Bool>ToNullable<Int>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, long?>("Nullable<Bool>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<bool?, long?>("Nullable<Bool>ToNullable<Long>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, sbyte?>("Nullable<Bool>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<bool?, sbyte?>("Nullable<Bool>ToNullable<SByte>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, short?>("Nullable<Bool>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<bool?, short?>("Nullable<Bool>ToNullable<Short>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, TimeSpan?>("Nullable<Bool>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<bool?, TimeSpan?>("Nullable<Bool>ToNullable<TimeSpan>", true, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<bool?, uint?>("Nullable<Bool>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<bool?, uint?>("Nullable<Bool>ToNullable<UInt>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, ulong?>("Nullable<Bool>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<bool?, ulong?>("Nullable<Bool>ToNullable<ULong>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, ushort?>("Nullable<Bool>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<bool?, ushort?>("Nullable<Bool>ToNullable<UShort>", true, ConvertResult.Success, 1),

                            // Interface/Class Types
                            new TryConvertGenericTest<bool?, IInterface>("Nullable<Bool>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<bool?, IInterface>("Nullable<Bool>ToInterface", true, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<bool?, BaseClass>("Nullable<Bool>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<bool?, BaseClass>("Nullable<Bool>ToBaseClass", true, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<bool?, DerivedClass>("Nullable<Bool>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<bool?, DerivedClass>("Nullable<Bool>ToDerivedClass", true, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
