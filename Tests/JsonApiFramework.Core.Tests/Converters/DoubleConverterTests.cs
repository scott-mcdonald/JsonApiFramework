// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class DoubleConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DoubleConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("DoubleToXXX",
                            // Simple Types
                            new ConvertGenericTest<double, bool>("DoubleToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<double, byte>("DoubleToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, byte[]>("DoubleToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<double, char>("DoubleToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<double, DateTime>("DoubleToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<double, DateTimeOffset>("DoubleToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<double, decimal>("DoubleToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, double>("DoubleToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, PrimaryColor>("DoubleToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<double, float>("DoubleToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, Guid>("DoubleToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<double, int>("DoubleToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, long>("DoubleToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, sbyte>("DoubleToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, short>("DoubleToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, string>("DoubleToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<double, TimeSpan>("DoubleToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<double, Type>("DoubleToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<double, uint>("DoubleToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, ulong>("DoubleToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, Uri>("DoubleToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<double, ushort>("DoubleToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<double, bool?>("DoubleToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<double, byte?>("DoubleToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, char?>("DoubleToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<double, DateTime?>("DoubleToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<double, DateTimeOffset?>("DoubleToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<double, decimal?>("DoubleToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, double?>("DoubleToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, PrimaryColor?>("DoubleToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<double, float?>("DoubleToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, Guid?>("DoubleToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<double, int?>("DoubleToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, long?>("DoubleToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, sbyte?>("DoubleToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, short?>("DoubleToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, TimeSpan?>("DoubleToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<double, uint?>("DoubleToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, ulong?>("DoubleToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, ushort?>("DoubleToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<double, IInterface>("DoubleToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<double, BaseClass>("DoubleToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<double, DerivedClass>("DoubleToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("DoubleToXXX",
                            // Simple Types
                            new TryConvertGenericTest<double, bool>("DoubleToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<double, byte>("DoubleToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, byte[]>("DoubleToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<double, char>("DoubleToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<double, DateTime>("DoubleToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<double, DateTimeOffset>("DoubleToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<double, decimal>("DoubleToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, double>("DoubleToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, PrimaryColor>("DoubleToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<double, float>("DoubleToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, Guid>("DoubleToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<double, int>("DoubleToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, long>("DoubleToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, sbyte>("DoubleToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, short>("DoubleToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, string>("DoubleToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<double, TimeSpan>("DoubleToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<double, Type>("DoubleToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<double, uint>("DoubleToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, ulong>("DoubleToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, Uri>("DoubleToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<double, ushort>("DoubleToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<double, bool?>("DoubleToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<double, byte?>("DoubleToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, char?>("DoubleToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<double, DateTime?>("DoubleToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<double, DateTimeOffset?>("DoubleToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<double, decimal?>("DoubleToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, double?>("DoubleToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, PrimaryColor?>("DoubleToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<double, float?>("DoubleToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, Guid?>("DoubleToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<double, int?>("DoubleToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, long?>("DoubleToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, sbyte?>("DoubleToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, short?>("DoubleToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, TimeSpan?>("DoubleToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<double, uint?>("DoubleToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, ulong?>("DoubleToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, ushort?>("DoubleToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<double, IInterface>("DoubleToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<double, BaseClass>("DoubleToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<double, DerivedClass>("DoubleToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
