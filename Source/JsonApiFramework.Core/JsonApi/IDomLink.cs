// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts queryable (non-mutating) access to a DOM node representing
    /// a json:api compliant meta object.
    /// </summary>
    public interface IDomLink : IDomNode
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        Link GeLink();
        #endregion
    }
}