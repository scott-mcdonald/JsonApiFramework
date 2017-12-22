// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.TypeConversion;

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents a binding to a CLR property that allows the programtically invocation of the CLR property getter and setter methods.</summary>
    public interface IClrPropertyBinding
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the CLR property name the property binding was created with.</summary>
        string ClrPropertyName { get; }

        /// <summary>Gets the CLR property type the property binding was created with.</summary>
        Type ClrPropertyType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Gets the CLR property value represented by this property binding on the given CLR object with type coercion done with the given type converter.
        /// </summary>
        /// <typeparam name="TObject">Type of CLR object to get the CLR property value from.</typeparam>
        /// <typeparam name="TValue">Type of CLR value to get by performing type coercion from the internal CLR property type.</typeparam>
        /// <param name="clrObject">CLR object to get the property value from.</param>
        /// <param name="typeConverter">Required type converter object to perform type coercion from the internal property type to the desired value type.</param>
        /// <param name="typeConverterContext">Optional type converter context object to perform type coercion from the internal property type to the desired value type.</param>
        /// <returns>The CLR property value represented by the CLR property binding converted to the desired CLR value type.</returns>
        TValue GetClrProperty<TObject, TValue>(TObject clrObject, ITypeConverter typeConverter, TypeConverterContext typeConverterContext);

        /// <summary>
        /// Sets the CLR property value represented by this property binding on the given CLR object with type coercion done with the given type converter.
        /// </summary>
        /// <typeparam name="TObject">Type of CLR object to set the CLR property value to.</typeparam>
        /// <typeparam name="TValue">Type of CLR value to set by performing type coercion to the internal CLR property type.</typeparam>
        /// <param name="clrObject">CLR object to set the property value to.</param>
        /// <param name="clrValue"></param>
        /// <param name="typeConverter">Required type converter object to perform type coercion from the given value type to the internal property type.</param>
        /// <param name="typeConverterContext">Optional type converter context object to perform type coercion from the given value type to the internal property type.</param>
        void SetClrProperty<TObject, TValue>(TObject clrObject, TValue clrValue, ITypeConverter typeConverter, TypeConverterContext typeConverterContext);
        #endregion
    }
}