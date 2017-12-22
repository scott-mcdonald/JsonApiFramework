// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class GuidConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public GuidConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("GuidToXXX",
                            // Simple Types
                            new ConvertGenericTest<Guid, bool>("GuidToBool", TestGuid, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Guid, byte>("GuidToByte", TestGuid, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Guid, byte[]>("GuidToByteArray", TestGuid, ConvertResult.Success, TestGuidByteArray),
                            new ConvertGenericTest<Guid, char>("GuidToChar", TestGuid, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Guid, DateTime>("GuidToDateTime", TestGuid, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Guid, DateTimeOffset>("GuidToDateTimeOffset", TestGuid, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Guid, decimal>("GuidToDecimal", TestGuid, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Guid, double>("GuidToDouble", TestGuid, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Guid, PrimaryColor>("GuidToEnum", TestGuid, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Guid, float>("GuidToFloat", TestGuid, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Guid, Guid>("GuidToGuid", TestGuid, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<Guid, int>("GuidToInt", TestGuid, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Guid, long>("GuidToLong", TestGuid, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Guid, sbyte>("GuidToSByte", TestGuid, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Guid, short>("GuidToShort", TestGuid, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Guid, string>("GuidToString", TestGuid, ConvertResult.Success, TestGuidString),
                            new ConvertGenericTest<Guid, TimeSpan>("GuidToTimeSpan", TestGuid, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Guid, Type>("GuidToType", TestGuid, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Guid, uint>("GuidToUInt", TestGuid, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Guid, ulong>("GuidToULong", TestGuid, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Guid, Uri>("GuidToUri", TestGuid, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Guid, ushort>("GuidToUShort", TestGuid, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<Guid, bool?>("GuidToNullable<Bool>", TestGuid, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Guid, byte?>("GuidToNullable<Byte>", TestGuid, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Guid, char?>("GuidToNullable<Char>", TestGuid, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Guid, DateTime?>("GuidToNullable<DateTime>", TestGuid, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Guid, DateTimeOffset?>("GuidToNullable<DateTimeOffset>", TestGuid, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Guid, decimal?>("GuidToNullable<Decimal>", TestGuid, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Guid, double?>("GuidToNullable<Double>", TestGuid, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Guid, PrimaryColor?>("GuidToNullable<Enum>", TestGuid, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Guid, float?>("GuidToNullable<Float>", TestGuid, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Guid, Guid?>("GuidToNullable<Guid>", TestGuid, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<Guid, int?>("GuidToNullable<Int>", TestGuid, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Guid, long?>("GuidToNullable<Long>", TestGuid, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Guid, sbyte?>("GuidToNullable<SByte>", TestGuid, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Guid, short?>("GuidToNullable<Short>", TestGuid, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Guid, TimeSpan?>("GuidToNullable<TimeSpan>", TestGuid, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Guid, uint?>("GuidToNullable<UInt>", TestGuid, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Guid, ulong?>("GuidToNullable<ULong>", TestGuid, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Guid, ushort?>("GuidToNullable<UShort>", TestGuid, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<Guid, IInterface>("GuidToInterface", TestGuid, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Guid, BaseClass>("GuidToBaseClass", TestGuid, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Guid, DerivedClass>("GuidToDerivedClass", TestGuid, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("GuidToXXX",
                            // Simple Types
                            new TryConvertGenericTest<Guid, bool>("GuidToBool", TestGuid, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Guid, byte>("GuidToByte", TestGuid, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Guid, byte[]>("GuidToByteArray", TestGuid, ConvertResult.Success, TestGuidByteArray),
                            new TryConvertGenericTest<Guid, char>("GuidToChar", TestGuid, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Guid, DateTime>("GuidToDateTime", TestGuid, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Guid, DateTimeOffset>("GuidToDateTimeOffset", TestGuid, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Guid, decimal>("GuidToDecimal", TestGuid, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Guid, double>("GuidToDouble", TestGuid, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Guid, PrimaryColor>("GuidToEnum", TestGuid, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Guid, float>("GuidToFloat", TestGuid, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Guid, Guid>("GuidToGuid", TestGuid, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<Guid, int>("GuidToInt", TestGuid, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Guid, long>("GuidToLong", TestGuid, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Guid, sbyte>("GuidToSByte", TestGuid, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Guid, short>("GuidToShort", TestGuid, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Guid, string>("GuidToString", TestGuid, ConvertResult.Success, TestGuidString),
                            new TryConvertGenericTest<Guid, TimeSpan>("GuidToTimeSpan", TestGuid, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Guid, Type>("GuidToType", TestGuid, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Guid, uint>("GuidToUInt", TestGuid, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Guid, ulong>("GuidToULong", TestGuid, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Guid, Uri>("GuidToUri", TestGuid, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Guid, ushort>("GuidToUShort", TestGuid, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<Guid, bool?>("GuidToNullable<Bool>", TestGuid, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Guid, byte?>("GuidToNullable<Byte>", TestGuid, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Guid, char?>("GuidToNullable<Char>", TestGuid, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Guid, DateTime?>("GuidToNullable<DateTime>", TestGuid, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Guid, DateTimeOffset?>("GuidToNullable<DateTimeOffset>", TestGuid, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Guid, decimal?>("GuidToNullable<Decimal>", TestGuid, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Guid, double?>("GuidToNullable<Double>", TestGuid, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Guid, PrimaryColor?>("GuidToNullable<Enum>", TestGuid, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Guid, float?>("GuidToNullable<Float>", TestGuid, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Guid, Guid?>("GuidToNullable<Guid>", TestGuid, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<Guid, int?>("GuidToNullable<Int>", TestGuid, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Guid, long?>("GuidToNullable<Long>", TestGuid, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Guid, sbyte?>("GuidToNullable<SByte>", TestGuid, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Guid, short?>("GuidToNullable<Short>", TestGuid, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Guid, TimeSpan?>("GuidToNullable<TimeSpan>", TestGuid, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Guid, uint?>("GuidToNullable<UInt>", TestGuid, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Guid, ulong?>("GuidToNullable<ULong>", TestGuid, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Guid, ushort?>("GuidToNullable<UShort>", TestGuid, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<Guid, IInterface>("GuidToInterface", TestGuid, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Guid, BaseClass>("GuidToBaseClass", TestGuid, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Guid, DerivedClass>("GuidToDerivedClass", TestGuid, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
