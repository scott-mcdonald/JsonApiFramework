// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class UIntConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public UIntConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("UIntToXXX",
                            // Simple Types
                            new ConvertGenericTest<uint, bool>("UIntToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<uint, byte>("UIntToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, byte[]>("UIntToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<uint, char>("UIntToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<uint, DateTime>("UIntToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<uint, DateTimeOffset>("UIntToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<uint, decimal>("UIntToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, double>("UIntToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, PrimaryColor>("UIntToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<uint, float>("UIntToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, Guid>("UIntToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<uint, int>("UIntToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, long>("UIntToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, sbyte>("UIntToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, short>("UIntToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, string>("UIntToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<uint, TimeSpan>("UIntToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<uint, Type>("UIntToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<uint, uint>("UIntToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, ulong>("UIntToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, Uri>("UIntToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<uint, ushort>("UIntToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<uint, bool?>("UIntToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<uint, byte?>("UIntToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, char?>("UIntToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<uint, DateTime?>("UIntToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<uint, DateTimeOffset?>("UIntToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<uint, decimal?>("UIntToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, double?>("UIntToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, PrimaryColor?>("UIntToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<uint, float?>("UIntToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, Guid?>("UIntToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<uint, int?>("UIntToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, long?>("UIntToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, sbyte?>("UIntToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, short?>("UIntToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, TimeSpan?>("UIntToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<uint, uint?>("UIntToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, ulong?>("UIntToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint, ushort?>("UIntToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<uint, IInterface>("UIntToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<uint, BaseClass>("UIntToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<uint, DerivedClass>("UIntToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("UIntToXXX",
                            // Simple Types
                            new TryConvertGenericTest<uint, bool>("UIntToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<uint, byte>("UIntToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, byte[]>("UIntToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<uint, char>("UIntToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<uint, DateTime>("UIntToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<uint, DateTimeOffset>("UIntToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<uint, decimal>("UIntToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, double>("UIntToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, PrimaryColor>("UIntToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<uint, float>("UIntToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, Guid>("UIntToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<uint, int>("UIntToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, long>("UIntToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, sbyte>("UIntToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, short>("UIntToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, string>("UIntToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<uint, TimeSpan>("UIntToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<uint, Type>("UIntToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<uint, uint>("UIntToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, ulong>("UIntToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, Uri>("UIntToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<uint, ushort>("UIntToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<uint, bool?>("UIntToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<uint, byte?>("UIntToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, char?>("UIntToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<uint, DateTime?>("UIntToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<uint, DateTimeOffset?>("UIntToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<uint, decimal?>("UIntToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, double?>("UIntToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, PrimaryColor?>("UIntToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<uint, float?>("UIntToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, Guid?>("UIntToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<uint, int?>("UIntToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, long?>("UIntToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, sbyte?>("UIntToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, short?>("UIntToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, TimeSpan?>("UIntToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<uint, uint?>("UIntToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, ulong?>("UIntToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint, ushort?>("UIntToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<uint, IInterface>("UIntToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<uint, BaseClass>("UIntToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<uint, DerivedClass>("UIntToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
