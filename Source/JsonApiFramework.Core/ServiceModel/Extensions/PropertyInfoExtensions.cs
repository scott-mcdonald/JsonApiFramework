// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.ServiceModel
{
    public static class PropertyInfoExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPropertyInfo Extensions Methods
        public static bool CanGetOrSetClrProperty(this IPropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);

            var clrPropertyNameDefined = String.IsNullOrWhiteSpace(propertyInfo.ClrPropertyName) == false;
            var clrPropertyTypeDefined = propertyInfo.ClrPropertyType != null;
            return clrPropertyNameDefined && clrPropertyTypeDefined;
        }
        #endregion
    }
}
