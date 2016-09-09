// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.Reflection;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class AttributeInfo : PropertyInfo
        , IAttributeInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AttributeInfo(Type clrDeclaringType, string clrPropertyName, Type clrPropertyType, string apiPropertyName, bool isComplexType)
            : base(clrDeclaringType, clrPropertyName, clrPropertyType)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            this.ApiPropertyName = apiPropertyName;
            this.IsComplexType = isComplexType;

            this.InitializeCollectionProperties(clrPropertyType);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IAttributeInfo Implementation
        [JsonProperty] public string ApiPropertyName { get; internal set; }

        [JsonProperty] public bool IsCollection { get; private set; }
        [JsonProperty] public bool IsComplexType { get; private set; }

        [JsonProperty] public Type ClrCollectionItemType { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public void Initialize(IReadOnlyDictionary<Type, IComplexType> clrTypeToComplexTypeDictionary)
        {
            if (clrTypeToComplexTypeDictionary == null)
                return;

            var clrPropertyType = this.ClrPropertyType;
            if (clrPropertyType.IsPrimitive())
                return;

            if (this.IsCollection)
            {
                var clrCollectionItemType = this.ClrCollectionItemType;
                if (clrTypeToComplexTypeDictionary.ContainsKey(clrCollectionItemType) == false)
                    return;
            }
            else
            {
                if (clrTypeToComplexTypeDictionary.ContainsKey(clrPropertyType) == false)
                    return;
            }

            this.IsComplexType = true;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal AttributeInfo()
        { }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void InitializeCollectionProperties(Type clrPropertyType)
        {
            if (clrPropertyType.IsPrimitive())
                return;

            Type clrCollectionItemType;
            if (!clrPropertyType.IsEnumerableOfT(out clrCollectionItemType))
                return;

            this.IsCollection = true;
            this.ClrCollectionItemType = clrCollectionItemType;
        }
        #endregion
    }
}