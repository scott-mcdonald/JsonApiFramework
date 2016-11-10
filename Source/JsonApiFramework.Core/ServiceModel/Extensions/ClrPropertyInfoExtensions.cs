// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace JsonApiFramework.ServiceModel
{
    public static class ClrPropertyInfoExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPropertyInfo Extensions Methods
        public static bool CanGetOrSetClrProperty(this IClrPropertyInfo clrPropertyInfo)
        {
            Contract.Requires(clrPropertyInfo != null);

            var clrPropertyNameDefined = string.IsNullOrWhiteSpace(clrPropertyInfo.ClrPropertyName) == false;
            var clrPropertyTypeDefined = clrPropertyInfo.ClrPropertyType != null;
            return clrPropertyNameDefined && clrPropertyTypeDefined;
        }
        #endregion
    }
}
