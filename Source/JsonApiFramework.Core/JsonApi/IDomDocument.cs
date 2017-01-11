// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts queryable (non-mutating) access to a DOM node representing
    /// a json:api compliant document.
    /// </summary>
    /// <remarks> 
    /// A JSON object MUST be at the root of every JSON API response
    /// containing data. This object defines a document's "top level".
    ///
    /// The document's "primary data" is a representation of the resource,
    /// collection of resources, or resource relationship primarily targeted
    /// by a request.
    ///
    /// A document MUST contain at least one of the following top-level
    /// members:
    /// - primary data
    /// - array of error objects
    /// - meta object
    ///
    /// Primary data MUST appear under a top-level key named "data". Primary
    /// data MUST be either a single resource or resource identifier object,
    /// an array of resource or resource identifier objects.
    /// </remarks>
    /// <see cref="http://jsonapi.org"/>
    public interface IDomDocument : IDomNode
        , IGetJsonApiVersion
        , IGetMeta
        , IGetLinks
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Gets the document type from this DOM document.</summary>
        DocumentType GetDocumentType();

        /// <summary>
        /// Gets the primary data from this DOM document as a single CLR resource.
        /// </summary>
        /// <remarks>
        /// Should only be called for a DOM document representing a resource document,
        /// otherwise an exception is thrown.
        /// </remarks>
        TObject GetResource<TObject>();

        /// <summary>
        /// Gets the primary data from this DOM document as a CLR resource collection.
        /// </summary>
        /// <remarks>
        /// Should only be called for a DOM document representing a resource collection document,
        /// otherwise an exception is thrown.
        /// </remarks>
        IEnumerable<TObject> GetResourceCollection<TObject>();

        /// <summary>
        /// Gets the optional included data from this DOM document as a CLR resource collection.
        /// </summary>
        /// <remarks>
        /// Should only be called for a DOM document representing either a resource docuemnt or resource collection document,
        /// otherwise an exception is thrown.
        /// </remarks>
        IEnumerable<TObject> GetIncludedResources<TObject>();

        /// <summary>
        /// Gets the errors from this DOM document.
        /// </summary>
        /// <remarks>
        /// Should only be called for a DOM document representing an errors document,
        /// otherwise an exception is thrown.
        /// </remarks>
        IEnumerable<Error> GetErrors();
        #endregion
    }
}