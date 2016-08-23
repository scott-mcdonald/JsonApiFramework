// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace JsonApiFramework.Reflection
{
    public static class TypeConverter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Non-Generic Convert Methods
        public static object Convert(object sourceValue, Type targetType, IFormatProvider targetFormatProvider = null)
        {
            Contract.Requires(targetType != null);

            try
            {
                object targetValue;
                if (TryConvertInternal(sourceValue, targetType, out targetValue, targetFormatProvider))
                {
                    return targetValue;
                }

                throw new TypeConverterException(BuildTypeConverterMessage(sourceValue, targetType));
            }
            catch (Exception exception)
            {
                throw new TypeConverterException(BuildTypeConverterMessage(sourceValue, targetType), exception);
            }
        }

        public static bool TryConvert(object sourceValue, Type targetType, out object targetValue, IFormatProvider targetFormatProvider = null)
        {
            Contract.Requires(targetType != null);

            try
            {
                return TryConvertInternal(sourceValue, targetType, out targetValue, targetFormatProvider);
            }
            catch (Exception)
            {
                targetValue = null;
                return false;
            }
        }
        #endregion

        #region Generic Convert Methods
        public static T Convert<T>(object sourceValue, IFormatProvider targetFormatProvider = null)
        {
            var targetType = typeof(T);
            return (T)Convert(sourceValue, targetType, targetFormatProvider);
        }

        public static bool TryConvert<T>(object sourceValue, out T targetValue, IFormatProvider targetFormatProvider = null)
        {
            var targetType = typeof(T);
            object targetValueAsObject;
            if (TryConvert(sourceValue, targetType, out targetValueAsObject, targetFormatProvider))
            {
                targetValue = (T)targetValueAsObject;
                return true;
            }

            targetValue = default(T);
            return false;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string BuildTypeConverterMessage(object sourceValue, Type targetType)
        {
            Contract.Requires(targetType != null);

            var sourceTypeAsString = sourceValue != null
                ? sourceValue.GetType().Name
                : "null";
            var targetTypeAsString = targetType.Name;

            return "Unable to convert from {0} to {1}".FormatWith(sourceTypeAsString, targetTypeAsString);
        }

        private static bool IsSystemConvertChangeTypeCapableOrIsEnum(Type type)
        {
            Contract.Requires(type != null);

            var isAvailableSystemConvertChangeTypes = AvailableSystemConvertChangeTypes.Contains(type);
            var isEnum = type.IsEnum();

            return isAvailableSystemConvertChangeTypes || isEnum;
        }

        private static bool TryConvertInternal(object sourceValue, Type targetType, out object targetValue, IFormatProvider targetFormatProvider)
        {
            Contract.Requires(targetType != null);

            targetValue = null;
            targetFormatProvider = targetFormatProvider ?? CultureInfo.InvariantCulture;

            // Handle case when source object is null.
            if (sourceValue == null)
            {
                // Handle case when target is a Nullable<T> type.
                if (targetType.IsNullableType())
                {
                    // Target is a Nullable<T> type which allows conversion to null.
                    return true;
                }

                // Handle case when target is a value type.
                if (targetType.IsValueType())
                {
                    // Target type can not be a value type as value types can not be null.
                    throw new InvalidOperationException("Unable to convert value type [name={0}] to a null object.".FormatWith(targetType.Name));
                }

                // Target must be a reference type which allows conversion to null.
                return true;
            }

            // Handle case when target and source are the exact same type.
            var sourceType = sourceValue.GetType();
            if (sourceType == targetType)
            {
                targetValue = sourceValue;
                return true;
            }

            // Handle case when target is a Nullable<T> type. Special CLR
            // rules for Nullable<T> and boxing:
            // 1. If source is null, then no boxing occurs and null is returned.
            // 2. If source is not null, then a boxed value type is returned; not a boxed Nullable<T> type.
            // Therefore the source is null case is handled previously, the
            if (targetType.IsNullableType())
            {
                targetType = Nullable.GetUnderlyingType(targetType);

                // Handle case when source and the underlying nullable target
                // are the exact same type.
                if (sourceType == targetType)
                {
                    targetValue = sourceValue;
                    return true;
                }
            }

            // Handle case when target is assignable from source.
            if (targetType.IsAssignableFrom(sourceType))
            {
                targetValue = sourceValue;
                return true;
            }

            // Handle case when source and target types are convertible with
            // Convert.ChangeType .NET system function or is an Enum.
            if (IsSystemConvertChangeTypeCapableOrIsEnum(sourceType) && IsSystemConvertChangeTypeCapableOrIsEnum(targetType))
            {
                // Handle special case when source type is a string or integer and converting to an enumeration.
                if (targetType.IsEnum())
                {
                    if (sourceType.IsStringType())
                    {
                        targetValue = Enum.Parse(targetType, (string)sourceValue, true);
                        return true;
                    }

                    if (sourceType.IsIntegralType())
                    {
                        targetValue = Enum.ToObject(targetType, sourceValue);
                        return true;
                    }
                }

                targetValue = System.Convert.ChangeType(sourceValue, targetType, targetFormatProvider);
                return true;
            }

            // Handle case string => Guid, Uri, DateTimeOffset, TimeSpan, byte[], Type
            if (HandleStringToSpecialCaseTypes(sourceValue, targetType, targetFormatProvider, ref targetValue))
                return true;

            // Handle case Guid, Uri, DateTimeOffset, TimeSpan, byte[], Type => string
            if (HandleSpecialCaseTypesToString(sourceValue, targetType, ref targetValue))
                return true;

            // Handle case DateTime => DateTimeOffset or DateTimeOffset => DateTime
            if (HandleSpecialCaseTypesOfDateTimeAndDateTimeOffset(sourceValue, targetType, ref targetValue))
                return true;

            // Handle case byte[] => Guid or Guid => byte[]
            if (HandleSpecialCaseTypesOfByteArrayAndGuid(sourceValue, targetType, ref targetValue))
                return true;

            // Handle case some derived Type => Type
            if (HandleSpecialCaseTypesOfTypeAndDerivedType(sourceValue, targetType, ref targetValue))
                return true;

            // Can not convert from source to target types.
            return false;
        }

        private static bool HandleStringToSpecialCaseTypes(object sourceValue, Type targetType, IFormatProvider targetFormatProvider, ref object targetValue)
        {
            var sourceValueAsString = sourceValue as string;
            if (sourceValueAsString == null)
                return false;

            if (targetType == typeof(Guid))
            {
                targetValue = new Guid(sourceValueAsString);
                return true;
            }

            if (targetType == typeof(Uri))
            {
                targetValue = new Uri(sourceValueAsString, UriKind.RelativeOrAbsolute);
                return true;
            }

            if (targetType == typeof(DateTime))
            {
                targetValue = DateTime.Parse(sourceValueAsString, targetFormatProvider, DateTimeStyles.RoundtripKind);
                return true;
            }

            if (targetType == typeof(DateTimeOffset))
            {
                targetValue = DateTimeOffset.Parse(sourceValueAsString, targetFormatProvider, DateTimeStyles.RoundtripKind);
                return true;
            }

            if (targetType == typeof(TimeSpan))
            {
                targetValue = TimeSpan.Parse(sourceValueAsString, targetFormatProvider);
                return true;
            }

            if (targetType == typeof(byte[]))
            {
                targetValue = System.Convert.FromBase64String(sourceValueAsString);
                return true;
            }

            if (typeof(Type).IsAssignableFrom(targetType))
            {
                targetValue = Type.GetType(sourceValueAsString, true);
                return true;
            }

            return false;
        }

        private static bool HandleSpecialCaseTypesToString(object sourceValue, Type targetType, ref object targetValue)
        {
            if (sourceValue is Guid && targetType == typeof(string))
            {
                targetValue = ((Guid)sourceValue).ToString("D");
                return true;
            }

            if (sourceValue is Uri && targetType == typeof(string))
            {
                targetValue = (sourceValue).ToString();
                return true;
            }

            if (sourceValue is DateTime && targetType == typeof(string))
            {
                targetValue = ((DateTime)sourceValue).ToString("O");
                return true;
            }

            if (sourceValue is DateTimeOffset && targetType == typeof(string))
            {
                targetValue = ((DateTimeOffset)sourceValue).ToString("O");
                return true;
            }

            if (sourceValue is TimeSpan && targetType == typeof(string))
            {
                targetValue = ((TimeSpan)sourceValue).ToString("c");
                return true;
            }

            if (sourceValue is byte[] && targetType == typeof(string))
            {
                targetValue = System.Convert.ToBase64String((byte[])sourceValue);
                return true;
            }

            if (sourceValue is Type && targetType == typeof(string))
            {
                targetValue = ((Type)sourceValue).GetCompactQualifiedName();
                return true;
            }

            return false;
        }

        private static bool HandleSpecialCaseTypesOfByteArrayAndGuid(object sourceValue, Type targetType, ref object targetValue)
        {
            // Handle case byte[] => Guid
            if (sourceValue is byte[] && targetType == typeof(Guid))
            {
                targetValue = new Guid((byte[])sourceValue);
                return true;
            }

            // Handle case Guid => byte []
            if (sourceValue is Guid && targetType == typeof(byte[]))
            {
                targetValue = ((Guid)sourceValue).ToByteArray();
                return true;
            }

            return false;
        }

        private static bool HandleSpecialCaseTypesOfDateTimeAndDateTimeOffset(object sourceValue, Type targetType, ref object targetValue)
        {
            // Handle case DateTime => DateTimeOffset
            if (sourceValue is DateTime && targetType == typeof(DateTimeOffset))
            {
                targetValue = new DateTimeOffset((DateTime)sourceValue);
                return true;
            }

            // Handle case DateTimeOffset => DateTime
            if (sourceValue is DateTimeOffset && targetType == typeof(DateTime))
            {
                targetValue = ((DateTimeOffset)sourceValue).DateTime;
                return true;
            }

            return false;
        }

        private static bool HandleSpecialCaseTypesOfTypeAndDerivedType(object sourceValue, Type targetType, ref object targetValue)
        {
            // Handle case some derived Type => Type
            if (sourceValue is Type && targetType == typeof(Type))
            {
                targetValue = sourceValue;
                return true;
            }

            return false;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly ISet<Type> AvailableSystemConvertChangeTypes = new HashSet<Type>
            {
                typeof(bool),
                typeof(byte),
                typeof(char),
                typeof(decimal),
                typeof(double),
                typeof(float),
                typeof(int),
                typeof(long),
                typeof(sbyte),
                typeof(short),
                typeof(string),
                typeof(uint),
                typeof(ulong),
                typeof(ushort)
            };
        #endregion
    }
}
