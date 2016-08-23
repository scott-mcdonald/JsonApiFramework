// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Server
{
    /// <summary>
    /// Abstracts the building of a json:api primary "resource" object
    /// through a progressive fluent interface style.
    /// </summary>
    public interface IPrimaryResourceBuilder<out TResource> : IResourceBuilder<IPrimaryResourceBuilder<TResource>, TResource>
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IIncludedBuilder ResourceEnd();
        #endregion
    }
}
