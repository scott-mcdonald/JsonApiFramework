// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Client
{
    public interface IResourceBuilder<out TBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TBuilder SetMeta(Meta meta);
        TBuilder SetId<T>(IId<T> id);

        IAttributesBuilder<TBuilder> Attributes();

        IRelationshipsBuilder<TBuilder> Relationships();
        #endregion
    }

    public interface IResourceBuilder<out TBuilder, out TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TBuilder SetMeta(Meta    meta);
        TBuilder SetId<T>(IId<T> id);

        IAttributesBuilder<TBuilder, TResource> Attributes();

        IRelationshipsBuilder<TBuilder> Relationships();
        #endregion
    }
}
