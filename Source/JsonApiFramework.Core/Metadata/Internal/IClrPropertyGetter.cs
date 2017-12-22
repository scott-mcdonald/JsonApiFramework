// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.TypeConversion;

namespace JsonApiFramework.Metadata.Internal
{
    internal interface IClrPropertyGetter
    { }

    internal interface IClrPropertyGetter<in TObject> : IClrPropertyGetter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TValue GetClrProperty<TValue>(TObject clrObject, ITypeConverter typeConverter, TypeConverterContext typeConverterContext);
        #endregion
    }
}