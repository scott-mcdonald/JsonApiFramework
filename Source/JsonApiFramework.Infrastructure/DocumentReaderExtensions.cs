// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

#pragma warning disable 1573

namespace JsonApiFramework;

public static class DocumentReaderExtensions
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region IDocumentReader Extensions
    /// <summary>
    /// Predicate that returns if the document has primary data of some kind.
    /// </summary>
    /// <returns>True if the document contains primary data, false otherwise.</returns>
    public static bool IsDataDocument(this IDocumentReader documentReader)
    {
        Contract.Requires(documentReader != null);

        var documentType = documentReader.GetDocumentType();
        return documentType.IsDataDocument();
    }

    /// <summary>
    /// Predicate that returns if the document has error objects instead of primary data or meta data.
    /// </summary>
    /// <returns>True if the document contains error objects, false otherwise.</returns>
    public static bool IsErrorsDocument(this IDocumentReader documentReader)
    {
        Contract.Requires(documentReader != null);

        var documentType = documentReader.GetDocumentType();
        return documentType.IsErrorsDocument();
    }

    /// <summary>
    /// Predicate that returns if the document has only meta data.
    /// </summary>
    /// <returns>True if the document contains only meta data, false otherwise.</returns>
    public static bool IsMetaOnlyDocument(this IDocumentReader documentReader)
    {
        Contract.Requires(documentReader != null);

        var documentType = documentReader.GetDocumentType();
        var documentMeta = documentReader.GetDocumentMeta();
        return documentType.IsMetaOnlyDocument(documentMeta);
    }

    /// <summary>
    /// Predicate that returns if the document is a valid json:api document overall.
    /// </summary>
    /// <returns>True if the document is a valid json:api document, false otherwise.</returns>
    public static bool IsValidDocument(this IDocumentReader documentReader)
    {
        Contract.Requires(documentReader != null);

        var documentType = documentReader.GetDocumentType();
        var documentMeta = documentReader.GetDocumentMeta();
        return documentType.IsValidDocument(documentMeta);
    }

    /// <summary>
    /// Gets the single related CLR resource referenced by the "to-one" relationship.
    /// </summary>
    /// <param name="relationship">The "to-one" relationship object that defines the
    /// single related CLR resource by json:api resource linkage.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of related CLR resource to get.</typeparam>
    /// <returns>Returns the single related CLR resource referenced by the "to-one"
    /// relationship if it exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if the relationship object
    /// passed is a "to-many" relationship instead of a "to-one" relationship.</exception>
    public static TResource GetRelatedResource<TResource>(this IDocumentReader documentReader, Relationship relationship, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);
        Contract.Requires(relationship != null);

        var clrRelatedResourceType = typeof(TResource);
        var clrRelatedResource = (TResource)documentReader.GetRelatedResource(clrRelatedResourceType, relationship, enumerateIncludedResources);
        return clrRelatedResource;
    }

    /// <summary>
    /// Gets the multiple related CLR resources referenced by the "to-many" relationship.
    /// </summary>
    /// <param name="relationship">The "to-many" relationship object that defines the
    /// multiple related CLR resources by json:api resource linkage.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of related CLR resource to get.</typeparam>
    /// <returns>Returns the LINQ-to-objects collection of related CLR resources
    /// referenced by the "to-many" relationship if they exist, empty collection otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if the relationship object
    /// passed is a "to-one" relationship instead of a "to-many" relationship.</exception>
    public static IEnumerable<TResource> GetRelatedResourceCollection<TResource>(this IDocumentReader documentReader, Relationship relationship, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);
        Contract.Requires(relationship != null);

        var clrRelatedResourceType = typeof(TResource);
        var clrRelatedResourceCollection = documentReader
            .GetRelatedResourceCollection(clrRelatedResourceType, relationship, enumerateIncludedResources)
            .Cast<TResource>();
        return clrRelatedResourceCollection;
    }

    /// <summary>
    /// Gets the single CLR resource for the given CLR resource type.
    /// </summary>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource to get.</typeparam>
    /// <returns>Returns the single CLR resource if it exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources for the given CLR resource type.</exception>
    public static TResource GetResource<TResource>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var clrResource = (TResource)documentReader.GetResource(clrResourceType, enumerateIncludedResources);
        return clrResource;
    }

    /// <summary>
    /// Gets the single CLR resource by resource identifier lookup.
    /// </summary>
    /// <param name="clrResourceId">CLR resource identifier value to lookup the single CLR resource with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource to find.</typeparam>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the single CLR resource if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
    public static TResource GetResource<TResource, TResourceId>(this IDocumentReader documentReader, TResourceId clrResourceId, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var clrResource = (TResource)documentReader.GetResource(clrResourceType, clrResourceId, enumerateIncludedResources);
        return clrResource;
    }

    /// <summary>
    /// Gets the multiple CLR resources for the given CLR resource type.
    /// </summary>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource to get.</typeparam>
    /// <returns>Returns the LINQ-to-objects collection of CLR resources if they exist,
    /// empty collection otherwise.</returns>
    public static IEnumerable<TResource> GetResourceCollection<TResource>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var clrResourceCollection = documentReader.GetResourceCollection(clrResourceType, enumerateIncludedResources).Cast<TResource>();
        return clrResourceCollection;
    }

    /// <summary>
    /// Gets the single CLR resource identifier for the given CLR resource type.
    /// </summary>
    /// <remarks>
    /// The CLR based resource identifier can come from either a json:api resource object or
    /// a resource identifier.
    /// </remarks>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource to find.</typeparam>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the single CLR resource identifier if it exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resource identifiers for the given CLR resource type.</exception>
    public static TResourceId GetResourceId<TResource, TResourceId>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var clrResourceId = documentReader.GetResourceId<TResourceId>(clrResourceType, enumerateIncludedResources);
        return clrResourceId;
    }

    /// <summary>
    /// Gets the multiple CLR resource identifiers for the given CLR resource type.
    /// </summary>
    /// <remarks>
    /// The CLR based resource identifiers can come from either json:api resource objects or
    /// resource identifiers.
    /// </remarks>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource to find.</typeparam>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the LINQ-to-objects collection of CLR resource identifiers if they exist,
    /// empty collection otherwise.</returns>
    public static IEnumerable<TResourceId> GetResourceIdCollection<TResource, TResourceId>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var clrResourceIdCollection = documentReader.GetResourceIdCollection<TResourceId>(clrResourceType, enumerateIncludedResources);
        return clrResourceIdCollection;
    }

    /// <summary>
    /// Gets the single resource or resource identifier json:api meta object for the given CLR resource type.
    /// </summary>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource or resource identifier to get meta for.</typeparam>
    /// <returns>Returns the single <c>Meta</c> object if the CLR resource or resource identifier exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources or resource identifiers for the given CLR resource type.</exception>
    public static Meta GetResourceMeta<TResource>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var apiResourceMeta = documentReader.GetResourceMeta(clrResourceType, enumerateIncludedResources);
        return apiResourceMeta;
    }

    /// <summary>
    /// Gets the single resource or resource identifier json:api meta object by resource identifier lookup.
    /// </summary>
    /// <param name="clrResourceId">CLR resource identifier value to lookup the single json:api meta object with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource.</typeparam>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the single json:api meta object if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources or resource identifiers with the same resource identifier for the given CLR resource type.</exception>
    public static Meta GetResourceMeta<TResource, TResourceId>(this IDocumentReader documentReader, TResourceId clrResourceId, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var apiResourceMeta = documentReader.GetResourceMeta(clrResourceType, clrResourceId, enumerateIncludedResources);
        return apiResourceMeta;
    }

    /// <summary>
    /// Gets all resource or resource identifier json:api meta objects for the given CLR resource type.
    /// </summary>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource or resource identifier to get meta collection for.</typeparam>
    /// <returns>Collection of <c>Meta</c> objects in document order.</returns>
    public static IEnumerable<Meta> GetResourceMetaCollection<TResource>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var apiResourceMetaCollection = documentReader.GetResourceMetaCollection(clrResourceType, enumerateIncludedResources);
        return apiResourceMetaCollection;
    }

    /// <summary>
    /// Gets the single resource json:api links object for the given CLR resource type.
    /// </summary>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource or resource identifier to get links for.</typeparam>
    /// <returns>Returns the single <c>Meta</c> object if the CLR resource exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources for the given CLR resource type.</exception>
    public static Links GetResourceLinks<TResource>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var apiResourceLinks = documentReader.GetResourceLinks(clrResourceType, enumerateIncludedResources);
        return apiResourceLinks;
    }

    /// <summary>
    /// Gets the single resource json:api links object by resource identifier lookup.
    /// </summary>
    /// <param name="clrResourceId">CLR resource identifier value to lookup the single json:api links object with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource.</typeparam>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the single json:api links object if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
    public static Links GetResourceLinks<TResource, TResourceId>(this IDocumentReader documentReader, TResourceId clrResourceId, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var apiResourceLinks = documentReader.GetResourceLinks(clrResourceType, clrResourceId, enumerateIncludedResources);
        return apiResourceLinks;
    }

    /// <summary>
    /// Gets all resource json:api links objects for the given CLR resource type.
    /// </summary>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource or resource identifier to get links collection for.</typeparam>
    /// <returns>Collection of <c>Links</c> objects in document order.</returns>
    public static IEnumerable<Links> GetResourceLinksCollection<TResource>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var apiResourceLinksCollection = documentReader.GetResourceLinksCollection(clrResourceType, enumerateIncludedResources);
        return apiResourceLinksCollection;
    }

    /// <summary>
    /// Gets the single resource json:api relationships object for the given CLR resource type.
    /// </summary>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource or resource identifier to get relationships for.</typeparam>
    /// <returns>Returns the single <c>Meta</c> object if the CLR resource exists, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources for the given CLR resource type.</exception>
    public static Relationships GetResourceRelationships<TResource>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var apiResourceRelationships = documentReader.GetResourceRelationships(clrResourceType, enumerateIncludedResources);
        return apiResourceRelationships;
    }

    /// <summary>
    /// Gets the single resource json:api relationships object by resource identifier lookup.
    /// </summary>
    /// <param name="clrResourceId">CLR resource identifier value to lookup the single json:api relationships object with.</param>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource.</typeparam>
    /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
    /// <returns>Returns the single json:api relationships object if found, null otherwise.</returns>
    /// <exception cref="DocumentReadException">Is thrown if there are
    /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
    public static Relationships GetResourceRelationships<TResource, TResourceId>(this IDocumentReader documentReader, TResourceId clrResourceId, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var apiResourceRelationships = documentReader.GetResourceRelationships(clrResourceType, clrResourceId, enumerateIncludedResources);
        return apiResourceRelationships;
    }

    /// <summary>
    /// Gets all resource json:api relationships objects for the given CLR resource type.
    /// </summary>
    /// <param name="enumerateIncludedResources">True to enumerate both primary and included resources, false to enumerate primary resources only. Defaults to true.</param>
    /// <typeparam name="TResource">Type of CLR resource or resource identifier to get relationships collection for.</typeparam>
    /// <returns>Collection of <c>Relationships</c> objects in document order.</returns>
    public static IEnumerable<Relationships> GetResourceRelationshipsCollection<TResource>(this IDocumentReader documentReader, bool enumerateIncludedResources = true)
        where TResource : class
    {
        Contract.Requires(documentReader != null);

        var clrResourceType = typeof(TResource);
        var apiResourceRelationshipsCollection = documentReader.GetResourceRelationshipsCollection(clrResourceType, enumerateIncludedResources);
        return apiResourceRelationshipsCollection;
    }
    #endregion
}