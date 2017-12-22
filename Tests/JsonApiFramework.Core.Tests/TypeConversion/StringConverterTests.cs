// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.TypeConversion
{
    public class StringConverterTests : ConverterTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public StringConverterTests(ITestOutputHelper output)
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
                        new AggregateUnitTest("StringToXXX",
                            // Simple Types
                            new ConvertGenericTest<string, bool>("StringToBool", "False", ConvertResult.Success, false),
                            new ConvertGenericTest<string, bool>("StringToBool", "True", ConvertResult.Success, true),
                            new ConvertGenericTest<string, bool>("StringToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<string, byte>("StringToByte", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, byte>("StringToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<string, byte[]>("StringToByteArray", TestByteArrayString, ConvertResult.Success, TestByteArray),
                            new ConvertGenericTest<string, byte[]>("StringToByteArray", null, ConvertResult.Success, default(byte[])),
                            new ConvertGenericTest<string, char>("StringToChar", "*", ConvertResult.Success, '*'),
                            new ConvertGenericTest<string, char>("StringToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<string, DateTime>("StringToDateTime", TestDateTimeString, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<string, DateTime>("StringToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new ConvertGenericTest<string, DateTime>("StringToDateTimeWithFormat", TestDateTimeStringWithFormat, ConvertResult.Success, TestDateTime, FormatDateTimeContext),
                            new ConvertGenericTest<string, DateTime>("StringToDateTimeWithFormatAndFormatProvider", TestDateTimeStringWithFormatAndFormatProvider, ConvertResult.Success, TestDateTime, FormatAndFormatProviderDateTimeContext),
                            new ConvertGenericTest<string, DateTimeOffset>("StringToDateTimeOffset", TestDateTimeOffsetString, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<string, DateTimeOffset>("StringToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new ConvertGenericTest<string, DateTimeOffset>("StringToDateTimeOffsetWithFormat", TestDateTimeOffsetStringWithFormat, ConvertResult.Success, TestDateTimeOffset, FormatDateTimeOffsetContext),
                            new ConvertGenericTest<string, DateTimeOffset>("StringToDateTimeOffsetWithFormatAndFormatProvider", TestDateTimeOffsetStringWithFormatAndFormatProvider, ConvertResult.Success, TestDateTimeOffset, FormatAndFormatProviderDateTimeOffsetContext),
                            new ConvertGenericTest<string, decimal>("StringToDecimal", "42.1", ConvertResult.Success, 42.1m),
                            new ConvertGenericTest<string, decimal>("StringToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<string, double>("StringToDouble", "42.2", ConvertResult.Success, 42.2),
                            new ConvertGenericTest<string, double>("StringToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<string, PrimaryColor>("StringToEnum", "42", ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<string, PrimaryColor>("StringToEnum", TestBlueString, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<string, PrimaryColor>("StringToEnum", TestBlueLowercaseString, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<string, PrimaryColor>("StringToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<string, float>("StringToFloat", "42.3", ConvertResult.Success, (float)42.3),
                            new ConvertGenericTest<string, float>("StringToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<string, Guid>("StringToGuid", TestGuidString, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<string, Guid>("StringToGuid", null, ConvertResult.Success, default(Guid)),
                            new ConvertGenericTest<string, int>("StringToInt", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, int>("StringToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<string, long>("StringToLong", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, long>("StringToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<string, sbyte>("StringToSByte", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, sbyte>("StringToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<string, short>("StringToShort", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, short>("StringToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<string, string>("StringToString", "The quick brown fox jumps over the lazy dog", ConvertResult.Success, "The quick brown fox jumps over the lazy dog"),
                            new ConvertGenericTest<string, string>("StringToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<string, TimeSpan>("StringToTimeSpan", TestTimeSpanString, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<string, TimeSpan>("StringToTimeSpan", null, ConvertResult.Success, default(TimeSpan)),
                            new ConvertGenericTest<string, TimeSpan>("StringToTimeSpanWithFormat", TestTimeSpanStringWithFormat, ConvertResult.Success, TestTimeSpan, FormatTimeSpanContext),
                            new ConvertGenericTest<string, TimeSpan>("StringToTimeSpanWithFormatAndFormatProvider", TestTimeSpanStringWithFormatAndFormatProvider, ConvertResult.Success, TestTimeSpan, FormatAndFormatProviderTimeSpanContext),
                            new ConvertGenericTest<string, Type>("StringToType", TestTypeString, ConvertResult.Success, TestType),
                            new ConvertGenericTest<string, Type>("StringToType", null, ConvertResult.Success, default(Type)),
                            new ConvertGenericTest<string, uint>("StringToUInt", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, uint>("StringToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<string, ulong>("StringToULong", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, ulong>("StringToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<string, Uri>("StringToUri", TestUriString, ConvertResult.Success, TestUri),
                            new ConvertGenericTest<string, Uri>("StringToUri", null, ConvertResult.Success, default(Uri)),
                            new ConvertGenericTest<string, ushort>("StringToUShort", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, ushort>("StringToUShort", null, ConvertResult.Success, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<string, bool?>("StringToNullable<Bool>", "False", ConvertResult.Success, false),
                            new ConvertGenericTest<string, bool?>("StringToNullable<Bool>", "True", ConvertResult.Success, true),
                            new ConvertGenericTest<string, bool?>("StringToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<string, byte?>("StringToNullable<Byte>", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, byte?>("StringToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<string, char?>("StringToNullable<Char>", "*", ConvertResult.Success, '*'),
                            new ConvertGenericTest<string, char?>("StringToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<string, DateTime?>("StringToNullable<DateTime>", TestDateTimeString, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<string, DateTime?>("StringToNullable<DateTime>WithFormat", TestDateTimeStringWithFormat, ConvertResult.Success, TestDateTime, FormatDateTimeContext),
                            new ConvertGenericTest<string, DateTime?>("StringToNullable<DateTime>WithFormatAndFormatProvider", TestDateTimeStringWithFormatAndFormatProvider, ConvertResult.Success, TestDateTime, FormatAndFormatProviderDateTimeContext),
                            new ConvertGenericTest<string, DateTime?>("StringToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new ConvertGenericTest<string, DateTimeOffset?>("StringToNullable<DateTimeOffset>", TestDateTimeOffsetString, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<string, DateTimeOffset?>("StringToNullable<DateTimeOffset>WithFormat", TestDateTimeOffsetStringWithFormat, ConvertResult.Success, TestDateTimeOffset, FormatDateTimeOffsetContext),
                            new ConvertGenericTest<string, DateTimeOffset?>("StringToNullable<DateTimeOffset>WithFormatAndFormatProvider", TestDateTimeOffsetStringWithFormatAndFormatProvider, ConvertResult.Success, TestDateTimeOffset, FormatAndFormatProviderDateTimeOffsetContext),
                            new ConvertGenericTest<string, DateTimeOffset?>("StringToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new ConvertGenericTest<string, decimal?>("StringToNullable<Decimal>", "42.1", ConvertResult.Success, 42.1m),
                            new ConvertGenericTest<string, decimal?>("StringToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<string, double?>("StringToNullable<Double>", "42.2", ConvertResult.Success, 42.2),
                            new ConvertGenericTest<string, double?>("StringToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<string, PrimaryColor?>("StringToNullable<Enum>", "42", ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<string, PrimaryColor?>("StringToNullable<Enum>", TestBlueString, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<string, PrimaryColor?>("StringToNullable<Enum>", TestBlueLowercaseString, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<string, PrimaryColor?>("StringToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<string, float?>("StringToNullable<Float>", "42.3", ConvertResult.Success, (float)42.3),
                            new ConvertGenericTest<string, float?>("StringToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<string, Guid?>("StringToNullable<Guid>", TestGuidString, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<string, Guid?>("StringToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new ConvertGenericTest<string, int?>("StringToNullable<Int>", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, int?>("StringToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<string, long?>("StringToNullable<Long>", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, long?>("StringToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<string, sbyte?>("StringToNullable<SByte>", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, sbyte?>("StringToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<string, short?>("StringToNullable<Short>", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, short?>("StringToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<string, TimeSpan?>("StringToNullable<TimeSpan>", TestTimeSpanString, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<string, TimeSpan?>("StringToNullable<TimeSpan>WithFormat", TestTimeSpanStringWithFormat, ConvertResult.Success, TestTimeSpan, FormatTimeSpanContext),
                            new ConvertGenericTest<string, TimeSpan?>("StringToNullable<TimeSpan>WithFormatAndFormatProvider", TestTimeSpanStringWithFormatAndFormatProvider, ConvertResult.Success, TestTimeSpan, FormatAndFormatProviderTimeSpanContext),
                            new ConvertGenericTest<string, TimeSpan?>("StringToNullable<TimeSpan>", null, ConvertResult.Success, new TimeSpan?()),
                            new ConvertGenericTest<string, uint?>("StringToNullable<UInt>", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, uint?>("StringToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<string, ulong?>("StringToNullable<ULong>", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, ulong?>("StringToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<string, ushort?>("StringToNullable<UShort>", "42", ConvertResult.Success, 42),
                            new ConvertGenericTest<string, ushort?>("StringToNullable<UShort>", null, ConvertResult.Success, new ushort?()),

                            // Interface/Class Types
                            new ConvertGenericTest<string, IInterface>("StringToInterface", "42", ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<string, IInterface>("StringToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<string, BaseClass>("StringToBaseClass", "42", ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<string, BaseClass>("StringToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<string, DerivedClass>("StringToDerivedClass", "42", ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<string, DerivedClass>("StringToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion

        #region TryConvertGenericTestData
        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                new object[]
                    {
                        new AggregateUnitTest("StringToXXX",
                            // Simple Types
                            new TryConvertGenericTest<string, bool>("StringToBool", "False", ConvertResult.Success, false),
                            new TryConvertGenericTest<string, bool>("StringToBool", "True", ConvertResult.Success, true),
                            new TryConvertGenericTest<string, bool>("StringToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<string, byte>("StringToByte", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, byte>("StringToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<string, byte[]>("StringToByteArray", TestByteArrayString, ConvertResult.Success, TestByteArray),
                            new TryConvertGenericTest<string, byte[]>("StringToByteArray", null, ConvertResult.Success, default(byte[])),
                            new TryConvertGenericTest<string, char>("StringToChar", "*", ConvertResult.Success, '*'),
                            new TryConvertGenericTest<string, char>("StringToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<string, DateTime>("StringToDateTime", TestDateTimeString, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<string, DateTime>("StringToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new TryConvertGenericTest<string, DateTime>("StringToDateTimeWithFormat", TestDateTimeStringWithFormat, ConvertResult.Success, TestDateTime, FormatDateTimeContext),
                            new TryConvertGenericTest<string, DateTime>("StringToDateTimeWithFormatAndFormatProvider", TestDateTimeStringWithFormatAndFormatProvider, ConvertResult.Success, TestDateTime, FormatAndFormatProviderDateTimeContext),
                            new TryConvertGenericTest<string, DateTimeOffset>("StringToDateTimeOffset", TestDateTimeOffsetString, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<string, DateTimeOffset>("StringToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new TryConvertGenericTest<string, DateTimeOffset>("StringToDateTimeOffsetWithFormat", TestDateTimeOffsetStringWithFormat, ConvertResult.Success, TestDateTimeOffset, FormatDateTimeOffsetContext),
                            new TryConvertGenericTest<string, DateTimeOffset>("StringToDateTimeOffsetWithFormatAndFormatProvider", TestDateTimeOffsetStringWithFormatAndFormatProvider, ConvertResult.Success, TestDateTimeOffset, FormatAndFormatProviderDateTimeOffsetContext),
                            new TryConvertGenericTest<string, decimal>("StringToDecimal", "42.1", ConvertResult.Success, 42.1m),
                            new TryConvertGenericTest<string, decimal>("StringToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<string, double>("StringToDouble", "42.2", ConvertResult.Success, 42.2),
                            new TryConvertGenericTest<string, double>("StringToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<string, PrimaryColor>("StringToEnum", "42", ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<string, PrimaryColor>("StringToEnum", TestBlueString, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<string, PrimaryColor>("StringToEnum", TestBlueLowercaseString, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<string, PrimaryColor>("StringToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<string, float>("StringToFloat", "42.3", ConvertResult.Success, (float)42.3),
                            new TryConvertGenericTest<string, float>("StringToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<string, Guid>("StringToGuid", TestGuidString, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<string, Guid>("StringToGuid", null, ConvertResult.Success, default(Guid)),
                            new TryConvertGenericTest<string, int>("StringToInt", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, int>("StringToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<string, long>("StringToLong", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, long>("StringToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<string, sbyte>("StringToSByte", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, sbyte>("StringToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<string, short>("StringToShort", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, short>("StringToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<string, string>("StringToString", "The quick brown fox jumps over the lazy dog", ConvertResult.Success, "The quick brown fox jumps over the lazy dog"),
                            new TryConvertGenericTest<string, string>("StringToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<string, TimeSpan>("StringToTimeSpan", TestTimeSpanString, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<string, TimeSpan>("StringToTimeSpan", null, ConvertResult.Success, default(TimeSpan)),
                            new TryConvertGenericTest<string, TimeSpan>("StringToTimeSpanWithFormat", TestTimeSpanStringWithFormat, ConvertResult.Success, TestTimeSpan, FormatTimeSpanContext),
                            new TryConvertGenericTest<string, TimeSpan>("StringToTimeSpanWithFormatAndFormatProvider", TestTimeSpanStringWithFormatAndFormatProvider, ConvertResult.Success, TestTimeSpan, FormatAndFormatProviderTimeSpanContext),
                            new TryConvertGenericTest<string, Type>("StringToType", TestTypeString, ConvertResult.Success, TestType),
                            new TryConvertGenericTest<string, Type>("StringToType", null, ConvertResult.Success, default(Type)),
                            new TryConvertGenericTest<string, uint>("StringToUInt", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, uint>("StringToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<string, ulong>("StringToULong", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, ulong>("StringToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<string, Uri>("StringToUri", TestUriString, ConvertResult.Success, TestUri),
                            new TryConvertGenericTest<string, Uri>("StringToUri", null, ConvertResult.Success, default(Uri)),
                            new TryConvertGenericTest<string, ushort>("StringToUShort", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, ushort>("StringToUShort", null, ConvertResult.Success, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<string, bool?>("StringToNullable<Bool>", "False", ConvertResult.Success, false),
                            new TryConvertGenericTest<string, bool?>("StringToNullable<Bool>", "True", ConvertResult.Success, true),
                            new TryConvertGenericTest<string, bool?>("StringToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<string, byte?>("StringToNullable<Byte>", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, byte?>("StringToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<string, char?>("StringToNullable<Char>", "*", ConvertResult.Success, '*'),
                            new TryConvertGenericTest<string, char?>("StringToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<string, DateTime?>("StringToNullable<DateTime>", TestDateTimeString, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<string, DateTime?>("StringToNullable<DateTime>WithFormat", TestDateTimeStringWithFormat, ConvertResult.Success, TestDateTime, FormatDateTimeContext),
                            new TryConvertGenericTest<string, DateTime?>("StringToNullable<DateTime>WithFormatAndFormatProvider", TestDateTimeStringWithFormatAndFormatProvider, ConvertResult.Success, TestDateTime, FormatAndFormatProviderDateTimeContext),
                            new TryConvertGenericTest<string, DateTime?>("StringToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new TryConvertGenericTest<string, DateTimeOffset?>("StringToNullable<DateTimeOffset>", TestDateTimeOffsetString, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<string, DateTimeOffset?>("StringToNullable<DateTimeOffset>WithFormat", TestDateTimeOffsetStringWithFormat, ConvertResult.Success, TestDateTimeOffset, FormatDateTimeOffsetContext),
                            new TryConvertGenericTest<string, DateTimeOffset?>("StringToNullable<DateTimeOffset>WithFormatAndFormatProvider", TestDateTimeOffsetStringWithFormatAndFormatProvider, ConvertResult.Success, TestDateTimeOffset, FormatAndFormatProviderDateTimeOffsetContext),
                            new TryConvertGenericTest<string, DateTimeOffset?>("StringToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new TryConvertGenericTest<string, decimal?>("StringToNullable<Decimal>", "42.1", ConvertResult.Success, 42.1m),
                            new TryConvertGenericTest<string, decimal?>("StringToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<string, double?>("StringToNullable<Double>", "42.2", ConvertResult.Success, 42.2),
                            new TryConvertGenericTest<string, double?>("StringToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<string, PrimaryColor?>("StringToNullable<Enum>", "42", ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<string, PrimaryColor?>("StringToNullable<Enum>", TestBlueString, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<string, PrimaryColor?>("StringToNullable<Enum>", TestBlueLowercaseString, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<string, PrimaryColor?>("StringToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<string, float?>("StringToNullable<Float>", "42.3", ConvertResult.Success, (float)42.3),
                            new TryConvertGenericTest<string, float?>("StringToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<string, Guid?>("StringToNullable<Guid>", TestGuidString, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<string, Guid?>("StringToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new TryConvertGenericTest<string, int?>("StringToNullable<Int>", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, int?>("StringToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<string, long?>("StringToNullable<Long>", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, long?>("StringToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<string, sbyte?>("StringToNullable<SByte>", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, sbyte?>("StringToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<string, short?>("StringToNullable<Short>", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, short?>("StringToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<string, TimeSpan?>("StringToNullable<TimeSpan>", TestTimeSpanString, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<string, TimeSpan?>("StringToNullable<TimeSpan>WithFormat", TestTimeSpanStringWithFormat, ConvertResult.Success, TestTimeSpan, FormatTimeSpanContext),
                            new TryConvertGenericTest<string, TimeSpan?>("StringToNullable<TimeSpan>WithFormatAndFormatProvider", TestTimeSpanStringWithFormatAndFormatProvider, ConvertResult.Success, TestTimeSpan, FormatAndFormatProviderTimeSpanContext),
                            new TryConvertGenericTest<string, TimeSpan?>("StringToNullable<TimeSpan>", null, ConvertResult.Success, new TimeSpan?()),
                            new TryConvertGenericTest<string, uint?>("StringToNullable<UInt>", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, uint?>("StringToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<string, ulong?>("StringToNullable<ULong>", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, ulong?>("StringToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<string, ushort?>("StringToNullable<UShort>", "42", ConvertResult.Success, 42),
                            new TryConvertGenericTest<string, ushort?>("StringToNullable<UShort>", null, ConvertResult.Success, new ushort?()),

                            // Interface/Class Types
                            new TryConvertGenericTest<string, IInterface>("StringToInterface", "42", ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<string, IInterface>("StringToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<string, BaseClass>("StringToBaseClass", "42", ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<string, BaseClass>("StringToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<string, DerivedClass>("StringToDerivedClass", "42", ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<string, DerivedClass>("StringToDerivedClass", null, ConvertResult.Failure, default(DerivedClass))
                        )
                    }
            };
        #endregion
    }
}
