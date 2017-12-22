// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Expressions;
using JsonApiFramework.TypeConversion;

namespace JsonApiFramework.Metadata.Internal
{
    internal class ClrPropertySetter<TObject, TProperty> : IClrPropertySetter<TObject>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ClrPropertySetter(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            this.ClrPropertySetterLambda = CreateClrPropertySetterLambda(clrPropertyName);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IClrPropertySetter<TObject> Implementation
        public void SetClrProperty<TValue>(TObject clrObject, TValue clrValue, ITypeConverter typeConverter, TypeConverterContext typeConverterContext)
        {
            Contract.Requires(clrObject != null);
            Contract.Requires(typeConverter != null);

            var clrPropertyValue = typeConverter.Convert<TValue, TProperty>(clrValue, typeConverterContext);
            this.ClrPropertySetterLambda(clrObject, clrPropertyValue);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Action<TObject, TProperty> ClrPropertySetterLambda { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Action<TObject, TProperty> CreateClrPropertySetterLambda(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var clrPropertySetterLambdaExpression = ExpressionBuilder.PropertySetter<TObject, TProperty>(clrPropertyName);
            var clrPropertySetterLambda = clrPropertySetterLambdaExpression.Compile();
            return clrPropertySetterLambda;
        }
        #endregion
    }
}