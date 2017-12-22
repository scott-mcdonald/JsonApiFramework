// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents a fluent-style builder of an individual relationship for a resource type.
    /// </summary>
    /// <typeparam name="TResource">The type of resource object to build metadata about.</typeparam>
    public interface IRelationshipInfoBuilder<TResource>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Sets the json:api rel of the relationship. This overrides any json:api rel set by default or convention.
        /// </summary>
        /// <remarks>
        /// By default, the json:api rel is the CLR type name of the related resource. Can also be set by conventions.
        /// </remarks>
        /// <param name="apiRel">Name to build the json:api relationship rel with.</param>
        /// <returns>A fluent-style builder to add more configuration for the relationship.</returns>
        IRelationshipInfoBuilder<TResource> SetApiRel(string apiRel);
        #endregion
    }
}