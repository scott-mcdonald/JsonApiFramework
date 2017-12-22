// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents a fluent-style builder of an individual attribute for a complex/resource type.
    /// </summary>
    /// <typeparam name="TObject">The type of complex/resource object to build metadata about.</typeparam>
    public interface IAttributeInfoBuilder<TObject>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Sets the json:api name of the attribute. This overrides any name set by default or convention.
        /// </summary>
        /// <remarks>
        /// By default, the json:api name is the CLR property name. Can also be set by conventions.
        /// </remarks>
        /// <param name="apiAttributeName">Name to build the json:api attribute name with.</param>
        /// <returns>A fluent-style builder to add more configuration for the attribute.</returns>
        IAttributeInfoBuilder<TObject> SetApiAttributeName(string apiAttributeName);

        /// <summary>
        /// Explicitly includes the attribute if it was excluded by default or convention.
        /// </summary>
        /// <returns>A fluent-style builder to add more configuration for the attribute.</returns>
        IAttributeInfoBuilder<TObject> Include();

        /// <summary>
        /// Explicitly excludes the attribute if it was included by default or convention.
        /// </summary>
        /// <returns>A fluent-style builder to add more configuration for the attribute.</returns>
        IAttributeInfoBuilder<TObject> Exclude();
        #endregion
    }
}