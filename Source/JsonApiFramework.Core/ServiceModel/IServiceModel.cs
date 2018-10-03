// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Json;

namespace JsonApiFramework.ServiceModel
{
    public interface IServiceModel : IJsonObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        IEnumerable<IComplexType> ComplexTypes { get; }
        IEnumerable<IResourceType> ResourceTypes { get; }

        IResourceType HomeResourceType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IComplexType GetComplexType(Type clrComplexType);

        IResourceType GetResourceType(string apiResourceType);
        IResourceType GetResourceType(Type clrResourceType);

        bool TryGetComplexType(Type clrComplexType, out IComplexType resourceType);

        bool TryGetResourceType(string apiResourceType, out IResourceType resourceType);
        bool TryGetResourceType(Type clrResourceType, out IResourceType resourceType);
        #endregion
    }
}