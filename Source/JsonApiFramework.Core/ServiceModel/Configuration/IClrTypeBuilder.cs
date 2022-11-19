// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.ServiceModel.Configuration;

public interface IClrTypeBuilder
{
    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region Properties
    Type ClrType { get; }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    IAttributeInfoBuilder Attribute(string clrPropertyName, Type clrPropertyType);
    #endregion
}

public interface IClrTypeBuilder<T> : IClrTypeBuilder
{ }