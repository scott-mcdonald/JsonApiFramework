// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Server
{
    public interface IToManyIncludedResources<out TFromResource, out TToResource>
        where TFromResource : class, IResource
        where TToResource : class, IResource
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        TFromResource FromResource { get; }

        string FromRel { get; }

        IEnumerable<TToResource> ToResourceCollection { get; }
        #endregion
    }
}