// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Expressions;
using JsonApiFramework.Reflection;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class PropertyInfo : MemberInfo
        , IPropertyInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public PropertyInfo(Type clrDeclaringType, string clrPropertyName, Type clrPropertyType)
            : base(clrDeclaringType)
        {
            this.ClrPropertyName = clrPropertyName;
            this.ClrPropertyType = clrPropertyType;

            this.InitializeClrPropertyMethods();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPropertyInfo Implementation
        [JsonProperty] public string ClrPropertyName { get; internal set; }
        [JsonProperty] public Type ClrPropertyType { get; internal set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IPropertyInfo Implementation
        public object GetClrProperty(object clrObject)
        {
            if (clrObject == null)
                return null;

            if (this.CanGetOrSetClrProperty() == false)
                return null;

            var clrPropertyValue = this.ClrPropertyGetterMethod.DynamicInvoke(clrObject);
            return clrPropertyValue;
        }

        public void SetClrProperty(object clrObject, object clrValue)
        {
            if (clrObject == null)
                return;

            if (this.CanGetOrSetClrProperty() == false)
                return;

            // Coerce incoming CLR value into a value based on the actual
            // property type.
            var clrPropertyType = this.ClrPropertyType;
            var clrPropertyValue = TypeConverter.Convert(clrValue, clrPropertyType);

            this.ClrPropertySetterMethod.DynamicInvoke(clrObject, clrPropertyValue);
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal PropertyInfo()
        { }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Delegate ClrPropertyGetterMethod { get; set; }
        private Delegate ClrPropertySetterMethod { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Helper Methods
        private Delegate CreateClrPropertyGetterMethod(Type clrResourceType, Type clrPropertyType)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrPropertyType != null);

            var clrPropertyName = this.ClrPropertyName;

            var clrGetterExpression = ExpressionBuilder.PropertyGetter(clrResourceType, clrPropertyName);
            var clrGetterMethod = clrGetterExpression.Compile();
            return clrGetterMethod;
        }

        private Delegate CreateClrPropertySetterMethod(Type clrResourceType, Type clrPropertyType)
        {
            Contract.Requires(clrResourceType != null);

            var clrPropertyName = this.ClrPropertyName;

            var clrSetterExpression = ExpressionBuilder.PropertySetter(clrResourceType, clrPropertyName);
            var clrSetterMethod = clrSetterExpression.Compile();
            return clrSetterMethod;
        }

        private void InitializeClrPropertyMethods()
        {
            if (this.CanGetOrSetClrProperty() == false)
                return;

            var clrDeclaringType = this.ClrDeclaringType;
            var clrPropertyType = this.ClrPropertyType;

            this.ClrPropertyGetterMethod = this.CreateClrPropertyGetterMethod(clrDeclaringType, clrPropertyType);
            this.ClrPropertySetterMethod = this.CreateClrPropertySetterMethod(clrDeclaringType, clrPropertyType);
        }
        #endregion
    }
}