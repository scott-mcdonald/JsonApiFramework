// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class BaseClassConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public BaseClassConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("BaseClassToXXX",
                            // Simple Types
                            new ConvertGenericTest<BaseClass, bool>("BaseClassToBool", TestBaseClass, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<BaseClass, bool>("BaseClassToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<BaseClass, byte>("BaseClassToByte", TestBaseClass, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<BaseClass, byte>("BaseClassToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<BaseClass, byte[]>("BaseClassToByteArray", TestBaseClass, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<BaseClass, byte[]>("BaseClassToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<BaseClass, char>("BaseClassToChar", TestBaseClass, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<BaseClass, char>("BaseClassToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<BaseClass, DateTime>("BaseClassToDateTime", TestBaseClass, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<BaseClass, DateTime>("BaseClassToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<BaseClass, DateTimeOffset>("BaseClassToDateTimeOffset", TestBaseClass, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<BaseClass, DateTimeOffset>("BaseClassToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<BaseClass, decimal>("BaseClassToDecimal", TestBaseClass, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<BaseClass, decimal>("BaseClassToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<BaseClass, double>("BaseClassToDouble", TestBaseClass, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<BaseClass, double>("BaseClassToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<BaseClass, PrimaryColor>("BaseClassToEnum", TestBaseClass, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<BaseClass, PrimaryColor>("BaseClassToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<BaseClass, float>("BaseClassToFloat", TestBaseClass, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<BaseClass, float>("BaseClassToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<BaseClass, Guid>("BaseClassToGuid", TestBaseClass, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<BaseClass, Guid>("BaseClassToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<BaseClass, int>("BaseClassToInt", TestBaseClass, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<BaseClass, int>("BaseClassToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<BaseClass, long>("BaseClassToLong", TestBaseClass, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<BaseClass, long>("BaseClassToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<BaseClass, sbyte>("BaseClassToSByte", TestBaseClass, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<BaseClass, sbyte>("BaseClassToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<BaseClass, short>("BaseClassToShort", TestBaseClass, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<BaseClass, short>("BaseClassToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<BaseClass, string>("BaseClassToString", TestBaseClass, ConvertResult.Failure, default(string)),
                            new ConvertGenericTest<BaseClass, string>("BaseClassToString", null, ConvertResult.Failure, default(string)),
                            new ConvertGenericTest<BaseClass, TimeSpan>("BaseClassToTimeSpan", TestBaseClass, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<BaseClass, TimeSpan>("BaseClassToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<BaseClass, Type>("BaseClassToType", TestBaseClass, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<BaseClass, Type>("BaseClassToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<BaseClass, uint>("BaseClassToUInt", TestBaseClass, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<BaseClass, uint>("BaseClassToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<BaseClass, ulong>("BaseClassToULong", TestBaseClass, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<BaseClass, ulong>("BaseClassToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<BaseClass, Uri>("BaseClassToUri", TestBaseClass, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<BaseClass, Uri>("BaseClassToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<BaseClass, ushort>("BaseClassToUShort", TestBaseClass, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<BaseClass, ushort>("BaseClassToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<BaseClass, bool?>("BaseClassToNullable<Bool>", TestBaseClass, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<BaseClass, bool?>("BaseClassToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<BaseClass, byte?>("BaseClassToNullable<Byte>", TestBaseClass, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<BaseClass, byte?>("BaseClassToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<BaseClass, char?>("BaseClassToNullable<Char>", TestBaseClass, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<BaseClass, char?>("BaseClassToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<BaseClass, DateTime?>("BaseClassToNullable<DateTime>", TestBaseClass, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<BaseClass, DateTime?>("BaseClassToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<BaseClass, DateTimeOffset?>("BaseClassToNullable<DateTimeOffset>", TestBaseClass, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<BaseClass, DateTimeOffset?>("BaseClassToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<BaseClass, decimal?>("BaseClassToNullable<Decimal>", TestBaseClass, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<BaseClass, decimal?>("BaseClassToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<BaseClass, double?>("BaseClassToNullable<Double>", TestBaseClass, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<BaseClass, double?>("BaseClassToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<BaseClass, PrimaryColor?>("BaseClassToNullable<Enum>", TestBaseClass, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<BaseClass, PrimaryColor?>("BaseClassToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<BaseClass, float?>("BaseClassToNullable<Float>", TestBaseClass, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<BaseClass, float?>("BaseClassToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<BaseClass, Guid?>("BaseClassToNullable<Guid>", TestBaseClass, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<BaseClass, Guid?>("BaseClassToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<BaseClass, int?>("BaseClassToNullable<Int>", TestBaseClass, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<BaseClass, int?>("BaseClassToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<BaseClass, long?>("BaseClassToNullable<Long>", TestBaseClass, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<BaseClass, long?>("BaseClassToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<BaseClass, sbyte?>("BaseClassToNullable<SByte>", TestBaseClass, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<BaseClass, sbyte?>("BaseClassToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<BaseClass, short?>("BaseClassToNullable<Short>", TestBaseClass, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<BaseClass, short?>("BaseClassToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<BaseClass, TimeSpan?>("BaseClassToNullable<TimeSpan>", TestBaseClass, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<BaseClass, TimeSpan?>("BaseClassToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<BaseClass, uint?>("BaseClassToNullable<UInt>", TestBaseClass, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<BaseClass, uint?>("BaseClassToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<BaseClass, ulong?>("BaseClassToNullable<ULong>", TestBaseClass, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<BaseClass, ulong?>("BaseClassToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<BaseClass, ushort?>("BaseClassToNullable<UShort>", TestBaseClass, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<BaseClass, ushort?>("BaseClassToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<BaseClass, IInterface>("BaseClassToInterface", TestBaseClass, ConvertResult.Success, TestBaseClass),
                            new ConvertGenericTest<BaseClass, IInterface>("BaseClassToInterface", null, ConvertResult.Success, default(IInterface)),
                            new ConvertGenericTest<BaseClass, BaseClass>("BaseClassToBaseClass", TestBaseClass, ConvertResult.Success, TestBaseClass),
                            new ConvertGenericTest<BaseClass, BaseClass>("BaseClassToBaseClass", null, ConvertResult.Success, default(BaseClass)),
                            new ConvertGenericTest<BaseClass, DerivedClass>("BaseClassToDerivedClass", TestBaseClass, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<BaseClass, DerivedClass>("BaseClassToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("BaseClassToXXX",
                            // Simple Types
                            new TryConvertGenericTest<BaseClass, bool>("BaseClassToBool", TestBaseClass, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<BaseClass, bool>("BaseClassToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<BaseClass, byte>("BaseClassToByte", TestBaseClass, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<BaseClass, byte>("BaseClassToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<BaseClass, byte[]>("BaseClassToByteArray", TestBaseClass, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<BaseClass, byte[]>("BaseClassToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<BaseClass, char>("BaseClassToChar", TestBaseClass, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<BaseClass, char>("BaseClassToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<BaseClass, DateTime>("BaseClassToDateTime", TestBaseClass, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<BaseClass, DateTime>("BaseClassToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<BaseClass, DateTimeOffset>("BaseClassToDateTimeOffset", TestBaseClass, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<BaseClass, DateTimeOffset>("BaseClassToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<BaseClass, decimal>("BaseClassToDecimal", TestBaseClass, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<BaseClass, decimal>("BaseClassToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<BaseClass, double>("BaseClassToDouble", TestBaseClass, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<BaseClass, double>("BaseClassToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<BaseClass, PrimaryColor>("BaseClassToEnum", TestBaseClass, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<BaseClass, PrimaryColor>("BaseClassToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<BaseClass, float>("BaseClassToFloat", TestBaseClass, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<BaseClass, float>("BaseClassToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<BaseClass, Guid>("BaseClassToGuid", TestBaseClass, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<BaseClass, Guid>("BaseClassToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<BaseClass, int>("BaseClassToInt", TestBaseClass, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<BaseClass, int>("BaseClassToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<BaseClass, long>("BaseClassToLong", TestBaseClass, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<BaseClass, long>("BaseClassToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<BaseClass, sbyte>("BaseClassToSByte", TestBaseClass, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<BaseClass, sbyte>("BaseClassToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<BaseClass, short>("BaseClassToShort", TestBaseClass, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<BaseClass, short>("BaseClassToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<BaseClass, string>("BaseClassToString", TestBaseClass, ConvertResult.Failure, default(string)),
                            new TryConvertGenericTest<BaseClass, string>("BaseClassToString", null, ConvertResult.Failure, default(string)),
                            new TryConvertGenericTest<BaseClass, TimeSpan>("BaseClassToTimeSpan", TestBaseClass, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<BaseClass, TimeSpan>("BaseClassToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<BaseClass, Type>("BaseClassToType", TestBaseClass, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<BaseClass, Type>("BaseClassToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<BaseClass, uint>("BaseClassToUInt", TestBaseClass, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<BaseClass, uint>("BaseClassToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<BaseClass, ulong>("BaseClassToULong", TestBaseClass, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<BaseClass, ulong>("BaseClassToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<BaseClass, Uri>("BaseClassToUri", TestBaseClass, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<BaseClass, Uri>("BaseClassToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<BaseClass, ushort>("BaseClassToUShort", TestBaseClass, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<BaseClass, ushort>("BaseClassToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<BaseClass, bool?>("BaseClassToNullable<Bool>", TestBaseClass, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<BaseClass, bool?>("BaseClassToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<BaseClass, byte?>("BaseClassToNullable<Byte>", TestBaseClass, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<BaseClass, byte?>("BaseClassToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<BaseClass, char?>("BaseClassToNullable<Char>", TestBaseClass, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<BaseClass, char?>("BaseClassToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<BaseClass, DateTime?>("BaseClassToNullable<DateTime>", TestBaseClass, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<BaseClass, DateTime?>("BaseClassToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<BaseClass, DateTimeOffset?>("BaseClassToNullable<DateTimeOffset>", TestBaseClass, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<BaseClass, DateTimeOffset?>("BaseClassToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<BaseClass, decimal?>("BaseClassToNullable<Decimal>", TestBaseClass, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<BaseClass, decimal?>("BaseClassToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<BaseClass, double?>("BaseClassToNullable<Double>", TestBaseClass, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<BaseClass, double?>("BaseClassToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<BaseClass, PrimaryColor?>("BaseClassToNullable<Enum>", TestBaseClass, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<BaseClass, PrimaryColor?>("BaseClassToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<BaseClass, float?>("BaseClassToNullable<Float>", TestBaseClass, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<BaseClass, float?>("BaseClassToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<BaseClass, Guid?>("BaseClassToNullable<Guid>", TestBaseClass, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<BaseClass, Guid?>("BaseClassToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<BaseClass, int?>("BaseClassToNullable<Int>", TestBaseClass, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<BaseClass, int?>("BaseClassToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<BaseClass, long?>("BaseClassToNullable<Long>", TestBaseClass, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<BaseClass, long?>("BaseClassToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<BaseClass, sbyte?>("BaseClassToNullable<SByte>", TestBaseClass, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<BaseClass, sbyte?>("BaseClassToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<BaseClass, short?>("BaseClassToNullable<Short>", TestBaseClass, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<BaseClass, short?>("BaseClassToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<BaseClass, TimeSpan?>("BaseClassToNullable<TimeSpan>", TestBaseClass, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<BaseClass, TimeSpan?>("BaseClassToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<BaseClass, uint?>("BaseClassToNullable<UInt>", TestBaseClass, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<BaseClass, uint?>("BaseClassToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<BaseClass, ulong?>("BaseClassToNullable<ULong>", TestBaseClass, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<BaseClass, ulong?>("BaseClassToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<BaseClass, ushort?>("BaseClassToNullable<UShort>", TestBaseClass, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<BaseClass, ushort?>("BaseClassToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<BaseClass, IInterface>("BaseClassToInterface", TestBaseClass, ConvertResult.Success, TestBaseClass),
                            new TryConvertGenericTest<BaseClass, IInterface>("BaseClassToInterface", null, ConvertResult.Success, default(IInterface)),
                            new TryConvertGenericTest<BaseClass, BaseClass>("BaseClassToBaseClass", TestBaseClass, ConvertResult.Success, TestBaseClass),
                            new TryConvertGenericTest<BaseClass, BaseClass>("BaseClassToBaseClass", null, ConvertResult.Success, default(BaseClass)),
                            new TryConvertGenericTest<BaseClass, DerivedClass>("BaseClassToDerivedClass", TestBaseClass, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<BaseClass, DerivedClass>("BaseClassToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
