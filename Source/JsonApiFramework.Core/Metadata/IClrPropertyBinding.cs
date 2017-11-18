// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Converters;

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents a binding to a CLR property that allows the programtically invocation of the CLR property getter and setter methods.</summary>
    public interface IClrPropertyBinding
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TValue GetClrProperty<TObject, TValue>(TObject clrObject, ITypeConverter typeConverter, TypeConverterContext typeConverterContext);

        void SetClrProperty<TObject, TValue>(TObject clrObject, TValue clrValue, ITypeConverter typeConverter, TypeConverterContext typeConverterContext);
        #endregion
    }
}