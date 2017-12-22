// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class FloatConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public FloatConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("FloatToXXX",
                            // Simple Types
                            new ConvertGenericTest<float, bool>("FloatToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<float, byte>("FloatToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, byte[]>("FloatToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<float, char>("FloatToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<float, DateTime>("FloatToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<float, DateTimeOffset>("FloatToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<float, decimal>("FloatToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, double>("FloatToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, PrimaryColor>("FloatToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<float, float>("FloatToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, Guid>("FloatToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<float, int>("FloatToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, long>("FloatToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, sbyte>("FloatToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, short>("FloatToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, string>("FloatToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<float, TimeSpan>("FloatToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<float, Type>("FloatToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<float, uint>("FloatToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, ulong>("FloatToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, Uri>("FloatToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<float, ushort>("FloatToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<float, bool?>("FloatToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<float, byte?>("FloatToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, char?>("FloatToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<float, DateTime?>("FloatToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<float, DateTimeOffset?>("FloatToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<float, decimal?>("FloatToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, double?>("FloatToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, PrimaryColor?>("FloatToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<float, float?>("FloatToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, Guid?>("FloatToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<float, int?>("FloatToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, long?>("FloatToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, sbyte?>("FloatToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, short?>("FloatToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, TimeSpan?>("FloatToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<float, uint?>("FloatToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, ulong?>("FloatToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float, ushort?>("FloatToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<float, IInterface>("FloatToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<float, BaseClass>("FloatToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<float, DerivedClass>("FloatToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("FloatToXXX",
                            // Simple Types
                            new TryConvertGenericTest<float, bool>("FloatToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<float, byte>("FloatToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, byte[]>("FloatToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<float, char>("FloatToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<float, DateTime>("FloatToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<float, DateTimeOffset>("FloatToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<float, decimal>("FloatToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, double>("FloatToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, PrimaryColor>("FloatToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<float, float>("FloatToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, Guid>("FloatToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<float, int>("FloatToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, long>("FloatToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, sbyte>("FloatToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, short>("FloatToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, string>("FloatToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<float, TimeSpan>("FloatToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<float, Type>("FloatToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<float, uint>("FloatToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, ulong>("FloatToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, Uri>("FloatToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<float, ushort>("FloatToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<float, bool?>("FloatToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<float, byte?>("FloatToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, char?>("FloatToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<float, DateTime?>("FloatToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<float, DateTimeOffset?>("FloatToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<float, decimal?>("FloatToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, double?>("FloatToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, PrimaryColor?>("FloatToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<float, float?>("FloatToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, Guid?>("FloatToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<float, int?>("FloatToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, long?>("FloatToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, sbyte?>("FloatToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, short?>("FloatToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, TimeSpan?>("FloatToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<float, uint?>("FloatToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, ulong?>("FloatToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float, ushort?>("FloatToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<float, IInterface>("FloatToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<float, BaseClass>("FloatToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<float, DerivedClass>("FloatToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
