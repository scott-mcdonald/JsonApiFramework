// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Converters
{
    /// <summary>
    /// Extension methods built from the <c>ITypeConverter</c> abstraction.
    /// </summary>
    public static class TypeConverterExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static TTarget Convert<TSource, TTarget>(this ITypeConverter typeConverter, TSource source)
        {
            Contract.Requires(typeConverter != null);

            return typeConverter.Convert<TSource, TTarget>(source, null);
        }

        public static bool TryConvert<TSource, TTarget>(this ITypeConverter typeConverter, TSource source, TypeConverterContext context, out TTarget target)
        {
            Contract.Requires(typeConverter != null);

            try
            {
                target = typeConverter.Convert<TSource, TTarget>(source, context);
                return true;
            }
            catch (Exception)
            {
                target = default(TTarget);
                return false;
            }
        }

        public static bool TryConvert<TSource, TTarget>(this ITypeConverter typeConverter, TSource source, out TTarget target)
        {
            Contract.Requires(typeConverter != null);

            try
            {
                target = typeConverter.Convert<TSource, TTarget>(source, null);
                return true;
            }
            catch (Exception)
            {
                target = default(TTarget);
                return false;
            }
        }
        #endregion
    }
}
