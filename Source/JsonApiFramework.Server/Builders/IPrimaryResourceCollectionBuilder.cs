// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.Server
{
    public interface IPrimaryResourceCollectionBuilder : IResourceBuilder<IPrimaryResourceCollectionBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IIncludedBuilder ResourceCollectionEnd();
        #endregion
    }

    public interface IPrimaryResourceCollectionBuilder<out TResource> : IResourceBuilder<IPrimaryResourceCollectionBuilder<TResource>, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IIncludedBuilder ResourceCollectionEnd();
        #endregion
    }
}
