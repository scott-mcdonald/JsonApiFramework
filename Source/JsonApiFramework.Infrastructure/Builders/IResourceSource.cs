// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Threading.Tasks;

namespace JsonApiFramework
{
    public interface IResourceSource<TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TResource GetResource();

        Task<TResource> GetResourceAsync();
        #endregion
    }
}
