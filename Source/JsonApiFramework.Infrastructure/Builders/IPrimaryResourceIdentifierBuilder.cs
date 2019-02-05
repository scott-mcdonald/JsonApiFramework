// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework
{
    public interface IPrimaryResourceIdentifierBuilder<out TResource> : IResourceIdentifierBuilder<IPrimaryResourceIdentifierBuilder<TResource>, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IDocumentWriter ResourceIdentifierEnd();
        #endregion
    }
}
