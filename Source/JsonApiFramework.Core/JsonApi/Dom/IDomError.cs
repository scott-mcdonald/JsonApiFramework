// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents a json:api error object.
    /// </summary>
    public interface IDomError : IDomObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>
        /// Gets the read-only DOM property node that represents the "id" of
        /// this json:api error object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, "id" is optional.
        /// </remarks>
        IDomProperty DomId { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// links object of this json:api error object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, links is optional.
        /// </remarks>
        IDomProperty DomLinks { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// status object of this json:api error object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, status is optional.
        /// </remarks>
        IDomProperty DomStatus { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// code object of this json:api error object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, code is optional.
        /// </remarks>
        IDomProperty DomCode { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// title object of this json:api error object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, title is optional.
        /// </remarks>
        IDomProperty DomTitle { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// detail object of this json:api error object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, detail is optional.
        /// </remarks>
        IDomProperty DomDetail { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// source object of this json:api error object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, source is optional.
        /// </remarks>
        IDomProperty DomSource { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// meta object of this json:api error object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, meta is optional.
        /// </remarks>
        IDomProperty DomMeta { get; }
        #endregion
    }
}