// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Reflection;

namespace JsonApiFramework.Reflection
{
    /// <summary>
    /// Reflection methods for the .NET PropertyInfo class.
    /// </summary>
    public static class PropertyReflection
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Property Methods
        public static bool IsStatic(PropertyInfo propertyInfo)
        {
            Contract.Requires(propertyInfo != null);

            var isStatic = (propertyInfo.CanRead  && propertyInfo.GetMethod.IsStatic) ||
                           (propertyInfo.CanWrite && propertyInfo.SetMethod.IsStatic);
            return isStatic;
        }
        #endregion
    }
}
