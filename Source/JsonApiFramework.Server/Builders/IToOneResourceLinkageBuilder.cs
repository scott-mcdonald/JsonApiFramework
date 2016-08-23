// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Server
{
    public interface IToOneResourceLinkageBuilder<out TResource> : IResourceBuilder<IToOneResourceLinkageBuilder<TResource>, TResource>
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IIncludedResourcesBuilder ToOneEnd();
        #endregion
    }
}
