// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Client
{
    public interface IAttributeProxy<out TProperty>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrPropertyType { get; }
        string ClrPropertyName { get; }
        TProperty ClrPropertyValue { get; }
        #endregion
    }
}
