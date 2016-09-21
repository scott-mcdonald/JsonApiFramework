// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Globalization;

namespace JsonApiFramework.Converters
{
    /// <summary>
    /// Optional runtime context when converting from one type to another type.
    /// </summary>
    public class TypeConverterContext
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>
        /// Optional format string to use when converting from string to other types.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Optional format provider to use when converting from string to other types.
        /// </summary>
        public IFormatProvider FormatProvider { get; set; }

        /// <summary>
        /// Optional DateTimeStyles to use when converting from string to DateTime or DateTimeOffset.
        /// </summary>
        public DateTimeStyles DateTimeStyles { get; set; }
        #endregion
    }
}