// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class LongConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public LongConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("LongToXXX",
                            // Simple Types
                            new ConvertGenericTest<long, bool>("LongToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<long, byte>("LongToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, byte[]>("LongToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<long, char>("LongToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<long, DateTime>("LongToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<long, DateTimeOffset>("LongToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<long, decimal>("LongToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, double>("LongToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, PrimaryColor>("LongToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<long, float>("LongToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, Guid>("LongToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<long, int>("LongToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, long>("LongToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, sbyte>("LongToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, short>("LongToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, string>("LongToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<long, TimeSpan>("LongToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<long, Type>("LongToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<long, uint>("LongToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, ulong>("LongToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, Uri>("LongToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<long, ushort>("LongToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<long, bool?>("LongToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<long, byte?>("LongToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, char?>("LongToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<long, DateTime?>("LongToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<long, DateTimeOffset?>("LongToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<long, decimal?>("LongToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, double?>("LongToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, PrimaryColor?>("LongToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<long, float?>("LongToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, Guid?>("LongToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<long, int?>("LongToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, long?>("LongToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, sbyte?>("LongToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, short?>("LongToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, TimeSpan?>("LongToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<long, uint?>("LongToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, ulong?>("LongToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, ushort?>("LongToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<long, IInterface>("LongToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<long, BaseClass>("LongToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<long, DerivedClass>("LongToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("LongToXXX",
                            // Simple Types
                            new TryConvertGenericTest<long, bool>("LongToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<long, byte>("LongToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, byte[]>("LongToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<long, char>("LongToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<long, DateTime>("LongToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<long, DateTimeOffset>("LongToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<long, decimal>("LongToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, double>("LongToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, PrimaryColor>("LongToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<long, float>("LongToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, Guid>("LongToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<long, int>("LongToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, long>("LongToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, sbyte>("LongToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, short>("LongToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, string>("LongToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<long, TimeSpan>("LongToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<long, Type>("LongToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<long, uint>("LongToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, ulong>("LongToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, Uri>("LongToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<long, ushort>("LongToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<long, bool?>("LongToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<long, byte?>("LongToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, char?>("LongToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<long, DateTime?>("LongToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<long, DateTimeOffset?>("LongToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<long, decimal?>("LongToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, double?>("LongToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, PrimaryColor?>("LongToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<long, float?>("LongToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, Guid?>("LongToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<long, int?>("LongToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, long?>("LongToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, sbyte?>("LongToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, short?>("LongToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, TimeSpan?>("LongToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<long, uint?>("LongToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, ulong?>("LongToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, ushort?>("LongToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<long, IInterface>("LongToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<long, BaseClass>("LongToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<long, DerivedClass>("LongToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
