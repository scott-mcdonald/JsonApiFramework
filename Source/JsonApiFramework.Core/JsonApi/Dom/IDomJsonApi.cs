// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents a json:api jsonapi object.
    /// </summary>
    public interface IDomJsonApi : IDomObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>
        /// Gets the read-only DOM property node that represents the version
        /// of this json:api jsonapi object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, version is optional.
        /// </remarks>
        IDomProperty DomVersion { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// meta object of this json:api jsonapi object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, meta is optional.
        /// </remarks>
        IDomProperty DomMeta { get; }
        #endregion
    }
}