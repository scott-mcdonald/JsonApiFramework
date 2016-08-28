// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework
{
    /// <summary>
    /// Extension methods for the .NET Object class.
    /// </summary>
    public static class ObjectExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Returns the string result of the <c>ToString</c> method on an
        /// object even if the object is null. If the object is null, returns
        /// a null string.
        /// </summary>
        public static string SafeToString(this object source)
        {
            return source != null
                ? source.ToString()
                : null;
        }

        /// <summary>
        /// Specialization of the <c>SafeToString</c> for an implementation
        /// of the <c>IFormattable</c> interface.
        /// </summary>
        public static string SafeToString(this IFormattable source, string format, IFormatProvider formatProvider)
        {
            return source != null
                ? source.ToString(format, formatProvider)
                : null;
        }
        #endregion
    }
}
