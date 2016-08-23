// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;

using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Reflection
{
    public class TypeConverterTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeConverterTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("ConvertTestData")]
        public void TestTypeConveterConvertNonGeneric(string name, IConvertTest[] genericConvertTestCollection)
        {
            foreach (var genericConvertTest in genericConvertTestCollection)
            {
                // Arrange

                // Act
                genericConvertTest.ActWithNonGenericConvert();
                genericConvertTest.OutputTest(this);

                // Assert
                genericConvertTest.AssertTest();
            }
        }

        [Theory]
        [MemberData("ConvertTestData")]
        public void TestTypeConveterTryConvertNonGeneric(string name, IConvertTest[] genericConvertTestCollection)
        {
            foreach (var genericConvertTest in genericConvertTestCollection)
            {
                // Arrange

                // Act
                genericConvertTest.ActWithNonGenericTryConvert();
                genericConvertTest.OutputTest(this);

                // Assert
                genericConvertTest.AssertTest();
            }
        }

        [Theory]
        [MemberData("ConvertTestData")]
        public void TestTypeConveterConvertGeneric(string name, IConvertTest[] genericConvertTestCollection)
        {
            foreach (var genericConvertTest in genericConvertTestCollection)
            {
                // Arrange

                // Act
                genericConvertTest.ActWithGenericConvert();
                genericConvertTest.OutputTest(this);

                // Assert
                genericConvertTest.AssertTest();
            }
        }

        [Theory]
        [MemberData("ConvertTestData")]
        public void TestTypeConveterTryConvertGeneric(string name, IConvertTest[] genericConvertTestCollection)
        {
            foreach (var genericConvertTest in genericConvertTestCollection)
            {
                // Arrange

                // Act
                genericConvertTest.ActWithGenericTryConvert();
                genericConvertTest.OutputTest(this);

                // Assert
                genericConvertTest.AssertTest();
            }
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        // ReSharper disable UnusedParameter.Local
        private static void AssertTest<T>(Type targetType, bool expectedResult, T expectedValue, bool actualResult, T actualValue)
        {
            Assert.Equal(expectedResult, actualResult);
            if (actualResult == false)
            {
                Assert.Equal(default(T), actualValue);
                return;
            }

            if (Object.Equals(expectedValue, default(T)))
            {
                Assert.Equal(default(T), actualValue);
                return;
            }

            if (!targetType.IsNullableType())
            {
                Assert.IsAssignableFrom(targetType, expectedValue);
                Assert.IsAssignableFrom(targetType, actualValue);
            }
            else
            {
                // Special handling for Nullable<T> due to special CLR boxing rules with Nullable<T>.
                var nullableUnderlyingTargetType = Nullable.GetUnderlyingType(targetType);
                Assert.IsType(nullableUnderlyingTargetType, expectedValue);
                Assert.IsType(nullableUnderlyingTargetType, actualValue);
            }

            Assert.Equal(expectedValue, actualValue);
        }
        // ReSharper restore UnusedParameter.Local

        private void OutputTest(object sourceValue, Type targetType, bool expectedResult, object expectedValue, bool actualResult, object actualValue)
        {
            var sourceValueAsString = sourceValue != null ? sourceValue.ToString() : "null";
            var targetTypeAsString = targetType.Name;
            if (targetType.IsNullableType())
            {
                var nullableUnderlyingTargetType = Nullable.GetUnderlyingType(targetType);
                targetTypeAsString = "Nullable<{0}>".FormatWith(nullableUnderlyingTargetType.ToString());
            }
            var expectedResultAsString = expectedResult.ToString();
            var expectedValueAsString = expectedResult && expectedValue != null
                ? expectedValue.ToString()
                : (expectedResult ? "null" : String.Empty);
            var actualResultAsString = actualResult.ToString();
            var actualValueAsString = actualResult && actualValue != null
                ? actualValue.ToString()
                : (actualResult ? "null" : String.Empty);
            this.Output.WriteLine("SoureValue={0} TargetType={1} ExpectedResult={2} ExpectedValue={3} ActualResult={4} ActualValue={5}".FormatWith(sourceValueAsString, targetTypeAsString, expectedResultAsString, expectedValueAsString, actualResultAsString, actualValueAsString));
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Types
        // ReSharper disable MemberCanBePrivate.Global
        public interface IConvertTest
        {
            void ActWithGenericConvert();
            void ActWithGenericTryConvert();

            void ActWithNonGenericConvert();
            void ActWithNonGenericTryConvert();

            void OutputTest(TypeConverterTests parent);
            void AssertTest();
        }

        public class ConvertTest<T> : IConvertTest
        {
            #region Constructor
            public ConvertTest(object sourceValue, bool expectedResult, T expectedValue)
            {
                this.SourceValue = sourceValue;
                this.ExpectedResult = expectedResult;
                this.ExpectedValue = expectedValue;
            }
            #endregion

            #region IConvertTest Implementation
            public void ActWithGenericConvert()
            {
                if (this.ExpectedResult == false)
                {
                    Assert.Throws<TypeConverterException>(() => TypeConverter.Convert<T>(this.SourceValue));
                }
                else
                {
                    this.ActualValue = TypeConverter.Convert<T>(this.SourceValue);
                    this.ActualResult = true;
                }
            }

            public void ActWithGenericTryConvert()
            {
                T actualValue;
                this.ActualResult = TypeConverter.TryConvert(this.SourceValue, out actualValue);
                this.ActualValue = actualValue;
            }

            public void ActWithNonGenericConvert()
            {
                var targetType = typeof(T);
                if (this.ExpectedResult == false)
                {
                    Assert.Throws<TypeConverterException>(() => TypeConverter.Convert(this.SourceValue, targetType));
                }
                else
                {
                    this.ActualValue = (T)(TypeConverter.Convert(this.SourceValue, targetType));
                    this.ActualResult = true;
                }
            }

            public void ActWithNonGenericTryConvert()
            {
                var targetType = typeof(T);

                object actualValue;
                this.ActualResult = TypeConverter.TryConvert(this.SourceValue, targetType, out actualValue);
                this.ActualValue = actualValue != null ? (T)actualValue : default(T);
            }

            public void OutputTest(TypeConverterTests parent)
            {
                parent.OutputTest(this.SourceValue, typeof(T), this.ExpectedResult, this.ExpectedValue, this.ActualResult, this.ActualValue);
            }

            public void AssertTest()
            {
                TypeConverterTests.AssertTest(typeof(T), this.ExpectedResult, this.ExpectedValue, this.ActualResult, this.ActualValue);
            }
            #endregion

            #region Properties
            private object SourceValue { get; set; }
            private bool ExpectedResult { get; set; }
            private T ExpectedValue { get; set; }
            private bool ActualResult { get; set; }
            private T ActualValue { get; set; }
            #endregion
        }
        #endregion

        #region Test Data
        public static readonly DateTime TestDateTime = new DateTime(1968, 5, 20, 20, 2, 42, 123, DateTimeKind.Utc);
        public static readonly string TestDateTimeString = TestDateTime.ToString("O");

        public static readonly DateTimeOffset TestDateTimeOffset = new DateTimeOffset(1968, 5, 20, 20, 2, 42, 123, TimeSpan.Zero);
        public static readonly string TestDateTimeOffsetString = TestDateTimeOffset.ToString("O");

        public static readonly TimeSpan TestTimeSpan = new TimeSpan(42, 0, 0, 0, 0);
        public static readonly string TestTimeSpanString = TestTimeSpan.ToString("c");

        public const string TestGuidString = "5167e9e1-a15f-41e1-af46-442ffcd37f1b";
        public static readonly Guid TestGuid = new Guid(TestGuidString);
        public static readonly byte[] TestGuidByteArray = TestGuid.ToByteArray();

        public const string TestUriString = "https://api.example.com:8002/api/en-us/articles/42";
        public static readonly Uri TestUri = new Uri(TestUriString);

        public static readonly byte[] TestByteArray = { 42, 24, 48, 84, 12, 21, 68, 86 };
        public const string TestByteArrayString = "KhgwVAwVRFY=";

        public static readonly Type TestType = typeof(TypeConverterTests);
        public static readonly string TestTypeString = TestType.GetCompactQualifiedName();

        public const int TestRedOrdinalValue0 = 0;
        public const int TestGreenOrdinalValue24 = 24;
        public const int TestBlueOrdinalValue42 = 42;

        public const string TestBlueString = "Blue";

        // ReSharper disable UnusedMember.Global
        public enum PrimaryColor
        {
            Red = TestRedOrdinalValue0,
            Green = TestGreenOrdinalValue24,
            Blue = TestBlueOrdinalValue42
        };
        // ReSharper restore UnusedMember.Global

        public const int TestEnumOrdinal = TestBlueOrdinalValue42;
        public const string TestEnumString = TestBlueString;
        public const PrimaryColor TestEnum = PrimaryColor.Blue;

        public interface IInterface
        { }

        public class BaseClass : IInterface
        { }

        public class DerivedClass : BaseClass
        { }

        public static readonly BaseClass TestBaseClass = new BaseClass();
        public static readonly DerivedClass TestDerivedClass = new DerivedClass();

        // ReSharper disable UnusedMember.Global
        // ReSharper disable RedundantCast
        // ReSharper disable RedundantExplicitNullableCreation
        public static readonly IEnumerable<object[]> ConvertTestData = new[]
            {
                new object[]
                    {
                        "WithBool", new IConvertTest[]
                            {
                                new ConvertTest<bool>(true, true, (bool)true),
                                new ConvertTest<bool>(false, true, (bool)false),
                                new ConvertTest<byte>(true, true, (byte)1),
                                new ConvertTest<char>(true, false, default(char)),
                                new ConvertTest<DateTime>(true, false, default(DateTime)),
                                new ConvertTest<decimal>(true, true, (decimal)1.0),
                                new ConvertTest<double>(true, true, (double)1.0),
                                new ConvertTest<float>(true, true, (float)1.0),
                                new ConvertTest<int>(true, true, (int)1),
                                new ConvertTest<long>(true, true, (long)1),
                                new ConvertTest<sbyte>(true, true, (sbyte)1),
                                new ConvertTest<short>(true, true, (short)1),
                                new ConvertTest<string>(true, true, (string)"True"),
                                new ConvertTest<string>(false, true, (string)"False"),
                                new ConvertTest<uint>(true, true, (uint)1),
                                new ConvertTest<ulong>(true, true, (ulong)1),
                                new ConvertTest<ushort>(true, true, (ushort)1),
                                new ConvertTest<PrimaryColor>(true, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(true, false, default(Guid)),
                                new ConvertTest<Uri>(true, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(true, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(true, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(true, false, default(byte[])),
                                new ConvertTest<Type>(true, false, default(Type)),
                                new ConvertTest<IInterface>(true, false, default(IInterface)),
                                new ConvertTest<BaseClass>(true, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>(true, false, default(DerivedClass)),

                                new ConvertTest<bool?>(true, true, new bool?((bool)true)),
                                new ConvertTest<bool?>(false, true, new bool?((bool)false)),
                                new ConvertTest<byte?>(true, true, new byte?((byte)1)),
                                new ConvertTest<char?>(true, false, default(char?)),
                                new ConvertTest<DateTime?>(true, false, default(DateTime?)),
                                new ConvertTest<decimal?>(true, true, new decimal?((decimal)1.0)),
                                new ConvertTest<double?>(true, true, new double?((double)1.0)),
                                new ConvertTest<float?>(true, true, new float?((float)1.0)),
                                new ConvertTest<int?>(true, true, new int?((int)1)),
                                new ConvertTest<long?>(true, true, new long?((long)1)),
                                new ConvertTest<sbyte?>(true, true, new sbyte?((sbyte)1)),
                                new ConvertTest<short?>(true, true, new short?((short)1)),
                                new ConvertTest<uint?>(true, true, new uint?((uint)1)),
                                new ConvertTest<ulong?>(true, true, new ulong?((ulong)1)),
                                new ConvertTest<ushort?>(true, true, new ushort?((ushort)1)),
                                new ConvertTest<PrimaryColor?>(true, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(true, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(true, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>(true, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithByte", new IConvertTest[]
                            {
                                new ConvertTest<bool>((byte)42, true, (bool)true),
                                new ConvertTest<byte>((byte)42, true, (byte)42),
                                new ConvertTest<char>((byte)42, true, (char)'*'),
                                new ConvertTest<DateTime>((byte)42, false, default(DateTime)),
                                new ConvertTest<decimal>((byte)42, true, (decimal)42.0),
                                new ConvertTest<double>((byte)42, true, (double)42.0),
                                new ConvertTest<float>((byte)42, true, (float)42.0),
                                new ConvertTest<int>((byte)42, true, (int)42),
                                new ConvertTest<long>((byte)42, true, (long)42),
                                new ConvertTest<sbyte>((byte)42, true, (sbyte)42),
                                new ConvertTest<short>((byte)42, true, (short)42),
                                new ConvertTest<string>((byte)42, true, (string)"42"),
                                new ConvertTest<uint>((byte)42, true, (uint)42),
                                new ConvertTest<ulong>((byte)42, true, (ulong)42),
                                new ConvertTest<ushort>((byte)42, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((byte)42, true, (PrimaryColor)42),
                                new ConvertTest<Guid>((byte)42, false, default(Guid)),
                                new ConvertTest<Uri>((byte)42, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((byte)42, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((byte)42, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((byte)42, false, default(byte[])),
                                new ConvertTest<Type>((byte)42, false, default(Type)),
                                new ConvertTest<IInterface>((byte)42, false, default(IInterface)),
                                new ConvertTest<BaseClass>((byte)42, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((byte)42, false, default(DerivedClass)),

                                new ConvertTest<bool?>((byte)42, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((byte)42, true, new byte?((byte)42)),
                                new ConvertTest<char?>((byte)42, true, new char?((char)'*')),
                                new ConvertTest<DateTime?>((byte)42, false, default(DateTime?)),
                                new ConvertTest<decimal?>((byte)42, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((byte)42, true, new double?((double)42.0)),
                                new ConvertTest<float?>((byte)42, true, new float?((float)42.0)),
                                new ConvertTest<int?>((byte)42, true, new int?((int)42)),
                                new ConvertTest<long?>((byte)42, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((byte)42, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((byte)42, true, new short?((short)42)),
                                new ConvertTest<uint?>((byte)42, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((byte)42, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((byte)42, true, new PrimaryColor?((PrimaryColor)42)),
                                new ConvertTest<Guid?>((byte)42, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((byte)42, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((byte)42, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithChar", new IConvertTest[]
                            {
                                new ConvertTest<bool>('*', false, default(bool)),
                                new ConvertTest<byte>('*', true, (byte)42),
                                new ConvertTest<char>('*', true, (char)'*'),
                                new ConvertTest<DateTime>('*', false, default(DateTime)),
                                new ConvertTest<decimal>('*', false, default(decimal)),
                                new ConvertTest<double>('*', false, default(double)),
                                new ConvertTest<float>('*', false, default(float)),
                                new ConvertTest<int>('*', true, (int)42),
                                new ConvertTest<long>('*', true, (long)42),
                                new ConvertTest<sbyte>('*', true, (sbyte)42),
                                new ConvertTest<short>('*', true, (short)42),
                                new ConvertTest<string>('*', true, (string)"*"),
                                new ConvertTest<uint>('*', true, (uint)42),
                                new ConvertTest<ulong>('*', true, (ulong)42),
                                new ConvertTest<ushort>('*', true, (ushort)42),
                                new ConvertTest<PrimaryColor>('*', true, (PrimaryColor)42),
                                new ConvertTest<Guid>('*', false, default(Guid)),
                                new ConvertTest<Uri>('*', false, default(Uri)),
                                new ConvertTest<DateTimeOffset>('*', false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>('*', false, default(TimeSpan)),
                                new ConvertTest<byte[]>('*', false, default(byte[])),
                                new ConvertTest<Type>('*', false, default(Type)),
                                new ConvertTest<IInterface>('*', false, default(IInterface)),
                                new ConvertTest<BaseClass>('*', false, default(BaseClass)),
                                new ConvertTest<DerivedClass>('*', false, default(DerivedClass)),

                                new ConvertTest<bool?>('*', false, default(bool?)),
                                new ConvertTest<byte?>('*', true, new byte?((byte)42)),
                                new ConvertTest<char?>('*', true, new char?((char)'*')),
                                new ConvertTest<DateTime?>('*', false, default(DateTime?)),
                                new ConvertTest<decimal?>('*', false, default(decimal?)),
                                new ConvertTest<double?>('*', false, default(double?)),
                                new ConvertTest<float?>('*', false, default(float?)),
                                new ConvertTest<int?>('*', true, new int?((int)42)),
                                new ConvertTest<long?>('*', true, new long?((long)42)),
                                new ConvertTest<sbyte?>('*', true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>('*', true, new short?((short)42)),
                                new ConvertTest<uint?>('*', true, new uint?((uint)42)),
                                new ConvertTest<ulong?>('*', true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>('*', true, new PrimaryColor?((PrimaryColor)42)),
                                new ConvertTest<Guid?>('*', false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>('*', false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>('*', false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithDateTime", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestDateTime, false, default(bool)),
                                new ConvertTest<byte>(TestDateTime, false, default(byte)),
                                new ConvertTest<char>(TestDateTime, false, default(char)),
                                new ConvertTest<DateTime>(TestDateTime, true, TestDateTime),
                                new ConvertTest<decimal>(TestDateTime, false, default(decimal)),
                                new ConvertTest<double>(TestDateTime, false, default(double)),
                                new ConvertTest<float>(TestDateTime, false, default(float)),
                                new ConvertTest<int>(TestDateTime, false, default(int)),
                                new ConvertTest<long>(TestDateTime, false, default(long)),
                                new ConvertTest<sbyte>(TestDateTime, false, default(sbyte)),
                                new ConvertTest<short>(TestDateTime, false, default(short)),
                                new ConvertTest<string>(TestDateTime, true, TestDateTimeString),
                                new ConvertTest<uint>(TestDateTime, false, default(uint)),
                                new ConvertTest<ulong>(TestDateTime, false, default(ulong)),
                                new ConvertTest<ushort>(TestDateTime, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(TestDateTime, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(TestDateTime, false, default(Guid)),
                                new ConvertTest<Uri>(TestDateTime, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(TestDateTime, true, TestDateTimeOffset),
                                new ConvertTest<TimeSpan>(TestDateTime, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(TestDateTime, false, default(byte[])),
                                new ConvertTest<Type>(TestDateTime, false, default(Type)),
                                new ConvertTest<IInterface>(TestDateTime, false, default(IInterface)),
                                new ConvertTest<BaseClass>(TestDateTime, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>(TestDateTime, false, default(DerivedClass)),

                                new ConvertTest<bool?>(TestDateTime, false, default(bool?)),
                                new ConvertTest<byte?>(TestDateTime, false, default(byte?)),
                                new ConvertTest<char?>(TestDateTime, false, default(char?)),
                                new ConvertTest<DateTime?>(TestDateTime, true, new DateTime?(TestDateTime)),
                                new ConvertTest<decimal?>(TestDateTime, false, default(decimal?)),
                                new ConvertTest<double?>(TestDateTime, false, default(double?)),
                                new ConvertTest<float?>(TestDateTime, false, default(float?)),
                                new ConvertTest<int?>(TestDateTime, false, default(int?)),
                                new ConvertTest<long?>(TestDateTime, false, default(long?)),
                                new ConvertTest<sbyte?>(TestDateTime, false, default(sbyte?)),
                                new ConvertTest<short?>(TestDateTime, false, default(short?)),
                                new ConvertTest<uint?>(TestDateTime, false, default(uint?)),
                                new ConvertTest<ulong?>(TestDateTime, false, default(ulong?)),
                                new ConvertTest<ushort?>(TestDateTime, false, default(ushort?)),
                                new ConvertTest<PrimaryColor?>(TestDateTime, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(TestDateTime, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(TestDateTime, true, new DateTimeOffset?(TestDateTimeOffset)),
                                new ConvertTest<TimeSpan?>(TestDateTime, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithDecimal", new IConvertTest[]
                            {
                                new ConvertTest<bool>((decimal)42.0, true, (bool)true),
                                new ConvertTest<byte>((decimal)42.0, true, (byte)42),
                                new ConvertTest<char>((decimal)42.0, false, default(char)),
                                new ConvertTest<DateTime>((decimal)42.0, false, default(DateTime)),
                                new ConvertTest<decimal>((decimal)42.0, true, (decimal)42.0),
                                new ConvertTest<double>((decimal)42.0, true, (double)42.0),
                                new ConvertTest<float>((decimal)42.0, true, (float)42.0),
                                new ConvertTest<int>((decimal)42.0, true, (int)42),
                                new ConvertTest<long>((decimal)42.0, true, (long)42),
                                new ConvertTest<sbyte>((decimal)42.0, true, (sbyte)42),
                                new ConvertTest<short>((decimal)42.0, true, (short)42),
                                new ConvertTest<string>((decimal)42.0, true, (string)"42"),
                                new ConvertTest<uint>((decimal)42.0, true, (uint)42),
                                new ConvertTest<ulong>((decimal)42.0, true, (ulong)42),
                                new ConvertTest<ushort>((decimal)42.0, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((decimal)42.0, false, default(PrimaryColor)),
                                new ConvertTest<Guid>((decimal)42.0, false, default(Guid)),
                                new ConvertTest<Uri>((decimal)42.0, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((decimal)42.0, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((decimal)42.0, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((decimal)42.0, false, default(byte[])),
                                new ConvertTest<Type>((decimal)42.0, false, default(Type)),
                                new ConvertTest<IInterface>((decimal)42.0, false, default(IInterface)),
                                new ConvertTest<BaseClass>((decimal)42.0, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((decimal)42.0, false, default(DerivedClass)),

                                new ConvertTest<bool?>((decimal)42.0, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((decimal)42.0, true, new byte?((byte)42)),
                                new ConvertTest<char?>((decimal)42.0, false, default(char?)),
                                new ConvertTest<DateTime?>((decimal)42.0, false, default(DateTime?)),
                                new ConvertTest<decimal?>((decimal)42.0, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((decimal)42.0, true, new double?((double)42.0)),
                                new ConvertTest<float?>((decimal)42.0, true, new float?((float)42.0)),
                                new ConvertTest<int?>((decimal)42.0, true, new int?((int)42)),
                                new ConvertTest<long?>((decimal)42.0, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((decimal)42.0, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((decimal)42.0, true, new short?((short)42)),
                                new ConvertTest<uint?>((decimal)42.0, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((decimal)42.0, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((decimal)42.0, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>((decimal)42.0, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((decimal)42.0, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((decimal)42.0, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithDouble", new IConvertTest[]
                            {
                                new ConvertTest<bool>((double)42.0, true, (bool)true),
                                new ConvertTest<byte>((double)42.0, true, (byte)42),
                                new ConvertTest<char>((double)42.0, false, default(char)),
                                new ConvertTest<DateTime>((double)42.0, false, default(DateTime)),
                                new ConvertTest<decimal>((double)42.0, true, (decimal)42.0),
                                new ConvertTest<double>((double)42.0, true, (double)42.0),
                                new ConvertTest<float>((double)42.0, true, (float)42.0),
                                new ConvertTest<int>((double)42.0, true, (int)42),
                                new ConvertTest<long>((double)42.0, true, (long)42),
                                new ConvertTest<sbyte>((double)42.0, true, (sbyte)42),
                                new ConvertTest<short>((double)42.0, true, (short)42),
                                new ConvertTest<string>((double)42.0, true, (string)"42"),
                                new ConvertTest<uint>((double)42.0, true, (uint)42),
                                new ConvertTest<ulong>((double)42.0, true, (ulong)42),
                                new ConvertTest<ushort>((double)42.0, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((double)42.0, false, default(PrimaryColor)),
                                new ConvertTest<Guid>((double)42.0, false, default(Guid)),
                                new ConvertTest<Uri>((double)42.0, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((double)42.0, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((double)42.0, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((double)42.0, false, default(byte[])),
                                new ConvertTest<Type>((double)42.0, false, default(Type)),
                                new ConvertTest<IInterface>((double)42.0, false, default(IInterface)),
                                new ConvertTest<BaseClass>((double)42.0, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((double)42.0, false, default(DerivedClass)),

                                new ConvertTest<bool?>((double)42.0, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((double)42.0, true, new byte?((byte)42)),
                                new ConvertTest<char?>((double)42.0, false, default(char?)),
                                new ConvertTest<DateTime?>((double)42.0, false, default(DateTime?)),
                                new ConvertTest<decimal?>((double)42.0, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((double)42.0, true, new double?((double)42.0)),
                                new ConvertTest<float?>((double)42.0, true, new float?((float)42.0)),
                                new ConvertTest<int?>((double)42.0, true, new int?((int)42)),
                                new ConvertTest<long?>((double)42.0, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((double)42.0, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((double)42.0, true, new short?((short)42)),
                                new ConvertTest<uint?>((double)42.0, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((double)42.0, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((double)42.0, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>((double)42.0, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((double)42.0, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((double)42.0, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithFloat", new IConvertTest[]
                            {
                                new ConvertTest<bool>((float)42.0, true, (bool)true),
                                new ConvertTest<byte>((float)42.0, true, (byte)42),
                                new ConvertTest<char>((float)42.0, false, default(char)),
                                new ConvertTest<DateTime>((float)42.0, false, default(DateTime)),
                                new ConvertTest<decimal>((float)42.0, true, (decimal)42.0),
                                new ConvertTest<double>((float)42.0, true, (double)42.0),
                                new ConvertTest<float>((float)42.0, true, (float)42.0),
                                new ConvertTest<int>((float)42.0, true, (int)42),
                                new ConvertTest<long>((float)42.0, true, (long)42),
                                new ConvertTest<sbyte>((float)42.0, true, (sbyte)42),
                                new ConvertTest<short>((float)42.0, true, (short)42),
                                new ConvertTest<string>((float)42.0, true, (string)"42"),
                                new ConvertTest<uint>((float)42.0, true, (uint)42),
                                new ConvertTest<ulong>((float)42.0, true, (ulong)42),
                                new ConvertTest<ushort>((float)42.0, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((float)42.0, false, default(PrimaryColor)),
                                new ConvertTest<Guid>((float)42.0, false, default(Guid)),
                                new ConvertTest<Uri>((float)42.0, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((float)42.0, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((float)42.0, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((float)42.0, false, default(byte[])),
                                new ConvertTest<Type>((float)42.0, false, default(Type)),
                                new ConvertTest<IInterface>((float)42.0, false, default(IInterface)),
                                new ConvertTest<BaseClass>((float)42.0, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((float)42.0, false, default(DerivedClass)),

                                new ConvertTest<bool?>((float)42.0, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((float)42.0, true, new byte?((byte)42)),
                                new ConvertTest<char?>((float)42.0, false, default(char?)),
                                new ConvertTest<DateTime?>((float)42.0, false, default(DateTime?)),
                                new ConvertTest<decimal?>((float)42.0, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((float)42.0, true, new double?((double)42.0)),
                                new ConvertTest<float?>((float)42.0, true, new float?((float)42.0)),
                                new ConvertTest<int?>((float)42.0, true, new int?((int)42)),
                                new ConvertTest<long?>((float)42.0, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((float)42.0, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((float)42.0, true, new short?((short)42)),
                                new ConvertTest<uint?>((float)42.0, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((float)42.0, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((float)42.0, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>((float)42.0, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((float)42.0, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((float)42.0, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithInt", new IConvertTest[]
                            {
                                new ConvertTest<bool>((int)42, true, (bool)true),
                                new ConvertTest<byte>((int)42, true, (byte)42),
                                new ConvertTest<char>((int)42, true, (char)'*'),
                                new ConvertTest<DateTime>((int)42, false, default(DateTime)),
                                new ConvertTest<decimal>((int)42, true, (decimal)42.0),
                                new ConvertTest<double>((int)42, true, (double)42.0),
                                new ConvertTest<float>((int)42, true, (float)42.0),
                                new ConvertTest<int>((int)42, true, (int)42),
                                new ConvertTest<long>((int)42, true, (long)42),
                                new ConvertTest<sbyte>((int)42, true, (sbyte)42),
                                new ConvertTest<short>((int)42, true, (short)42),
                                new ConvertTest<string>((int)42, true, (string)"42"),
                                new ConvertTest<uint>((int)42, true, (uint)42),
                                new ConvertTest<ulong>((int)42, true, (ulong)42),
                                new ConvertTest<ushort>((int)42, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((int)42, true, (PrimaryColor)42),
                                new ConvertTest<Guid>((int)42, false, default(Guid)),
                                new ConvertTest<Uri>((int)42, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((int)42, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((int)42, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((int)42, false, default(byte[])),
                                new ConvertTest<Type>((int)42, false, default(Type)),
                                new ConvertTest<IInterface>((int)42, false, default(IInterface)),
                                new ConvertTest<BaseClass>((int)42, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((int)42, false, default(DerivedClass)),

                                new ConvertTest<bool?>((int)42, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((int)42, true, new byte?((byte)42)),
                                new ConvertTest<char?>((int)42, true, new char?((char)'*')),
                                new ConvertTest<DateTime?>((int)42, false, default(DateTime?)),
                                new ConvertTest<decimal?>((int)42, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((int)42, true, new double?((double)42.0)),
                                new ConvertTest<float?>((int)42, true, new float?((float)42.0)),
                                new ConvertTest<int?>((int)42, true, new int?((int)42)),
                                new ConvertTest<long?>((int)42, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((int)42, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((int)42, true, new short?((short)42)),
                                new ConvertTest<uint?>((int)42, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((int)42, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((int)42, true, new PrimaryColor?((PrimaryColor)42)),
                                new ConvertTest<Guid?>((int)42, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((int)42, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((int)42, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithLong", new IConvertTest[]
                            {
                                new ConvertTest<bool>((long)42, true, (bool)true),
                                new ConvertTest<byte>((long)42, true, (byte)42),
                                new ConvertTest<char>((long)42, true, (char)'*'),
                                new ConvertTest<DateTime>((long)42, false, default(DateTime)),
                                new ConvertTest<decimal>((long)42, true, (decimal)42.0),
                                new ConvertTest<double>((long)42, true, (double)42.0),
                                new ConvertTest<float>((long)42, true, (float)42.0),
                                new ConvertTest<int>((long)42, true, (int)42),
                                new ConvertTest<long>((long)42, true, (long)42),
                                new ConvertTest<sbyte>((long)42, true, (sbyte)42),
                                new ConvertTest<short>((long)42, true, (short)42),
                                new ConvertTest<string>((long)42, true, (string)"42"),
                                new ConvertTest<uint>((long)42, true, (uint)42),
                                new ConvertTest<ulong>((long)42, true, (ulong)42),
                                new ConvertTest<ushort>((long)42, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((long)42, true, (PrimaryColor)42),
                                new ConvertTest<Guid>((long)42, false, default(Guid)),
                                new ConvertTest<Uri>((long)42, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((long)42, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((long)42, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((long)42, false, default(byte[])),
                                new ConvertTest<Type>((long)42, false, default(Type)),
                                new ConvertTest<IInterface>((long)42, false, default(IInterface)),
                                new ConvertTest<BaseClass>((long)42, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((long)42, false, default(DerivedClass)),

                                new ConvertTest<bool?>((long)42, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((long)42, true, new byte?((byte)42)),
                                new ConvertTest<char?>((long)42, true, new char?((char)'*')),
                                new ConvertTest<DateTime?>((long)42, false, default(DateTime?)),
                                new ConvertTest<decimal?>((long)42, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((long)42, true, new double?((double)42.0)),
                                new ConvertTest<float?>((long)42, true, new float?((float)42.0)),
                                new ConvertTest<int?>((long)42, true, new int?((int)42)),
                                new ConvertTest<long?>((long)42, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((long)42, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((long)42, true, new short?((short)42)),
                                new ConvertTest<uint?>((long)42, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((long)42, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((long)42, true, new PrimaryColor?((PrimaryColor)42)),
                                new ConvertTest<Guid?>((long)42, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((long)42, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((long)42, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithSByte", new IConvertTest[]
                            {
                                new ConvertTest<bool>((sbyte)42, true, (bool)true),
                                new ConvertTest<byte>((sbyte)42, true, (byte)42),
                                new ConvertTest<char>((sbyte)42, true, (char)'*'),
                                new ConvertTest<DateTime>((sbyte)42, false, default(DateTime)),
                                new ConvertTest<decimal>((sbyte)42, true, (decimal)42.0),
                                new ConvertTest<double>((sbyte)42, true, (double)42.0),
                                new ConvertTest<float>((sbyte)42, true, (float)42.0),
                                new ConvertTest<int>((sbyte)42, true, (int)42),
                                new ConvertTest<long>((sbyte)42, true, (long)42),
                                new ConvertTest<sbyte>((sbyte)42, true, (sbyte)42),
                                new ConvertTest<short>((sbyte)42, true, (short)42),
                                new ConvertTest<string>((sbyte)42, true, (string)"42"),
                                new ConvertTest<uint>((sbyte)42, true, (uint)42),
                                new ConvertTest<ulong>((sbyte)42, true, (ulong)42),
                                new ConvertTest<ushort>((sbyte)42, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((sbyte)42, true, (PrimaryColor)42),
                                new ConvertTest<Guid>((sbyte)42, false, default(Guid)),
                                new ConvertTest<Uri>((sbyte)42, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((sbyte)42, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((sbyte)42, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((sbyte)42, false, default(byte[])),
                                new ConvertTest<Type>((sbyte)42, false, default(Type)),
                                new ConvertTest<IInterface>((sbyte)42, false, default(IInterface)),
                                new ConvertTest<BaseClass>((sbyte)42, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((sbyte)42, false, default(DerivedClass)),

                                new ConvertTest<bool?>((sbyte)42, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((sbyte)42, true, new byte?((byte)42)),
                                new ConvertTest<char?>((sbyte)42, true, new char?((char)'*')),
                                new ConvertTest<DateTime?>((sbyte)42, false, default(DateTime?)),
                                new ConvertTest<decimal?>((sbyte)42, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((sbyte)42, true, new double?((double)42.0)),
                                new ConvertTest<float?>((sbyte)42, true, new float?((float)42.0)),
                                new ConvertTest<int?>((sbyte)42, true, new int?((int)42)),
                                new ConvertTest<long?>((sbyte)42, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((sbyte)42, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((sbyte)42, true, new short?((short)42)),
                                new ConvertTest<uint?>((sbyte)42, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((sbyte)42, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((sbyte)42, true, new PrimaryColor?((PrimaryColor)42)),
                                new ConvertTest<Guid?>((sbyte)42, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((sbyte)42, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((sbyte)42, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithShort", new IConvertTest[]
                            {
                                new ConvertTest<bool>((short)42, true, (bool)true),
                                new ConvertTest<byte>((short)42, true, (byte)42),
                                new ConvertTest<char>((short)42, true, (char)'*'),
                                new ConvertTest<DateTime>((short)42, false, default(DateTime)),
                                new ConvertTest<decimal>((short)42, true, (decimal)42.0),
                                new ConvertTest<double>((short)42, true, (double)42.0),
                                new ConvertTest<float>((short)42, true, (float)42.0),
                                new ConvertTest<int>((short)42, true, (int)42),
                                new ConvertTest<long>((short)42, true, (long)42),
                                new ConvertTest<sbyte>((short)42, true, (sbyte)42),
                                new ConvertTest<short>((short)42, true, (short)42),
                                new ConvertTest<string>((short)42, true, (string)"42"),
                                new ConvertTest<uint>((short)42, true, (uint)42),
                                new ConvertTest<ulong>((short)42, true, (ulong)42),
                                new ConvertTest<ushort>((short)42, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((short)42, true, (PrimaryColor)42),
                                new ConvertTest<Guid>((short)42, false, default(Guid)),
                                new ConvertTest<Uri>((short)42, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((short)42, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((short)42, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((short)42, false, default(byte[])),
                                new ConvertTest<Type>((short)42, false, default(Type)),
                                new ConvertTest<IInterface>((short)42, false, default(IInterface)),
                                new ConvertTest<BaseClass>((short)42, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((short)42, false, default(DerivedClass)),

                                new ConvertTest<bool?>((short)42, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((short)42, true, new byte?((byte)42)),
                                new ConvertTest<char?>((short)42, true, new char?((char)'*')),
                                new ConvertTest<DateTime?>((short)42, false, default(DateTime?)),
                                new ConvertTest<decimal?>((short)42, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((short)42, true, new double?((double)42.0)),
                                new ConvertTest<float?>((short)42, true, new float?((float)42.0)),
                                new ConvertTest<int?>((short)42, true, new int?((int)42)),
                                new ConvertTest<long?>((short)42, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((short)42, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((short)42, true, new short?((short)42)),
                                new ConvertTest<uint?>((short)42, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((short)42, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((short)42, true, new PrimaryColor?((PrimaryColor)42)),
                                new ConvertTest<Guid?>((short)42, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((short)42, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((short)42, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithString", new IConvertTest[]
                            {
                                new ConvertTest<bool>("False", true, (bool)false),
                                new ConvertTest<bool>("True", true, (bool)true),
                                new ConvertTest<byte>("42", true, (byte)42),
                                new ConvertTest<char>("*", true, '*'),
                                new ConvertTest<DateTime>(TestDateTimeString, true, TestDateTime),
                                new ConvertTest<decimal>("42.1", true, (decimal)42.1),
                                new ConvertTest<double>("42.2", true, (double)42.2),
                                new ConvertTest<float>("42.3", true, (float)42.3),
                                new ConvertTest<int>("42", true, (int)42),
                                new ConvertTest<long>("42", true, (long)42),
                                new ConvertTest<sbyte>("42", true, (sbyte)42),
                                new ConvertTest<short>("42", true, (short)42),
                                new ConvertTest<string>("The quick brown fox jumps over the lazy dog", true, "The quick brown fox jumps over the lazy dog"),
                                new ConvertTest<uint>("42", true, (uint)42),
                                new ConvertTest<ulong>("42", true, (ulong)42),
                                new ConvertTest<ushort>("42", true, (ushort)42),
                                new ConvertTest<PrimaryColor>(TestBlueOrdinalValue42.ToString(CultureInfo.InvariantCulture), true, (PrimaryColor)TestBlueOrdinalValue42),
                                new ConvertTest<PrimaryColor>(TestBlueString, true, (PrimaryColor)PrimaryColor.Blue),
                                new ConvertTest<Guid>(TestGuidString, true, TestGuid),
                                new ConvertTest<Uri>(TestUriString, true, TestUri),
                                new ConvertTest<DateTimeOffset>(TestDateTimeOffsetString, true, TestDateTimeOffset),
                                new ConvertTest<TimeSpan>(TestTimeSpanString, true, TestTimeSpan),
                                new ConvertTest<byte[]>(TestByteArrayString, true, TestByteArray),
                                new ConvertTest<Type>(TestTypeString, true, TestType),
                                new ConvertTest<IInterface>("42", false, default(IInterface)),
                                new ConvertTest<BaseClass>("42", false, default(BaseClass)),
                                new ConvertTest<DerivedClass>("42", false, default(DerivedClass)),

                                new ConvertTest<bool?>("False", true, new bool?((bool)false)),
                                new ConvertTest<bool?>("True", true, new bool?((bool)true)),
                                new ConvertTest<byte?>("42", true, new byte?((byte)42)),
                                new ConvertTest<char?>("*", true, new char?((char)'*')),
                                new ConvertTest<DateTime?>(TestDateTimeString, true, new DateTime?(TestDateTime)),
                                new ConvertTest<decimal?>("42.1", true, new decimal?((decimal)42.1)),
                                new ConvertTest<double?>("42.2", true, new double?((double)42.2)),
                                new ConvertTest<float?>("42.3", true, new float?((float)42.3)),
                                new ConvertTest<int?>("42", true, new int?((int)42)),
                                new ConvertTest<long?>("42", true, new long?((long)42)),
                                new ConvertTest<sbyte?>("42", true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>("42", true, new short?((short)42)),
                                new ConvertTest<uint?>("42", true, new uint?((uint)42)),
                                new ConvertTest<ulong?>("42", true, new ulong?((ulong)42)),
                                new ConvertTest<ushort?>("42", true, new ushort?((ushort)42)),
                                new ConvertTest<PrimaryColor?>(TestBlueOrdinalValue42.ToString(CultureInfo.InvariantCulture), true, new PrimaryColor?((PrimaryColor)TestBlueOrdinalValue42)),
                                new ConvertTest<PrimaryColor?>(TestBlueString, true, new PrimaryColor?(PrimaryColor.Blue)),
                                new ConvertTest<Guid?>(TestGuidString, true, new Guid?(TestGuid)),
                                new ConvertTest<DateTimeOffset?>(TestDateTimeOffsetString, true, new DateTimeOffset?(TestDateTimeOffset)),
                                new ConvertTest<TimeSpan?>(TestTimeSpanString, true, new TimeSpan?(TestTimeSpan))
                            }
                    },
                new object[]
                    {
                        "WithUInt", new IConvertTest[]
                            {
                                new ConvertTest<bool>((uint)42, true, (bool)true),
                                new ConvertTest<byte>((uint)42, true, (byte)42),
                                new ConvertTest<char>((uint)42, true, (char)'*'),
                                new ConvertTest<DateTime>((uint)42, false, default(DateTime)),
                                new ConvertTest<decimal>((uint)42, true, (decimal)42.0),
                                new ConvertTest<double>((uint)42, true, (double)42.0),
                                new ConvertTest<float>((uint)42, true, (float)42.0),
                                new ConvertTest<int>((uint)42, true, (int)42),
                                new ConvertTest<long>((uint)42, true, (long)42),
                                new ConvertTest<sbyte>((uint)42, true, (sbyte)42),
                                new ConvertTest<short>((uint)42, true, (short)42),
                                new ConvertTest<string>((uint)42, true, (string)"42"),
                                new ConvertTest<uint>((uint)42, true, (uint)42),
                                new ConvertTest<ulong>((uint)42, true, (ulong)42),
                                new ConvertTest<ushort>((uint)42, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((uint)42, true, (PrimaryColor)42),
                                new ConvertTest<Guid>((uint)42, false, default(Guid)),
                                new ConvertTest<Uri>((uint)42, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((uint)42, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((uint)42, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((uint)42, false, default(byte[])),
                                new ConvertTest<Type>((uint)42, false, default(Type)),
                                new ConvertTest<IInterface>((uint)42, false, default(IInterface)),
                                new ConvertTest<BaseClass>((uint)42, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((uint)42, false, default(DerivedClass)),

                                new ConvertTest<bool?>((uint)42, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((uint)42, true, new byte?((byte)42)),
                                new ConvertTest<char?>((uint)42, true, new char?((char)'*')),
                                new ConvertTest<DateTime?>((uint)42, false, default(DateTime?)),
                                new ConvertTest<decimal?>((uint)42, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((uint)42, true, new double?((double)42.0)),
                                new ConvertTest<float?>((uint)42, true, new float?((float)42.0)),
                                new ConvertTest<int?>((uint)42, true, new int?((int)42)),
                                new ConvertTest<long?>((uint)42, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((uint)42, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((uint)42, true, new short?((short)42)),
                                new ConvertTest<uint?>((uint)42, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((uint)42, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((uint)42, true, new PrimaryColor?((PrimaryColor)42)),
                                new ConvertTest<Guid?>((uint)42, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((uint)42, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((uint)42, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithULong", new IConvertTest[]
                            {
                                new ConvertTest<bool>((ulong)42, true, (bool)true),
                                new ConvertTest<byte>((ulong)42, true, (byte)42),
                                new ConvertTest<char>((ulong)42, true, (char)'*'),
                                new ConvertTest<DateTime>((ulong)42, false, default(DateTime)),
                                new ConvertTest<decimal>((ulong)42, true, (decimal)42.0),
                                new ConvertTest<double>((ulong)42, true, (double)42.0),
                                new ConvertTest<float>((ulong)42, true, (float)42.0),
                                new ConvertTest<int>((ulong)42, true, (int)42),
                                new ConvertTest<long>((ulong)42, true, (long)42),
                                new ConvertTest<sbyte>((ulong)42, true, (sbyte)42),
                                new ConvertTest<short>((ulong)42, true, (short)42),
                                new ConvertTest<string>((ulong)42, true, (string)"42"),
                                new ConvertTest<uint>((ulong)42, true, (uint)42),
                                new ConvertTest<ulong>((ulong)42, true, (ulong)42),
                                new ConvertTest<ushort>((ulong)42, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((ulong)42, true, (PrimaryColor)42),
                                new ConvertTest<Guid>((ulong)42, false, default(Guid)),
                                new ConvertTest<Uri>((ulong)42, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((ulong)42, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((ulong)42, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((ulong)42, false, default(byte[])),
                                new ConvertTest<Type>((ulong)42, false, default(Type)),
                                new ConvertTest<IInterface>((ulong)42, false, default(IInterface)),
                                new ConvertTest<BaseClass>((ulong)42, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((ulong)42, false, default(DerivedClass)),

                                new ConvertTest<bool?>((ulong)42, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((ulong)42, true, new byte?((byte)42)),
                                new ConvertTest<char?>((ulong)42, true, new char?((char)'*')),
                                new ConvertTest<DateTime?>((ulong)42, false, default(DateTime?)),
                                new ConvertTest<decimal?>((ulong)42, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((ulong)42, true, new double?((double)42.0)),
                                new ConvertTest<float?>((ulong)42, true, new float?((float)42.0)),
                                new ConvertTest<int?>((ulong)42, true, new int?((int)42)),
                                new ConvertTest<long?>((ulong)42, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((ulong)42, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((ulong)42, true, new short?((short)42)),
                                new ConvertTest<uint?>((ulong)42, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((ulong)42, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((ulong)42, true, new PrimaryColor?((PrimaryColor)42)),
                                new ConvertTest<Guid?>((ulong)42, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((ulong)42, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((ulong)42, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithUShort", new IConvertTest[]
                            {
                                new ConvertTest<bool>((ushort)42, true, (bool)true),
                                new ConvertTest<byte>((ushort)42, true, (byte)42),
                                new ConvertTest<char>((ushort)42, true, (char)'*'),
                                new ConvertTest<DateTime>((ushort)42, false, default(DateTime)),
                                new ConvertTest<decimal>((ushort)42, true, (decimal)42.0),
                                new ConvertTest<double>((ushort)42, true, (double)42.0),
                                new ConvertTest<float>((ushort)42, true, (float)42.0),
                                new ConvertTest<int>((ushort)42, true, (int)42),
                                new ConvertTest<long>((ushort)42, true, (long)42),
                                new ConvertTest<sbyte>((ushort)42, true, (sbyte)42),
                                new ConvertTest<short>((ushort)42, true, (short)42),
                                new ConvertTest<string>((ushort)42, true, (string)"42"),
                                new ConvertTest<uint>((ushort)42, true, (uint)42),
                                new ConvertTest<ulong>((ushort)42, true, (ulong)42),
                                new ConvertTest<ushort>((ushort)42, true, (ushort)42),
                                new ConvertTest<PrimaryColor>((ushort)42, true, (PrimaryColor)42),
                                new ConvertTest<Guid>((ushort)42, false, default(Guid)),
                                new ConvertTest<Uri>((ushort)42, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>((ushort)42, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>((ushort)42, false, default(TimeSpan)),
                                new ConvertTest<byte[]>((ushort)42, false, default(byte[])),
                                new ConvertTest<Type>((ushort)42, false, default(Type)),
                                new ConvertTest<IInterface>((ushort)42, false, default(IInterface)),
                                new ConvertTest<BaseClass>((ushort)42, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>((ushort)42, false, default(DerivedClass)),

                                new ConvertTest<bool?>((ushort)42, true, new bool?((bool)true)),
                                new ConvertTest<byte?>((ushort)42, true, new byte?((byte)42)),
                                new ConvertTest<char?>((ushort)42, true, new char?((char)'*')),
                                new ConvertTest<DateTime?>((ushort)42, false, default(DateTime?)),
                                new ConvertTest<decimal?>((ushort)42, true, new decimal?((decimal)42.0)),
                                new ConvertTest<double?>((ushort)42, true, new double?((double)42.0)),
                                new ConvertTest<float?>((ushort)42, true, new float?((float)42.0)),
                                new ConvertTest<int?>((ushort)42, true, new int?((int)42)),
                                new ConvertTest<long?>((ushort)42, true, new long?((long)42)),
                                new ConvertTest<sbyte?>((ushort)42, true, new sbyte?((sbyte)42)),
                                new ConvertTest<short?>((ushort)42, true, new short?((short)42)),
                                new ConvertTest<uint?>((ushort)42, true, new uint?((uint)42)),
                                new ConvertTest<ulong?>((ushort)42, true, new ulong?((ulong)42)),
                                new ConvertTest<PrimaryColor?>((ushort)42, true, new PrimaryColor?((PrimaryColor)42)),
                                new ConvertTest<Guid?>((ushort)42, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>((ushort)42, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>((ushort)42, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithEnum", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestEnum, true, (bool)true),
                                new ConvertTest<byte>(TestEnum, true, (byte)TestEnumOrdinal),
                                new ConvertTest<char>(TestEnum, true, (char)TestEnumOrdinal),
                                new ConvertTest<DateTime>(TestEnum, false, default(DateTime)),
                                new ConvertTest<decimal>(TestEnum, true, (decimal)TestEnumOrdinal),
                                new ConvertTest<double>(TestEnum, true, (double)TestEnumOrdinal),
                                new ConvertTest<float>(TestEnum, true, (float)TestEnumOrdinal),
                                new ConvertTest<int>(TestEnum, true, (int)TestEnumOrdinal),
                                new ConvertTest<long>(TestEnum, true, (long)TestEnumOrdinal),
                                new ConvertTest<sbyte>(TestEnum, true, (sbyte)TestEnumOrdinal),
                                new ConvertTest<short>(TestEnum, true, (short)TestEnumOrdinal),
                                new ConvertTest<string>(TestEnum, true, TestEnumString),
                                new ConvertTest<uint>(TestEnum, true, (uint)TestEnumOrdinal),
                                new ConvertTest<ulong>(TestEnum, true, (ulong)TestEnumOrdinal),
                                new ConvertTest<ushort>(TestEnum, true, (ushort)TestEnumOrdinal),
                                new ConvertTest<PrimaryColor>(TestEnum, true, TestEnum),
                                new ConvertTest<Guid>(TestEnum, false, default(Guid)),
                                new ConvertTest<Uri>(TestEnum, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(TestEnum, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(TestEnum, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(TestEnum, false, default(byte[])),
                                new ConvertTest<Type>(TestEnum, false, default(Type)),
                                new ConvertTest<IInterface>(TestEnum, false, default(IInterface)),
                                new ConvertTest<BaseClass>(TestEnum, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>(TestEnum, false, default(DerivedClass)),

                                new ConvertTest<bool?>(TestEnum, true, new bool?((bool)true)),
                                new ConvertTest<byte?>(TestEnum, true, new byte?((byte)TestEnumOrdinal)),
                                new ConvertTest<char?>(TestEnum, true, new char?((char)TestEnumOrdinal)),
                                new ConvertTest<DateTime?>(TestEnum, false, default(DateTime?)),
                                new ConvertTest<decimal?>(TestEnum, true, new decimal?((decimal)TestEnumOrdinal)),
                                new ConvertTest<double?>(TestEnum, true, new double?((double)TestEnumOrdinal)),
                                new ConvertTest<float?>(TestEnum, true, new float?((float)TestEnumOrdinal)),
                                new ConvertTest<int?>(TestEnum, true, new int?((int)TestEnumOrdinal)),
                                new ConvertTest<long?>(TestEnum, true, new long?((long)TestEnumOrdinal)),
                                new ConvertTest<sbyte?>(TestEnum, true, new sbyte?((sbyte)TestEnumOrdinal)),
                                new ConvertTest<short?>(TestEnum, true, new short?((short)TestEnumOrdinal)),
                                new ConvertTest<uint?>(TestEnum, true, new uint?((uint)TestEnumOrdinal)),
                                new ConvertTest<ulong?>(TestEnum, true, new ulong?((ulong)TestEnumOrdinal)),
                                new ConvertTest<PrimaryColor?>(TestEnum, true, new PrimaryColor?(TestEnum)),
                                new ConvertTest<Guid?>(TestEnum, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(TestEnum, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>(TestEnum, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithGuid", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestGuid, false, default(bool)),
                                new ConvertTest<byte>(TestGuid, false, default(byte)),
                                new ConvertTest<char>(TestGuid, false, default(char)),
                                new ConvertTest<DateTime>(TestGuid, false, default(DateTime)),
                                new ConvertTest<decimal>(TestGuid, false, default(decimal)),
                                new ConvertTest<double>(TestGuid, false, default(double)),
                                new ConvertTest<float>(TestGuid, false, default(float)),
                                new ConvertTest<int>(TestGuid, false, default(int)),
                                new ConvertTest<long>(TestGuid, false, default(long)),
                                new ConvertTest<sbyte>(TestGuid, false, default(sbyte)),
                                new ConvertTest<short>(TestGuid, false, default(short)),
                                new ConvertTest<string>(TestGuid, true, TestGuidString),
                                new ConvertTest<uint>(TestGuid, false, default(uint)),
                                new ConvertTest<ulong>(TestGuid, false, default(ulong)),
                                new ConvertTest<ushort>(TestGuid, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(TestGuid, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(TestGuid, true, TestGuid),
                                new ConvertTest<Uri>(TestGuid, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(TestGuid, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(TestGuid, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(TestGuid, true, TestGuidByteArray),
                                new ConvertTest<Type>(TestGuid, false, default(Type)),
                                new ConvertTest<IInterface>(TestGuid, false, default(IInterface)),
                                new ConvertTest<BaseClass>(TestGuid, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>(TestGuid, false, default(DerivedClass)),

                                new ConvertTest<bool?>(TestGuid, false, default(bool?)),
                                new ConvertTest<byte?>(TestGuid, false, default(byte?)),
                                new ConvertTest<char?>(TestGuid, false, default(char?)),
                                new ConvertTest<DateTime?>(TestGuid, false, default(DateTime?)),
                                new ConvertTest<decimal?>(TestGuid, false, default(decimal?)),
                                new ConvertTest<double?>(TestGuid, false, default(double?)),
                                new ConvertTest<float?>(TestGuid, false, default(float?)),
                                new ConvertTest<int?>(TestGuid, false, default(int?)),
                                new ConvertTest<long?>(TestGuid, false, default(long?)),
                                new ConvertTest<sbyte?>(TestGuid, false, default(sbyte?)),
                                new ConvertTest<short?>(TestGuid, false, default(short?)),
                                new ConvertTest<uint?>(TestGuid, false, default(uint?)),
                                new ConvertTest<ulong?>(TestGuid, false, default(ulong?)),
                                new ConvertTest<ushort?>(TestGuid, false, default(ushort?)),
                                new ConvertTest<PrimaryColor?>(TestGuid, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(TestGuid, true, new Guid?(TestGuid)),
                                new ConvertTest<DateTimeOffset?>(TestGuid, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>(TestGuid, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithUri", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestUri, false, default(bool)),
                                new ConvertTest<byte>(TestUri, false, default(byte)),
                                new ConvertTest<char>(TestUri, false, default(char)),
                                new ConvertTest<DateTime>(TestUri, false, default(DateTime)),
                                new ConvertTest<decimal>(TestUri, false, default(decimal)),
                                new ConvertTest<double>(TestUri, false, default(double)),
                                new ConvertTest<float>(TestUri, false, default(float)),
                                new ConvertTest<int>(TestUri, false, default(int)),
                                new ConvertTest<long>(TestUri, false, default(long)),
                                new ConvertTest<sbyte>(TestUri, false, default(sbyte)),
                                new ConvertTest<short>(TestUri, false, default(short)),
                                new ConvertTest<string>(TestUri, true, TestUriString),
                                new ConvertTest<uint>(TestUri, false, default(uint)),
                                new ConvertTest<ulong>(TestUri, false, default(ulong)),
                                new ConvertTest<ushort>(TestUri, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(TestUri, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(TestUri, false, default(Guid)),
                                new ConvertTest<Uri>(TestUri, true, TestUri),
                                new ConvertTest<DateTimeOffset>(TestUri, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(TestUri, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(TestUri, false, default(byte[])),
                                new ConvertTest<Type>(TestUri, false, default(Type)),
                                new ConvertTest<IInterface>(TestUri, false, default(IInterface)),
                                new ConvertTest<BaseClass>(TestUri, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>(TestUri, false, default(DerivedClass)),

                                new ConvertTest<bool?>(TestUri, false, default(bool?)),
                                new ConvertTest<byte?>(TestUri, false, default(byte?)),
                                new ConvertTest<char?>(TestUri, false, default(char?)),
                                new ConvertTest<DateTime?>(TestUri, false, default(DateTime?)),
                                new ConvertTest<decimal?>(TestUri, false, default(decimal?)),
                                new ConvertTest<double?>(TestUri, false, default(double?)),
                                new ConvertTest<float?>(TestUri, false, default(float?)),
                                new ConvertTest<int?>(TestUri, false, default(int?)),
                                new ConvertTest<long?>(TestUri, false, default(long?)),
                                new ConvertTest<sbyte?>(TestUri, false, default(sbyte?)),
                                new ConvertTest<short?>(TestUri, false, default(short?)),
                                new ConvertTest<uint?>(TestUri, false, default(uint?)),
                                new ConvertTest<ulong?>(TestUri, false, default(ulong?)),
                                new ConvertTest<ushort?>(TestUri, false, default(ushort?)),
                                new ConvertTest<PrimaryColor?>(TestUri, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(TestUri, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(TestUri, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>(TestUri, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithDateTimeOffset", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestDateTimeOffset, false, default(bool)),
                                new ConvertTest<byte>(TestDateTimeOffset, false, default(byte)),
                                new ConvertTest<char>(TestDateTimeOffset, false, default(char)),
                                new ConvertTest<DateTime>(TestDateTimeOffset, true, TestDateTime),
                                new ConvertTest<decimal>(TestDateTimeOffset, false, default(decimal)),
                                new ConvertTest<double>(TestDateTimeOffset, false, default(double)),
                                new ConvertTest<float>(TestDateTimeOffset, false, default(float)),
                                new ConvertTest<int>(TestDateTimeOffset, false, default(int)),
                                new ConvertTest<long>(TestDateTimeOffset, false, default(long)),
                                new ConvertTest<sbyte>(TestDateTimeOffset, false, default(sbyte)),
                                new ConvertTest<short>(TestDateTimeOffset, false, default(short)),
                                new ConvertTest<string>(TestDateTimeOffset, true, TestDateTimeOffsetString),
                                new ConvertTest<uint>(TestDateTimeOffset, false, default(uint)),
                                new ConvertTest<ulong>(TestDateTimeOffset, false, default(ulong)),
                                new ConvertTest<ushort>(TestDateTimeOffset, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(TestDateTimeOffset, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(TestDateTimeOffset, false, default(Guid)),
                                new ConvertTest<Uri>(TestDateTimeOffset, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(TestDateTimeOffset, true, TestDateTimeOffset),
                                new ConvertTest<TimeSpan>(TestDateTimeOffset, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(TestDateTimeOffset, false, default(byte[])),
                                new ConvertTest<Type>(TestDateTimeOffset, false, default(Type)),
                                new ConvertTest<IInterface>(TestDateTimeOffset, false, default(IInterface)),
                                new ConvertTest<BaseClass>(TestDateTimeOffset, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>(TestDateTimeOffset, false, default(DerivedClass)),

                                new ConvertTest<bool?>(TestDateTimeOffset, false, default(bool?)),
                                new ConvertTest<byte?>(TestDateTimeOffset, false, default(byte?)),
                                new ConvertTest<char?>(TestDateTimeOffset, false, default(char?)),
                                new ConvertTest<DateTime?>(TestDateTimeOffset, true, new DateTime?(TestDateTime)),
                                new ConvertTest<decimal?>(TestDateTimeOffset, false, default(decimal?)),
                                new ConvertTest<double?>(TestDateTimeOffset, false, default(double?)),
                                new ConvertTest<float?>(TestDateTimeOffset, false, default(float?)),
                                new ConvertTest<int?>(TestDateTimeOffset, false, default(int?)),
                                new ConvertTest<long?>(TestDateTimeOffset, false, default(long?)),
                                new ConvertTest<sbyte?>(TestDateTimeOffset, false, default(sbyte?)),
                                new ConvertTest<short?>(TestDateTimeOffset, false, default(short?)),
                                new ConvertTest<uint?>(TestDateTimeOffset, false, default(uint?)),
                                new ConvertTest<ulong?>(TestDateTimeOffset, false, default(ulong?)),
                                new ConvertTest<ushort?>(TestDateTimeOffset, false, default(ushort?)),
                                new ConvertTest<PrimaryColor?>(TestDateTimeOffset, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(TestDateTimeOffset, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(TestDateTimeOffset, true, new DateTimeOffset?(TestDateTimeOffset)),
                                new ConvertTest<TimeSpan?>(TestDateTimeOffset, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithTimeSpan", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestTimeSpan, false, default(bool)),
                                new ConvertTest<byte>(TestTimeSpan, false, default(byte)),
                                new ConvertTest<char>(TestTimeSpan, false, default(char)),
                                new ConvertTest<DateTime>(TestTimeSpan, false, default(DateTime)),
                                new ConvertTest<decimal>(TestTimeSpan, false, default(decimal)),
                                new ConvertTest<double>(TestTimeSpan, false, default(double)),
                                new ConvertTest<float>(TestTimeSpan, false, default(float)),
                                new ConvertTest<int>(TestTimeSpan, false, default(int)),
                                new ConvertTest<long>(TestTimeSpan, false, default(long)),
                                new ConvertTest<sbyte>(TestTimeSpan, false, default(sbyte)),
                                new ConvertTest<short>(TestTimeSpan, false, default(short)),
                                new ConvertTest<string>(TestTimeSpan, true, TestTimeSpanString),
                                new ConvertTest<uint>(TestTimeSpan, false, default(uint)),
                                new ConvertTest<ulong>(TestTimeSpan, false, default(ulong)),
                                new ConvertTest<ushort>(TestTimeSpan, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(TestTimeSpan, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(TestTimeSpan, false, default(Guid)),
                                new ConvertTest<Uri>(TestTimeSpan, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(TestTimeSpan, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(TestTimeSpan, true, TestTimeSpan),
                                new ConvertTest<byte[]>(TestTimeSpan, false, default(byte[])),
                                new ConvertTest<Type>(TestTimeSpan, false, default(Type)),
                                new ConvertTest<IInterface>(TestTimeSpan, false, default(IInterface)),
                                new ConvertTest<BaseClass>(TestTimeSpan, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>(TestTimeSpan, false, default(DerivedClass)),

                                new ConvertTest<bool?>(TestTimeSpan, false, default(bool?)),
                                new ConvertTest<byte?>(TestTimeSpan, false, default(byte?)),
                                new ConvertTest<char?>(TestTimeSpan, false, default(char?)),
                                new ConvertTest<DateTime?>(TestTimeSpan, false, default(DateTime?)),
                                new ConvertTest<decimal?>(TestTimeSpan, false, default(decimal?)),
                                new ConvertTest<double?>(TestTimeSpan, false, default(double?)),
                                new ConvertTest<float?>(TestTimeSpan, false, default(float?)),
                                new ConvertTest<int?>(TestTimeSpan, false, default(int?)),
                                new ConvertTest<long?>(TestTimeSpan, false, default(long?)),
                                new ConvertTest<sbyte?>(TestTimeSpan, false, default(sbyte?)),
                                new ConvertTest<short?>(TestTimeSpan, false, default(short?)),
                                new ConvertTest<uint?>(TestTimeSpan, false, default(uint?)),
                                new ConvertTest<ulong?>(TestTimeSpan, false, default(ulong?)),
                                new ConvertTest<ushort?>(TestTimeSpan, false, default(ushort?)),
                                new ConvertTest<PrimaryColor?>(TestTimeSpan, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(TestTimeSpan, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(TestTimeSpan, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>(TestTimeSpan, true, new TimeSpan?(TestTimeSpan))
                            }
                    },
                new object[]
                    {
                        "WithByteArray", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestByteArray, false, default(bool)),
                                new ConvertTest<byte>(TestByteArray, false, default(byte)),
                                new ConvertTest<char>(TestByteArray, false, default(char)),
                                new ConvertTest<DateTime>(TestByteArray, false, default(DateTime)),
                                new ConvertTest<decimal>(TestByteArray, false, default(decimal)),
                                new ConvertTest<double>(TestByteArray, false, default(double)),
                                new ConvertTest<float>(TestByteArray, false, default(float)),
                                new ConvertTest<int>(TestByteArray, false, default(int)),
                                new ConvertTest<long>(TestByteArray, false, default(long)),
                                new ConvertTest<sbyte>(TestByteArray, false, default(sbyte)),
                                new ConvertTest<short>(TestByteArray, false, default(short)),
                                new ConvertTest<string>(TestByteArray, true, TestByteArrayString),
                                new ConvertTest<uint>(TestByteArray, false, default(uint)),
                                new ConvertTest<ulong>(TestByteArray, false, default(ulong)),
                                new ConvertTest<ushort>(TestByteArray, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(TestByteArray, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(TestByteArray, false, default(Guid)),
                                new ConvertTest<Uri>(TestByteArray, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(TestByteArray, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(TestByteArray, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(TestByteArray, true, TestByteArray),
                                new ConvertTest<Type>(TestByteArray, false, default(Type)),
                                new ConvertTest<IInterface>(TestByteArray, false, default(IInterface)),
                                new ConvertTest<BaseClass>(TestByteArray, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>(TestByteArray, false, default(DerivedClass)),

                                new ConvertTest<bool?>(TestByteArray, false, default(bool?)),
                                new ConvertTest<byte?>(TestByteArray, false, default(byte?)),
                                new ConvertTest<char?>(TestByteArray, false, default(char?)),
                                new ConvertTest<DateTime?>(TestByteArray, false, default(DateTime?)),
                                new ConvertTest<decimal?>(TestByteArray, false, default(decimal?)),
                                new ConvertTest<double?>(TestByteArray, false, default(double?)),
                                new ConvertTest<float?>(TestByteArray, false, default(float?)),
                                new ConvertTest<int?>(TestByteArray, false, default(int?)),
                                new ConvertTest<long?>(TestByteArray, false, default(long?)),
                                new ConvertTest<sbyte?>(TestByteArray, false, default(sbyte?)),
                                new ConvertTest<short?>(TestByteArray, false, default(short?)),
                                new ConvertTest<uint?>(TestByteArray, false, default(uint?)),
                                new ConvertTest<ulong?>(TestByteArray, false, default(ulong?)),
                                new ConvertTest<ushort?>(TestByteArray, false, default(ushort?)),
                                new ConvertTest<PrimaryColor?>(TestByteArray, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(TestByteArray, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(TestByteArray, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>(TestByteArray, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithType", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestType, false, default(bool)),
                                new ConvertTest<byte>(TestType, false, default(byte)),
                                new ConvertTest<char>(TestType, false, default(char)),
                                new ConvertTest<DateTime>(TestType, false, default(DateTime)),
                                new ConvertTest<decimal>(TestType, false, default(decimal)),
                                new ConvertTest<double>(TestType, false, default(double)),
                                new ConvertTest<float>(TestType, false, default(float)),
                                new ConvertTest<int>(TestType, false, default(int)),
                                new ConvertTest<long>(TestType, false, default(long)),
                                new ConvertTest<sbyte>(TestType, false, default(sbyte)),
                                new ConvertTest<short>(TestType, false, default(short)),
                                new ConvertTest<string>(TestType, true, TestTypeString),
                                new ConvertTest<uint>(TestType, false, default(uint)),
                                new ConvertTest<ulong>(TestType, false, default(ulong)),
                                new ConvertTest<ushort>(TestType, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(TestType, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(TestType, false, default(Guid)),
                                new ConvertTest<Uri>(TestType, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(TestType, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(TestType, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(TestType, false, default(byte[])),
                                new ConvertTest<Type>(TestType, true, TestType),
                                new ConvertTest<IInterface>(TestType, false, default(IInterface)),
                                new ConvertTest<BaseClass>(TestType, false, default(BaseClass)),
                                new ConvertTest<DerivedClass>(TestType, false, default(DerivedClass)),

                                new ConvertTest<bool?>(TestType, false, default(bool?)),
                                new ConvertTest<byte?>(TestType, false, default(byte?)),
                                new ConvertTest<char?>(TestType, false, default(char?)),
                                new ConvertTest<DateTime?>(TestType, false, default(DateTime?)),
                                new ConvertTest<decimal?>(TestType, false, default(decimal?)),
                                new ConvertTest<double?>(TestType, false, default(double?)),
                                new ConvertTest<float?>(TestType, false, default(float?)),
                                new ConvertTest<int?>(TestType, false, default(int?)),
                                new ConvertTest<long?>(TestType, false, default(long?)),
                                new ConvertTest<sbyte?>(TestType, false, default(sbyte?)),
                                new ConvertTest<short?>(TestType, false, default(short?)),
                                new ConvertTest<uint?>(TestType, false, default(uint?)),
                                new ConvertTest<ulong?>(TestType, false, default(ulong?)),
                                new ConvertTest<ushort?>(TestType, false, default(ushort?)),
                                new ConvertTest<PrimaryColor?>(TestType, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(TestType, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(TestType, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>(TestType, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithNull", new IConvertTest[]
                            {
                                new ConvertTest<bool>(null, false, default(bool)),
                                new ConvertTest<byte>(null, false, default(byte)),
                                new ConvertTest<char>(null, false, default(char)),
                                new ConvertTest<DateTime>(null, false, default(DateTime)),
                                new ConvertTest<decimal>(null, false, default(decimal)),
                                new ConvertTest<double>(null, false, default(double)),
                                new ConvertTest<float>(null, false, default(float)),
                                new ConvertTest<int>(null, false, default(int)),
                                new ConvertTest<long>(null, false, default(long)),
                                new ConvertTest<sbyte>(null, false, default(sbyte)),
                                new ConvertTest<short>(null, false, default(short)),
                                new ConvertTest<string>(null, true, default(string)),
                                new ConvertTest<uint>(null, false, default(uint)),
                                new ConvertTest<ulong>(null, false, default(ulong)),
                                new ConvertTest<ushort>(null, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(null, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(null, false, default(Guid)),
                                new ConvertTest<Uri>(null, true, default(Uri)),
                                new ConvertTest<DateTimeOffset>(null, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(null, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(null, true, default(byte[])),
                                new ConvertTest<Type>(null, true, default(Type)),
                                new ConvertTest<IInterface>(null, true, default(IInterface)),
                                new ConvertTest<BaseClass>(null, true, default(BaseClass)),
                                new ConvertTest<DerivedClass>(null, true, default(DerivedClass)),

                                new ConvertTest<bool?>(null, true, new bool?()),
                                new ConvertTest<byte?>(null, true, new byte?()),
                                new ConvertTest<char?>(null, true, new char?()),
                                new ConvertTest<DateTime?>(null, true, new DateTime?()),
                                new ConvertTest<decimal?>(null, true, new decimal?()),
                                new ConvertTest<double?>(null, true, new double?()),
                                new ConvertTest<float?>(null, true, new float?()),
                                new ConvertTest<int?>(null, true, new int?()),
                                new ConvertTest<long?>(null, true, new long?()),
                                new ConvertTest<sbyte?>(null, true, new sbyte?()),
                                new ConvertTest<short?>(null, true, new short?()),
                                new ConvertTest<uint?>(null, true, new uint?()),
                                new ConvertTest<ulong?>(null, true, new ulong?()),
                                new ConvertTest<ushort?>(null, true, new ushort?()),
                                new ConvertTest<PrimaryColor?>(null, true, new PrimaryColor?()),
                                new ConvertTest<Guid?>(null, true, new Guid?()),
                                new ConvertTest<DateTimeOffset?>(null, true, new DateTimeOffset?()),
                                new ConvertTest<TimeSpan?>(null, true, new TimeSpan?())
                            }
                    },
                new object[]
                    {
                        "WithBaseClass", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestBaseClass, false, default(bool)),
                                new ConvertTest<byte>(TestBaseClass, false, default(byte)),
                                new ConvertTest<char>(TestBaseClass, false, default(char)),
                                new ConvertTest<DateTime>(TestBaseClass, false, default(DateTime)),
                                new ConvertTest<decimal>(TestBaseClass, false, default(decimal)),
                                new ConvertTest<double>(TestBaseClass, false, default(double)),
                                new ConvertTest<float>(TestBaseClass, false, default(float)),
                                new ConvertTest<int>(TestBaseClass, false, default(int)),
                                new ConvertTest<long>(TestBaseClass, false, default(long)),
                                new ConvertTest<sbyte>(TestBaseClass, false, default(sbyte)),
                                new ConvertTest<short>(TestBaseClass, false, default(short)),
                                new ConvertTest<string>(TestBaseClass, false, default(string)),
                                new ConvertTest<uint>(TestBaseClass, false, default(uint)),
                                new ConvertTest<ulong>(TestBaseClass, false, default(ulong)),
                                new ConvertTest<ushort>(TestBaseClass, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(TestBaseClass, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(TestBaseClass, false, default(Guid)),
                                new ConvertTest<Uri>(TestBaseClass, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(TestBaseClass, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(TestBaseClass, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(TestBaseClass, false, default(byte[])),
                                new ConvertTest<Type>(TestBaseClass, false, default(Type)),
                                new ConvertTest<IInterface>(TestBaseClass, true, (IInterface)TestBaseClass),
                                new ConvertTest<BaseClass>(TestBaseClass, true, TestBaseClass),
                                new ConvertTest<DerivedClass>(TestBaseClass, false, default(DerivedClass)),

                                new ConvertTest<bool?>(TestBaseClass, false, default(bool?)),
                                new ConvertTest<byte?>(TestBaseClass, false, default(byte?)),
                                new ConvertTest<char?>(TestBaseClass, false, default(char?)),
                                new ConvertTest<DateTime?>(TestBaseClass, false, default(DateTime?)),
                                new ConvertTest<decimal?>(TestBaseClass, false, default(decimal?)),
                                new ConvertTest<double?>(TestBaseClass, false, default(double?)),
                                new ConvertTest<float?>(TestBaseClass, false, default(float?)),
                                new ConvertTest<int?>(TestBaseClass, false, default(int?)),
                                new ConvertTest<long?>(TestBaseClass, false, default(long?)),
                                new ConvertTest<sbyte?>(TestBaseClass, false, default(sbyte?)),
                                new ConvertTest<short?>(TestBaseClass, false, default(short?)),
                                new ConvertTest<uint?>(TestBaseClass, false, default(uint?)),
                                new ConvertTest<ulong?>(TestBaseClass, false, default(ulong?)),
                                new ConvertTest<ushort?>(TestBaseClass, false, default(ushort?)),
                                new ConvertTest<PrimaryColor?>(TestBaseClass, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(TestBaseClass, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(TestBaseClass, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>(TestBaseClass, false, default(TimeSpan?))
                            }
                    },
                new object[]
                    {
                        "WithBaseClass", new IConvertTest[]
                            {
                                new ConvertTest<bool>(TestDerivedClass, false, default(bool)),
                                new ConvertTest<byte>(TestDerivedClass, false, default(byte)),
                                new ConvertTest<char>(TestDerivedClass, false, default(char)),
                                new ConvertTest<DateTime>(TestDerivedClass, false, default(DateTime)),
                                new ConvertTest<decimal>(TestDerivedClass, false, default(decimal)),
                                new ConvertTest<double>(TestDerivedClass, false, default(double)),
                                new ConvertTest<float>(TestDerivedClass, false, default(float)),
                                new ConvertTest<int>(TestDerivedClass, false, default(int)),
                                new ConvertTest<long>(TestDerivedClass, false, default(long)),
                                new ConvertTest<sbyte>(TestDerivedClass, false, default(sbyte)),
                                new ConvertTest<short>(TestDerivedClass, false, default(short)),
                                new ConvertTest<string>(TestDerivedClass, false, default(string)),
                                new ConvertTest<uint>(TestDerivedClass, false, default(uint)),
                                new ConvertTest<ulong>(TestDerivedClass, false, default(ulong)),
                                new ConvertTest<ushort>(TestDerivedClass, false, default(ushort)),
                                new ConvertTest<PrimaryColor>(TestDerivedClass, false, default(PrimaryColor)),
                                new ConvertTest<Guid>(TestDerivedClass, false, default(Guid)),
                                new ConvertTest<Uri>(TestDerivedClass, false, default(Uri)),
                                new ConvertTest<DateTimeOffset>(TestDerivedClass, false, default(DateTimeOffset)),
                                new ConvertTest<TimeSpan>(TestDerivedClass, false, default(TimeSpan)),
                                new ConvertTest<byte[]>(TestDerivedClass, false, default(byte[])),
                                new ConvertTest<Type>(TestDerivedClass, false, default(Type)),
                                new ConvertTest<IInterface>(TestDerivedClass, true, (IInterface)TestDerivedClass),
                                new ConvertTest<BaseClass>(TestDerivedClass, true, (BaseClass)TestDerivedClass),
                                new ConvertTest<DerivedClass>(TestDerivedClass, true, TestDerivedClass),

                                new ConvertTest<bool?>(TestDerivedClass, false, default(bool?)),
                                new ConvertTest<byte?>(TestDerivedClass, false, default(byte?)),
                                new ConvertTest<char?>(TestDerivedClass, false, default(char?)),
                                new ConvertTest<DateTime?>(TestDerivedClass, false, default(DateTime?)),
                                new ConvertTest<decimal?>(TestDerivedClass, false, default(decimal?)),
                                new ConvertTest<double?>(TestDerivedClass, false, default(double?)),
                                new ConvertTest<float?>(TestDerivedClass, false, default(float?)),
                                new ConvertTest<int?>(TestDerivedClass, false, default(int?)),
                                new ConvertTest<long?>(TestDerivedClass, false, default(long?)),
                                new ConvertTest<sbyte?>(TestDerivedClass, false, default(sbyte?)),
                                new ConvertTest<short?>(TestDerivedClass, false, default(short?)),
                                new ConvertTest<uint?>(TestDerivedClass, false, default(uint?)),
                                new ConvertTest<ulong?>(TestDerivedClass, false, default(ulong?)),
                                new ConvertTest<ushort?>(TestDerivedClass, false, default(ushort?)),
                                new ConvertTest<PrimaryColor?>(TestDerivedClass, false, default(PrimaryColor?)),
                                new ConvertTest<Guid?>(TestDerivedClass, false, default(Guid?)),
                                new ConvertTest<DateTimeOffset?>(TestDerivedClass, false, default(DateTimeOffset?)),
                                new ConvertTest<TimeSpan?>(TestDerivedClass, false, default(TimeSpan?))
                            }
                    },
            };
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper restore RedundantExplicitNullableCreation
        // ReSharper restore RedundantCast
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
