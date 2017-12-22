// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class UShortConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public UShortConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("UShortToXXX",
                            // Simple Types
                            new ConvertGenericTest<ushort, bool>("UShortToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ushort, byte>("UShortToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, byte[]>("UShortToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ushort, char>("UShortToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ushort, DateTime>("UShortToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ushort, DateTimeOffset>("UShortToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ushort, decimal>("UShortToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, double>("UShortToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, PrimaryColor>("UShortToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ushort, float>("UShortToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, Guid>("UShortToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ushort, int>("UShortToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, long>("UShortToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, sbyte>("UShortToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, short>("UShortToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, string>("UShortToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<ushort, TimeSpan>("UShortToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ushort, Type>("UShortToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ushort, uint>("UShortToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, ulong>("UShortToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, Uri>("UShortToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ushort, ushort>("UShortToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<ushort, bool?>("UShortToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ushort, byte?>("UShortToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, char?>("UShortToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ushort, DateTime?>("UShortToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ushort, DateTimeOffset?>("UShortToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ushort, decimal?>("UShortToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, double?>("UShortToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, PrimaryColor?>("UShortToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ushort, float?>("UShortToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, Guid?>("UShortToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ushort, int?>("UShortToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, long?>("UShortToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, sbyte?>("UShortToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, short?>("UShortToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, TimeSpan?>("UShortToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ushort, uint?>("UShortToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, ulong?>("UShortToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort, ushort?>("UShortToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<ushort, IInterface>("UShortToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ushort, BaseClass>("UShortToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ushort, DerivedClass>("UShortToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("UShortToXXX",
                            // Simple Types
                            new TryConvertGenericTest<ushort, bool>("UShortToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ushort, byte>("UShortToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, byte[]>("UShortToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ushort, char>("UShortToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ushort, DateTime>("UShortToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ushort, DateTimeOffset>("UShortToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ushort, decimal>("UShortToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, double>("UShortToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, PrimaryColor>("UShortToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ushort, float>("UShortToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, Guid>("UShortToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ushort, int>("UShortToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, long>("UShortToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, sbyte>("UShortToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, short>("UShortToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, string>("UShortToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<ushort, TimeSpan>("UShortToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ushort, Type>("UShortToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ushort, uint>("UShortToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, ulong>("UShortToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, Uri>("UShortToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ushort, ushort>("UShortToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<ushort, bool?>("UShortToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ushort, byte?>("UShortToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, char?>("UShortToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ushort, DateTime?>("UShortToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ushort, DateTimeOffset?>("UShortToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ushort, decimal?>("UShortToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, double?>("UShortToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, PrimaryColor?>("UShortToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ushort, float?>("UShortToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, Guid?>("UShortToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ushort, int?>("UShortToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, long?>("UShortToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, sbyte?>("UShortToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, short?>("UShortToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, TimeSpan?>("UShortToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ushort, uint?>("UShortToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, ulong?>("UShortToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort, ushort?>("UShortToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<ushort, IInterface>("UShortToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ushort, BaseClass>("UShortToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ushort, DerivedClass>("UShortToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
