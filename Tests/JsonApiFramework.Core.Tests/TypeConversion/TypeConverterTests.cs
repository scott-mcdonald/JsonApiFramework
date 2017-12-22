// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class TypeConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("TypeToXXX",
                            // Simple Types
                            new ConvertGenericTest<Type, bool>("TypeToBool", TestType, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Type, bool>("TypeToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Type, byte>("TypeToByte", TestType, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Type, byte>("TypeToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Type, byte[]>("TypeToByteArray", TestType, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<Type, byte[]>("TypeToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<Type, char>("TypeToChar", TestType, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Type, char>("TypeToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Type, DateTime>("TypeToDateTime", TestType, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Type, DateTime>("TypeToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Type, DateTimeOffset>("TypeToDateTimeOffset", TestType, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Type, DateTimeOffset>("TypeToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Type, decimal>("TypeToDecimal", TestType, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Type, decimal>("TypeToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Type, double>("TypeToDouble", TestType, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Type, double>("TypeToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Type, PrimaryColor>("TypeToEnum", TestType, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Type, PrimaryColor>("TypeToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Type, float>("TypeToFloat", TestType, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Type, float>("TypeToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Type, Guid>("TypeToGuid", TestType, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<Type, Guid>("TypeToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<Type, int>("TypeToInt", TestType, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Type, int>("TypeToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Type, long>("TypeToLong", TestType, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Type, long>("TypeToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Type, sbyte>("TypeToSByte", TestType, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Type, sbyte>("TypeToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Type, short>("TypeToShort", TestType, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Type, short>("TypeToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Type, string>("TypeToString", TestType, ConvertResult.Success, TestTypeString),
                            new ConvertGenericTest<Type, string>("TypeToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<Type, TimeSpan>("TypeToTimeSpan", TestType, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Type, TimeSpan>("TypeToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Type, Type>("TypeToType", TestType, ConvertResult.Success, TestType),
                            new ConvertGenericTest<Type, Type>("TypeToType", null, ConvertResult.Success, default(Type)),
                            new ConvertGenericTest<Type, uint>("TypeToUInt", TestType, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Type, uint>("TypeToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Type, ulong>("TypeToULong", TestType, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Type, ulong>("TypeToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Type, Uri>("TypeToUri", TestType, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Type, Uri>("TypeToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Type, ushort>("TypeToUShort", TestType, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<Type, ushort>("TypeToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<Type, bool?>("TypeToNullable<Bool>", TestType, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Type, bool?>("TypeToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Type, byte?>("TypeToNullable<Byte>", TestType, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Type, byte?>("TypeToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Type, char?>("TypeToNullable<Char>", TestType, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Type, char?>("TypeToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Type, DateTime?>("TypeToNullable<DateTime>", TestType, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Type, DateTime?>("TypeToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Type, DateTimeOffset?>("TypeToNullable<DateTimeOffset>", TestType, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Type, DateTimeOffset?>("TypeToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Type, decimal?>("TypeToNullable<Decimal>", TestType, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Type, decimal?>("TypeToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Type, double?>("TypeToNullable<Double>", TestType, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Type, double?>("TypeToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Type, PrimaryColor?>("TypeToNullable<Enum>", TestType, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Type, PrimaryColor?>("TypeToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Type, float?>("TypeToNullable<Float>", TestType, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Type, float?>("TypeToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Type, Guid?>("TypeToNullable<Guid>", TestType, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<Type, Guid?>("TypeToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<Type, int?>("TypeToNullable<Int>", TestType, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Type, int?>("TypeToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Type, long?>("TypeToNullable<Long>", TestType, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Type, long?>("TypeToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Type, sbyte?>("TypeToNullable<SByte>", TestType, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Type, sbyte?>("TypeToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Type, short?>("TypeToNullable<Short>", TestType, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Type, short?>("TypeToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Type, TimeSpan?>("TypeToNullable<TimeSpan>", TestType, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Type, TimeSpan?>("TypeToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Type, uint?>("TypeToNullable<UInt>", TestType, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Type, uint?>("TypeToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Type, ulong?>("TypeToNullable<ULong>", TestType, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Type, ulong?>("TypeToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Type, ushort?>("TypeToNullable<UShort>", TestType, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<Type, ushort?>("TypeToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<Type, IInterface>("TypeToInterface", TestType, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Type, IInterface>("TypeToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Type, BaseClass>("TypeToBaseClass", TestType, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Type, BaseClass>("TypeToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Type, DerivedClass>("TypeToDerivedClass", TestType, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<Type, DerivedClass>("TypeToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("TypeToXXX",
                            // Simple Types
                            new TryConvertGenericTest<Type, bool>("TypeToBool", TestType, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Type, bool>("TypeToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Type, byte>("TypeToByte", TestType, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Type, byte>("TypeToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Type, byte[]>("TypeToByteArray", TestType, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<Type, byte[]>("TypeToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<Type, char>("TypeToChar", TestType, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Type, char>("TypeToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Type, DateTime>("TypeToDateTime", TestType, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Type, DateTime>("TypeToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Type, DateTimeOffset>("TypeToDateTimeOffset", TestType, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Type, DateTimeOffset>("TypeToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Type, decimal>("TypeToDecimal", TestType, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Type, decimal>("TypeToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Type, double>("TypeToDouble", TestType, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Type, double>("TypeToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Type, PrimaryColor>("TypeToEnum", TestType, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Type, PrimaryColor>("TypeToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Type, float>("TypeToFloat", TestType, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Type, float>("TypeToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Type, Guid>("TypeToGuid", TestType, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<Type, Guid>("TypeToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<Type, int>("TypeToInt", TestType, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Type, int>("TypeToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Type, long>("TypeToLong", TestType, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Type, long>("TypeToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Type, sbyte>("TypeToSByte", TestType, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Type, sbyte>("TypeToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Type, short>("TypeToShort", TestType, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Type, short>("TypeToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Type, string>("TypeToString", TestType, ConvertResult.Success, TestTypeString),
                            new TryConvertGenericTest<Type, string>("TypeToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<Type, TimeSpan>("TypeToTimeSpan", TestType, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Type, TimeSpan>("TypeToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Type, Type>("TypeToType", TestType, ConvertResult.Success, TestType),
                            new TryConvertGenericTest<Type, Type>("TypeToType", null, ConvertResult.Success, default(Type)),
                            new TryConvertGenericTest<Type, uint>("TypeToUInt", TestType, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Type, uint>("TypeToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Type, ulong>("TypeToULong", TestType, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Type, ulong>("TypeToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Type, Uri>("TypeToUri", TestType, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Type, Uri>("TypeToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Type, ushort>("TypeToUShort", TestType, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<Type, ushort>("TypeToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<Type, bool?>("TypeToNullable<Bool>", TestType, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Type, bool?>("TypeToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Type, byte?>("TypeToNullable<Byte>", TestType, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Type, byte?>("TypeToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Type, char?>("TypeToNullable<Char>", TestType, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Type, char?>("TypeToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Type, DateTime?>("TypeToNullable<DateTime>", TestType, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Type, DateTime?>("TypeToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Type, DateTimeOffset?>("TypeToNullable<DateTimeOffset>", TestType, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Type, DateTimeOffset?>("TypeToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Type, decimal?>("TypeToNullable<Decimal>", TestType, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Type, decimal?>("TypeToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Type, double?>("TypeToNullable<Double>", TestType, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Type, double?>("TypeToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Type, PrimaryColor?>("TypeToNullable<Enum>", TestType, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Type, PrimaryColor?>("TypeToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Type, float?>("TypeToNullable<Float>", TestType, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Type, float?>("TypeToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Type, Guid?>("TypeToNullable<Guid>", TestType, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<Type, Guid?>("TypeToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<Type, int?>("TypeToNullable<Int>", TestType, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Type, int?>("TypeToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Type, long?>("TypeToNullable<Long>", TestType, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Type, long?>("TypeToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Type, sbyte?>("TypeToNullable<SByte>", TestType, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Type, sbyte?>("TypeToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Type, short?>("TypeToNullable<Short>", TestType, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Type, short?>("TypeToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Type, TimeSpan?>("TypeToNullable<TimeSpan>", TestType, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Type, TimeSpan?>("TypeToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Type, uint?>("TypeToNullable<UInt>", TestType, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Type, uint?>("TypeToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Type, ulong?>("TypeToNullable<ULong>", TestType, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Type, ulong?>("TypeToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Type, ushort?>("TypeToNullable<UShort>", TestType, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<Type, ushort?>("TypeToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<Type, IInterface>("TypeToInterface", TestType, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Type, IInterface>("TypeToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Type, BaseClass>("TypeToBaseClass", TestType, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Type, BaseClass>("TypeToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Type, DerivedClass>("TypeToDerivedClass", TestType, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<Type, DerivedClass>("TypeToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
