// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class DecimalConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DecimalConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("DecimalToXXX",
                            // Simple Types
                            new ConvertGenericTest<decimal, bool>("DecimalToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<decimal, byte>("DecimalToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, byte[]>("DecimalToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<decimal, char>("DecimalToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<decimal, DateTime>("DecimalToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<decimal, DateTimeOffset>("DecimalToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<decimal, decimal>("DecimalToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, double>("DecimalToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, PrimaryColor>("DecimalToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<decimal, float>("DecimalToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, Guid>("DecimalToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<decimal, int>("DecimalToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, long>("DecimalToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, sbyte>("DecimalToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, short>("DecimalToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, string>("DecimalToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<decimal, TimeSpan>("DecimalToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<decimal, Type>("DecimalToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<decimal, uint>("DecimalToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, ulong>("DecimalToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, Uri>("DecimalToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<decimal, ushort>("DecimalToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<decimal, bool?>("DecimalToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<decimal, byte?>("DecimalToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, char?>("DecimalToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<decimal, DateTime?>("DecimalToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<decimal, DateTimeOffset?>("DecimalToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<decimal, decimal?>("DecimalToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, double?>("DecimalToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, PrimaryColor?>("DecimalToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<decimal, float?>("DecimalToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, Guid?>("DecimalToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<decimal, int?>("DecimalToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, long?>("DecimalToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, sbyte?>("DecimalToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, short?>("DecimalToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, TimeSpan?>("DecimalToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<decimal, uint?>("DecimalToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, ulong?>("DecimalToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, ushort?>("DecimalToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<decimal, IInterface>("DecimalToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<decimal, BaseClass>("DecimalToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<decimal, DerivedClass>("DecimalToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("DecimalToXXX",
                            // Simple Types
                            new TryConvertGenericTest<decimal, bool>("DecimalToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<decimal, byte>("DecimalToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, byte[]>("DecimalToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<decimal, char>("DecimalToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<decimal, DateTime>("DecimalToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<decimal, DateTimeOffset>("DecimalToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<decimal, decimal>("DecimalToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, double>("DecimalToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, PrimaryColor>("DecimalToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<decimal, float>("DecimalToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, Guid>("DecimalToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<decimal, int>("DecimalToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, long>("DecimalToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, sbyte>("DecimalToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, short>("DecimalToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, string>("DecimalToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<decimal, TimeSpan>("DecimalToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<decimal, Type>("DecimalToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<decimal, uint>("DecimalToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, ulong>("DecimalToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, Uri>("DecimalToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<decimal, ushort>("DecimalToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<decimal, bool?>("DecimalToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<decimal, byte?>("DecimalToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, char?>("DecimalToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<decimal, DateTime?>("DecimalToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<decimal, DateTimeOffset?>("DecimalToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<decimal, decimal?>("DecimalToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, double?>("DecimalToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, PrimaryColor?>("DecimalToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<decimal, float?>("DecimalToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, Guid?>("DecimalToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<decimal, int?>("DecimalToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, long?>("DecimalToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, sbyte?>("DecimalToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, short?>("DecimalToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, TimeSpan?>("DecimalToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<decimal, uint?>("DecimalToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, ulong?>("DecimalToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, ushort?>("DecimalToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<decimal, IInterface>("DecimalToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<decimal, BaseClass>("DecimalToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<decimal, DerivedClass>("DecimalToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
