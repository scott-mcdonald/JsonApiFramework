// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.ServiceModel
{
    public interface IClrObjectInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrObjectType { get; }
        #endregion
    }

    public interface IClrObjectInfo<out TObject> : IClrObjectInfo
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TObject CreateClrObject();
        #endregion
    }
}