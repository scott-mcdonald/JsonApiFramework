// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace JsonApiFramework
{
    public interface IResourceIdentifierCollectionSource<TResourceId>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IEnumerable<TResourceId> GetResourceIdCollection();

        Task<IEnumerable<TResourceId>> GetResourceIdCollectionAsync();
        #endregion
    }
}
