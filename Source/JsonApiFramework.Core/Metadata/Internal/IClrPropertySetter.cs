// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.TypeConversion;

namespace JsonApiFramework.Metadata.Internal
{
    internal interface IClrPropertySetter
    { }

    internal interface IClrPropertySetter<in TObject> : IClrPropertySetter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void SetClrProperty<TValue>(TObject clrObject, TValue clrValue, ITypeConverter typeConverter, TypeConverterContext typeConverterContext);
        #endregion
    }
}