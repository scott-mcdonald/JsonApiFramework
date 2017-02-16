// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class EnumConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public EnumConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("EnumToXXX",
                            // Simple Types
                            new ConvertGenericTest<PrimaryColor, bool>("EnumToBool", PrimaryColor.Red, ConvertResult.Success, false),
                            new ConvertGenericTest<PrimaryColor, bool>("EnumToBool", PrimaryColor.Blue, ConvertResult.Success, true),
                            new ConvertGenericTest<PrimaryColor, byte>("EnumToByte", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, byte[]>("EnumToByteArray", PrimaryColor.Blue, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<PrimaryColor, char>("EnumToChar", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new ConvertGenericTest<PrimaryColor, DateTime>("EnumToDateTime", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<PrimaryColor, DateTimeOffset>("EnumToDateTimeOffset", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<PrimaryColor, decimal>("EnumToDecimal", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, double>("EnumToDouble", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, PrimaryColor>("EnumToEnum", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<PrimaryColor, float>("EnumToFloat", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, Guid>("EnumToGuid", PrimaryColor.Blue, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<PrimaryColor, int>("EnumToInt", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, long>("EnumToLong", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, sbyte>("EnumToSByte", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, short>("EnumToShort", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, string>("EnumToString", PrimaryColor.Blue, ConvertResult.Success, TestBlueString),
                            new ConvertGenericTest<PrimaryColor, string>("EnumToStringWithFormat", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinalAsString, FormatEnumContext),
                            new ConvertGenericTest<PrimaryColor, TimeSpan>("EnumToTimeSpan", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<PrimaryColor, Type>("EnumToType", PrimaryColor.Blue, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<PrimaryColor, uint>("EnumToUInt", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, ulong>("EnumToULong", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, Uri>("EnumToUri", PrimaryColor.Blue, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<PrimaryColor, ushort>("EnumToUShort", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),

                            // Nullable Types
                            new ConvertGenericTest<PrimaryColor, bool?>("EnumToNullable<Bool>", PrimaryColor.Red, ConvertResult.Success, false),
                            new ConvertGenericTest<PrimaryColor, bool?>("EnumToNullable<Bool>", PrimaryColor.Blue, ConvertResult.Success, true),
                            new ConvertGenericTest<PrimaryColor, byte?>("EnumToNullable<Byte>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, char?>("EnumToNullable<Char>", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new ConvertGenericTest<PrimaryColor, DateTime?>("EnumToNullable<DateTime>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<PrimaryColor, DateTimeOffset?>("EnumToNullable<DateTimeOffset>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<PrimaryColor, decimal?>("EnumToNullable<Decimal>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, double?>("EnumToNullable<Double>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, PrimaryColor?>("EnumToNullable<Enum>", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<PrimaryColor, float?>("EnumToNullable<Float>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, Guid?>("EnumToNullable<Guid>", PrimaryColor.Blue, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<PrimaryColor, int?>("EnumToNullable<Int>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, long?>("EnumToNullable<Long>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, sbyte?>("EnumToNullable<SByte>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, short?>("EnumToNullable<Short>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, TimeSpan?>("EnumToNullable<TimeSpan>", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<PrimaryColor, uint?>("EnumToNullable<UInt>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, ulong?>("EnumToNullable<ULong>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new ConvertGenericTest<PrimaryColor, ushort?>("EnumToNullable<UShort>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),

                            // Interface/Class Types
                            new ConvertGenericTest<PrimaryColor, IInterface>("EnumToInterface", PrimaryColor.Blue, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<PrimaryColor, BaseClass>("EnumToBaseClass", PrimaryColor.Blue, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<PrimaryColor, DerivedClass>("EnumToDerivedClass", PrimaryColor.Blue, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("EnumToXXX",
                            // Simple Types
                            new TryConvertGenericTest<PrimaryColor, bool>("EnumToBool", PrimaryColor.Red, ConvertResult.Success, false),
                            new TryConvertGenericTest<PrimaryColor, bool>("EnumToBool", PrimaryColor.Blue, ConvertResult.Success, true),
                            new TryConvertGenericTest<PrimaryColor, byte>("EnumToByte", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, byte[]>("EnumToByteArray", PrimaryColor.Blue, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<PrimaryColor, char>("EnumToChar", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<PrimaryColor, DateTime>("EnumToDateTime", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<PrimaryColor, DateTimeOffset>("EnumToDateTimeOffset", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<PrimaryColor, decimal>("EnumToDecimal", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, double>("EnumToDouble", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, PrimaryColor>("EnumToEnum", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<PrimaryColor, float>("EnumToFloat", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, Guid>("EnumToGuid", PrimaryColor.Blue, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<PrimaryColor, int>("EnumToInt", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, long>("EnumToLong", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, sbyte>("EnumToSByte", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, short>("EnumToShort", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, string>("EnumToString", PrimaryColor.Blue, ConvertResult.Success, TestBlueString),
                            new TryConvertGenericTest<PrimaryColor, string>("EnumToStringWithFormat", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinalAsString, FormatEnumContext),
                            new TryConvertGenericTest<PrimaryColor, TimeSpan>("EnumToTimeSpan", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<PrimaryColor, Type>("EnumToType", PrimaryColor.Blue, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<PrimaryColor, uint>("EnumToUInt", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, ulong>("EnumToULong", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, Uri>("EnumToUri", PrimaryColor.Blue, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<PrimaryColor, ushort>("EnumToUShort", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),

                            // Nullable Types
                            new TryConvertGenericTest<PrimaryColor, bool?>("EnumToNullable<Bool>", PrimaryColor.Red, ConvertResult.Success, false),
                            new TryConvertGenericTest<PrimaryColor, bool?>("EnumToNullable<Bool>", PrimaryColor.Blue, ConvertResult.Success, true),
                            new TryConvertGenericTest<PrimaryColor, byte?>("EnumToNullable<Byte>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, char?>("EnumToNullable<Char>", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<PrimaryColor, DateTime?>("EnumToNullable<DateTime>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<PrimaryColor, DateTimeOffset?>("EnumToNullable<DateTimeOffset>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<PrimaryColor, decimal?>("EnumToNullable<Decimal>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, double?>("EnumToNullable<Double>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, PrimaryColor?>("EnumToNullable<Enum>", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<PrimaryColor, float?>("EnumToNullable<Float>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, Guid?>("EnumToNullable<Guid>", PrimaryColor.Blue, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<PrimaryColor, int?>("EnumToNullable<Int>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, long?>("EnumToNullable<Long>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, sbyte?>("EnumToNullable<SByte>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, short?>("EnumToNullable<Short>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, TimeSpan?>("EnumToNullable<TimeSpan>", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<PrimaryColor, uint?>("EnumToNullable<UInt>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, ulong?>("EnumToNullable<ULong>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),
                            new TryConvertGenericTest<PrimaryColor, ushort?>("EnumToNullable<UShort>", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinal),

                            // Interface/Class Types
                            new TryConvertGenericTest<PrimaryColor, IInterface>("EnumToInterface", PrimaryColor.Blue, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<PrimaryColor, BaseClass>("EnumToBaseClass", PrimaryColor.Blue, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<PrimaryColor, DerivedClass>("EnumToDerivedClass", PrimaryColor.Blue, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
