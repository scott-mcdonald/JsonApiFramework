// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using JsonApiFramework.Reflection;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public static class ComplexTypeBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IAttributeInfoBuilder Attribute<TComplex, TProperty>(this IComplexTypeBuilder<TComplex> complexTypeBuilder, Expression<Func<TComplex, TProperty>> clrPropertySelector)
        {
            Contract.Requires(complexTypeBuilder != null);
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyType = typeof(TProperty);
            var clrPropertyName = StaticReflection.GetMemberName(clrPropertySelector);
            return complexTypeBuilder.Attribute(clrPropertyName, clrPropertyType);
        }
        #endregion
    }
}
