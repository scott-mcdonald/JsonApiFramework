// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Converters
{
    /// <summary>
    /// Abstract a type converter that converts from one type to another type.
    /// </summary>
    public interface ITypeConverter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TTarget Convert<TSource, TTarget>(TSource source, TypeConverterContext context);
        #endregion
    }
}
