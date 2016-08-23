// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.Internal.Dom
{
    internal enum DomNodeType
    {
        Unknown,

        Document,
        JsonApiVersion,
        Meta,
        Links,
        Link,
        HRef,
        Data,
        DataCollection,
        Resource,
        Type,
        Id,
        Attributes,
        Attribute,
        Relationships,
        Relationship,
        ResourceIdentifier,
        Included,
        Errors,
        Error,
    }
}
