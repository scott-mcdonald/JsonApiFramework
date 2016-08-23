// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.ServiceModel
{
    public interface IAttributesInfo : IInfoObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IEnumerable<IAttributeInfo> Collection { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IAttributeInfo GetApiAttribute(string apiPropertyName);
        IAttributeInfo GetClrAttribute(string clrPropertyName);

        bool TryGetApiAttribute(string apiPropertyName, out IAttributeInfo attribute);
        bool TryGetClrAttribute(string clrPropertyName, out IAttributeInfo attribute);
        #endregion
    }
}