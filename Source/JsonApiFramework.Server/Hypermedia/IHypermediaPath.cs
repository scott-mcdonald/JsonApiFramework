// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

using JsonApiFramework.Http;

namespace JsonApiFramework.Server.Hypermedia
{
    public interface IHypermediaPath : IPath
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        HypermediaPathType HypermediaPathType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        Type GetClrResourceType();
        #endregion
    }
}