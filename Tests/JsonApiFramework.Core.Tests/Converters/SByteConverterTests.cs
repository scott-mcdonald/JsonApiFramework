// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class SByteConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public SByteConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("SByteToXXX",
                            // Simple Types
                            new ConvertGenericTest<sbyte, bool>("SByteToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<sbyte, byte>("SByteToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, byte[]>("SByteToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<sbyte, char>("SByteToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<sbyte, DateTime>("SByteToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<sbyte, DateTimeOffset>("SByteToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<sbyte, decimal>("SByteToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, double>("SByteToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, PrimaryColor>("SByteToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<sbyte, float>("SByteToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, Guid>("SByteToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<sbyte, int>("SByteToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, long>("SByteToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, sbyte>("SByteToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, short>("SByteToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, string>("SByteToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<sbyte, TimeSpan>("SByteToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<sbyte, Type>("SByteToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<sbyte, uint>("SByteToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, ulong>("SByteToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, Uri>("SByteToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<sbyte, ushort>("SByteToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<sbyte, bool?>("SByteToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<sbyte, byte?>("SByteToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, char?>("SByteToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<sbyte, DateTime?>("SByteToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<sbyte, DateTimeOffset?>("SByteToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<sbyte, decimal?>("SByteToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, double?>("SByteToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, PrimaryColor?>("SByteToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<sbyte, float?>("SByteToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, Guid?>("SByteToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<sbyte, int?>("SByteToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, long?>("SByteToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, sbyte?>("SByteToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, short?>("SByteToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, TimeSpan?>("SByteToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<sbyte, uint?>("SByteToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, ulong?>("SByteToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, ushort?>("SByteToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<sbyte, IInterface>("SByteToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<sbyte, BaseClass>("SByteToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<sbyte, DerivedClass>("SByteToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("SByteToXXX",
                            // Simple Types
                            new TryConvertGenericTest<sbyte, bool>("SByteToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<sbyte, byte>("SByteToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, byte[]>("SByteToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<sbyte, char>("SByteToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<sbyte, DateTime>("SByteToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<sbyte, DateTimeOffset>("SByteToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<sbyte, decimal>("SByteToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, double>("SByteToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, PrimaryColor>("SByteToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<sbyte, float>("SByteToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, Guid>("SByteToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<sbyte, int>("SByteToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, long>("SByteToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, sbyte>("SByteToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, short>("SByteToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, string>("SByteToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<sbyte, TimeSpan>("SByteToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<sbyte, Type>("SByteToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<sbyte, uint>("SByteToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, ulong>("SByteToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, Uri>("SByteToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<sbyte, ushort>("SByteToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<sbyte, bool?>("SByteToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<sbyte, byte?>("SByteToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, char?>("SByteToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<sbyte, DateTime?>("SByteToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<sbyte, DateTimeOffset?>("SByteToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<sbyte, decimal?>("SByteToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, double?>("SByteToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, PrimaryColor?>("SByteToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<sbyte, float?>("SByteToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, Guid?>("SByteToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<sbyte, int?>("SByteToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, long?>("SByteToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, sbyte?>("SByteToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, short?>("SByteToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, TimeSpan?>("SByteToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<sbyte, uint?>("SByteToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, ulong?>("SByteToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, ushort?>("SByteToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<sbyte, IInterface>("SByteToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<sbyte, BaseClass>("SByteToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<sbyte, DerivedClass>("SByteToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
