// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Client
{
    public interface IPrimaryResourceBuilder<out TResource> : IResourceBuilder<IPrimaryResourceBuilder<TResource>, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IDocumentWriter ResourceEnd();
        #endregion
    }
}
