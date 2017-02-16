// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;

using FluentAssertions;

using JsonApiFramework.Converters;
using JsonApiFramework.Reflection;
using JsonApiFramework.XUnit;

using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Converters
{
    public abstract class ConverterTests : XUnitTests
    {
        // PUBLIC TYPES /////////////////////////////////////////////////////
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
                var stopWatch = new Stopwatch();
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
                stopWatch.Stop();
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
                this.WriteLine("  Result:    {0} executionTime = {1:0.000} ms", this.ActualResult, stopWatch.Elapsed.TotalMilliseconds);
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
                                if (TypeReflection.IsNullableType(typeof(TTarget)))
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
                if (!TypeReflection.IsNullableType(type))
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

                if (!TypeReflection.IsNullableType(valueType) && TypeReflection.IsValueType(valueType))
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

                var stopWatch = new Stopwatch();
                var actualResult = this.TypeConverter.TryConvert(source, context, out actualValue);
                stopWatch.Stop();

                this.ActualResult = actualResult ? ConvertResult.Success : ConvertResult.Failure;
                this.ActualValue = actualValue;

                var actualValueAsString = ValueAsString(this.ActualValue);
                var actualTypeAsString = TypeAsString<TTarget>();

                this.WriteLine("Actual");
                this.WriteLine("  Result:  {0} executionTime = {1:0.000} ms", this.ActualResult, stopWatch.Elapsed.TotalMilliseconds);
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
                                if (TypeReflection.IsNullableType(typeof(TTarget)))
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
                var propertyInfo = TypeReflection.GetProperty(nullableType, StaticReflection.GetMemberName<int?>(x => x.HasValue), ReflectionFlags.Public | ReflectionFlags.Instance);
                var propertyExpression = Expression.Property(instanceExpression, propertyInfo);
                var lambdaExpression = (Expression<Func<T, bool>>)Expression.Lambda(propertyExpression, instanceExpression);
                var labmda = lambdaExpression.Compile();
                var hasValue = labmda(nullable);
                return hasValue;
            }

            private static string TypeAsString<T>()
            {
                var type = typeof(T);
                if (!TypeReflection.IsNullableType(type))
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

                if (!TypeReflection.IsNullableType(valueType) && TypeReflection.IsValueType(valueType))
                    return valueAsString;

                var valueAsObject = (object)value;
                if (valueAsObject == null)
                    valueAsString = "null";
                return valueAsString;
            }
            #endregion
        }
        #endregion

        // PROTECTED FIELDS /////////////////////////////////////////////////
        #region Sample Data
        public const string DefaultDateTimeFormat = "O";
        public const string FullDateTimeFormat = "F";
        public static readonly IFormatProvider SpanishMexicoCulture = new CultureInfo("es-MX");

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
        public static readonly IFormatProvider FrenchFranceCulture = new CultureInfo("fr-FR");

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

        public static readonly Type TestType = typeof(ConverterTests);
        public static readonly string TestTypeString = TypeReflection.GetCompactQualifiedName(TestType);

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

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ConverterTests(ITestOutputHelper output, bool bufferOutput)
            : base(output, bufferOutput)
        { }
        #endregion
    }
}
