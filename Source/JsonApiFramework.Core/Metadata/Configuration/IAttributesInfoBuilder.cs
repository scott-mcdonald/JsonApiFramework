// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Linq.Expressions;

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents a fluent-style builder of attributes for complex/resource types.
    /// </summary>
    /// <typeparam name="TObject">The type of complex/resource object to build metadata about.</typeparam>
    public interface IAttributesInfoBuilder<TObject>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Adds metadata for an individual attribute to the complex/resource type.
        /// </summary>
        /// <typeparam name="TProperty">The type of attribute property on the complex/resource type.</typeparam>
        /// <param name="clrPropertySelector">Expression that selects the property on the complex/resource type.</param>
        /// <returns>A fluent-style attributes builder to add more metadata for other individual attributes on the complex/resource type.</returns>
        IAttributesInfoBuilder<TObject> Attribute<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector);

        /// <summary>
        /// Adds metadata for an individual attribute to the complex/resource type with optional configuration.
        /// </summary>
        /// <typeparam name="TProperty">The type of attribute property on the complex/resource type.</typeparam>
        /// <param name="clrPropertySelector">Expression that selects the property on the complex/resource type.</param>
        /// <param name="configuration">Optional attribute configuration, can be null.</param>
        /// <returns>A fluent-style attributes builder to add more metadata for other individual attributes on the complex/resource type.</returns>
        IAttributesInfoBuilder<TObject> Attribute<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector,
                                                             Func<IAttributeInfoBuilder<TObject>, IAttributeInfoBuilder<TObject>> configuration);
        #endregion
    }
}