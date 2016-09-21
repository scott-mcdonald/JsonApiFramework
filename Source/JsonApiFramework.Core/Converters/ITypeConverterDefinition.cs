// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Converters
{
    /// <summary>
    /// Abstracts a non-generic type converter definition that allows easy
    /// access to the combination of source to target types it can convert.
    /// </summary>
    public interface ITypeConverterDefinition
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Methods
        Type SourceType { get; }
        Type TargetType { get; }
        #endregion
    }

    /// <summary>
    /// Abstracts a generic type converter definition for a specific
    /// combination of source to target types.
    /// </summary>
    public interface ITypeConverterDefinition<in TSource, out TTarget> : ITypeConverterDefinition
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TTarget Convert(TSource source, TypeConverterContext context);
        #endregion
    }
}
