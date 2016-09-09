// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Expressions;
using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal abstract class ClrTypeInfo : JsonObject
        , IClrTypeInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IResourceType Implementation
        [JsonProperty] public Type ClrType { get; private set; }
        [JsonProperty] public IAttributesInfo AttributesInfo { get; private set; }
        #endregion

        #region Properties
        public string ClrTypeName { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceType Implementation
        public void Initialize(IReadOnlyDictionary<Type, IComplexType> clrTypeToComplexTypeDictionary)
        {
            if (clrTypeToComplexTypeDictionary == null)
                return;

            if (this.AttributesInfo == null)
                return;

            this.AttributesInfo.Initialize(clrTypeToComplexTypeDictionary);
        }

        public object CreateClrObject()
        {
            var clrObject = this.ClrObjectNewMethod.DynamicInvoke();
            return clrObject;
        }

        public IAttributeInfo GetApiAttributeInfo(string apiPropertyName)
        {
            if (this.AttributesInfo != null)
            {
                return this.AttributesInfo.GetApiAttributeInfo(apiPropertyName);
            }

            var attributesInfoMissingException = this.CreateInfoMissingException<AttributesInfo>();
            throw attributesInfoMissingException;
        }

        public IAttributeInfo GetClrAttributeInfo(string clrPropertyName)
        {
            if (this.AttributesInfo != null)
            {
                return this.AttributesInfo.GetClrAttributeInfo(clrPropertyName);
            }

            var attributesInfoMissingException = this.CreateInfoMissingException<AttributesInfo>();
            throw attributesInfoMissingException;
        }

        public bool TryGetApiAttributeInfo(string apiPropertyName, out IAttributeInfo attributeInfo)
        {
            attributeInfo = null;
            return this.AttributesInfo != null &&
                   this.AttributesInfo.TryGetApiAttributeInfo(apiPropertyName, out attributeInfo);
        }

        public bool TryGetClrAttributeInfo(string clrPropertyName, out IAttributeInfo attributeInfo)
        {
            attributeInfo = null;
            return this.AttributesInfo != null &&
                   this.AttributesInfo.TryGetClrAttributeInfo(clrPropertyName, out attributeInfo);
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ClrTypeInfo(Type clrType,
                              IAttributesInfo attributesInfo)
        {
            Contract.Requires(clrType != null);
            Contract.Requires(attributesInfo != null);

            // JSON Properties
            this.ClrType = clrType;
            this.AttributesInfo = attributesInfo;

            // Non-JSON Properties
            this.ClrTypeName = clrType.Name;

            // Initialization
            this.InitializeClrObjectNewMethod();
        }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Methods
        protected ServiceModelException CreateInfoMissingException<TInfo>()
        {
            var serviceModelInfoTypeName = this.GetType().Name;
            var clrTypeDescription = "{0} [clrType={1}]".FormatWith(serviceModelInfoTypeName, this.ClrTypeName);

            var infoDescription = typeof(TInfo).Name;

            var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                         .FormatWith(clrTypeDescription, infoDescription);
            var infoMissingException = new ServiceModelException(detail);
            return infoMissingException;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ClrTypeInfo()
        { }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Delegate ClrObjectNewMethod { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Initialize Methods
        private void InitializeClrObjectNewMethod()
        {
            var clrObjectNewExpression = ExpressionBuilder.New(this.ClrType);
            var clrObjectNewMethod = clrObjectNewExpression.Compile();
            this.ClrObjectNewMethod = clrObjectNewMethod;
        }
        #endregion
    }
}