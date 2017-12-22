// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class ULongConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ULongConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("ULongToXXX",
                            // Simple Types
                            new ConvertGenericTest<ulong, bool>("ULongToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ulong, byte>("ULongToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, byte[]>("ULongToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ulong, char>("ULongToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ulong, DateTime>("ULongToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ulong, DateTimeOffset>("ULongToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ulong, decimal>("ULongToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, double>("ULongToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, PrimaryColor>("ULongToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ulong, float>("ULongToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, Guid>("ULongToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ulong, int>("ULongToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, long>("ULongToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, sbyte>("ULongToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, short>("ULongToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, string>("ULongToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<ulong, TimeSpan>("ULongToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ulong, Type>("ULongToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ulong, uint>("ULongToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, ulong>("ULongToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, Uri>("ULongToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ulong, ushort>("ULongToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<ulong, bool?>("ULongToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ulong, byte?>("ULongToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, char?>("ULongToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ulong, DateTime?>("ULongToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ulong, DateTimeOffset?>("ULongToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ulong, decimal?>("ULongToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, double?>("ULongToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, PrimaryColor?>("ULongToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ulong, float?>("ULongToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, Guid?>("ULongToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ulong, int?>("ULongToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, long?>("ULongToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, sbyte?>("ULongToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, short?>("ULongToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, TimeSpan?>("ULongToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ulong, uint?>("ULongToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, ulong?>("ULongToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, ushort?>("ULongToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<ulong, IInterface>("ULongToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ulong, BaseClass>("ULongToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ulong, DerivedClass>("ULongToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("ULongToXXX",
                            // Simple Types
                            new TryConvertGenericTest<ulong, bool>("ULongToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ulong, byte>("ULongToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, byte[]>("ULongToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ulong, char>("ULongToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ulong, DateTime>("ULongToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ulong, DateTimeOffset>("ULongToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ulong, decimal>("ULongToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, double>("ULongToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, PrimaryColor>("ULongToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ulong, float>("ULongToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, Guid>("ULongToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ulong, int>("ULongToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, long>("ULongToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, sbyte>("ULongToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, short>("ULongToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, string>("ULongToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<ulong, TimeSpan>("ULongToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ulong, Type>("ULongToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ulong, uint>("ULongToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, ulong>("ULongToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, Uri>("ULongToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ulong, ushort>("ULongToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<ulong, bool?>("ULongToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ulong, byte?>("ULongToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, char?>("ULongToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ulong, DateTime?>("ULongToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ulong, DateTimeOffset?>("ULongToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ulong, decimal?>("ULongToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, double?>("ULongToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, PrimaryColor?>("ULongToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ulong, float?>("ULongToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, Guid?>("ULongToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ulong, int?>("ULongToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, long?>("ULongToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, sbyte?>("ULongToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, short?>("ULongToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, TimeSpan?>("ULongToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ulong, uint?>("ULongToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, ulong?>("ULongToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, ushort?>("ULongToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<ulong, IInterface>("ULongToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ulong, BaseClass>("ULongToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ulong, DerivedClass>("ULongToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
