// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents the resource-oriented type the primary "data" is for a
    /// json:api compliant document.
    /// </summary>
    public enum DataType
    {
        Unknown,
        Resource,
        ResourceIdentifier
    };
}