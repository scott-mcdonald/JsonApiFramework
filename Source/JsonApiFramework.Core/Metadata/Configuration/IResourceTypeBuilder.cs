// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Linq.Expressions;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Metadata.Configuration
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a fluent-style builder of resource types.
    /// </summary>
    /// <typeparam name="TResource">The type of resource object to build metadata about.</typeparam>
    public interface IResourceTypeBuilder<TResource> : ITypeBaseBuilder<TResource>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Sets the resource identity of the resource type.
        /// </summary>
        /// <typeparam name="TProperty">The type of resource identifier property on the resource type.</typeparam>
        /// <param name="clrIdPropertySelector">Expression that selects the resource identifier property on the resource type.</param>
        void ResourceIdentity<TProperty>(Expression<Func<TResource, TProperty>> clrIdPropertySelector);

        /// <summary>
        /// Sets the resource identity of the resource type with optional configuration.
        /// </summary>
        /// <typeparam name="TProperty">The type of resource identifier property on the resource type.</typeparam>
        /// <param name="clrIdPropertySelector">Expression that selects the resource identifier property on the resource type.</param>
        /// <param name="configuration">Optional resource identity configuration, can be null.</param>
        void ResourceIdentity<TProperty>(Expression<Func<TResource, TProperty>> clrIdPropertySelector,
                                         Func<IResourceIdentityInfoBuilder<TResource>, IResourceIdentityInfoBuilder<TResource>> configuration);

        /// <summary>
        /// Sets the relationships as a whole of the resource type.
        /// </summary>
        /// <remarks>
        /// This version is for resource types that have no <see cref="JsonApiFramework.JsonApi.Relationships"/> property.
        /// </remarks>
        /// <returns>A fluent-style relationships builder for the resource type.</returns>
        IRelationshipsInfoBuilder<TResource> Relationships();

        /// <summary>
        /// Sets the relationships as a whole of the resource type to build with a <see cref="JsonApiFramework.JsonApi.Relationships"/> property on the resource type.
        /// </summary>
        /// <remarks>
        /// This version is for resource types that have a <see cref="JsonApiFramework.JsonApi.Relationships"/> property.
        /// </remarks>
        /// <param name="clrRelationshipsPropertySelector">Expression that selects the <c>Relationships</c> property on the resource type.</param>
        /// <returns>A fluent-style relationships builder for the resource type.</returns>
        IRelationshipsInfoBuilder<TResource> Relationships(Expression<Func<TResource, Relationships>> clrRelationshipsPropertySelector);

        /// <summary>
        /// Sets the relationships as a whole of the resource type to build with a <see cref="JsonApiFramework.JsonApi.Links"/> property on the resource type.
        /// <remarks>
        /// This version is for resource types that have a <see cref="JsonApiFramework.JsonApi.Links"/> property.
        /// </remarks>
        /// </summary>
        /// <param name="clrLinksPropertySelector">Expression that selects the <c>Links</c> property on the resource type.</param>
        void Links(Expression<Func<TResource, Links>> clrLinksPropertySelector);
        #endregion
    }
}