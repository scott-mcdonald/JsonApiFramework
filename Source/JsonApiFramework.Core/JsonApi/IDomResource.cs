// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts queryable (non-mutating) access to a DOM node representing
    /// a json:api compliant resource object.
    /// </summary>
    public interface IDomResource : IDomNode
        , IGetMeta
        , IGetRelationships
        , IGetLinks
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        string GetApiType();
        string GetApiId();
        IEnumerable<IDomAttribute> GetDomAttributes();
        #endregion
    }
}