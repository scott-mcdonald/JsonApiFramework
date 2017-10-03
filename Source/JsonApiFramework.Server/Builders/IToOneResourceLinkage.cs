// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server
{
    public interface IToOneResourceLinkage
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        bool HasValue { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        ResourceIdentifier CreateApiResourceIdentifier(IResourceType resourceType);
        #endregion
    }

    public interface IToOneResourceLinkage<out TResourceId> : IToOneResourceLinkage
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        TResourceId Value { get; }
        #endregion
    }
}