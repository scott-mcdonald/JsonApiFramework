// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Reflection;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class AttributeInfo<TObject, TProperty> : ClrPropertyInfo<TObject, TProperty>
        , IAttributeInfo
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public AttributeInfo(string clrPropertyName, string apiPropertyName, bool isComplexType)
            : base(clrPropertyName)
        {
            Contract.Requires(String.IsNullOrWhiteSpace(apiPropertyName) == false);

            this.ApiPropertyName = apiPropertyName;
            this.IsComplexType = isComplexType;

            var collectionProperitesInfo = GetCollectionPropertiesInfo();
            this.IsCollection = collectionProperitesInfo.Item1;
            this.ClrCollectionItemType = collectionProperitesInfo.Item2;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IClrPropertyInfo Implementation
        [JsonProperty] public string ApiPropertyName { get; }
        [JsonProperty] public bool IsCollection { get; }
        [JsonProperty] public bool IsComplexType { get; }
        [JsonProperty] public Type ClrCollectionItemType { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Initialize Methods
        private static Tuple<bool, Type> GetCollectionPropertiesInfo()
        {
            var clrPropertyType = typeof(TProperty);

            if (clrPropertyType.IsPrimitive())
                return new Tuple<bool, Type>(false, null);

            Type clrCollectionItemType;
            return clrPropertyType.IsEnumerableOfT(out clrCollectionItemType)
                ? new Tuple<bool, Type>(true, clrCollectionItemType)
                : new Tuple<bool, Type>(false, null);
        }
        #endregion
    }
}