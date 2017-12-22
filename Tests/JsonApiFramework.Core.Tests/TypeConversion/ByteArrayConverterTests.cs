// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class ByteArrayConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ByteArrayConverterTests(ITestOutputHelper output)
            : base(output, true)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(ConvertGenericTestData))]
        public void TestTypeConveterConvertGeneric(IUnitTest unitTest)
        {
            unitTest.Execute(this);
            this.WriteBuffer();
        }

        [Theory]
        [MemberData(nameof(TryConvertGenericTestData))]   
        public void TestTypeConveterTryConvertGeneric(IUnitTest unitTest)
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
                        new AggregateUnitTest("ByteArrayToXXX",
                            // Simple Types
                            new ConvertGenericTest<byte[], bool>("ByteArrayToBool", TestByteArray, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<byte[], bool>("ByteArrayToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<byte[], byte>("ByteArrayToByte", TestByteArray, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<byte[], byte>("ByteArrayToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<byte[], byte[]>("ByteArrayToByteArray", TestByteArray, ConvertResult.Success, TestByteArray),
                            new ConvertGenericTest<byte[], byte[]>("ByteArrayToByteArray", null, ConvertResult.Success, null),
                            new ConvertGenericTest<byte[], char>("ByteArrayToChar", TestByteArray, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<byte[], char>("ByteArrayToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<byte[], DateTime>("ByteArrayToDateTime", TestByteArray, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte[], DateTime>("ByteArrayToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte[], DateTimeOffset>("ByteArrayToDateTimeOffset", TestByteArray, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<byte[], DateTimeOffset>("ByteArrayToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<byte[], decimal>("ByteArrayToDecimal", TestByteArray, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<byte[], decimal>("ByteArrayToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<byte[], double>("ByteArrayToDouble", TestByteArray, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<byte[], double>("ByteArrayToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<byte[], PrimaryColor>("ByteArrayToEnum", TestByteArray, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<byte[], PrimaryColor>("ByteArrayToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<byte[], float>("ByteArrayToFloat", TestByteArray, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<byte[], float>("ByteArrayToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<byte[], Guid>("ByteArrayToGuid", TestGuidByteArray, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<byte[], Guid>("ByteArrayToGuid", null, ConvertResult.Success, default(Guid)),
                            new ConvertGenericTest<byte[], int>("ByteArrayToInt", TestByteArray, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<byte[], int>("ByteArrayToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<byte[], long>("ByteArrayToLong", TestByteArray, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<byte[], long>("ByteArrayToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<byte[], sbyte>("ByteArrayToSByte", TestByteArray, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<byte[], sbyte>("ByteArrayToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<byte[], short>("ByteArrayToShort", TestByteArray, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<byte[], short>("ByteArrayToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<byte[], string>("ByteArrayToString", TestByteArray, ConvertResult.Success, TestByteArrayString),
                            new ConvertGenericTest<byte[], string>("ByteArrayToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<byte[], TimeSpan>("ByteArrayToTimeSpan", TestByteArray, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<byte[], TimeSpan>("ByteArrayToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<byte[], Type>("ByteArrayToType", TestByteArray, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<byte[], Type>("ByteArrayToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<byte[], uint>("ByteArrayToUInt", TestByteArray, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<byte[], uint>("ByteArrayToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<byte[], ulong>("ByteArrayToULong", TestByteArray, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<byte[], ulong>("ByteArrayToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<byte[], Uri>("ByteArrayToUri", TestByteArray, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<byte[], Uri>("ByteArrayToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<byte[], ushort>("ByteArrayToUShort", TestByteArray, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<byte[], ushort>("ByteArrayToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<byte[], bool?>("ByteArrayToNullable<Bool>", TestByteArray, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<byte[], bool?>("ByteArrayToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<byte[], byte?>("ByteArrayToNullable<Byte>", TestByteArray, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<byte[], byte?>("ByteArrayToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<byte[], char?>("ByteArrayToNullable<Char>", TestByteArray, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<byte[], char?>("ByteArrayToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<byte[], DateTime?>("ByteArrayToNullable<DateTime>", TestByteArray, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<byte[], DateTime?>("ByteArrayToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<byte[], DateTimeOffset?>("ByteArrayToNullable<DateTimeOffset>", TestByteArray, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<byte[], DateTimeOffset?>("ByteArrayToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<byte[], decimal?>("ByteArrayToNullable<Decimal>", TestByteArray, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<byte[], decimal?>("ByteArrayToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<byte[], double?>("ByteArrayToNullable<Double>", TestByteArray, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<byte[], double?>("ByteArrayToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<byte[], PrimaryColor?>("ByteArrayToNullable<Enum>", TestByteArray, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<byte[], PrimaryColor?>("ByteArrayToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<byte[], float?>("ByteArrayToNullable<Float>", TestByteArray, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<byte[], float?>("ByteArrayToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<byte[], Guid?>("ByteArrayToNullable<Guid>", TestGuidByteArray, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<byte[], Guid?>("ByteArrayToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new ConvertGenericTest<byte[], int?>("ByteArrayToNullable<Int>", TestByteArray, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<byte[], int?>("ByteArrayToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<byte[], long?>("ByteArrayToNullable<Long>", TestByteArray, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<byte[], long?>("ByteArrayToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<byte[], sbyte?>("ByteArrayToNullable<SByte>", TestByteArray, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<byte[], sbyte?>("ByteArrayToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<byte[], short?>("ByteArrayToNullable<Short>", TestByteArray, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<byte[], short?>("ByteArrayToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<byte[], TimeSpan?>("ByteArrayToNullable<TimeSpan>", TestByteArray, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<byte[], TimeSpan?>("ByteArrayToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<byte[], uint?>("ByteArrayToNullable<UInt>", TestByteArray, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<byte[], uint?>("ByteArrayToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<byte[], ulong?>("ByteArrayToNullable<ULong>", TestByteArray, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<byte[], ulong?>("ByteArrayToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<byte[], ushort?>("ByteArrayToNullable<UShort>", TestByteArray, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<byte[], ushort?>("ByteArrayToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<byte[], IInterface>("ByteArrayToInterface", TestByteArray, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<byte[], IInterface>("ByteArrayToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<byte[], BaseClass>("ByteArrayToBaseClass", TestByteArray, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<byte[], BaseClass>("ByteArrayToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<byte[], DerivedClass>("ByteArrayToDerivedClass", TestByteArray, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<byte[], DerivedClass>("ByteArrayToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("ByteArrayToXXX",
                            // Simple Types
                            new TryConvertGenericTest<byte[], bool>("ByteArrayToBool", TestByteArray, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<byte[], bool>("ByteArrayToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<byte[], byte>("ByteArrayToByte", TestByteArray, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<byte[], byte>("ByteArrayToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<byte[], byte[]>("ByteArrayToByteArray", TestByteArray, ConvertResult.Success, TestByteArray),
                            new TryConvertGenericTest<byte[], byte[]>("ByteArrayToByteArray", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<byte[], char>("ByteArrayToChar", TestByteArray, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<byte[], char>("ByteArrayToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<byte[], DateTime>("ByteArrayToDateTime", TestByteArray, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte[], DateTime>("ByteArrayToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte[], DateTimeOffset>("ByteArrayToDateTimeOffset", TestByteArray, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<byte[], DateTimeOffset>("ByteArrayToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<byte[], decimal>("ByteArrayToDecimal", TestByteArray, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<byte[], decimal>("ByteArrayToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<byte[], double>("ByteArrayToDouble", TestByteArray, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<byte[], double>("ByteArrayToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<byte[], PrimaryColor>("ByteArrayToEnum", TestByteArray, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<byte[], PrimaryColor>("ByteArrayToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<byte[], float>("ByteArrayToFloat", TestByteArray, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<byte[], float>("ByteArrayToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<byte[], Guid>("ByteArrayToGuid", TestGuidByteArray, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<byte[], Guid>("ByteArrayToGuid", null, ConvertResult.Success, default(Guid)),
                            new TryConvertGenericTest<byte[], int>("ByteArrayToInt", TestByteArray, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<byte[], int>("ByteArrayToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<byte[], long>("ByteArrayToLong", TestByteArray, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<byte[], long>("ByteArrayToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<byte[], sbyte>("ByteArrayToSByte", TestByteArray, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<byte[], sbyte>("ByteArrayToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<byte[], short>("ByteArrayToShort", TestByteArray, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<byte[], short>("ByteArrayToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<byte[], string>("ByteArrayToString", TestByteArray, ConvertResult.Success, TestByteArrayString),
                            new TryConvertGenericTest<byte[], string>("ByteArrayToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<byte[], TimeSpan>("ByteArrayToTimeSpan", TestByteArray, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<byte[], TimeSpan>("ByteArrayToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<byte[], Type>("ByteArrayToType", TestByteArray, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<byte[], Type>("ByteArrayToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<byte[], uint>("ByteArrayToUInt", TestByteArray, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<byte[], uint>("ByteArrayToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<byte[], ulong>("ByteArrayToULong", TestByteArray, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<byte[], ulong>("ByteArrayToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<byte[], Uri>("ByteArrayToUri", TestByteArray, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<byte[], Uri>("ByteArrayToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<byte[], ushort>("ByteArrayToUShort", TestByteArray, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<byte[], ushort>("ByteArrayToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<byte[], bool?>("ByteArrayToNullable<Bool>", TestByteArray, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<byte[], bool?>("ByteArrayToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<byte[], byte?>("ByteArrayToNullable<Byte>", TestByteArray, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<byte[], byte?>("ByteArrayToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<byte[], char?>("ByteArrayToNullable<Char>", TestByteArray, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<byte[], char?>("ByteArrayToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<byte[], DateTime?>("ByteArrayToNullable<DateTime>", TestByteArray, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<byte[], DateTime?>("ByteArrayToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<byte[], DateTimeOffset?>("ByteArrayToNullable<DateTimeOffset>", TestByteArray, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<byte[], DateTimeOffset?>("ByteArrayToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<byte[], decimal?>("ByteArrayToNullable<Decimal>", TestByteArray, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<byte[], decimal?>("ByteArrayToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<byte[], double?>("ByteArrayToNullable<Double>", TestByteArray, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<byte[], double?>("ByteArrayToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<byte[], PrimaryColor?>("ByteArrayToNullable<Enum>", TestByteArray, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<byte[], PrimaryColor?>("ByteArrayToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<byte[], float?>("ByteArrayToNullable<Float>", TestByteArray, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<byte[], float?>("ByteArrayToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<byte[], Guid?>("ByteArrayToNullable<Guid>", TestGuidByteArray, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<byte[], Guid?>("ByteArrayToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new TryConvertGenericTest<byte[], int?>("ByteArrayToNullable<Int>", TestByteArray, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<byte[], int?>("ByteArrayToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<byte[], long?>("ByteArrayToNullable<Long>", TestByteArray, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<byte[], long?>("ByteArrayToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<byte[], sbyte?>("ByteArrayToNullable<SByte>", TestByteArray, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<byte[], sbyte?>("ByteArrayToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<byte[], short?>("ByteArrayToNullable<Short>", TestByteArray, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<byte[], short?>("ByteArrayToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<byte[], TimeSpan?>("ByteArrayToNullable<TimeSpan>", TestByteArray, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<byte[], TimeSpan?>("ByteArrayToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<byte[], uint?>("ByteArrayToNullable<UInt>", TestByteArray, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<byte[], uint?>("ByteArrayToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<byte[], ulong?>("ByteArrayToNullable<ULong>", TestByteArray, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<byte[], ulong?>("ByteArrayToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<byte[], ushort?>("ByteArrayToNullable<UShort>", TestByteArray, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<byte[], ushort?>("ByteArrayToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<byte[], IInterface>("ByteArrayToInterface", TestByteArray, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<byte[], IInterface>("ByteArrayToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<byte[], BaseClass>("ByteArrayToBaseClass", TestByteArray, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<byte[], BaseClass>("ByteArrayToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<byte[], DerivedClass>("ByteArrayToDerivedClass", TestByteArray, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<byte[], DerivedClass>("ByteArrayToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
