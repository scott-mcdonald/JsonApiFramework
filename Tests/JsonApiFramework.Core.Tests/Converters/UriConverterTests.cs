// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class UriConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public UriConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("UriToXXX",
                            // Simple Types
                            new ConvertGenericTest<Uri, bool>("UriToBool", TestUri, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Uri, bool>("UriToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Uri, byte>("UriToByte", TestUri, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Uri, byte>("UriToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Uri, byte[]>("UriToByteArray", TestUri, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<Uri, byte[]>("UriToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<Uri, char>("UriToChar", TestUri, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Uri, char>("UriToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Uri, DateTime>("UriToDateTime", TestUri, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Uri, DateTime>("UriToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Uri, DateTimeOffset>("UriToDateTimeOffset", TestUri, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Uri, DateTimeOffset>("UriToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Uri, decimal>("UriToDecimal", TestUri, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Uri, decimal>("UriToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Uri, double>("UriToDouble", TestUri, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Uri, double>("UriToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Uri, PrimaryColor>("UriToEnum", TestUri, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Uri, PrimaryColor>("UriToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Uri, float>("UriToFloat", TestUri, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Uri, float>("UriToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Uri, Guid>("UriToGuid", TestUri, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<Uri, Guid>("UriToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<Uri, int>("UriToInt", TestUri, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Uri, int>("UriToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Uri, long>("UriToLong", TestUri, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Uri, long>("UriToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Uri, sbyte>("UriToSByte", TestUri, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Uri, sbyte>("UriToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Uri, short>("UriToShort", TestUri, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Uri, short>("UriToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Uri, string>("UriToString", TestUri, ConvertResult.Success, TestUriString),
                            new ConvertGenericTest<Uri, string>("UriToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<Uri, TimeSpan>("UriToTimeSpan", TestUri, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Uri, TimeSpan>("UriToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Uri, Type>("UriToType", TestUri, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Uri, Type>("UriToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Uri, uint>("UriToUInt", TestUri, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Uri, uint>("UriToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Uri, ulong>("UriToULong", TestUri, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Uri, ulong>("UriToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Uri, Uri>("UriToUri", TestUri, ConvertResult.Success, TestUri),
                            new ConvertGenericTest<Uri, Uri>("UriToUri", null, ConvertResult.Success, default(Uri)),
                            new ConvertGenericTest<Uri, ushort>("UriToUShort", TestUri, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<Uri, ushort>("UriToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<Uri, bool?>("UriToNullable<Bool>", TestUri, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Uri, bool?>("UriToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Uri, byte?>("UriToNullable<Byte>", TestUri, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Uri, byte?>("UriToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Uri, char?>("UriToNullable<Char>", TestUri, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Uri, char?>("UriToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Uri, DateTime?>("UriToNullable<DateTime>", TestUri, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Uri, DateTime?>("UriToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Uri, DateTimeOffset?>("UriToNullable<DateTimeOffset>", TestUri, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Uri, DateTimeOffset?>("UriToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Uri, decimal?>("UriToNullable<Decimal>", TestUri, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Uri, decimal?>("UriToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Uri, double?>("UriToNullable<Double>", TestUri, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Uri, double?>("UriToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Uri, PrimaryColor?>("UriToNullable<Enum>", TestUri, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Uri, PrimaryColor?>("UriToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Uri, float?>("UriToNullable<Float>", TestUri, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Uri, float?>("UriToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Uri, Guid?>("UriToNullable<Guid>", TestUri, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<Uri, Guid?>("UriToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<Uri, int?>("UriToNullable<Int>", TestUri, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Uri, int?>("UriToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Uri, long?>("UriToNullable<Long>", TestUri, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Uri, long?>("UriToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Uri, sbyte?>("UriToNullable<SByte>", TestUri, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Uri, sbyte?>("UriToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Uri, short?>("UriToNullable<Short>", TestUri, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Uri, short?>("UriToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Uri, TimeSpan?>("UriToNullable<TimeSpan>", TestUri, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Uri, TimeSpan?>("UriToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Uri, uint?>("UriToNullable<UInt>", TestUri, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Uri, uint?>("UriToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Uri, ulong?>("UriToNullable<ULong>", TestUri, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Uri, ulong?>("UriToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Uri, ushort?>("UriToNullable<UShort>", TestUri, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<Uri, ushort?>("UriToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<Uri, IInterface>("UriToInterface", TestUri, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Uri, IInterface>("UriToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Uri, BaseClass>("UriToBaseClass", TestUri, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Uri, BaseClass>("UriToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Uri, DerivedClass>("UriToDerivedClass", TestUri, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<Uri, DerivedClass>("UriToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("UriToXXX",
                            // Simple Types
                            new TryConvertGenericTest<Uri, bool>("UriToBool", TestUri, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Uri, bool>("UriToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Uri, byte>("UriToByte", TestUri, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Uri, byte>("UriToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Uri, byte[]>("UriToByteArray", TestUri, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<Uri, byte[]>("UriToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<Uri, char>("UriToChar", TestUri, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Uri, char>("UriToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Uri, DateTime>("UriToDateTime", TestUri, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Uri, DateTime>("UriToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Uri, DateTimeOffset>("UriToDateTimeOffset", TestUri, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Uri, DateTimeOffset>("UriToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Uri, decimal>("UriToDecimal", TestUri, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Uri, decimal>("UriToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Uri, double>("UriToDouble", TestUri, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Uri, double>("UriToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Uri, PrimaryColor>("UriToEnum", TestUri, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Uri, PrimaryColor>("UriToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Uri, float>("UriToFloat", TestUri, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Uri, float>("UriToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Uri, Guid>("UriToGuid", TestUri, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<Uri, Guid>("UriToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<Uri, int>("UriToInt", TestUri, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Uri, int>("UriToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Uri, long>("UriToLong", TestUri, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Uri, long>("UriToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Uri, sbyte>("UriToSByte", TestUri, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Uri, sbyte>("UriToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Uri, short>("UriToShort", TestUri, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Uri, short>("UriToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Uri, string>("UriToString", TestUri, ConvertResult.Success, TestUriString),
                            new TryConvertGenericTest<Uri, string>("UriToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<Uri, TimeSpan>("UriToTimeSpan", TestUri, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Uri, TimeSpan>("UriToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Uri, Type>("UriToType", TestUri, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Uri, Type>("UriToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Uri, uint>("UriToUInt", TestUri, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Uri, uint>("UriToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Uri, ulong>("UriToULong", TestUri, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Uri, ulong>("UriToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Uri, Uri>("UriToUri", TestUri, ConvertResult.Success, TestUri),
                            new TryConvertGenericTest<Uri, Uri>("UriToUri", null, ConvertResult.Success, default(Uri)),
                            new TryConvertGenericTest<Uri, ushort>("UriToUShort", TestUri, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<Uri, ushort>("UriToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<Uri, bool?>("UriToNullable<Bool>", TestUri, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Uri, bool?>("UriToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Uri, byte?>("UriToNullable<Byte>", TestUri, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Uri, byte?>("UriToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Uri, char?>("UriToNullable<Char>", TestUri, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Uri, char?>("UriToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Uri, DateTime?>("UriToNullable<DateTime>", TestUri, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Uri, DateTime?>("UriToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Uri, DateTimeOffset?>("UriToNullable<DateTimeOffset>", TestUri, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Uri, DateTimeOffset?>("UriToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Uri, decimal?>("UriToNullable<Decimal>", TestUri, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Uri, decimal?>("UriToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Uri, double?>("UriToNullable<Double>", TestUri, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Uri, double?>("UriToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Uri, PrimaryColor?>("UriToNullable<Enum>", TestUri, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Uri, PrimaryColor?>("UriToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Uri, float?>("UriToNullable<Float>", TestUri, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Uri, float?>("UriToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Uri, Guid?>("UriToNullable<Guid>", TestUri, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<Uri, Guid?>("UriToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<Uri, int?>("UriToNullable<Int>", TestUri, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Uri, int?>("UriToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Uri, long?>("UriToNullable<Long>", TestUri, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Uri, long?>("UriToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Uri, sbyte?>("UriToNullable<SByte>", TestUri, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Uri, sbyte?>("UriToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Uri, short?>("UriToNullable<Short>", TestUri, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Uri, short?>("UriToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Uri, TimeSpan?>("UriToNullable<TimeSpan>", TestUri, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Uri, TimeSpan?>("UriToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Uri, uint?>("UriToNullable<UInt>", TestUri, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Uri, uint?>("UriToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Uri, ulong?>("UriToNullable<ULong>", TestUri, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Uri, ulong?>("UriToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Uri, ushort?>("UriToNullable<UShort>", TestUri, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<Uri, ushort?>("UriToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<Uri, IInterface>("UriToInterface", TestUri, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Uri, IInterface>("UriToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Uri, BaseClass>("UriToBaseClass", TestUri, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Uri, BaseClass>("UriToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Uri, DerivedClass>("UriToDerivedClass", TestUri, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<Uri, DerivedClass>("UriToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
