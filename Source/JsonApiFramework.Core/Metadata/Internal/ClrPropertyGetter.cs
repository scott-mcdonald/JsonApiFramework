// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Expressions;
using JsonApiFramework.TypeConversion;

namespace JsonApiFramework.Metadata.Internal
{
    internal class ClrPropertyGetter<TObject, TProperty> : IClrPropertyGetter<TObject>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ClrPropertyGetter(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            this.ClrPropertyGetterLambda = CreateClrPropertyGetterLambda(clrPropertyName);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IClrPropertyGetter<TObject> Implementation
        public TValue GetClrProperty<TValue>(TObject clrObject, ITypeConverter typeConverter, TypeConverterContext typeConverterContext)
        {
            Contract.Requires(clrObject != null);
            Contract.Requires(typeConverter != null);

            var clrPropertyValue = this.ClrPropertyGetterLambda(clrObject);
            var clrValue = typeConverter.Convert<TProperty, TValue>(clrPropertyValue, typeConverterContext);
            return clrValue;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<TObject, TProperty> ClrPropertyGetterLambda { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static Func<TObject, TProperty> CreateClrPropertyGetterLambda(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var clrPropertyGetterLambdaExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(clrPropertyName);
            var clrPropertyGetterLambda = clrPropertyGetterLambdaExpression.Compile();
            return clrPropertyGetterLambda;
        }
        #endregion
    }
}