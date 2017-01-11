// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts queryable (non-mutating) access to a DOM node representing
    /// an attribute for a json:api resource or complex type.
    /// </summary>
    public interface IDomAttribute : IDomNode
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        string GetApiName();
        string GetClrName();
        Type GetClrType();
        #endregion
    }
}