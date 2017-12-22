// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents a collection of complex/resource type configuration objects.
    /// </summary>
    public interface IConfigurationCollection
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Adds a complex type configuration object to this configuration collection.
        /// </summary>
        /// <typeparam name="TComplex">The type of complex object that is part of the service model.</typeparam>
        /// <param name="complexTypeConfiguration">The complex type configuration object to add to this configuration collection.</param>
        void Add<TComplex>(ComplexTypeConfiguration<TComplex> complexTypeConfiguration);

        /// <summary>
        /// Adds a resource type configuration object to this configuration collection.
        /// </summary>
        /// <typeparam name="TResource">The type of resource object that is part of the service model.</typeparam>
        /// <param name="resourceTypeConfiguration">The resource type configuration object to add to this configuration collection.</param>
        void Add<TResource>(ResourceTypeConfiguration<TResource> resourceTypeConfiguration);
        #endregion
    }
}