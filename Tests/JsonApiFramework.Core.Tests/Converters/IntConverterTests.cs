// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class IntConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public IntConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("IntToXXX",
                            // Simple Types
                            new ConvertGenericTest<int, bool>("IntToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<int, byte>("IntToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, byte[]>("IntToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<int, char>("IntToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<int, DateTime>("IntToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<int, DateTimeOffset>("IntToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<int, decimal>("IntToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, double>("IntToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, PrimaryColor>("IntToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<int, float>("IntToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, Guid>("IntToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<int, int>("IntToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, long>("IntToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, sbyte>("IntToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, short>("IntToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, string>("IntToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<int, TimeSpan>("IntToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<int, Type>("IntToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<int, uint>("IntToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, ulong>("IntToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, Uri>("IntToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<int, ushort>("IntToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<int, bool?>("IntToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<int, byte?>("IntToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, char?>("IntToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<int, DateTime?>("IntToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<int, DateTimeOffset?>("IntToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<int, decimal?>("IntToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, double?>("IntToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, PrimaryColor?>("IntToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<int, float?>("IntToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, Guid?>("IntToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<int, int?>("IntToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, long?>("IntToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, sbyte?>("IntToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, short?>("IntToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, TimeSpan?>("IntToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<int, uint?>("IntToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, ulong?>("IntToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int, ushort?>("IntToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<int, IInterface>("IntToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<int, BaseClass>("IntToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<int, DerivedClass>("IntToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("IntToXXX",
                            // Simple Types
                            new TryConvertGenericTest<int, bool>("IntToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<int, byte>("IntToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, byte[]>("IntToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<int, char>("IntToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<int, DateTime>("IntToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<int, DateTimeOffset>("IntToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<int, decimal>("IntToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, double>("IntToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, PrimaryColor>("IntToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<int, float>("IntToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, Guid>("IntToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<int, int>("IntToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, long>("IntToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, sbyte>("IntToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, short>("IntToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, string>("IntToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<int, TimeSpan>("IntToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<int, Type>("IntToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<int, uint>("IntToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, ulong>("IntToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, Uri>("IntToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<int, ushort>("IntToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<int, bool?>("IntToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<int, byte?>("IntToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, char?>("IntToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<int, DateTime?>("IntToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<int, DateTimeOffset?>("IntToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<int, decimal?>("IntToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, double?>("IntToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, PrimaryColor?>("IntToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<int, float?>("IntToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, Guid?>("IntToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<int, int?>("IntToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, long?>("IntToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, sbyte?>("IntToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, short?>("IntToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, TimeSpan?>("IntToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<int, uint?>("IntToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, ulong?>("IntToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int, ushort?>("IntToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<int, IInterface>("IntToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<int, BaseClass>("IntToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<int, DerivedClass>("IntToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
