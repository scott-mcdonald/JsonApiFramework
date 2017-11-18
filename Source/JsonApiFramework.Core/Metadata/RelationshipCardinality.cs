// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the relationship cardinality 'from' a resource type 'to' the related resource type.</summary>
    public enum RelationshipCardinality
    {
        /// <summary>Represents a relationship from one resource type to one related resource.</summary>
        ToOne = 1,

        /// <summary>Represents a relationship from one resource type to many related resources.</summary>
        ToMany = 2
    }
}