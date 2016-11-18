// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

namespace JsonApiFramework.ServiceModel
{
    public interface IServiceModel
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IEnumerable<IComplexType> ComplexTypes { get; }
        IEnumerable<IResourceType> ResourceTypes { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IComplexType GetComplexType(Type clrComplexType);
        IResourceType GetResourceType(string apiResourceType);
        IResourceType GetResourceType(Type clrResourceType);
        #endregion
    }
}