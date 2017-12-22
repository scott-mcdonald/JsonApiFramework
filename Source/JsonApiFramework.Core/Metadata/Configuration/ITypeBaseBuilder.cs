// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents a fluent-style builder for the commonality of building complex and resource types.
    /// </summary>
    /// <typeparam name="TObject">The type of complex/resource object to build metadata about.</typeparam>
    public interface ITypeBaseBuilder<TObject>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Sets the attributes as a whole of the complex/resource type to build.
        /// </summary>
        /// <returns>A fluent-style attributes builder for the complex/resource type.</returns>
        IAttributesInfoBuilder<TObject> Attributes();
        #endregion
    }
}