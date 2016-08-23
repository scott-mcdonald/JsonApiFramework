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
    internal class PropertyInfo : InfoObject
        , IPropertyInfo
    {
        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        public PropertyInfo(string clrPropertyName, Type clrPropertyType)
        {
            this.ClrPropertyName = clrPropertyName;
            this.ClrPropertyType = clrPropertyType;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPropertyInfo Implementation
        [JsonProperty] public string ClrPropertyName { get; internal set; }
        [JsonProperty] public Type ClrPropertyType { get; internal set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region InfoObject Overrides
        public override void Initialize(IServiceModel serviceModel, IResourceType resourceType)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(resourceType != null);

            base.Initialize(serviceModel, resourceType);

            // Initialize the CLR property getter/setter compiled expression methods.
            this.InitializeClrPropertyMethods();
        }
        #endregion

        #region IPropertyInfo Implementation
        public object GetClrProperty(object clrResource)
        {
            if (clrResource == null)
                return null;

            if (this.CanGetOrSetClrProperty() == false)
                return null;

            var clrPropertyValue = this.ClrPropertyGetterMethod.DynamicInvoke(clrResource);
            return clrPropertyValue;
        }

        public void SetClrProperty(object clrResource, object clrValue)
        {
            if (clrResource == null)
                return;

            if (this.CanGetOrSetClrProperty() == false)
                return;

            // Coerce incoming CLR value into a value based on the actual
            // property type.
            var clrPropertyType = this.ClrPropertyType;
            var clrPropertyValue = TypeConverter.Convert(clrValue, clrPropertyType);

            this.ClrPropertySetterMethod.DynamicInvoke(clrResource, clrPropertyValue);
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

            var clrResourceType = this.ResourceType.ClrResourceType;
            var clrPropertyType = this.ClrPropertyType;

            this.ClrPropertyGetterMethod = this.CreateClrPropertyGetterMethod(clrResourceType, clrPropertyType);
            this.ClrPropertySetterMethod = this.CreateClrPropertySetterMethod(clrResourceType, clrPropertyType);
        }
        #endregion
    }
}