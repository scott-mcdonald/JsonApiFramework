// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Reflection
{
    /// <summary>
    /// Represents an exception that is thrown from the <c>TypeConverter</c>
    /// class if unable to convert types.
    /// </summary>
    public class TypeConverterException : Exception
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public TypeConverterException()
        { }

        public TypeConverterException(string message)
            : base(message)
        { }

        public TypeConverterException(string message, Exception innerException)
            : base(message, innerException)
        { }
        #endregion
    }
}