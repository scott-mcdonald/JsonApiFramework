// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents the root document node
    /// in the DOM document tree.
    /// </summary>
    /// <remarks> 
    /// A document MUST contain at least one of the following members:
    /// - data: the document primary data
    /// - errors: an array of error objects
    /// - meta: a meta object that contains non-standard meta-information
    ///
    /// The members data and errors must not coexist in the same document.
    /// </remarks>
    /// <see cref="http://jsonapi.org"/>
    public interface IDomDocument : IDomObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the json:api document type of this DOM document.</summary>
        DocumentType ApiDocumentType { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the document
        /// json:api version object of this json:api document.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. version is optional.
        /// </remarks>
        IDomProperty DomJsonApiVersion { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the document
        /// json:api meta object of this json:api document.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. meta is optional.
        /// </remarks>
        IDomProperty DomMeta { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the document
        /// json:api links object of this json:api document.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. links is optional.
        /// </remarks>
        IDomProperty DomLinks { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the document
        /// primary data of this json:api document object. Primary data can
        /// only be 1 of the following:
        /// 1. resource object
        /// 2. resource identifier object
        /// 3. array of resource objects
        /// 4. array of resource identifier objects
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. data is optional.
        /// </remarks>
        IDomProperty DomData { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the document
        /// included data of this json:api document object. Included data can
        /// only be an array of resource objects when the primary data is a
        /// resource object or array of resource objects.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. included is optional.
        /// </remarks>
        IDomProperty DomIncluded { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the document
        /// errors array of this json:api document object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. errors is optional.
        /// </remarks>
        IDomProperty DomErrors { get; }
        #endregion
    }
}