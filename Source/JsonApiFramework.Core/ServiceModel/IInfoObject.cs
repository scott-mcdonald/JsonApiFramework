// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.ServiceModel
{
    public interface IInfoObject
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        void Initialize(IServiceModel serviceModel, IResourceType resourceType);
        #endregion
    }
}