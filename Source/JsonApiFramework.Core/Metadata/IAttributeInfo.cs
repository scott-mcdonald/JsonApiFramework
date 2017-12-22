// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata
{
    /// <summary>Represents the metadata for an individual 'attribute/property' on a json:api/CLR complex/resource type.</summary>
    public interface IAttributeInfo
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the json:api attribute name.</summary>
        string ApiAttributeName { get; }

        /// <summary>Gets the CLR property binding object.</summary>
        IClrPropertyBinding ClrPropertyBinding { get; }
        #endregion
    }
}