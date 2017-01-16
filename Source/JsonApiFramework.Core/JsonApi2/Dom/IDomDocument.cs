// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents the root document node
    /// in the DOM document tree.
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
    public interface IDomDocument : IDomObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the document type of this DOM document.</summary>
        ApiDocumentType ApiDocumentType { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// version object of this json:api document.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. version is optional.
        /// </remarks>
        IDomProperty DomJsonApiVersion { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// meta object of this json:api document.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. meta is optional.
        /// </remarks>
        IDomProperty DomMeta { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// links object of this json:api document.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. links is optional.
        /// </remarks>
        IDomProperty DomLinks { get; }
        #endregion
    }
}