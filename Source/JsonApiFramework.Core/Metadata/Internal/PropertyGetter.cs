// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Converters;
using JsonApiFramework.Expressions;

namespace JsonApiFramework.Metadata.Internal
{
    internal interface IPropertyGetter
    { }

    internal interface IPropertyGetter<in TObject> : IPropertyGetter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TValue GetClrProperty<TValue>(TObject clrObject, ITypeConverter typeConverter, TypeConverterContext typeConverterContext);
        #endregion
    }

    internal class PropertyGetter<TObject, TProperty> : IPropertyGetter<TObject>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public PropertyGetter(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            this.PropertyGetterLambda = CreatePropertyGetterLambda(clrPropertyName);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPropertyGetter<TObject> Implementation
        public TValue GetClrProperty<TValue>(TObject clrObject, ITypeConverter typeConverter, TypeConverterContext typeConverterContext)
        {
            Contract.Requires(clrObject != null);
            Contract.Requires(typeConverter != null);

            var clrPropertyValue = this.PropertyGetterLambda(clrObject);
            var clrValue = typeConverter.Convert<TProperty, TValue>(clrPropertyValue, typeConverterContext);
            return clrValue;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<TObject, TProperty> PropertyGetterLambda { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<TObject, TProperty> CreatePropertyGetterLambda(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var propertyGetterLambdaExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(clrPropertyName);
            var propertyGetterLambda = propertyGetterLambdaExpression.Compile();
            return propertyGetterLambda;
        }
        #endregion
    }
}