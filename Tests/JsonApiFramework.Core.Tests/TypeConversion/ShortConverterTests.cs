// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class ShortConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ShortConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("ShortToXXX",
                            // Simple Types
                            new ConvertGenericTest<short, bool>("ShortToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<short, byte>("ShortToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, byte[]>("ShortToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<short, char>("ShortToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<short, DateTime>("ShortToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<short, DateTimeOffset>("ShortToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<short, decimal>("ShortToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, double>("ShortToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, PrimaryColor>("ShortToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<short, float>("ShortToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, Guid>("ShortToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<short, int>("ShortToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, long>("ShortToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, sbyte>("ShortToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, short>("ShortToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, string>("ShortToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<short, TimeSpan>("ShortToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<short, Type>("ShortToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<short, uint>("ShortToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, ulong>("ShortToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, Uri>("ShortToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<short, ushort>("ShortToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<short, bool?>("ShortToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<short, byte?>("ShortToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, char?>("ShortToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<short, DateTime?>("ShortToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<short, DateTimeOffset?>("ShortToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<short, decimal?>("ShortToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, double?>("ShortToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, PrimaryColor?>("ShortToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<short, float?>("ShortToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, Guid?>("ShortToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<short, int?>("ShortToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, long?>("ShortToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, sbyte?>("ShortToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, short?>("ShortToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, TimeSpan?>("ShortToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<short, uint?>("ShortToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, ulong?>("ShortToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short, ushort?>("ShortToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<short, IInterface>("ShortToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<short, BaseClass>("ShortToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<short, DerivedClass>("ShortToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("ShortToXXX",
                            // Simple Types
                            new TryConvertGenericTest<short, bool>("ShortToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<short, byte>("ShortToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, byte[]>("ShortToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<short, char>("ShortToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<short, DateTime>("ShortToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<short, DateTimeOffset>("ShortToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<short, decimal>("ShortToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, double>("ShortToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, PrimaryColor>("ShortToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<short, float>("ShortToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, Guid>("ShortToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<short, int>("ShortToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, long>("ShortToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, sbyte>("ShortToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, short>("ShortToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, string>("ShortToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<short, TimeSpan>("ShortToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<short, Type>("ShortToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<short, uint>("ShortToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, ulong>("ShortToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, Uri>("ShortToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<short, ushort>("ShortToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<short, bool?>("ShortToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<short, byte?>("ShortToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, char?>("ShortToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<short, DateTime?>("ShortToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<short, DateTimeOffset?>("ShortToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<short, decimal?>("ShortToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, double?>("ShortToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, PrimaryColor?>("ShortToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<short, float?>("ShortToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, Guid?>("ShortToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<short, int?>("ShortToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, long?>("ShortToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, sbyte?>("ShortToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, short?>("ShortToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, TimeSpan?>("ShortToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<short, uint?>("ShortToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, ulong?>("ShortToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short, ushort?>("ShortToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<short, IInterface>("ShortToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<short, BaseClass>("ShortToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<short, DerivedClass>("ShortToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
