// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents a json:api resource object.
    /// </summary>
    public interface IDomResource : IDomObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// meta object of this json:api resource object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. meta is optional.
        /// </remarks>
        IDomProperty DomMeta { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the "type" of
        /// this json:api resource object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is required, i.e. "type" is required.
        /// </remarks>
        IDomProperty DomType { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the "id" of
        /// this json:api resource object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is required, i.e. "id" is required.
        /// 
        /// The ONE exception to "id" being required is when the resource
        /// originates at the client and represents a new resource to be
        /// created on the server.
        /// </remarks>
        IDomProperty DomId { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// attributes object of this json:api resource object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. attributes is optional.
        /// </remarks>
        IDomProperty DomAttributes { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// relationships object of this json:api resource object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. relationships is optional.
        /// </remarks>
        IDomProperty DomRelationships { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// links object of this json:api resource object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. links is optional.
        /// </remarks>
        IDomProperty DomLinks { get; }
        #endregion
    }
}