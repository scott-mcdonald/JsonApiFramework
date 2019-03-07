// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    public interface IResourceBuilder<out TBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TBuilder SetMeta(Meta              meta);
        TBuilder SetMeta(IEnumerable<Meta> metaCollection);

        IResourcePathContextBuilder<TBuilder> Paths();

        IRelationshipsBuilder<TBuilder> Relationships();

        IResourceLinksBuilder<TBuilder> Links();
        #endregion
    }

    public interface IResourceBuilder<out TBuilder, out TResource>
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
