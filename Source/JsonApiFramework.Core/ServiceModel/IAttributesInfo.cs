// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.ServiceModel;

public interface IAttributesInfo : IMemberInfo
{
    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region Properties
    IEnumerable<IAttributeInfo> Collection { get; }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    void Initialize(IReadOnlyDictionary<Type, IComplexType> clrTypeToComplexTypeDictionary);

    IAttributeInfo GetApiAttributeInfo(string apiPropertyName);
    IAttributeInfo GetClrAttributeInfo(string clrPropertyName);

    bool TryGetApiAttributeInfo(string apiPropertyName, out IAttributeInfo attribute);
    bool TryGetClrAttributeInfo(string clrPropertyName, out IAttributeInfo attribute);
    #endregion
}