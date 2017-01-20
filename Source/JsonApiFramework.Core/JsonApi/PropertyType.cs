// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents the property type for a json:api compliant property.
    /// </summary>
    public enum PropertyType
    {
        None,
        JsonApi,
        Version,
        Meta,
        Links,
        Link,
        HRef,
        Data,
        Resource,
        Type,
        Id,
        Attributes,
        Relationships,
        Relationship,
        ResourceIdentifier,
        Included,
        Errors,
        Error,
    };
}