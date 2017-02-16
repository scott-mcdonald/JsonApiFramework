// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class CharConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public CharConverterTests(ITestOutputHelper output)
            : base(output, false)
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
                        new AggregateUnitTest("CharToXXX",
                            // Simple Types
                            new ConvertGenericTest<char, bool>("CharToBool", '*', ConvertResult.Success, true),
                            new ConvertGenericTest<char, byte>("CharToByte", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, byte[]>("CharToByteArray", '*', ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<char, char>("CharToChar", '*', ConvertResult.Success, '*'),
                            new ConvertGenericTest<char, DateTime>("CharToDateTime", '*', ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<char, DateTimeOffset>("CharToDateTimeOffset", '*', ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<char, decimal>("CharToDecimal", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, double>("CharToDouble", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, PrimaryColor>("CharToEnum", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<char, float>("CharToFloat", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, Guid>("CharToGuid", '*', ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<char, int>("CharToInt", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, long>("CharToLong", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, sbyte>("CharToSByte", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, short>("CharToShort", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, string>("CharToString", '*', ConvertResult.Success, "*"),
                            new ConvertGenericTest<char, TimeSpan>("CharToTimeSpan", '*', ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<char, Type>("CharToType", '*', ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<char, uint>("CharToUInt", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, ulong>("CharToULong", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, Uri>("CharToUri", '*', ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<char, ushort>("CharToUShort", '*', ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<char, bool?>("CharToNullable<Bool>", '*', ConvertResult.Success, true),
                            new ConvertGenericTest<char, byte?>("CharToNullable<Byte>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, char?>("CharToNullable<Char>", '*', ConvertResult.Success, '*'),
                            new ConvertGenericTest<char, DateTime?>("CharToNullable<DateTime>", '*', ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<char, DateTimeOffset?>("CharToNullable<DateTimeOffset>", '*', ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<char, decimal?>("CharToNullable<Decimal>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, double?>("CharToNullable<Double>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, PrimaryColor?>("CharToNullable<Enum>", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<char, float?>("CharToNullable<Float>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, Guid?>("CharToNullable<Guid>", '*', ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<char, int?>("CharToNullable<Int>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, long?>("CharToNullable<Long>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, sbyte?>("CharToNullable<SByte>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, short?>("CharToNullable<Short>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, TimeSpan?>("CharToNullable<TimeSpan>", '*', ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<char, uint?>("CharToNullable<UInt>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, ulong?>("CharToNullable<ULong>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char, ushort?>("CharToNullable<UShort>", '*', ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<char, IInterface>("CharToInterface", '*', ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<char, BaseClass>("CharToBaseClass", '*', ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<char, DerivedClass>("CharToDerivedClass", '*', ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("CharToXXX",
                            // Simple Types
                            new TryConvertGenericTest<char, bool>("CharToBool", '*', ConvertResult.Success, true),
                            new TryConvertGenericTest<char, byte>("CharToByte", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, byte[]>("CharToByteArray", '*', ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<char, char>("CharToChar", '*', ConvertResult.Success, '*'),
                            new TryConvertGenericTest<char, DateTime>("CharToDateTime", '*', ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<char, DateTimeOffset>("CharToDateTimeOffset", '*', ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<char, decimal>("CharToDecimal", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, double>("CharToDouble", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, PrimaryColor>("CharToEnum", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<char, float>("CharToFloat", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, Guid>("CharToGuid", '*', ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<char, int>("CharToInt", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, long>("CharToLong", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, sbyte>("CharToSByte", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, short>("CharToShort", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, string>("CharToString", '*', ConvertResult.Success, "*"),
                            new TryConvertGenericTest<char, TimeSpan>("CharToTimeSpan", '*', ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<char, Type>("CharToType", '*', ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<char, uint>("CharToUInt", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, ulong>("CharToULong", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, Uri>("CharToUri", '*', ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<char, ushort>("CharToUShort", '*', ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<char, bool?>("CharToNullable<Bool>", '*', ConvertResult.Success, true),
                            new TryConvertGenericTest<char, byte?>("CharToNullable<Byte>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, char?>("CharToNullable<Char>", '*', ConvertResult.Success, '*'),
                            new TryConvertGenericTest<char, DateTime?>("CharToNullable<DateTime>", '*', ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<char, DateTimeOffset?>("CharToNullable<DateTimeOffset>", '*', ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<char, decimal?>("CharToNullable<Decimal>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, double?>("CharToNullable<Double>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, PrimaryColor?>("CharToNullable<Enum>", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<char, float?>("CharToNullable<Float>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, Guid?>("CharToNullable<Guid>", '*', ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<char, int?>("CharToNullable<Int>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, long?>("CharToNullable<Long>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, sbyte?>("CharToNullable<SByte>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, short?>("CharToNullable<Short>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, TimeSpan?>("CharToNullable<TimeSpan>", '*', ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<char, uint?>("CharToNullable<UInt>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, ulong?>("CharToNullable<ULong>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char, ushort?>("CharToNullable<UShort>", '*', ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<char, IInterface>("CharToInterface", '*', ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<char, BaseClass>("CharToBaseClass", '*', ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<char, DerivedClass>("CharToDerivedClass", '*', ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
