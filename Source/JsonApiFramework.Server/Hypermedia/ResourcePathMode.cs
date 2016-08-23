// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.Server.Hypermedia
{
    /// <summary>
    /// Enumeration used by hypermedia assemblers to know whether to include
    /// or ignore the resource identifier when constructing URL paths.
    /// </summary>
    public enum ResourcePathMode
    {
        IncludeApiId,   // All other path construction use cases - default.
        IgnoreApiId     // Used for to-one hierarchical path construction use case only.
    }
}
