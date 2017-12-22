// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>Represents a fluent-style builder of service models.</summary>
    public interface IServiceModelBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Sets the name of the service model to build.
        /// </summary>
        /// <param name="name">Name to build the service model with.</param>
        void SetName(string name);

        /// <summary>
        /// Adds metadata for a complex type to the service model.
        /// </summary>
        /// <typeparam name="TComplex">The type of complex object that is part of the service model.</typeparam>
        /// <returns>A fluent-style complex type builder for the complex type.</returns>
        IComplexTypeBuilder<TComplex> ComplexType<TComplex>();

        /// <summary>
        /// Adds metadata for a resource type to the service model.
        /// </summary>
        /// <typeparam name="TResource">The type of resource object that is part of the service model.</typeparam>
        /// <returns>A fluent-style resource type builder for the resource type.</returns>
        IResourceTypeBuilder<TResource> ResourceType<TResource>();
        #endregion
    }
}