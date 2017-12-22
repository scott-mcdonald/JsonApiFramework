// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class DerivedClassConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DerivedClassConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("DerivedClassToXXX",
                            // Simple Types
                            new ConvertGenericTest<DerivedClass, bool>("DerivedClassToBool", TestDerivedClass, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DerivedClass, bool>("DerivedClassToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DerivedClass, byte>("DerivedClassToByte", TestDerivedClass, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DerivedClass, byte>("DerivedClassToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DerivedClass, byte[]>("DerivedClassToByteArray", TestDerivedClass, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DerivedClass, byte[]>("DerivedClassToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DerivedClass, char>("DerivedClassToChar", TestDerivedClass, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DerivedClass, char>("DerivedClassToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DerivedClass, DateTime>("DerivedClassToDateTime", TestDerivedClass, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<DerivedClass, DateTime>("DerivedClassToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<DerivedClass, DateTimeOffset>("DerivedClassToDateTimeOffset", TestDerivedClass, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<DerivedClass, DateTimeOffset>("DerivedClassToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<DerivedClass, decimal>("DerivedClassToDecimal", TestDerivedClass, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DerivedClass, decimal>("DerivedClassToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DerivedClass, double>("DerivedClassToDouble", TestDerivedClass, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DerivedClass, double>("DerivedClassToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DerivedClass, PrimaryColor>("DerivedClassToEnum", TestDerivedClass, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DerivedClass, PrimaryColor>("DerivedClassToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DerivedClass, float>("DerivedClassToFloat", TestDerivedClass, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DerivedClass, float>("DerivedClassToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DerivedClass, Guid>("DerivedClassToGuid", TestDerivedClass, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DerivedClass, Guid>("DerivedClassToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DerivedClass, int>("DerivedClassToInt", TestDerivedClass, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DerivedClass, int>("DerivedClassToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DerivedClass, long>("DerivedClassToLong", TestDerivedClass, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DerivedClass, long>("DerivedClassToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DerivedClass, sbyte>("DerivedClassToSByte", TestDerivedClass, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DerivedClass, sbyte>("DerivedClassToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DerivedClass, short>("DerivedClassToShort", TestDerivedClass, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DerivedClass, short>("DerivedClassToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DerivedClass, string>("DerivedClassToString", TestDerivedClass, ConvertResult.Failure, default(string)),
                            new ConvertGenericTest<DerivedClass, string>("DerivedClassToString", null, ConvertResult.Failure, default(string)),
                            new ConvertGenericTest<DerivedClass, TimeSpan>("DerivedClassToTimeSpan", TestDerivedClass, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DerivedClass, TimeSpan>("DerivedClassToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DerivedClass, Type>("DerivedClassToType", TestDerivedClass, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DerivedClass, Type>("DerivedClassToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DerivedClass, uint>("DerivedClassToUInt", TestDerivedClass, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DerivedClass, uint>("DerivedClassToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DerivedClass, ulong>("DerivedClassToULong", TestDerivedClass, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DerivedClass, ulong>("DerivedClassToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DerivedClass, Uri>("DerivedClassToUri", TestDerivedClass, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DerivedClass, Uri>("DerivedClassToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DerivedClass, ushort>("DerivedClassToUShort", TestDerivedClass, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<DerivedClass, ushort>("DerivedClassToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DerivedClass, bool?>("DerivedClassToNullable<Bool>", TestDerivedClass, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DerivedClass, bool?>("DerivedClassToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DerivedClass, byte?>("DerivedClassToNullable<Byte>", TestDerivedClass, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DerivedClass, byte?>("DerivedClassToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DerivedClass, char?>("DerivedClassToNullable<Char>", TestDerivedClass, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DerivedClass, char?>("DerivedClassToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DerivedClass, DateTime?>("DerivedClassToNullable<DateTime>", TestDerivedClass, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<DerivedClass, DateTime?>("DerivedClassToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<DerivedClass, DateTimeOffset?>("DerivedClassToNullable<DateTimeOffset>", TestDerivedClass, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<DerivedClass, DateTimeOffset?>("DerivedClassToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<DerivedClass, decimal?>("DerivedClassToNullable<Decimal>", TestDerivedClass, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DerivedClass, decimal?>("DerivedClassToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DerivedClass, double?>("DerivedClassToNullable<Double>", TestDerivedClass, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DerivedClass, double?>("DerivedClassToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DerivedClass, PrimaryColor?>("DerivedClassToNullable<Enum>", TestDerivedClass, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DerivedClass, PrimaryColor?>("DerivedClassToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DerivedClass, float?>("DerivedClassToNullable<Float>", TestDerivedClass, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DerivedClass, float?>("DerivedClassToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DerivedClass, Guid?>("DerivedClassToNullable<Guid>", TestDerivedClass, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<DerivedClass, Guid?>("DerivedClassToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<DerivedClass, int?>("DerivedClassToNullable<Int>", TestDerivedClass, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DerivedClass, int?>("DerivedClassToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DerivedClass, long?>("DerivedClassToNullable<Long>", TestDerivedClass, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DerivedClass, long?>("DerivedClassToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DerivedClass, sbyte?>("DerivedClassToNullable<SByte>", TestDerivedClass, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DerivedClass, sbyte?>("DerivedClassToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DerivedClass, short?>("DerivedClassToNullable<Short>", TestDerivedClass, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DerivedClass, short?>("DerivedClassToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DerivedClass, TimeSpan?>("DerivedClassToNullable<TimeSpan>", TestDerivedClass, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<DerivedClass, TimeSpan?>("DerivedClassToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<DerivedClass, uint?>("DerivedClassToNullable<UInt>", TestDerivedClass, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DerivedClass, uint?>("DerivedClassToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DerivedClass, ulong?>("DerivedClassToNullable<ULong>", TestDerivedClass, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DerivedClass, ulong?>("DerivedClassToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DerivedClass, ushort?>("DerivedClassToNullable<UShort>", TestDerivedClass, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<DerivedClass, ushort?>("DerivedClassToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DerivedClass, IInterface>("DerivedClassToInterface", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new ConvertGenericTest<DerivedClass, IInterface>("DerivedClassToInterface", null, ConvertResult.Success, default(IInterface)),
                            new ConvertGenericTest<DerivedClass, BaseClass>("DerivedClassToBaseClass", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new ConvertGenericTest<DerivedClass, BaseClass>("DerivedClassToBaseClass", null, ConvertResult.Success, default(BaseClass)),
                            new ConvertGenericTest<DerivedClass, DerivedClass>("DerivedClassToDerivedClass", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new ConvertGenericTest<DerivedClass, DerivedClass>("DerivedClassToDerivedClass", null, ConvertResult.Success, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("DerivedClassToXXX",
                            // Simple Types
                            new TryConvertGenericTest<DerivedClass, bool>("DerivedClassToBool", TestDerivedClass, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DerivedClass, bool>("DerivedClassToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DerivedClass, byte>("DerivedClassToByte", TestDerivedClass, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DerivedClass, byte>("DerivedClassToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DerivedClass, byte[]>("DerivedClassToByteArray", TestDerivedClass, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DerivedClass, byte[]>("DerivedClassToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DerivedClass, char>("DerivedClassToChar", TestDerivedClass, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DerivedClass, char>("DerivedClassToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DerivedClass, DateTime>("DerivedClassToDateTime", TestDerivedClass, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<DerivedClass, DateTime>("DerivedClassToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<DerivedClass, DateTimeOffset>("DerivedClassToDateTimeOffset", TestDerivedClass, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<DerivedClass, DateTimeOffset>("DerivedClassToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<DerivedClass, decimal>("DerivedClassToDecimal", TestDerivedClass, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DerivedClass, decimal>("DerivedClassToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DerivedClass, double>("DerivedClassToDouble", TestDerivedClass, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DerivedClass, double>("DerivedClassToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DerivedClass, PrimaryColor>("DerivedClassToEnum", TestDerivedClass, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DerivedClass, PrimaryColor>("DerivedClassToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DerivedClass, float>("DerivedClassToFloat", TestDerivedClass, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DerivedClass, float>("DerivedClassToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DerivedClass, Guid>("DerivedClassToGuid", TestDerivedClass, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DerivedClass, Guid>("DerivedClassToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DerivedClass, int>("DerivedClassToInt", TestDerivedClass, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DerivedClass, int>("DerivedClassToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DerivedClass, long>("DerivedClassToLong", TestDerivedClass, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DerivedClass, long>("DerivedClassToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DerivedClass, sbyte>("DerivedClassToSByte", TestDerivedClass, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DerivedClass, sbyte>("DerivedClassToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DerivedClass, short>("DerivedClassToShort", TestDerivedClass, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DerivedClass, short>("DerivedClassToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DerivedClass, string>("DerivedClassToString", TestDerivedClass, ConvertResult.Failure, default(string)),
                            new TryConvertGenericTest<DerivedClass, string>("DerivedClassToString", null, ConvertResult.Failure, default(string)),
                            new TryConvertGenericTest<DerivedClass, TimeSpan>("DerivedClassToTimeSpan", TestDerivedClass, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DerivedClass, TimeSpan>("DerivedClassToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DerivedClass, Type>("DerivedClassToType", TestDerivedClass, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DerivedClass, Type>("DerivedClassToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DerivedClass, uint>("DerivedClassToUInt", TestDerivedClass, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DerivedClass, uint>("DerivedClassToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DerivedClass, ulong>("DerivedClassToULong", TestDerivedClass, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DerivedClass, ulong>("DerivedClassToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DerivedClass, Uri>("DerivedClassToUri", TestDerivedClass, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DerivedClass, Uri>("DerivedClassToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DerivedClass, ushort>("DerivedClassToUShort", TestDerivedClass, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<DerivedClass, ushort>("DerivedClassToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DerivedClass, bool?>("DerivedClassToNullable<Bool>", TestDerivedClass, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DerivedClass, bool?>("DerivedClassToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DerivedClass, byte?>("DerivedClassToNullable<Byte>", TestDerivedClass, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DerivedClass, byte?>("DerivedClassToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DerivedClass, char?>("DerivedClassToNullable<Char>", TestDerivedClass, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DerivedClass, char?>("DerivedClassToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DerivedClass, DateTime?>("DerivedClassToNullable<DateTime>", TestDerivedClass, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<DerivedClass, DateTime?>("DerivedClassToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<DerivedClass, DateTimeOffset?>("DerivedClassToNullable<DateTimeOffset>", TestDerivedClass, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<DerivedClass, DateTimeOffset?>("DerivedClassToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<DerivedClass, decimal?>("DerivedClassToNullable<Decimal>", TestDerivedClass, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DerivedClass, decimal?>("DerivedClassToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DerivedClass, double?>("DerivedClassToNullable<Double>", TestDerivedClass, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DerivedClass, double?>("DerivedClassToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DerivedClass, PrimaryColor?>("DerivedClassToNullable<Enum>", TestDerivedClass, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DerivedClass, PrimaryColor?>("DerivedClassToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DerivedClass, float?>("DerivedClassToNullable<Float>", TestDerivedClass, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DerivedClass, float?>("DerivedClassToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DerivedClass, Guid?>("DerivedClassToNullable<Guid>", TestDerivedClass, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<DerivedClass, Guid?>("DerivedClassToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<DerivedClass, int?>("DerivedClassToNullable<Int>", TestDerivedClass, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DerivedClass, int?>("DerivedClassToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DerivedClass, long?>("DerivedClassToNullable<Long>", TestDerivedClass, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DerivedClass, long?>("DerivedClassToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DerivedClass, sbyte?>("DerivedClassToNullable<SByte>", TestDerivedClass, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DerivedClass, sbyte?>("DerivedClassToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DerivedClass, short?>("DerivedClassToNullable<Short>", TestDerivedClass, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DerivedClass, short?>("DerivedClassToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DerivedClass, TimeSpan?>("DerivedClassToNullable<TimeSpan>", TestDerivedClass, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<DerivedClass, TimeSpan?>("DerivedClassToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<DerivedClass, uint?>("DerivedClassToNullable<UInt>", TestDerivedClass, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DerivedClass, uint?>("DerivedClassToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DerivedClass, ulong?>("DerivedClassToNullable<ULong>", TestDerivedClass, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DerivedClass, ulong?>("DerivedClassToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DerivedClass, ushort?>("DerivedClassToNullable<UShort>", TestDerivedClass, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<DerivedClass, ushort?>("DerivedClassToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DerivedClass, IInterface>("DerivedClassToInterface", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new TryConvertGenericTest<DerivedClass, IInterface>("DerivedClassToInterface", null, ConvertResult.Success, default(IInterface)),
                            new TryConvertGenericTest<DerivedClass, BaseClass>("DerivedClassToBaseClass", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new TryConvertGenericTest<DerivedClass, BaseClass>("DerivedClassToBaseClass", null, ConvertResult.Success, default(BaseClass)),
                            new TryConvertGenericTest<DerivedClass, DerivedClass>("DerivedClassToDerivedClass", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new TryConvertGenericTest<DerivedClass, DerivedClass>("DerivedClassToDerivedClass", null, ConvertResult.Success, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
