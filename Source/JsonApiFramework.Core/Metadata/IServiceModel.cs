// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.Metadata
{
    /// <summary>
    /// Represents the metadata for the domain model of resources produced or consumed by a hypermedia API.
    /// </summary>
    /// <remarks>
    /// The service model at a high level:
    /// <list type="bullet">
    /// <item><description>Defines the complex and resource types that makeup the domain model of a json:api compliant hypermedia API.</description></item>
    /// <item><description>Defines the relationships between resources.</description></item>
    /// <item><description>Defines resource identity for CLR and json:api resources for referential equality purposes.</description></item>
    /// </list>
    /// </remarks>
    [JsonConverter(typeof(ServiceModelConverter))]
    public interface IServiceModel : IJsonObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the name of the service model.</summary>
        string Name { get; }

        /// <summary>Gets the collection of all complex types in the service model.</summary>
        IReadOnlyCollection<IComplexType> ComplexTypes { get; }

        /// <summary>Gets the collection of all resource types in the service model.</summary>
        IReadOnlyCollection<IResourceType> ResourceTypes { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Creates a CLR complex object.
        /// </summary>
        /// <typeparam name="TComplex">CLR typeparameter of CLR complex object to create.</typeparam>
        /// <returns>A newly created complex object.</returns>
        TComplex CreateClrComplexObject<TComplex>();

        /// <summary>
        /// Creates a CLR resource object.
        /// </summary>
        /// <typeparam name="TResource">CLR typeparameter of CLR resource object to create.</typeparam>
        /// <returns>A newly created resource object.</returns>
        TResource CreateClrResourceObject<TResource>();

        /// <summary>Try and get the complex type by CLR type.</summary>
        /// <param name="clrComplexType">CLR type to lookup the complex type by.</param>
        /// <param name="complexType">The complex type if it exists in the service model, null otherwise.</param>
        /// <returns>True if complex type exists in the service model, False otherwise.</returns>
        bool TryGetComplexType(Type clrComplexType, out IComplexType complexType);

        /// <summary>Try and get the resource type by json:api type.</summary>
        /// <param name="apiResourceType">json:api type string to lookup the resource type by.</param>
        /// <param name="resourceType">The resource type if it exists in the service model, null otherwise.</param>
        /// <returns>True if resource type exists in the service model, False otherwise.</returns>
        bool TryGetResourceType(string apiResourceType, out IResourceType resourceType);

        /// <summary>Try and get the resource type by CLR type.</summary>
        /// <param name="clrResourceType">CLR type to lookup the resource type by.</param>
        /// <param name="resourceType">The resource type if it exists in the service model, null otherwise.</param>
        /// <returns>True if resource type exists in the service model, False otherwise.</returns>
        bool TryGetResourceType(Type clrResourceType, out IResourceType resourceType);
        #endregion
    }
}