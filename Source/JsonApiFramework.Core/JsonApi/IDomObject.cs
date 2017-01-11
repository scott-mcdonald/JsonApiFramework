// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

namespace JsonApiFramework.JsonApi
{
    public interface IDomObject : IDomNode
        , IEnumerable<IDomAttribute>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        Type GetClrType();
        #endregion
    }
}