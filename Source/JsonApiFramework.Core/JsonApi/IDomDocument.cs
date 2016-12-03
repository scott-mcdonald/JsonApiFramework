// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts a json:api compliant document as a 1-n document object
    /// model tree of nodes.
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
        , IGetLinks
        , IGetMeta
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        DocumentType GetDocumentType();
        #endregion
    }
}