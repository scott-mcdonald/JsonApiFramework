﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.JsonApi;

namespace JsonApiFramework;

/// <summary>
/// Abstracts reading or querying a json:api compliant document.
/// </summary>
/// <remarks>
/// Resources are read from both the primary <c>data</c> and <c>included</c>
/// resource sections of a json:api compliant document.
/// </remarks>
public interface IDocumentReader
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Methods
    /// <summary>
    /// Gets the document-level json-api version object.
    /// </summary>
    /// <returns>Returns the document-level json:api version object if it exists, null otherwise.</returns>
    JsonApiVersion GetJsonApiVersion();

    /// <summary>
    /// Gets the json:api document type.
    /// </summary>
    /// <returns>Document type enumeration.</returns>
    DocumentType GetDocumentType();

    /// <summary>
    /// Gets the document-level meta object.
    /// </summary>
    /// <returns>Returns the document-level meta object if it exists, null otherwise.</returns>
    Meta GetDocumentMeta();

    /// <summary>
    /// Gets the document-level links object.
    /// </summary>
    /// <returns>Returns the document-level links object if it exists, null otherwise.</returns>
    Links GetDocumentLinks();

    /// <summary>
    /// Gets the single related CLR resource referenced by the "to-one" relationship.
    /// </summary>
    /// <param name="clrRelatedResourceType">Type of CLR related resource to get.</param>
    /// <param name="relationship">The "to-one" relationship object that defines the
    /// single related CLR resource by json:api resource linkage.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the single related CLR resource referenced by the "to-one"
    /// relationship if it exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if the relationship object
    /// passed is a "to-many" relationship instead of a "to-one" relationship.</exception>
    object GetRelatedResource(Type clrRelatedResourceType, Relationship relationship, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the multiple related CLR resources referenced by the "to-many" relationship.
    /// </summary>
    /// <param name="clrRelatedResourceType">Type of CLR related resource to get.</param>
    /// <param name="relationship">The "to-many" relationship object that defines the
    /// multiple related CLR resources by json:api resource linkage.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the LINQ-to-objects collection of related CLR resources
    /// referenced by the "to-many" relationship if they exist, empty collection otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if the relationship object
    /// passed is a "to-one" relationship instead of a "to-many" relationship.</exception>
    IEnumerable<object> GetRelatedResourceCollection(Type clrRelatedResourceType, Relationship relationship, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single CLR resource for the given CLR resource type.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource to get.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the single CLR resource if it exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources for the given CLR resource type.</exception>
    object GetResource(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single CLR resource by resource identifier lookup.
    /// </summary>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <param name="clrResourceType">Type of CLR resource to find.</param>
    /// <param name="clrResourceId">CLR resource identifier value to lookup the single CLR resource with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the single CLR resource if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
    object GetResource<TResourceId>(Type clrResourceType, TResourceId clrResourceId, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the multiple CLR resources for the given CLR resource type.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource to get.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the LINQ-to-objects collection of CLR resources if they exist,
    /// empty collection otherwise.</returns>
    IEnumerable<object> GetResourceCollection(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single CLR resource identifier for the given CLR resource type.
    /// </summary>
    /// <remarks>
    /// The CLR based resource identifier can come from either a json:api resource object or
    /// a resource identifier.
    /// </remarks>
    /// <param name="clrResourceType">Type of CLR resource that contains the CLR resource identifier.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the single CLR resource identifier if it exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resource identifiers for the given CLR resource type.</exception>
    TResourceId GetResourceId<TResourceId>(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the multiple CLR resource identifiers for the given CLR resource type.
    /// </summary>
    /// <remarks>
    /// The CLR based resource identifiers can come from either json:api resource objects or
    /// resource identifiers.
    /// </remarks>
    /// <param name="clrResourceType">Type of CLR resource that contains the CLR resource identifier.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the LINQ-to-objects collection of CLR resource identifiers if they exist,
    /// empty collection otherwise.</returns>
    IEnumerable<TResourceId> GetResourceIdCollection<TResourceId>(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single resource or resource identifier json:api meta object for the given CLR resource type.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource that has meta associated with it.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the single <c>Meta</c> object if the CLR resource or resource identifier exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources or resource identifiers for the given CLR resource type.</exception>
    Meta GetResourceMeta(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single resource or resource identifier json:api meta object by resource lookup.
    /// </summary>
    /// <param name="clrResource">CLR resource to lookup the single json:api meta object with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the single json:api meta object if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources or resource identifiers with the same resource identifier for the given CLR resource type.</exception>
    Meta GetResourceMeta(object clrResource, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single resource or resource identifier json:api meta object by resource identifier lookup.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource that has meta associated with it.</param>
    /// <param name="clrResourceId">CLR resource identifier value to lookup the single json:api meta object with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the single json:api meta object if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources or resource identifiers with the same resource identifier for the given CLR resource type.</exception>
    Meta GetResourceMeta<TResourceId>(Type clrResourceType, TResourceId clrResourceId, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets all resource or resource identifier json:api meta objects for the given CLR resource type.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource that has meta associated with it.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Collection of <c>Meta</c> objects in document order.</returns>
    IEnumerable<Meta> GetResourceMetaCollection(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single resource json:api links object for the given CLR resource type.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource that has links associated with it.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the single <c>Meta</c> object if the CLR resource exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources for the given CLR resource type.</exception>
    Links GetResourceLinks(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single resource json:api links object by resource lookup.
    /// </summary>
    /// <param name="clrResource">CLR resource to lookup the single json:api links object with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the single json:api links object if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
    Links GetResourceLinks(object clrResource, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single resource json:api links object by resource identifier lookup.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource that has links associated with it.</param>
    /// <param name="clrResourceId">CLR resource identifier value to lookup the single json:api links object with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the single json:api links object if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
    Links GetResourceLinks<TResourceId>(Type clrResourceType, TResourceId clrResourceId, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets all resource json:api links objects for the given CLR resource type.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource that has links associated with it.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Collection of <c>Links</c> objects in document order.</returns>
    IEnumerable<Links> GetResourceLinksCollection(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single resource json:api relationships object for the given CLR resource type.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource that has relationships associated with it.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the single <c>Meta</c> object if the CLR resource exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources for the given CLR resource type.</exception>
    Relationships GetResourceRelationships(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single resource json:api relationships object by resource lookup.
    /// </summary>
    /// <param name="clrResource">CLR resource to lookup the single json:api relationships object with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the single json:api relationships object if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
    Relationships GetResourceRelationships(object clrResource, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the single resource json:api relationships object by resource identifier lookup.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource that has relationships associated with it.</param>
    /// <param name="clrResourceId">CLR resource identifier value to lookup the single json:api relationships object with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the single json:api relationships object if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
    Relationships GetResourceRelationships<TResourceId>(Type clrResourceType, TResourceId clrResourceId, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets all resource json:api relationships objects for the given CLR resource type.
    /// </summary>
    /// <param name="clrResourceType">Type of CLR resource that has relationships associated with it.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Collection of <c>Relationships</c> objects in document order.</returns>
    IEnumerable<Relationships> GetResourceRelationshipsCollection(Type clrResourceType, bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets all CLR resource types in the document.
    /// </summary>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <returns>Returns the LINQ-to-objects collection of all CLR resource
    /// types in the document.</returns>
    IEnumerable<Type> GetResourceTypeCollection(bool enumerateIncludedResources = true);

    /// <summary>
    /// Gets the json:api errors object which is a collection of json:api compliant error objects.
    /// </summary>
    /// <remarks>
    /// This API is only applicable for json:api error documents.
    /// </remarks>
    /// <returns>Collection of <c>Error</c> objects in document order.</returns>
    IEnumerable<Error> GetErrorCollection();
    #endregion
}
