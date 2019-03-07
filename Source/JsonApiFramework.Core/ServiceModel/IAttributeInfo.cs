// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Extension;

namespace JsonApiFramework.ServiceModel
{
    public interface IAttributeInfo : IPropertyInfo, IExtensibleObject<IAttributeInfo>
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string ApiPropertyName { get; }

        bool IsCollection { get; }
        bool IsComplexType { get; }

        Type ClrCollectionItemType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void Initialize(IReadOnlyDictionary<Type, IComplexType> clrTypeToComplexTypeDictionary);
        #endregion
    }
}