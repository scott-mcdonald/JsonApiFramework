// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Threading.Tasks;

namespace JsonApiFramework.Server
{
    public interface IToOneIncludedResourceSource<TFromResource, TToResource>
        where TFromResource : class
        where TToResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Properties
        IToOneIncludedResource<TFromResource, TToResource> GetToOneIncludedResource();

        Task<IToOneIncludedResource<TFromResource, TToResource>> GetToOneIncludedResourceAsync();
        #endregion
    }
}
