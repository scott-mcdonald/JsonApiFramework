// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts any object that has gettable <c>Type</c> and <c>Id</c> properties
    /// that represent "type" and "id" of json:api resource or resource identifiers.
    /// </summary>
    /// <remarks>
    /// Resource identity per specification is any object with "type" and "id" string
    /// members that represent a "compound primary key" for the ecosystem of resources
    /// for a particular system.
    /// </remarks>
    public interface IGetResourceIdentity
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the json:api "type" of this resource or resource identifier.</summary>
        string Type { get; }

        /// <summary>Gets the json:api "id" of this resource or resource identifier.</summary>
        string Id { get; }
        #endregion
    }
}