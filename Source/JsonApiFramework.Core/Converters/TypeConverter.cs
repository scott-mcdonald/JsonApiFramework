// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

using JsonApiFramework.Reflection;

namespace JsonApiFramework.Converters
{
    /// <summary>
    /// Type converter that converts from one type to another type.
    /// </summary>
    /// <notes>
    /// Major design goals:
    /// - No boxing/unboxing
    /// - No to little reflection and only upon startup
    /// - Favor generic friendly compiled lambdas over reflection
    /// - Cache any reflection or compiled lambdas for future use
    /// </notes>
    public class TypeConverter : ITypeConverter
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeConverter()
        {
            this.AddDefaultDefinitions();
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Convert Methods
        public TTarget Convert<TSource, TTarget>(TSource source, TypeConverterContext context)
        {
            TTarget target;

            // If source and target type are the same, return source.
            if (TryConvertForSameTypes(source, out target))
                return target;

            // Try convert with a type converter definition, if possible.
            if (this.TryConvertByDefinition(source, context, out target))
                return target;

            // Try direct cast from source to target, if possible.
            if (TryConvertByCast(source, out target))
                return target;

            // Try programmatic conversion for special cases, if possible.
            // 1. Nullable
            // 2. Enum

            // .. Nullable
            if (this.TryConvertForNullable(source, context, out target))
                return target;

            // .. Enum
            if (this.TryConvertForEnum(source, context, out target))
                return target;

            // Unable to convert between source and target types.
            var typeConverterException = TypeConverterException.Create<TSource, TTarget>(source);
            throw typeConverterException;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDictionary<Tuple<Type, Type>, ITypeConverterDefinition> TypeConverterDefinitions { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Convert Implementation Methods
        private static bool TryConvertForSameTypes<TSource, TTarget>(TSource source, out TTarget target)
        {
            if (typeof(TSource) != typeof(TTarget))
            {
                target = default(TTarget);
                return false;
            }

            // Will always work because they are the same type.
            target = Functions.CastSourceToTarget<TSource, TTarget>(source);
            return true;
        }

        private bool TryConvertByDefinition<TSource, TTarget>(TSource source, TypeConverterContext context, out TTarget target)
        {
            ITypeConverterDefinition<TSource, TTarget> definition;
            if (!this.TryGetTypeConverterDefinition(out definition))
            {
                target = default(TTarget);
                return false;
            }

            try
            {
                target = definition.Convert(source, context);
                return true;
            }
            catch (Exception exception)
            {
                var typeConverterException = TypeConverterException.Create<TSource, TTarget>(source, exception);
                throw typeConverterException;
            }
        }

        private static bool TryConvertByCast<TSource, TTarget>(TSource source, out TTarget target)
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            var isTargetTypeAssignableFromSourceType = targetType.IsAssignableFrom(sourceType);
            if (isTargetTypeAssignableFromSourceType == false)
            {
                target = default(TTarget);
                return false;
            }

            try
            {
                target = Functions.CastSourceToTarget<TSource, TTarget>(source);
                return true;
            }
            catch (Exception exception)
            {
                var typeConverterException = TypeConverterException.Create<TSource, TTarget>(source, exception);
                throw typeConverterException;
            }
        }

        private bool TryConvertForEnum<TSource, TTarget>(TSource source, TypeConverterContext context, out TTarget target)
        {
            var sourceType = typeof(TSource);
            var isSourceTypeEnum = sourceType.IsEnum();

            var targetType = typeof(TTarget);
            var isTargetTypeEnum = targetType.IsEnum();

            if (isSourceTypeEnum)
            {
                target = !isTargetTypeEnum
                    ? Functions.ConvertEnumSourceToTarget<TSource, TTarget>(this, source, context)
                    : Functions.ConvertEnumSourceToEnumTarget<TSource, TTarget>(this, source, context);
                return true;
            }

            if (isTargetTypeEnum)
            {
                target = Functions.ConvertSourceToEnumTarget<TSource, TTarget>(this, source, context);
                return true;
            }

            target = default(TTarget);
            return false;
        }

        private bool TryConvertForNullable<TSource, TTarget>(TSource source, TypeConverterContext context, out TTarget target)
        {
            var sourceType = typeof(TSource);
            var isSourceTypeNullable = sourceType.IsNullableType();

            var targetType = typeof(TTarget);
            var isTargetTypeNullable = targetType.IsNullableType();

            if (isSourceTypeNullable)
            {
                target = !isTargetTypeNullable
                    ? Functions.ConvertNullableSourceToTarget<TSource, TTarget>(this, source, context)
                    : Functions.ConvertNullableSourceToNullableTarget<TSource, TTarget>(this, source, context);
                return true;
            }

            if (isTargetTypeNullable)
            {
                target = Functions.ConvertSourceToNullableTarget<TSource, TTarget>(this, source, context);
                return true;
            }

            target = default(TTarget);
            return false;
        }
        #endregion

        #region TypeConverterDefinition Methods
        private void AddDefaultDefinitions()
        {
            foreach (var definition in DefaultDefinitions)
            {
                this.AddTypeConverterDefinition(definition);
            }
        }

        private void AddTypeConverterDefinition(ITypeConverterDefinition definition)
        {
            Contract.Requires(definition != null);

            this.TypeConverterDefinitions = this.TypeConverterDefinitions ?? new Dictionary<Tuple<Type, Type>, ITypeConverterDefinition>();

            var key = CreateTypeConverterDefinitionKey(definition);
            this.TypeConverterDefinitions.Add(key, definition);
        }

        private static Tuple<Type, Type> CreateTypeConverterDefinitionKey(ITypeConverterDefinition definition)
        {
            Contract.Requires(definition != null);

            var sourceType = definition.SourceType;
            var targetType = definition.TargetType;

            var key = CreateTypeConverterDefinitionKey(sourceType, targetType);
            return key;
        }

        private static Tuple<Type, Type> CreateTypeConverterDefinitionKey(Type sourceType, Type targetType)
        {
            Contract.Requires(sourceType != null);
            Contract.Requires(targetType != null);

            var key = new Tuple<Type, Type>(sourceType, targetType);
            return key;
        }

        private bool TryGetTypeConverterDefinition<TSource, TTarget>(out ITypeConverterDefinition<TSource, TTarget> definition)
        {
            definition = null;

            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);

            var key = CreateTypeConverterDefinitionKey(sourceType, targetType);
            ITypeConverterDefinition value;
            if (!this.TypeConverterDefinitions.TryGetValue(key, out value))
                return false;

            definition = (ITypeConverterDefinition<TSource, TTarget>)value;
            return true;
        }
        #endregion

        #region TypeConverterDefinitionFunc Implementation Methods
        private static string ConvertDateTimeToString(DateTime dateTime, TypeConverterContext context)
        {
            return String.IsNullOrWhiteSpace(context.SafeGetFormat())
                ? dateTime.ToString("O")
                : dateTime.ToString(context.SafeGetFormat(), context.SafeGetFormatProvider());
        }

        private static string ConvertDateTimeOffsetToString(DateTimeOffset dateTimeOffset, TypeConverterContext context)
        {
            return String.IsNullOrWhiteSpace(context.SafeGetFormat())
                ? dateTimeOffset.ToString("O")
                : dateTimeOffset.ToString(context.SafeGetFormat(), context.SafeGetFormatProvider());
        }

        private static string ConvertGuidToString(Guid guid, TypeConverterContext context)
        {
            return String.IsNullOrWhiteSpace(context.SafeGetFormat())
                ? guid.ToString("D")
                : guid.ToString(context.SafeGetFormat());
        }

        private static DateTime ConvertStringToDateTime(string str, TypeConverterContext context)
        {
            if (String.IsNullOrWhiteSpace(str))
                return default(DateTime);

            return String.IsNullOrWhiteSpace(context.SafeGetFormat())
                ? DateTime.Parse(str, context.SafeGetFormatProvider(), context.SafeGetDateTimeStyles())
                : DateTime.ParseExact(str, context.SafeGetFormat(), context.SafeGetFormatProvider(), context.SafeGetDateTimeStyles());
        }

        private static DateTimeOffset ConvertStringToDateTimeOffset(string str, TypeConverterContext context)
        {
            if (String.IsNullOrWhiteSpace(str))
                return default(DateTimeOffset);

            return String.IsNullOrWhiteSpace(context.SafeGetFormat())
                ? DateTimeOffset.Parse(str, context.SafeGetFormatProvider(), context.SafeGetDateTimeStyles())
                : DateTimeOffset.ParseExact(str, context.SafeGetFormat(), context.SafeGetFormatProvider(), context.SafeGetDateTimeStyles());
        }

        private static Guid ConvertStringToGuid(string str, TypeConverterContext context)
        {
            if (String.IsNullOrWhiteSpace(str))
                return default(Guid);

            return String.IsNullOrWhiteSpace(context.SafeGetFormat())
                ? Guid.Parse(str)
                : Guid.ParseExact(str, context.SafeGetFormat());
        }

        private static TimeSpan ConvertStringToTimeSpan(string str, TypeConverterContext context)
        {
            if (String.IsNullOrWhiteSpace(str))
                return default(TimeSpan);

            return String.IsNullOrWhiteSpace(context.SafeGetFormat())
                ? TimeSpan.Parse(str, context.SafeGetFormatProvider())
                : TimeSpan.ParseExact(str, context.SafeGetFormat(), context.SafeGetFormatProvider());
        }

        private static string ConvertTimeSpanToString(TimeSpan timeSpan, TypeConverterContext context)
        {
            return String.IsNullOrWhiteSpace(context.SafeGetFormat())
                ? timeSpan.ToString("c")
                : timeSpan.ToString(context.SafeGetFormat(), context.SafeGetFormatProvider());
        }
        #endregion

        #region Utility Methods
        private static Type GetValidateType<T>()
        {
            var type = typeof(T);
            if (type.IsNullableType())
                type = Nullable.GetUnderlyingType(type);
            if (type.IsEnum())
                type = Enum.GetUnderlyingType(type);
            return type;
        }

        // ReSharper disable UnusedMember.Local
        // ReSharper disable UnusedParameter.Local
        private static TEnum ParseEnum<TEnum>(string value, TypeConverterContext context)
        // ReSharper restore UnusedParameter.Local
        // ReSharper restore UnusedMember.Local
            where TEnum : struct
        {
            TEnum enumeration;
            if (Enum.TryParse(value, true, out enumeration))
            {
                return enumeration;
            }

            var typeConverterException = TypeConverterException.Create<string, TEnum>(value);
            throw typeConverterException;
        }

        // ReSharper disable UnusedMember.Local
        // ReSharper disable UnusedParameter.Local
        private void ValidateConvert<TSource, TTarget>(TSource source, TypeConverterContext context)
        // ReSharper restore UnusedParameter.Local
        // ReSharper restore UnusedMember.Local
        {
            if (typeof(TSource) == typeof(TTarget))
                return;

            var sourceType = GetValidateType<TSource>();
            var targetType = GetValidateType<TTarget>();

            var key = CreateTypeConverterDefinitionKey(sourceType, targetType);
            if (this.TypeConverterDefinitions.ContainsKey(key))
                return;

            var isTargetTypeAssignableFromSourceType = targetType.IsAssignableFrom(sourceType);
            if (isTargetTypeAssignableFromSourceType)
                return;

            var typeConverterException = TypeConverterException.Create<TSource, TTarget>(source);
            throw typeConverterException;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly ITypeConverterDefinition[] DefaultDefinitions =
            {
                // Simple Types /////////////////////////////////////////////

                // BoolToXXX
                new TypeConverterDefinitionFunc<bool, bool>((s, c) => s),
                new TypeConverterDefinitionFunc<bool, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<bool, char>((s, c) => s ? (char)1 : (char)0),
                new TypeConverterDefinitionFunc<bool, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<bool, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<bool, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<bool, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<bool, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<bool, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<bool, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<bool, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<bool, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<bool, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<bool, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // ByteToXXX
                new TypeConverterDefinitionFunc<byte, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<byte, byte>((s, c) => s),
                new TypeConverterDefinitionFunc<byte, char>((s, c) => System.Convert.ToChar(s)),
                new TypeConverterDefinitionFunc<byte, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<byte, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<byte, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<byte, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<byte, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<byte, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<byte, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<byte, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<byte, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<byte, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<byte, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // ByteArrayToXXX
                new TypeConverterDefinitionFunc<byte[], byte[]>((s, c) => s),
                new TypeConverterDefinitionFunc<byte[], Guid>((s, c) => s != null ? new Guid(s) : default(Guid)),
                new TypeConverterDefinitionFunc<byte[], string>((s, c) => s != null ? System.Convert.ToBase64String(s) : null),

                // CharToXXX
                new TypeConverterDefinitionFunc<char, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<char, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<char, char>((s, c) => s),
                new TypeConverterDefinitionFunc<char, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<char, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<char, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<char, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<char, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<char, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<char, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<char, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<char, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<char, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<char, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // DateTimeToXXX
                new TypeConverterDefinitionFunc<DateTime, DateTime>((s, c) => s),
                new TypeConverterDefinitionFunc<DateTime, DateTimeOffset>((s, c) => (DateTimeOffset)s),
                new TypeConverterDefinitionFunc<DateTime, string>(ConvertDateTimeToString),

                // DateTimeOffsetToXXX
                new TypeConverterDefinitionFunc<DateTimeOffset, DateTime>((s, c) => s.DateTime),
                new TypeConverterDefinitionFunc<DateTimeOffset, DateTimeOffset>((s, c) => s),
                new TypeConverterDefinitionFunc<DateTimeOffset, string>(ConvertDateTimeOffsetToString),

                // DecimalToXXX
                new TypeConverterDefinitionFunc<decimal, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<decimal, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<decimal, char>((s, c) => (char)s),
                new TypeConverterDefinitionFunc<decimal, decimal>((s, c) => s),
                new TypeConverterDefinitionFunc<decimal, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<decimal, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<decimal, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<decimal, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<decimal, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<decimal, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<decimal, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<decimal, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<decimal, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<decimal, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // DoubleToXXX
                new TypeConverterDefinitionFunc<double, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<double, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<double, char>((s, c) => (char)s),
                new TypeConverterDefinitionFunc<double, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<double, double>((s, c) => s),
                new TypeConverterDefinitionFunc<double, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<double, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<double, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<double, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<double, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<double, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<double, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<double, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<double, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // FloatToXXX
                new TypeConverterDefinitionFunc<float, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<float, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<float, char>((s, c) => (char)s),
                new TypeConverterDefinitionFunc<float, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<float, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<float, float>((s, c) => s),
                new TypeConverterDefinitionFunc<float, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<float, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<float, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<float, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<float, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<float, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<float, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<float, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // GuidToXXX
                new TypeConverterDefinitionFunc<Guid, byte[]>((s, c) => s.ToByteArray()),
                new TypeConverterDefinitionFunc<Guid, Guid>((s, c) => s),
                new TypeConverterDefinitionFunc<Guid, string>(ConvertGuidToString),

                // IntToXXX
                new TypeConverterDefinitionFunc<int, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<int, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<int, char>((s, c) => System.Convert.ToChar(s)),
                new TypeConverterDefinitionFunc<int, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<int, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<int, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<int, int>((s, c) => s),
                new TypeConverterDefinitionFunc<int, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<int, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<int, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<int, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<int, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<int, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<int, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // LongToXXX
                new TypeConverterDefinitionFunc<long, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<long, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<long, char>((s, c) => System.Convert.ToChar(s)),
                new TypeConverterDefinitionFunc<long, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<long, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<long, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<long, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<long, long>((s, c) => s),
                new TypeConverterDefinitionFunc<long, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<long, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<long, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<long, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<long, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<long, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // SByteToXXX
                new TypeConverterDefinitionFunc<sbyte, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<sbyte, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<sbyte, char>((s, c) => System.Convert.ToChar(s)),
                new TypeConverterDefinitionFunc<sbyte, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<sbyte, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<sbyte, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<sbyte, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<sbyte, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<sbyte, sbyte>((s, c) => s),
                new TypeConverterDefinitionFunc<sbyte, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<sbyte, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<sbyte, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<sbyte, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<sbyte, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // ShortToXXX
                new TypeConverterDefinitionFunc<short, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<short, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<short, char>((s, c) => System.Convert.ToChar(s)),
                new TypeConverterDefinitionFunc<short, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<short, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<short, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<short, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<short, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<short, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<short, short>((s, c) => s),
                new TypeConverterDefinitionFunc<short, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<short, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<short, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<short, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // StringToXXX
                new TypeConverterDefinitionFunc<string, bool>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToBoolean(s, c.SafeGetFormatProvider()) : default(bool)),
                new TypeConverterDefinitionFunc<string, byte>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToByte(s, c.SafeGetFormatProvider()) : default(byte)),
                new TypeConverterDefinitionFunc<string, byte[]>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.FromBase64String(s) : default(byte[])),
                new TypeConverterDefinitionFunc<string, char>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToChar(s, c.SafeGetFormatProvider()) : default(char)),
                new TypeConverterDefinitionFunc<string, DateTime>(ConvertStringToDateTime),
                new TypeConverterDefinitionFunc<string, DateTimeOffset>(ConvertStringToDateTimeOffset),
                new TypeConverterDefinitionFunc<string, decimal>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToDecimal(s, c.SafeGetFormatProvider()) : default(decimal)),
                new TypeConverterDefinitionFunc<string, double>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToDouble(s, c.SafeGetFormatProvider()) : default(double)),
                new TypeConverterDefinitionFunc<string, float>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToSingle(s, c.SafeGetFormatProvider()) : default(float)),
                new TypeConverterDefinitionFunc<string, Guid>(ConvertStringToGuid),
                new TypeConverterDefinitionFunc<string, int>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToInt32(s, c.SafeGetFormatProvider()) : default(int)),
                new TypeConverterDefinitionFunc<string, long>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToInt64(s, c.SafeGetFormatProvider()) : default(long)),
                new TypeConverterDefinitionFunc<string, sbyte>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToSByte(s, c.SafeGetFormatProvider()) : default(sbyte)),
                new TypeConverterDefinitionFunc<string, short>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToInt16(s, c.SafeGetFormatProvider()) : default(short)),
                new TypeConverterDefinitionFunc<string, string>((s, c) => s),
                new TypeConverterDefinitionFunc<string, TimeSpan>(ConvertStringToTimeSpan),
                new TypeConverterDefinitionFunc<string, Type>((s, c) => !String.IsNullOrWhiteSpace(s) ? Type.GetType(s, true) : default(Type)),
                new TypeConverterDefinitionFunc<string, uint>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToUInt32(s, c.SafeGetFormatProvider()) : default(uint)),
                new TypeConverterDefinitionFunc<string, ulong>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToUInt64(s, c.SafeGetFormatProvider()) : default(ulong)),
                new TypeConverterDefinitionFunc<string, Uri>((s, c) => !String.IsNullOrWhiteSpace(s) ? new Uri(s, UriKind.RelativeOrAbsolute) : default(Uri)),
                new TypeConverterDefinitionFunc<string, ushort>((s, c) => !String.IsNullOrWhiteSpace(s) ? System.Convert.ToUInt16(s, c.SafeGetFormatProvider()) : default(ushort)),

                // TimeSpanToXXX
                new TypeConverterDefinitionFunc<TimeSpan, string>(ConvertTimeSpanToString),
                new TypeConverterDefinitionFunc<TimeSpan, TimeSpan>((s, c) => s),

                // TypeToXXX
                new TypeConverterDefinitionFunc<Type, string>((s, c) => s != null ? s.GetCompactQualifiedName() : null),
                new TypeConverterDefinitionFunc<Type, Type>((s, c) => s),

                // UIntToXXX
                new TypeConverterDefinitionFunc<uint, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<uint, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<uint, char>((s, c) => System.Convert.ToChar(s)),
                new TypeConverterDefinitionFunc<uint, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<uint, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<uint, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<uint, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<uint, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<uint, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<uint, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<uint, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<uint, uint>((s, c) => s),
                new TypeConverterDefinitionFunc<uint, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<uint, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // ULongToXXX
                new TypeConverterDefinitionFunc<ulong, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<ulong, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<ulong, char>((s, c) => System.Convert.ToChar(s)),
                new TypeConverterDefinitionFunc<ulong, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<ulong, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<ulong, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<ulong, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<ulong, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<ulong, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<ulong, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<ulong, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<ulong, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<ulong, ulong>((s, c) => s),
                new TypeConverterDefinitionFunc<ulong, ushort>((s, c) => System.Convert.ToUInt16(s)),

                // UriToXXX
                new TypeConverterDefinitionFunc<Uri, string>((s, c) => s != null ? s.ToString() : null),
                new TypeConverterDefinitionFunc<Uri, Uri>((s, c) => s),

                // UShortToXXX
                new TypeConverterDefinitionFunc<ushort, bool>((s, c) => System.Convert.ToBoolean(s)),
                new TypeConverterDefinitionFunc<ushort, byte>((s, c) => System.Convert.ToByte(s)),
                new TypeConverterDefinitionFunc<ushort, char>((s, c) => System.Convert.ToChar(s)),
                new TypeConverterDefinitionFunc<ushort, decimal>((s, c) => System.Convert.ToDecimal(s)),
                new TypeConverterDefinitionFunc<ushort, double>((s, c) => System.Convert.ToDouble(s)),
                new TypeConverterDefinitionFunc<ushort, float>((s, c) => System.Convert.ToSingle(s)),
                new TypeConverterDefinitionFunc<ushort, int>((s, c) => System.Convert.ToInt32(s)),
                new TypeConverterDefinitionFunc<ushort, long>((s, c) => System.Convert.ToInt64(s)),
                new TypeConverterDefinitionFunc<ushort, sbyte>((s, c) => System.Convert.ToSByte(s)),
                new TypeConverterDefinitionFunc<ushort, short>((s, c) => System.Convert.ToInt16(s)),
                new TypeConverterDefinitionFunc<ushort, string>((s, c) => System.Convert.ToString(s, c.SafeGetFormatProvider())),
                new TypeConverterDefinitionFunc<ushort, uint>((s, c) => System.Convert.ToUInt32(s)),
                new TypeConverterDefinitionFunc<ushort, ulong>((s, c) => System.Convert.ToUInt64(s)),
                new TypeConverterDefinitionFunc<ushort, ushort>((s, c) => s)
            };

        private static readonly MethodInfo ConvertMethodInfoOpen = typeof(TypeConverter)
            .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
            .Single(x => x.Name == "Convert");

        private static readonly MethodInfo StringIsNullOrWhiteSpaceMethodInfo = typeof(String)
            .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static)
            .Single(x => x.Name == "IsNullOrWhiteSpace");

        private static readonly MethodInfo ParseEnumMethodInfoOpen = typeof(TypeConverter)
            .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Static)
            .Single(x => x.Name == "ParseEnum");

        private static readonly MethodInfo ValidateConvertMethodInfoOpen = typeof(TypeConverter)
            .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance)
            .Single(x => x.Name == "ValidateConvert");
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        /// <summary>
        /// Returns dynamically built lambdas (cached) to perform various
        /// conversion functions.
        /// </summary>
        /// <notes>
        /// Design goals include:
        /// - Avoid unnecessary boxing/unboxing for value types.
        /// - Use reflection only once upon startup only if needed.
        /// </notes>
        private static class Functions
        {
            // PUBLIC METHODS ///////////////////////////////////////////////
            #region Methods
            public static TTarget CastSourceToTarget<TSource, TTarget>(TSource source)
            {
                var target = Cache<TSource, TTarget>.CastSourceToTarget(source);
                return target;
            }

            public static TTarget ConvertSourceToEnumTarget<TSource, TTarget>(TypeConverter typeConverter, TSource source, TypeConverterContext context)
            {
                Contract.Requires(typeConverter != null);

                var target = Cache<TSource, TTarget>.ConvertSourceToEnumTarget(typeConverter, source, context);
                return target;
            }

            public static TTarget ConvertEnumSourceToTarget<TSource, TTarget>(TypeConverter typeConverter, TSource source, TypeConverterContext context)
            {
                Contract.Requires(typeConverter != null);

                var target = Cache<TSource, TTarget>.ConvertEnumSourceToTarget(typeConverter, source, context);
                return target;
            }

            public static TTarget ConvertEnumSourceToEnumTarget<TSource, TTarget>(TypeConverter typeConverter, TSource source, TypeConverterContext context)
            {
                Contract.Requires(typeConverter != null);

                var target = Cache<TSource, TTarget>.ConvertEnumSourceToEnumTarget(typeConverter, source, context);
                return target;
            }

            public static TTarget ConvertSourceToNullableTarget<TSource, TTarget>(TypeConverter typeConverter, TSource source, TypeConverterContext context)
            {
                Contract.Requires(typeConverter != null);

                var target = Cache<TSource, TTarget>.ConvertSourceToNullableTarget(typeConverter, source, context);
                return target;
            }

            public static TTarget ConvertNullableSourceToTarget<TSource, TTarget>(TypeConverter typeConverter, TSource source, TypeConverterContext context)
            {
                Contract.Requires(typeConverter != null);

                var target = Cache<TSource, TTarget>.ConvertNullableSourceToTarget(typeConverter, source, context);
                return target;
            }

            public static TTarget ConvertNullableSourceToNullableTarget<TSource, TTarget>(TypeConverter typeConverter, TSource source, TypeConverterContext context)
            {
                Contract.Requires(typeConverter != null);

                var target = Cache<TSource, TTarget>.ConvertNullableSourceToNullableTarget(typeConverter, source, context);
                return target;
            }
            #endregion

            // PRIVATE TYPES ////////////////////////////////////////////////
            #region Types
            private static class Cache<TSource, TTarget>
            {
                // PUBLIC PROPERTIES ////////////////////////////////////////////
                #region Properties
                public static Func<TSource, TTarget> CastSourceToTarget { get { return _castSourceToTarget.Value; } }

                public static Func<TypeConverter, TSource, TypeConverterContext, TTarget> ConvertSourceToEnumTarget { get { return _convertSourceToEnumTarget.Value; } }
                public static Func<TypeConverter, TSource, TypeConverterContext, TTarget> ConvertEnumSourceToTarget { get { return _convertEnumSourceToTarget.Value; } }
                public static Func<TypeConverter, TSource, TypeConverterContext, TTarget> ConvertEnumSourceToEnumTarget { get { return _convertEnumSourceToEnumTarget.Value; } }

                public static Func<TypeConverter, TSource, TypeConverterContext, TTarget> ConvertSourceToNullableTarget { get { return _convertSourceToNullableTarget.Value; } }
                public static Func<TypeConverter, TSource, TypeConverterContext, TTarget> ConvertNullableSourceToTarget { get { return _convertNullableSourceToTarget.Value; } }
                public static Func<TypeConverter, TSource, TypeConverterContext, TTarget> ConvertNullableSourceToNullableTarget { get { return _convertNullableSourceToNullableTarget.Value; } }
                #endregion

                // PRIVATE METHODS //////////////////////////////////////////////
                #region Methods
                private static Func<TSource, TTarget> CreateCastSourceToTarget()
                {
                    var sourceType = typeof(TSource);
                    var targetType = typeof(TTarget);

                    var sourceParameterExpression = Expression.Parameter(sourceType, "source");
                    var castSourceToTargetExpression = Expression.ConvertChecked(sourceParameterExpression, targetType);
                    var castSourceToTargetLambdaExpression = Expression
                        .Lambda<Func<TSource, TTarget>>(castSourceToTargetExpression, sourceParameterExpression);
                    var castSourceToTargetLambda = castSourceToTargetLambdaExpression.Compile();
                    return castSourceToTargetLambda;
                }

                private static Func<TypeConverter, TSource, TypeConverterContext, TTarget> CreateConvertSourceToEnumTarget()
                {
                    var converterType = typeof(TypeConverter);
                    var contextType = typeof(TypeConverterContext);
                    var sourceType = typeof(TSource);
                    var targetType = typeof(TTarget);
                    var targetUnderlyingType = Enum.GetUnderlyingType(targetType);

                    var converterParameterExpression = Expression.Parameter(converterType, "converter");
                    var sourceParameterExpression = Expression.Parameter(sourceType, "source");
                    var contextParameterExpression = Expression.Parameter(contextType, "context");

                    // Special case of source string
                    if (sourceType.IsString())
                    {
                        var callStringIsNullOrWhitespaceExpression = Expression.Call(
                            StringIsNullOrWhiteSpaceMethodInfo,
                            sourceParameterExpression);

                        var defaultTargetExpression = Expression.Default(targetType);

                        var converterParseEnumMethodClosed = ParseEnumMethodInfoOpen.MakeGenericMethod(targetType);

                        var callConverterParseEnumExpression = Expression.Call(
                            converterParseEnumMethodClosed,
                            sourceParameterExpression,
                            contextParameterExpression);

                        var conditionExpression = Expression.Condition(
                            callStringIsNullOrWhitespaceExpression,
                            defaultTargetExpression,
                            callConverterParseEnumExpression);

                        var callConverterParseEnumLambdaExpression = Expression
                            .Lambda<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(
                                conditionExpression,
                                converterParameterExpression,
                                sourceParameterExpression,
                                contextParameterExpression);

                        var callConverterParseEnumLambda = callConverterParseEnumLambdaExpression.Compile();
                        return callConverterParseEnumLambda;
                    }

                    var convertMethodInfoClosed = ConvertMethodInfoOpen.MakeGenericMethod(sourceType, targetUnderlyingType);

                    var callConvertMethodExpression = Expression.Call(
                        converterParameterExpression,
                        convertMethodInfoClosed,
                        sourceParameterExpression,
                        contextParameterExpression);

                    var castConvertMethodExpression = Expression.ConvertChecked(callConvertMethodExpression, targetType);

                    var convertSourceToNullableTargetLambdaExpression = Expression
                        .Lambda<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(
                            castConvertMethodExpression,
                            converterParameterExpression,
                            sourceParameterExpression,
                            contextParameterExpression);

                    var convertSourceToNullableTargetLambda = convertSourceToNullableTargetLambdaExpression.Compile();
                    return convertSourceToNullableTargetLambda;
                }

                private static Func<TypeConverter, TSource, TypeConverterContext, TTarget> CreateConvertEnumSourceToTarget()
                {
                    var converterType = typeof(TypeConverter);
                    var contextType = typeof(TypeConverterContext);
                    var sourceType = typeof(TSource);
                    var sourceUnderlyingType = Enum.GetUnderlyingType(sourceType);
                    var targetType = typeof(TTarget);

                    var converterParameterExpression = Expression.Parameter(converterType, "converter");
                    var sourceParameterExpression = Expression.Parameter(sourceType, "source");
                    var contextParameterExpression = Expression.Parameter(contextType, "context");

                    // Special case of target string.
                    if (targetType.IsString())
                    {
                        var enumType = typeof(Enum);
                        var enumToStringWithFormatMethod = enumType
                            .GetMethod("ToString", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, typeof(string));

                        var typeConverterContextExtensionsType = typeof(TypeConverterContextExtensions);
                        var contextSafeGetFormatMethod = typeConverterContextExtensionsType.GetMethod("SafeGetFormat", contextType);

                        var callContextSafeGetFormatExpression = Expression.Call(contextSafeGetFormatMethod, contextParameterExpression);

                        // ReSharper disable PossiblyMistakenUseOfParamsMethod
                        var callEnumToStringExpression = Expression.Call(sourceParameterExpression, enumToStringWithFormatMethod, callContextSafeGetFormatExpression);
                        // ReSharper restore PossiblyMistakenUseOfParamsMethod

                        var callEnumToStringWithFormatLambdaExpression = Expression
                            .Lambda<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(
                                callEnumToStringExpression,
                                converterParameterExpression,
                                sourceParameterExpression,
                                contextParameterExpression);

                        var callEnumToStringWithFormatLambda = callEnumToStringWithFormatLambdaExpression.Compile();
                        return callEnumToStringWithFormatLambda;
                    }

                    var convertMethodInfoClosed = ConvertMethodInfoOpen.MakeGenericMethod(sourceUnderlyingType, targetType);

                    var castSourceToUnderlyingTypeExpression = Expression.ConvertChecked(sourceParameterExpression, sourceUnderlyingType);

                    var callConvertMethodExpression = Expression.Call(
                        converterParameterExpression,
                        convertMethodInfoClosed,
                        castSourceToUnderlyingTypeExpression,
                        contextParameterExpression);

                    var castConvertMethodExpression = Expression.ConvertChecked(callConvertMethodExpression, targetType);

                    var convertEnumSourceToTargetLambdaExpression = Expression
                        .Lambda<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(
                            castConvertMethodExpression,
                            converterParameterExpression,
                            sourceParameterExpression,
                            contextParameterExpression);

                    var convertEnumSourceToTargetLambda = convertEnumSourceToTargetLambdaExpression.Compile();
                    return convertEnumSourceToTargetLambda;
                }

                private static Func<TypeConverter, TSource, TypeConverterContext, TTarget> CreateConvertEnumSourceToEnumTarget()
                {
                    var converterType = typeof(TypeConverter);
                    var contextType = typeof(TypeConverterContext);
                    var sourceType = typeof(TSource);
                    var sourceUnderlyingType = Enum.GetUnderlyingType(sourceType);
                    var targetType = typeof(TTarget);
                    var targetUnderlyingType = Enum.GetUnderlyingType(targetType);

                    var converterParameterExpression = Expression.Parameter(converterType, "converter");
                    var sourceParameterExpression = Expression.Parameter(sourceType, "source");
                    var contextParameterExpression = Expression.Parameter(contextType, "context");

                    var convertMethodInfoClosed = ConvertMethodInfoOpen.MakeGenericMethod(sourceUnderlyingType, targetUnderlyingType);

                    var castSourceToUnderlyingTypeExpression = Expression.ConvertChecked(sourceParameterExpression, sourceUnderlyingType);

                    var callConvertMethodExpression = Expression.Call(
                        converterParameterExpression,
                        convertMethodInfoClosed,
                        castSourceToUnderlyingTypeExpression,
                        contextParameterExpression);

                    var castConvertMethodExpression = Expression.ConvertChecked(callConvertMethodExpression, targetType);

                    var convertEnumSourceToEnumTargetLambdaExpression = Expression
                        .Lambda<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(
                            castConvertMethodExpression,
                            converterParameterExpression,
                            sourceParameterExpression,
                            contextParameterExpression);

                    var convertEnumSourceToEnumTargetLambda = convertEnumSourceToEnumTargetLambdaExpression.Compile();
                    return convertEnumSourceToEnumTargetLambda;
                }

                private static Func<TypeConverter, TSource, TypeConverterContext, TTarget> CreateConvertSourceToNullableTarget()
                {
                    var converterType = typeof(TypeConverter);
                    var contextType = typeof(TypeConverterContext);
                    var sourceType = typeof(TSource);
                    var targetType = typeof(TTarget);
                    var targetUnderlyingType = Nullable.GetUnderlyingType(targetType);

                    var converterParameterExpression = Expression.Parameter(converterType, "converter");
                    var sourceParameterExpression = Expression.Parameter(sourceType, "source");
                    var contextParameterExpression = Expression.Parameter(contextType, "context");

                    var convertMethodInfoClosed = ConvertMethodInfoOpen.MakeGenericMethod(sourceType, targetUnderlyingType);

                    var callConvertMethodExpression = Expression.Call(
                        converterParameterExpression,
                        convertMethodInfoClosed,
                        sourceParameterExpression,
                        contextParameterExpression);

                    var castConvertMethodExpression = Expression.ConvertChecked(callConvertMethodExpression, targetType);

                    var isValueType = sourceType.IsValueType();
                    if (isValueType)
                    {
                        var convertValueToNullableTargetLambdaExpression = Expression
                            .Lambda<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(
                                castConvertMethodExpression,
                                converterParameterExpression,
                                sourceParameterExpression,
                                contextParameterExpression);

                        var convertValueToNullableTargetLambda = convertValueToNullableTargetLambdaExpression.Compile();
                        return convertValueToNullableTargetLambda;
                    }

                    // If source is null, check if conversion is valid before setting target to default(T).
                    var validateConverttMethodInfoClosed = ValidateConvertMethodInfoOpen.MakeGenericMethod(sourceType, targetType);

                    var nullConstantExpression = Expression.Constant(null);
                    var defaultTargetExpression = Expression.Default(targetType);
                    var returnTarget = Expression.Label(targetType);
                    var blockExpression = Expression.Block(
                        targetType,
                        Expression.IfThenElse(
                            // Condition: if (source == null)
                            Expression.ReferenceEqual(sourceParameterExpression, nullConstantExpression),
                            // True Condition: Call converter.ValidateConvert(source, context) followed by return default(T)
                            Expression.Block(
                                Expression.Call(converterParameterExpression,
                                                validateConverttMethodInfoClosed,
                                                sourceParameterExpression,
                                                contextParameterExpression),
                                Expression.Return(returnTarget, defaultTargetExpression)),
                            // False Condition: return Call converter.Convert(source, context) for target underlying type
                            Expression.Return(returnTarget, castConvertMethodExpression)),
                        Expression.Label(returnTarget, defaultTargetExpression));

                    var convertObjectToNullableTargetLambdaExpression = Expression
                        .Lambda<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(
                            blockExpression,
                            converterParameterExpression,
                            sourceParameterExpression,
                            contextParameterExpression);

                    var convertObjectToNullableTargetLambda = convertObjectToNullableTargetLambdaExpression.Compile();
                    return convertObjectToNullableTargetLambda;
                }

                private static Func<TypeConverter, TSource, TypeConverterContext, TTarget> CreateConvertNullableSourceToTarget()
                {
                    var converterType = typeof(TypeConverter);
                    var contextType = typeof(TypeConverterContext);
                    var sourceType = typeof(TSource);
                    var sourceUnderlyingType = Nullable.GetUnderlyingType(sourceType);
                    var targetType = typeof(TTarget);

                    var converterParameterExpression = Expression.Parameter(converterType, "converter");
                    var sourceParameterExpression = Expression.Parameter(sourceType, "source");
                    var contextParameterExpression = Expression.Parameter(contextType, "context");

                    var convertMethodInfoClosed = ConvertMethodInfoOpen.MakeGenericMethod(sourceUnderlyingType, targetType);
                    var sourceHasValuePropertyInfo = sourceType.GetProperty("HasValue", BindingFlags.Public | BindingFlags.Instance);
                    var sourceValuePropertyInfo = sourceType.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);

                    var sourceParameterValuePropertyExpression = Expression.Property(sourceParameterExpression, sourceValuePropertyInfo);

                    var callConvertMethodExpression = Expression.Call(
                        converterParameterExpression,
                        convertMethodInfoClosed,
                        sourceParameterValuePropertyExpression,
                        contextParameterExpression);

                    var castConvertMethodExpression = Expression.ConvertChecked(callConvertMethodExpression, targetType);

                    // If source is null, check if conversion is valid before setting target to default(T).
                    var validateConvertMethodInfoClosed = ValidateConvertMethodInfoOpen.MakeGenericMethod(sourceType, targetType);

                    var defaultTargetExpression = Expression.Default(targetType);
                    var returnTarget = Expression.Label(targetType);
                    var blockExpression = Expression.Block(
                        targetType,
                        Expression.IfThenElse(
                            // Condition: if (source.HasValue)
                            Expression.Property(sourceParameterExpression, sourceHasValuePropertyInfo),
                            // True Condition: return Call converter.Convert(source, context) for target underlying type
                            Expression.Return(returnTarget, castConvertMethodExpression),
                            // False Condition: Call converter.ValidateConvert(source, context) followed by return default(T)
                            Expression.Block(
                                Expression.Call(converterParameterExpression,
                                                validateConvertMethodInfoClosed,
                                                sourceParameterExpression,
                                                contextParameterExpression),
                                Expression.Return(returnTarget, defaultTargetExpression))),
                        Expression.Label(returnTarget, defaultTargetExpression));

                    var convertNullableSourceToTargetLambdaExpression = Expression
                        .Lambda<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(
                            blockExpression,
                            converterParameterExpression,
                            sourceParameterExpression,
                            contextParameterExpression);

                    var convertNullableSourceToTargetLambda = convertNullableSourceToTargetLambdaExpression.Compile();
                    return convertNullableSourceToTargetLambda;
                }

                private static Func<TypeConverter, TSource, TypeConverterContext, TTarget> CreateConvertNullableSourceToNullableTarget()
                {
                    var converterType = typeof(TypeConverter);
                    var contextType = typeof(TypeConverterContext);
                    var sourceType = typeof(TSource);
                    var sourceUnderlyingType = Nullable.GetUnderlyingType(sourceType);
                    var targetType = typeof(TTarget);
                    var targetUnderlyingType = Nullable.GetUnderlyingType(targetType);

                    var converterParameterExpression = Expression.Parameter(converterType, "converter");
                    var sourceParameterExpression = Expression.Parameter(sourceType, "source");
                    var contextParameterExpression = Expression.Parameter(contextType, "context");

                    var convertMethodInfoClosed = ConvertMethodInfoOpen.MakeGenericMethod(sourceUnderlyingType, targetUnderlyingType);
                    var sourceHasValuePropertyInfo = sourceType.GetProperty("HasValue", BindingFlags.Public | BindingFlags.Instance);
                    var sourceValuePropertyInfo = sourceType.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);

                    var sourceParameterValuePropertyExpression = Expression.Property(sourceParameterExpression, sourceValuePropertyInfo);

                    var callConvertMethodExpression = Expression.Call(
                        converterParameterExpression,
                        convertMethodInfoClosed,
                        sourceParameterValuePropertyExpression,
                        contextParameterExpression);

                    var castConvertMethodExpression = Expression.ConvertChecked(callConvertMethodExpression, targetType);

                    // If source is null, check if conversion is valid before setting target to default(T).
                    var validateConvertMethodInfoClosed = ValidateConvertMethodInfoOpen.MakeGenericMethod(sourceType, targetType);

                    var defaultTargetExpression = Expression.Default(targetType);
                    var returnTarget = Expression.Label(targetType);
                    var blockExpression = Expression.Block(
                        targetType,
                        Expression.IfThenElse(
                            // Condition: if (source.HasValue)
                            Expression.Property(sourceParameterExpression, sourceHasValuePropertyInfo),
                            // True Condition: return Call converter.Convert(source, context) for target underlying type
                            Expression.Return(returnTarget, castConvertMethodExpression),
                            // False Condition: Call converter.ValidateConvert(source, context) followed by return default(T)
                            Expression.Block(
                                Expression.Call(converterParameterExpression,
                                                validateConvertMethodInfoClosed,
                                                sourceParameterExpression,
                                                contextParameterExpression),
                                Expression.Return(returnTarget, defaultTargetExpression))),
                        Expression.Label(returnTarget, defaultTargetExpression));

                    var convertNullableSourceToNullableTargetLambdaExpression = Expression
                        .Lambda<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(
                            blockExpression,
                            converterParameterExpression,
                            sourceParameterExpression,
                            contextParameterExpression);

                    var convertNullableSourceToNullableTargetLambda = convertNullableSourceToNullableTargetLambdaExpression.Compile();
                    return convertNullableSourceToNullableTargetLambda;
                }
                #endregion

                // PRIVATE FIELDS ///////////////////////////////////////////////
                #region Private Fields
                // ReSharper disable InconsistentNaming
                private static readonly Lazy<Func<TSource, TTarget>> _castSourceToTarget = new Lazy<Func<TSource, TTarget>>(CreateCastSourceToTarget, LazyThreadSafetyMode.PublicationOnly);

                private static readonly Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>> _convertSourceToEnumTarget = new Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(CreateConvertSourceToEnumTarget, LazyThreadSafetyMode.PublicationOnly);
                private static readonly Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>> _convertEnumSourceToTarget = new Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(CreateConvertEnumSourceToTarget, LazyThreadSafetyMode.PublicationOnly);
                private static readonly Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>> _convertEnumSourceToEnumTarget = new Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(CreateConvertEnumSourceToEnumTarget, LazyThreadSafetyMode.PublicationOnly);

                private static readonly Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>> _convertSourceToNullableTarget = new Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(CreateConvertSourceToNullableTarget, LazyThreadSafetyMode.PublicationOnly);
                private static readonly Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>> _convertNullableSourceToTarget = new Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(CreateConvertNullableSourceToTarget, LazyThreadSafetyMode.PublicationOnly);
                private static readonly Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>> _convertNullableSourceToNullableTarget = new Lazy<Func<TypeConverter, TSource, TypeConverterContext, TTarget>>(CreateConvertNullableSourceToNullableTarget, LazyThreadSafetyMode.PublicationOnly);
                // ReSharper restore InconsistentNaming
                #endregion
            }
            #endregion
        }
        #endregion
    }
}
