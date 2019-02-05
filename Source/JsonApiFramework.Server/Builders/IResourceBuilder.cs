// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    /// <summary>
    /// Abstracts json:api compliant "resource" building with a progressive
    /// fluent interface style. 
    /// </summary>
    /// <typeparam name="TBuilder">Type of builder interface to
    /// return while building the json:api "resource" object.</typeparam>
    /// <typeparam name="TResource">Type of CLR "resource" object</typeparam>
    public interface IResourceBuilder<out TBuilder, out TResource>
        where TBuilder : class
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TBuilder SetMeta(Meta meta);
        TBuilder SetMeta(IEnumerable<Meta> metaCollection);

        IResourcePathContextBuilder<TBuilder> Paths();

        IRelationshipsBuilder<TBuilder, TResource> Relationships();

        IResourceLinksBuilder<TBuilder, TResource> Links();
        #endregion
    }
}
