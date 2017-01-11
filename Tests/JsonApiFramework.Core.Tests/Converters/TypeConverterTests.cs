// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using FluentAssertions;
using JsonApiFramework.Converters;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;
using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public class TypeConverterTests : XUnitTests
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeConverterTests(ITestOutputHelper output)
            : base(output, true)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("ConvertGenericTestData")]
        public void TestTypeConveterConvertGeneric(string name, IUnitTest[] unitTestCollection)
        {
            this.WriteLine(name);
            this.WriteLine();
            this.WriteDashedLine();
            this.WriteLine();

            var outerStopWatch = new Stopwatch();
            var innerStopWatch = new Stopwatch();

            outerStopWatch.Restart();
            foreach (var unitTest in unitTestCollection)
            {
                innerStopWatch.Restart();
                unitTest.Execute(this);
                innerStopWatch.Stop();

                this.WriteLine();
                this.WriteLine("Execution Time = {0:0.000} ms", innerStopWatch.Elapsed.TotalMilliseconds);
                this.WriteLine();
                this.WriteDashedLine();
                this.WriteLine();
            }
            outerStopWatch.Stop();

            this.WriteLine();
            this.WriteLine("Total Execution Time = {0:0.000} ms", outerStopWatch.Elapsed.TotalMilliseconds);

            //this.WriteBuffer();
        }

        [Theory]
        [MemberData("TryConvertGenericTestData")]   
        public void TestTypeConveterTryConvertGeneric(string name, IUnitTest[] unitTestCollection)
        {
            this.WriteLine(name);
            this.WriteLine();
            this.WriteDashedLine();
            this.WriteLine();

            var outerStopWatch = new Stopwatch();
            var innerStopWatch = new Stopwatch();

            outerStopWatch.Restart();
            foreach (var unitTest in unitTestCollection)
            {
                innerStopWatch.Restart();
                unitTest.Execute(this);
                innerStopWatch.Stop();

                this.WriteLine();
                this.WriteLine("Execution Time = {0:0.000} ms", innerStopWatch.Elapsed.TotalMilliseconds);
                this.WriteLine();
                this.WriteDashedLine();
                this.WriteLine();
            }
            outerStopWatch.Stop();

            this.WriteLine();
            this.WriteLine("Total Execution Time = {0:0.000} ms", outerStopWatch.Elapsed.TotalMilliseconds);

            //this.WriteBuffer();
        }
        #endregion

        // PRIVATE FIELDS ////////////////////////////////////////////////////
        #region Test Data

        #region Sample Data
        public const string DefaultDateTimeFormat = "O";
        public const string FullDateTimeFormat = "F";
        public static readonly IFormatProvider SpanishMexicoCulture = CultureInfo.CreateSpecificCulture("es-MX");

        public static readonly TypeConverterContext FormatDateTimeContext = new TypeConverterContext
            {
                Format = FullDateTimeFormat,
                DateTimeStyles = DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
            };

        public static readonly TypeConverterContext FormatAndFormatProviderDateTimeContext = new TypeConverterContext
            {
                Format = FullDateTimeFormat,
                FormatProvider = SpanishMexicoCulture,
                DateTimeStyles = DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
            };

        public static readonly TypeConverterContext FormatDateTimeOffsetContext = new TypeConverterContext
            {
                Format = FullDateTimeFormat,
                DateTimeStyles = DateTimeStyles.AssumeUniversal
            };

        public static readonly TypeConverterContext FormatAndFormatProviderDateTimeOffsetContext = new TypeConverterContext
            {
                Format = FullDateTimeFormat,
                FormatProvider = SpanishMexicoCulture,
                DateTimeStyles = DateTimeStyles.AssumeUniversal
            };

        public static readonly DateTime TestDateTime = new DateTime(1968, 5, 20, 20, 2, 42, 0, DateTimeKind.Utc);
        public static readonly string TestDateTimeString = TestDateTime.ToString(DefaultDateTimeFormat);
        public static readonly string TestDateTimeStringWithFormat = TestDateTime.ToString(FullDateTimeFormat);
        public static readonly string TestDateTimeStringWithFormatAndFormatProvider = TestDateTime.ToString(FullDateTimeFormat, SpanishMexicoCulture);

        public static readonly DateTimeOffset TestDateTimeOffset = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 0, TimeSpan.Zero);
        public static readonly string TestDateTimeOffsetString = TestDateTimeOffset.ToString(DefaultDateTimeFormat);
        public static readonly string TestDateTimeOffsetStringWithFormat = TestDateTimeOffset.ToString(FullDateTimeFormat);
        public static readonly string TestDateTimeOffsetStringWithFormatAndFormatProvider = TestDateTimeOffset.ToString(FullDateTimeFormat, SpanishMexicoCulture);


        public const string DefaultTimeSpanFormat = "c";
        public const string GeneralShortTimeSpanFormat = "g";
        public static readonly IFormatProvider FrenchFranceCulture = CultureInfo.CreateSpecificCulture("fr-FR");

        public static readonly TypeConverterContext FormatTimeSpanContext = new TypeConverterContext
            {
                Format = GeneralShortTimeSpanFormat
            };

        public static readonly TypeConverterContext FormatAndFormatProviderTimeSpanContext = new TypeConverterContext
            {
                Format = GeneralShortTimeSpanFormat,
                FormatProvider = FrenchFranceCulture
            };

        public static readonly TimeSpan TestTimeSpan = new TimeSpan(42, 12, 24, 36, 123);
        public static readonly string TestTimeSpanString = TestTimeSpan.ToString(DefaultTimeSpanFormat);
        public static readonly string TestTimeSpanStringWithFormat = TestTimeSpan.ToString(GeneralShortTimeSpanFormat);
        public static readonly string TestTimeSpanStringWithFormatAndFormatProvider = TestTimeSpan.ToString(GeneralShortTimeSpanFormat, FrenchFranceCulture);

        public const string TestGuidString = "5167e9e1-a15f-41e1-af46-442ffcd37f1b";
        public static readonly Guid TestGuid = new Guid(TestGuidString);
        public static readonly byte[] TestGuidByteArray = TestGuid.ToByteArray();

        public const string TestUriString = "https://api.example.com:8002/api/en-us/articles/42";
        public static readonly Uri TestUri = new Uri(TestUriString);

        public static readonly byte[] TestByteArray = { 42, 24, 48, 84, 12, 21, 68, 86 };
        public const string TestByteArrayString = "KhgwVAwVRFY=";

        public static readonly Type TestType = typeof(TypeConverterTests);
        public static readonly string TestTypeString = TestType.GetCompactQualifiedName();

        public const int TestRedOrdinal = 0;
        public const int TestGreenOrdinal = 1;
        public const int TestBlueOrdinal = 42;
        public const string TestBlueString = "Blue";
        public const string TestBlueLowercaseString = "blue";
        public const string IntegerEnumFormat = "D";
        public const string TestBlueOrdinalAsString = "42";

        public static readonly TypeConverterContext FormatEnumContext = new TypeConverterContext
            {
                Format = IntegerEnumFormat
            };

        // ReSharper disable UnusedMember.Global
        public enum PrimaryColor
        {
            Red = TestRedOrdinal,
            Green = TestGreenOrdinal,
            Blue = TestBlueOrdinal
        };
        // ReSharper restore UnusedMember.Global

        public interface IInterface
        { }

        public class BaseClass : IInterface
        {
            public override string ToString() { return typeof(BaseClass).Name; }
        }

        public class DerivedClass : BaseClass
        {
            public override string ToString() { return typeof(DerivedClass).Name; }
        }

        public static readonly BaseClass TestBaseClass = new BaseClass();
        public static readonly DerivedClass TestDerivedClass = new DerivedClass();
        #endregion

        public static readonly IEnumerable<object[]> ConvertGenericTestData = new[]
            {
                // Simple Types /////////////////////////////////////////////

                #region BoolToXXX
                new object []
                {
                    "BoolToXXX - Generic",
                    new object []
                        {
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
                            new ConvertGenericTest<bool, DerivedClass>("BoolToDerivedClass", true, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region ByteToXXX
                new object []
                {
                    "ByteToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<byte, bool>("ByteToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<byte, byte>("ByteToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte, byte[]>("ByteToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<byte, char>("ByteToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<byte, DateTime>("ByteToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte, DateTimeOffset>("ByteToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
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
                            new ConvertGenericTest<byte, Guid?>("ByteToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
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
                            new ConvertGenericTest<byte, DerivedClass>("ByteToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region ByteArrayToXXX
                new object []
                {
                    "ByteArrayToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<byte[], bool>("ByteArrayToBool", TestByteArray, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<byte[], bool>("ByteArrayToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<byte[], byte>("ByteArrayToByte", TestByteArray, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<byte[], byte>("ByteArrayToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<byte[], byte[]>("ByteArrayToByteArray", TestByteArray, ConvertResult.Success, TestByteArray),
                            new ConvertGenericTest<byte[], byte[]>("ByteArrayToByteArray", null, ConvertResult.Success, null),
                            new ConvertGenericTest<byte[], char>("ByteArrayToChar", TestByteArray, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<byte[], char>("ByteArrayToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<byte[], DateTime>("ByteArrayToDateTime", TestByteArray, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte[], DateTime>("ByteArrayToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte[], DateTimeOffset>("ByteArrayToDateTimeOffset", TestByteArray, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<byte[], DateTimeOffset>("ByteArrayToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<byte[], decimal>("ByteArrayToDecimal", TestByteArray, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<byte[], decimal>("ByteArrayToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<byte[], double>("ByteArrayToDouble", TestByteArray, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<byte[], double>("ByteArrayToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<byte[], PrimaryColor>("ByteArrayToEnum", TestByteArray, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<byte[], PrimaryColor>("ByteArrayToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<byte[], float>("ByteArrayToFloat", TestByteArray, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<byte[], float>("ByteArrayToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<byte[], Guid>("ByteArrayToGuid", TestGuidByteArray, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<byte[], Guid>("ByteArrayToGuid", null, ConvertResult.Success, default(Guid)),
                            new ConvertGenericTest<byte[], int>("ByteArrayToInt", TestByteArray, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<byte[], int>("ByteArrayToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<byte[], long>("ByteArrayToLong", TestByteArray, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<byte[], long>("ByteArrayToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<byte[], sbyte>("ByteArrayToSByte", TestByteArray, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<byte[], sbyte>("ByteArrayToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<byte[], short>("ByteArrayToShort", TestByteArray, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<byte[], short>("ByteArrayToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<byte[], string>("ByteArrayToString", TestByteArray, ConvertResult.Success, TestByteArrayString),
                            new ConvertGenericTest<byte[], string>("ByteArrayToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<byte[], TimeSpan>("ByteArrayToTimeSpan", TestByteArray, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<byte[], TimeSpan>("ByteArrayToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<byte[], Type>("ByteArrayToType", TestByteArray, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<byte[], Type>("ByteArrayToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<byte[], uint>("ByteArrayToUInt", TestByteArray, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<byte[], uint>("ByteArrayToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<byte[], ulong>("ByteArrayToULong", TestByteArray, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<byte[], ulong>("ByteArrayToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<byte[], Uri>("ByteArrayToUri", TestByteArray, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<byte[], Uri>("ByteArrayToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<byte[], ushort>("ByteArrayToUShort", TestByteArray, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<byte[], ushort>("ByteArrayToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<byte[], bool?>("ByteArrayToNullable<Bool>", TestByteArray, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<byte[], bool?>("ByteArrayToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<byte[], byte?>("ByteArrayToNullable<Byte>", TestByteArray, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<byte[], byte?>("ByteArrayToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<byte[], char?>("ByteArrayToNullable<Char>", TestByteArray, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<byte[], char?>("ByteArrayToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<byte[], DateTime?>("ByteArrayToNullable<DateTime>", TestByteArray, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<byte[], DateTime?>("ByteArrayToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<byte[], DateTimeOffset?>("ByteArrayToNullable<DateTimeOffset>", TestByteArray, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<byte[], DateTimeOffset?>("ByteArrayToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<byte[], decimal?>("ByteArrayToNullable<Decimal>", TestByteArray, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<byte[], decimal?>("ByteArrayToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<byte[], double?>("ByteArrayToNullable<Double>", TestByteArray, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<byte[], double?>("ByteArrayToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<byte[], PrimaryColor?>("ByteArrayToNullable<Enum>", TestByteArray, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<byte[], PrimaryColor?>("ByteArrayToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<byte[], float?>("ByteArrayToNullable<Float>", TestByteArray, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<byte[], float?>("ByteArrayToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<byte[], Guid?>("ByteArrayToNullable<Guid>", TestGuidByteArray, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<byte[], Guid?>("ByteArrayToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new ConvertGenericTest<byte[], int?>("ByteArrayToNullable<Int>", TestByteArray, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<byte[], int?>("ByteArrayToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<byte[], long?>("ByteArrayToNullable<Long>", TestByteArray, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<byte[], long?>("ByteArrayToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<byte[], sbyte?>("ByteArrayToNullable<SByte>", TestByteArray, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<byte[], sbyte?>("ByteArrayToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<byte[], short?>("ByteArrayToNullable<Short>", TestByteArray, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<byte[], short?>("ByteArrayToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<byte[], TimeSpan?>("ByteArrayToNullable<TimeSpan>", TestByteArray, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<byte[], TimeSpan?>("ByteArrayToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<byte[], uint?>("ByteArrayToNullable<UInt>", TestByteArray, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<byte[], uint?>("ByteArrayToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<byte[], ulong?>("ByteArrayToNullable<ULong>", TestByteArray, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<byte[], ulong?>("ByteArrayToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<byte[], ushort?>("ByteArrayToNullable<UShort>", TestByteArray, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<byte[], ushort?>("ByteArrayToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<byte[], IInterface>("ByteArrayToInterface", TestByteArray, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<byte[], IInterface>("ByteArrayToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<byte[], BaseClass>("ByteArrayToBaseClass", TestByteArray, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<byte[], BaseClass>("ByteArrayToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<byte[], DerivedClass>("ByteArrayToDerivedClass", TestByteArray, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<byte[], DerivedClass>("ByteArrayToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region CharToXXX
                new object []
                {
                    "CharToXXX - Generic",
                    new object []
                        {
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
                            new ConvertGenericTest<char, DerivedClass>("CharToDerivedClass", '*', ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DateTimeToXXX
                new object []
                {
                    "DateTimeToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<DateTime, bool>("DateTimeToBool", TestDateTime, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTime, byte>("DateTimeToByte", TestDateTime, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTime, byte[]>("DateTimeToByteArray", TestDateTime, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTime, char>("DateTimeToChar", TestDateTime, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTime, DateTime>("DateTimeToDateTime", TestDateTime, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTime, DateTimeOffset>("DateTimeToDateTimeOffset", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTime, decimal>("DateTimeToDecimal", TestDateTime, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTime, double>("DateTimeToDouble", TestDateTime, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTime, PrimaryColor>("DateTimeToEnum", TestDateTime, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTime, float>("DateTimeToFloat", TestDateTime, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTime, Guid>("DateTimeToGuid", TestDateTime, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTime, int>("DateTimeToInt", TestDateTime, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTime, long>("DateTimeToLong", TestDateTime, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTime, sbyte>("DateTimeToSByte", TestDateTime, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTime, short>("DateTimeToShort", TestDateTime, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTime, string>("DateTimeToString", TestDateTime, ConvertResult.Success, TestDateTimeString),
                            new ConvertGenericTest<DateTime, string>("DateTimeToStringWithFormat", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormat, FormatDateTimeContext),
                            new ConvertGenericTest<DateTime, string>("DateTimeToStringWithFormatAndFormatProvider", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeContext),
                            new ConvertGenericTest<DateTime, TimeSpan>("DateTimeToTimeSpan", TestDateTime, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTime, Type>("DateTimeToType", TestDateTime, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTime, uint>("DateTimeToUInt", TestDateTime, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTime, ulong>("DateTimeToULong", TestDateTime, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTime, Uri>("DateTimeToUri", TestDateTime, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTime, ushort>("DateTimeToUShort", TestDateTime, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DateTime, bool?>("DateTimeToNullable<Bool>", TestDateTime, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTime, byte?>("DateTimeToNullable<Byte>", TestDateTime, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTime, char?>("DateTimeToNullable<Char>", TestDateTime, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTime, DateTime?>("DateTimeToNullable<DateTime>", TestDateTime, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTime, DateTimeOffset?>("DateTimeToNullable<DateTimeOffset>", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTime, decimal?>("DateTimeToNullable<Decimal>", TestDateTime, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTime, double?>("DateTimeToNullable<Double>", TestDateTime, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTime, PrimaryColor?>("DateTimeToNullable<Enum>", TestDateTime, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTime, float?>("DateTimeToNullable<Float>", TestDateTime, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTime, Guid?>("DateTimeToNullable<Guid>", TestDateTime, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<DateTime, int?>("DateTimeToNullable<Int>", TestDateTime, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTime, long?>("DateTimeToNullable<Long>", TestDateTime, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTime, sbyte?>("DateTimeToNullable<SByte>", TestDateTime, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTime, short?>("DateTimeToNullable<Short>", TestDateTime, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTime, TimeSpan?>("DateTimeToNullable<TimeSpan>", TestDateTime, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<DateTime, uint?>("DateTimeToNullable<UInt>", TestDateTime, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTime, ulong?>("DateTimeToNullable<ULong>", TestDateTime, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTime, ushort?>("DateTimeToNullable<UShort>", TestDateTime, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DateTime, IInterface>("DateTimeToInterface", TestDateTime, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTime, BaseClass>("DateTimeToBaseClass", TestDateTime, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTime, DerivedClass>("DateTimeToDerivedClass", TestDateTime, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DateTimeOffsetToXXX
                new object []
                {
                    "DateTimeOffsetToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<DateTimeOffset, bool>("DateTimeOffsetToBool", TestDateTimeOffset, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTimeOffset, byte>("DateTimeOffsetToByte", TestDateTimeOffset, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTimeOffset, byte[]>("DateTimeOffsetToByteArray", TestDateTimeOffset, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTimeOffset, char>("DateTimeOffsetToChar", TestDateTimeOffset, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTimeOffset, DateTime>("DateTimeOffsetToDateTime", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTimeOffset, DateTimeOffset>("DateTimeOffsetToDateTimeOffset", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTimeOffset, decimal>("DateTimeOffsetToDecimal", TestDateTimeOffset, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTimeOffset, double>("DateTimeOffsetToDouble", TestDateTimeOffset, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTimeOffset, PrimaryColor>("DateTimeOffsetToEnum", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTimeOffset, float>("DateTimeOffsetToFloat", TestDateTimeOffset, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTimeOffset, Guid>("DateTimeOffsetToGuid", TestDateTimeOffset, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTimeOffset, int>("DateTimeOffsetToInt", TestDateTimeOffset, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTimeOffset, long>("DateTimeOffsetToLong", TestDateTimeOffset, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTimeOffset, sbyte>("DateTimeOffsetToSByte", TestDateTimeOffset, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTimeOffset, short>("DateTimeOffsetToShort", TestDateTimeOffset, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToString", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetString),
                            new ConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToStringWithFormat", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormat, FormatDateTimeOffsetContext),
                            new ConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToStringWithFormatAndFormatProvider", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeOffsetContext),
                            new ConvertGenericTest<DateTimeOffset, TimeSpan>("DateTimeOffsetToTimeSpan", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTimeOffset, Type>("DateTimeOffsetToType", TestDateTimeOffset, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTimeOffset, uint>("DateTimeOffsetToUInt", TestDateTimeOffset, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTimeOffset, ulong>("DateTimeOffsetToULong", TestDateTimeOffset, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTimeOffset, Uri>("DateTimeOffsetToUri", TestDateTimeOffset, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTimeOffset, ushort>("DateTimeOffsetToUShort", TestDateTimeOffset, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DateTimeOffset, bool?>("DateTimeOffsetToNullable<Bool>", TestDateTimeOffset, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTimeOffset, byte?>("DateTimeOffsetToNullable<Byte>", TestDateTimeOffset, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTimeOffset, char?>("DateTimeOffsetToNullable<Char>", TestDateTimeOffset, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTimeOffset, DateTime?>("DateTimeOffsetToNullable<DateTime>", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTimeOffset, DateTimeOffset?>("DateTimeOffsetToNullable<DateTimeOffset>", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTimeOffset, decimal?>("DateTimeOffsetToNullable<Decimal>", TestDateTimeOffset, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTimeOffset, double?>("DateTimeOffsetToNullable<Double>", TestDateTimeOffset, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTimeOffset, PrimaryColor?>("DateTimeOffsetToNullable<Enum>", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTimeOffset, float?>("DateTimeOffsetToNullable<Float>", TestDateTimeOffset, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTimeOffset, Guid?>("DateTimeOffsetToNullable<Guid>", TestDateTimeOffset, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<DateTimeOffset, int?>("DateTimeOffsetToNullable<Int>", TestDateTimeOffset, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTimeOffset, long?>("DateTimeOffsetToNullable<Long>", TestDateTimeOffset, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTimeOffset, sbyte?>("DateTimeOffsetToNullable<SByte>", TestDateTimeOffset, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTimeOffset, short?>("DateTimeOffsetToNullable<Short>", TestDateTimeOffset, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTimeOffset, TimeSpan?>("DateTimeOffsetToNullable<TimeSpan>", TestDateTimeOffset, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<DateTimeOffset, uint?>("DateTimeOffsetToNullable<UInt>", TestDateTimeOffset, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTimeOffset, ulong?>("DateTimeOffsetToNullable<ULong>", TestDateTimeOffset, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTimeOffset, ushort?>("DateTimeOffsetToNullable<UShort>", TestDateTimeOffset, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DateTimeOffset, IInterface>("DateTimeOffsetToInterface", TestDateTimeOffset, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTimeOffset, BaseClass>("DateTimeOffsetToBaseClass", TestDateTimeOffset, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTimeOffset, DerivedClass>("DateTimeOffsetToDerivedClass", TestDateTimeOffset, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DecimalToXXX
                new object []
                {
                    "DecimalToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<decimal, bool>("DecimalToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<decimal, byte>("DecimalToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, byte[]>("DecimalToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<decimal, char>("DecimalToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<decimal, DateTime>("DecimalToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<decimal, DateTimeOffset>("DecimalToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<decimal, decimal>("DecimalToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, double>("DecimalToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, PrimaryColor>("DecimalToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<decimal, float>("DecimalToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, Guid>("DecimalToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<decimal, int>("DecimalToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, long>("DecimalToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, sbyte>("DecimalToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, short>("DecimalToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, string>("DecimalToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<decimal, TimeSpan>("DecimalToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<decimal, Type>("DecimalToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<decimal, uint>("DecimalToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, ulong>("DecimalToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, Uri>("DecimalToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<decimal, ushort>("DecimalToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<decimal, bool?>("DecimalToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<decimal, byte?>("DecimalToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, char?>("DecimalToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<decimal, DateTime?>("DecimalToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<decimal, DateTimeOffset?>("DecimalToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<decimal, decimal?>("DecimalToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, double?>("DecimalToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, PrimaryColor?>("DecimalToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<decimal, float?>("DecimalToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, Guid?>("DecimalToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<decimal, int?>("DecimalToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, long?>("DecimalToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, sbyte?>("DecimalToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, short?>("DecimalToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, TimeSpan?>("DecimalToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<decimal, uint?>("DecimalToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, ulong?>("DecimalToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal, ushort?>("DecimalToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<decimal, IInterface>("DecimalToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<decimal, BaseClass>("DecimalToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<decimal, DerivedClass>("DecimalToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DoubleToXXX
                new object []
                {
                    "DoubleToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<double, bool>("DoubleToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<double, byte>("DoubleToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, byte[]>("DoubleToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<double, char>("DoubleToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<double, DateTime>("DoubleToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<double, DateTimeOffset>("DoubleToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<double, decimal>("DoubleToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, double>("DoubleToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, PrimaryColor>("DoubleToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<double, float>("DoubleToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, Guid>("DoubleToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<double, int>("DoubleToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, long>("DoubleToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, sbyte>("DoubleToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, short>("DoubleToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, string>("DoubleToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<double, TimeSpan>("DoubleToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<double, Type>("DoubleToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<double, uint>("DoubleToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, ulong>("DoubleToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, Uri>("DoubleToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<double, ushort>("DoubleToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<double, bool?>("DoubleToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<double, byte?>("DoubleToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, char?>("DoubleToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<double, DateTime?>("DoubleToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<double, DateTimeOffset?>("DoubleToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<double, decimal?>("DoubleToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, double?>("DoubleToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, PrimaryColor?>("DoubleToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<double, float?>("DoubleToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, Guid?>("DoubleToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<double, int?>("DoubleToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, long?>("DoubleToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, sbyte?>("DoubleToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, short?>("DoubleToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, TimeSpan?>("DoubleToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<double, uint?>("DoubleToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, ulong?>("DoubleToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double, ushort?>("DoubleToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<double, IInterface>("DoubleToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<double, BaseClass>("DoubleToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<double, DerivedClass>("DoubleToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region EnumToXXX
                new object []
                {
                    "EnumToXXX - Generic",
                    new object []
                        {
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
                            new ConvertGenericTest<PrimaryColor, DerivedClass>("EnumToDerivedClass", PrimaryColor.Blue, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region FloatToXXX
                new object []
                {
                    "FloatToXXX - Generic",
                    new object []
                        {
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
                            new ConvertGenericTest<float, DerivedClass>("FloatToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region GuidToXXX
                new object []
                {
                    "GuidToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<Guid, bool>("GuidToBool", TestGuid, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Guid, byte>("GuidToByte", TestGuid, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Guid, byte[]>("GuidToByteArray", TestGuid, ConvertResult.Success, TestGuidByteArray),
                            new ConvertGenericTest<Guid, char>("GuidToChar", TestGuid, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Guid, DateTime>("GuidToDateTime", TestGuid, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Guid, DateTimeOffset>("GuidToDateTimeOffset", TestGuid, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Guid, decimal>("GuidToDecimal", TestGuid, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Guid, double>("GuidToDouble", TestGuid, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Guid, PrimaryColor>("GuidToEnum", TestGuid, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Guid, float>("GuidToFloat", TestGuid, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Guid, Guid>("GuidToGuid", TestGuid, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<Guid, int>("GuidToInt", TestGuid, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Guid, long>("GuidToLong", TestGuid, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Guid, sbyte>("GuidToSByte", TestGuid, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Guid, short>("GuidToShort", TestGuid, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Guid, string>("GuidToString", TestGuid, ConvertResult.Success, TestGuidString),
                            new ConvertGenericTest<Guid, TimeSpan>("GuidToTimeSpan", TestGuid, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Guid, Type>("GuidToType", TestGuid, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Guid, uint>("GuidToUInt", TestGuid, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Guid, ulong>("GuidToULong", TestGuid, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Guid, Uri>("GuidToUri", TestGuid, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Guid, ushort>("GuidToUShort", TestGuid, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<Guid, bool?>("GuidToNullable<Bool>", TestGuid, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Guid, byte?>("GuidToNullable<Byte>", TestGuid, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Guid, char?>("GuidToNullable<Char>", TestGuid, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Guid, DateTime?>("GuidToNullable<DateTime>", TestGuid, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Guid, DateTimeOffset?>("GuidToNullable<DateTimeOffset>", TestGuid, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Guid, decimal?>("GuidToNullable<Decimal>", TestGuid, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Guid, double?>("GuidToNullable<Double>", TestGuid, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Guid, PrimaryColor?>("GuidToNullable<Enum>", TestGuid, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Guid, float?>("GuidToNullable<Float>", TestGuid, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Guid, Guid?>("GuidToNullable<Guid>", TestGuid, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<Guid, int?>("GuidToNullable<Int>", TestGuid, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Guid, long?>("GuidToNullable<Long>", TestGuid, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Guid, sbyte?>("GuidToNullable<SByte>", TestGuid, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Guid, short?>("GuidToNullable<Short>", TestGuid, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Guid, TimeSpan?>("GuidToNullable<TimeSpan>", TestGuid, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Guid, uint?>("GuidToNullable<UInt>", TestGuid, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Guid, ulong?>("GuidToNullable<ULong>", TestGuid, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Guid, ushort?>("GuidToNullable<UShort>", TestGuid, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<Guid, IInterface>("GuidToInterface", TestGuid, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Guid, BaseClass>("GuidToBaseClass", TestGuid, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Guid, DerivedClass>("GuidToDerivedClass", TestGuid, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region IntToXXX
                new object []
                {
                    "IntToXXX - Generic",
                    new object []
                        {
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
                            new ConvertGenericTest<int, DerivedClass>("IntToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region LongToXXX
                new object []
                {
                    "LongToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<long, bool>("LongToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<long, byte>("LongToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, byte[]>("LongToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<long, char>("LongToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<long, DateTime>("LongToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<long, DateTimeOffset>("LongToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<long, decimal>("LongToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, double>("LongToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, PrimaryColor>("LongToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<long, float>("LongToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, Guid>("LongToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<long, int>("LongToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, long>("LongToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, sbyte>("LongToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, short>("LongToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, string>("LongToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<long, TimeSpan>("LongToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<long, Type>("LongToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<long, uint>("LongToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, ulong>("LongToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, Uri>("LongToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<long, ushort>("LongToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<long, bool?>("LongToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<long, byte?>("LongToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, char?>("LongToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<long, DateTime?>("LongToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<long, DateTimeOffset?>("LongToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<long, decimal?>("LongToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, double?>("LongToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, PrimaryColor?>("LongToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<long, float?>("LongToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, Guid?>("LongToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<long, int?>("LongToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, long?>("LongToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, sbyte?>("LongToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, short?>("LongToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, TimeSpan?>("LongToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<long, uint?>("LongToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, ulong?>("LongToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long, ushort?>("LongToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<long, IInterface>("LongToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<long, BaseClass>("LongToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<long, DerivedClass>("LongToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region SByteToXXX
                new object []
                {
                    "SByteToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<sbyte, bool>("SByteToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<sbyte, byte>("SByteToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, byte[]>("SByteToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<sbyte, char>("SByteToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<sbyte, DateTime>("SByteToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<sbyte, DateTimeOffset>("SByteToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<sbyte, decimal>("SByteToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, double>("SByteToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, PrimaryColor>("SByteToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<sbyte, float>("SByteToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, Guid>("SByteToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<sbyte, int>("SByteToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, long>("SByteToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, sbyte>("SByteToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, short>("SByteToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, string>("SByteToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<sbyte, TimeSpan>("SByteToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<sbyte, Type>("SByteToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<sbyte, uint>("SByteToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, ulong>("SByteToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, Uri>("SByteToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<sbyte, ushort>("SByteToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<sbyte, bool?>("SByteToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<sbyte, byte?>("SByteToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, char?>("SByteToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<sbyte, DateTime?>("SByteToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<sbyte, DateTimeOffset?>("SByteToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<sbyte, decimal?>("SByteToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, double?>("SByteToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, PrimaryColor?>("SByteToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<sbyte, float?>("SByteToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, Guid?>("SByteToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<sbyte, int?>("SByteToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, long?>("SByteToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, sbyte?>("SByteToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, short?>("SByteToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, TimeSpan?>("SByteToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<sbyte, uint?>("SByteToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, ulong?>("SByteToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte, ushort?>("SByteToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<sbyte, IInterface>("SByteToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<sbyte, BaseClass>("SByteToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<sbyte, DerivedClass>("SByteToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region ShortToXXX
                new object []
                {
                    "ShortToXXX - Generic",
                    new object []
                        {
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
                            new ConvertGenericTest<short, DerivedClass>("ShortToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region StringToXXX
                new object []
                {
                    "StringToXXX - Generic",
                    new object []
                        {
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
                            new ConvertGenericTest<string, DerivedClass>("StringToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region TimeSpanToXXX
                new object []
                {
                    "TimeSpanToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<TimeSpan, bool>("TimeSpanToBool", TestTimeSpan, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<TimeSpan, byte>("TimeSpanToByte", TestTimeSpan, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<TimeSpan, byte[]>("TimeSpanToByteArray", TestTimeSpan, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<TimeSpan, char>("TimeSpanToChar", TestTimeSpan, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<TimeSpan, DateTime>("TimeSpanToDateTime", TestTimeSpan, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<TimeSpan, DateTimeOffset>("TimeSpanToDateTimeOffset", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<TimeSpan, decimal>("TimeSpanToDecimal", TestTimeSpan, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<TimeSpan, double>("TimeSpanToDouble", TestTimeSpan, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<TimeSpan, PrimaryColor>("TimeSpanToEnum", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<TimeSpan, float>("TimeSpanToFloat", TestTimeSpan, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<TimeSpan, Guid>("TimeSpanToGuid", TestTimeSpan, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<TimeSpan, int>("TimeSpanToInt", TestTimeSpan, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<TimeSpan, long>("TimeSpanToLong", TestTimeSpan, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<TimeSpan, sbyte>("TimeSpanToSByte", TestTimeSpan, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<TimeSpan, short>("TimeSpanToShort", TestTimeSpan, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<TimeSpan, string>("TimeSpanToString", TestTimeSpan, ConvertResult.Success, TestTimeSpanString),
                            new ConvertGenericTest<TimeSpan, string>("TimeSpanToStringWithFormat", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormat, FormatTimeSpanContext),
                            new ConvertGenericTest<TimeSpan, string>("TimeSpanToStringWithFormatAndFormatProvider", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormatAndFormatProvider, FormatAndFormatProviderTimeSpanContext),
                            new ConvertGenericTest<TimeSpan, TimeSpan>("TimeSpanToTimeSpan", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<TimeSpan, Type>("TimeSpanToType", TestTimeSpan, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<TimeSpan, uint>("TimeSpanToUInt", TestTimeSpan, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<TimeSpan, ulong>("TimeSpanToULong", TestTimeSpan, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<TimeSpan, Uri>("TimeSpanToUri", TestTimeSpan, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<TimeSpan, ushort>("TimeSpanToUShort", TestTimeSpan, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<TimeSpan, bool?>("TimeSpanToNullable<Bool>", TestTimeSpan, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<TimeSpan, byte?>("TimeSpanToNullable<Byte>", TestTimeSpan, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<TimeSpan, char?>("TimeSpanToNullable<Char>", TestTimeSpan, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<TimeSpan, DateTime?>("TimeSpanToNullable<DateTime>", TestTimeSpan, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<TimeSpan, DateTimeOffset?>("TimeSpanToNullable<DateTimeOffset>", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<TimeSpan, decimal?>("TimeSpanToNullable<Decimal>", TestTimeSpan, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<TimeSpan, double?>("TimeSpanToNullable<Double>", TestTimeSpan, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<TimeSpan, PrimaryColor?>("TimeSpanToNullable<Enum>", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<TimeSpan, float?>("TimeSpanToNullable<Float>", TestTimeSpan, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<TimeSpan, Guid?>("TimeSpanToNullable<Guid>", TestTimeSpan, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<TimeSpan, int?>("TimeSpanToNullable<Int>", TestTimeSpan, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<TimeSpan, long?>("TimeSpanToNullable<Long>", TestTimeSpan, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<TimeSpan, sbyte?>("TimeSpanToNullable<SByte>", TestTimeSpan, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<TimeSpan, short?>("TimeSpanToNullable<Short>", TestTimeSpan, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<TimeSpan, TimeSpan?>("TimeSpanToNullable<TimeSpan>", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<TimeSpan, uint?>("TimeSpanToNullable<UInt>", TestTimeSpan, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<TimeSpan, ulong?>("TimeSpanToNullable<ULong>", TestTimeSpan, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<TimeSpan, ushort?>("TimeSpanToNullable<UShort>", TestTimeSpan, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<TimeSpan, IInterface>("TimeSpanToInterface", TestTimeSpan, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<TimeSpan, BaseClass>("TimeSpanToBaseClass", TestTimeSpan, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<TimeSpan, DerivedClass>("TimeSpanToDerivedClass", TestTimeSpan, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region TypeToXXX
                new object []
                {
                    "TypeToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<Type, bool>("TypeToBool", TestType, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Type, bool>("TypeToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Type, byte>("TypeToByte", TestType, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Type, byte>("TypeToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Type, byte[]>("TypeToByteArray", TestType, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<Type, byte[]>("TypeToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<Type, char>("TypeToChar", TestType, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Type, char>("TypeToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Type, DateTime>("TypeToDateTime", TestType, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Type, DateTime>("TypeToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Type, DateTimeOffset>("TypeToDateTimeOffset", TestType, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Type, DateTimeOffset>("TypeToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Type, decimal>("TypeToDecimal", TestType, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Type, decimal>("TypeToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Type, double>("TypeToDouble", TestType, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Type, double>("TypeToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Type, PrimaryColor>("TypeToEnum", TestType, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Type, PrimaryColor>("TypeToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Type, float>("TypeToFloat", TestType, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Type, float>("TypeToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Type, Guid>("TypeToGuid", TestType, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<Type, Guid>("TypeToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<Type, int>("TypeToInt", TestType, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Type, int>("TypeToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Type, long>("TypeToLong", TestType, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Type, long>("TypeToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Type, sbyte>("TypeToSByte", TestType, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Type, sbyte>("TypeToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Type, short>("TypeToShort", TestType, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Type, short>("TypeToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Type, string>("TypeToString", TestType, ConvertResult.Success, TestTypeString),
                            new ConvertGenericTest<Type, string>("TypeToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<Type, TimeSpan>("TypeToTimeSpan", TestType, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Type, TimeSpan>("TypeToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Type, Type>("TypeToType", TestType, ConvertResult.Success, TestType),
                            new ConvertGenericTest<Type, Type>("TypeToType", null, ConvertResult.Success, default(Type)),
                            new ConvertGenericTest<Type, uint>("TypeToUInt", TestType, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Type, uint>("TypeToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Type, ulong>("TypeToULong", TestType, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Type, ulong>("TypeToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Type, Uri>("TypeToUri", TestType, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Type, Uri>("TypeToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Type, ushort>("TypeToUShort", TestType, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<Type, ushort>("TypeToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<Type, bool?>("TypeToNullable<Bool>", TestType, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Type, bool?>("TypeToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Type, byte?>("TypeToNullable<Byte>", TestType, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Type, byte?>("TypeToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Type, char?>("TypeToNullable<Char>", TestType, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Type, char?>("TypeToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Type, DateTime?>("TypeToNullable<DateTime>", TestType, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Type, DateTime?>("TypeToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Type, DateTimeOffset?>("TypeToNullable<DateTimeOffset>", TestType, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Type, DateTimeOffset?>("TypeToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Type, decimal?>("TypeToNullable<Decimal>", TestType, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Type, decimal?>("TypeToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Type, double?>("TypeToNullable<Double>", TestType, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Type, double?>("TypeToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Type, PrimaryColor?>("TypeToNullable<Enum>", TestType, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Type, PrimaryColor?>("TypeToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Type, float?>("TypeToNullable<Float>", TestType, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Type, float?>("TypeToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Type, Guid?>("TypeToNullable<Guid>", TestType, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<Type, Guid?>("TypeToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<Type, int?>("TypeToNullable<Int>", TestType, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Type, int?>("TypeToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Type, long?>("TypeToNullable<Long>", TestType, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Type, long?>("TypeToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Type, sbyte?>("TypeToNullable<SByte>", TestType, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Type, sbyte?>("TypeToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Type, short?>("TypeToNullable<Short>", TestType, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Type, short?>("TypeToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Type, TimeSpan?>("TypeToNullable<TimeSpan>", TestType, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Type, TimeSpan?>("TypeToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Type, uint?>("TypeToNullable<UInt>", TestType, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Type, uint?>("TypeToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Type, ulong?>("TypeToNullable<ULong>", TestType, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Type, ulong?>("TypeToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Type, ushort?>("TypeToNullable<UShort>", TestType, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<Type, ushort?>("TypeToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<Type, IInterface>("TypeToInterface", TestType, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Type, IInterface>("TypeToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Type, BaseClass>("TypeToBaseClass", TestType, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Type, BaseClass>("TypeToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Type, DerivedClass>("TypeToDerivedClass", TestType, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<Type, DerivedClass>("TypeToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region UIntToXXX
                new object []
                {
                    "UIntToXXX - Generic",
                    new object []
                        {
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
                            new ConvertGenericTest<uint, DerivedClass>("UIntToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region ULongToXXX
                new object []
                {
                    "ULongToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<ulong, bool>("ULongToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ulong, byte>("ULongToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, byte[]>("ULongToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ulong, char>("ULongToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ulong, DateTime>("ULongToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ulong, DateTimeOffset>("ULongToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ulong, decimal>("ULongToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, double>("ULongToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, PrimaryColor>("ULongToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ulong, float>("ULongToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, Guid>("ULongToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ulong, int>("ULongToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, long>("ULongToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, sbyte>("ULongToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, short>("ULongToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, string>("ULongToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<ulong, TimeSpan>("ULongToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ulong, Type>("ULongToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ulong, uint>("ULongToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, ulong>("ULongToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, Uri>("ULongToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ulong, ushort>("ULongToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<ulong, bool?>("ULongToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ulong, byte?>("ULongToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, char?>("ULongToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ulong, DateTime?>("ULongToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ulong, DateTimeOffset?>("ULongToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ulong, decimal?>("ULongToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, double?>("ULongToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, PrimaryColor?>("ULongToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ulong, float?>("ULongToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, Guid?>("ULongToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ulong, int?>("ULongToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, long?>("ULongToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, sbyte?>("ULongToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, short?>("ULongToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, TimeSpan?>("ULongToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ulong, uint?>("ULongToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, ulong?>("ULongToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong, ushort?>("ULongToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<ulong, IInterface>("ULongToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ulong, BaseClass>("ULongToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ulong, DerivedClass>("ULongToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region UriToXXX
                new object []
                {
                    "UriToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<Uri, bool>("UriToBool", TestUri, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Uri, bool>("UriToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Uri, byte>("UriToByte", TestUri, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Uri, byte>("UriToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Uri, byte[]>("UriToByteArray", TestUri, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<Uri, byte[]>("UriToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<Uri, char>("UriToChar", TestUri, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Uri, char>("UriToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Uri, DateTime>("UriToDateTime", TestUri, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Uri, DateTime>("UriToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Uri, DateTimeOffset>("UriToDateTimeOffset", TestUri, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Uri, DateTimeOffset>("UriToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Uri, decimal>("UriToDecimal", TestUri, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Uri, decimal>("UriToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Uri, double>("UriToDouble", TestUri, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Uri, double>("UriToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Uri, PrimaryColor>("UriToEnum", TestUri, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Uri, PrimaryColor>("UriToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Uri, float>("UriToFloat", TestUri, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Uri, float>("UriToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Uri, Guid>("UriToGuid", TestUri, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<Uri, Guid>("UriToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<Uri, int>("UriToInt", TestUri, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Uri, int>("UriToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Uri, long>("UriToLong", TestUri, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Uri, long>("UriToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Uri, sbyte>("UriToSByte", TestUri, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Uri, sbyte>("UriToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Uri, short>("UriToShort", TestUri, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Uri, short>("UriToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Uri, string>("UriToString", TestUri, ConvertResult.Success, TestUriString),
                            new ConvertGenericTest<Uri, string>("UriToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<Uri, TimeSpan>("UriToTimeSpan", TestUri, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Uri, TimeSpan>("UriToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Uri, Type>("UriToType", TestUri, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Uri, Type>("UriToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Uri, uint>("UriToUInt", TestUri, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Uri, uint>("UriToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Uri, ulong>("UriToULong", TestUri, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Uri, ulong>("UriToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Uri, Uri>("UriToUri", TestUri, ConvertResult.Success, TestUri),
                            new ConvertGenericTest<Uri, Uri>("UriToUri", null, ConvertResult.Success, default(Uri)),
                            new ConvertGenericTest<Uri, ushort>("UriToUShort", TestUri, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<Uri, ushort>("UriToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<Uri, bool?>("UriToNullable<Bool>", TestUri, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Uri, bool?>("UriToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Uri, byte?>("UriToNullable<Byte>", TestUri, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Uri, byte?>("UriToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Uri, char?>("UriToNullable<Char>", TestUri, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Uri, char?>("UriToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Uri, DateTime?>("UriToNullable<DateTime>", TestUri, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Uri, DateTime?>("UriToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<Uri, DateTimeOffset?>("UriToNullable<DateTimeOffset>", TestUri, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Uri, DateTimeOffset?>("UriToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<Uri, decimal?>("UriToNullable<Decimal>", TestUri, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Uri, decimal?>("UriToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Uri, double?>("UriToNullable<Double>", TestUri, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Uri, double?>("UriToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Uri, PrimaryColor?>("UriToNullable<Enum>", TestUri, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Uri, PrimaryColor?>("UriToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Uri, float?>("UriToNullable<Float>", TestUri, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Uri, float?>("UriToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Uri, Guid?>("UriToNullable<Guid>", TestUri, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<Uri, Guid?>("UriToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<Uri, int?>("UriToNullable<Int>", TestUri, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Uri, int?>("UriToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Uri, long?>("UriToNullable<Long>", TestUri, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Uri, long?>("UriToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Uri, sbyte?>("UriToNullable<SByte>", TestUri, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Uri, sbyte?>("UriToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Uri, short?>("UriToNullable<Short>", TestUri, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Uri, short?>("UriToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Uri, TimeSpan?>("UriToNullable<TimeSpan>", TestUri, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Uri, TimeSpan?>("UriToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<Uri, uint?>("UriToNullable<UInt>", TestUri, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Uri, uint?>("UriToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Uri, ulong?>("UriToNullable<ULong>", TestUri, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Uri, ulong?>("UriToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Uri, ushort?>("UriToNullable<UShort>", TestUri, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<Uri, ushort?>("UriToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<Uri, IInterface>("UriToInterface", TestUri, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Uri, IInterface>("UriToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Uri, BaseClass>("UriToBaseClass", TestUri, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Uri, BaseClass>("UriToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Uri, DerivedClass>("UriToDerivedClass", TestUri, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<Uri, DerivedClass>("UriToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region UShortToXXX
                new object []
                {
                    "UShortToXXX - Generic",
                    new object []
                        {
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
                            new ConvertGenericTest<ushort, DerivedClass>("UShortToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                // Nullable Types ///////////////////////////////////////////

                #region Nullable<Bool>ToXXX
                new object []
                {
                    "Nullable<Bool>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<bool?, bool>("Nullable<Bool>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<bool?, bool>("Nullable<Bool>ToBool", true, ConvertResult.Success, true),
                            new ConvertGenericTest<bool?, byte>("Nullable<Bool>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<bool?, byte>("Nullable<Bool>ToByte", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, byte[]>("Nullable<Bool>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<bool?, byte[]>("Nullable<Bool>ToByteArray", true, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<bool?, char>("Nullable<Bool>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<bool?, char>("Nullable<Bool>ToChar", true, ConvertResult.Success, (char)1),
                            new ConvertGenericTest<bool?, DateTime>("Nullable<Bool>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<bool?, DateTime>("Nullable<Bool>ToDateTime", true, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<bool?, DateTimeOffset>("Nullable<Bool>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<bool?, DateTimeOffset>("Nullable<Bool>ToDateTimeOffset", true, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<bool?, decimal>("Nullable<Bool>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<bool?, decimal>("Nullable<Bool>ToDecimal", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, double>("Nullable<Bool>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<bool?, double>("Nullable<Bool>ToDouble", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, PrimaryColor>("Nullable<Bool>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<bool?, PrimaryColor>("Nullable<Bool>ToEnum", true, ConvertResult.Success, PrimaryColor.Green),
                            new ConvertGenericTest<bool?, float>("Nullable<Bool>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<bool?, float>("Nullable<Bool>ToFloat", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, Guid>("Nullable<Bool>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<bool?, Guid>("Nullable<Bool>ToGuid", true, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<bool?, int>("Nullable<Bool>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<bool?, int>("Nullable<Bool>ToInt", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, long>("Nullable<Bool>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<bool?, long>("Nullable<Bool>ToLong", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, sbyte>("Nullable<Bool>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<bool?, sbyte>("Nullable<Bool>ToSByte", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, short>("Nullable<Bool>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<bool?, short>("Nullable<Bool>ToShort", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, string>("Nullable<Bool>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<bool?, string>("Nullable<Bool>ToString", true, ConvertResult.Success, "True"),
                            new ConvertGenericTest<bool?, TimeSpan>("Nullable<Bool>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<bool?, TimeSpan>("Nullable<Bool>ToTimeSpan", true, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<bool?, Type>("Nullable<Bool>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<bool?, Type>("Nullable<Bool>ToType", true, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<bool?, uint>("Nullable<Bool>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<bool?, uint>("Nullable<Bool>ToUInt", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, ulong>("Nullable<Bool>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<bool?, ulong>("Nullable<Bool>ToULong", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, Uri>("Nullable<Bool>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<bool?, Uri>("Nullable<Bool>ToUri", true, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<bool?, ushort>("Nullable<Bool>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<bool?, ushort>("Nullable<Bool>ToUShort", true, ConvertResult.Success, 1),

                            // Nullable Types
                            new ConvertGenericTest<bool?, bool?>("Nullable<Bool>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<bool?, bool?>("Nullable<Bool>ToNullable<Bool>", true, ConvertResult.Success, true),
                            new ConvertGenericTest<bool?, byte?>("Nullable<Bool>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<bool?, byte?>("Nullable<Bool>ToNullable<Byte>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, char?>("Nullable<Bool>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<bool?, char?>("Nullable<Bool>ToNullable<Char>", true, ConvertResult.Success, (char)1),
                            new ConvertGenericTest<bool?, DateTime?>("Nullable<Bool>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<bool?, DateTime?>("Nullable<Bool>ToNullable<DateTime>", true, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<bool?, DateTimeOffset?>("Nullable<Bool>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<bool?, DateTimeOffset?>("Nullable<Bool>ToNullable<DateTimeOffset>", true, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<bool?, decimal?>("Nullable<Bool>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<bool?, decimal?>("Nullable<Bool>ToNullable<Decimal>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, double?>("Nullable<Bool>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<bool?, double?>("Nullable<Bool>ToNullable<Double>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, PrimaryColor?>("Nullable<Bool>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<bool?, PrimaryColor?>("Nullable<Bool>ToNullable<Enum>", true, ConvertResult.Success, PrimaryColor.Green),
                            new ConvertGenericTest<bool?, float?>("Nullable<Bool>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<bool?, float?>("Nullable<Bool>ToNullable<Float>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, Guid?>("Nullable<Bool>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<bool?, Guid?>("Nullable<Bool>ToNullable<Guid>", true, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<bool?, int?>("Nullable<Bool>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<bool?, int?>("Nullable<Bool>ToNullable<Int>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, long?>("Nullable<Bool>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<bool?, long?>("Nullable<Bool>ToNullable<Long>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, sbyte?>("Nullable<Bool>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<bool?, sbyte?>("Nullable<Bool>ToNullable<SByte>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, short?>("Nullable<Bool>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<bool?, short?>("Nullable<Bool>ToNullable<Short>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, TimeSpan?>("Nullable<Bool>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<bool?, TimeSpan?>("Nullable<Bool>ToNullable<TimeSpan>", true, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<bool?, uint?>("Nullable<Bool>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<bool?, uint?>("Nullable<Bool>ToNullable<UInt>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, ulong?>("Nullable<Bool>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<bool?, ulong?>("Nullable<Bool>ToNullable<ULong>", true, ConvertResult.Success, 1),
                            new ConvertGenericTest<bool?, ushort?>("Nullable<Bool>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<bool?, ushort?>("Nullable<Bool>ToNullable<UShort>", true, ConvertResult.Success, 1),

                            // Interface/Class Types
                            new ConvertGenericTest<bool?, IInterface>("Nullable<Bool>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<bool?, IInterface>("Nullable<Bool>ToInterface", true, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<bool?, BaseClass>("Nullable<Bool>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<bool?, BaseClass>("Nullable<Bool>ToBaseClass", true, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<bool?, DerivedClass>("Nullable<Bool>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<bool?, DerivedClass>("Nullable<Bool>ToDerivedClass", true, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Byte>ToXXX
                new object []
                {
                    "Nullable<Byte>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<byte?, bool>("Nullable<Byte>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<byte?, bool>("Nullable<Byte>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<byte?, byte>("Nullable<Byte>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<byte?, byte>("Nullable<Byte>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, byte[]>("Nullable<Byte>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<byte?, byte[]>("Nullable<Byte>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<byte?, char>("Nullable<Byte>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<byte?, char>("Nullable<Byte>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<byte?, DateTime>("Nullable<Byte>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte?, DateTime>("Nullable<Byte>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<byte?, DateTimeOffset>("Nullable<Byte>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<byte?, DateTimeOffset>("Nullable<Byte>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<byte?, decimal>("Nullable<Byte>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<byte?, decimal>("Nullable<Byte>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, double>("Nullable<Byte>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<byte?, double>("Nullable<Byte>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, PrimaryColor>("Nullable<Byte>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<byte?, PrimaryColor>("Nullable<Byte>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<byte?, float>("Nullable<Byte>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<byte?, float>("Nullable<Byte>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, Guid>("Nullable<Byte>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<byte?, Guid>("Nullable<Byte>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<byte?, int>("Nullable<Byte>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<byte?, int>("Nullable<Byte>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, long>("Nullable<Byte>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<byte?, long>("Nullable<Byte>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, sbyte>("Nullable<Byte>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<byte?, sbyte>("Nullable<Byte>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, short>("Nullable<Byte>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<byte?, short>("Nullable<Byte>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, string>("Nullable<Byte>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<byte?, string>("Nullable<Byte>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<byte?, TimeSpan>("Nullable<Byte>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<byte?, TimeSpan>("Nullable<Byte>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<byte?, Type>("Nullable<Byte>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<byte?, Type>("Nullable<Byte>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<byte?, uint>("Nullable<Byte>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<byte?, uint>("Nullable<Byte>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, ulong>("Nullable<Byte>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<byte?, ulong>("Nullable<Byte>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, Uri>("Nullable<Byte>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<byte?, Uri>("Nullable<Byte>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<byte?, ushort>("Nullable<Byte>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<byte?, ushort>("Nullable<Byte>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<byte?, bool?>("Nullable<Byte>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<byte?, bool?>("Nullable<Byte>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<byte?, byte?>("Nullable<Byte>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<byte?, byte?>("Nullable<Byte>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, char?>("Nullable<Byte>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<byte?, char?>("Nullable<Byte>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<byte?, DateTime?>("Nullable<Byte>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<byte?, DateTime?>("Nullable<Byte>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<byte?, DateTimeOffset?>("Nullable<Byte>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<byte?, DateTimeOffset?>("Nullable<Byte>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<byte?, decimal?>("Nullable<Byte>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<byte?, decimal?>("Nullable<Byte>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, double?>("Nullable<Byte>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<byte?, double?>("Nullable<Byte>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, PrimaryColor?>("Nullable<Byte>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<byte?, PrimaryColor?>("Nullable<Byte>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<byte?, float?>("Nullable<Byte>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<byte?, float?>("Nullable<Byte>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, Guid?>("Nullable<Byte>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<byte?, Guid?>("Nullable<Byte>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<byte?, int?>("Nullable<Byte>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<byte?, int?>("Nullable<Byte>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, long?>("Nullable<Byte>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<byte?, long?>("Nullable<Byte>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, sbyte?>("Nullable<Byte>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<byte?, sbyte?>("Nullable<Byte>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, short?>("Nullable<Byte>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<byte?, short?>("Nullable<Byte>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, TimeSpan?>("Nullable<Byte>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<byte?, TimeSpan?>("Nullable<Byte>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<byte?, uint?>("Nullable<Byte>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<byte?, uint?>("Nullable<Byte>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, ulong?>("Nullable<Byte>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<byte?, ulong?>("Nullable<Byte>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<byte?, ushort?>("Nullable<Byte>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<byte?, ushort?>("Nullable<Byte>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<byte?, IInterface>("Nullable<Byte>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<byte?, IInterface>("Nullable<Byte>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<byte?, BaseClass>("Nullable<Byte>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<byte?, BaseClass>("Nullable<Byte>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<byte?, DerivedClass>("Nullable<Byte>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<byte?, DerivedClass>("Nullable<Byte>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Char>ToXXX
                new object []
                {
                    "Nullable<Char>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<char?, bool>("Nullable<Char>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<char?, bool>("Nullable<Char>ToBool", '*', ConvertResult.Success, true),
                            new ConvertGenericTest<char?, byte>("Nullable<Char>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<char?, byte>("Nullable<Char>ToByte", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, byte[]>("Nullable<Char>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<char?, byte[]>("Nullable<Char>ToByteArray", '*', ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<char?, char>("Nullable<Char>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<char?, char>("Nullable<Char>ToChar", '*', ConvertResult.Success, '*'),
                            new ConvertGenericTest<char?, DateTime>("Nullable<Char>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<char?, DateTime>("Nullable<Char>ToDateTime", '*', ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<char?, DateTimeOffset>("Nullable<Char>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<char?, DateTimeOffset>("Nullable<Char>ToDateTimeOffset", '*', ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<char?, decimal>("Nullable<Char>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<char?, decimal>("Nullable<Char>ToDecimal", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, double>("Nullable<Char>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<char?, double>("Nullable<Char>ToDouble", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, PrimaryColor>("Nullable<Char>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<char?, PrimaryColor>("Nullable<Char>ToEnum", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<char?, float>("Nullable<Char>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<char?, float>("Nullable<Char>ToFloat", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, Guid>("Nullable<Char>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<char?, Guid>("Nullable<Char>ToGuid", '*', ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<char?, int>("Nullable<Char>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<char?, int>("Nullable<Char>ToInt", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, long>("Nullable<Char>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<char?, long>("Nullable<Char>ToLong", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, sbyte>("Nullable<Char>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<char?, sbyte>("Nullable<Char>ToSByte", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, short>("Nullable<Char>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<char?, short>("Nullable<Char>ToShort", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, string>("Nullable<Char>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<char?, string>("Nullable<Char>ToString", '*', ConvertResult.Success, "*"),
                            new ConvertGenericTest<char?, TimeSpan>("Nullable<Char>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<char?, TimeSpan>("Nullable<Char>ToTimeSpan", '*', ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<char?, Type>("Nullable<Char>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<char?, Type>("Nullable<Char>ToType", '*', ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<char?, uint>("Nullable<Char>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<char?, uint>("Nullable<Char>ToUInt", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, ulong>("Nullable<Char>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<char?, ulong>("Nullable<Char>ToULong", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, Uri>("Nullable<Char>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<char?, Uri>("Nullable<Char>ToUri", '*', ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<char?, ushort>("Nullable<Char>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<char?, ushort>("Nullable<Char>ToUShort", '*', ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<char?, bool?>("Nullable<Char>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<char?, bool?>("Nullable<Char>ToNullable<Bool>", '*', ConvertResult.Success, true),
                            new ConvertGenericTest<char?, byte?>("Nullable<Char>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<char?, byte?>("Nullable<Char>ToNullable<Byte>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, char?>("Nullable<Char>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<char?, char?>("Nullable<Char>ToNullable<Char>", '*', ConvertResult.Success, '*'),
                            new ConvertGenericTest<char?, DateTime?>("Nullable<Char>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<char?, DateTime?>("Nullable<Char>ToNullable<DateTime>", '*', ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<char?, DateTimeOffset?>("Nullable<Char>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<char?, DateTimeOffset?>("Nullable<Char>ToNullable<DateTimeOffset>", '*', ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<char?, decimal?>("Nullable<Char>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<char?, decimal?>("Nullable<Char>ToNullable<Decimal>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, double?>("Nullable<Char>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<char?, double?>("Nullable<Char>ToNullable<Double>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, PrimaryColor?>("Nullable<Char>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<char?, PrimaryColor?>("Nullable<Char>ToNullable<Enum>", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<char?, float?>("Nullable<Char>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<char?, float?>("Nullable<Char>ToNullable<Float>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, Guid?>("Nullable<Char>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<char?, Guid?>("Nullable<Char>ToNullable<Guid>", '*', ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<char?, int?>("Nullable<Char>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<char?, int?>("Nullable<Char>ToNullable<Int>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, long?>("Nullable<Char>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<char?, long?>("Nullable<Char>ToNullable<Long>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, sbyte?>("Nullable<Char>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<char?, sbyte?>("Nullable<Char>ToNullable<SByte>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, short?>("Nullable<Char>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<char?, short?>("Nullable<Char>ToNullable<Short>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, TimeSpan?>("Nullable<Char>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<char?, TimeSpan?>("Nullable<Char>ToNullable<TimeSpan>", '*', ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<char?, uint?>("Nullable<Char>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<char?, uint?>("Nullable<Char>ToNullable<UInt>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, ulong?>("Nullable<Char>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<char?, ulong?>("Nullable<Char>ToNullable<ULong>", '*', ConvertResult.Success, 42),
                            new ConvertGenericTest<char?, ushort?>("Nullable<Char>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<char?, ushort?>("Nullable<Char>ToNullable<UShort>", '*', ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<char?, IInterface>("Nullable<Char>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<char?, IInterface>("Nullable<Char>ToInterface", '*', ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<char?, BaseClass>("Nullable<Char>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<char?, BaseClass>("Nullable<Char>ToBaseClass", '*', ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<char?, DerivedClass>("Nullable<Char>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<char?, DerivedClass>("Nullable<Char>ToDerivedClass", '*', ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<DateTime>ToXXX
                new object []
                {
                    "Nullable<DateTime>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<DateTime?, bool>("Nullable<DateTime>ToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTime?, bool>("Nullable<DateTime>ToBool", TestDateTime, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTime?, byte>("Nullable<DateTime>ToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTime?, byte>("Nullable<DateTime>ToByte", TestDateTime, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTime?, byte[]>("Nullable<DateTime>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTime?, byte[]>("Nullable<DateTime>ToByteArray", TestDateTime, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTime?, char>("Nullable<DateTime>ToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTime?, char>("Nullable<DateTime>ToChar", TestDateTime, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTime?, DateTime>("Nullable<DateTime>ToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new ConvertGenericTest<DateTime?, DateTime>("Nullable<DateTime>ToDateTime", TestDateTime, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTime?, DateTimeOffset>("Nullable<DateTime>ToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new ConvertGenericTest<DateTime?, DateTimeOffset>("Nullable<DateTime>ToDateTimeOffset", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTime?, decimal>("Nullable<DateTime>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTime?, decimal>("Nullable<DateTime>ToDecimal", TestDateTime, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTime?, double>("Nullable<DateTime>ToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTime?, double>("Nullable<DateTime>ToDouble", TestDateTime, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTime?, PrimaryColor>("Nullable<DateTime>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTime?, PrimaryColor>("Nullable<DateTime>ToEnum", TestDateTime, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTime?, float>("Nullable<DateTime>ToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTime?, float>("Nullable<DateTime>ToFloat", TestDateTime, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTime?, Guid>("Nullable<DateTime>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTime?, Guid>("Nullable<DateTime>ToGuid", TestDateTime, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTime?, int>("Nullable<DateTime>ToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTime?, int>("Nullable<DateTime>ToInt", TestDateTime, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTime?, long>("Nullable<DateTime>ToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTime?, long>("Nullable<DateTime>ToLong", TestDateTime, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTime?, sbyte>("Nullable<DateTime>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTime?, sbyte>("Nullable<DateTime>ToSByte", TestDateTime, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTime?, short>("Nullable<DateTime>ToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTime?, short>("Nullable<DateTime>ToShort", TestDateTime, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToString", TestDateTime, ConvertResult.Success, TestDateTimeString),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormat", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormat, FormatDateTimeContext),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormatAndFormatProvider", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeContext),
                            new ConvertGenericTest<DateTime?, TimeSpan>("Nullable<DateTime>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTime?, TimeSpan>("Nullable<DateTime>ToTimeSpan", TestDateTime, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTime?, Type>("Nullable<DateTime>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTime?, Type>("Nullable<DateTime>ToType", TestDateTime, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTime?, uint>("Nullable<DateTime>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTime?, uint>("Nullable<DateTime>ToUInt", TestDateTime, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTime?, ulong>("Nullable<DateTime>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTime?, ulong>("Nullable<DateTime>ToULong", TestDateTime, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTime?, Uri>("Nullable<DateTime>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTime?, Uri>("Nullable<DateTime>ToUri", TestDateTime, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTime?, ushort>("Nullable<DateTime>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<DateTime?, ushort>("Nullable<DateTime>ToUShort", TestDateTime, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DateTime?, bool?>("Nullable<DateTime>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTime?, bool?>("Nullable<DateTime>ToNullable<Bool>", TestDateTime, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTime?, byte?>("Nullable<DateTime>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTime?, byte?>("Nullable<DateTime>ToNullable<Byte>", TestDateTime, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTime?, char?>("Nullable<DateTime>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTime?, char?>("Nullable<DateTime>ToNullable<Char>", TestDateTime, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTime?, DateTime?>("Nullable<DateTime>ToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new ConvertGenericTest<DateTime?, DateTime?>("Nullable<DateTime>ToNullable<DateTime>", TestDateTime, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTime?, DateTimeOffset?>("Nullable<DateTime>ToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new ConvertGenericTest<DateTime?, DateTimeOffset?>("Nullable<DateTime>ToNullable<DateTimeOffset>", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTime?, decimal?>("Nullable<DateTime>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTime?, decimal?>("Nullable<DateTime>ToNullable<Decimal>", TestDateTime, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTime?, double?>("Nullable<DateTime>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTime?, double?>("Nullable<DateTime>ToNullable<Double>", TestDateTime, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTime?, PrimaryColor?>("Nullable<DateTime>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTime?, PrimaryColor?>("Nullable<DateTime>ToNullable<Enum>", TestDateTime, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTime?, float?>("Nullable<DateTime>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTime?, float?>("Nullable<DateTime>ToNullable<Float>", TestDateTime, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTime?, Guid?>("Nullable<DateTime>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<DateTime?, Guid?>("Nullable<DateTime>ToNullable<Guid>", TestDateTime, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<DateTime?, int?>("Nullable<DateTime>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTime?, int?>("Nullable<DateTime>ToNullable<Int>", TestDateTime, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTime?, long?>("Nullable<DateTime>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTime?, long?>("Nullable<DateTime>ToNullable<Long>", TestDateTime, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTime?, sbyte?>("Nullable<DateTime>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTime?, sbyte?>("Nullable<DateTime>ToNullable<SByte>", TestDateTime, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTime?, short?>("Nullable<DateTime>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTime?, short?>("Nullable<DateTime>ToNullable<Short>", TestDateTime, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTime?, TimeSpan?>("Nullable<DateTime>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<DateTime?, TimeSpan?>("Nullable<DateTime>ToNullable<TimeSpan>", TestDateTime, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<DateTime?, uint?>("Nullable<DateTime>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTime?, uint?>("Nullable<DateTime>ToNullable<UInt>", TestDateTime, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTime?, ulong?>("Nullable<DateTime>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTime?, ulong?>("Nullable<DateTime>ToNullable<ULong>", TestDateTime, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTime?, ushort?>("Nullable<DateTime>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<DateTime?, ushort?>("Nullable<DateTime>ToNullable<UShort>", TestDateTime, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DateTime?, IInterface>("Nullable<DateTime>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTime?, IInterface>("Nullable<DateTime>ToInterface", TestDateTime, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTime?, BaseClass>("Nullable<DateTime>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTime?, BaseClass>("Nullable<DateTime>ToBaseClass", TestDateTime, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTime?, DerivedClass>("Nullable<DateTime>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<DateTime?, DerivedClass>("Nullable<DateTime>ToDerivedClass", TestDateTime, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<DateTimeOffset>ToXXX
                new object []
                {
                    "Nullable<DateTimeOffset>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<DateTimeOffset?, bool>("Nullable<DateTimeOffset>ToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTimeOffset?, bool>("Nullable<DateTimeOffset>ToBool", TestDateTimeOffset, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DateTimeOffset?, byte>("Nullable<DateTimeOffset>ToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTimeOffset?, byte>("Nullable<DateTimeOffset>ToByte", TestDateTimeOffset, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DateTimeOffset?, byte[]>("Nullable<DateTimeOffset>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTimeOffset?, byte[]>("Nullable<DateTimeOffset>ToByteArray", TestDateTimeOffset, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DateTimeOffset?, char>("Nullable<DateTimeOffset>ToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTimeOffset?, char>("Nullable<DateTimeOffset>ToChar", TestDateTimeOffset, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DateTimeOffset?, DateTime>("Nullable<DateTimeOffset>ToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new ConvertGenericTest<DateTimeOffset?, DateTime>("Nullable<DateTimeOffset>ToDateTime", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTimeOffset?, DateTimeOffset>("Nullable<DateTimeOffset>ToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new ConvertGenericTest<DateTimeOffset?, DateTimeOffset>("Nullable<DateTimeOffset>ToDateTimeOffset", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTimeOffset?, decimal>("Nullable<DateTimeOffset>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTimeOffset?, decimal>("Nullable<DateTimeOffset>ToDecimal", TestDateTimeOffset, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DateTimeOffset?, double>("Nullable<DateTimeOffset>ToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTimeOffset?, double>("Nullable<DateTimeOffset>ToDouble", TestDateTimeOffset, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DateTimeOffset?, PrimaryColor>("Nullable<DateTimeOffset>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTimeOffset?, PrimaryColor>("Nullable<DateTimeOffset>ToEnum", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DateTimeOffset?, float>("Nullable<DateTimeOffset>ToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTimeOffset?, float>("Nullable<DateTimeOffset>ToFloat", TestDateTimeOffset, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DateTimeOffset?, Guid>("Nullable<DateTimeOffset>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTimeOffset?, Guid>("Nullable<DateTimeOffset>ToGuid", TestDateTimeOffset, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DateTimeOffset?, int>("Nullable<DateTimeOffset>ToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTimeOffset?, int>("Nullable<DateTimeOffset>ToInt", TestDateTimeOffset, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DateTimeOffset?, long>("Nullable<DateTimeOffset>ToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTimeOffset?, long>("Nullable<DateTimeOffset>ToLong", TestDateTimeOffset, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DateTimeOffset?, sbyte>("Nullable<DateTimeOffset>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTimeOffset?, sbyte>("Nullable<DateTimeOffset>ToSByte", TestDateTimeOffset, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DateTimeOffset?, short>("Nullable<DateTimeOffset>ToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTimeOffset?, short>("Nullable<DateTimeOffset>ToShort", TestDateTimeOffset, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToString", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetString),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormat", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormat, FormatDateTimeOffsetContext),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormatAndFormatProvider", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeOffsetContext),
                            new ConvertGenericTest<DateTimeOffset?, TimeSpan>("Nullable<DateTimeOffset>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTimeOffset?, TimeSpan>("Nullable<DateTimeOffset>ToTimeSpan", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DateTimeOffset?, Type>("Nullable<DateTimeOffset>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTimeOffset?, Type>("Nullable<DateTimeOffset>ToType", TestDateTimeOffset, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DateTimeOffset?, uint>("Nullable<DateTimeOffset>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTimeOffset?, uint>("Nullable<DateTimeOffset>ToUInt", TestDateTimeOffset, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DateTimeOffset?, ulong>("Nullable<DateTimeOffset>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTimeOffset?, ulong>("Nullable<DateTimeOffset>ToULong", TestDateTimeOffset, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DateTimeOffset?, Uri>("Nullable<DateTimeOffset>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTimeOffset?, Uri>("Nullable<DateTimeOffset>ToUri", TestDateTimeOffset, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DateTimeOffset?, ushort>("Nullable<DateTimeOffset>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<DateTimeOffset?, ushort>("Nullable<DateTimeOffset>ToUShort", TestDateTimeOffset, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DateTimeOffset?, bool?>("Nullable<DateTimeOffset>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTimeOffset?, bool?>("Nullable<DateTimeOffset>ToNullable<Bool>", TestDateTimeOffset, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DateTimeOffset?, byte?>("Nullable<DateTimeOffset>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTimeOffset?, byte?>("Nullable<DateTimeOffset>ToNullable<Byte>", TestDateTimeOffset, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DateTimeOffset?, char?>("Nullable<DateTimeOffset>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTimeOffset?, char?>("Nullable<DateTimeOffset>ToNullable<Char>", TestDateTimeOffset, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DateTimeOffset?, DateTime?>("Nullable<DateTimeOffset>ToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new ConvertGenericTest<DateTimeOffset?, DateTime?>("Nullable<DateTimeOffset>ToNullable<DateTime>", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new ConvertGenericTest<DateTimeOffset?, DateTimeOffset?>("Nullable<DateTimeOffset>ToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new ConvertGenericTest<DateTimeOffset?, DateTimeOffset?>("Nullable<DateTimeOffset>ToNullable<DateTimeOffset>", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new ConvertGenericTest<DateTimeOffset?, decimal?>("Nullable<DateTimeOffset>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTimeOffset?, decimal?>("Nullable<DateTimeOffset>ToNullable<Decimal>", TestDateTimeOffset, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DateTimeOffset?, double?>("Nullable<DateTimeOffset>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTimeOffset?, double?>("Nullable<DateTimeOffset>ToNullable<Double>", TestDateTimeOffset, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DateTimeOffset?, PrimaryColor?>("Nullable<DateTimeOffset>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTimeOffset?, PrimaryColor?>("Nullable<DateTimeOffset>ToNullable<Enum>", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DateTimeOffset?, float?>("Nullable<DateTimeOffset>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTimeOffset?, float?>("Nullable<DateTimeOffset>ToNullable<Float>", TestDateTimeOffset, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DateTimeOffset?, Guid?>("Nullable<DateTimeOffset>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<DateTimeOffset?, Guid?>("Nullable<DateTimeOffset>ToNullable<Guid>", TestDateTimeOffset, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<DateTimeOffset?, int?>("Nullable<DateTimeOffset>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTimeOffset?, int?>("Nullable<DateTimeOffset>ToNullable<Int>", TestDateTimeOffset, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DateTimeOffset?, long?>("Nullable<DateTimeOffset>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTimeOffset?, long?>("Nullable<DateTimeOffset>ToNullable<Long>", TestDateTimeOffset, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DateTimeOffset?, sbyte?>("Nullable<DateTimeOffset>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTimeOffset?, sbyte?>("Nullable<DateTimeOffset>ToNullable<SByte>", TestDateTimeOffset, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DateTimeOffset?, short?>("Nullable<DateTimeOffset>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTimeOffset?, short?>("Nullable<DateTimeOffset>ToNullable<Short>", TestDateTimeOffset, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DateTimeOffset?, TimeSpan?>("Nullable<DateTimeOffset>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<DateTimeOffset?, TimeSpan?>("Nullable<DateTimeOffset>ToNullable<TimeSpan>", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<DateTimeOffset?, uint?>("Nullable<DateTimeOffset>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTimeOffset?, uint?>("Nullable<DateTimeOffset>ToNullable<UInt>", TestDateTimeOffset, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DateTimeOffset?, ulong?>("Nullable<DateTimeOffset>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTimeOffset?, ulong?>("Nullable<DateTimeOffset>ToNullable<ULong>", TestDateTimeOffset, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DateTimeOffset?, ushort?>("Nullable<DateTimeOffset>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<DateTimeOffset?, ushort?>("Nullable<DateTimeOffset>ToNullable<UShort>", TestDateTimeOffset, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DateTimeOffset?, IInterface>("Nullable<DateTimeOffset>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTimeOffset?, IInterface>("Nullable<DateTimeOffset>ToInterface", TestDateTimeOffset, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<DateTimeOffset?, BaseClass>("Nullable<DateTimeOffset>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTimeOffset?, BaseClass>("Nullable<DateTimeOffset>ToBaseClass", TestDateTimeOffset, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<DateTimeOffset?, DerivedClass>("Nullable<DateTimeOffset>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<DateTimeOffset?, DerivedClass>("Nullable<DateTimeOffset>ToDerivedClass", TestDateTimeOffset, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Decimal>ToXXX
                new object []
                {
                    "Nullable<Decimal>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<decimal?, bool>("Nullable<Decimal>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<decimal?, bool>("Nullable<Decimal>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<decimal?, byte>("Nullable<Decimal>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<decimal?, byte>("Nullable<Decimal>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, byte[]>("Nullable<Decimal>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<decimal?, byte[]>("Nullable<Decimal>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<decimal?, char>("Nullable<Decimal>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<decimal?, char>("Nullable<Decimal>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<decimal?, DateTime>("Nullable<Decimal>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<decimal?, DateTime>("Nullable<Decimal>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<decimal?, DateTimeOffset>("Nullable<Decimal>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<decimal?, DateTimeOffset>("Nullable<Decimal>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<decimal?, decimal>("Nullable<Decimal>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<decimal?, decimal>("Nullable<Decimal>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, double>("Nullable<Decimal>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<decimal?, double>("Nullable<Decimal>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, PrimaryColor>("Nullable<Decimal>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<decimal?, PrimaryColor>("Nullable<Decimal>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<decimal?, float>("Nullable<Decimal>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<decimal?, float>("Nullable<Decimal>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, Guid>("Nullable<Decimal>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<decimal?, Guid>("Nullable<Decimal>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<decimal?, int>("Nullable<Decimal>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<decimal?, int>("Nullable<Decimal>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, long>("Nullable<Decimal>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<decimal?, long>("Nullable<Decimal>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, sbyte>("Nullable<Decimal>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<decimal?, sbyte>("Nullable<Decimal>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, short>("Nullable<Decimal>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<decimal?, short>("Nullable<Decimal>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, string>("Nullable<Decimal>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<decimal?, string>("Nullable<Decimal>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<decimal?, TimeSpan>("Nullable<Decimal>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<decimal?, TimeSpan>("Nullable<Decimal>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<decimal?, Type>("Nullable<Decimal>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<decimal?, Type>("Nullable<Decimal>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<decimal?, uint>("Nullable<Decimal>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<decimal?, uint>("Nullable<Decimal>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, ulong>("Nullable<Decimal>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<decimal?, ulong>("Nullable<Decimal>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, Uri>("Nullable<Decimal>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<decimal?, Uri>("Nullable<Decimal>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<decimal?, ushort>("Nullable<Decimal>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<decimal?, ushort>("Nullable<Decimal>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<decimal?, bool?>("Nullable<Decimal>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<decimal?, bool?>("Nullable<Decimal>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<decimal?, byte?>("Nullable<Decimal>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<decimal?, byte?>("Nullable<Decimal>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, char?>("Nullable<Decimal>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<decimal?, char?>("Nullable<Decimal>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<decimal?, DateTime?>("Nullable<Decimal>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<decimal?, DateTime?>("Nullable<Decimal>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<decimal?, DateTimeOffset?>("Nullable<Decimal>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<decimal?, DateTimeOffset?>("Nullable<Decimal>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<decimal?, decimal?>("Nullable<Decimal>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<decimal?, decimal?>("Nullable<Decimal>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, double?>("Nullable<Decimal>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<decimal?, double?>("Nullable<Decimal>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, PrimaryColor?>("Nullable<Decimal>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<decimal?, PrimaryColor?>("Nullable<Decimal>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<decimal?, float?>("Nullable<Decimal>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<decimal?, float?>("Nullable<Decimal>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, Guid?>("Nullable<Decimal>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<decimal?, Guid?>("Nullable<Decimal>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<decimal?, int?>("Nullable<Decimal>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<decimal?, int?>("Nullable<Decimal>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, long?>("Nullable<Decimal>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<decimal?, long?>("Nullable<Decimal>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, sbyte?>("Nullable<Decimal>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<decimal?, sbyte?>("Nullable<Decimal>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, short?>("Nullable<Decimal>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<decimal?, short?>("Nullable<Decimal>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, TimeSpan?>("Nullable<Decimal>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<decimal?, TimeSpan?>("Nullable<Decimal>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<decimal?, uint?>("Nullable<Decimal>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<decimal?, uint?>("Nullable<Decimal>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, ulong?>("Nullable<Decimal>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<decimal?, ulong?>("Nullable<Decimal>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<decimal?, ushort?>("Nullable<Decimal>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<decimal?, ushort?>("Nullable<Decimal>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<decimal?, IInterface>("Nullable<Decimal>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<decimal?, IInterface>("Nullable<Decimal>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<decimal?, BaseClass>("Nullable<Decimal>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<decimal?, BaseClass>("Nullable<Decimal>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<decimal?, DerivedClass>("Nullable<Decimal>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<decimal?, DerivedClass>("Nullable<Decimal>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Double>ToXXX
                new object []
                {
                    "Nullable<Double>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<double?, bool>("Nullable<Double>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<double?, bool>("Nullable<Double>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<double?, byte>("Nullable<Double>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<double?, byte>("Nullable<Double>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, byte[]>("Nullable<Double>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<double?, byte[]>("Nullable<Double>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<double?, char>("Nullable<Double>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<double?, char>("Nullable<Double>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<double?, DateTime>("Nullable<Double>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<double?, DateTime>("Nullable<Double>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<double?, DateTimeOffset>("Nullable<Double>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<double?, DateTimeOffset>("Nullable<Double>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<double?, decimal>("Nullable<Double>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<double?, decimal>("Nullable<Double>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, double>("Nullable<Double>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<double?, double>("Nullable<Double>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, PrimaryColor>("Nullable<Double>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<double?, PrimaryColor>("Nullable<Double>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<double?, float>("Nullable<Double>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<double?, float>("Nullable<Double>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, Guid>("Nullable<Double>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<double?, Guid>("Nullable<Double>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<double?, int>("Nullable<Double>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<double?, int>("Nullable<Double>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, long>("Nullable<Double>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<double?, long>("Nullable<Double>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, sbyte>("Nullable<Double>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<double?, sbyte>("Nullable<Double>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, short>("Nullable<Double>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<double?, short>("Nullable<Double>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, string>("Nullable<Double>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<double?, string>("Nullable<Double>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<double?, TimeSpan>("Nullable<Double>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<double?, TimeSpan>("Nullable<Double>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<double?, Type>("Nullable<Double>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<double?, Type>("Nullable<Double>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<double?, uint>("Nullable<Double>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<double?, uint>("Nullable<Double>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, ulong>("Nullable<Double>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<double?, ulong>("Nullable<Double>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, Uri>("Nullable<Double>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<double?, Uri>("Nullable<Double>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<double?, ushort>("Nullable<Double>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<double?, ushort>("Nullable<Double>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<double?, bool?>("Nullable<Double>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<double?, bool?>("Nullable<Double>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<double?, byte?>("Nullable<Double>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<double?, byte?>("Nullable<Double>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, char?>("Nullable<Double>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<double?, char?>("Nullable<Double>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<double?, DateTime?>("Nullable<Double>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<double?, DateTime?>("Nullable<Double>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<double?, DateTimeOffset?>("Nullable<Double>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<double?, DateTimeOffset?>("Nullable<Double>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<double?, decimal?>("Nullable<Double>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<double?, decimal?>("Nullable<Double>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, double?>("Nullable<Double>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<double?, double?>("Nullable<Double>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, PrimaryColor?>("Nullable<Double>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<double?, PrimaryColor?>("Nullable<Double>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<double?, float?>("Nullable<Double>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<double?, float?>("Nullable<Double>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, Guid?>("Nullable<Double>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<double?, Guid?>("Nullable<Double>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<double?, int?>("Nullable<Double>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<double?, int?>("Nullable<Double>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, long?>("Nullable<Double>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<double?, long?>("Nullable<Double>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, sbyte?>("Nullable<Double>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<double?, sbyte?>("Nullable<Double>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, short?>("Nullable<Double>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<double?, short?>("Nullable<Double>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, TimeSpan?>("Nullable<Double>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<double?, TimeSpan?>("Nullable<Double>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<double?, uint?>("Nullable<Double>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<double?, uint?>("Nullable<Double>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, ulong?>("Nullable<Double>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<double?, ulong?>("Nullable<Double>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<double?, ushort?>("Nullable<Double>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<double?, ushort?>("Nullable<Double>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<double?, IInterface>("Nullable<Double>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<double?, IInterface>("Nullable<Double>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<double?, BaseClass>("Nullable<Double>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<double?, BaseClass>("Nullable<Double>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<double?, DerivedClass>("Nullable<Double>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<double?, DerivedClass>("Nullable<Double>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Enum>ToXXX
                new object []
                {
                    "Nullable<Enum>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<PrimaryColor?, bool>("Nullable<Enum>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<PrimaryColor?, bool>("Nullable<Enum>ToBool", PrimaryColor.Blue, ConvertResult.Success, true),
                            new ConvertGenericTest<PrimaryColor?, byte>("Nullable<Enum>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<PrimaryColor?, byte>("Nullable<Enum>ToByte", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, byte[]>("Nullable<Enum>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<PrimaryColor?, byte[]>("Nullable<Enum>ToByteArray", PrimaryColor.Blue, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<PrimaryColor?, char>("Nullable<Enum>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<PrimaryColor?, char>("Nullable<Enum>ToChar", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new ConvertGenericTest<PrimaryColor?, DateTime>("Nullable<Enum>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<PrimaryColor?, DateTime>("Nullable<Enum>ToDateTime", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<PrimaryColor?, DateTimeOffset>("Nullable<Enum>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<PrimaryColor?, DateTimeOffset>("Nullable<Enum>ToDateTimeOffset", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<PrimaryColor?, decimal>("Nullable<Enum>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<PrimaryColor?, decimal>("Nullable<Enum>ToDecimal", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, double>("Nullable<Enum>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<PrimaryColor?, double>("Nullable<Enum>ToDouble", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, PrimaryColor>("Nullable<Enum>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<PrimaryColor?, PrimaryColor>("Nullable<Enum>ToEnum", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<PrimaryColor?, float>("Nullable<Enum>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<PrimaryColor?, float>("Nullable<Enum>ToFloat", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, Guid>("Nullable<Enum>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<PrimaryColor?, Guid>("Nullable<Enum>ToGuid", PrimaryColor.Blue, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<PrimaryColor?, int>("Nullable<Enum>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<PrimaryColor?, int>("Nullable<Enum>ToInt", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, long>("Nullable<Enum>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<PrimaryColor?, long>("Nullable<Enum>ToLong", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, sbyte>("Nullable<Enum>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<PrimaryColor?, sbyte>("Nullable<Enum>ToSByte", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, short>("Nullable<Enum>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<PrimaryColor?, short>("Nullable<Enum>ToShort", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToString", PrimaryColor.Blue, ConvertResult.Success, TestBlueString),
                            new ConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToStringWithFormat", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinalAsString, FormatEnumContext),
                            new ConvertGenericTest<PrimaryColor?, TimeSpan>("Nullable<Enum>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<PrimaryColor?, TimeSpan>("Nullable<Enum>ToTimeSpan", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<PrimaryColor?, Type>("Nullable<Enum>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<PrimaryColor?, Type>("Nullable<Enum>ToType", PrimaryColor.Blue, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<PrimaryColor?, uint>("Nullable<Enum>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<PrimaryColor?, uint>("Nullable<Enum>ToUInt", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, ulong>("Nullable<Enum>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<PrimaryColor?, ulong>("Nullable<Enum>ToULong", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, Uri>("Nullable<Enum>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<PrimaryColor?, Uri>("Nullable<Enum>ToUri", PrimaryColor.Blue, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<PrimaryColor?, ushort>("Nullable<Enum>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<PrimaryColor?, ushort>("Nullable<Enum>ToUShort", PrimaryColor.Blue, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<PrimaryColor?, bool?>("Nullable<Enum>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<PrimaryColor?, bool?>("Nullable<Enum>ToNullable<Bool>", PrimaryColor.Blue, ConvertResult.Success, true),
                            new ConvertGenericTest<PrimaryColor?, byte?>("Nullable<Enum>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<PrimaryColor?, byte?>("Nullable<Enum>ToNullable<Byte>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, char?>("Nullable<Enum>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<PrimaryColor?, char?>("Nullable<Enum>ToNullable<Char>", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new ConvertGenericTest<PrimaryColor?, DateTime?>("Nullable<Enum>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<PrimaryColor?, DateTime?>("Nullable<Enum>ToNullable<DateTime>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<PrimaryColor?, DateTimeOffset?>("Nullable<Enum>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<PrimaryColor?, DateTimeOffset?>("Nullable<Enum>ToNullable<DateTimeOffset>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<PrimaryColor?, decimal?>("Nullable<Enum>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<PrimaryColor?, decimal?>("Nullable<Enum>ToNullable<Decimal>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, double?>("Nullable<Enum>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<PrimaryColor?, double?>("Nullable<Enum>ToNullable<Double>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, PrimaryColor?>("Nullable<Enum>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<PrimaryColor?, PrimaryColor?>("Nullable<Enum>ToNullable<Enum>", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<PrimaryColor?, float?>("Nullable<Enum>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<PrimaryColor?, float?>("Nullable<Enum>ToNullable<Float>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, Guid?>("Nullable<Enum>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<PrimaryColor?, Guid?>("Nullable<Enum>ToNullable<Guid>", PrimaryColor.Blue, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<PrimaryColor?, int?>("Nullable<Enum>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<PrimaryColor?, int?>("Nullable<Enum>ToNullable<Int>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, long?>("Nullable<Enum>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<PrimaryColor?, long?>("Nullable<Enum>ToNullable<Long>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, sbyte?>("Nullable<Enum>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<PrimaryColor?, sbyte?>("Nullable<Enum>ToNullable<SByte>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, short?>("Nullable<Enum>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<PrimaryColor?, short?>("Nullable<Enum>ToNullable<Short>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, TimeSpan?>("Nullable<Enum>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<PrimaryColor?, TimeSpan?>("Nullable<Enum>ToNullable<TimeSpan>", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<PrimaryColor?, uint?>("Nullable<Enum>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<PrimaryColor?, uint?>("Nullable<Enum>ToNullable<UInt>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, ulong?>("Nullable<Enum>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<PrimaryColor?, ulong?>("Nullable<Enum>ToNullable<ULong>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new ConvertGenericTest<PrimaryColor?, ushort?>("Nullable<Enum>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<PrimaryColor?, ushort?>("Nullable<Enum>ToNullable<UShort>", PrimaryColor.Blue, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<PrimaryColor?, IInterface>("Nullable<Enum>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<PrimaryColor?, IInterface>("Nullable<Enum>ToInterface", PrimaryColor.Blue, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<PrimaryColor?, BaseClass>("Nullable<Enum>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<PrimaryColor?, BaseClass>("Nullable<Enum>ToBaseClass", PrimaryColor.Blue, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<PrimaryColor?, DerivedClass>("Nullable<Enum>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<PrimaryColor?, DerivedClass>("Nullable<Enum>ToDerivedClass", PrimaryColor.Blue, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Float>ToXXX
                new object []
                {
                    "Nullable<Float>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<float?, bool>("Nullable<Float>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<float?, bool>("Nullable<Float>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<float?, byte>("Nullable<Float>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<float?, byte>("Nullable<Float>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, byte[]>("Nullable<Float>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<float?, byte[]>("Nullable<Float>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<float?, char>("Nullable<Float>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<float?, char>("Nullable<Float>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<float?, DateTime>("Nullable<Float>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<float?, DateTime>("Nullable<Float>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<float?, DateTimeOffset>("Nullable<Float>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<float?, DateTimeOffset>("Nullable<Float>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<float?, decimal>("Nullable<Float>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<float?, decimal>("Nullable<Float>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, double>("Nullable<Float>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<float?, double>("Nullable<Float>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, PrimaryColor>("Nullable<Float>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<float?, PrimaryColor>("Nullable<Float>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<float?, float>("Nullable<Float>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<float?, float>("Nullable<Float>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, Guid>("Nullable<Float>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<float?, Guid>("Nullable<Float>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<float?, int>("Nullable<Float>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<float?, int>("Nullable<Float>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, long>("Nullable<Float>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<float?, long>("Nullable<Float>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, sbyte>("Nullable<Float>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<float?, sbyte>("Nullable<Float>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, short>("Nullable<Float>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<float?, short>("Nullable<Float>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, string>("Nullable<Float>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<float?, string>("Nullable<Float>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<float?, TimeSpan>("Nullable<Float>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<float?, TimeSpan>("Nullable<Float>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<float?, Type>("Nullable<Float>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<float?, Type>("Nullable<Float>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<float?, uint>("Nullable<Float>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<float?, uint>("Nullable<Float>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, ulong>("Nullable<Float>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<float?, ulong>("Nullable<Float>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, Uri>("Nullable<Float>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<float?, Uri>("Nullable<Float>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<float?, ushort>("Nullable<Float>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<float?, ushort>("Nullable<Float>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<float?, bool?>("Nullable<Float>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<float?, bool?>("Nullable<Float>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<float?, byte?>("Nullable<Float>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<float?, byte?>("Nullable<Float>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, char?>("Nullable<Float>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<float?, char?>("Nullable<Float>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<float?, DateTime?>("Nullable<Float>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<float?, DateTime?>("Nullable<Float>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<float?, DateTimeOffset?>("Nullable<Float>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<float?, DateTimeOffset?>("Nullable<Float>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<float?, decimal?>("Nullable<Float>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<float?, decimal?>("Nullable<Float>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, double?>("Nullable<Float>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<float?, double?>("Nullable<Float>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, PrimaryColor?>("Nullable<Float>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<float?, PrimaryColor?>("Nullable<Float>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<float?, float?>("Nullable<Float>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<float?, float?>("Nullable<Float>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, Guid?>("Nullable<Float>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<float?, Guid?>("Nullable<Float>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<float?, int?>("Nullable<Float>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<float?, int?>("Nullable<Float>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, long?>("Nullable<Float>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<float?, long?>("Nullable<Float>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, sbyte?>("Nullable<Float>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<float?, sbyte?>("Nullable<Float>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, short?>("Nullable<Float>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<float?, short?>("Nullable<Float>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, TimeSpan?>("Nullable<Float>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<float?, TimeSpan?>("Nullable<Float>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<float?, uint?>("Nullable<Float>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<float?, uint?>("Nullable<Float>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, ulong?>("Nullable<Float>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<float?, ulong?>("Nullable<Float>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<float?, ushort?>("Nullable<Float>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<float?, ushort?>("Nullable<Float>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<float?, IInterface>("Nullable<Float>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<float?, IInterface>("Nullable<Float>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<float?, BaseClass>("Nullable<Float>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<float?, BaseClass>("Nullable<Float>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<float?, DerivedClass>("Nullable<Float>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<float?, DerivedClass>("Nullable<Float>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Guid>ToXXX
                new object []
                {
                    "Nullable<Guid>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<Guid?, bool>("Nullable<Guid>ToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Guid?, bool>("Nullable<Guid>ToBool", TestGuid, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<Guid?, byte>("Nullable<Guid>ToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Guid?, byte>("Nullable<Guid>ToByte", TestGuid, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<Guid?, byte[]>("Nullable<Guid>ToByteArray", null, ConvertResult.Success, default(byte[])),
                            new ConvertGenericTest<Guid?, byte[]>("Nullable<Guid>ToByteArray", TestGuid, ConvertResult.Success, TestGuidByteArray),
                            new ConvertGenericTest<Guid?, char>("Nullable<Guid>ToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Guid?, char>("Nullable<Guid>ToChar", TestGuid, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<Guid?, DateTime>("Nullable<Guid>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<Guid?, DateTime>("Nullable<Guid>ToDateTime", TestGuid, ConvertResult.Failure, TestDateTime),
                            new ConvertGenericTest<Guid?, DateTimeOffset>("Nullable<Guid>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<Guid?, DateTimeOffset>("Nullable<Guid>ToDateTimeOffset", TestGuid, ConvertResult.Failure, TestDateTimeOffset),
                            new ConvertGenericTest<Guid?, decimal>("Nullable<Guid>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Guid?, decimal>("Nullable<Guid>ToDecimal", TestGuid, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<Guid?, double>("Nullable<Guid>ToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Guid?, double>("Nullable<Guid>ToDouble", TestGuid, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<Guid?, PrimaryColor>("Nullable<Guid>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Guid?, PrimaryColor>("Nullable<Guid>ToEnum", TestGuid, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<Guid?, float>("Nullable<Guid>ToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Guid?, float>("Nullable<Guid>ToFloat", TestGuid, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<Guid?, Guid>("Nullable<Guid>ToGuid", null, ConvertResult.Success, default(Guid)),
                            new ConvertGenericTest<Guid?, Guid>("Nullable<Guid>ToGuid", TestGuid, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<Guid?, int>("Nullable<Guid>ToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Guid?, int>("Nullable<Guid>ToInt", TestGuid, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<Guid?, long>("Nullable<Guid>ToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Guid?, long>("Nullable<Guid>ToLong", TestGuid, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<Guid?, sbyte>("Nullable<Guid>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Guid?, sbyte>("Nullable<Guid>ToSByte", TestGuid, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<Guid?, short>("Nullable<Guid>ToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Guid?, short>("Nullable<Guid>ToShort", TestGuid, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<Guid?, string>("Nullable<Guid>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<Guid?, string>("Nullable<Guid>ToString", TestGuid, ConvertResult.Success, TestGuidString),
                            new ConvertGenericTest<Guid?, TimeSpan>("Nullable<Guid>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Guid?, TimeSpan>("Nullable<Guid>ToTimeSpan", TestGuid, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<Guid?, Type>("Nullable<Guid>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Guid?, Type>("Nullable<Guid>ToType", TestGuid, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<Guid?, uint>("Nullable<Guid>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Guid?, uint>("Nullable<Guid>ToUInt", TestGuid, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<Guid?, ulong>("Nullable<Guid>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Guid?, ulong>("Nullable<Guid>ToULong", TestGuid, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<Guid?, Uri>("Nullable<Guid>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Guid?, Uri>("Nullable<Guid>ToUri", TestGuid, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<Guid?, ushort>("Nullable<Guid>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<Guid?, ushort>("Nullable<Guid>ToUShort", TestGuid, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<Guid?, bool?>("Nullable<Guid>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Guid?, bool?>("Nullable<Guid>ToNullable<Bool>", TestGuid, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<Guid?, byte?>("Nullable<Guid>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Guid?, byte?>("Nullable<Guid>ToNullable<Byte>", TestGuid, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<Guid?, char?>("Nullable<Guid>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Guid?, char?>("Nullable<Guid>ToNullable<Char>", TestGuid, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<Guid?, DateTime?>("Nullable<Guid>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<Guid?, DateTime?>("Nullable<Guid>ToNullable<DateTime>", TestGuid, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<Guid?, DateTimeOffset?>("Nullable<Guid>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<Guid?, DateTimeOffset?>("Nullable<Guid>ToNullable<DateTimeOffset>", TestGuid, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<Guid?, decimal?>("Nullable<Guid>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Guid?, decimal?>("Nullable<Guid>ToNullable<Decimal>", TestGuid, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<Guid?, double?>("Nullable<Guid>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Guid?, double?>("Nullable<Guid>ToNullable<Double>", TestGuid, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<Guid?, PrimaryColor?>("Nullable<Guid>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Guid?, PrimaryColor?>("Nullable<Guid>ToNullable<Enum>", TestGuid, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<Guid?, float?>("Nullable<Guid>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Guid?, float?>("Nullable<Guid>ToNullable<Float>", TestGuid, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<Guid?, Guid?>("Nullable<Guid>ToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new ConvertGenericTest<Guid?, Guid?>("Nullable<Guid>ToNullable<Guid>", TestGuid, ConvertResult.Success, TestGuid),
                            new ConvertGenericTest<Guid?, int?>("Nullable<Guid>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Guid?, int?>("Nullable<Guid>ToNullable<Int>", TestGuid, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<Guid?, long?>("Nullable<Guid>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Guid?, long?>("Nullable<Guid>ToNullable<Long>", TestGuid, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<Guid?, sbyte?>("Nullable<Guid>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Guid?, sbyte?>("Nullable<Guid>ToNullable<SByte>", TestGuid, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<Guid?, short?>("Nullable<Guid>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Guid?, short?>("Nullable<Guid>ToNullable<Short>", TestGuid, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<Guid?, TimeSpan?>("Nullable<Guid>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<Guid?, TimeSpan?>("Nullable<Guid>ToNullable<TimeSpan>", TestGuid, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<Guid?, uint?>("Nullable<Guid>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Guid?, uint?>("Nullable<Guid>ToNullable<UInt>", TestGuid, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<Guid?, ulong?>("Nullable<Guid>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Guid?, ulong?>("Nullable<Guid>ToNullable<ULong>", TestGuid, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<Guid?, ushort?>("Nullable<Guid>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<Guid?, ushort?>("Nullable<Guid>ToNullable<UShort>", TestGuid, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<Guid?, IInterface>("Nullable<Guid>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Guid?, IInterface>("Nullable<Guid>ToInterface", TestGuid, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<Guid?, BaseClass>("Nullable<Guid>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Guid?, BaseClass>("Nullable<Guid>ToBaseClass", TestGuid, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<Guid?, DerivedClass>("Nullable<Guid>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<Guid?, DerivedClass>("Nullable<Guid>ToDerivedClass", TestGuid, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Int>ToXXX
                new object []
                {
                    "Nullable<Int>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<int?, bool>("Nullable<Int>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<int?, bool>("Nullable<Int>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<int?, byte>("Nullable<Int>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<int?, byte>("Nullable<Int>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, byte[]>("Nullable<Int>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<int?, byte[]>("Nullable<Int>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<int?, char>("Nullable<Int>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<int?, char>("Nullable<Int>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<int?, DateTime>("Nullable<Int>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<int?, DateTime>("Nullable<Int>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<int?, DateTimeOffset>("Nullable<Int>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<int?, DateTimeOffset>("Nullable<Int>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<int?, decimal>("Nullable<Int>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<int?, decimal>("Nullable<Int>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, double>("Nullable<Int>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<int?, double>("Nullable<Int>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, PrimaryColor>("Nullable<Int>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<int?, PrimaryColor>("Nullable<Int>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<int?, float>("Nullable<Int>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<int?, float>("Nullable<Int>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, Guid>("Nullable<Int>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<int?, Guid>("Nullable<Int>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<int?, int>("Nullable<Int>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<int?, int>("Nullable<Int>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, long>("Nullable<Int>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<int?, long>("Nullable<Int>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, sbyte>("Nullable<Int>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<int?, sbyte>("Nullable<Int>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, short>("Nullable<Int>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<int?, short>("Nullable<Int>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, string>("Nullable<Int>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<int?, string>("Nullable<Int>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<int?, TimeSpan>("Nullable<Int>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<int?, TimeSpan>("Nullable<Int>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<int?, Type>("Nullable<Int>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<int?, Type>("Nullable<Int>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<int?, uint>("Nullable<Int>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<int?, uint>("Nullable<Int>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, ulong>("Nullable<Int>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<int?, ulong>("Nullable<Int>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, Uri>("Nullable<Int>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<int?, Uri>("Nullable<Int>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<int?, ushort>("Nullable<Int>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<int?, ushort>("Nullable<Int>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<int?, bool?>("Nullable<Int>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<int?, bool?>("Nullable<Int>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<int?, byte?>("Nullable<Int>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<int?, byte?>("Nullable<Int>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, char?>("Nullable<Int>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<int?, char?>("Nullable<Int>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<int?, DateTime?>("Nullable<Int>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<int?, DateTime?>("Nullable<Int>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<int?, DateTimeOffset?>("Nullable<Int>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<int?, DateTimeOffset?>("Nullable<Int>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<int?, decimal?>("Nullable<Int>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<int?, decimal?>("Nullable<Int>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, double?>("Nullable<Int>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<int?, double?>("Nullable<Int>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, PrimaryColor?>("Nullable<Int>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<int?, PrimaryColor?>("Nullable<Int>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<int?, float?>("Nullable<Int>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<int?, float?>("Nullable<Int>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, Guid?>("Nullable<Int>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<int?, Guid?>("Nullable<Int>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<int?, int?>("Nullable<Int>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<int?, int?>("Nullable<Int>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, long?>("Nullable<Int>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<int?, long?>("Nullable<Int>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, sbyte?>("Nullable<Int>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<int?, sbyte?>("Nullable<Int>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, short?>("Nullable<Int>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<int?, short?>("Nullable<Int>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, TimeSpan?>("Nullable<Int>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<int?, TimeSpan?>("Nullable<Int>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<int?, uint?>("Nullable<Int>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<int?, uint?>("Nullable<Int>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, ulong?>("Nullable<Int>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<int?, ulong?>("Nullable<Int>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<int?, ushort?>("Nullable<Int>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<int?, ushort?>("Nullable<Int>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<int?, IInterface>("Nullable<Int>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<int?, IInterface>("Nullable<Int>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<int?, BaseClass>("Nullable<Int>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<int?, BaseClass>("Nullable<Int>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<int?, DerivedClass>("Nullable<Int>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<int?, DerivedClass>("Nullable<Int>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Long>ToXXX
                new object []
                {
                    "Nullable<Long>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<long?, bool>("Nullable<Long>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<long?, bool>("Nullable<Long>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<long?, byte>("Nullable<Long>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<long?, byte>("Nullable<Long>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, byte[]>("Nullable<Long>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<long?, byte[]>("Nullable<Long>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<long?, char>("Nullable<Long>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<long?, char>("Nullable<Long>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<long?, DateTime>("Nullable<Long>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<long?, DateTime>("Nullable<Long>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<long?, DateTimeOffset>("Nullable<Long>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<long?, DateTimeOffset>("Nullable<Long>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<long?, decimal>("Nullable<Long>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<long?, decimal>("Nullable<Long>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, double>("Nullable<Long>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<long?, double>("Nullable<Long>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, PrimaryColor>("Nullable<Long>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<long?, PrimaryColor>("Nullable<Long>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<long?, float>("Nullable<Long>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<long?, float>("Nullable<Long>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, Guid>("Nullable<Long>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<long?, Guid>("Nullable<Long>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<long?, int>("Nullable<Long>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<long?, int>("Nullable<Long>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, long>("Nullable<Long>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<long?, long>("Nullable<Long>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, sbyte>("Nullable<Long>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<long?, sbyte>("Nullable<Long>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, short>("Nullable<Long>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<long?, short>("Nullable<Long>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, string>("Nullable<Long>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<long?, string>("Nullable<Long>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<long?, TimeSpan>("Nullable<Long>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<long?, TimeSpan>("Nullable<Long>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<long?, Type>("Nullable<Long>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<long?, Type>("Nullable<Long>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<long?, uint>("Nullable<Long>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<long?, uint>("Nullable<Long>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, ulong>("Nullable<Long>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<long?, ulong>("Nullable<Long>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, Uri>("Nullable<Long>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<long?, Uri>("Nullable<Long>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<long?, ushort>("Nullable<Long>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<long?, ushort>("Nullable<Long>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<long?, bool?>("Nullable<Long>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<long?, bool?>("Nullable<Long>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<long?, byte?>("Nullable<Long>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<long?, byte?>("Nullable<Long>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, char?>("Nullable<Long>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<long?, char?>("Nullable<Long>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<long?, DateTime?>("Nullable<Long>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<long?, DateTime?>("Nullable<Long>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<long?, DateTimeOffset?>("Nullable<Long>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<long?, DateTimeOffset?>("Nullable<Long>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<long?, decimal?>("Nullable<Long>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<long?, decimal?>("Nullable<Long>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, double?>("Nullable<Long>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<long?, double?>("Nullable<Long>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, PrimaryColor?>("Nullable<Long>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<long?, PrimaryColor?>("Nullable<Long>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<long?, float?>("Nullable<Long>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<long?, float?>("Nullable<Long>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, Guid?>("Nullable<Long>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<long?, Guid?>("Nullable<Long>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<long?, int?>("Nullable<Long>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<long?, int?>("Nullable<Long>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, long?>("Nullable<Long>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<long?, long?>("Nullable<Long>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, sbyte?>("Nullable<Long>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<long?, sbyte?>("Nullable<Long>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, short?>("Nullable<Long>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<long?, short?>("Nullable<Long>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, TimeSpan?>("Nullable<Long>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<long?, TimeSpan?>("Nullable<Long>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<long?, uint?>("Nullable<Long>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<long?, uint?>("Nullable<Long>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, ulong?>("Nullable<Long>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<long?, ulong?>("Nullable<Long>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<long?, ushort?>("Nullable<Long>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<long?, ushort?>("Nullable<Long>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<long?, IInterface>("Nullable<Long>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<long?, IInterface>("Nullable<Long>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<long?, BaseClass>("Nullable<Long>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<long?, BaseClass>("Nullable<Long>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<long?, DerivedClass>("Nullable<Long>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<long?, DerivedClass>("Nullable<Long>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<SByte>ToXXX
                new object []
                {
                    "Nullable<SByte>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<sbyte?, bool>("Nullable<SByte>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<sbyte?, bool>("Nullable<SByte>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<sbyte?, byte>("Nullable<SByte>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<sbyte?, byte>("Nullable<SByte>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, byte[]>("Nullable<SByte>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<sbyte?, byte[]>("Nullable<SByte>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<sbyte?, char>("Nullable<SByte>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<sbyte?, char>("Nullable<SByte>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<sbyte?, DateTime>("Nullable<SByte>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<sbyte?, DateTime>("Nullable<SByte>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<sbyte?, DateTimeOffset>("Nullable<SByte>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<sbyte?, DateTimeOffset>("Nullable<SByte>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<sbyte?, decimal>("Nullable<SByte>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<sbyte?, decimal>("Nullable<SByte>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, double>("Nullable<SByte>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<sbyte?, double>("Nullable<SByte>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, PrimaryColor>("Nullable<SByte>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<sbyte?, PrimaryColor>("Nullable<SByte>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<sbyte?, float>("Nullable<SByte>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<sbyte?, float>("Nullable<SByte>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, Guid>("Nullable<SByte>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<sbyte?, Guid>("Nullable<SByte>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<sbyte?, int>("Nullable<SByte>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<sbyte?, int>("Nullable<SByte>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, long>("Nullable<SByte>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<sbyte?, long>("Nullable<SByte>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, sbyte>("Nullable<SByte>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<sbyte?, sbyte>("Nullable<SByte>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, short>("Nullable<SByte>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<sbyte?, short>("Nullable<SByte>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, string>("Nullable<SByte>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<sbyte?, string>("Nullable<SByte>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<sbyte?, TimeSpan>("Nullable<SByte>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<sbyte?, TimeSpan>("Nullable<SByte>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<sbyte?, Type>("Nullable<SByte>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<sbyte?, Type>("Nullable<SByte>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<sbyte?, uint>("Nullable<SByte>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<sbyte?, uint>("Nullable<SByte>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, ulong>("Nullable<SByte>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<sbyte?, ulong>("Nullable<SByte>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, Uri>("Nullable<SByte>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<sbyte?, Uri>("Nullable<SByte>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<sbyte?, ushort>("Nullable<SByte>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<sbyte?, ushort>("Nullable<SByte>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<sbyte?, bool?>("Nullable<SByte>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<sbyte?, bool?>("Nullable<SByte>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<sbyte?, byte?>("Nullable<SByte>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<sbyte?, byte?>("Nullable<SByte>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, char?>("Nullable<SByte>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<sbyte?, char?>("Nullable<SByte>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<sbyte?, DateTime?>("Nullable<SByte>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<sbyte?, DateTime?>("Nullable<SByte>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<sbyte?, DateTimeOffset?>("Nullable<SByte>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<sbyte?, DateTimeOffset?>("Nullable<SByte>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<sbyte?, decimal?>("Nullable<SByte>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<sbyte?, decimal?>("Nullable<SByte>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, double?>("Nullable<SByte>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<sbyte?, double?>("Nullable<SByte>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, PrimaryColor?>("Nullable<SByte>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<sbyte?, PrimaryColor?>("Nullable<SByte>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<sbyte?, float?>("Nullable<SByte>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<sbyte?, float?>("Nullable<SByte>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, Guid?>("Nullable<SByte>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<sbyte?, Guid?>("Nullable<SByte>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<sbyte?, int?>("Nullable<SByte>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<sbyte?, int?>("Nullable<SByte>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, long?>("Nullable<SByte>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<sbyte?, long?>("Nullable<SByte>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, sbyte?>("Nullable<SByte>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<sbyte?, sbyte?>("Nullable<SByte>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, short?>("Nullable<SByte>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<sbyte?, short?>("Nullable<SByte>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, TimeSpan?>("Nullable<SByte>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<sbyte?, TimeSpan?>("Nullable<SByte>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<sbyte?, uint?>("Nullable<SByte>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<sbyte?, uint?>("Nullable<SByte>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, ulong?>("Nullable<SByte>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<sbyte?, ulong?>("Nullable<SByte>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<sbyte?, ushort?>("Nullable<SByte>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<sbyte?, ushort?>("Nullable<SByte>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<sbyte?, IInterface>("Nullable<SByte>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<sbyte?, IInterface>("Nullable<SByte>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<sbyte?, BaseClass>("Nullable<SByte>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<sbyte?, BaseClass>("Nullable<SByte>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<sbyte?, DerivedClass>("Nullable<SByte>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<sbyte?, DerivedClass>("Nullable<SByte>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Short>ToXXX
                new object []
                {
                    "Nullable<Short>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<short?, bool>("Nullable<Short>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<short?, bool>("Nullable<Short>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<short?, byte>("Nullable<Short>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<short?, byte>("Nullable<Short>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, byte[]>("Nullable<Short>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<short?, byte[]>("Nullable<Short>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<short?, char>("Nullable<Short>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<short?, char>("Nullable<Short>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<short?, DateTime>("Nullable<Short>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<short?, DateTime>("Nullable<Short>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<short?, DateTimeOffset>("Nullable<Short>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<short?, DateTimeOffset>("Nullable<Short>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<short?, decimal>("Nullable<Short>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<short?, decimal>("Nullable<Short>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, double>("Nullable<Short>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<short?, double>("Nullable<Short>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, PrimaryColor>("Nullable<Short>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<short?, PrimaryColor>("Nullable<Short>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<short?, float>("Nullable<Short>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<short?, float>("Nullable<Short>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, Guid>("Nullable<Short>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<short?, Guid>("Nullable<Short>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<short?, int>("Nullable<Short>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<short?, int>("Nullable<Short>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, long>("Nullable<Short>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<short?, long>("Nullable<Short>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, sbyte>("Nullable<Short>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<short?, sbyte>("Nullable<Short>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, short>("Nullable<Short>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<short?, short>("Nullable<Short>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, string>("Nullable<Short>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<short?, string>("Nullable<Short>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<short?, TimeSpan>("Nullable<Short>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<short?, TimeSpan>("Nullable<Short>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<short?, Type>("Nullable<Short>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<short?, Type>("Nullable<Short>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<short?, uint>("Nullable<Short>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<short?, uint>("Nullable<Short>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, ulong>("Nullable<Short>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<short?, ulong>("Nullable<Short>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, Uri>("Nullable<Short>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<short?, Uri>("Nullable<Short>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<short?, ushort>("Nullable<Short>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<short?, ushort>("Nullable<Short>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<short?, bool?>("Nullable<Short>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<short?, bool?>("Nullable<Short>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<short?, byte?>("Nullable<Short>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<short?, byte?>("Nullable<Short>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, char?>("Nullable<Short>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<short?, char?>("Nullable<Short>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<short?, DateTime?>("Nullable<Short>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<short?, DateTime?>("Nullable<Short>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<short?, DateTimeOffset?>("Nullable<Short>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<short?, DateTimeOffset?>("Nullable<Short>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<short?, decimal?>("Nullable<Short>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<short?, decimal?>("Nullable<Short>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, double?>("Nullable<Short>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<short?, double?>("Nullable<Short>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, PrimaryColor?>("Nullable<Short>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<short?, PrimaryColor?>("Nullable<Short>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<short?, float?>("Nullable<Short>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<short?, float?>("Nullable<Short>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, Guid?>("Nullable<Short>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<short?, Guid?>("Nullable<Short>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<short?, int?>("Nullable<Short>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<short?, int?>("Nullable<Short>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, long?>("Nullable<Short>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<short?, long?>("Nullable<Short>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, sbyte?>("Nullable<Short>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<short?, sbyte?>("Nullable<Short>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, short?>("Nullable<Short>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<short?, short?>("Nullable<Short>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, TimeSpan?>("Nullable<Short>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<short?, TimeSpan?>("Nullable<Short>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<short?, uint?>("Nullable<Short>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<short?, uint?>("Nullable<Short>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, ulong?>("Nullable<Short>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<short?, ulong?>("Nullable<Short>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<short?, ushort?>("Nullable<Short>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<short?, ushort?>("Nullable<Short>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<short?, IInterface>("Nullable<Short>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<short?, IInterface>("Nullable<Short>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<short?, BaseClass>("Nullable<Short>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<short?, BaseClass>("Nullable<Short>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<short?, DerivedClass>("Nullable<Short>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<short?, DerivedClass>("Nullable<Short>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<TimeSpan>ToXXX
                new object []
                {
                    "Nullable<TimeSpan>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<TimeSpan?, bool>("Nullable<TimeSpan>ToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<TimeSpan?, bool>("Nullable<TimeSpan>ToBool", TestTimeSpan, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<TimeSpan?, byte>("Nullable<TimeSpan>ToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<TimeSpan?, byte>("Nullable<TimeSpan>ToByte", TestTimeSpan, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<TimeSpan?, byte[]>("Nullable<TimeSpan>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<TimeSpan?, byte[]>("Nullable<TimeSpan>ToByteArray", TestTimeSpan, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<TimeSpan?, char>("Nullable<TimeSpan>ToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<TimeSpan?, char>("Nullable<TimeSpan>ToChar", TestTimeSpan, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<TimeSpan?, DateTime>("Nullable<TimeSpan>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<TimeSpan?, DateTime>("Nullable<TimeSpan>ToDateTime", TestTimeSpan, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<TimeSpan?, DateTimeOffset>("Nullable<TimeSpan>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<TimeSpan?, DateTimeOffset>("Nullable<TimeSpan>ToDateTimeOffset", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<TimeSpan?, decimal>("Nullable<TimeSpan>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<TimeSpan?, decimal>("Nullable<TimeSpan>ToDecimal", TestTimeSpan, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<TimeSpan?, double>("Nullable<TimeSpan>ToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<TimeSpan?, double>("Nullable<TimeSpan>ToDouble", TestTimeSpan, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<TimeSpan?, PrimaryColor>("Nullable<TimeSpan>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<TimeSpan?, PrimaryColor>("Nullable<TimeSpan>ToEnum", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<TimeSpan?, float>("Nullable<TimeSpan>ToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<TimeSpan?, float>("Nullable<TimeSpan>ToFloat", TestTimeSpan, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<TimeSpan?, Guid>("Nullable<TimeSpan>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<TimeSpan?, Guid>("Nullable<TimeSpan>ToGuid", TestTimeSpan, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<TimeSpan?, int>("Nullable<TimeSpan>ToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<TimeSpan?, int>("Nullable<TimeSpan>ToInt", TestTimeSpan, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<TimeSpan?, long>("Nullable<TimeSpan>ToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<TimeSpan?, long>("Nullable<TimeSpan>ToLong", TestTimeSpan, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<TimeSpan?, sbyte>("Nullable<TimeSpan>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<TimeSpan?, sbyte>("Nullable<TimeSpan>ToSByte", TestTimeSpan, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<TimeSpan?, short>("Nullable<TimeSpan>ToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<TimeSpan?, short>("Nullable<TimeSpan>ToShort", TestTimeSpan, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToString", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToString", TestTimeSpan, ConvertResult.Success, TestTimeSpanString),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormat", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormat, FormatTimeSpanContext),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new ConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormatAndFormatProvider", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormatAndFormatProvider, FormatAndFormatProviderTimeSpanContext),
                            new ConvertGenericTest<TimeSpan?, TimeSpan>("Nullable<TimeSpan>ToTimeSpan", null, ConvertResult.Success, default(TimeSpan)),
                            new ConvertGenericTest<TimeSpan?, TimeSpan>("Nullable<TimeSpan>ToTimeSpan", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<TimeSpan?, Type>("Nullable<TimeSpan>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<TimeSpan?, Type>("Nullable<TimeSpan>ToType", TestTimeSpan, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<TimeSpan?, uint>("Nullable<TimeSpan>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<TimeSpan?, uint>("Nullable<TimeSpan>ToUInt", TestTimeSpan, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<TimeSpan?, ulong>("Nullable<TimeSpan>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<TimeSpan?, ulong>("Nullable<TimeSpan>ToULong", TestTimeSpan, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<TimeSpan?, Uri>("Nullable<TimeSpan>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<TimeSpan?, Uri>("Nullable<TimeSpan>ToUri", TestTimeSpan, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<TimeSpan?, ushort>("Nullable<TimeSpan>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<TimeSpan?, ushort>("Nullable<TimeSpan>ToUShort", TestTimeSpan, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<TimeSpan?, bool?>("Nullable<TimeSpan>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<TimeSpan?, bool?>("Nullable<TimeSpan>ToNullable<Bool>", TestTimeSpan, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<TimeSpan?, byte?>("Nullable<TimeSpan>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<TimeSpan?, byte?>("Nullable<TimeSpan>ToNullable<Byte>", TestTimeSpan, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<TimeSpan?, char?>("Nullable<TimeSpan>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<TimeSpan?, char?>("Nullable<TimeSpan>ToNullable<Char>", TestTimeSpan, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<TimeSpan?, DateTime?>("Nullable<TimeSpan>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<TimeSpan?, DateTime?>("Nullable<TimeSpan>ToNullable<DateTime>", TestTimeSpan, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<TimeSpan?, DateTimeOffset?>("Nullable<TimeSpan>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<TimeSpan?, DateTimeOffset?>("Nullable<TimeSpan>ToNullable<DateTimeOffset>", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<TimeSpan?, decimal?>("Nullable<TimeSpan>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<TimeSpan?, decimal?>("Nullable<TimeSpan>ToNullable<Decimal>", TestTimeSpan, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<TimeSpan?, double?>("Nullable<TimeSpan>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<TimeSpan?, double?>("Nullable<TimeSpan>ToNullable<Double>", TestTimeSpan, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<TimeSpan?, PrimaryColor?>("Nullable<TimeSpan>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<TimeSpan?, PrimaryColor?>("Nullable<TimeSpan>ToNullable<Enum>", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<TimeSpan?, float?>("Nullable<TimeSpan>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<TimeSpan?, float?>("Nullable<TimeSpan>ToNullable<Float>", TestTimeSpan, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<TimeSpan?, Guid?>("Nullable<TimeSpan>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<TimeSpan?, Guid?>("Nullable<TimeSpan>ToNullable<Guid>", TestTimeSpan, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<TimeSpan?, int?>("Nullable<TimeSpan>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<TimeSpan?, int?>("Nullable<TimeSpan>ToNullable<Int>", TestTimeSpan, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<TimeSpan?, long?>("Nullable<TimeSpan>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<TimeSpan?, long?>("Nullable<TimeSpan>ToNullable<Long>", TestTimeSpan, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<TimeSpan?, sbyte?>("Nullable<TimeSpan>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<TimeSpan?, sbyte?>("Nullable<TimeSpan>ToNullable<SByte>", TestTimeSpan, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<TimeSpan?, short?>("Nullable<TimeSpan>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<TimeSpan?, short?>("Nullable<TimeSpan>ToNullable<Short>", TestTimeSpan, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<TimeSpan?, TimeSpan?>("Nullable<TimeSpan>ToNullable<TimeSpan>", null, ConvertResult.Success, new TimeSpan?()),
                            new ConvertGenericTest<TimeSpan?, TimeSpan?>("Nullable<TimeSpan>ToNullable<TimeSpan>", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new ConvertGenericTest<TimeSpan?, uint?>("Nullable<TimeSpan>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<TimeSpan?, uint?>("Nullable<TimeSpan>ToNullable<UInt>", TestTimeSpan, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<TimeSpan?, ulong?>("Nullable<TimeSpan>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<TimeSpan?, ulong?>("Nullable<TimeSpan>ToNullable<ULong>", TestTimeSpan, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<TimeSpan?, ushort?>("Nullable<TimeSpan>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<TimeSpan?, ushort?>("Nullable<TimeSpan>ToNullable<UShort>", TestTimeSpan, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<TimeSpan?, IInterface>("Nullable<TimeSpan>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<TimeSpan?, IInterface>("Nullable<TimeSpan>ToInterface", TestTimeSpan, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<TimeSpan?, BaseClass>("Nullable<TimeSpan>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<TimeSpan?, BaseClass>("Nullable<TimeSpan>ToBaseClass", TestTimeSpan, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<TimeSpan?, DerivedClass>("Nullable<TimeSpan>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<TimeSpan?, DerivedClass>("Nullable<TimeSpan>ToDerivedClass", TestTimeSpan, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<UInt>ToXXX
                new object []
                {
                    "Nullable<UInt>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<uint?, bool>("Nullable<UInt>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<uint?, bool>("Nullable<UInt>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<uint?, byte>("Nullable<UInt>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<uint?, byte>("Nullable<UInt>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, byte[]>("Nullable<UInt>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<uint?, byte[]>("Nullable<UInt>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<uint?, char>("Nullable<UInt>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<uint?, char>("Nullable<UInt>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<uint?, DateTime>("Nullable<UInt>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<uint?, DateTime>("Nullable<UInt>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<uint?, DateTimeOffset>("Nullable<UInt>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<uint?, DateTimeOffset>("Nullable<UInt>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<uint?, decimal>("Nullable<UInt>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<uint?, decimal>("Nullable<UInt>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, double>("Nullable<UInt>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<uint?, double>("Nullable<UInt>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, PrimaryColor>("Nullable<UInt>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<uint?, PrimaryColor>("Nullable<UInt>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<uint?, float>("Nullable<UInt>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<uint?, float>("Nullable<UInt>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, Guid>("Nullable<UInt>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<uint?, Guid>("Nullable<UInt>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<uint?, int>("Nullable<UInt>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<uint?, int>("Nullable<UInt>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, long>("Nullable<UInt>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<uint?, long>("Nullable<UInt>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, sbyte>("Nullable<UInt>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<uint?, sbyte>("Nullable<UInt>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, short>("Nullable<UInt>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<uint?, short>("Nullable<UInt>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, string>("Nullable<UInt>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<uint?, string>("Nullable<UInt>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<uint?, TimeSpan>("Nullable<UInt>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<uint?, TimeSpan>("Nullable<UInt>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<uint?, Type>("Nullable<UInt>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<uint?, Type>("Nullable<UInt>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<uint?, uint>("Nullable<UInt>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<uint?, uint>("Nullable<UInt>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, ulong>("Nullable<UInt>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<uint?, ulong>("Nullable<UInt>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, Uri>("Nullable<UInt>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<uint?, Uri>("Nullable<UInt>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<uint?, ushort>("Nullable<UInt>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<uint?, ushort>("Nullable<UInt>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<uint?, bool?>("Nullable<UInt>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<uint?, bool?>("Nullable<UInt>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<uint?, byte?>("Nullable<UInt>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<uint?, byte?>("Nullable<UInt>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, char?>("Nullable<UInt>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<uint?, char?>("Nullable<UInt>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<uint?, DateTime?>("Nullable<UInt>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<uint?, DateTime?>("Nullable<UInt>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<uint?, DateTimeOffset?>("Nullable<UInt>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<uint?, DateTimeOffset?>("Nullable<UInt>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<uint?, decimal?>("Nullable<UInt>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<uint?, decimal?>("Nullable<UInt>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, double?>("Nullable<UInt>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<uint?, double?>("Nullable<UInt>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, PrimaryColor?>("Nullable<UInt>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<uint?, PrimaryColor?>("Nullable<UInt>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<uint?, float?>("Nullable<UInt>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<uint?, float?>("Nullable<UInt>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, Guid?>("Nullable<UInt>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<uint?, Guid?>("Nullable<UInt>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<uint?, int?>("Nullable<UInt>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<uint?, int?>("Nullable<UInt>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, long?>("Nullable<UInt>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<uint?, long?>("Nullable<UInt>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, sbyte?>("Nullable<UInt>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<uint?, sbyte?>("Nullable<UInt>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, short?>("Nullable<UInt>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<uint?, short?>("Nullable<UInt>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, TimeSpan?>("Nullable<UInt>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<uint?, TimeSpan?>("Nullable<UInt>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<uint?, uint?>("Nullable<UInt>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<uint?, uint?>("Nullable<UInt>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, ulong?>("Nullable<UInt>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<uint?, ulong?>("Nullable<UInt>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<uint?, ushort?>("Nullable<UInt>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<uint?, ushort?>("Nullable<UInt>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<uint?, IInterface>("Nullable<UInt>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<uint?, IInterface>("Nullable<UInt>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<uint?, BaseClass>("Nullable<UInt>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<uint?, BaseClass>("Nullable<UInt>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<uint?, DerivedClass>("Nullable<UInt>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<uint?, DerivedClass>("Nullable<UInt>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<ULong>ToXXX
                new object []
                {
                    "Nullable<ULong>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<ulong?, bool>("Nullable<ULong>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<ulong?, bool>("Nullable<ULong>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ulong?, byte>("Nullable<ULong>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<ulong?, byte>("Nullable<ULong>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, byte[]>("Nullable<ULong>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ulong?, byte[]>("Nullable<ULong>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ulong?, char>("Nullable<ULong>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<ulong?, char>("Nullable<ULong>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ulong?, DateTime>("Nullable<ULong>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ulong?, DateTime>("Nullable<ULong>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ulong?, DateTimeOffset>("Nullable<ULong>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ulong?, DateTimeOffset>("Nullable<ULong>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ulong?, decimal>("Nullable<ULong>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<ulong?, decimal>("Nullable<ULong>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, double>("Nullable<ULong>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<ulong?, double>("Nullable<ULong>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, PrimaryColor>("Nullable<ULong>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<ulong?, PrimaryColor>("Nullable<ULong>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ulong?, float>("Nullable<ULong>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<ulong?, float>("Nullable<ULong>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, Guid>("Nullable<ULong>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ulong?, Guid>("Nullable<ULong>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ulong?, int>("Nullable<ULong>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<ulong?, int>("Nullable<ULong>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, long>("Nullable<ULong>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<ulong?, long>("Nullable<ULong>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, sbyte>("Nullable<ULong>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<ulong?, sbyte>("Nullable<ULong>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, short>("Nullable<ULong>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<ulong?, short>("Nullable<ULong>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, string>("Nullable<ULong>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<ulong?, string>("Nullable<ULong>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<ulong?, TimeSpan>("Nullable<ULong>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ulong?, TimeSpan>("Nullable<ULong>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ulong?, Type>("Nullable<ULong>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ulong?, Type>("Nullable<ULong>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ulong?, uint>("Nullable<ULong>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<ulong?, uint>("Nullable<ULong>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, ulong>("Nullable<ULong>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<ulong?, ulong>("Nullable<ULong>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, Uri>("Nullable<ULong>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ulong?, Uri>("Nullable<ULong>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ulong?, ushort>("Nullable<ULong>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<ulong?, ushort>("Nullable<ULong>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<ulong?, bool?>("Nullable<ULong>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<ulong?, bool?>("Nullable<ULong>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ulong?, byte?>("Nullable<ULong>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<ulong?, byte?>("Nullable<ULong>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, char?>("Nullable<ULong>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<ulong?, char?>("Nullable<ULong>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ulong?, DateTime?>("Nullable<ULong>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ulong?, DateTime?>("Nullable<ULong>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ulong?, DateTimeOffset?>("Nullable<ULong>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ulong?, DateTimeOffset?>("Nullable<ULong>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ulong?, decimal?>("Nullable<ULong>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<ulong?, decimal?>("Nullable<ULong>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, double?>("Nullable<ULong>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<ulong?, double?>("Nullable<ULong>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, PrimaryColor?>("Nullable<ULong>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<ulong?, PrimaryColor?>("Nullable<ULong>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ulong?, float?>("Nullable<ULong>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<ulong?, float?>("Nullable<ULong>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, Guid?>("Nullable<ULong>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ulong?, Guid?>("Nullable<ULong>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ulong?, int?>("Nullable<ULong>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<ulong?, int?>("Nullable<ULong>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, long?>("Nullable<ULong>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<ulong?, long?>("Nullable<ULong>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, sbyte?>("Nullable<ULong>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<ulong?, sbyte?>("Nullable<ULong>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, short?>("Nullable<ULong>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<ulong?, short?>("Nullable<ULong>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, TimeSpan?>("Nullable<ULong>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ulong?, TimeSpan?>("Nullable<ULong>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ulong?, uint?>("Nullable<ULong>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<ulong?, uint?>("Nullable<ULong>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, ulong?>("Nullable<ULong>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<ulong?, ulong?>("Nullable<ULong>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ulong?, ushort?>("Nullable<ULong>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<ulong?, ushort?>("Nullable<ULong>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<ulong?, IInterface>("Nullable<ULong>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ulong?, IInterface>("Nullable<ULong>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ulong?, BaseClass>("Nullable<ULong>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ulong?, BaseClass>("Nullable<ULong>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ulong?, DerivedClass>("Nullable<ULong>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<ulong?, DerivedClass>("Nullable<ULong>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<UShort>ToXXX
                new object []
                {
                    "Nullable<UShort>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<ushort?, bool>("Nullable<UShort>ToBool", null, ConvertResult.Success, default(bool)),
                            new ConvertGenericTest<ushort?, bool>("Nullable<UShort>ToBool", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ushort?, byte>("Nullable<UShort>ToByte", null, ConvertResult.Success, default(byte)),
                            new ConvertGenericTest<ushort?, byte>("Nullable<UShort>ToByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, byte[]>("Nullable<UShort>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ushort?, byte[]>("Nullable<UShort>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<ushort?, char>("Nullable<UShort>ToChar", null, ConvertResult.Success, default(char)),
                            new ConvertGenericTest<ushort?, char>("Nullable<UShort>ToChar", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ushort?, DateTime>("Nullable<UShort>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ushort?, DateTime>("Nullable<UShort>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<ushort?, DateTimeOffset>("Nullable<UShort>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ushort?, DateTimeOffset>("Nullable<UShort>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<ushort?, decimal>("Nullable<UShort>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new ConvertGenericTest<ushort?, decimal>("Nullable<UShort>ToDecimal", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, double>("Nullable<UShort>ToDouble", null, ConvertResult.Success, default(double)),
                            new ConvertGenericTest<ushort?, double>("Nullable<UShort>ToDouble", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, PrimaryColor>("Nullable<UShort>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new ConvertGenericTest<ushort?, PrimaryColor>("Nullable<UShort>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ushort?, float>("Nullable<UShort>ToFloat", null, ConvertResult.Success, default(float)),
                            new ConvertGenericTest<ushort?, float>("Nullable<UShort>ToFloat", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, Guid>("Nullable<UShort>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ushort?, Guid>("Nullable<UShort>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<ushort?, int>("Nullable<UShort>ToInt", null, ConvertResult.Success, default(int)),
                            new ConvertGenericTest<ushort?, int>("Nullable<UShort>ToInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, long>("Nullable<UShort>ToLong", null, ConvertResult.Success, default(long)),
                            new ConvertGenericTest<ushort?, long>("Nullable<UShort>ToLong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, sbyte>("Nullable<UShort>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new ConvertGenericTest<ushort?, sbyte>("Nullable<UShort>ToSByte", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, short>("Nullable<UShort>ToShort", null, ConvertResult.Success, default(short)),
                            new ConvertGenericTest<ushort?, short>("Nullable<UShort>ToShort", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, string>("Nullable<UShort>ToString", null, ConvertResult.Success, null),
                            new ConvertGenericTest<ushort?, string>("Nullable<UShort>ToString", 42, ConvertResult.Success, "42"),
                            new ConvertGenericTest<ushort?, TimeSpan>("Nullable<UShort>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ushort?, TimeSpan>("Nullable<UShort>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<ushort?, Type>("Nullable<UShort>ToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ushort?, Type>("Nullable<UShort>ToType", 42, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<ushort?, uint>("Nullable<UShort>ToUInt", null, ConvertResult.Success, default(uint)),
                            new ConvertGenericTest<ushort?, uint>("Nullable<UShort>ToUInt", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, ulong>("Nullable<UShort>ToULong", null, ConvertResult.Success, default(ulong)),
                            new ConvertGenericTest<ushort?, ulong>("Nullable<UShort>ToULong", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, Uri>("Nullable<UShort>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ushort?, Uri>("Nullable<UShort>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<ushort?, ushort>("Nullable<UShort>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new ConvertGenericTest<ushort?, ushort>("Nullable<UShort>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new ConvertGenericTest<ushort?, bool?>("Nullable<UShort>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new ConvertGenericTest<ushort?, bool?>("Nullable<UShort>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new ConvertGenericTest<ushort?, byte?>("Nullable<UShort>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new ConvertGenericTest<ushort?, byte?>("Nullable<UShort>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, char?>("Nullable<UShort>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new ConvertGenericTest<ushort?, char?>("Nullable<UShort>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new ConvertGenericTest<ushort?, DateTime?>("Nullable<UShort>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ushort?, DateTime?>("Nullable<UShort>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new ConvertGenericTest<ushort?, DateTimeOffset?>("Nullable<UShort>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ushort?, DateTimeOffset?>("Nullable<UShort>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new ConvertGenericTest<ushort?, decimal?>("Nullable<UShort>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new ConvertGenericTest<ushort?, decimal?>("Nullable<UShort>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, double?>("Nullable<UShort>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new ConvertGenericTest<ushort?, double?>("Nullable<UShort>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, PrimaryColor?>("Nullable<UShort>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new ConvertGenericTest<ushort?, PrimaryColor?>("Nullable<UShort>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new ConvertGenericTest<ushort?, float?>("Nullable<UShort>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new ConvertGenericTest<ushort?, float?>("Nullable<UShort>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, Guid?>("Nullable<UShort>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ushort?, Guid?>("Nullable<UShort>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new ConvertGenericTest<ushort?, int?>("Nullable<UShort>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new ConvertGenericTest<ushort?, int?>("Nullable<UShort>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, long?>("Nullable<UShort>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new ConvertGenericTest<ushort?, long?>("Nullable<UShort>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, sbyte?>("Nullable<UShort>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new ConvertGenericTest<ushort?, sbyte?>("Nullable<UShort>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, short?>("Nullable<UShort>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new ConvertGenericTest<ushort?, short?>("Nullable<UShort>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, TimeSpan?>("Nullable<UShort>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ushort?, TimeSpan?>("Nullable<UShort>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new ConvertGenericTest<ushort?, uint?>("Nullable<UShort>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new ConvertGenericTest<ushort?, uint?>("Nullable<UShort>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, ulong?>("Nullable<UShort>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new ConvertGenericTest<ushort?, ulong?>("Nullable<UShort>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new ConvertGenericTest<ushort?, ushort?>("Nullable<UShort>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new ConvertGenericTest<ushort?, ushort?>("Nullable<UShort>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new ConvertGenericTest<ushort?, IInterface>("Nullable<UShort>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ushort?, IInterface>("Nullable<UShort>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new ConvertGenericTest<ushort?, BaseClass>("Nullable<UShort>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ushort?, BaseClass>("Nullable<UShort>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new ConvertGenericTest<ushort?, DerivedClass>("Nullable<UShort>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<ushort?, DerivedClass>("Nullable<UShort>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                // Interface/Class Types ////////////////////////////////////

                #region BaseClassToXXX
                new object []
                {
                    "BaseClassToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<BaseClass, bool>("BaseClassToBool", TestBaseClass, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<BaseClass, bool>("BaseClassToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<BaseClass, byte>("BaseClassToByte", TestBaseClass, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<BaseClass, byte>("BaseClassToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<BaseClass, byte[]>("BaseClassToByteArray", TestBaseClass, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<BaseClass, byte[]>("BaseClassToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<BaseClass, char>("BaseClassToChar", TestBaseClass, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<BaseClass, char>("BaseClassToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<BaseClass, DateTime>("BaseClassToDateTime", TestBaseClass, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<BaseClass, DateTime>("BaseClassToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<BaseClass, DateTimeOffset>("BaseClassToDateTimeOffset", TestBaseClass, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<BaseClass, DateTimeOffset>("BaseClassToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<BaseClass, decimal>("BaseClassToDecimal", TestBaseClass, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<BaseClass, decimal>("BaseClassToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<BaseClass, double>("BaseClassToDouble", TestBaseClass, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<BaseClass, double>("BaseClassToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<BaseClass, PrimaryColor>("BaseClassToEnum", TestBaseClass, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<BaseClass, PrimaryColor>("BaseClassToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<BaseClass, float>("BaseClassToFloat", TestBaseClass, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<BaseClass, float>("BaseClassToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<BaseClass, Guid>("BaseClassToGuid", TestBaseClass, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<BaseClass, Guid>("BaseClassToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<BaseClass, int>("BaseClassToInt", TestBaseClass, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<BaseClass, int>("BaseClassToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<BaseClass, long>("BaseClassToLong", TestBaseClass, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<BaseClass, long>("BaseClassToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<BaseClass, sbyte>("BaseClassToSByte", TestBaseClass, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<BaseClass, sbyte>("BaseClassToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<BaseClass, short>("BaseClassToShort", TestBaseClass, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<BaseClass, short>("BaseClassToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<BaseClass, string>("BaseClassToString", TestBaseClass, ConvertResult.Failure, default(string)),
                            new ConvertGenericTest<BaseClass, string>("BaseClassToString", null, ConvertResult.Failure, default(string)),
                            new ConvertGenericTest<BaseClass, TimeSpan>("BaseClassToTimeSpan", TestBaseClass, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<BaseClass, TimeSpan>("BaseClassToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<BaseClass, Type>("BaseClassToType", TestBaseClass, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<BaseClass, Type>("BaseClassToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<BaseClass, uint>("BaseClassToUInt", TestBaseClass, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<BaseClass, uint>("BaseClassToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<BaseClass, ulong>("BaseClassToULong", TestBaseClass, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<BaseClass, ulong>("BaseClassToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<BaseClass, Uri>("BaseClassToUri", TestBaseClass, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<BaseClass, Uri>("BaseClassToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<BaseClass, ushort>("BaseClassToUShort", TestBaseClass, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<BaseClass, ushort>("BaseClassToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<BaseClass, bool?>("BaseClassToNullable<Bool>", TestBaseClass, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<BaseClass, bool?>("BaseClassToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<BaseClass, byte?>("BaseClassToNullable<Byte>", TestBaseClass, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<BaseClass, byte?>("BaseClassToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<BaseClass, char?>("BaseClassToNullable<Char>", TestBaseClass, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<BaseClass, char?>("BaseClassToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<BaseClass, DateTime?>("BaseClassToNullable<DateTime>", TestBaseClass, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<BaseClass, DateTime?>("BaseClassToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<BaseClass, DateTimeOffset?>("BaseClassToNullable<DateTimeOffset>", TestBaseClass, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<BaseClass, DateTimeOffset?>("BaseClassToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<BaseClass, decimal?>("BaseClassToNullable<Decimal>", TestBaseClass, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<BaseClass, decimal?>("BaseClassToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<BaseClass, double?>("BaseClassToNullable<Double>", TestBaseClass, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<BaseClass, double?>("BaseClassToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<BaseClass, PrimaryColor?>("BaseClassToNullable<Enum>", TestBaseClass, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<BaseClass, PrimaryColor?>("BaseClassToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<BaseClass, float?>("BaseClassToNullable<Float>", TestBaseClass, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<BaseClass, float?>("BaseClassToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<BaseClass, Guid?>("BaseClassToNullable<Guid>", TestBaseClass, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<BaseClass, Guid?>("BaseClassToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<BaseClass, int?>("BaseClassToNullable<Int>", TestBaseClass, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<BaseClass, int?>("BaseClassToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<BaseClass, long?>("BaseClassToNullable<Long>", TestBaseClass, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<BaseClass, long?>("BaseClassToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<BaseClass, sbyte?>("BaseClassToNullable<SByte>", TestBaseClass, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<BaseClass, sbyte?>("BaseClassToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<BaseClass, short?>("BaseClassToNullable<Short>", TestBaseClass, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<BaseClass, short?>("BaseClassToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<BaseClass, TimeSpan?>("BaseClassToNullable<TimeSpan>", TestBaseClass, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<BaseClass, TimeSpan?>("BaseClassToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<BaseClass, uint?>("BaseClassToNullable<UInt>", TestBaseClass, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<BaseClass, uint?>("BaseClassToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<BaseClass, ulong?>("BaseClassToNullable<ULong>", TestBaseClass, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<BaseClass, ulong?>("BaseClassToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<BaseClass, ushort?>("BaseClassToNullable<UShort>", TestBaseClass, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<BaseClass, ushort?>("BaseClassToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<BaseClass, IInterface>("BaseClassToInterface", TestBaseClass, ConvertResult.Success, TestBaseClass),
                            new ConvertGenericTest<BaseClass, IInterface>("BaseClassToInterface", null, ConvertResult.Success, default(IInterface)),
                            new ConvertGenericTest<BaseClass, BaseClass>("BaseClassToBaseClass", TestBaseClass, ConvertResult.Success, TestBaseClass),
                            new ConvertGenericTest<BaseClass, BaseClass>("BaseClassToBaseClass", null, ConvertResult.Success, default(BaseClass)),
                            new ConvertGenericTest<BaseClass, DerivedClass>("BaseClassToDerivedClass", TestBaseClass, ConvertResult.Failure, default(DerivedClass)),
                            new ConvertGenericTest<BaseClass, DerivedClass>("BaseClassToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DerivedClassToXXX
                new object []
                {
                    "DerivedClassToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new ConvertGenericTest<DerivedClass, bool>("DerivedClassToBool", TestDerivedClass, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DerivedClass, bool>("DerivedClassToBool", null, ConvertResult.Failure, default(bool)),
                            new ConvertGenericTest<DerivedClass, byte>("DerivedClassToByte", TestDerivedClass, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DerivedClass, byte>("DerivedClassToByte", null, ConvertResult.Failure, default(byte)),
                            new ConvertGenericTest<DerivedClass, byte[]>("DerivedClassToByteArray", TestDerivedClass, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DerivedClass, byte[]>("DerivedClassToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new ConvertGenericTest<DerivedClass, char>("DerivedClassToChar", TestDerivedClass, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DerivedClass, char>("DerivedClassToChar", null, ConvertResult.Failure, default(char)),
                            new ConvertGenericTest<DerivedClass, DateTime>("DerivedClassToDateTime", TestDerivedClass, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<DerivedClass, DateTime>("DerivedClassToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new ConvertGenericTest<DerivedClass, DateTimeOffset>("DerivedClassToDateTimeOffset", TestDerivedClass, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<DerivedClass, DateTimeOffset>("DerivedClassToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new ConvertGenericTest<DerivedClass, decimal>("DerivedClassToDecimal", TestDerivedClass, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DerivedClass, decimal>("DerivedClassToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new ConvertGenericTest<DerivedClass, double>("DerivedClassToDouble", TestDerivedClass, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DerivedClass, double>("DerivedClassToDouble", null, ConvertResult.Failure, default(double)),
                            new ConvertGenericTest<DerivedClass, PrimaryColor>("DerivedClassToEnum", TestDerivedClass, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DerivedClass, PrimaryColor>("DerivedClassToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new ConvertGenericTest<DerivedClass, float>("DerivedClassToFloat", TestDerivedClass, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DerivedClass, float>("DerivedClassToFloat", null, ConvertResult.Failure, default(float)),
                            new ConvertGenericTest<DerivedClass, Guid>("DerivedClassToGuid", TestDerivedClass, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DerivedClass, Guid>("DerivedClassToGuid", null, ConvertResult.Failure, default(Guid)),
                            new ConvertGenericTest<DerivedClass, int>("DerivedClassToInt", TestDerivedClass, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DerivedClass, int>("DerivedClassToInt", null, ConvertResult.Failure, default(int)),
                            new ConvertGenericTest<DerivedClass, long>("DerivedClassToLong", TestDerivedClass, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DerivedClass, long>("DerivedClassToLong", null, ConvertResult.Failure, default(long)),
                            new ConvertGenericTest<DerivedClass, sbyte>("DerivedClassToSByte", TestDerivedClass, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DerivedClass, sbyte>("DerivedClassToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new ConvertGenericTest<DerivedClass, short>("DerivedClassToShort", TestDerivedClass, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DerivedClass, short>("DerivedClassToShort", null, ConvertResult.Failure, default(short)),
                            new ConvertGenericTest<DerivedClass, string>("DerivedClassToString", TestDerivedClass, ConvertResult.Failure, default(string)),
                            new ConvertGenericTest<DerivedClass, string>("DerivedClassToString", null, ConvertResult.Failure, default(string)),
                            new ConvertGenericTest<DerivedClass, TimeSpan>("DerivedClassToTimeSpan", TestDerivedClass, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DerivedClass, TimeSpan>("DerivedClassToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new ConvertGenericTest<DerivedClass, Type>("DerivedClassToType", TestDerivedClass, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DerivedClass, Type>("DerivedClassToType", null, ConvertResult.Failure, default(Type)),
                            new ConvertGenericTest<DerivedClass, uint>("DerivedClassToUInt", TestDerivedClass, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DerivedClass, uint>("DerivedClassToUInt", null, ConvertResult.Failure, default(uint)),
                            new ConvertGenericTest<DerivedClass, ulong>("DerivedClassToULong", TestDerivedClass, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DerivedClass, ulong>("DerivedClassToULong", null, ConvertResult.Failure, default(ulong)),
                            new ConvertGenericTest<DerivedClass, Uri>("DerivedClassToUri", TestDerivedClass, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DerivedClass, Uri>("DerivedClassToUri", null, ConvertResult.Failure, default(Uri)),
                            new ConvertGenericTest<DerivedClass, ushort>("DerivedClassToUShort", TestDerivedClass, ConvertResult.Failure, default(ushort)),
                            new ConvertGenericTest<DerivedClass, ushort>("DerivedClassToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new ConvertGenericTest<DerivedClass, bool?>("DerivedClassToNullable<Bool>", TestDerivedClass, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DerivedClass, bool?>("DerivedClassToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new ConvertGenericTest<DerivedClass, byte?>("DerivedClassToNullable<Byte>", TestDerivedClass, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DerivedClass, byte?>("DerivedClassToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new ConvertGenericTest<DerivedClass, char?>("DerivedClassToNullable<Char>", TestDerivedClass, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DerivedClass, char?>("DerivedClassToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new ConvertGenericTest<DerivedClass, DateTime?>("DerivedClassToNullable<DateTime>", TestDerivedClass, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<DerivedClass, DateTime?>("DerivedClassToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new ConvertGenericTest<DerivedClass, DateTimeOffset?>("DerivedClassToNullable<DateTimeOffset>", TestDerivedClass, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<DerivedClass, DateTimeOffset?>("DerivedClassToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new ConvertGenericTest<DerivedClass, decimal?>("DerivedClassToNullable<Decimal>", TestDerivedClass, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DerivedClass, decimal?>("DerivedClassToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new ConvertGenericTest<DerivedClass, double?>("DerivedClassToNullable<Double>", TestDerivedClass, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DerivedClass, double?>("DerivedClassToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new ConvertGenericTest<DerivedClass, PrimaryColor?>("DerivedClassToNullable<Enum>", TestDerivedClass, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DerivedClass, PrimaryColor?>("DerivedClassToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new ConvertGenericTest<DerivedClass, float?>("DerivedClassToNullable<Float>", TestDerivedClass, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DerivedClass, float?>("DerivedClassToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new ConvertGenericTest<DerivedClass, Guid?>("DerivedClassToNullable<Guid>", TestDerivedClass, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<DerivedClass, Guid?>("DerivedClassToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new ConvertGenericTest<DerivedClass, int?>("DerivedClassToNullable<Int>", TestDerivedClass, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DerivedClass, int?>("DerivedClassToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new ConvertGenericTest<DerivedClass, long?>("DerivedClassToNullable<Long>", TestDerivedClass, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DerivedClass, long?>("DerivedClassToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new ConvertGenericTest<DerivedClass, sbyte?>("DerivedClassToNullable<SByte>", TestDerivedClass, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DerivedClass, sbyte?>("DerivedClassToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new ConvertGenericTest<DerivedClass, short?>("DerivedClassToNullable<Short>", TestDerivedClass, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DerivedClass, short?>("DerivedClassToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new ConvertGenericTest<DerivedClass, TimeSpan?>("DerivedClassToNullable<TimeSpan>", TestDerivedClass, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<DerivedClass, TimeSpan?>("DerivedClassToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new ConvertGenericTest<DerivedClass, uint?>("DerivedClassToNullable<UInt>", TestDerivedClass, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DerivedClass, uint?>("DerivedClassToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new ConvertGenericTest<DerivedClass, ulong?>("DerivedClassToNullable<ULong>", TestDerivedClass, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DerivedClass, ulong?>("DerivedClassToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new ConvertGenericTest<DerivedClass, ushort?>("DerivedClassToNullable<UShort>", TestDerivedClass, ConvertResult.Failure, default(ushort?)),
                            new ConvertGenericTest<DerivedClass, ushort?>("DerivedClassToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new ConvertGenericTest<DerivedClass, IInterface>("DerivedClassToInterface", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new ConvertGenericTest<DerivedClass, IInterface>("DerivedClassToInterface", null, ConvertResult.Success, default(IInterface)),
                            new ConvertGenericTest<DerivedClass, BaseClass>("DerivedClassToBaseClass", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new ConvertGenericTest<DerivedClass, BaseClass>("DerivedClassToBaseClass", null, ConvertResult.Success, default(BaseClass)),
                            new ConvertGenericTest<DerivedClass, DerivedClass>("DerivedClassToDerivedClass", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new ConvertGenericTest<DerivedClass, DerivedClass>("DerivedClassToDerivedClass", null, ConvertResult.Success, default(DerivedClass)),
                        }
                },
                #endregion

            };

        public static readonly IEnumerable<object[]> TryConvertGenericTestData = new[]
            {
                // Simple Types /////////////////////////////////////////////

                #region BoolToXXX
                new object []
                {
                    "BoolToXXX - Generic",
                    new object []
                        {
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
                            new TryConvertGenericTest<bool, DerivedClass>("BoolToDerivedClass", true, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region ByteToXXX
                new object []
                {
                    "ByteToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<byte, bool>("ByteToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<byte, byte>("ByteToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte, byte[]>("ByteToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<byte, char>("ByteToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<byte, DateTime>("ByteToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte, DateTimeOffset>("ByteToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
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
                            new TryConvertGenericTest<byte, Guid?>("ByteToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
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
                            new TryConvertGenericTest<byte, DerivedClass>("ByteToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region ByteArrayToXXX
                new object []
                {
                    "ByteArrayToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<byte[], bool>("ByteArrayToBool", TestByteArray, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<byte[], bool>("ByteArrayToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<byte[], byte>("ByteArrayToByte", TestByteArray, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<byte[], byte>("ByteArrayToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<byte[], byte[]>("ByteArrayToByteArray", TestByteArray, ConvertResult.Success, TestByteArray),
                            new TryConvertGenericTest<byte[], byte[]>("ByteArrayToByteArray", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<byte[], char>("ByteArrayToChar", TestByteArray, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<byte[], char>("ByteArrayToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<byte[], DateTime>("ByteArrayToDateTime", TestByteArray, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte[], DateTime>("ByteArrayToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte[], DateTimeOffset>("ByteArrayToDateTimeOffset", TestByteArray, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<byte[], DateTimeOffset>("ByteArrayToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<byte[], decimal>("ByteArrayToDecimal", TestByteArray, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<byte[], decimal>("ByteArrayToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<byte[], double>("ByteArrayToDouble", TestByteArray, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<byte[], double>("ByteArrayToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<byte[], PrimaryColor>("ByteArrayToEnum", TestByteArray, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<byte[], PrimaryColor>("ByteArrayToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<byte[], float>("ByteArrayToFloat", TestByteArray, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<byte[], float>("ByteArrayToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<byte[], Guid>("ByteArrayToGuid", TestGuidByteArray, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<byte[], Guid>("ByteArrayToGuid", null, ConvertResult.Success, default(Guid)),
                            new TryConvertGenericTest<byte[], int>("ByteArrayToInt", TestByteArray, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<byte[], int>("ByteArrayToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<byte[], long>("ByteArrayToLong", TestByteArray, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<byte[], long>("ByteArrayToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<byte[], sbyte>("ByteArrayToSByte", TestByteArray, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<byte[], sbyte>("ByteArrayToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<byte[], short>("ByteArrayToShort", TestByteArray, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<byte[], short>("ByteArrayToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<byte[], string>("ByteArrayToString", TestByteArray, ConvertResult.Success, TestByteArrayString),
                            new TryConvertGenericTest<byte[], string>("ByteArrayToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<byte[], TimeSpan>("ByteArrayToTimeSpan", TestByteArray, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<byte[], TimeSpan>("ByteArrayToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<byte[], Type>("ByteArrayToType", TestByteArray, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<byte[], Type>("ByteArrayToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<byte[], uint>("ByteArrayToUInt", TestByteArray, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<byte[], uint>("ByteArrayToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<byte[], ulong>("ByteArrayToULong", TestByteArray, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<byte[], ulong>("ByteArrayToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<byte[], Uri>("ByteArrayToUri", TestByteArray, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<byte[], Uri>("ByteArrayToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<byte[], ushort>("ByteArrayToUShort", TestByteArray, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<byte[], ushort>("ByteArrayToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<byte[], bool?>("ByteArrayToNullable<Bool>", TestByteArray, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<byte[], bool?>("ByteArrayToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<byte[], byte?>("ByteArrayToNullable<Byte>", TestByteArray, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<byte[], byte?>("ByteArrayToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<byte[], char?>("ByteArrayToNullable<Char>", TestByteArray, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<byte[], char?>("ByteArrayToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<byte[], DateTime?>("ByteArrayToNullable<DateTime>", TestByteArray, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<byte[], DateTime?>("ByteArrayToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<byte[], DateTimeOffset?>("ByteArrayToNullable<DateTimeOffset>", TestByteArray, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<byte[], DateTimeOffset?>("ByteArrayToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<byte[], decimal?>("ByteArrayToNullable<Decimal>", TestByteArray, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<byte[], decimal?>("ByteArrayToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<byte[], double?>("ByteArrayToNullable<Double>", TestByteArray, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<byte[], double?>("ByteArrayToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<byte[], PrimaryColor?>("ByteArrayToNullable<Enum>", TestByteArray, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<byte[], PrimaryColor?>("ByteArrayToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<byte[], float?>("ByteArrayToNullable<Float>", TestByteArray, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<byte[], float?>("ByteArrayToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<byte[], Guid?>("ByteArrayToNullable<Guid>", TestGuidByteArray, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<byte[], Guid?>("ByteArrayToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new TryConvertGenericTest<byte[], int?>("ByteArrayToNullable<Int>", TestByteArray, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<byte[], int?>("ByteArrayToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<byte[], long?>("ByteArrayToNullable<Long>", TestByteArray, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<byte[], long?>("ByteArrayToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<byte[], sbyte?>("ByteArrayToNullable<SByte>", TestByteArray, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<byte[], sbyte?>("ByteArrayToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<byte[], short?>("ByteArrayToNullable<Short>", TestByteArray, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<byte[], short?>("ByteArrayToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<byte[], TimeSpan?>("ByteArrayToNullable<TimeSpan>", TestByteArray, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<byte[], TimeSpan?>("ByteArrayToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<byte[], uint?>("ByteArrayToNullable<UInt>", TestByteArray, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<byte[], uint?>("ByteArrayToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<byte[], ulong?>("ByteArrayToNullable<ULong>", TestByteArray, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<byte[], ulong?>("ByteArrayToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<byte[], ushort?>("ByteArrayToNullable<UShort>", TestByteArray, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<byte[], ushort?>("ByteArrayToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<byte[], IInterface>("ByteArrayToInterface", TestByteArray, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<byte[], IInterface>("ByteArrayToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<byte[], BaseClass>("ByteArrayToBaseClass", TestByteArray, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<byte[], BaseClass>("ByteArrayToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<byte[], DerivedClass>("ByteArrayToDerivedClass", TestByteArray, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<byte[], DerivedClass>("ByteArrayToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region CharToXXX
                new object []
                {
                    "CharToXXX - Generic",
                    new object []
                        {
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
                            new TryConvertGenericTest<char, DerivedClass>("CharToDerivedClass", '*', ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DateTimeToXXX
                new object []
                {
                    "DateTimeToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<DateTime, bool>("DateTimeToBool", TestDateTime, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTime, byte>("DateTimeToByte", TestDateTime, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTime, byte[]>("DateTimeToByteArray", TestDateTime, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTime, char>("DateTimeToChar", TestDateTime, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTime, DateTime>("DateTimeToDateTime", TestDateTime, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTime, DateTimeOffset>("DateTimeToDateTimeOffset", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTime, decimal>("DateTimeToDecimal", TestDateTime, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTime, double>("DateTimeToDouble", TestDateTime, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTime, PrimaryColor>("DateTimeToEnum", TestDateTime, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTime, float>("DateTimeToFloat", TestDateTime, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTime, Guid>("DateTimeToGuid", TestDateTime, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTime, int>("DateTimeToInt", TestDateTime, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTime, long>("DateTimeToLong", TestDateTime, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTime, sbyte>("DateTimeToSByte", TestDateTime, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTime, short>("DateTimeToShort", TestDateTime, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTime, string>("DateTimeToString", TestDateTime, ConvertResult.Success, TestDateTimeString),
                            new TryConvertGenericTest<DateTime, string>("DateTimeToStringWithFormat", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormat, FormatDateTimeContext),
                            new TryConvertGenericTest<DateTime, string>("DateTimeToStringWithFormatAndFormatProvider", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeContext),
                            new TryConvertGenericTest<DateTime, TimeSpan>("DateTimeToTimeSpan", TestDateTime, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTime, Type>("DateTimeToType", TestDateTime, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTime, uint>("DateTimeToUInt", TestDateTime, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTime, ulong>("DateTimeToULong", TestDateTime, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTime, Uri>("DateTimeToUri", TestDateTime, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTime, ushort>("DateTimeToUShort", TestDateTime, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DateTime, bool?>("DateTimeToNullable<Bool>", TestDateTime, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTime, byte?>("DateTimeToNullable<Byte>", TestDateTime, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTime, char?>("DateTimeToNullable<Char>", TestDateTime, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTime, DateTime?>("DateTimeToNullable<DateTime>", TestDateTime, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTime, DateTimeOffset?>("DateTimeToNullable<DateTimeOffset>", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTime, decimal?>("DateTimeToNullable<Decimal>", TestDateTime, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTime, double?>("DateTimeToNullable<Double>", TestDateTime, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTime, PrimaryColor?>("DateTimeToNullable<Enum>", TestDateTime, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTime, float?>("DateTimeToNullable<Float>", TestDateTime, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTime, Guid?>("DateTimeToNullable<Guid>", TestDateTime, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<DateTime, int?>("DateTimeToNullable<Int>", TestDateTime, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTime, long?>("DateTimeToNullable<Long>", TestDateTime, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTime, sbyte?>("DateTimeToNullable<SByte>", TestDateTime, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTime, short?>("DateTimeToNullable<Short>", TestDateTime, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTime, TimeSpan?>("DateTimeToNullable<TimeSpan>", TestDateTime, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<DateTime, uint?>("DateTimeToNullable<UInt>", TestDateTime, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTime, ulong?>("DateTimeToNullable<ULong>", TestDateTime, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTime, ushort?>("DateTimeToNullable<UShort>", TestDateTime, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DateTime, IInterface>("DateTimeToInterface", TestDateTime, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTime, BaseClass>("DateTimeToBaseClass", TestDateTime, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTime, DerivedClass>("DateTimeToDerivedClass", TestDateTime, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DateTimeOffsetToXXX
                new object []
                {
                    "DateTimeOffsetToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<DateTimeOffset, bool>("DateTimeOffsetToBool", TestDateTimeOffset, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTimeOffset, byte>("DateTimeOffsetToByte", TestDateTimeOffset, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTimeOffset, byte[]>("DateTimeOffsetToByteArray", TestDateTimeOffset, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTimeOffset, char>("DateTimeOffsetToChar", TestDateTimeOffset, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTimeOffset, DateTime>("DateTimeOffsetToDateTime", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTimeOffset, DateTimeOffset>("DateTimeOffsetToDateTimeOffset", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTimeOffset, decimal>("DateTimeOffsetToDecimal", TestDateTimeOffset, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTimeOffset, double>("DateTimeOffsetToDouble", TestDateTimeOffset, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTimeOffset, PrimaryColor>("DateTimeOffsetToEnum", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTimeOffset, float>("DateTimeOffsetToFloat", TestDateTimeOffset, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTimeOffset, Guid>("DateTimeOffsetToGuid", TestDateTimeOffset, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTimeOffset, int>("DateTimeOffsetToInt", TestDateTimeOffset, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTimeOffset, long>("DateTimeOffsetToLong", TestDateTimeOffset, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTimeOffset, sbyte>("DateTimeOffsetToSByte", TestDateTimeOffset, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTimeOffset, short>("DateTimeOffsetToShort", TestDateTimeOffset, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToString", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetString),
                            new TryConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToStringWithFormat", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormat, FormatDateTimeOffsetContext),
                            new TryConvertGenericTest<DateTimeOffset, string>("DateTimeOffsetToStringWithFormatAndFormatProvider", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeOffsetContext),
                            new TryConvertGenericTest<DateTimeOffset, TimeSpan>("DateTimeOffsetToTimeSpan", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTimeOffset, Type>("DateTimeOffsetToType", TestDateTimeOffset, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTimeOffset, uint>("DateTimeOffsetToUInt", TestDateTimeOffset, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTimeOffset, ulong>("DateTimeOffsetToULong", TestDateTimeOffset, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTimeOffset, Uri>("DateTimeOffsetToUri", TestDateTimeOffset, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTimeOffset, ushort>("DateTimeOffsetToUShort", TestDateTimeOffset, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DateTimeOffset, bool?>("DateTimeOffsetToNullable<Bool>", TestDateTimeOffset, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTimeOffset, byte?>("DateTimeOffsetToNullable<Byte>", TestDateTimeOffset, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTimeOffset, char?>("DateTimeOffsetToNullable<Char>", TestDateTimeOffset, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTimeOffset, DateTime?>("DateTimeOffsetToNullable<DateTime>", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTimeOffset, DateTimeOffset?>("DateTimeOffsetToNullable<DateTimeOffset>", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTimeOffset, decimal?>("DateTimeOffsetToNullable<Decimal>", TestDateTimeOffset, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTimeOffset, double?>("DateTimeOffsetToNullable<Double>", TestDateTimeOffset, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTimeOffset, PrimaryColor?>("DateTimeOffsetToNullable<Enum>", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTimeOffset, float?>("DateTimeOffsetToNullable<Float>", TestDateTimeOffset, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTimeOffset, Guid?>("DateTimeOffsetToNullable<Guid>", TestDateTimeOffset, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<DateTimeOffset, int?>("DateTimeOffsetToNullable<Int>", TestDateTimeOffset, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTimeOffset, long?>("DateTimeOffsetToNullable<Long>", TestDateTimeOffset, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTimeOffset, sbyte?>("DateTimeOffsetToNullable<SByte>", TestDateTimeOffset, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTimeOffset, short?>("DateTimeOffsetToNullable<Short>", TestDateTimeOffset, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTimeOffset, TimeSpan?>("DateTimeOffsetToNullable<TimeSpan>", TestDateTimeOffset, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<DateTimeOffset, uint?>("DateTimeOffsetToNullable<UInt>", TestDateTimeOffset, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTimeOffset, ulong?>("DateTimeOffsetToNullable<ULong>", TestDateTimeOffset, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTimeOffset, ushort?>("DateTimeOffsetToNullable<UShort>", TestDateTimeOffset, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DateTimeOffset, IInterface>("DateTimeOffsetToInterface", TestDateTimeOffset, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTimeOffset, BaseClass>("DateTimeOffsetToBaseClass", TestDateTimeOffset, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTimeOffset, DerivedClass>("DateTimeOffsetToDerivedClass", TestDateTimeOffset, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DecimalToXXX
                new object []
                {
                    "DecimalToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<decimal, bool>("DecimalToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<decimal, byte>("DecimalToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, byte[]>("DecimalToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<decimal, char>("DecimalToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<decimal, DateTime>("DecimalToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<decimal, DateTimeOffset>("DecimalToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<decimal, decimal>("DecimalToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, double>("DecimalToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, PrimaryColor>("DecimalToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<decimal, float>("DecimalToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, Guid>("DecimalToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<decimal, int>("DecimalToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, long>("DecimalToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, sbyte>("DecimalToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, short>("DecimalToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, string>("DecimalToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<decimal, TimeSpan>("DecimalToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<decimal, Type>("DecimalToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<decimal, uint>("DecimalToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, ulong>("DecimalToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, Uri>("DecimalToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<decimal, ushort>("DecimalToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<decimal, bool?>("DecimalToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<decimal, byte?>("DecimalToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, char?>("DecimalToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<decimal, DateTime?>("DecimalToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<decimal, DateTimeOffset?>("DecimalToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<decimal, decimal?>("DecimalToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, double?>("DecimalToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, PrimaryColor?>("DecimalToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<decimal, float?>("DecimalToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, Guid?>("DecimalToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<decimal, int?>("DecimalToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, long?>("DecimalToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, sbyte?>("DecimalToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, short?>("DecimalToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, TimeSpan?>("DecimalToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<decimal, uint?>("DecimalToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, ulong?>("DecimalToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal, ushort?>("DecimalToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<decimal, IInterface>("DecimalToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<decimal, BaseClass>("DecimalToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<decimal, DerivedClass>("DecimalToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DoubleToXXX
                new object []
                {
                    "DoubleToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<double, bool>("DoubleToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<double, byte>("DoubleToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, byte[]>("DoubleToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<double, char>("DoubleToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<double, DateTime>("DoubleToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<double, DateTimeOffset>("DoubleToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<double, decimal>("DoubleToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, double>("DoubleToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, PrimaryColor>("DoubleToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<double, float>("DoubleToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, Guid>("DoubleToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<double, int>("DoubleToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, long>("DoubleToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, sbyte>("DoubleToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, short>("DoubleToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, string>("DoubleToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<double, TimeSpan>("DoubleToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<double, Type>("DoubleToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<double, uint>("DoubleToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, ulong>("DoubleToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, Uri>("DoubleToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<double, ushort>("DoubleToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<double, bool?>("DoubleToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<double, byte?>("DoubleToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, char?>("DoubleToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<double, DateTime?>("DoubleToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<double, DateTimeOffset?>("DoubleToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<double, decimal?>("DoubleToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, double?>("DoubleToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, PrimaryColor?>("DoubleToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<double, float?>("DoubleToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, Guid?>("DoubleToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<double, int?>("DoubleToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, long?>("DoubleToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, sbyte?>("DoubleToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, short?>("DoubleToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, TimeSpan?>("DoubleToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<double, uint?>("DoubleToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, ulong?>("DoubleToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double, ushort?>("DoubleToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<double, IInterface>("DoubleToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<double, BaseClass>("DoubleToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<double, DerivedClass>("DoubleToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region EnumToXXX
                new object []
                {
                    "EnumToXXX - Generic",
                    new object []
                        {
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
                            new TryConvertGenericTest<PrimaryColor, DerivedClass>("EnumToDerivedClass", PrimaryColor.Blue, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region FloatToXXX
                new object []
                {
                    "FloatToXXX - Generic",
                    new object []
                        {
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
                            new TryConvertGenericTest<float, DerivedClass>("FloatToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region GuidToXXX
                new object []
                {
                    "GuidToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<Guid, bool>("GuidToBool", TestGuid, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Guid, byte>("GuidToByte", TestGuid, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Guid, byte[]>("GuidToByteArray", TestGuid, ConvertResult.Success, TestGuidByteArray),
                            new TryConvertGenericTest<Guid, char>("GuidToChar", TestGuid, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Guid, DateTime>("GuidToDateTime", TestGuid, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Guid, DateTimeOffset>("GuidToDateTimeOffset", TestGuid, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Guid, decimal>("GuidToDecimal", TestGuid, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Guid, double>("GuidToDouble", TestGuid, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Guid, PrimaryColor>("GuidToEnum", TestGuid, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Guid, float>("GuidToFloat", TestGuid, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Guid, Guid>("GuidToGuid", TestGuid, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<Guid, int>("GuidToInt", TestGuid, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Guid, long>("GuidToLong", TestGuid, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Guid, sbyte>("GuidToSByte", TestGuid, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Guid, short>("GuidToShort", TestGuid, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Guid, string>("GuidToString", TestGuid, ConvertResult.Success, TestGuidString),
                            new TryConvertGenericTest<Guid, TimeSpan>("GuidToTimeSpan", TestGuid, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Guid, Type>("GuidToType", TestGuid, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Guid, uint>("GuidToUInt", TestGuid, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Guid, ulong>("GuidToULong", TestGuid, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Guid, Uri>("GuidToUri", TestGuid, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Guid, ushort>("GuidToUShort", TestGuid, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<Guid, bool?>("GuidToNullable<Bool>", TestGuid, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Guid, byte?>("GuidToNullable<Byte>", TestGuid, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Guid, char?>("GuidToNullable<Char>", TestGuid, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Guid, DateTime?>("GuidToNullable<DateTime>", TestGuid, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Guid, DateTimeOffset?>("GuidToNullable<DateTimeOffset>", TestGuid, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Guid, decimal?>("GuidToNullable<Decimal>", TestGuid, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Guid, double?>("GuidToNullable<Double>", TestGuid, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Guid, PrimaryColor?>("GuidToNullable<Enum>", TestGuid, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Guid, float?>("GuidToNullable<Float>", TestGuid, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Guid, Guid?>("GuidToNullable<Guid>", TestGuid, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<Guid, int?>("GuidToNullable<Int>", TestGuid, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Guid, long?>("GuidToNullable<Long>", TestGuid, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Guid, sbyte?>("GuidToNullable<SByte>", TestGuid, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Guid, short?>("GuidToNullable<Short>", TestGuid, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Guid, TimeSpan?>("GuidToNullable<TimeSpan>", TestGuid, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Guid, uint?>("GuidToNullable<UInt>", TestGuid, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Guid, ulong?>("GuidToNullable<ULong>", TestGuid, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Guid, ushort?>("GuidToNullable<UShort>", TestGuid, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<Guid, IInterface>("GuidToInterface", TestGuid, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Guid, BaseClass>("GuidToBaseClass", TestGuid, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Guid, DerivedClass>("GuidToDerivedClass", TestGuid, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region IntToXXX
                new object []
                {
                    "IntToXXX - Generic",
                    new object []
                        {
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
                            new TryConvertGenericTest<int, DerivedClass>("IntToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region LongToXXX
                new object []
                {
                    "LongToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<long, bool>("LongToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<long, byte>("LongToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, byte[]>("LongToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<long, char>("LongToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<long, DateTime>("LongToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<long, DateTimeOffset>("LongToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<long, decimal>("LongToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, double>("LongToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, PrimaryColor>("LongToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<long, float>("LongToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, Guid>("LongToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<long, int>("LongToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, long>("LongToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, sbyte>("LongToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, short>("LongToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, string>("LongToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<long, TimeSpan>("LongToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<long, Type>("LongToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<long, uint>("LongToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, ulong>("LongToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, Uri>("LongToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<long, ushort>("LongToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<long, bool?>("LongToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<long, byte?>("LongToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, char?>("LongToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<long, DateTime?>("LongToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<long, DateTimeOffset?>("LongToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<long, decimal?>("LongToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, double?>("LongToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, PrimaryColor?>("LongToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<long, float?>("LongToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, Guid?>("LongToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<long, int?>("LongToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, long?>("LongToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, sbyte?>("LongToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, short?>("LongToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, TimeSpan?>("LongToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<long, uint?>("LongToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, ulong?>("LongToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long, ushort?>("LongToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<long, IInterface>("LongToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<long, BaseClass>("LongToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<long, DerivedClass>("LongToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region SByteToXXX
                new object []
                {
                    "SByteToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<sbyte, bool>("SByteToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<sbyte, byte>("SByteToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, byte[]>("SByteToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<sbyte, char>("SByteToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<sbyte, DateTime>("SByteToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<sbyte, DateTimeOffset>("SByteToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<sbyte, decimal>("SByteToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, double>("SByteToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, PrimaryColor>("SByteToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<sbyte, float>("SByteToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, Guid>("SByteToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<sbyte, int>("SByteToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, long>("SByteToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, sbyte>("SByteToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, short>("SByteToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, string>("SByteToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<sbyte, TimeSpan>("SByteToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<sbyte, Type>("SByteToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<sbyte, uint>("SByteToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, ulong>("SByteToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, Uri>("SByteToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<sbyte, ushort>("SByteToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<sbyte, bool?>("SByteToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<sbyte, byte?>("SByteToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, char?>("SByteToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<sbyte, DateTime?>("SByteToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<sbyte, DateTimeOffset?>("SByteToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<sbyte, decimal?>("SByteToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, double?>("SByteToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, PrimaryColor?>("SByteToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<sbyte, float?>("SByteToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, Guid?>("SByteToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<sbyte, int?>("SByteToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, long?>("SByteToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, sbyte?>("SByteToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, short?>("SByteToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, TimeSpan?>("SByteToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<sbyte, uint?>("SByteToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, ulong?>("SByteToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte, ushort?>("SByteToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<sbyte, IInterface>("SByteToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<sbyte, BaseClass>("SByteToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<sbyte, DerivedClass>("SByteToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region ShortToXXX
                new object []
                {
                    "ShortToXXX - Generic",
                    new object []
                        {
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
                            new TryConvertGenericTest<short, DerivedClass>("ShortToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region StringToXXX
                new object []
                {
                    "StringToXXX - Generic",
                    new object []
                        {
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
                            new TryConvertGenericTest<string, DerivedClass>("StringToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region TimeSpanToXXX
                new object []
                {
                    "TimeSpanToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<TimeSpan, bool>("TimeSpanToBool", TestTimeSpan, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<TimeSpan, byte>("TimeSpanToByte", TestTimeSpan, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<TimeSpan, byte[]>("TimeSpanToByteArray", TestTimeSpan, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<TimeSpan, char>("TimeSpanToChar", TestTimeSpan, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<TimeSpan, DateTime>("TimeSpanToDateTime", TestTimeSpan, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<TimeSpan, DateTimeOffset>("TimeSpanToDateTimeOffset", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<TimeSpan, decimal>("TimeSpanToDecimal", TestTimeSpan, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<TimeSpan, double>("TimeSpanToDouble", TestTimeSpan, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<TimeSpan, PrimaryColor>("TimeSpanToEnum", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<TimeSpan, float>("TimeSpanToFloat", TestTimeSpan, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<TimeSpan, Guid>("TimeSpanToGuid", TestTimeSpan, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<TimeSpan, int>("TimeSpanToInt", TestTimeSpan, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<TimeSpan, long>("TimeSpanToLong", TestTimeSpan, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<TimeSpan, sbyte>("TimeSpanToSByte", TestTimeSpan, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<TimeSpan, short>("TimeSpanToShort", TestTimeSpan, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<TimeSpan, string>("TimeSpanToString", TestTimeSpan, ConvertResult.Success, TestTimeSpanString),
                            new TryConvertGenericTest<TimeSpan, string>("TimeSpanToStringWithFormat", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormat, FormatTimeSpanContext),
                            new TryConvertGenericTest<TimeSpan, string>("TimeSpanToStringWithFormatAndFormatProvider", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormatAndFormatProvider, FormatAndFormatProviderTimeSpanContext),
                            new TryConvertGenericTest<TimeSpan, TimeSpan>("TimeSpanToTimeSpan", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<TimeSpan, Type>("TimeSpanToType", TestTimeSpan, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<TimeSpan, uint>("TimeSpanToUInt", TestTimeSpan, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<TimeSpan, ulong>("TimeSpanToULong", TestTimeSpan, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<TimeSpan, Uri>("TimeSpanToUri", TestTimeSpan, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<TimeSpan, ushort>("TimeSpanToUShort", TestTimeSpan, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<TimeSpan, bool?>("TimeSpanToNullable<Bool>", TestTimeSpan, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<TimeSpan, byte?>("TimeSpanToNullable<Byte>", TestTimeSpan, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<TimeSpan, char?>("TimeSpanToNullable<Char>", TestTimeSpan, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<TimeSpan, DateTime?>("TimeSpanToNullable<DateTime>", TestTimeSpan, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<TimeSpan, DateTimeOffset?>("TimeSpanToNullable<DateTimeOffset>", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<TimeSpan, decimal?>("TimeSpanToNullable<Decimal>", TestTimeSpan, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<TimeSpan, double?>("TimeSpanToNullable<Double>", TestTimeSpan, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<TimeSpan, PrimaryColor?>("TimeSpanToNullable<Enum>", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<TimeSpan, float?>("TimeSpanToNullable<Float>", TestTimeSpan, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<TimeSpan, Guid?>("TimeSpanToNullable<Guid>", TestTimeSpan, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<TimeSpan, int?>("TimeSpanToNullable<Int>", TestTimeSpan, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<TimeSpan, long?>("TimeSpanToNullable<Long>", TestTimeSpan, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<TimeSpan, sbyte?>("TimeSpanToNullable<SByte>", TestTimeSpan, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<TimeSpan, short?>("TimeSpanToNullable<Short>", TestTimeSpan, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<TimeSpan, TimeSpan?>("TimeSpanToNullable<TimeSpan>", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<TimeSpan, uint?>("TimeSpanToNullable<UInt>", TestTimeSpan, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<TimeSpan, ulong?>("TimeSpanToNullable<ULong>", TestTimeSpan, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<TimeSpan, ushort?>("TimeSpanToNullable<UShort>", TestTimeSpan, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<TimeSpan, IInterface>("TimeSpanToInterface", TestTimeSpan, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<TimeSpan, BaseClass>("TimeSpanToBaseClass", TestTimeSpan, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<TimeSpan, DerivedClass>("TimeSpanToDerivedClass", TestTimeSpan, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region TypeToXXX
                new object []
                {
                    "TypeToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<Type, bool>("TypeToBool", TestType, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Type, bool>("TypeToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Type, byte>("TypeToByte", TestType, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Type, byte>("TypeToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Type, byte[]>("TypeToByteArray", TestType, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<Type, byte[]>("TypeToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<Type, char>("TypeToChar", TestType, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Type, char>("TypeToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Type, DateTime>("TypeToDateTime", TestType, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Type, DateTime>("TypeToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Type, DateTimeOffset>("TypeToDateTimeOffset", TestType, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Type, DateTimeOffset>("TypeToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Type, decimal>("TypeToDecimal", TestType, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Type, decimal>("TypeToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Type, double>("TypeToDouble", TestType, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Type, double>("TypeToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Type, PrimaryColor>("TypeToEnum", TestType, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Type, PrimaryColor>("TypeToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Type, float>("TypeToFloat", TestType, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Type, float>("TypeToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Type, Guid>("TypeToGuid", TestType, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<Type, Guid>("TypeToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<Type, int>("TypeToInt", TestType, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Type, int>("TypeToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Type, long>("TypeToLong", TestType, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Type, long>("TypeToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Type, sbyte>("TypeToSByte", TestType, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Type, sbyte>("TypeToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Type, short>("TypeToShort", TestType, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Type, short>("TypeToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Type, string>("TypeToString", TestType, ConvertResult.Success, TestTypeString),
                            new TryConvertGenericTest<Type, string>("TypeToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<Type, TimeSpan>("TypeToTimeSpan", TestType, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Type, TimeSpan>("TypeToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Type, Type>("TypeToType", TestType, ConvertResult.Success, TestType),
                            new TryConvertGenericTest<Type, Type>("TypeToType", null, ConvertResult.Success, default(Type)),
                            new TryConvertGenericTest<Type, uint>("TypeToUInt", TestType, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Type, uint>("TypeToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Type, ulong>("TypeToULong", TestType, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Type, ulong>("TypeToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Type, Uri>("TypeToUri", TestType, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Type, Uri>("TypeToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Type, ushort>("TypeToUShort", TestType, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<Type, ushort>("TypeToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<Type, bool?>("TypeToNullable<Bool>", TestType, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Type, bool?>("TypeToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Type, byte?>("TypeToNullable<Byte>", TestType, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Type, byte?>("TypeToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Type, char?>("TypeToNullable<Char>", TestType, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Type, char?>("TypeToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Type, DateTime?>("TypeToNullable<DateTime>", TestType, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Type, DateTime?>("TypeToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Type, DateTimeOffset?>("TypeToNullable<DateTimeOffset>", TestType, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Type, DateTimeOffset?>("TypeToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Type, decimal?>("TypeToNullable<Decimal>", TestType, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Type, decimal?>("TypeToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Type, double?>("TypeToNullable<Double>", TestType, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Type, double?>("TypeToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Type, PrimaryColor?>("TypeToNullable<Enum>", TestType, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Type, PrimaryColor?>("TypeToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Type, float?>("TypeToNullable<Float>", TestType, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Type, float?>("TypeToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Type, Guid?>("TypeToNullable<Guid>", TestType, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<Type, Guid?>("TypeToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<Type, int?>("TypeToNullable<Int>", TestType, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Type, int?>("TypeToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Type, long?>("TypeToNullable<Long>", TestType, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Type, long?>("TypeToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Type, sbyte?>("TypeToNullable<SByte>", TestType, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Type, sbyte?>("TypeToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Type, short?>("TypeToNullable<Short>", TestType, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Type, short?>("TypeToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Type, TimeSpan?>("TypeToNullable<TimeSpan>", TestType, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Type, TimeSpan?>("TypeToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Type, uint?>("TypeToNullable<UInt>", TestType, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Type, uint?>("TypeToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Type, ulong?>("TypeToNullable<ULong>", TestType, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Type, ulong?>("TypeToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Type, ushort?>("TypeToNullable<UShort>", TestType, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<Type, ushort?>("TypeToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<Type, IInterface>("TypeToInterface", TestType, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Type, IInterface>("TypeToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Type, BaseClass>("TypeToBaseClass", TestType, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Type, BaseClass>("TypeToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Type, DerivedClass>("TypeToDerivedClass", TestType, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<Type, DerivedClass>("TypeToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region UIntToXXX
                new object []
                {
                    "UIntToXXX - Generic",
                    new object []
                        {
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
                            new TryConvertGenericTest<uint, DerivedClass>("UIntToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region ULongToXXX
                new object []
                {
                    "ULongToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<ulong, bool>("ULongToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ulong, byte>("ULongToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, byte[]>("ULongToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ulong, char>("ULongToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ulong, DateTime>("ULongToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ulong, DateTimeOffset>("ULongToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ulong, decimal>("ULongToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, double>("ULongToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, PrimaryColor>("ULongToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ulong, float>("ULongToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, Guid>("ULongToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ulong, int>("ULongToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, long>("ULongToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, sbyte>("ULongToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, short>("ULongToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, string>("ULongToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<ulong, TimeSpan>("ULongToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ulong, Type>("ULongToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ulong, uint>("ULongToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, ulong>("ULongToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, Uri>("ULongToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ulong, ushort>("ULongToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<ulong, bool?>("ULongToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ulong, byte?>("ULongToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, char?>("ULongToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ulong, DateTime?>("ULongToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ulong, DateTimeOffset?>("ULongToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ulong, decimal?>("ULongToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, double?>("ULongToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, PrimaryColor?>("ULongToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ulong, float?>("ULongToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, Guid?>("ULongToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ulong, int?>("ULongToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, long?>("ULongToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, sbyte?>("ULongToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, short?>("ULongToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, TimeSpan?>("ULongToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ulong, uint?>("ULongToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, ulong?>("ULongToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong, ushort?>("ULongToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<ulong, IInterface>("ULongToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ulong, BaseClass>("ULongToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ulong, DerivedClass>("ULongToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region UriToXXX
                new object []
                {
                    "UriToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<Uri, bool>("UriToBool", TestUri, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Uri, bool>("UriToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Uri, byte>("UriToByte", TestUri, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Uri, byte>("UriToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Uri, byte[]>("UriToByteArray", TestUri, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<Uri, byte[]>("UriToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<Uri, char>("UriToChar", TestUri, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Uri, char>("UriToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Uri, DateTime>("UriToDateTime", TestUri, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Uri, DateTime>("UriToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Uri, DateTimeOffset>("UriToDateTimeOffset", TestUri, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Uri, DateTimeOffset>("UriToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Uri, decimal>("UriToDecimal", TestUri, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Uri, decimal>("UriToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Uri, double>("UriToDouble", TestUri, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Uri, double>("UriToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Uri, PrimaryColor>("UriToEnum", TestUri, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Uri, PrimaryColor>("UriToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Uri, float>("UriToFloat", TestUri, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Uri, float>("UriToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Uri, Guid>("UriToGuid", TestUri, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<Uri, Guid>("UriToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<Uri, int>("UriToInt", TestUri, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Uri, int>("UriToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Uri, long>("UriToLong", TestUri, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Uri, long>("UriToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Uri, sbyte>("UriToSByte", TestUri, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Uri, sbyte>("UriToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Uri, short>("UriToShort", TestUri, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Uri, short>("UriToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Uri, string>("UriToString", TestUri, ConvertResult.Success, TestUriString),
                            new TryConvertGenericTest<Uri, string>("UriToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<Uri, TimeSpan>("UriToTimeSpan", TestUri, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Uri, TimeSpan>("UriToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Uri, Type>("UriToType", TestUri, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Uri, Type>("UriToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Uri, uint>("UriToUInt", TestUri, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Uri, uint>("UriToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Uri, ulong>("UriToULong", TestUri, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Uri, ulong>("UriToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Uri, Uri>("UriToUri", TestUri, ConvertResult.Success, TestUri),
                            new TryConvertGenericTest<Uri, Uri>("UriToUri", null, ConvertResult.Success, default(Uri)),
                            new TryConvertGenericTest<Uri, ushort>("UriToUShort", TestUri, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<Uri, ushort>("UriToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<Uri, bool?>("UriToNullable<Bool>", TestUri, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Uri, bool?>("UriToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Uri, byte?>("UriToNullable<Byte>", TestUri, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Uri, byte?>("UriToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Uri, char?>("UriToNullable<Char>", TestUri, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Uri, char?>("UriToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Uri, DateTime?>("UriToNullable<DateTime>", TestUri, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Uri, DateTime?>("UriToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<Uri, DateTimeOffset?>("UriToNullable<DateTimeOffset>", TestUri, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Uri, DateTimeOffset?>("UriToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<Uri, decimal?>("UriToNullable<Decimal>", TestUri, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Uri, decimal?>("UriToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Uri, double?>("UriToNullable<Double>", TestUri, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Uri, double?>("UriToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Uri, PrimaryColor?>("UriToNullable<Enum>", TestUri, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Uri, PrimaryColor?>("UriToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Uri, float?>("UriToNullable<Float>", TestUri, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Uri, float?>("UriToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Uri, Guid?>("UriToNullable<Guid>", TestUri, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<Uri, Guid?>("UriToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<Uri, int?>("UriToNullable<Int>", TestUri, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Uri, int?>("UriToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Uri, long?>("UriToNullable<Long>", TestUri, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Uri, long?>("UriToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Uri, sbyte?>("UriToNullable<SByte>", TestUri, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Uri, sbyte?>("UriToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Uri, short?>("UriToNullable<Short>", TestUri, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Uri, short?>("UriToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Uri, TimeSpan?>("UriToNullable<TimeSpan>", TestUri, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Uri, TimeSpan?>("UriToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<Uri, uint?>("UriToNullable<UInt>", TestUri, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Uri, uint?>("UriToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Uri, ulong?>("UriToNullable<ULong>", TestUri, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Uri, ulong?>("UriToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Uri, ushort?>("UriToNullable<UShort>", TestUri, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<Uri, ushort?>("UriToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<Uri, IInterface>("UriToInterface", TestUri, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Uri, IInterface>("UriToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Uri, BaseClass>("UriToBaseClass", TestUri, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Uri, BaseClass>("UriToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Uri, DerivedClass>("UriToDerivedClass", TestUri, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<Uri, DerivedClass>("UriToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region UShortToXXX
                new object []
                {
                    "UShortToXXX - Generic",
                    new object []
                        {
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
                            new TryConvertGenericTest<ushort, DerivedClass>("UShortToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                // Nullable Types ///////////////////////////////////////////

                #region Nullable<Bool>ToXXX
                new object []
                {
                    "Nullable<Bool>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<bool?, bool>("Nullable<Bool>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<bool?, bool>("Nullable<Bool>ToBool", true, ConvertResult.Success, true),
                            new TryConvertGenericTest<bool?, byte>("Nullable<Bool>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<bool?, byte>("Nullable<Bool>ToByte", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, byte[]>("Nullable<Bool>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<bool?, byte[]>("Nullable<Bool>ToByteArray", true, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<bool?, char>("Nullable<Bool>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<bool?, char>("Nullable<Bool>ToChar", true, ConvertResult.Success, (char)1),
                            new TryConvertGenericTest<bool?, DateTime>("Nullable<Bool>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<bool?, DateTime>("Nullable<Bool>ToDateTime", true, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<bool?, DateTimeOffset>("Nullable<Bool>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<bool?, DateTimeOffset>("Nullable<Bool>ToDateTimeOffset", true, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<bool?, decimal>("Nullable<Bool>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<bool?, decimal>("Nullable<Bool>ToDecimal", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, double>("Nullable<Bool>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<bool?, double>("Nullable<Bool>ToDouble", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, PrimaryColor>("Nullable<Bool>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<bool?, PrimaryColor>("Nullable<Bool>ToEnum", true, ConvertResult.Success, PrimaryColor.Green),
                            new TryConvertGenericTest<bool?, float>("Nullable<Bool>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<bool?, float>("Nullable<Bool>ToFloat", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, Guid>("Nullable<Bool>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<bool?, Guid>("Nullable<Bool>ToGuid", true, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<bool?, int>("Nullable<Bool>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<bool?, int>("Nullable<Bool>ToInt", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, long>("Nullable<Bool>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<bool?, long>("Nullable<Bool>ToLong", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, sbyte>("Nullable<Bool>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<bool?, sbyte>("Nullable<Bool>ToSByte", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, short>("Nullable<Bool>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<bool?, short>("Nullable<Bool>ToShort", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, string>("Nullable<Bool>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<bool?, string>("Nullable<Bool>ToString", true, ConvertResult.Success, "True"),
                            new TryConvertGenericTest<bool?, TimeSpan>("Nullable<Bool>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<bool?, TimeSpan>("Nullable<Bool>ToTimeSpan", true, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<bool?, Type>("Nullable<Bool>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<bool?, Type>("Nullable<Bool>ToType", true, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<bool?, uint>("Nullable<Bool>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<bool?, uint>("Nullable<Bool>ToUInt", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, ulong>("Nullable<Bool>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<bool?, ulong>("Nullable<Bool>ToULong", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, Uri>("Nullable<Bool>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<bool?, Uri>("Nullable<Bool>ToUri", true, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<bool?, ushort>("Nullable<Bool>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<bool?, ushort>("Nullable<Bool>ToUShort", true, ConvertResult.Success, 1),

                            // Nullable Types
                            new TryConvertGenericTest<bool?, bool?>("Nullable<Bool>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<bool?, bool?>("Nullable<Bool>ToNullable<Bool>", true, ConvertResult.Success, true),
                            new TryConvertGenericTest<bool?, byte?>("Nullable<Bool>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<bool?, byte?>("Nullable<Bool>ToNullable<Byte>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, char?>("Nullable<Bool>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<bool?, char?>("Nullable<Bool>ToNullable<Char>", true, ConvertResult.Success, (char)1),
                            new TryConvertGenericTest<bool?, DateTime?>("Nullable<Bool>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<bool?, DateTime?>("Nullable<Bool>ToNullable<DateTime>", true, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<bool?, DateTimeOffset?>("Nullable<Bool>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<bool?, DateTimeOffset?>("Nullable<Bool>ToNullable<DateTimeOffset>", true, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<bool?, decimal?>("Nullable<Bool>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<bool?, decimal?>("Nullable<Bool>ToNullable<Decimal>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, double?>("Nullable<Bool>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<bool?, double?>("Nullable<Bool>ToNullable<Double>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, PrimaryColor?>("Nullable<Bool>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<bool?, PrimaryColor?>("Nullable<Bool>ToNullable<Enum>", true, ConvertResult.Success, PrimaryColor.Green),
                            new TryConvertGenericTest<bool?, float?>("Nullable<Bool>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<bool?, float?>("Nullable<Bool>ToNullable<Float>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, Guid?>("Nullable<Bool>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<bool?, Guid?>("Nullable<Bool>ToNullable<Guid>", true, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<bool?, int?>("Nullable<Bool>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<bool?, int?>("Nullable<Bool>ToNullable<Int>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, long?>("Nullable<Bool>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<bool?, long?>("Nullable<Bool>ToNullable<Long>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, sbyte?>("Nullable<Bool>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<bool?, sbyte?>("Nullable<Bool>ToNullable<SByte>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, short?>("Nullable<Bool>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<bool?, short?>("Nullable<Bool>ToNullable<Short>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, TimeSpan?>("Nullable<Bool>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<bool?, TimeSpan?>("Nullable<Bool>ToNullable<TimeSpan>", true, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<bool?, uint?>("Nullable<Bool>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<bool?, uint?>("Nullable<Bool>ToNullable<UInt>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, ulong?>("Nullable<Bool>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<bool?, ulong?>("Nullable<Bool>ToNullable<ULong>", true, ConvertResult.Success, 1),
                            new TryConvertGenericTest<bool?, ushort?>("Nullable<Bool>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<bool?, ushort?>("Nullable<Bool>ToNullable<UShort>", true, ConvertResult.Success, 1),

                            // Interface/Class Types
                            new TryConvertGenericTest<bool?, IInterface>("Nullable<Bool>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<bool?, IInterface>("Nullable<Bool>ToInterface", true, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<bool?, BaseClass>("Nullable<Bool>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<bool?, BaseClass>("Nullable<Bool>ToBaseClass", true, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<bool?, DerivedClass>("Nullable<Bool>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<bool?, DerivedClass>("Nullable<Bool>ToDerivedClass", true, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Byte>ToXXX
                new object []
                {
                    "Nullable<Byte>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<byte?, bool>("Nullable<Byte>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<byte?, bool>("Nullable<Byte>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<byte?, byte>("Nullable<Byte>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<byte?, byte>("Nullable<Byte>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, byte[]>("Nullable<Byte>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<byte?, byte[]>("Nullable<Byte>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<byte?, char>("Nullable<Byte>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<byte?, char>("Nullable<Byte>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<byte?, DateTime>("Nullable<Byte>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte?, DateTime>("Nullable<Byte>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<byte?, DateTimeOffset>("Nullable<Byte>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<byte?, DateTimeOffset>("Nullable<Byte>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<byte?, decimal>("Nullable<Byte>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<byte?, decimal>("Nullable<Byte>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, double>("Nullable<Byte>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<byte?, double>("Nullable<Byte>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, PrimaryColor>("Nullable<Byte>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<byte?, PrimaryColor>("Nullable<Byte>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<byte?, float>("Nullable<Byte>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<byte?, float>("Nullable<Byte>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, Guid>("Nullable<Byte>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<byte?, Guid>("Nullable<Byte>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<byte?, int>("Nullable<Byte>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<byte?, int>("Nullable<Byte>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, long>("Nullable<Byte>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<byte?, long>("Nullable<Byte>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, sbyte>("Nullable<Byte>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<byte?, sbyte>("Nullable<Byte>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, short>("Nullable<Byte>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<byte?, short>("Nullable<Byte>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, string>("Nullable<Byte>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<byte?, string>("Nullable<Byte>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<byte?, TimeSpan>("Nullable<Byte>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<byte?, TimeSpan>("Nullable<Byte>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<byte?, Type>("Nullable<Byte>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<byte?, Type>("Nullable<Byte>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<byte?, uint>("Nullable<Byte>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<byte?, uint>("Nullable<Byte>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, ulong>("Nullable<Byte>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<byte?, ulong>("Nullable<Byte>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, Uri>("Nullable<Byte>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<byte?, Uri>("Nullable<Byte>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<byte?, ushort>("Nullable<Byte>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<byte?, ushort>("Nullable<Byte>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<byte?, bool?>("Nullable<Byte>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<byte?, bool?>("Nullable<Byte>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<byte?, byte?>("Nullable<Byte>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<byte?, byte?>("Nullable<Byte>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, char?>("Nullable<Byte>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<byte?, char?>("Nullable<Byte>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<byte?, DateTime?>("Nullable<Byte>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<byte?, DateTime?>("Nullable<Byte>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<byte?, DateTimeOffset?>("Nullable<Byte>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<byte?, DateTimeOffset?>("Nullable<Byte>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<byte?, decimal?>("Nullable<Byte>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<byte?, decimal?>("Nullable<Byte>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, double?>("Nullable<Byte>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<byte?, double?>("Nullable<Byte>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, PrimaryColor?>("Nullable<Byte>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<byte?, PrimaryColor?>("Nullable<Byte>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<byte?, float?>("Nullable<Byte>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<byte?, float?>("Nullable<Byte>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, Guid?>("Nullable<Byte>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<byte?, Guid?>("Nullable<Byte>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<byte?, int?>("Nullable<Byte>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<byte?, int?>("Nullable<Byte>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, long?>("Nullable<Byte>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<byte?, long?>("Nullable<Byte>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, sbyte?>("Nullable<Byte>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<byte?, sbyte?>("Nullable<Byte>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, short?>("Nullable<Byte>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<byte?, short?>("Nullable<Byte>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, TimeSpan?>("Nullable<Byte>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<byte?, TimeSpan?>("Nullable<Byte>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<byte?, uint?>("Nullable<Byte>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<byte?, uint?>("Nullable<Byte>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, ulong?>("Nullable<Byte>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<byte?, ulong?>("Nullable<Byte>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<byte?, ushort?>("Nullable<Byte>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<byte?, ushort?>("Nullable<Byte>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<byte?, IInterface>("Nullable<Byte>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<byte?, IInterface>("Nullable<Byte>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<byte?, BaseClass>("Nullable<Byte>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<byte?, BaseClass>("Nullable<Byte>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<byte?, DerivedClass>("Nullable<Byte>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<byte?, DerivedClass>("Nullable<Byte>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Char>ToXXX
                new object []
                {
                    "Nullable<Char>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<char?, bool>("Nullable<Char>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<char?, bool>("Nullable<Char>ToBool", '*', ConvertResult.Success, true),
                            new TryConvertGenericTest<char?, byte>("Nullable<Char>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<char?, byte>("Nullable<Char>ToByte", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, byte[]>("Nullable<Char>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<char?, byte[]>("Nullable<Char>ToByteArray", '*', ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<char?, char>("Nullable<Char>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<char?, char>("Nullable<Char>ToChar", '*', ConvertResult.Success, '*'),
                            new TryConvertGenericTest<char?, DateTime>("Nullable<Char>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<char?, DateTime>("Nullable<Char>ToDateTime", '*', ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<char?, DateTimeOffset>("Nullable<Char>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<char?, DateTimeOffset>("Nullable<Char>ToDateTimeOffset", '*', ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<char?, decimal>("Nullable<Char>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<char?, decimal>("Nullable<Char>ToDecimal", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, double>("Nullable<Char>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<char?, double>("Nullable<Char>ToDouble", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, PrimaryColor>("Nullable<Char>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<char?, PrimaryColor>("Nullable<Char>ToEnum", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<char?, float>("Nullable<Char>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<char?, float>("Nullable<Char>ToFloat", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, Guid>("Nullable<Char>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<char?, Guid>("Nullable<Char>ToGuid", '*', ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<char?, int>("Nullable<Char>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<char?, int>("Nullable<Char>ToInt", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, long>("Nullable<Char>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<char?, long>("Nullable<Char>ToLong", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, sbyte>("Nullable<Char>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<char?, sbyte>("Nullable<Char>ToSByte", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, short>("Nullable<Char>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<char?, short>("Nullable<Char>ToShort", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, string>("Nullable<Char>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<char?, string>("Nullable<Char>ToString", '*', ConvertResult.Success, "*"),
                            new TryConvertGenericTest<char?, TimeSpan>("Nullable<Char>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<char?, TimeSpan>("Nullable<Char>ToTimeSpan", '*', ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<char?, Type>("Nullable<Char>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<char?, Type>("Nullable<Char>ToType", '*', ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<char?, uint>("Nullable<Char>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<char?, uint>("Nullable<Char>ToUInt", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, ulong>("Nullable<Char>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<char?, ulong>("Nullable<Char>ToULong", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, Uri>("Nullable<Char>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<char?, Uri>("Nullable<Char>ToUri", '*', ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<char?, ushort>("Nullable<Char>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<char?, ushort>("Nullable<Char>ToUShort", '*', ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<char?, bool?>("Nullable<Char>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<char?, bool?>("Nullable<Char>ToNullable<Bool>", '*', ConvertResult.Success, true),
                            new TryConvertGenericTest<char?, byte?>("Nullable<Char>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<char?, byte?>("Nullable<Char>ToNullable<Byte>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, char?>("Nullable<Char>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<char?, char?>("Nullable<Char>ToNullable<Char>", '*', ConvertResult.Success, '*'),
                            new TryConvertGenericTest<char?, DateTime?>("Nullable<Char>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<char?, DateTime?>("Nullable<Char>ToNullable<DateTime>", '*', ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<char?, DateTimeOffset?>("Nullable<Char>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<char?, DateTimeOffset?>("Nullable<Char>ToNullable<DateTimeOffset>", '*', ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<char?, decimal?>("Nullable<Char>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<char?, decimal?>("Nullable<Char>ToNullable<Decimal>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, double?>("Nullable<Char>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<char?, double?>("Nullable<Char>ToNullable<Double>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, PrimaryColor?>("Nullable<Char>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<char?, PrimaryColor?>("Nullable<Char>ToNullable<Enum>", '*', ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<char?, float?>("Nullable<Char>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<char?, float?>("Nullable<Char>ToNullable<Float>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, Guid?>("Nullable<Char>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<char?, Guid?>("Nullable<Char>ToNullable<Guid>", '*', ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<char?, int?>("Nullable<Char>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<char?, int?>("Nullable<Char>ToNullable<Int>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, long?>("Nullable<Char>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<char?, long?>("Nullable<Char>ToNullable<Long>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, sbyte?>("Nullable<Char>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<char?, sbyte?>("Nullable<Char>ToNullable<SByte>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, short?>("Nullable<Char>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<char?, short?>("Nullable<Char>ToNullable<Short>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, TimeSpan?>("Nullable<Char>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<char?, TimeSpan?>("Nullable<Char>ToNullable<TimeSpan>", '*', ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<char?, uint?>("Nullable<Char>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<char?, uint?>("Nullable<Char>ToNullable<UInt>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, ulong?>("Nullable<Char>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<char?, ulong?>("Nullable<Char>ToNullable<ULong>", '*', ConvertResult.Success, 42),
                            new TryConvertGenericTest<char?, ushort?>("Nullable<Char>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<char?, ushort?>("Nullable<Char>ToNullable<UShort>", '*', ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<char?, IInterface>("Nullable<Char>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<char?, IInterface>("Nullable<Char>ToInterface", '*', ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<char?, BaseClass>("Nullable<Char>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<char?, BaseClass>("Nullable<Char>ToBaseClass", '*', ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<char?, DerivedClass>("Nullable<Char>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<char?, DerivedClass>("Nullable<Char>ToDerivedClass", '*', ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<DateTime>ToXXX
                new object []
                {
                    "Nullable<DateTime>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<DateTime?, bool>("Nullable<DateTime>ToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTime?, bool>("Nullable<DateTime>ToBool", TestDateTime, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTime?, byte>("Nullable<DateTime>ToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTime?, byte>("Nullable<DateTime>ToByte", TestDateTime, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTime?, byte[]>("Nullable<DateTime>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTime?, byte[]>("Nullable<DateTime>ToByteArray", TestDateTime, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTime?, char>("Nullable<DateTime>ToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTime?, char>("Nullable<DateTime>ToChar", TestDateTime, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTime?, DateTime>("Nullable<DateTime>ToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new TryConvertGenericTest<DateTime?, DateTime>("Nullable<DateTime>ToDateTime", TestDateTime, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTime?, DateTimeOffset>("Nullable<DateTime>ToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new TryConvertGenericTest<DateTime?, DateTimeOffset>("Nullable<DateTime>ToDateTimeOffset", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTime?, decimal>("Nullable<DateTime>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTime?, decimal>("Nullable<DateTime>ToDecimal", TestDateTime, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTime?, double>("Nullable<DateTime>ToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTime?, double>("Nullable<DateTime>ToDouble", TestDateTime, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTime?, PrimaryColor>("Nullable<DateTime>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTime?, PrimaryColor>("Nullable<DateTime>ToEnum", TestDateTime, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTime?, float>("Nullable<DateTime>ToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTime?, float>("Nullable<DateTime>ToFloat", TestDateTime, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTime?, Guid>("Nullable<DateTime>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTime?, Guid>("Nullable<DateTime>ToGuid", TestDateTime, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTime?, int>("Nullable<DateTime>ToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTime?, int>("Nullable<DateTime>ToInt", TestDateTime, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTime?, long>("Nullable<DateTime>ToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTime?, long>("Nullable<DateTime>ToLong", TestDateTime, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTime?, sbyte>("Nullable<DateTime>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTime?, sbyte>("Nullable<DateTime>ToSByte", TestDateTime, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTime?, short>("Nullable<DateTime>ToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTime?, short>("Nullable<DateTime>ToShort", TestDateTime, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToString", TestDateTime, ConvertResult.Success, TestDateTimeString),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormat", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormat, FormatDateTimeContext),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTime?, string>("Nullable<DateTime>ToStringWithFormatAndFormatProvider", TestDateTime, ConvertResult.Success, TestDateTimeStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeContext),
                            new TryConvertGenericTest<DateTime?, TimeSpan>("Nullable<DateTime>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTime?, TimeSpan>("Nullable<DateTime>ToTimeSpan", TestDateTime, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTime?, Type>("Nullable<DateTime>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTime?, Type>("Nullable<DateTime>ToType", TestDateTime, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTime?, uint>("Nullable<DateTime>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTime?, uint>("Nullable<DateTime>ToUInt", TestDateTime, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTime?, ulong>("Nullable<DateTime>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTime?, ulong>("Nullable<DateTime>ToULong", TestDateTime, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTime?, Uri>("Nullable<DateTime>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTime?, Uri>("Nullable<DateTime>ToUri", TestDateTime, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTime?, ushort>("Nullable<DateTime>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<DateTime?, ushort>("Nullable<DateTime>ToUShort", TestDateTime, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DateTime?, bool?>("Nullable<DateTime>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTime?, bool?>("Nullable<DateTime>ToNullable<Bool>", TestDateTime, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTime?, byte?>("Nullable<DateTime>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTime?, byte?>("Nullable<DateTime>ToNullable<Byte>", TestDateTime, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTime?, char?>("Nullable<DateTime>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTime?, char?>("Nullable<DateTime>ToNullable<Char>", TestDateTime, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTime?, DateTime?>("Nullable<DateTime>ToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new TryConvertGenericTest<DateTime?, DateTime?>("Nullable<DateTime>ToNullable<DateTime>", TestDateTime, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTime?, DateTimeOffset?>("Nullable<DateTime>ToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new TryConvertGenericTest<DateTime?, DateTimeOffset?>("Nullable<DateTime>ToNullable<DateTimeOffset>", TestDateTime, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTime?, decimal?>("Nullable<DateTime>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTime?, decimal?>("Nullable<DateTime>ToNullable<Decimal>", TestDateTime, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTime?, double?>("Nullable<DateTime>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTime?, double?>("Nullable<DateTime>ToNullable<Double>", TestDateTime, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTime?, PrimaryColor?>("Nullable<DateTime>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTime?, PrimaryColor?>("Nullable<DateTime>ToNullable<Enum>", TestDateTime, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTime?, float?>("Nullable<DateTime>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTime?, float?>("Nullable<DateTime>ToNullable<Float>", TestDateTime, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTime?, Guid?>("Nullable<DateTime>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<DateTime?, Guid?>("Nullable<DateTime>ToNullable<Guid>", TestDateTime, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<DateTime?, int?>("Nullable<DateTime>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTime?, int?>("Nullable<DateTime>ToNullable<Int>", TestDateTime, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTime?, long?>("Nullable<DateTime>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTime?, long?>("Nullable<DateTime>ToNullable<Long>", TestDateTime, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTime?, sbyte?>("Nullable<DateTime>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTime?, sbyte?>("Nullable<DateTime>ToNullable<SByte>", TestDateTime, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTime?, short?>("Nullable<DateTime>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTime?, short?>("Nullable<DateTime>ToNullable<Short>", TestDateTime, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTime?, TimeSpan?>("Nullable<DateTime>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<DateTime?, TimeSpan?>("Nullable<DateTime>ToNullable<TimeSpan>", TestDateTime, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<DateTime?, uint?>("Nullable<DateTime>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTime?, uint?>("Nullable<DateTime>ToNullable<UInt>", TestDateTime, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTime?, ulong?>("Nullable<DateTime>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTime?, ulong?>("Nullable<DateTime>ToNullable<ULong>", TestDateTime, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTime?, ushort?>("Nullable<DateTime>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<DateTime?, ushort?>("Nullable<DateTime>ToNullable<UShort>", TestDateTime, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DateTime?, IInterface>("Nullable<DateTime>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTime?, IInterface>("Nullable<DateTime>ToInterface", TestDateTime, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTime?, BaseClass>("Nullable<DateTime>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTime?, BaseClass>("Nullable<DateTime>ToBaseClass", TestDateTime, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTime?, DerivedClass>("Nullable<DateTime>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<DateTime?, DerivedClass>("Nullable<DateTime>ToDerivedClass", TestDateTime, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<DateTimeOffset>ToXXX
                new object []
                {
                    "Nullable<DateTimeOffset>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<DateTimeOffset?, bool>("Nullable<DateTimeOffset>ToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTimeOffset?, bool>("Nullable<DateTimeOffset>ToBool", TestDateTimeOffset, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DateTimeOffset?, byte>("Nullable<DateTimeOffset>ToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTimeOffset?, byte>("Nullable<DateTimeOffset>ToByte", TestDateTimeOffset, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DateTimeOffset?, byte[]>("Nullable<DateTimeOffset>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTimeOffset?, byte[]>("Nullable<DateTimeOffset>ToByteArray", TestDateTimeOffset, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DateTimeOffset?, char>("Nullable<DateTimeOffset>ToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTimeOffset?, char>("Nullable<DateTimeOffset>ToChar", TestDateTimeOffset, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DateTimeOffset?, DateTime>("Nullable<DateTimeOffset>ToDateTime", null, ConvertResult.Success, default(DateTime)),
                            new TryConvertGenericTest<DateTimeOffset?, DateTime>("Nullable<DateTimeOffset>ToDateTime", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTimeOffset?, DateTimeOffset>("Nullable<DateTimeOffset>ToDateTimeOffset", null, ConvertResult.Success, default(DateTimeOffset)),
                            new TryConvertGenericTest<DateTimeOffset?, DateTimeOffset>("Nullable<DateTimeOffset>ToDateTimeOffset", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTimeOffset?, decimal>("Nullable<DateTimeOffset>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTimeOffset?, decimal>("Nullable<DateTimeOffset>ToDecimal", TestDateTimeOffset, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DateTimeOffset?, double>("Nullable<DateTimeOffset>ToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTimeOffset?, double>("Nullable<DateTimeOffset>ToDouble", TestDateTimeOffset, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DateTimeOffset?, PrimaryColor>("Nullable<DateTimeOffset>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTimeOffset?, PrimaryColor>("Nullable<DateTimeOffset>ToEnum", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DateTimeOffset?, float>("Nullable<DateTimeOffset>ToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTimeOffset?, float>("Nullable<DateTimeOffset>ToFloat", TestDateTimeOffset, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DateTimeOffset?, Guid>("Nullable<DateTimeOffset>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTimeOffset?, Guid>("Nullable<DateTimeOffset>ToGuid", TestDateTimeOffset, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DateTimeOffset?, int>("Nullable<DateTimeOffset>ToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTimeOffset?, int>("Nullable<DateTimeOffset>ToInt", TestDateTimeOffset, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DateTimeOffset?, long>("Nullable<DateTimeOffset>ToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTimeOffset?, long>("Nullable<DateTimeOffset>ToLong", TestDateTimeOffset, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DateTimeOffset?, sbyte>("Nullable<DateTimeOffset>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTimeOffset?, sbyte>("Nullable<DateTimeOffset>ToSByte", TestDateTimeOffset, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DateTimeOffset?, short>("Nullable<DateTimeOffset>ToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTimeOffset?, short>("Nullable<DateTimeOffset>ToShort", TestDateTimeOffset, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToString", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetString),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormat", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormat, FormatDateTimeOffsetContext),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<DateTimeOffset?, string>("Nullable<DateTimeOffset>ToStringWithFormatAndFormatProvider", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffsetStringWithFormatAndFormatProvider, FormatAndFormatProviderDateTimeOffsetContext),
                            new TryConvertGenericTest<DateTimeOffset?, TimeSpan>("Nullable<DateTimeOffset>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTimeOffset?, TimeSpan>("Nullable<DateTimeOffset>ToTimeSpan", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DateTimeOffset?, Type>("Nullable<DateTimeOffset>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTimeOffset?, Type>("Nullable<DateTimeOffset>ToType", TestDateTimeOffset, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DateTimeOffset?, uint>("Nullable<DateTimeOffset>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTimeOffset?, uint>("Nullable<DateTimeOffset>ToUInt", TestDateTimeOffset, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DateTimeOffset?, ulong>("Nullable<DateTimeOffset>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTimeOffset?, ulong>("Nullable<DateTimeOffset>ToULong", TestDateTimeOffset, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DateTimeOffset?, Uri>("Nullable<DateTimeOffset>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTimeOffset?, Uri>("Nullable<DateTimeOffset>ToUri", TestDateTimeOffset, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DateTimeOffset?, ushort>("Nullable<DateTimeOffset>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<DateTimeOffset?, ushort>("Nullable<DateTimeOffset>ToUShort", TestDateTimeOffset, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DateTimeOffset?, bool?>("Nullable<DateTimeOffset>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTimeOffset?, bool?>("Nullable<DateTimeOffset>ToNullable<Bool>", TestDateTimeOffset, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DateTimeOffset?, byte?>("Nullable<DateTimeOffset>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTimeOffset?, byte?>("Nullable<DateTimeOffset>ToNullable<Byte>", TestDateTimeOffset, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DateTimeOffset?, char?>("Nullable<DateTimeOffset>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTimeOffset?, char?>("Nullable<DateTimeOffset>ToNullable<Char>", TestDateTimeOffset, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DateTimeOffset?, DateTime?>("Nullable<DateTimeOffset>ToNullable<DateTime>", null, ConvertResult.Success, new DateTime?()),
                            new TryConvertGenericTest<DateTimeOffset?, DateTime?>("Nullable<DateTimeOffset>ToNullable<DateTime>", TestDateTimeOffset, ConvertResult.Success, TestDateTime),
                            new TryConvertGenericTest<DateTimeOffset?, DateTimeOffset?>("Nullable<DateTimeOffset>ToNullable<DateTimeOffset>", null, ConvertResult.Success, new DateTimeOffset?()),
                            new TryConvertGenericTest<DateTimeOffset?, DateTimeOffset?>("Nullable<DateTimeOffset>ToNullable<DateTimeOffset>", TestDateTimeOffset, ConvertResult.Success, TestDateTimeOffset),
                            new TryConvertGenericTest<DateTimeOffset?, decimal?>("Nullable<DateTimeOffset>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTimeOffset?, decimal?>("Nullable<DateTimeOffset>ToNullable<Decimal>", TestDateTimeOffset, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DateTimeOffset?, double?>("Nullable<DateTimeOffset>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTimeOffset?, double?>("Nullable<DateTimeOffset>ToNullable<Double>", TestDateTimeOffset, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DateTimeOffset?, PrimaryColor?>("Nullable<DateTimeOffset>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTimeOffset?, PrimaryColor?>("Nullable<DateTimeOffset>ToNullable<Enum>", TestDateTimeOffset, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DateTimeOffset?, float?>("Nullable<DateTimeOffset>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTimeOffset?, float?>("Nullable<DateTimeOffset>ToNullable<Float>", TestDateTimeOffset, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DateTimeOffset?, Guid?>("Nullable<DateTimeOffset>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<DateTimeOffset?, Guid?>("Nullable<DateTimeOffset>ToNullable<Guid>", TestDateTimeOffset, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<DateTimeOffset?, int?>("Nullable<DateTimeOffset>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTimeOffset?, int?>("Nullable<DateTimeOffset>ToNullable<Int>", TestDateTimeOffset, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DateTimeOffset?, long?>("Nullable<DateTimeOffset>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTimeOffset?, long?>("Nullable<DateTimeOffset>ToNullable<Long>", TestDateTimeOffset, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DateTimeOffset?, sbyte?>("Nullable<DateTimeOffset>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTimeOffset?, sbyte?>("Nullable<DateTimeOffset>ToNullable<SByte>", TestDateTimeOffset, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DateTimeOffset?, short?>("Nullable<DateTimeOffset>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTimeOffset?, short?>("Nullable<DateTimeOffset>ToNullable<Short>", TestDateTimeOffset, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DateTimeOffset?, TimeSpan?>("Nullable<DateTimeOffset>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<DateTimeOffset?, TimeSpan?>("Nullable<DateTimeOffset>ToNullable<TimeSpan>", TestDateTimeOffset, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<DateTimeOffset?, uint?>("Nullable<DateTimeOffset>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTimeOffset?, uint?>("Nullable<DateTimeOffset>ToNullable<UInt>", TestDateTimeOffset, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DateTimeOffset?, ulong?>("Nullable<DateTimeOffset>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTimeOffset?, ulong?>("Nullable<DateTimeOffset>ToNullable<ULong>", TestDateTimeOffset, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DateTimeOffset?, ushort?>("Nullable<DateTimeOffset>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<DateTimeOffset?, ushort?>("Nullable<DateTimeOffset>ToNullable<UShort>", TestDateTimeOffset, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DateTimeOffset?, IInterface>("Nullable<DateTimeOffset>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTimeOffset?, IInterface>("Nullable<DateTimeOffset>ToInterface", TestDateTimeOffset, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<DateTimeOffset?, BaseClass>("Nullable<DateTimeOffset>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTimeOffset?, BaseClass>("Nullable<DateTimeOffset>ToBaseClass", TestDateTimeOffset, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<DateTimeOffset?, DerivedClass>("Nullable<DateTimeOffset>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<DateTimeOffset?, DerivedClass>("Nullable<DateTimeOffset>ToDerivedClass", TestDateTimeOffset, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Decimal>ToXXX
                new object []
                {
                    "Nullable<Decimal>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<decimal?, bool>("Nullable<Decimal>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<decimal?, bool>("Nullable<Decimal>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<decimal?, byte>("Nullable<Decimal>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<decimal?, byte>("Nullable<Decimal>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, byte[]>("Nullable<Decimal>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<decimal?, byte[]>("Nullable<Decimal>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<decimal?, char>("Nullable<Decimal>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<decimal?, char>("Nullable<Decimal>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<decimal?, DateTime>("Nullable<Decimal>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<decimal?, DateTime>("Nullable<Decimal>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<decimal?, DateTimeOffset>("Nullable<Decimal>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<decimal?, DateTimeOffset>("Nullable<Decimal>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<decimal?, decimal>("Nullable<Decimal>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<decimal?, decimal>("Nullable<Decimal>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, double>("Nullable<Decimal>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<decimal?, double>("Nullable<Decimal>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, PrimaryColor>("Nullable<Decimal>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<decimal?, PrimaryColor>("Nullable<Decimal>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<decimal?, float>("Nullable<Decimal>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<decimal?, float>("Nullable<Decimal>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, Guid>("Nullable<Decimal>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<decimal?, Guid>("Nullable<Decimal>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<decimal?, int>("Nullable<Decimal>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<decimal?, int>("Nullable<Decimal>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, long>("Nullable<Decimal>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<decimal?, long>("Nullable<Decimal>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, sbyte>("Nullable<Decimal>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<decimal?, sbyte>("Nullable<Decimal>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, short>("Nullable<Decimal>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<decimal?, short>("Nullable<Decimal>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, string>("Nullable<Decimal>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<decimal?, string>("Nullable<Decimal>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<decimal?, TimeSpan>("Nullable<Decimal>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<decimal?, TimeSpan>("Nullable<Decimal>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<decimal?, Type>("Nullable<Decimal>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<decimal?, Type>("Nullable<Decimal>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<decimal?, uint>("Nullable<Decimal>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<decimal?, uint>("Nullable<Decimal>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, ulong>("Nullable<Decimal>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<decimal?, ulong>("Nullable<Decimal>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, Uri>("Nullable<Decimal>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<decimal?, Uri>("Nullable<Decimal>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<decimal?, ushort>("Nullable<Decimal>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<decimal?, ushort>("Nullable<Decimal>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<decimal?, bool?>("Nullable<Decimal>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<decimal?, bool?>("Nullable<Decimal>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<decimal?, byte?>("Nullable<Decimal>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<decimal?, byte?>("Nullable<Decimal>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, char?>("Nullable<Decimal>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<decimal?, char?>("Nullable<Decimal>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<decimal?, DateTime?>("Nullable<Decimal>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<decimal?, DateTime?>("Nullable<Decimal>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<decimal?, DateTimeOffset?>("Nullable<Decimal>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<decimal?, DateTimeOffset?>("Nullable<Decimal>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<decimal?, decimal?>("Nullable<Decimal>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<decimal?, decimal?>("Nullable<Decimal>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, double?>("Nullable<Decimal>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<decimal?, double?>("Nullable<Decimal>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, PrimaryColor?>("Nullable<Decimal>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<decimal?, PrimaryColor?>("Nullable<Decimal>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<decimal?, float?>("Nullable<Decimal>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<decimal?, float?>("Nullable<Decimal>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, Guid?>("Nullable<Decimal>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<decimal?, Guid?>("Nullable<Decimal>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<decimal?, int?>("Nullable<Decimal>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<decimal?, int?>("Nullable<Decimal>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, long?>("Nullable<Decimal>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<decimal?, long?>("Nullable<Decimal>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, sbyte?>("Nullable<Decimal>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<decimal?, sbyte?>("Nullable<Decimal>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, short?>("Nullable<Decimal>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<decimal?, short?>("Nullable<Decimal>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, TimeSpan?>("Nullable<Decimal>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<decimal?, TimeSpan?>("Nullable<Decimal>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<decimal?, uint?>("Nullable<Decimal>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<decimal?, uint?>("Nullable<Decimal>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, ulong?>("Nullable<Decimal>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<decimal?, ulong?>("Nullable<Decimal>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<decimal?, ushort?>("Nullable<Decimal>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<decimal?, ushort?>("Nullable<Decimal>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<decimal?, IInterface>("Nullable<Decimal>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<decimal?, IInterface>("Nullable<Decimal>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<decimal?, BaseClass>("Nullable<Decimal>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<decimal?, BaseClass>("Nullable<Decimal>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<decimal?, DerivedClass>("Nullable<Decimal>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<decimal?, DerivedClass>("Nullable<Decimal>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Double>ToXXX
                new object []
                {
                    "Nullable<Double>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<double?, bool>("Nullable<Double>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<double?, bool>("Nullable<Double>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<double?, byte>("Nullable<Double>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<double?, byte>("Nullable<Double>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, byte[]>("Nullable<Double>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<double?, byte[]>("Nullable<Double>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<double?, char>("Nullable<Double>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<double?, char>("Nullable<Double>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<double?, DateTime>("Nullable<Double>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<double?, DateTime>("Nullable<Double>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<double?, DateTimeOffset>("Nullable<Double>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<double?, DateTimeOffset>("Nullable<Double>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<double?, decimal>("Nullable<Double>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<double?, decimal>("Nullable<Double>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, double>("Nullable<Double>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<double?, double>("Nullable<Double>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, PrimaryColor>("Nullable<Double>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<double?, PrimaryColor>("Nullable<Double>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<double?, float>("Nullable<Double>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<double?, float>("Nullable<Double>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, Guid>("Nullable<Double>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<double?, Guid>("Nullable<Double>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<double?, int>("Nullable<Double>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<double?, int>("Nullable<Double>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, long>("Nullable<Double>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<double?, long>("Nullable<Double>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, sbyte>("Nullable<Double>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<double?, sbyte>("Nullable<Double>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, short>("Nullable<Double>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<double?, short>("Nullable<Double>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, string>("Nullable<Double>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<double?, string>("Nullable<Double>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<double?, TimeSpan>("Nullable<Double>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<double?, TimeSpan>("Nullable<Double>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<double?, Type>("Nullable<Double>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<double?, Type>("Nullable<Double>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<double?, uint>("Nullable<Double>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<double?, uint>("Nullable<Double>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, ulong>("Nullable<Double>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<double?, ulong>("Nullable<Double>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, Uri>("Nullable<Double>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<double?, Uri>("Nullable<Double>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<double?, ushort>("Nullable<Double>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<double?, ushort>("Nullable<Double>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<double?, bool?>("Nullable<Double>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<double?, bool?>("Nullable<Double>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<double?, byte?>("Nullable<Double>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<double?, byte?>("Nullable<Double>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, char?>("Nullable<Double>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<double?, char?>("Nullable<Double>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<double?, DateTime?>("Nullable<Double>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<double?, DateTime?>("Nullable<Double>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<double?, DateTimeOffset?>("Nullable<Double>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<double?, DateTimeOffset?>("Nullable<Double>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<double?, decimal?>("Nullable<Double>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<double?, decimal?>("Nullable<Double>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, double?>("Nullable<Double>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<double?, double?>("Nullable<Double>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, PrimaryColor?>("Nullable<Double>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<double?, PrimaryColor?>("Nullable<Double>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<double?, float?>("Nullable<Double>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<double?, float?>("Nullable<Double>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, Guid?>("Nullable<Double>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<double?, Guid?>("Nullable<Double>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<double?, int?>("Nullable<Double>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<double?, int?>("Nullable<Double>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, long?>("Nullable<Double>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<double?, long?>("Nullable<Double>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, sbyte?>("Nullable<Double>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<double?, sbyte?>("Nullable<Double>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, short?>("Nullable<Double>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<double?, short?>("Nullable<Double>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, TimeSpan?>("Nullable<Double>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<double?, TimeSpan?>("Nullable<Double>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<double?, uint?>("Nullable<Double>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<double?, uint?>("Nullable<Double>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, ulong?>("Nullable<Double>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<double?, ulong?>("Nullable<Double>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<double?, ushort?>("Nullable<Double>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<double?, ushort?>("Nullable<Double>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<double?, IInterface>("Nullable<Double>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<double?, IInterface>("Nullable<Double>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<double?, BaseClass>("Nullable<Double>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<double?, BaseClass>("Nullable<Double>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<double?, DerivedClass>("Nullable<Double>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<double?, DerivedClass>("Nullable<Double>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Enum>ToXXX
                new object []
                {
                    "Nullable<Enum>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<PrimaryColor?, bool>("Nullable<Enum>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<PrimaryColor?, bool>("Nullable<Enum>ToBool", PrimaryColor.Blue, ConvertResult.Success, true),
                            new TryConvertGenericTest<PrimaryColor?, byte>("Nullable<Enum>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<PrimaryColor?, byte>("Nullable<Enum>ToByte", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, byte[]>("Nullable<Enum>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<PrimaryColor?, byte[]>("Nullable<Enum>ToByteArray", PrimaryColor.Blue, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<PrimaryColor?, char>("Nullable<Enum>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<PrimaryColor?, char>("Nullable<Enum>ToChar", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<PrimaryColor?, DateTime>("Nullable<Enum>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<PrimaryColor?, DateTime>("Nullable<Enum>ToDateTime", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<PrimaryColor?, DateTimeOffset>("Nullable<Enum>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<PrimaryColor?, DateTimeOffset>("Nullable<Enum>ToDateTimeOffset", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<PrimaryColor?, decimal>("Nullable<Enum>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<PrimaryColor?, decimal>("Nullable<Enum>ToDecimal", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, double>("Nullable<Enum>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<PrimaryColor?, double>("Nullable<Enum>ToDouble", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, PrimaryColor>("Nullable<Enum>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<PrimaryColor?, PrimaryColor>("Nullable<Enum>ToEnum", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<PrimaryColor?, float>("Nullable<Enum>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<PrimaryColor?, float>("Nullable<Enum>ToFloat", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, Guid>("Nullable<Enum>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<PrimaryColor?, Guid>("Nullable<Enum>ToGuid", PrimaryColor.Blue, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<PrimaryColor?, int>("Nullable<Enum>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<PrimaryColor?, int>("Nullable<Enum>ToInt", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, long>("Nullable<Enum>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<PrimaryColor?, long>("Nullable<Enum>ToLong", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, sbyte>("Nullable<Enum>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<PrimaryColor?, sbyte>("Nullable<Enum>ToSByte", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, short>("Nullable<Enum>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<PrimaryColor?, short>("Nullable<Enum>ToShort", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToString", PrimaryColor.Blue, ConvertResult.Success, TestBlueString),
                            new TryConvertGenericTest<PrimaryColor?, string>("Nullable<Enum>ToStringWithFormat", PrimaryColor.Blue, ConvertResult.Success, TestBlueOrdinalAsString, FormatEnumContext),
                            new TryConvertGenericTest<PrimaryColor?, TimeSpan>("Nullable<Enum>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<PrimaryColor?, TimeSpan>("Nullable<Enum>ToTimeSpan", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<PrimaryColor?, Type>("Nullable<Enum>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<PrimaryColor?, Type>("Nullable<Enum>ToType", PrimaryColor.Blue, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<PrimaryColor?, uint>("Nullable<Enum>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<PrimaryColor?, uint>("Nullable<Enum>ToUInt", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, ulong>("Nullable<Enum>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<PrimaryColor?, ulong>("Nullable<Enum>ToULong", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, Uri>("Nullable<Enum>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<PrimaryColor?, Uri>("Nullable<Enum>ToUri", PrimaryColor.Blue, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<PrimaryColor?, ushort>("Nullable<Enum>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<PrimaryColor?, ushort>("Nullable<Enum>ToUShort", PrimaryColor.Blue, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<PrimaryColor?, bool?>("Nullable<Enum>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<PrimaryColor?, bool?>("Nullable<Enum>ToNullable<Bool>", PrimaryColor.Blue, ConvertResult.Success, true),
                            new TryConvertGenericTest<PrimaryColor?, byte?>("Nullable<Enum>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<PrimaryColor?, byte?>("Nullable<Enum>ToNullable<Byte>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, char?>("Nullable<Enum>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<PrimaryColor?, char?>("Nullable<Enum>ToNullable<Char>", PrimaryColor.Blue, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<PrimaryColor?, DateTime?>("Nullable<Enum>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<PrimaryColor?, DateTime?>("Nullable<Enum>ToNullable<DateTime>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<PrimaryColor?, DateTimeOffset?>("Nullable<Enum>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<PrimaryColor?, DateTimeOffset?>("Nullable<Enum>ToNullable<DateTimeOffset>", PrimaryColor.Blue, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<PrimaryColor?, decimal?>("Nullable<Enum>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<PrimaryColor?, decimal?>("Nullable<Enum>ToNullable<Decimal>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, double?>("Nullable<Enum>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<PrimaryColor?, double?>("Nullable<Enum>ToNullable<Double>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, PrimaryColor?>("Nullable<Enum>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<PrimaryColor?, PrimaryColor?>("Nullable<Enum>ToNullable<Enum>", PrimaryColor.Blue, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<PrimaryColor?, float?>("Nullable<Enum>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<PrimaryColor?, float?>("Nullable<Enum>ToNullable<Float>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, Guid?>("Nullable<Enum>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<PrimaryColor?, Guid?>("Nullable<Enum>ToNullable<Guid>", PrimaryColor.Blue, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<PrimaryColor?, int?>("Nullable<Enum>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<PrimaryColor?, int?>("Nullable<Enum>ToNullable<Int>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, long?>("Nullable<Enum>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<PrimaryColor?, long?>("Nullable<Enum>ToNullable<Long>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, sbyte?>("Nullable<Enum>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<PrimaryColor?, sbyte?>("Nullable<Enum>ToNullable<SByte>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, short?>("Nullable<Enum>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<PrimaryColor?, short?>("Nullable<Enum>ToNullable<Short>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, TimeSpan?>("Nullable<Enum>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<PrimaryColor?, TimeSpan?>("Nullable<Enum>ToNullable<TimeSpan>", PrimaryColor.Blue, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<PrimaryColor?, uint?>("Nullable<Enum>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<PrimaryColor?, uint?>("Nullable<Enum>ToNullable<UInt>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, ulong?>("Nullable<Enum>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<PrimaryColor?, ulong?>("Nullable<Enum>ToNullable<ULong>", PrimaryColor.Blue, ConvertResult.Success, 42),
                            new TryConvertGenericTest<PrimaryColor?, ushort?>("Nullable<Enum>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<PrimaryColor?, ushort?>("Nullable<Enum>ToNullable<UShort>", PrimaryColor.Blue, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<PrimaryColor?, IInterface>("Nullable<Enum>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<PrimaryColor?, IInterface>("Nullable<Enum>ToInterface", PrimaryColor.Blue, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<PrimaryColor?, BaseClass>("Nullable<Enum>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<PrimaryColor?, BaseClass>("Nullable<Enum>ToBaseClass", PrimaryColor.Blue, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<PrimaryColor?, DerivedClass>("Nullable<Enum>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<PrimaryColor?, DerivedClass>("Nullable<Enum>ToDerivedClass", PrimaryColor.Blue, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Float>ToXXX
                new object []
                {
                    "Nullable<Float>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<float?, bool>("Nullable<Float>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<float?, bool>("Nullable<Float>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<float?, byte>("Nullable<Float>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<float?, byte>("Nullable<Float>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, byte[]>("Nullable<Float>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<float?, byte[]>("Nullable<Float>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<float?, char>("Nullable<Float>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<float?, char>("Nullable<Float>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<float?, DateTime>("Nullable<Float>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<float?, DateTime>("Nullable<Float>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<float?, DateTimeOffset>("Nullable<Float>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<float?, DateTimeOffset>("Nullable<Float>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<float?, decimal>("Nullable<Float>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<float?, decimal>("Nullable<Float>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, double>("Nullable<Float>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<float?, double>("Nullable<Float>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, PrimaryColor>("Nullable<Float>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<float?, PrimaryColor>("Nullable<Float>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<float?, float>("Nullable<Float>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<float?, float>("Nullable<Float>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, Guid>("Nullable<Float>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<float?, Guid>("Nullable<Float>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<float?, int>("Nullable<Float>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<float?, int>("Nullable<Float>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, long>("Nullable<Float>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<float?, long>("Nullable<Float>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, sbyte>("Nullable<Float>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<float?, sbyte>("Nullable<Float>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, short>("Nullable<Float>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<float?, short>("Nullable<Float>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, string>("Nullable<Float>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<float?, string>("Nullable<Float>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<float?, TimeSpan>("Nullable<Float>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<float?, TimeSpan>("Nullable<Float>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<float?, Type>("Nullable<Float>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<float?, Type>("Nullable<Float>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<float?, uint>("Nullable<Float>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<float?, uint>("Nullable<Float>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, ulong>("Nullable<Float>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<float?, ulong>("Nullable<Float>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, Uri>("Nullable<Float>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<float?, Uri>("Nullable<Float>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<float?, ushort>("Nullable<Float>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<float?, ushort>("Nullable<Float>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<float?, bool?>("Nullable<Float>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<float?, bool?>("Nullable<Float>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<float?, byte?>("Nullable<Float>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<float?, byte?>("Nullable<Float>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, char?>("Nullable<Float>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<float?, char?>("Nullable<Float>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<float?, DateTime?>("Nullable<Float>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<float?, DateTime?>("Nullable<Float>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<float?, DateTimeOffset?>("Nullable<Float>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<float?, DateTimeOffset?>("Nullable<Float>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<float?, decimal?>("Nullable<Float>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<float?, decimal?>("Nullable<Float>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, double?>("Nullable<Float>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<float?, double?>("Nullable<Float>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, PrimaryColor?>("Nullable<Float>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<float?, PrimaryColor?>("Nullable<Float>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<float?, float?>("Nullable<Float>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<float?, float?>("Nullable<Float>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, Guid?>("Nullable<Float>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<float?, Guid?>("Nullable<Float>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<float?, int?>("Nullable<Float>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<float?, int?>("Nullable<Float>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, long?>("Nullable<Float>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<float?, long?>("Nullable<Float>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, sbyte?>("Nullable<Float>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<float?, sbyte?>("Nullable<Float>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, short?>("Nullable<Float>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<float?, short?>("Nullable<Float>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, TimeSpan?>("Nullable<Float>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<float?, TimeSpan?>("Nullable<Float>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<float?, uint?>("Nullable<Float>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<float?, uint?>("Nullable<Float>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, ulong?>("Nullable<Float>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<float?, ulong?>("Nullable<Float>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<float?, ushort?>("Nullable<Float>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<float?, ushort?>("Nullable<Float>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<float?, IInterface>("Nullable<Float>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<float?, IInterface>("Nullable<Float>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<float?, BaseClass>("Nullable<Float>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<float?, BaseClass>("Nullable<Float>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<float?, DerivedClass>("Nullable<Float>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<float?, DerivedClass>("Nullable<Float>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Guid>ToXXX
                new object []
                {
                    "Nullable<Guid>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<Guid?, bool>("Nullable<Guid>ToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Guid?, bool>("Nullable<Guid>ToBool", TestGuid, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<Guid?, byte>("Nullable<Guid>ToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Guid?, byte>("Nullable<Guid>ToByte", TestGuid, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<Guid?, byte[]>("Nullable<Guid>ToByteArray", null, ConvertResult.Success, default(byte[])),
                            new TryConvertGenericTest<Guid?, byte[]>("Nullable<Guid>ToByteArray", TestGuid, ConvertResult.Success, TestGuidByteArray),
                            new TryConvertGenericTest<Guid?, char>("Nullable<Guid>ToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Guid?, char>("Nullable<Guid>ToChar", TestGuid, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<Guid?, DateTime>("Nullable<Guid>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<Guid?, DateTime>("Nullable<Guid>ToDateTime", TestGuid, ConvertResult.Failure, TestDateTime),
                            new TryConvertGenericTest<Guid?, DateTimeOffset>("Nullable<Guid>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<Guid?, DateTimeOffset>("Nullable<Guid>ToDateTimeOffset", TestGuid, ConvertResult.Failure, TestDateTimeOffset),
                            new TryConvertGenericTest<Guid?, decimal>("Nullable<Guid>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Guid?, decimal>("Nullable<Guid>ToDecimal", TestGuid, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<Guid?, double>("Nullable<Guid>ToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Guid?, double>("Nullable<Guid>ToDouble", TestGuid, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<Guid?, PrimaryColor>("Nullable<Guid>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Guid?, PrimaryColor>("Nullable<Guid>ToEnum", TestGuid, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<Guid?, float>("Nullable<Guid>ToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Guid?, float>("Nullable<Guid>ToFloat", TestGuid, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<Guid?, Guid>("Nullable<Guid>ToGuid", null, ConvertResult.Success, default(Guid)),
                            new TryConvertGenericTest<Guid?, Guid>("Nullable<Guid>ToGuid", TestGuid, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<Guid?, int>("Nullable<Guid>ToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Guid?, int>("Nullable<Guid>ToInt", TestGuid, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<Guid?, long>("Nullable<Guid>ToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Guid?, long>("Nullable<Guid>ToLong", TestGuid, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<Guid?, sbyte>("Nullable<Guid>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Guid?, sbyte>("Nullable<Guid>ToSByte", TestGuid, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<Guid?, short>("Nullable<Guid>ToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Guid?, short>("Nullable<Guid>ToShort", TestGuid, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<Guid?, string>("Nullable<Guid>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<Guid?, string>("Nullable<Guid>ToString", TestGuid, ConvertResult.Success, TestGuidString),
                            new TryConvertGenericTest<Guid?, TimeSpan>("Nullable<Guid>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Guid?, TimeSpan>("Nullable<Guid>ToTimeSpan", TestGuid, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<Guid?, Type>("Nullable<Guid>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Guid?, Type>("Nullable<Guid>ToType", TestGuid, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<Guid?, uint>("Nullable<Guid>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Guid?, uint>("Nullable<Guid>ToUInt", TestGuid, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<Guid?, ulong>("Nullable<Guid>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Guid?, ulong>("Nullable<Guid>ToULong", TestGuid, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<Guid?, Uri>("Nullable<Guid>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Guid?, Uri>("Nullable<Guid>ToUri", TestGuid, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<Guid?, ushort>("Nullable<Guid>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<Guid?, ushort>("Nullable<Guid>ToUShort", TestGuid, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<Guid?, bool?>("Nullable<Guid>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Guid?, bool?>("Nullable<Guid>ToNullable<Bool>", TestGuid, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<Guid?, byte?>("Nullable<Guid>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Guid?, byte?>("Nullable<Guid>ToNullable<Byte>", TestGuid, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<Guid?, char?>("Nullable<Guid>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Guid?, char?>("Nullable<Guid>ToNullable<Char>", TestGuid, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<Guid?, DateTime?>("Nullable<Guid>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<Guid?, DateTime?>("Nullable<Guid>ToNullable<DateTime>", TestGuid, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<Guid?, DateTimeOffset?>("Nullable<Guid>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<Guid?, DateTimeOffset?>("Nullable<Guid>ToNullable<DateTimeOffset>", TestGuid, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<Guid?, decimal?>("Nullable<Guid>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Guid?, decimal?>("Nullable<Guid>ToNullable<Decimal>", TestGuid, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<Guid?, double?>("Nullable<Guid>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Guid?, double?>("Nullable<Guid>ToNullable<Double>", TestGuid, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<Guid?, PrimaryColor?>("Nullable<Guid>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Guid?, PrimaryColor?>("Nullable<Guid>ToNullable<Enum>", TestGuid, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<Guid?, float?>("Nullable<Guid>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Guid?, float?>("Nullable<Guid>ToNullable<Float>", TestGuid, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<Guid?, Guid?>("Nullable<Guid>ToNullable<Guid>", null, ConvertResult.Success, new Guid?()),
                            new TryConvertGenericTest<Guid?, Guid?>("Nullable<Guid>ToNullable<Guid>", TestGuid, ConvertResult.Success, TestGuid),
                            new TryConvertGenericTest<Guid?, int?>("Nullable<Guid>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Guid?, int?>("Nullable<Guid>ToNullable<Int>", TestGuid, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<Guid?, long?>("Nullable<Guid>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Guid?, long?>("Nullable<Guid>ToNullable<Long>", TestGuid, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<Guid?, sbyte?>("Nullable<Guid>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Guid?, sbyte?>("Nullable<Guid>ToNullable<SByte>", TestGuid, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<Guid?, short?>("Nullable<Guid>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Guid?, short?>("Nullable<Guid>ToNullable<Short>", TestGuid, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<Guid?, TimeSpan?>("Nullable<Guid>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<Guid?, TimeSpan?>("Nullable<Guid>ToNullable<TimeSpan>", TestGuid, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<Guid?, uint?>("Nullable<Guid>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Guid?, uint?>("Nullable<Guid>ToNullable<UInt>", TestGuid, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<Guid?, ulong?>("Nullable<Guid>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Guid?, ulong?>("Nullable<Guid>ToNullable<ULong>", TestGuid, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<Guid?, ushort?>("Nullable<Guid>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<Guid?, ushort?>("Nullable<Guid>ToNullable<UShort>", TestGuid, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<Guid?, IInterface>("Nullable<Guid>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Guid?, IInterface>("Nullable<Guid>ToInterface", TestGuid, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<Guid?, BaseClass>("Nullable<Guid>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Guid?, BaseClass>("Nullable<Guid>ToBaseClass", TestGuid, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<Guid?, DerivedClass>("Nullable<Guid>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<Guid?, DerivedClass>("Nullable<Guid>ToDerivedClass", TestGuid, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Int>ToXXX
                new object []
                {
                    "Nullable<Int>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<int?, bool>("Nullable<Int>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<int?, bool>("Nullable<Int>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<int?, byte>("Nullable<Int>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<int?, byte>("Nullable<Int>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, byte[]>("Nullable<Int>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<int?, byte[]>("Nullable<Int>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<int?, char>("Nullable<Int>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<int?, char>("Nullable<Int>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<int?, DateTime>("Nullable<Int>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<int?, DateTime>("Nullable<Int>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<int?, DateTimeOffset>("Nullable<Int>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<int?, DateTimeOffset>("Nullable<Int>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<int?, decimal>("Nullable<Int>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<int?, decimal>("Nullable<Int>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, double>("Nullable<Int>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<int?, double>("Nullable<Int>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, PrimaryColor>("Nullable<Int>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<int?, PrimaryColor>("Nullable<Int>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<int?, float>("Nullable<Int>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<int?, float>("Nullable<Int>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, Guid>("Nullable<Int>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<int?, Guid>("Nullable<Int>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<int?, int>("Nullable<Int>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<int?, int>("Nullable<Int>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, long>("Nullable<Int>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<int?, long>("Nullable<Int>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, sbyte>("Nullable<Int>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<int?, sbyte>("Nullable<Int>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, short>("Nullable<Int>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<int?, short>("Nullable<Int>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, string>("Nullable<Int>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<int?, string>("Nullable<Int>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<int?, TimeSpan>("Nullable<Int>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<int?, TimeSpan>("Nullable<Int>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<int?, Type>("Nullable<Int>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<int?, Type>("Nullable<Int>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<int?, uint>("Nullable<Int>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<int?, uint>("Nullable<Int>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, ulong>("Nullable<Int>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<int?, ulong>("Nullable<Int>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, Uri>("Nullable<Int>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<int?, Uri>("Nullable<Int>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<int?, ushort>("Nullable<Int>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<int?, ushort>("Nullable<Int>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<int?, bool?>("Nullable<Int>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<int?, bool?>("Nullable<Int>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<int?, byte?>("Nullable<Int>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<int?, byte?>("Nullable<Int>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, char?>("Nullable<Int>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<int?, char?>("Nullable<Int>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<int?, DateTime?>("Nullable<Int>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<int?, DateTime?>("Nullable<Int>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<int?, DateTimeOffset?>("Nullable<Int>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<int?, DateTimeOffset?>("Nullable<Int>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<int?, decimal?>("Nullable<Int>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<int?, decimal?>("Nullable<Int>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, double?>("Nullable<Int>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<int?, double?>("Nullable<Int>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, PrimaryColor?>("Nullable<Int>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<int?, PrimaryColor?>("Nullable<Int>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<int?, float?>("Nullable<Int>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<int?, float?>("Nullable<Int>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, Guid?>("Nullable<Int>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<int?, Guid?>("Nullable<Int>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<int?, int?>("Nullable<Int>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<int?, int?>("Nullable<Int>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, long?>("Nullable<Int>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<int?, long?>("Nullable<Int>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, sbyte?>("Nullable<Int>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<int?, sbyte?>("Nullable<Int>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, short?>("Nullable<Int>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<int?, short?>("Nullable<Int>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, TimeSpan?>("Nullable<Int>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<int?, TimeSpan?>("Nullable<Int>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<int?, uint?>("Nullable<Int>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<int?, uint?>("Nullable<Int>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, ulong?>("Nullable<Int>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<int?, ulong?>("Nullable<Int>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<int?, ushort?>("Nullable<Int>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<int?, ushort?>("Nullable<Int>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<int?, IInterface>("Nullable<Int>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<int?, IInterface>("Nullable<Int>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<int?, BaseClass>("Nullable<Int>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<int?, BaseClass>("Nullable<Int>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<int?, DerivedClass>("Nullable<Int>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<int?, DerivedClass>("Nullable<Int>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Long>ToXXX
                new object []
                {
                    "Nullable<Long>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<long?, bool>("Nullable<Long>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<long?, bool>("Nullable<Long>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<long?, byte>("Nullable<Long>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<long?, byte>("Nullable<Long>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, byte[]>("Nullable<Long>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<long?, byte[]>("Nullable<Long>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<long?, char>("Nullable<Long>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<long?, char>("Nullable<Long>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<long?, DateTime>("Nullable<Long>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<long?, DateTime>("Nullable<Long>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<long?, DateTimeOffset>("Nullable<Long>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<long?, DateTimeOffset>("Nullable<Long>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<long?, decimal>("Nullable<Long>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<long?, decimal>("Nullable<Long>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, double>("Nullable<Long>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<long?, double>("Nullable<Long>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, PrimaryColor>("Nullable<Long>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<long?, PrimaryColor>("Nullable<Long>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<long?, float>("Nullable<Long>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<long?, float>("Nullable<Long>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, Guid>("Nullable<Long>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<long?, Guid>("Nullable<Long>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<long?, int>("Nullable<Long>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<long?, int>("Nullable<Long>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, long>("Nullable<Long>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<long?, long>("Nullable<Long>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, sbyte>("Nullable<Long>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<long?, sbyte>("Nullable<Long>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, short>("Nullable<Long>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<long?, short>("Nullable<Long>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, string>("Nullable<Long>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<long?, string>("Nullable<Long>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<long?, TimeSpan>("Nullable<Long>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<long?, TimeSpan>("Nullable<Long>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<long?, Type>("Nullable<Long>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<long?, Type>("Nullable<Long>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<long?, uint>("Nullable<Long>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<long?, uint>("Nullable<Long>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, ulong>("Nullable<Long>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<long?, ulong>("Nullable<Long>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, Uri>("Nullable<Long>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<long?, Uri>("Nullable<Long>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<long?, ushort>("Nullable<Long>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<long?, ushort>("Nullable<Long>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<long?, bool?>("Nullable<Long>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<long?, bool?>("Nullable<Long>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<long?, byte?>("Nullable<Long>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<long?, byte?>("Nullable<Long>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, char?>("Nullable<Long>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<long?, char?>("Nullable<Long>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<long?, DateTime?>("Nullable<Long>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<long?, DateTime?>("Nullable<Long>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<long?, DateTimeOffset?>("Nullable<Long>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<long?, DateTimeOffset?>("Nullable<Long>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<long?, decimal?>("Nullable<Long>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<long?, decimal?>("Nullable<Long>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, double?>("Nullable<Long>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<long?, double?>("Nullable<Long>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, PrimaryColor?>("Nullable<Long>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<long?, PrimaryColor?>("Nullable<Long>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<long?, float?>("Nullable<Long>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<long?, float?>("Nullable<Long>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, Guid?>("Nullable<Long>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<long?, Guid?>("Nullable<Long>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<long?, int?>("Nullable<Long>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<long?, int?>("Nullable<Long>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, long?>("Nullable<Long>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<long?, long?>("Nullable<Long>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, sbyte?>("Nullable<Long>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<long?, sbyte?>("Nullable<Long>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, short?>("Nullable<Long>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<long?, short?>("Nullable<Long>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, TimeSpan?>("Nullable<Long>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<long?, TimeSpan?>("Nullable<Long>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<long?, uint?>("Nullable<Long>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<long?, uint?>("Nullable<Long>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, ulong?>("Nullable<Long>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<long?, ulong?>("Nullable<Long>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<long?, ushort?>("Nullable<Long>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<long?, ushort?>("Nullable<Long>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<long?, IInterface>("Nullable<Long>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<long?, IInterface>("Nullable<Long>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<long?, BaseClass>("Nullable<Long>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<long?, BaseClass>("Nullable<Long>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<long?, DerivedClass>("Nullable<Long>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<long?, DerivedClass>("Nullable<Long>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<SByte>ToXXX
                new object []
                {
                    "Nullable<SByte>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<sbyte?, bool>("Nullable<SByte>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<sbyte?, bool>("Nullable<SByte>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<sbyte?, byte>("Nullable<SByte>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<sbyte?, byte>("Nullable<SByte>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, byte[]>("Nullable<SByte>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<sbyte?, byte[]>("Nullable<SByte>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<sbyte?, char>("Nullable<SByte>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<sbyte?, char>("Nullable<SByte>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<sbyte?, DateTime>("Nullable<SByte>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<sbyte?, DateTime>("Nullable<SByte>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<sbyte?, DateTimeOffset>("Nullable<SByte>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<sbyte?, DateTimeOffset>("Nullable<SByte>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<sbyte?, decimal>("Nullable<SByte>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<sbyte?, decimal>("Nullable<SByte>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, double>("Nullable<SByte>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<sbyte?, double>("Nullable<SByte>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, PrimaryColor>("Nullable<SByte>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<sbyte?, PrimaryColor>("Nullable<SByte>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<sbyte?, float>("Nullable<SByte>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<sbyte?, float>("Nullable<SByte>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, Guid>("Nullable<SByte>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<sbyte?, Guid>("Nullable<SByte>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<sbyte?, int>("Nullable<SByte>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<sbyte?, int>("Nullable<SByte>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, long>("Nullable<SByte>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<sbyte?, long>("Nullable<SByte>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, sbyte>("Nullable<SByte>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<sbyte?, sbyte>("Nullable<SByte>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, short>("Nullable<SByte>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<sbyte?, short>("Nullable<SByte>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, string>("Nullable<SByte>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<sbyte?, string>("Nullable<SByte>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<sbyte?, TimeSpan>("Nullable<SByte>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<sbyte?, TimeSpan>("Nullable<SByte>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<sbyte?, Type>("Nullable<SByte>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<sbyte?, Type>("Nullable<SByte>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<sbyte?, uint>("Nullable<SByte>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<sbyte?, uint>("Nullable<SByte>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, ulong>("Nullable<SByte>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<sbyte?, ulong>("Nullable<SByte>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, Uri>("Nullable<SByte>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<sbyte?, Uri>("Nullable<SByte>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<sbyte?, ushort>("Nullable<SByte>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<sbyte?, ushort>("Nullable<SByte>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<sbyte?, bool?>("Nullable<SByte>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<sbyte?, bool?>("Nullable<SByte>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<sbyte?, byte?>("Nullable<SByte>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<sbyte?, byte?>("Nullable<SByte>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, char?>("Nullable<SByte>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<sbyte?, char?>("Nullable<SByte>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<sbyte?, DateTime?>("Nullable<SByte>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<sbyte?, DateTime?>("Nullable<SByte>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<sbyte?, DateTimeOffset?>("Nullable<SByte>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<sbyte?, DateTimeOffset?>("Nullable<SByte>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<sbyte?, decimal?>("Nullable<SByte>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<sbyte?, decimal?>("Nullable<SByte>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, double?>("Nullable<SByte>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<sbyte?, double?>("Nullable<SByte>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, PrimaryColor?>("Nullable<SByte>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<sbyte?, PrimaryColor?>("Nullable<SByte>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<sbyte?, float?>("Nullable<SByte>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<sbyte?, float?>("Nullable<SByte>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, Guid?>("Nullable<SByte>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<sbyte?, Guid?>("Nullable<SByte>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<sbyte?, int?>("Nullable<SByte>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<sbyte?, int?>("Nullable<SByte>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, long?>("Nullable<SByte>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<sbyte?, long?>("Nullable<SByte>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, sbyte?>("Nullable<SByte>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<sbyte?, sbyte?>("Nullable<SByte>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, short?>("Nullable<SByte>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<sbyte?, short?>("Nullable<SByte>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, TimeSpan?>("Nullable<SByte>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<sbyte?, TimeSpan?>("Nullable<SByte>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<sbyte?, uint?>("Nullable<SByte>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<sbyte?, uint?>("Nullable<SByte>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, ulong?>("Nullable<SByte>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<sbyte?, ulong?>("Nullable<SByte>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<sbyte?, ushort?>("Nullable<SByte>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<sbyte?, ushort?>("Nullable<SByte>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<sbyte?, IInterface>("Nullable<SByte>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<sbyte?, IInterface>("Nullable<SByte>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<sbyte?, BaseClass>("Nullable<SByte>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<sbyte?, BaseClass>("Nullable<SByte>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<sbyte?, DerivedClass>("Nullable<SByte>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<sbyte?, DerivedClass>("Nullable<SByte>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<Short>ToXXX
                new object []
                {
                    "Nullable<Short>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<short?, bool>("Nullable<Short>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<short?, bool>("Nullable<Short>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<short?, byte>("Nullable<Short>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<short?, byte>("Nullable<Short>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, byte[]>("Nullable<Short>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<short?, byte[]>("Nullable<Short>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<short?, char>("Nullable<Short>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<short?, char>("Nullable<Short>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<short?, DateTime>("Nullable<Short>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<short?, DateTime>("Nullable<Short>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<short?, DateTimeOffset>("Nullable<Short>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<short?, DateTimeOffset>("Nullable<Short>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<short?, decimal>("Nullable<Short>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<short?, decimal>("Nullable<Short>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, double>("Nullable<Short>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<short?, double>("Nullable<Short>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, PrimaryColor>("Nullable<Short>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<short?, PrimaryColor>("Nullable<Short>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<short?, float>("Nullable<Short>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<short?, float>("Nullable<Short>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, Guid>("Nullable<Short>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<short?, Guid>("Nullable<Short>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<short?, int>("Nullable<Short>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<short?, int>("Nullable<Short>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, long>("Nullable<Short>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<short?, long>("Nullable<Short>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, sbyte>("Nullable<Short>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<short?, sbyte>("Nullable<Short>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, short>("Nullable<Short>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<short?, short>("Nullable<Short>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, string>("Nullable<Short>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<short?, string>("Nullable<Short>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<short?, TimeSpan>("Nullable<Short>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<short?, TimeSpan>("Nullable<Short>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<short?, Type>("Nullable<Short>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<short?, Type>("Nullable<Short>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<short?, uint>("Nullable<Short>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<short?, uint>("Nullable<Short>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, ulong>("Nullable<Short>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<short?, ulong>("Nullable<Short>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, Uri>("Nullable<Short>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<short?, Uri>("Nullable<Short>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<short?, ushort>("Nullable<Short>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<short?, ushort>("Nullable<Short>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<short?, bool?>("Nullable<Short>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<short?, bool?>("Nullable<Short>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<short?, byte?>("Nullable<Short>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<short?, byte?>("Nullable<Short>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, char?>("Nullable<Short>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<short?, char?>("Nullable<Short>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<short?, DateTime?>("Nullable<Short>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<short?, DateTime?>("Nullable<Short>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<short?, DateTimeOffset?>("Nullable<Short>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<short?, DateTimeOffset?>("Nullable<Short>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<short?, decimal?>("Nullable<Short>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<short?, decimal?>("Nullable<Short>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, double?>("Nullable<Short>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<short?, double?>("Nullable<Short>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, PrimaryColor?>("Nullable<Short>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<short?, PrimaryColor?>("Nullable<Short>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<short?, float?>("Nullable<Short>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<short?, float?>("Nullable<Short>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, Guid?>("Nullable<Short>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<short?, Guid?>("Nullable<Short>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<short?, int?>("Nullable<Short>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<short?, int?>("Nullable<Short>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, long?>("Nullable<Short>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<short?, long?>("Nullable<Short>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, sbyte?>("Nullable<Short>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<short?, sbyte?>("Nullable<Short>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, short?>("Nullable<Short>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<short?, short?>("Nullable<Short>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, TimeSpan?>("Nullable<Short>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<short?, TimeSpan?>("Nullable<Short>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<short?, uint?>("Nullable<Short>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<short?, uint?>("Nullable<Short>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, ulong?>("Nullable<Short>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<short?, ulong?>("Nullable<Short>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<short?, ushort?>("Nullable<Short>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<short?, ushort?>("Nullable<Short>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<short?, IInterface>("Nullable<Short>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<short?, IInterface>("Nullable<Short>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<short?, BaseClass>("Nullable<Short>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<short?, BaseClass>("Nullable<Short>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<short?, DerivedClass>("Nullable<Short>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<short?, DerivedClass>("Nullable<Short>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<TimeSpan>ToXXX
                new object []
                {
                    "Nullable<TimeSpan>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<TimeSpan?, bool>("Nullable<TimeSpan>ToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<TimeSpan?, bool>("Nullable<TimeSpan>ToBool", TestTimeSpan, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<TimeSpan?, byte>("Nullable<TimeSpan>ToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<TimeSpan?, byte>("Nullable<TimeSpan>ToByte", TestTimeSpan, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<TimeSpan?, byte[]>("Nullable<TimeSpan>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<TimeSpan?, byte[]>("Nullable<TimeSpan>ToByteArray", TestTimeSpan, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<TimeSpan?, char>("Nullable<TimeSpan>ToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<TimeSpan?, char>("Nullable<TimeSpan>ToChar", TestTimeSpan, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<TimeSpan?, DateTime>("Nullable<TimeSpan>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<TimeSpan?, DateTime>("Nullable<TimeSpan>ToDateTime", TestTimeSpan, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<TimeSpan?, DateTimeOffset>("Nullable<TimeSpan>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<TimeSpan?, DateTimeOffset>("Nullable<TimeSpan>ToDateTimeOffset", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<TimeSpan?, decimal>("Nullable<TimeSpan>ToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<TimeSpan?, decimal>("Nullable<TimeSpan>ToDecimal", TestTimeSpan, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<TimeSpan?, double>("Nullable<TimeSpan>ToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<TimeSpan?, double>("Nullable<TimeSpan>ToDouble", TestTimeSpan, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<TimeSpan?, PrimaryColor>("Nullable<TimeSpan>ToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<TimeSpan?, PrimaryColor>("Nullable<TimeSpan>ToEnum", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<TimeSpan?, float>("Nullable<TimeSpan>ToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<TimeSpan?, float>("Nullable<TimeSpan>ToFloat", TestTimeSpan, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<TimeSpan?, Guid>("Nullable<TimeSpan>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<TimeSpan?, Guid>("Nullable<TimeSpan>ToGuid", TestTimeSpan, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<TimeSpan?, int>("Nullable<TimeSpan>ToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<TimeSpan?, int>("Nullable<TimeSpan>ToInt", TestTimeSpan, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<TimeSpan?, long>("Nullable<TimeSpan>ToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<TimeSpan?, long>("Nullable<TimeSpan>ToLong", TestTimeSpan, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<TimeSpan?, sbyte>("Nullable<TimeSpan>ToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<TimeSpan?, sbyte>("Nullable<TimeSpan>ToSByte", TestTimeSpan, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<TimeSpan?, short>("Nullable<TimeSpan>ToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<TimeSpan?, short>("Nullable<TimeSpan>ToShort", TestTimeSpan, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToString", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToString", TestTimeSpan, ConvertResult.Success, TestTimeSpanString),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormat", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormat", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormat, FormatTimeSpanContext),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormatAndFormatProvider", null, ConvertResult.Success, default(string)),
                            new TryConvertGenericTest<TimeSpan?, string>("Nullable<TimeSpan>ToStringWithFormatAndFormatProvider", TestTimeSpan, ConvertResult.Success, TestTimeSpanStringWithFormatAndFormatProvider, FormatAndFormatProviderTimeSpanContext),
                            new TryConvertGenericTest<TimeSpan?, TimeSpan>("Nullable<TimeSpan>ToTimeSpan", null, ConvertResult.Success, default(TimeSpan)),
                            new TryConvertGenericTest<TimeSpan?, TimeSpan>("Nullable<TimeSpan>ToTimeSpan", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<TimeSpan?, Type>("Nullable<TimeSpan>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<TimeSpan?, Type>("Nullable<TimeSpan>ToType", TestTimeSpan, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<TimeSpan?, uint>("Nullable<TimeSpan>ToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<TimeSpan?, uint>("Nullable<TimeSpan>ToUInt", TestTimeSpan, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<TimeSpan?, ulong>("Nullable<TimeSpan>ToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<TimeSpan?, ulong>("Nullable<TimeSpan>ToULong", TestTimeSpan, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<TimeSpan?, Uri>("Nullable<TimeSpan>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<TimeSpan?, Uri>("Nullable<TimeSpan>ToUri", TestTimeSpan, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<TimeSpan?, ushort>("Nullable<TimeSpan>ToUShort", null, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<TimeSpan?, ushort>("Nullable<TimeSpan>ToUShort", TestTimeSpan, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<TimeSpan?, bool?>("Nullable<TimeSpan>ToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<TimeSpan?, bool?>("Nullable<TimeSpan>ToNullable<Bool>", TestTimeSpan, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<TimeSpan?, byte?>("Nullable<TimeSpan>ToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<TimeSpan?, byte?>("Nullable<TimeSpan>ToNullable<Byte>", TestTimeSpan, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<TimeSpan?, char?>("Nullable<TimeSpan>ToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<TimeSpan?, char?>("Nullable<TimeSpan>ToNullable<Char>", TestTimeSpan, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<TimeSpan?, DateTime?>("Nullable<TimeSpan>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<TimeSpan?, DateTime?>("Nullable<TimeSpan>ToNullable<DateTime>", TestTimeSpan, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<TimeSpan?, DateTimeOffset?>("Nullable<TimeSpan>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<TimeSpan?, DateTimeOffset?>("Nullable<TimeSpan>ToNullable<DateTimeOffset>", TestTimeSpan, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<TimeSpan?, decimal?>("Nullable<TimeSpan>ToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<TimeSpan?, decimal?>("Nullable<TimeSpan>ToNullable<Decimal>", TestTimeSpan, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<TimeSpan?, double?>("Nullable<TimeSpan>ToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<TimeSpan?, double?>("Nullable<TimeSpan>ToNullable<Double>", TestTimeSpan, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<TimeSpan?, PrimaryColor?>("Nullable<TimeSpan>ToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<TimeSpan?, PrimaryColor?>("Nullable<TimeSpan>ToNullable<Enum>", TestTimeSpan, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<TimeSpan?, float?>("Nullable<TimeSpan>ToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<TimeSpan?, float?>("Nullable<TimeSpan>ToNullable<Float>", TestTimeSpan, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<TimeSpan?, Guid?>("Nullable<TimeSpan>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<TimeSpan?, Guid?>("Nullable<TimeSpan>ToNullable<Guid>", TestTimeSpan, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<TimeSpan?, int?>("Nullable<TimeSpan>ToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<TimeSpan?, int?>("Nullable<TimeSpan>ToNullable<Int>", TestTimeSpan, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<TimeSpan?, long?>("Nullable<TimeSpan>ToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<TimeSpan?, long?>("Nullable<TimeSpan>ToNullable<Long>", TestTimeSpan, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<TimeSpan?, sbyte?>("Nullable<TimeSpan>ToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<TimeSpan?, sbyte?>("Nullable<TimeSpan>ToNullable<SByte>", TestTimeSpan, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<TimeSpan?, short?>("Nullable<TimeSpan>ToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<TimeSpan?, short?>("Nullable<TimeSpan>ToNullable<Short>", TestTimeSpan, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<TimeSpan?, TimeSpan?>("Nullable<TimeSpan>ToNullable<TimeSpan>", null, ConvertResult.Success, new TimeSpan?()),
                            new TryConvertGenericTest<TimeSpan?, TimeSpan?>("Nullable<TimeSpan>ToNullable<TimeSpan>", TestTimeSpan, ConvertResult.Success, TestTimeSpan),
                            new TryConvertGenericTest<TimeSpan?, uint?>("Nullable<TimeSpan>ToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<TimeSpan?, uint?>("Nullable<TimeSpan>ToNullable<UInt>", TestTimeSpan, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<TimeSpan?, ulong?>("Nullable<TimeSpan>ToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<TimeSpan?, ulong?>("Nullable<TimeSpan>ToNullable<ULong>", TestTimeSpan, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<TimeSpan?, ushort?>("Nullable<TimeSpan>ToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<TimeSpan?, ushort?>("Nullable<TimeSpan>ToNullable<UShort>", TestTimeSpan, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<TimeSpan?, IInterface>("Nullable<TimeSpan>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<TimeSpan?, IInterface>("Nullable<TimeSpan>ToInterface", TestTimeSpan, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<TimeSpan?, BaseClass>("Nullable<TimeSpan>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<TimeSpan?, BaseClass>("Nullable<TimeSpan>ToBaseClass", TestTimeSpan, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<TimeSpan?, DerivedClass>("Nullable<TimeSpan>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<TimeSpan?, DerivedClass>("Nullable<TimeSpan>ToDerivedClass", TestTimeSpan, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<UInt>ToXXX
                new object []
                {
                    "Nullable<UInt>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<uint?, bool>("Nullable<UInt>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<uint?, bool>("Nullable<UInt>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<uint?, byte>("Nullable<UInt>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<uint?, byte>("Nullable<UInt>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, byte[]>("Nullable<UInt>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<uint?, byte[]>("Nullable<UInt>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<uint?, char>("Nullable<UInt>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<uint?, char>("Nullable<UInt>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<uint?, DateTime>("Nullable<UInt>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<uint?, DateTime>("Nullable<UInt>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<uint?, DateTimeOffset>("Nullable<UInt>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<uint?, DateTimeOffset>("Nullable<UInt>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<uint?, decimal>("Nullable<UInt>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<uint?, decimal>("Nullable<UInt>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, double>("Nullable<UInt>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<uint?, double>("Nullable<UInt>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, PrimaryColor>("Nullable<UInt>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<uint?, PrimaryColor>("Nullable<UInt>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<uint?, float>("Nullable<UInt>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<uint?, float>("Nullable<UInt>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, Guid>("Nullable<UInt>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<uint?, Guid>("Nullable<UInt>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<uint?, int>("Nullable<UInt>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<uint?, int>("Nullable<UInt>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, long>("Nullable<UInt>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<uint?, long>("Nullable<UInt>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, sbyte>("Nullable<UInt>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<uint?, sbyte>("Nullable<UInt>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, short>("Nullable<UInt>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<uint?, short>("Nullable<UInt>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, string>("Nullable<UInt>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<uint?, string>("Nullable<UInt>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<uint?, TimeSpan>("Nullable<UInt>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<uint?, TimeSpan>("Nullable<UInt>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<uint?, Type>("Nullable<UInt>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<uint?, Type>("Nullable<UInt>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<uint?, uint>("Nullable<UInt>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<uint?, uint>("Nullable<UInt>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, ulong>("Nullable<UInt>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<uint?, ulong>("Nullable<UInt>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, Uri>("Nullable<UInt>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<uint?, Uri>("Nullable<UInt>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<uint?, ushort>("Nullable<UInt>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<uint?, ushort>("Nullable<UInt>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<uint?, bool?>("Nullable<UInt>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<uint?, bool?>("Nullable<UInt>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<uint?, byte?>("Nullable<UInt>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<uint?, byte?>("Nullable<UInt>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, char?>("Nullable<UInt>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<uint?, char?>("Nullable<UInt>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<uint?, DateTime?>("Nullable<UInt>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<uint?, DateTime?>("Nullable<UInt>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<uint?, DateTimeOffset?>("Nullable<UInt>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<uint?, DateTimeOffset?>("Nullable<UInt>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<uint?, decimal?>("Nullable<UInt>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<uint?, decimal?>("Nullable<UInt>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, double?>("Nullable<UInt>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<uint?, double?>("Nullable<UInt>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, PrimaryColor?>("Nullable<UInt>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<uint?, PrimaryColor?>("Nullable<UInt>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<uint?, float?>("Nullable<UInt>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<uint?, float?>("Nullable<UInt>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, Guid?>("Nullable<UInt>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<uint?, Guid?>("Nullable<UInt>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<uint?, int?>("Nullable<UInt>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<uint?, int?>("Nullable<UInt>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, long?>("Nullable<UInt>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<uint?, long?>("Nullable<UInt>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, sbyte?>("Nullable<UInt>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<uint?, sbyte?>("Nullable<UInt>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, short?>("Nullable<UInt>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<uint?, short?>("Nullable<UInt>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, TimeSpan?>("Nullable<UInt>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<uint?, TimeSpan?>("Nullable<UInt>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<uint?, uint?>("Nullable<UInt>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<uint?, uint?>("Nullable<UInt>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, ulong?>("Nullable<UInt>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<uint?, ulong?>("Nullable<UInt>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<uint?, ushort?>("Nullable<UInt>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<uint?, ushort?>("Nullable<UInt>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<uint?, IInterface>("Nullable<UInt>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<uint?, IInterface>("Nullable<UInt>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<uint?, BaseClass>("Nullable<UInt>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<uint?, BaseClass>("Nullable<UInt>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<uint?, DerivedClass>("Nullable<UInt>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<uint?, DerivedClass>("Nullable<UInt>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<ULong>ToXXX
                new object []
                {
                    "Nullable<ULong>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<ulong?, bool>("Nullable<ULong>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<ulong?, bool>("Nullable<ULong>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ulong?, byte>("Nullable<ULong>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<ulong?, byte>("Nullable<ULong>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, byte[]>("Nullable<ULong>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ulong?, byte[]>("Nullable<ULong>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ulong?, char>("Nullable<ULong>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<ulong?, char>("Nullable<ULong>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ulong?, DateTime>("Nullable<ULong>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ulong?, DateTime>("Nullable<ULong>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ulong?, DateTimeOffset>("Nullable<ULong>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ulong?, DateTimeOffset>("Nullable<ULong>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ulong?, decimal>("Nullable<ULong>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<ulong?, decimal>("Nullable<ULong>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, double>("Nullable<ULong>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<ulong?, double>("Nullable<ULong>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, PrimaryColor>("Nullable<ULong>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<ulong?, PrimaryColor>("Nullable<ULong>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ulong?, float>("Nullable<ULong>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<ulong?, float>("Nullable<ULong>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, Guid>("Nullable<ULong>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ulong?, Guid>("Nullable<ULong>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ulong?, int>("Nullable<ULong>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<ulong?, int>("Nullable<ULong>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, long>("Nullable<ULong>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<ulong?, long>("Nullable<ULong>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, sbyte>("Nullable<ULong>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<ulong?, sbyte>("Nullable<ULong>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, short>("Nullable<ULong>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<ulong?, short>("Nullable<ULong>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, string>("Nullable<ULong>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<ulong?, string>("Nullable<ULong>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<ulong?, TimeSpan>("Nullable<ULong>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ulong?, TimeSpan>("Nullable<ULong>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ulong?, Type>("Nullable<ULong>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ulong?, Type>("Nullable<ULong>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ulong?, uint>("Nullable<ULong>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<ulong?, uint>("Nullable<ULong>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, ulong>("Nullable<ULong>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<ulong?, ulong>("Nullable<ULong>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, Uri>("Nullable<ULong>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ulong?, Uri>("Nullable<ULong>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ulong?, ushort>("Nullable<ULong>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<ulong?, ushort>("Nullable<ULong>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<ulong?, bool?>("Nullable<ULong>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<ulong?, bool?>("Nullable<ULong>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ulong?, byte?>("Nullable<ULong>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<ulong?, byte?>("Nullable<ULong>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, char?>("Nullable<ULong>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<ulong?, char?>("Nullable<ULong>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ulong?, DateTime?>("Nullable<ULong>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ulong?, DateTime?>("Nullable<ULong>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ulong?, DateTimeOffset?>("Nullable<ULong>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ulong?, DateTimeOffset?>("Nullable<ULong>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ulong?, decimal?>("Nullable<ULong>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<ulong?, decimal?>("Nullable<ULong>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, double?>("Nullable<ULong>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<ulong?, double?>("Nullable<ULong>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, PrimaryColor?>("Nullable<ULong>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<ulong?, PrimaryColor?>("Nullable<ULong>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ulong?, float?>("Nullable<ULong>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<ulong?, float?>("Nullable<ULong>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, Guid?>("Nullable<ULong>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ulong?, Guid?>("Nullable<ULong>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ulong?, int?>("Nullable<ULong>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<ulong?, int?>("Nullable<ULong>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, long?>("Nullable<ULong>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<ulong?, long?>("Nullable<ULong>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, sbyte?>("Nullable<ULong>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<ulong?, sbyte?>("Nullable<ULong>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, short?>("Nullable<ULong>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<ulong?, short?>("Nullable<ULong>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, TimeSpan?>("Nullable<ULong>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ulong?, TimeSpan?>("Nullable<ULong>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ulong?, uint?>("Nullable<ULong>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<ulong?, uint?>("Nullable<ULong>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, ulong?>("Nullable<ULong>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<ulong?, ulong?>("Nullable<ULong>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ulong?, ushort?>("Nullable<ULong>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<ulong?, ushort?>("Nullable<ULong>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<ulong?, IInterface>("Nullable<ULong>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ulong?, IInterface>("Nullable<ULong>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ulong?, BaseClass>("Nullable<ULong>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ulong?, BaseClass>("Nullable<ULong>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ulong?, DerivedClass>("Nullable<ULong>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<ulong?, DerivedClass>("Nullable<ULong>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region Nullable<UShort>ToXXX
                new object []
                {
                    "Nullable<UShort>ToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<ushort?, bool>("Nullable<UShort>ToBool", null, ConvertResult.Success, default(bool)),
                            new TryConvertGenericTest<ushort?, bool>("Nullable<UShort>ToBool", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ushort?, byte>("Nullable<UShort>ToByte", null, ConvertResult.Success, default(byte)),
                            new TryConvertGenericTest<ushort?, byte>("Nullable<UShort>ToByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, byte[]>("Nullable<UShort>ToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ushort?, byte[]>("Nullable<UShort>ToByteArray", 42, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<ushort?, char>("Nullable<UShort>ToChar", null, ConvertResult.Success, default(char)),
                            new TryConvertGenericTest<ushort?, char>("Nullable<UShort>ToChar", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ushort?, DateTime>("Nullable<UShort>ToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ushort?, DateTime>("Nullable<UShort>ToDateTime", 42, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<ushort?, DateTimeOffset>("Nullable<UShort>ToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ushort?, DateTimeOffset>("Nullable<UShort>ToDateTimeOffset", 42, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<ushort?, decimal>("Nullable<UShort>ToDecimal", null, ConvertResult.Success, default(decimal)),
                            new TryConvertGenericTest<ushort?, decimal>("Nullable<UShort>ToDecimal", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, double>("Nullable<UShort>ToDouble", null, ConvertResult.Success, default(double)),
                            new TryConvertGenericTest<ushort?, double>("Nullable<UShort>ToDouble", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, PrimaryColor>("Nullable<UShort>ToEnum", null, ConvertResult.Success, default(PrimaryColor)),
                            new TryConvertGenericTest<ushort?, PrimaryColor>("Nullable<UShort>ToEnum", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ushort?, float>("Nullable<UShort>ToFloat", null, ConvertResult.Success, default(float)),
                            new TryConvertGenericTest<ushort?, float>("Nullable<UShort>ToFloat", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, Guid>("Nullable<UShort>ToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ushort?, Guid>("Nullable<UShort>ToGuid", 42, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<ushort?, int>("Nullable<UShort>ToInt", null, ConvertResult.Success, default(int)),
                            new TryConvertGenericTest<ushort?, int>("Nullable<UShort>ToInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, long>("Nullable<UShort>ToLong", null, ConvertResult.Success, default(long)),
                            new TryConvertGenericTest<ushort?, long>("Nullable<UShort>ToLong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, sbyte>("Nullable<UShort>ToSByte", null, ConvertResult.Success, default(sbyte)),
                            new TryConvertGenericTest<ushort?, sbyte>("Nullable<UShort>ToSByte", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, short>("Nullable<UShort>ToShort", null, ConvertResult.Success, default(short)),
                            new TryConvertGenericTest<ushort?, short>("Nullable<UShort>ToShort", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, string>("Nullable<UShort>ToString", null, ConvertResult.Success, null),
                            new TryConvertGenericTest<ushort?, string>("Nullable<UShort>ToString", 42, ConvertResult.Success, "42"),
                            new TryConvertGenericTest<ushort?, TimeSpan>("Nullable<UShort>ToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ushort?, TimeSpan>("Nullable<UShort>ToTimeSpan", 42, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<ushort?, Type>("Nullable<UShort>ToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ushort?, Type>("Nullable<UShort>ToType", 42, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<ushort?, uint>("Nullable<UShort>ToUInt", null, ConvertResult.Success, default(uint)),
                            new TryConvertGenericTest<ushort?, uint>("Nullable<UShort>ToUInt", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, ulong>("Nullable<UShort>ToULong", null, ConvertResult.Success, default(ulong)),
                            new TryConvertGenericTest<ushort?, ulong>("Nullable<UShort>ToULong", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, Uri>("Nullable<UShort>ToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ushort?, Uri>("Nullable<UShort>ToUri", 42, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<ushort?, ushort>("Nullable<UShort>ToUShort", null, ConvertResult.Success, default(ushort)),
                            new TryConvertGenericTest<ushort?, ushort>("Nullable<UShort>ToUShort", 42, ConvertResult.Success, 42),

                            // Nullable Types
                            new TryConvertGenericTest<ushort?, bool?>("Nullable<UShort>ToNullable<Bool>", null, ConvertResult.Success, new bool?()),
                            new TryConvertGenericTest<ushort?, bool?>("Nullable<UShort>ToNullable<Bool>", 42, ConvertResult.Success, true),
                            new TryConvertGenericTest<ushort?, byte?>("Nullable<UShort>ToNullable<Byte>", null, ConvertResult.Success, new byte?()),
                            new TryConvertGenericTest<ushort?, byte?>("Nullable<UShort>ToNullable<Byte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, char?>("Nullable<UShort>ToNullable<Char>", null, ConvertResult.Success, new char?()),
                            new TryConvertGenericTest<ushort?, char?>("Nullable<UShort>ToNullable<Char>", 42, ConvertResult.Success, '*'),
                            new TryConvertGenericTest<ushort?, DateTime?>("Nullable<UShort>ToNullable<DateTime>", null, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ushort?, DateTime?>("Nullable<UShort>ToNullable<DateTime>", 42, ConvertResult.Failure, default(DateTime?)),
                            new TryConvertGenericTest<ushort?, DateTimeOffset?>("Nullable<UShort>ToNullable<DateTimeOffset>", null, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ushort?, DateTimeOffset?>("Nullable<UShort>ToNullable<DateTimeOffset>", 42, ConvertResult.Failure, default(DateTimeOffset?)),
                            new TryConvertGenericTest<ushort?, decimal?>("Nullable<UShort>ToNullable<Decimal>", null, ConvertResult.Success, new decimal?()),
                            new TryConvertGenericTest<ushort?, decimal?>("Nullable<UShort>ToNullable<Decimal>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, double?>("Nullable<UShort>ToNullable<Double>", null, ConvertResult.Success, new double?()),
                            new TryConvertGenericTest<ushort?, double?>("Nullable<UShort>ToNullable<Double>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, PrimaryColor?>("Nullable<UShort>ToNullable<Enum>", null, ConvertResult.Success, new PrimaryColor?()),
                            new TryConvertGenericTest<ushort?, PrimaryColor?>("Nullable<UShort>ToNullable<Enum>", 42, ConvertResult.Success, PrimaryColor.Blue),
                            new TryConvertGenericTest<ushort?, float?>("Nullable<UShort>ToNullable<Float>", null, ConvertResult.Success, new float?()),
                            new TryConvertGenericTest<ushort?, float?>("Nullable<UShort>ToNullable<Float>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, Guid?>("Nullable<UShort>ToNullable<Guid>", null, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ushort?, Guid?>("Nullable<UShort>ToNullable<Guid>", 42, ConvertResult.Failure, default(Guid?)),
                            new TryConvertGenericTest<ushort?, int?>("Nullable<UShort>ToNullable<Int>", null, ConvertResult.Success, new int?()),
                            new TryConvertGenericTest<ushort?, int?>("Nullable<UShort>ToNullable<Int>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, long?>("Nullable<UShort>ToNullable<Long>", null, ConvertResult.Success, new long?()),
                            new TryConvertGenericTest<ushort?, long?>("Nullable<UShort>ToNullable<Long>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, sbyte?>("Nullable<UShort>ToNullable<SByte>", null, ConvertResult.Success, new sbyte?()),
                            new TryConvertGenericTest<ushort?, sbyte?>("Nullable<UShort>ToNullable<SByte>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, short?>("Nullable<UShort>ToNullable<Short>", null, ConvertResult.Success, new short?()),
                            new TryConvertGenericTest<ushort?, short?>("Nullable<UShort>ToNullable<Short>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, TimeSpan?>("Nullable<UShort>ToNullable<TimeSpan>", null, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ushort?, TimeSpan?>("Nullable<UShort>ToNullable<TimeSpan>", 42, ConvertResult.Failure, default(TimeSpan?)),
                            new TryConvertGenericTest<ushort?, uint?>("Nullable<UShort>ToNullable<UInt>", null, ConvertResult.Success, new uint?()),
                            new TryConvertGenericTest<ushort?, uint?>("Nullable<UShort>ToNullable<UInt>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, ulong?>("Nullable<UShort>ToNullable<ULong>", null, ConvertResult.Success, new ulong?()),
                            new TryConvertGenericTest<ushort?, ulong?>("Nullable<UShort>ToNullable<ULong>", 42, ConvertResult.Success, 42),
                            new TryConvertGenericTest<ushort?, ushort?>("Nullable<UShort>ToNullable<UShort>", null, ConvertResult.Success, new ushort?()),
                            new TryConvertGenericTest<ushort?, ushort?>("Nullable<UShort>ToNullable<UShort>", 42, ConvertResult.Success, 42),

                            // Interface/Class Types
                            new TryConvertGenericTest<ushort?, IInterface>("Nullable<UShort>ToInterface", null, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ushort?, IInterface>("Nullable<UShort>ToInterface", 42, ConvertResult.Failure, default(IInterface)),
                            new TryConvertGenericTest<ushort?, BaseClass>("Nullable<UShort>ToBaseClass", null, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ushort?, BaseClass>("Nullable<UShort>ToBaseClass", 42, ConvertResult.Failure, default(BaseClass)),
                            new TryConvertGenericTest<ushort?, DerivedClass>("Nullable<UShort>ToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<ushort?, DerivedClass>("Nullable<UShort>ToDerivedClass", 42, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                // Interface/Class Types ////////////////////////////////////

                #region BaseClassToXXX
                new object []
                {
                    "BaseClassToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<BaseClass, bool>("BaseClassToBool", TestBaseClass, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<BaseClass, bool>("BaseClassToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<BaseClass, byte>("BaseClassToByte", TestBaseClass, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<BaseClass, byte>("BaseClassToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<BaseClass, byte[]>("BaseClassToByteArray", TestBaseClass, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<BaseClass, byte[]>("BaseClassToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<BaseClass, char>("BaseClassToChar", TestBaseClass, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<BaseClass, char>("BaseClassToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<BaseClass, DateTime>("BaseClassToDateTime", TestBaseClass, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<BaseClass, DateTime>("BaseClassToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<BaseClass, DateTimeOffset>("BaseClassToDateTimeOffset", TestBaseClass, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<BaseClass, DateTimeOffset>("BaseClassToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<BaseClass, decimal>("BaseClassToDecimal", TestBaseClass, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<BaseClass, decimal>("BaseClassToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<BaseClass, double>("BaseClassToDouble", TestBaseClass, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<BaseClass, double>("BaseClassToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<BaseClass, PrimaryColor>("BaseClassToEnum", TestBaseClass, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<BaseClass, PrimaryColor>("BaseClassToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<BaseClass, float>("BaseClassToFloat", TestBaseClass, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<BaseClass, float>("BaseClassToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<BaseClass, Guid>("BaseClassToGuid", TestBaseClass, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<BaseClass, Guid>("BaseClassToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<BaseClass, int>("BaseClassToInt", TestBaseClass, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<BaseClass, int>("BaseClassToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<BaseClass, long>("BaseClassToLong", TestBaseClass, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<BaseClass, long>("BaseClassToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<BaseClass, sbyte>("BaseClassToSByte", TestBaseClass, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<BaseClass, sbyte>("BaseClassToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<BaseClass, short>("BaseClassToShort", TestBaseClass, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<BaseClass, short>("BaseClassToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<BaseClass, string>("BaseClassToString", TestBaseClass, ConvertResult.Failure, default(string)),
                            new TryConvertGenericTest<BaseClass, string>("BaseClassToString", null, ConvertResult.Failure, default(string)),
                            new TryConvertGenericTest<BaseClass, TimeSpan>("BaseClassToTimeSpan", TestBaseClass, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<BaseClass, TimeSpan>("BaseClassToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<BaseClass, Type>("BaseClassToType", TestBaseClass, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<BaseClass, Type>("BaseClassToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<BaseClass, uint>("BaseClassToUInt", TestBaseClass, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<BaseClass, uint>("BaseClassToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<BaseClass, ulong>("BaseClassToULong", TestBaseClass, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<BaseClass, ulong>("BaseClassToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<BaseClass, Uri>("BaseClassToUri", TestBaseClass, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<BaseClass, Uri>("BaseClassToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<BaseClass, ushort>("BaseClassToUShort", TestBaseClass, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<BaseClass, ushort>("BaseClassToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<BaseClass, bool?>("BaseClassToNullable<Bool>", TestBaseClass, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<BaseClass, bool?>("BaseClassToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<BaseClass, byte?>("BaseClassToNullable<Byte>", TestBaseClass, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<BaseClass, byte?>("BaseClassToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<BaseClass, char?>("BaseClassToNullable<Char>", TestBaseClass, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<BaseClass, char?>("BaseClassToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<BaseClass, DateTime?>("BaseClassToNullable<DateTime>", TestBaseClass, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<BaseClass, DateTime?>("BaseClassToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<BaseClass, DateTimeOffset?>("BaseClassToNullable<DateTimeOffset>", TestBaseClass, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<BaseClass, DateTimeOffset?>("BaseClassToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<BaseClass, decimal?>("BaseClassToNullable<Decimal>", TestBaseClass, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<BaseClass, decimal?>("BaseClassToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<BaseClass, double?>("BaseClassToNullable<Double>", TestBaseClass, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<BaseClass, double?>("BaseClassToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<BaseClass, PrimaryColor?>("BaseClassToNullable<Enum>", TestBaseClass, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<BaseClass, PrimaryColor?>("BaseClassToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<BaseClass, float?>("BaseClassToNullable<Float>", TestBaseClass, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<BaseClass, float?>("BaseClassToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<BaseClass, Guid?>("BaseClassToNullable<Guid>", TestBaseClass, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<BaseClass, Guid?>("BaseClassToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<BaseClass, int?>("BaseClassToNullable<Int>", TestBaseClass, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<BaseClass, int?>("BaseClassToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<BaseClass, long?>("BaseClassToNullable<Long>", TestBaseClass, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<BaseClass, long?>("BaseClassToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<BaseClass, sbyte?>("BaseClassToNullable<SByte>", TestBaseClass, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<BaseClass, sbyte?>("BaseClassToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<BaseClass, short?>("BaseClassToNullable<Short>", TestBaseClass, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<BaseClass, short?>("BaseClassToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<BaseClass, TimeSpan?>("BaseClassToNullable<TimeSpan>", TestBaseClass, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<BaseClass, TimeSpan?>("BaseClassToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<BaseClass, uint?>("BaseClassToNullable<UInt>", TestBaseClass, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<BaseClass, uint?>("BaseClassToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<BaseClass, ulong?>("BaseClassToNullable<ULong>", TestBaseClass, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<BaseClass, ulong?>("BaseClassToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<BaseClass, ushort?>("BaseClassToNullable<UShort>", TestBaseClass, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<BaseClass, ushort?>("BaseClassToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<BaseClass, IInterface>("BaseClassToInterface", TestBaseClass, ConvertResult.Success, TestBaseClass),
                            new TryConvertGenericTest<BaseClass, IInterface>("BaseClassToInterface", null, ConvertResult.Success, default(IInterface)),
                            new TryConvertGenericTest<BaseClass, BaseClass>("BaseClassToBaseClass", TestBaseClass, ConvertResult.Success, TestBaseClass),
                            new TryConvertGenericTest<BaseClass, BaseClass>("BaseClassToBaseClass", null, ConvertResult.Success, default(BaseClass)),
                            new TryConvertGenericTest<BaseClass, DerivedClass>("BaseClassToDerivedClass", TestBaseClass, ConvertResult.Failure, default(DerivedClass)),
                            new TryConvertGenericTest<BaseClass, DerivedClass>("BaseClassToDerivedClass", null, ConvertResult.Failure, default(DerivedClass)),
                        }
                },
                #endregion

                #region DerivedClassToXXX
                new object []
                {
                    "DerivedClassToXXX - Generic",
                    new object []
                        {
                            // Simple Types
                            new TryConvertGenericTest<DerivedClass, bool>("DerivedClassToBool", TestDerivedClass, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DerivedClass, bool>("DerivedClassToBool", null, ConvertResult.Failure, default(bool)),
                            new TryConvertGenericTest<DerivedClass, byte>("DerivedClassToByte", TestDerivedClass, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DerivedClass, byte>("DerivedClassToByte", null, ConvertResult.Failure, default(byte)),
                            new TryConvertGenericTest<DerivedClass, byte[]>("DerivedClassToByteArray", TestDerivedClass, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DerivedClass, byte[]>("DerivedClassToByteArray", null, ConvertResult.Failure, default(byte[])),
                            new TryConvertGenericTest<DerivedClass, char>("DerivedClassToChar", TestDerivedClass, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DerivedClass, char>("DerivedClassToChar", null, ConvertResult.Failure, default(char)),
                            new TryConvertGenericTest<DerivedClass, DateTime>("DerivedClassToDateTime", TestDerivedClass, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<DerivedClass, DateTime>("DerivedClassToDateTime", null, ConvertResult.Failure, default(DateTime)),
                            new TryConvertGenericTest<DerivedClass, DateTimeOffset>("DerivedClassToDateTimeOffset", TestDerivedClass, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<DerivedClass, DateTimeOffset>("DerivedClassToDateTimeOffset", null, ConvertResult.Failure, default(DateTimeOffset)),
                            new TryConvertGenericTest<DerivedClass, decimal>("DerivedClassToDecimal", TestDerivedClass, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DerivedClass, decimal>("DerivedClassToDecimal", null, ConvertResult.Failure, default(decimal)),
                            new TryConvertGenericTest<DerivedClass, double>("DerivedClassToDouble", TestDerivedClass, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DerivedClass, double>("DerivedClassToDouble", null, ConvertResult.Failure, default(double)),
                            new TryConvertGenericTest<DerivedClass, PrimaryColor>("DerivedClassToEnum", TestDerivedClass, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DerivedClass, PrimaryColor>("DerivedClassToEnum", null, ConvertResult.Failure, default(PrimaryColor)),
                            new TryConvertGenericTest<DerivedClass, float>("DerivedClassToFloat", TestDerivedClass, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DerivedClass, float>("DerivedClassToFloat", null, ConvertResult.Failure, default(float)),
                            new TryConvertGenericTest<DerivedClass, Guid>("DerivedClassToGuid", TestDerivedClass, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DerivedClass, Guid>("DerivedClassToGuid", null, ConvertResult.Failure, default(Guid)),
                            new TryConvertGenericTest<DerivedClass, int>("DerivedClassToInt", TestDerivedClass, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DerivedClass, int>("DerivedClassToInt", null, ConvertResult.Failure, default(int)),
                            new TryConvertGenericTest<DerivedClass, long>("DerivedClassToLong", TestDerivedClass, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DerivedClass, long>("DerivedClassToLong", null, ConvertResult.Failure, default(long)),
                            new TryConvertGenericTest<DerivedClass, sbyte>("DerivedClassToSByte", TestDerivedClass, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DerivedClass, sbyte>("DerivedClassToSByte", null, ConvertResult.Failure, default(sbyte)),
                            new TryConvertGenericTest<DerivedClass, short>("DerivedClassToShort", TestDerivedClass, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DerivedClass, short>("DerivedClassToShort", null, ConvertResult.Failure, default(short)),
                            new TryConvertGenericTest<DerivedClass, string>("DerivedClassToString", TestDerivedClass, ConvertResult.Failure, default(string)),
                            new TryConvertGenericTest<DerivedClass, string>("DerivedClassToString", null, ConvertResult.Failure, default(string)),
                            new TryConvertGenericTest<DerivedClass, TimeSpan>("DerivedClassToTimeSpan", TestDerivedClass, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DerivedClass, TimeSpan>("DerivedClassToTimeSpan", null, ConvertResult.Failure, default(TimeSpan)),
                            new TryConvertGenericTest<DerivedClass, Type>("DerivedClassToType", TestDerivedClass, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DerivedClass, Type>("DerivedClassToType", null, ConvertResult.Failure, default(Type)),
                            new TryConvertGenericTest<DerivedClass, uint>("DerivedClassToUInt", TestDerivedClass, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DerivedClass, uint>("DerivedClassToUInt", null, ConvertResult.Failure, default(uint)),
                            new TryConvertGenericTest<DerivedClass, ulong>("DerivedClassToULong", TestDerivedClass, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DerivedClass, ulong>("DerivedClassToULong", null, ConvertResult.Failure, default(ulong)),
                            new TryConvertGenericTest<DerivedClass, Uri>("DerivedClassToUri", TestDerivedClass, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DerivedClass, Uri>("DerivedClassToUri", null, ConvertResult.Failure, default(Uri)),
                            new TryConvertGenericTest<DerivedClass, ushort>("DerivedClassToUShort", TestDerivedClass, ConvertResult.Failure, default(ushort)),
                            new TryConvertGenericTest<DerivedClass, ushort>("DerivedClassToUShort", null, ConvertResult.Failure, default(ushort)),

                            // Nullable Types
                            new TryConvertGenericTest<DerivedClass, bool?>("DerivedClassToNullable<Bool>", TestDerivedClass, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DerivedClass, bool?>("DerivedClassToNullable<Bool>", null, ConvertResult.Failure, default(bool?)),
                            new TryConvertGenericTest<DerivedClass, byte?>("DerivedClassToNullable<Byte>", TestDerivedClass, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DerivedClass, byte?>("DerivedClassToNullable<Byte>", null, ConvertResult.Failure, default(byte?)),
                            new TryConvertGenericTest<DerivedClass, char?>("DerivedClassToNullable<Char>", TestDerivedClass, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DerivedClass, char?>("DerivedClassToNullable<Char>", null, ConvertResult.Failure, default(char?)),
                            new TryConvertGenericTest<DerivedClass, DateTime?>("DerivedClassToNullable<DateTime>", TestDerivedClass, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<DerivedClass, DateTime?>("DerivedClassToNullable<DateTime>", null, ConvertResult.Failure, new DateTime?()),
                            new TryConvertGenericTest<DerivedClass, DateTimeOffset?>("DerivedClassToNullable<DateTimeOffset>", TestDerivedClass, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<DerivedClass, DateTimeOffset?>("DerivedClassToNullable<DateTimeOffset>", null, ConvertResult.Failure, new DateTimeOffset?()),
                            new TryConvertGenericTest<DerivedClass, decimal?>("DerivedClassToNullable<Decimal>", TestDerivedClass, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DerivedClass, decimal?>("DerivedClassToNullable<Decimal>", null, ConvertResult.Failure, default(decimal?)),
                            new TryConvertGenericTest<DerivedClass, double?>("DerivedClassToNullable<Double>", TestDerivedClass, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DerivedClass, double?>("DerivedClassToNullable<Double>", null, ConvertResult.Failure, default(double?)),
                            new TryConvertGenericTest<DerivedClass, PrimaryColor?>("DerivedClassToNullable<Enum>", TestDerivedClass, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DerivedClass, PrimaryColor?>("DerivedClassToNullable<Enum>", null, ConvertResult.Failure, default(PrimaryColor?)),
                            new TryConvertGenericTest<DerivedClass, float?>("DerivedClassToNullable<Float>", TestDerivedClass, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DerivedClass, float?>("DerivedClassToNullable<Float>", null, ConvertResult.Failure, default(float?)),
                            new TryConvertGenericTest<DerivedClass, Guid?>("DerivedClassToNullable<Guid>", TestDerivedClass, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<DerivedClass, Guid?>("DerivedClassToNullable<Guid>", null, ConvertResult.Failure, new Guid?()),
                            new TryConvertGenericTest<DerivedClass, int?>("DerivedClassToNullable<Int>", TestDerivedClass, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DerivedClass, int?>("DerivedClassToNullable<Int>", null, ConvertResult.Failure, default(int?)),
                            new TryConvertGenericTest<DerivedClass, long?>("DerivedClassToNullable<Long>", TestDerivedClass, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DerivedClass, long?>("DerivedClassToNullable<Long>", null, ConvertResult.Failure, default(long?)),
                            new TryConvertGenericTest<DerivedClass, sbyte?>("DerivedClassToNullable<SByte>", TestDerivedClass, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DerivedClass, sbyte?>("DerivedClassToNullable<SByte>", null, ConvertResult.Failure, default(sbyte?)),
                            new TryConvertGenericTest<DerivedClass, short?>("DerivedClassToNullable<Short>", TestDerivedClass, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DerivedClass, short?>("DerivedClassToNullable<Short>", null, ConvertResult.Failure, default(short?)),
                            new TryConvertGenericTest<DerivedClass, TimeSpan?>("DerivedClassToNullable<TimeSpan>", TestDerivedClass, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<DerivedClass, TimeSpan?>("DerivedClassToNullable<TimeSpan>", null, ConvertResult.Failure, new TimeSpan?()),
                            new TryConvertGenericTest<DerivedClass, uint?>("DerivedClassToNullable<UInt>", TestDerivedClass, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DerivedClass, uint?>("DerivedClassToNullable<UInt>", null, ConvertResult.Failure, default(uint?)),
                            new TryConvertGenericTest<DerivedClass, ulong?>("DerivedClassToNullable<ULong>", TestDerivedClass, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DerivedClass, ulong?>("DerivedClassToNullable<ULong>", null, ConvertResult.Failure, default(ulong?)),
                            new TryConvertGenericTest<DerivedClass, ushort?>("DerivedClassToNullable<UShort>", TestDerivedClass, ConvertResult.Failure, default(ushort?)),
                            new TryConvertGenericTest<DerivedClass, ushort?>("DerivedClassToNullable<UShort>", null, ConvertResult.Failure, default(ushort?)),

                            // Interface/Class Types
                            new TryConvertGenericTest<DerivedClass, IInterface>("DerivedClassToInterface", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new TryConvertGenericTest<DerivedClass, IInterface>("DerivedClassToInterface", null, ConvertResult.Success, default(IInterface)),
                            new TryConvertGenericTest<DerivedClass, BaseClass>("DerivedClassToBaseClass", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new TryConvertGenericTest<DerivedClass, BaseClass>("DerivedClassToBaseClass", null, ConvertResult.Success, default(BaseClass)),
                            new TryConvertGenericTest<DerivedClass, DerivedClass>("DerivedClassToDerivedClass", TestDerivedClass, ConvertResult.Success, TestDerivedClass),
                            new TryConvertGenericTest<DerivedClass, DerivedClass>("DerivedClassToDerivedClass", null, ConvertResult.Success, default(DerivedClass)),
                        }
                },
                #endregion

            };
        #endregion

        #region Test Types
        public enum ConvertResult
        {
            Success,
            Failure
        }

        public class ConvertGenericTest<TSource, TTarget> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public ConvertGenericTest(string name, TSource source, ConvertResult expectedResult, TTarget expectedValue, TypeConverterContext context = null)
                : base(name)
            {
                this.Source = source;
                this.ExpectedResult = expectedResult;
                this.ExpectedValue = expectedValue;
                this.Context = context;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.TypeConverter = new TypeConverter();

                var sourceAsString = ValueAsString(this.Source);
                var sourceTypeAsString = TypeAsString<TSource>();

                this.WriteLine("Source:      {0} ({1})", sourceAsString, sourceTypeAsString);
                this.WriteLine();

                var expectedValueAsString = ValueAsString(this.ExpectedValue);
                var expectedTypeAsString = TypeAsString<TTarget>();

                var expectedExceptionValueAsString = this.ExpectedResult == ConvertResult.Success
                    ? "null"
                    : "not null";

                var expectedExceptionTypeAsString = TypeAsString<TypeConverterException>();

                this.WriteLine("Expected");
                this.WriteLine("  Result:    {0}", this.ExpectedResult);
                this.WriteLine("  Value:     {0} ({1})", expectedValueAsString, expectedTypeAsString);
                this.WriteLine("  Exception: {0} ({1})", expectedExceptionValueAsString, expectedExceptionTypeAsString);
                this.WriteLine();
            }

            protected override void Act()
            {
                var source = this.Source;
                var context = this.Context;

                var exceptionThrown = false;
                this.Stopwatch.Restart();
                try
                {
                    this.ActualValue = this.TypeConverter.Convert<TSource, TTarget>(source, context);
                }
                catch (Exception exception)
                {
                    exceptionThrown = true;
                    this.ActualValue = default(TTarget);
                    this.ActualException = exception;
                }
                this.Stopwatch.Stop();
                this.ActualResult = exceptionThrown ? ConvertResult.Failure : ConvertResult.Success;

                var actualValueAsString = ValueAsString(this.ActualValue);
                var actualTypeAsString = TypeAsString<TTarget>();

                var actualExceptionValueAsString = this.ActualException == null
                    ? "null"
                    : "not null";

                var actualExceptionTypeAsString = this.ActualException != null
                    ? this.ActualException.GetType().Name
                    : TypeAsString<TypeConverterException>();

                this.WriteLine("Actual");
                this.WriteLine("  Result:    {0} executionTime = {1:0.000} ms", this.ActualResult, this.Stopwatch.Elapsed.TotalMilliseconds);
                this.WriteLine("  Value:     {0} ({1})", actualValueAsString, actualTypeAsString);
                this.WriteLine("  Exception: {0} ({1})", actualExceptionValueAsString, actualExceptionTypeAsString);
            }

            protected override void Assert()
            {
                this.AssertResult();
                this.AssertValue();
                this.AssertException();
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private ITypeConverter TypeConverter { get; set; }

            private ConvertResult ActualResult { get; set; }
            private TTarget ActualValue { get; set; }
            private Exception ActualException { get; set; }
            #endregion

            #region User Supplied Properties
            private TSource Source { get; set; }

            private ConvertResult ExpectedResult { get; set; }
            private TTarget ExpectedValue { get; set; }
            private TypeConverterContext Context { get; set; }
            #endregion

            // PRIVATE METHODS //////////////////////////////////////////////
            #region Methods
            private void AssertResult()
            {
                this.ActualResult.Should().Be(this.ExpectedResult);
            }

            private void AssertValue()
            {
                switch (this.ActualResult)
                {
                    case ConvertResult.Success:
                        {
                            var isExpectedValueEqualToDefaultTarget = ReferenceEquals(this.ExpectedValue, default(TTarget));
                            if (isExpectedValueEqualToDefaultTarget)
                            {
                                // Source is default(TSource)
                                this.ActualValue.Should().Be(default(TTarget));
                            }
                            else
                            {
                                // Special case if target is byte array.
                                if (typeof(TTarget) == typeof(byte[]))
                                {
                                    this.ActualValue.Should().BeAssignableTo<TTarget>();

                                    var expectedValue = this.ExpectedValue as byte[];
                                    var actualValue = this.ActualValue as byte[];
                                    actualValue.Should().ContainInOrder(expectedValue);
                                    return;
                                }

                                // Special case if target is nullable
                                if (typeof(TTarget).IsNullableType())
                                {
                                    this.ActualValue.Should().BeAssignableTo<TTarget>();
                                    this.ActualValue.Should().Be(this.ExpectedValue);
                                    return;
                                }

                                // Special case if target is type.
                                if (typeof(TTarget) == typeof(Type))
                                {
                                    this.ActualValue.Should().BeAssignableTo<Type>();
                                    this.ActualValue.Should().Be(this.ExpectedValue);
                                    return;
                                }

                                this.ActualValue.Should().BeAssignableTo<TTarget>();
                                this.ActualValue.Should().Be(this.ExpectedValue);
                            }
                        }
                        break;

                    case ConvertResult.Failure:
                        {
                            this.ActualValue.Should().Be(default(TTarget));
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            private void AssertException()
            {
                switch (this.ExpectedResult)
                {
                    case ConvertResult.Success:
                        this.ActualException.Should().BeNull();
                        break;

                    case ConvertResult.Failure:
                        this.ActualException.Should().NotBeNull();
                        this.ActualException.Should().BeOfType<TypeConverterException>();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            private static string TypeAsString<T>()
            {
                var type = typeof(T);
                if (!type.IsNullableType())
                {
                    var typeName = type.Name;
                    return typeName;
                }

                var underlyingType = Nullable.GetUnderlyingType(type);
                var underlyingTypeName = underlyingType.Name;
                var nullableTypeName = String.Format("Nullable<{0}>", underlyingTypeName);
                return nullableTypeName;
            }

            private static string ValueAsString<TValue>(TValue value)
            {
                var valueAsString = value.SafeToString();
                var valueType = typeof(TValue);

                if (!valueType.IsNullableType() && valueType.IsValueType())
                    return valueAsString;

                var valueAsObject = (object)value;
                if (valueAsObject == null)
                    valueAsString = "null";
                return valueAsString;
            }
            #endregion
        }

        public class TryConvertGenericTest<TSource, TTarget> : UnitTest
        {
            // PUBLIC CONSTRUCTORS //////////////////////////////////////////
            #region Constructors
            public TryConvertGenericTest(string name, TSource source, ConvertResult expectedResult, TTarget expectedValue, TypeConverterContext context = null)
                : base(name)
            {
                this.Source = source;
                this.ExpectedResult = expectedResult;
                this.ExpectedValue = expectedValue;
                this.Context = context;
            }
            #endregion

            // PROTECTED METHODS ////////////////////////////////////////////
            #region UnitTest Overrides
            protected override void Arrange()
            {
                this.TypeConverter = new TypeConverter();

                var sourceAsString = ValueAsString(this.Source);
                var sourceTypeAsString = TypeAsString<TSource>();

                this.WriteLine("Source:    {0} ({1})", sourceAsString, sourceTypeAsString);
                this.WriteLine();

                var expectedValueAsString = ValueAsString(this.ExpectedValue);
                var expectedTypeAsString = TypeAsString<TTarget>();

                this.WriteLine("Expected");
                this.WriteLine("  Result:  {0}", this.ExpectedResult);
                this.WriteLine("  Value:   {0} ({1})", expectedValueAsString, expectedTypeAsString);
                this.WriteLine();
            }

            protected override void Act()
            {
                var source = this.Source;
                var context = this.Context;
                TTarget actualValue;

                this.Stopwatch.Restart();
                var actualResult = this.TypeConverter.TryConvert(source, context, out actualValue);
                this.Stopwatch.Stop();

                this.ActualResult = actualResult ? ConvertResult.Success : ConvertResult.Failure;
                this.ActualValue = actualValue;

                var actualValueAsString = ValueAsString(this.ActualValue);
                var actualTypeAsString = TypeAsString<TTarget>();

                this.WriteLine("Actual");
                this.WriteLine("  Result:  {0} executionTime = {1:0.000} ms", this.ActualResult, this.Stopwatch.Elapsed.TotalMilliseconds);
                this.WriteLine("  Value:   {0} ({1})", actualValueAsString, actualTypeAsString);
            }

            protected override void Assert()
            {
                this.ActualResult.Should().Be(this.ExpectedResult);

                switch (this.ActualResult)
                {
                    case ConvertResult.Success:
                        {
                            var isExpectedValueEqualToDefaultTarget = ReferenceEquals(this.ExpectedValue, default(TTarget));
                            if (isExpectedValueEqualToDefaultTarget)
                            {
                                // Source is default(TSource)
                                this.ActualValue.Should().Be(default(TTarget));
                            }
                            else
                            {
                                // Special case if target is byte array.
                                if (typeof(TTarget) == typeof(byte[]))
                                {
                                    this.ActualValue.Should().BeAssignableTo<TTarget>();

                                    var expectedValue = this.ExpectedValue as byte[];
                                    var actualValue = this.ActualValue as byte[];
                                    actualValue.Should().ContainInOrder(expectedValue);
                                    return;
                                }

                                // Special case if target is nullable
                                if (typeof(TTarget).IsNullableType())
                                {
                                    this.ActualValue.Should().BeAssignableTo<TTarget>();
                                    this.ActualValue.Should().Be(this.ExpectedValue);
                                    return;
                                }

                                // Special case if target is type.
                                if (typeof(TTarget) == typeof(Type))
                                {
                                    this.ActualValue.Should().BeAssignableTo<Type>();
                                    this.ActualValue.Should().Be(this.ExpectedValue);
                                    return;
                                }

                                this.ActualValue.Should().BeAssignableTo<TTarget>();
                                this.ActualValue.Should().Be(this.ExpectedValue);
                            }
                        }
                        break;

                    case ConvertResult.Failure:
                        {
                            this.ActualValue.Should().Be(default(TTarget));
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            #endregion

            // PRIVATE PROPERTIES ///////////////////////////////////////////
            #region Calculated Properties
            private ITypeConverter TypeConverter { get; set; }
            private ConvertResult ActualResult { get; set; }
            private TTarget ActualValue { get; set; }
            #endregion

            #region User Supplied Properties
            private TSource Source { get; set; }

            private ConvertResult ExpectedResult { get; set; }
            private TTarget ExpectedValue { get; set; }
            private TypeConverterContext Context { get; set; }
            #endregion

            // PRIVATE METHODS //////////////////////////////////////////////
            #region Methods
            private static bool GetNullableHasValue<T>(T nullable)
            {
                var nullableType = typeof(T);
                var instanceExpression = Expression.Parameter(nullableType, "i");
                var propertyInfo = nullableType.GetProperty(StaticReflection.GetMemberName<int?>(x => x.HasValue), BindingFlags.Public | BindingFlags.Instance);
                var propertyExpression = Expression.Property(instanceExpression, propertyInfo);
                var lambdaExpression = (Expression<Func<T, bool>>)Expression.Lambda(propertyExpression, instanceExpression);
                var labmda = lambdaExpression.Compile();
                var hasValue = labmda(nullable);
                return hasValue;
            }

            private static string TypeAsString<T>()
            {
                var type = typeof(T);
                if (!type.IsNullableType())
                {
                    var typeName = type.Name;
                    return typeName;
                }

                var underlyingType = Nullable.GetUnderlyingType(type);
                var underlyingTypeName = underlyingType.Name;
                var nullableTypeName = String.Format("Nullable<{0}>", underlyingTypeName);
                return nullableTypeName;
            }

            private static string ValueAsString<TValue>(TValue value)
            {
                var valueAsString = value.SafeToString();
                var valueType = typeof(TValue);

                if (!valueType.IsNullableType() && valueType.IsValueType())
                    return valueAsString;

                var valueAsObject = (object)value;
                if (valueAsObject == null)
                    valueAsString = "null";
                return valueAsString;
            }
            #endregion
        }
        #endregion
    }
}
