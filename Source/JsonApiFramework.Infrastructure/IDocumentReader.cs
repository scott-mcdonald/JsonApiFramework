// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework
{
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
        /// <typeparam name="TResource">Type of related CLR resource to get.</typeparam>
        /// <param name="relationship">The "to-one" relationship object that defines the
        /// single related CLR resource by json:api resource linkage.</param>
        /// <returns>Returns the single related CLR resource referenced by the "to-one"
        /// relationship if it exists, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if the relationship object
        /// passed is a "to-many" relationship instead of a "to-one" relationship.</exception>
        TResource GetRelatedToOneResource<TResource>(Relationship relationship)
            where TResource : class, IResource;

        /// <summary>
        /// Gets the multiple related CLR resources referenced by the "to-many" relationship.
        /// </summary>
        /// <typeparam name="TResource">Type of related CLR resource to get.</typeparam>
        /// <param name="relationship">The "to-many" relationship object that defines the
        /// multiple related CLR resources by json:api resource linkage.</param>
        /// <returns>Returns the LINQ-to-objects collection of related CLR resources
        /// referenced by the "to-many" relationship if they exist, empty collection otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if the relationship object
        /// passed is a "to-one" relationship instead of a "to-many" relationship.</exception>
        IEnumerable<TResource> GetRelatedToManyResourceCollection<TResource>(Relationship relationship)
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single CLR resource for the given CLR resource type.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource to get.</typeparam>
        /// <returns>Returns the single CLR resource if it exists, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources for the given CLR resource type.</exception>
        TResource GetResource<TResource>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single CLR resource by resource identifier lookup.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource to find.</typeparam>
        /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
        /// <param name="clrResourceId">CLR resource identifier value to lookup the single CLR resource with.</param>
        /// <returns>Returns the single CLR resource if found, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
        TResource GetResource<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource;

        /// <summary>
        /// Gets the multiple CLR resources for the given CLR resource type.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource to get.</typeparam>
        /// <returns>Returns the LINQ-to-objects collection of CLR resources if they exist,
        /// empty collection otherwise.</returns>
        IEnumerable<TResource> GetResourceCollection<TResource>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single CLR resource identifier for the given CLR resource type.
        /// </summary>
        /// <remarks>
        /// The CLR based resource identifier can come from either a json:api resource object or
        /// a resource identifier.
        /// </remarks>
        /// <typeparam name="TResource">Type of CLR resource to find.</typeparam>
        /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
        /// <returns>Returns the single CLR resource identifier if it exists, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resource identifiers for the given CLR resource type.</exception>
        TResourceId GetResourceId<TResource, TResourceId>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets the multiple CLR resource identifiers for the given CLR resource type.
        /// </summary>
        /// <remarks>
        /// The CLR based resource identifiers can come from either json:api resource objects or
        /// resource identifiers.
        /// </remarks>
        /// <typeparam name="TResource">Type of CLR resource to find.</typeparam>
        /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
        /// <returns>Returns the LINQ-to-objects collection of CLR resource identifiers if they exist,
        /// empty collection otherwise.</returns>
        IEnumerable<TResourceId> GetResourceIdCollection<TResource, TResourceId>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single resource or resource identifier json:api meta object for the given CLR resource type.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource or resource identifier to get meta for.</typeparam>
        /// <returns>Returns the single <c>Meta</c> object if the CLR resource or resource identifier exists, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources or resource identifiers for the given CLR resource type.</exception>
        Meta GetResourceMeta<TResource>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single resource or resource identifier json:api meta object by resource lookup.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource.</typeparam>
        /// <param name="clrResource">CLR resource to lookup the single json:api meta object with.</param>
        /// <returns>Returns the single json:api meta object if found, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources or resource identifiers with the same resource identifier for the given CLR resource type.</exception>
        Meta GetResourceMeta<TResource>(TResource clrResource)
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single resource or resource identifier json:api meta object by resource identifier lookup.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource.</typeparam>
        /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
        /// <param name="clrResourceId">CLR resource identifier value to lookup the single json:api meta object with.</param>
        /// <returns>Returns the single json:api meta object if found, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources or resource identifiers with the same resource identifier for the given CLR resource type.</exception>
        Meta GetResourceMeta<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource;

        /// <summary>
        /// Gets all resource or resource identifier json:api meta objects for the given CLR resource type.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource or resource identifier to get meta collection for.</typeparam>
        /// <returns>Collection of <c>Meta</c> objects in document order.</returns>
        IEnumerable<Meta> GetResourceMetaCollection<TResource>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single resource json:api links object for the given CLR resource type.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource or resource identifier to get links for.</typeparam>
        /// <returns>Returns the single <c>Meta</c> object if the CLR resource exists, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources for the given CLR resource type.</exception>
        Links GetResourceLinks<TResource>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single resource json:api links object by resource lookup.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource.</typeparam>
        /// <param name="clrResource">CLR resource to lookup the single json:api links object with.</param>
        /// <returns>Returns the single json:api links object if found, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
        Links GetResourceLinks<TResource>(TResource clrResource)
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single resource json:api links object by resource identifier lookup.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource.</typeparam>
        /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
        /// <param name="clrResourceId">CLR resource identifier value to lookup the single json:api links object with.</param>
        /// <returns>Returns the single json:api links object if found, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
        Links GetResourceLinks<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource;

        /// <summary>
        /// Gets all resource json:api links objects for the given CLR resource type.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource or resource identifier to get links collection for.</typeparam>
        /// <returns>Collection of <c>Links</c> objects in document order.</returns>
        IEnumerable<Links> GetResourceLinksCollection<TResource>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single resource json:api relationships object for the given CLR resource type.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource or resource identifier to get relationships for.</typeparam>
        /// <returns>Returns the single <c>Meta</c> object if the CLR resource exists, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources for the given CLR resource type.</exception>
        Relationships GetResourceRelationships<TResource>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single resource json:api relationships object by resource lookup.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource.</typeparam>
        /// <param name="clrResource">CLR resource to lookup the single json:api relationships object with.</param>
        /// <returns>Returns the single json:api relationships object if found, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
        Relationships GetResourceRelationships<TResource>(TResource clrResource)
            where TResource : class, IResource;

        /// <summary>
        /// Gets the single resource json:api relationships object by resource identifier lookup.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource.</typeparam>
        /// <typeparam name="TResourceId">Type of CLR resource identifier.</typeparam>
        /// <param name="clrResourceId">CLR resource identifier value to lookup the single json:api relationships object with.</param>
        /// <returns>Returns the single json:api relationships object if found, null otherwise.</returns>
        /// <exception cref="DocumentReadException">Is thrown if there are
        /// multiple CLR resources with the same resource identifier for the given CLR resource type.</exception>
        Relationships GetResourceRelationships<TResource, TResourceId>(TResourceId clrResourceId)
            where TResource : class, IResource;

        /// <summary>
        /// Gets all resource json:api relationships objects for the given CLR resource type.
        /// </summary>
        /// <typeparam name="TResource">Type of CLR resource or resource identifier to get relationships collection for.</typeparam>
        /// <returns>Collection of <c>Relationships</c> objects in document order.</returns>
        IEnumerable<Relationships> GetResourceRelationshipsCollection<TResource>()
            where TResource : class, IResource;

        /// <summary>
        /// Gets all CLR resource types in the document.
        /// </summary>
        /// <returns>Returns the LINQ-to-objects collection of all CLR resource
        /// types in the document.</returns>
        IEnumerable<Type> GetResourceTypes();

        /// <summary>
        /// Gets the json:api errors object which is a collection of json:api compliant error objects.
        /// </summary>
        /// <remarks>
        /// This API is only applicable for json:api error documents.
        /// </remarks>
        /// <returns>Collection of <c>Error</c> objects in document order.</returns>
        IEnumerable<Error> GetErrors();
        #endregion
    }
}
