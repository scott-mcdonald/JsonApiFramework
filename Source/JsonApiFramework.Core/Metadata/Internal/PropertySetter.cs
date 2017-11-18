// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Converters;
using JsonApiFramework.Expressions;

namespace JsonApiFramework.Metadata.Internal
{
    internal interface IPropertySetter
    { }

    internal interface IPropertySetter<in TObject> : IPropertySetter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void SetClrProperty<TValue>(TObject clrObject, TValue clrValue, ITypeConverter typeConverter, TypeConverterContext typeConverterContext);
        #endregion
    }

    internal class PropertySetter<TObject, TProperty> : IPropertySetter<TObject>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public PropertySetter(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            this.PropertySetterLambda = CreatePropertySetterLambda(clrPropertyName);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPropertySetter<TObject> Implementation
        public void SetClrProperty<TValue>(TObject clrObject, TValue clrValue, ITypeConverter typeConverter, TypeConverterContext typeConverterContext)
        {
            Contract.Requires(clrObject != null);
            Contract.Requires(typeConverter != null);

            var clrPropertyValue = typeConverter.Convert<TValue, TProperty>(clrValue, typeConverterContext);
            this.PropertySetterLambda(clrObject, clrPropertyValue);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Action<TObject, TProperty> PropertySetterLambda { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Action<TObject, TProperty> CreatePropertySetterLambda(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var propertySetterLambdaExpression = ExpressionBuilder.PropertySetter<TObject, TProperty>(clrPropertyName);
            var propertySetterLambda = propertySetterLambdaExpression.Compile();
            return propertySetterLambda;
        }
        #endregion
    }
}