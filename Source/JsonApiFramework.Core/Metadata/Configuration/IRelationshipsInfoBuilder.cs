// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Linq.Expressions;

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents a fluent-style builder of relationships for a resource type.
    /// </summary>
    /// <typeparam name="TResource">The type of resource object to build metadata about.</typeparam>
    public interface IRelationshipsInfoBuilder<TResource>
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Adds metadata for an individual relationship to the resource type.
        /// </summary>
        /// <remarks>
        /// This version is for resource types that have the related resource or collection of related resources as a property.
        /// The json:api cardinality will be derived by the property type, if it is a collection then it is a to-many relationship otherwise it is a to-one relationship.
        /// </remarks>
        /// <typeparam name="TProperty">The type of related resource(s) property on the resource type.</typeparam>
        /// <param name="clrRelatedResourcePropertySelector">Expression that selects the related resource(s) property on the resource type.</param>
        /// <returns>A fluent-style relationships builder to add more metadata for other relationships on the resource type.</returns>
        IRelationshipsInfoBuilder<TResource> Relationship<TProperty>(Expression<Func<TResource, TProperty>> clrRelatedResourcePropertySelector);

        /// <summary>
        /// Adds metadata for an individual relationship to the resource type with optional configuration.
        /// </summary>
        /// <remarks>
        /// This version is for resource types that have the related resource or collection of related resources as a property.
        /// The json:api cardinality will be derived by the property type, if it is a collection then it is a to-many relationship otherwise it is a to-one relationship.
        /// </remarks>
        /// <typeparam name="TProperty">The type of related resource(s) property on the resource type.</typeparam>
        /// <param name="clrRelatedResourcePropertySelector">Expression that selects the related resource(s) property on the resource type.</param>
        /// <param name="configuration">Optional relationship configuration, can be null.</param>
        /// <returns>A fluent-style relationships builder to add more metadata for other relationships on the resource type.</returns>
        IRelationshipsInfoBuilder<TResource> Relationship<TProperty>(Expression<Func<TResource, TProperty>> clrRelatedResourcePropertySelector,
                                                                     Func<IRelationshipInfoBuilder<TResource>, IRelationshipInfoBuilder<TResource>> configuration);

        /// <summary>
        /// Adds metadata for an individual to-many relationship to the resource type.
        /// </summary>
        /// <remarks>
        /// This version is for resource types that do not have the collection of related resources as a property.
        /// The json:api cardinality is implied by the name of the method, i.e. to-many relationship.
        /// </remarks>
        /// <typeparam name="TRelatedResource">The type of related resource on the resource type</typeparam>
        /// <returns>A fluent-style relationships builder to add more metadata for other relationships on the resource type.</returns>
        IRelationshipsInfoBuilder<TResource> ToManyRelationship<TRelatedResource>();

        /// <summary>
        /// Adds metadata for an individual to-many relationship to the resource type with optional configuration.
        /// </summary>
        /// <remarks>
        /// This version is for resource types that do not have the collection of related resources as a property.
        /// The json:api cardinality is implied by the name of the method, i.e. to-many relationship.
        /// </remarks>
        /// <typeparam name="TRelatedResource">The type of related resource on the resource type</typeparam>
        /// <param name="configuration">Optional relationship configuration, can be null.</param>
        /// <returns>A fluent-style relationships builder to add more metadata for other relationships on the resource type.</returns>
        IRelationshipsInfoBuilder<TResource> ToManyRelationship<TRelatedResource>(Func<IRelationshipInfoBuilder<TResource>, IRelationshipInfoBuilder<TResource>> configuration);

        /// <summary>
        /// Adds metadata for an individual to-one relationship to the resource type.
        /// </summary>
        /// <remarks>
        /// This version is for resource types that do not have the related resource as a property.
        /// The json:api cardinality is implied by the name of the method, i.e. to-one relationship.
        /// </remarks>
        /// <typeparam name="TRelatedResource">The type of related resource on the resource type</typeparam>
        /// <returns>A fluent-style relationships builder to add more metadata for other relationships on the resource type.</returns>
        IRelationshipsInfoBuilder<TResource> ToOneRelationship<TRelatedResource>();

        /// <summary>
        /// Adds metadata for an individual to-one relationship to the resource type with optional configuration.
        /// </summary>
        /// <remarks>
        /// This version is for resource types that do not have the related resource as a property.
        /// The json:api cardinality is implied by the name of the method, i.e. to-one relationship.
        /// </remarks>
        /// <typeparam name="TRelatedResource">The type of related resource on the resource type</typeparam>
        /// <param name="configuration">Optional relationship configuration, can be null.</param>
        /// <returns>A fluent-style relationships builder to add more metadata for other relationships on the resource type.</returns>
        IRelationshipsInfoBuilder<TResource> ToOneRelationship<TRelatedResource>(Func<IRelationshipInfoBuilder<TResource>, IRelationshipInfoBuilder<TResource>> configuration);
        #endregion
    }
}   