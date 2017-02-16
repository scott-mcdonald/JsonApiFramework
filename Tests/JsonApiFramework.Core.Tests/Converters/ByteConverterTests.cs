// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class ByteConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ByteConverterTests(ITestOutputHelper output)
            : base(output, true)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(ConvertGenericTestData))]
        public void TestTypeConveterConvertGeneric(IUnitTest unitTest)
        {
            unitTest.Execute(this);
            this.WriteBuffer();
        }

        [Theory]
        [MemberData(nameof(TryConvertGenericTestData))]   
        public void TestTypeConveterTryConvertGeneric(IUnitTest unitTest)
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
                        new AggregateUnitTest("ByteToXXX",
                            // Simple Types
                            new ConvertGenericTest<byte, bool>("ByteToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<byte, byte>("ByteToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, byte[]>("ByteToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<byte, char>("ByteToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<byte, DateTime>("ByteToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte, DateTimeOffset>("ByteToDateTimeOffset", 42, ConvertResult.Failure,default(DateTimeOffset)),
                            new ConvertGenericTest<byte, decimal>("ByteToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, double>("ByteToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, PrimaryColor>("ByteToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<byte, float>("ByteToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, Guid>("ByteToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<byte, int>("ByteToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, long>("ByteToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, sbyte>("ByteToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, short>("ByteToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, string>("ByteToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<byte, TimeSpan>("ByteToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<byte, Type>("ByteToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<byte, uint>("ByteToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, ulong>("ByteToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, Uri>("ByteToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<byte, ushort>("ByteToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<byte, bool?>("ByteToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<byte, byte?>("ByteToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, char?>("ByteToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<byte, DateTime?>("ByteToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<byte, DateTimeOffset?>("ByteToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<byte, decimal?>("ByteToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, double?>("ByteToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, PrimaryColor?>("ByteToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<byte, float?>("ByteToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, Guid?>("ByteToNullable<Guid>", 42, ConvertResult.Failure,default(Guid?)),
                            new ConvertGenericTest<byte, int?>("ByteToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, long?>("ByteToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, sbyte?>("ByteToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, short?>("ByteToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, TimeSpan?>("ByteToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<byte, uint?>("ByteToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, ulong?>("ByteToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, ushort?>("ByteToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<byte, IInterface>("ByteToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<byte, BaseClass>("ByteToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<byte, DerivedClass>("ByteToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("ByteToXXX",
                            // Simple Types
                            new TryConvertGenericTest<byte, bool>("ByteToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<byte, byte>("ByteToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, byte[]>("ByteToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<byte, char>("ByteToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<byte, DateTime>("ByteToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte, DateTimeOffset>("ByteToDateTimeOffset", 42, ConvertResult.Failure,default(DateTimeOffset)),
                            new TryConvertGenericTest<byte, decimal>("ByteToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, double>("ByteToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, PrimaryColor>("ByteToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<byte, float>("ByteToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, Guid>("ByteToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<byte, int>("ByteToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, long>("ByteToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, sbyte>("ByteToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, short>("ByteToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, string>("ByteToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<byte, TimeSpan>("ByteToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<byte, Type>("ByteToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<byte, uint>("ByteToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, ulong>("ByteToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, Uri>("ByteToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<byte, ushort>("ByteToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<byte, bool?>("ByteToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<byte, byte?>("ByteToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, char?>("ByteToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<byte, DateTime?>("ByteToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<byte, DateTimeOffset?>("ByteToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<byte, decimal?>("ByteToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, double?>("ByteToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, PrimaryColor?>("ByteToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<byte, float?>("ByteToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, Guid?>("ByteToNullable<Guid>", 42, ConvertResult.Failure,default(Guid?)),
                            new TryConvertGenericTest<byte, int?>("ByteToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, long?>("ByteToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, sbyte?>("ByteToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, short?>("ByteToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, TimeSpan?>("ByteToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<byte, uint?>("ByteToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, ulong?>("ByteToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, ushort?>("ByteToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<byte, IInterface>("ByteToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<byte, BaseClass>("ByteToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<byte, DerivedClass>("ByteToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
