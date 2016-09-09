// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public interface IComplexTypeBuilder
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type ClrComplexType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IAttributeInfoBuilder Attribute(string clrPropertyName, Type clrPropertyType);
        #endregion
    }

    public interface IComplexTypeBuilder<TComplex> : IComplexTypeBuilder
    { }
}