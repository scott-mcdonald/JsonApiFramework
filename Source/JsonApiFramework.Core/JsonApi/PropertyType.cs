// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents the property type for a json:api compliant property contained
    /// by a json:api compliant object.
    /// </summary>
    public enum PropertyType
    {
        None,

        Attributes,
        Code,
        Data,
        Detail,
        Error,
        Errors,
        HRef,
        Id,
        Included,
        JsonApi,
        Link,
        Links,
        Meta,
        Relationship,
        Relationships,
        Source,
        Status,
        Title,
        Type,
        Version,
    };
}