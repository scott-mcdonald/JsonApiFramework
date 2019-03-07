// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

namespace JsonApiFramework.Server
{
    public interface IToManyIncludedResources
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type   FromResourceType { get; }
        object FromResource     { get; }
        string FromRel          { get; }

        Type                ToResourceType       { get; }
        IEnumerable<object> ToResourceCollection { get; }
        #endregion
    }

    public interface IToManyIncludedResources<out TFromResource, out TToResource>
        where TFromResource : class
        where TToResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Type          FromResourceType { get; }
        TFromResource FromResource     { get; }
        string        FromRel          { get; }

        Type                     ToResourceType       { get; }
        IEnumerable<TToResource> ToResourceCollection { get; }
        #endregion
    }
}