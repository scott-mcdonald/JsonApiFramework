// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Converters;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>
    /// Abstracts a value node in the DOM tree.
    /// </summary>
    public interface IDomValue : IDomNode
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>
        /// Gets the underlying CLR value type contained in this DOM value
        /// node.
        /// </summary>
        Type ClrUnderlyingValueType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Gets the CLR value with optional type converter from this DOM value
        /// node as a target generic type.
        /// </summary>
        TTarget ClrValue<TTarget>(ITypeConverter typeConverter, TypeConverterContext typeConverterContext);
        #endregion
    }
}