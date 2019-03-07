// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework
{
    /// <summary>
    /// Abstracts json:api compliant "resource identifier" building with a
    /// progressive fluent interface style. 
    /// </summary>
    /// <typeparam name="TBuilder">Type of builder interface to
    /// return while building the json:api "resource identifier" object.</typeparam>
    public interface IResourceIdentifierBuilder<out TBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        TBuilder SetMeta(Meta              meta);
        TBuilder SetMeta(IEnumerable<Meta> metaCollection);

        TBuilder SetId<T>(IId<T>           id);
        TBuilder SetId<T>(IIdCollection<T> idCollection);
        #endregion
    }
}
