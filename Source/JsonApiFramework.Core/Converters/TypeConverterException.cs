// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Converters
{
    /// <summary>
    /// Represents an exception that is thrown when the <c>TypeConverter</c>
    /// is unable to convert between types for the "Convert" methods.
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

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Factory Methods
        public static TypeConverterException Create(object source, Type sourceType, Type targetType, Exception innerException = null)
        {
            Contract.Requires(targetType != null);

            var sourceAsString = source != null ? source.ToString() : "null";
            var sourceTypeAsString = sourceType != null ? sourceType.Name : "unknown";
            var targetTypeAsString = targetType.Name;

            var message = String.Format(CoreErrorStrings.TypeConverterExceptionMessage, sourceAsString, sourceTypeAsString, targetTypeAsString);
            var exception = innerException == null
                ? new TypeConverterException(message)
                : new TypeConverterException(message, innerException);
            return exception;
        }

        public static TypeConverterException Create<TSource, TTarget>(TSource source, Exception innerException = null)
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            return Create(source, sourceType, targetType, innerException);
        }
        #endregion
    }
}