// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JetBrains.Annotations;

namespace JsonApiFramework
{
    /// <summary>
    /// Extension methods for the .NET String class.
    /// </summary>
    public static class StringExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Returns a value indicating whether the specified String object
        /// occurs within this string with full control how the strings are
        /// compared with the <c>StringComparison</c> enumeration.
        /// </summary>
        public static bool Contains(this string str, string value, StringComparison comparison)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(str) == false);

            return String.IsNullOrWhiteSpace(value) == false && str.IndexOf(value, comparison) >= 0;
        }

        /// <summary>
        /// Format a string object the same as the Format static method with
        /// optional arguments.
        /// </summary>
        [StringFormatMethod("format")]
        public static string FormatWith(this string format, params object[] args)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(format) == false);

            return String.Format(format, args);
        }

        /// <summary>
        /// Format a string object the same as the Format static method with
        /// a format provider and optional arguments.
        /// </summary>
        [StringFormatMethod("format")]
        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(format) == false);

            return String.Format(provider, format, args);
        }

        /// <summary>
        /// Parse a string object into an enumeration. Throws an exception
        /// if unable to parse string into the specified enumeration.
        /// </summary>
        public static TEnum ParseEnum<TEnum>(this string str, bool ignoreCase = false)
            where TEnum : struct
        {
            Contract.Requires(String.IsNullOrWhiteSpace(str) == false);

            return ignoreCase
                ? (TEnum)Enum.Parse(typeof(TEnum), str, true)
                : (TEnum)Enum.Parse(typeof(TEnum), str);
        }

        /// <summary>
        /// Try and parse a string object into an enumeration. Returns true
        /// if successful, false otherwise.
        /// </summary>
        public static bool TryParseEnum<TEnum>(this string str, out TEnum result, bool ignoreCase = false)
            where TEnum : struct
        {
            Contract.Requires(String.IsNullOrWhiteSpace(str) == false);

            return ignoreCase
                ? Enum.TryParse(str, true, out result)
                : Enum.TryParse(str, out result);
        }
        #endregion
    }
}
