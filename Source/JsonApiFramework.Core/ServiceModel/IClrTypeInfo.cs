// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Json;

namespace JsonApiFramework.ServiceModel
{
    public interface IClrTypeInfo : IJsonObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string ClrTypeName { get; }
        Type ClrType { get; }

        IAttributesInfo AttributesInfo { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void Initialize(IReadOnlyDictionary<Type, IComplexType> clrTypeToComplexTypeDictionary);

        object CreateClrObject();

        IAttributeInfo GetApiAttributeInfo(string apiPropertyName);
        IAttributeInfo GetClrAttributeInfo(string clrPropertyName);

        bool TryGetApiAttributeInfo(string apiPropertyName, out IAttributeInfo attributeInfo);
        bool TryGetClrAttributeInfo(string clrPropertyName, out IAttributeInfo attributeInfo);
        #endregion
    }
}