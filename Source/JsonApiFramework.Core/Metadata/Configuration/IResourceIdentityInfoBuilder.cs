// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents a fluent-style builder of the resource identity for a resource type.
    /// </summary>
    /// <typeparam name="TResource">The type of resource object to build metadata about.</typeparam>
    public interface IResourceIdentityInfoBuilder<TResource>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Sets the json:api type of the json:api resource identity. This overrides any json:api type set by default or convention.
        /// </summary>
        /// <remarks>
        /// By default, the json:api type is the CLR resource type name. Can also be set by conventions.
        /// </remarks>
        /// <param name="apiType">Name to build the json:api resource identity type with.</param>
        /// <returns>A fluent-style builder to add more configuration for the resource identity.</returns>
        IResourceIdentityInfoBuilder<TResource> SetApiType(string apiType);
        #endregion
    }
}