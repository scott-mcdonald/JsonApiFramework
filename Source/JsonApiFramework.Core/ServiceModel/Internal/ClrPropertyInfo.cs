// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Expressions;
using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ClrPropertyInfo<TObject, TProperty> : JsonObject
        , IClrPropertyInfo<TObject, TProperty>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ClrPropertyInfo(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            this.ClrPropertyName = clrPropertyName;

            this.InitializePropertyGetterLambda(clrPropertyName);
            this.InitializePropertySetterLambda(clrPropertyName);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IClrPropertyInfo Implementation
        [JsonProperty] public string ClrPropertyName { get; }
        [JsonProperty] public Type ClrPropertyType => typeof(TProperty);
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IClrPropertyInfo<TObject, TProperty> Implementation
        public TProperty GetClrProperty(TObject clrObject)
        {
            if (this.CanGetOrSetClrProperty() == false)
                return default(TProperty);

            var clrProperty = this.ClrPropertyGetterLambda(clrObject);
            return clrProperty;
        }

        public void SetClrProperty(TObject clrObject, TProperty clrProperty)
        {
            if (this.CanGetOrSetClrProperty() == false)
                return;

            this.ClrPropertySetterLambda(clrObject, clrProperty);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Func<TObject, TProperty> ClrPropertyGetterLambda { get; set; }
        private Action<TObject, TProperty> ClrPropertySetterLambda { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Initialize Methods
        private void InitializePropertyGetterLambda(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var clrPropertyGetterLambdaExpression = ExpressionBuilder.PropertyGetter<TObject, TProperty>(clrPropertyName);
            var clrPropertyGetterLambda = clrPropertyGetterLambdaExpression.Compile();
            this.ClrPropertyGetterLambda = clrPropertyGetterLambda;
        }

        private void InitializePropertySetterLambda(string clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(clrPropertyName) == false);

            var clrPropertySetterLambdaExpression = ExpressionBuilder.PropertySetter<TObject, TProperty>(clrPropertyName);
            var clrPropertySetterLambda = clrPropertySetterLambdaExpression.Compile();
            this.ClrPropertySetterLambda = clrPropertySetterLambda;
        }
        #endregion
    }
}