// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace JsonApiFramework
{
    public interface IResourceCollectionSource<TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IEnumerable<TResource> GetResourceCollection();

        Task<IEnumerable<TResource>> GetResourceCollectionAsync();
        #endregion
    }
}
