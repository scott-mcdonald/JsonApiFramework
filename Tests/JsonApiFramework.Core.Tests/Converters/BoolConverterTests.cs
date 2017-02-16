// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class BoolConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public BoolConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("BoolToXXX",
                            // Simple Types
                            new ConvertGenericTest<bool, bool>("BoolToBool", true, ConvertResult.Success, true),
                            new ConvertGenericTest<bool, byte>("BoolToByte", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, byte[]>("BoolToByteArray", true, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<bool, char>("BoolToChar", true, ConvertResult.Success, (char)1),
                            new ConvertGenericTest<bool, DateTime>("BoolToDateTime", true, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<bool, DateTimeOffset>("BoolToDateTimeOffset", true, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<bool, decimal>("BoolToDecimal", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, double>("BoolToDouble", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, PrimaryColor>("BoolToEnum", true, ConvertResult.Success, PrimaryColor.Green),
                            new ConvertGenericTest<bool, float>("BoolToFloat", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, Guid>("BoolToGuid", true, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<bool, int>("BoolToInt", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, long>("BoolToLong", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, sbyte>("BoolToSByte", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, short>("BoolToShort", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, string>("BoolToString", false, ConvertResult.Success, "False"),
                            new ConvertGenericTest<bool, string>("BoolToString", true, ConvertResult.Success, "True"),
                            new ConvertGenericTest<bool, TimeSpan>("BoolToTimeSpan", true, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<bool, Type>("BoolToType", true, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<bool, uint>("BoolToUInt", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, ulong>("BoolToULong", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, ushort>("BoolToUShort", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, Uri>("BoolToUri", true, ConvertResult.Failure, default(Uri)),

                            // Nullable Types
                            new ConvertGenericTest<bool, bool?>("BoolToNullable<Bool>", true, ConvertResult.Success, true),
                            new ConvertGenericTest<bool, byte?>("BoolToNullable<Byte>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, char?>("BoolToNullable<Char>", true, ConvertResult.Success, (char)1),
                            new ConvertGenericTest<bool, DateTime?>("BoolToNullable<DateTime>", true, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<bool, DateTimeOffset?>("BoolToNullable<DateTimeOffset>", true, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<bool, decimal?>("BoolToNullable<Decimal>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, double?>("BoolToNullable<Double>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, PrimaryColor?>("BoolToNullable<Enum>", true, ConvertResult.Success, PrimaryColor.Green),
                            new ConvertGenericTest<bool, float?>("BoolToNullable<Float>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, Guid?>("BoolToNullable<Guid>", true, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<bool, int?>("BoolToNullable<Int>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, long?>("BoolToNullable<Long>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, sbyte?>("BoolToNullable<SByte>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, short?>("BoolToNullable<Short>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, TimeSpan?>("BoolToNullable<TimeSpan>", true, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<bool, uint?>("BoolToNullable<UInt>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, ulong?>("BoolToNullable<ULong>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool, ushort?>("BoolToNullable<UShort>", true, ConvertResult.Success, 1),

                            // Interface/Class Types
                            new ConvertGenericTest<bool, IInterface>("BoolToInterface", true, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<bool, BaseClass>("BoolToBaseClass", true, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<bool, DerivedClass>("BoolToDerivedClass", true, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("BoolToXXX",
                            // Simple Types
                            new TryConvertGenericTest<bool, bool>("BoolToBool", true, ConvertResult.Success, true),
                            new TryConvertGenericTest<bool, byte>("BoolToByte", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, byte[]>("BoolToByteArray", true, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<bool, char>("BoolToChar", true, ConvertResult.Success, (char)1),
                            new TryConvertGenericTest<bool, DateTime>("BoolToDateTime", true, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<bool, DateTimeOffset>("BoolToDateTimeOffset", true, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<bool, decimal>("BoolToDecimal", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, double>("BoolToDouble", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, PrimaryColor>("BoolToEnum", true, ConvertResult.Success, PrimaryColor.Green),
                            new TryConvertGenericTest<bool, float>("BoolToFloat", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, Guid>("BoolToGuid", true, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<bool, int>("BoolToInt", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, long>("BoolToLong", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, sbyte>("BoolToSByte", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, short>("BoolToShort", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, string>("BoolToString", false, ConvertResult.Success, "False"),
                            new TryConvertGenericTest<bool, string>("BoolToString", true, ConvertResult.Success, "True"),
                            new TryConvertGenericTest<bool, TimeSpan>("BoolToTimeSpan", true, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<bool, Type>("BoolToType", true, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<bool, uint>("BoolToUInt", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, ulong>("BoolToULong", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, ushort>("BoolToUShort", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, Uri>("BoolToUri", true, ConvertResult.Failure, default(Uri)),

                            // Nullable Types
                            new TryConvertGenericTest<bool, bool?>("BoolToNullable<Bool>", true, ConvertResult.Success, true),
                            new TryConvertGenericTest<bool, byte?>("BoolToNullable<Byte>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, char?>("BoolToNullable<Char>", true, ConvertResult.Success, (char)1),
                            new TryConvertGenericTest<bool, DateTime?>("BoolToNullable<DateTime>", true, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<bool, DateTimeOffset?>("BoolToNullable<DateTimeOffset>", true, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<bool, decimal?>("BoolToNullable<Decimal>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, double?>("BoolToNullable<Double>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, PrimaryColor?>("BoolToNullable<Enum>", true, ConvertResult.Success, PrimaryColor.Green),
                            new TryConvertGenericTest<bool, float?>("BoolToNullable<Float>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, Guid?>("BoolToNullable<Guid>", true, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<bool, int?>("BoolToNullable<Int>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, long?>("BoolToNullable<Long>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, sbyte?>("BoolToNullable<SByte>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, short?>("BoolToNullable<Short>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, TimeSpan?>("BoolToNullable<TimeSpan>", true, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<bool, uint?>("BoolToNullable<UInt>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, ulong?>("BoolToNullable<ULong>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool, ushort?>("BoolToNullable<UShort>", true, ConvertResult.Success, 1),

                            // Interface/Class Types
                            new TryConvertGenericTest<bool, IInterface>("BoolToInterface", true, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<bool, BaseClass>("BoolToBaseClass", true, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<bool, DerivedClass>("BoolToDerivedClass", true, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
