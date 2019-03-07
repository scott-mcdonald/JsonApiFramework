// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    /// <summary>
    /// Abstracts framework-level json:api compliant "link" creation with a
    /// builder progressive fluent interface style.
    /// </summary>
    /// <typeparam name="TParentBuilder">Type of parent builder interface to
    /// return when done building the json:api "link" object.</typeparam>
    public interface ILinkBuilder<out TParentBuilder>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Sets the "meta" member with the same meta of the link.</summary>
        ILinkBuilder<TParentBuilder> SetMeta(Meta meta);

        ILinkBuilder<TParentBuilder> SetMeta(IEnumerable<Meta> metaCollection);

        TParentBuilder LinkEnd();
        #endregion
    }
}
