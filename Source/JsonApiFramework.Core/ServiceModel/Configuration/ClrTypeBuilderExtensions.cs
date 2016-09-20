// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using JsonApiFramework.Reflection;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public static class ClrTypeBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IAttributeInfoBuilder Attribute<T, TProperty>(this IClrTypeBuilder<T> clrTypeBuilder, Expression<Func<T, TProperty>> clrPropertySelector)
        {
            Contract.Requires(clrTypeBuilder != null);
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyType = typeof(TProperty);
            var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
            return clrTypeBuilder.Attribute(clrPropertyName, clrPropertyType);
        }
        #endregion
    }
}
