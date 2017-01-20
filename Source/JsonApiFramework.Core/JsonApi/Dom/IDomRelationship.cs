// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi.Dom
{
    /// <summary>
    /// Abstracts read-only DOM node that represents a json:api relationship object.
    /// </summary>
    /// <remarks>
    /// A json:api relationship must have at least one of the following members:
    /// - Meta (meta object that contains non-standard meta-information about the relationship)
    /// - Data (to-one or to-many resource linkage)
    /// - Links (a links object containing at least a 'self' or 'related' json:api relationship link
    /// </remarks>
    public interface IDomRelationship : IDomObject
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the json:api relationship type of this DOM relationship.</summary>
        RelationshipType ApiRelationshipType { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// meta object of this json:api relationship object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. meta is optional.
        /// </remarks>
        IDomProperty DomMeta { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// data object of this json:api relationship object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. data is optional.
        /// </remarks>
        IDomProperty DomData { get; }

        /// <summary>
        /// Gets the read-only DOM property node that represents the json:api
        /// link sobject of this json:api relationship object.
        /// </summary>
        /// <remarks>
        /// This DOM property node is optional, i.e. links is optional.
        /// </remarks>
        IDomProperty DomLinks { get; }
        #endregion
    }
}