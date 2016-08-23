// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.ServiceModel
{
    public interface IPropertyInfo : IInfoObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string ClrPropertyName { get; }
        Type ClrPropertyType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        object GetClrProperty(object clrResource);
        void SetClrProperty(object clrResource, object clrValue);
        #endregion
    }
}