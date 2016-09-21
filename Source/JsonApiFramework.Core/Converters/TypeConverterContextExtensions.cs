// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Globalization;

namespace JsonApiFramework.Converters
{
    /// <summary>
    /// Extension methods built from the <c>TypeConverterContext</c> class.
    /// </summary>
    public static class TypeConverterContextExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static string SafeGetFormat(this TypeConverterContext context)
        { return context != null ? context.Format : null; }

        public static IFormatProvider SafeGetFormatProvider(this TypeConverterContext context)
        { return context != null ? context.FormatProvider : null; }

        public static DateTimeStyles SafeGetDateTimeStyles(this TypeConverterContext context)
        { return context != null ? context.DateTimeStyles : DefaultDateTimeStyles; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const DateTimeStyles DefaultDateTimeStyles = DateTimeStyles.RoundtripKind;
        #endregion
    }
}
