// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the metadata for an individual 'attribute/property' on a json:api/CLR complex/resource type.</summary>
    public interface IAttributeInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the json:api attribute name.</summary>
        string ApiAttributeName { get; }

        /// <summary>Gets the CLR property name.</summary>
        string ClrPropertyName { get; }

        /// <summary>Gets the CLR property type.</summary>
        Type ClrPropertyType { get; }

        /// <summary>Gets the predicate representing if the json:api/CLR attribute/property is a complex type or not.</summary>
        bool IsComplexType { get; }

        /// <summary>Gets the predicate representing if the json:api/CLR attribute/property is a collection or not.</summary>
        bool IsCollection { get; }

        /// <summary>Gets the CLR collection item type if the json:api/CLR attribute/property is a collection, null otherwise.</summary>
        Type ClrCollectionItemType { get; }

        /// <summary>Gets the predicate representing if the json:api/CLR attribute/property collection item is a complex type or not, null otherwise.</summary>
        bool? IsCollectionItemComplexType { get; }

        /// <summary>Gets the CLR property binding object.</summary>
        IClrPropertyBinding ClrPropertyBinding { get; }
        #endregion
    }
}