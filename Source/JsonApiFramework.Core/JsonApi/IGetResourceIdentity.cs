// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts any object that has gettable <c>Type</c> and <c>Id</c> properties.
    /// </summary>
    /// <remarks>
    /// Resource identity per specification is any object with "type" and "id" string
    /// members that represent a "compound primary key" for the ecosystem of resources for a particular system.
    /// </remarks>
    public interface IGetResourceIdentity
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        string Type { get; }
        string Id { get; }
        #endregion
    }
}
