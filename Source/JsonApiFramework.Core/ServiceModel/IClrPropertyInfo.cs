// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.ServiceModel
{
    public interface IClrPropertyInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string ClrPropertyName { get; }

        Type ClrPropertyType { get; }
        #endregion
    }

    public interface IClrPropertyInfo<in TObject, TProperty> : IClrPropertyInfo
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TProperty GetClrProperty(TObject clrObject);

        void SetClrProperty(TObject clrObject, TProperty clrProperty);
        #endregion
    }
}