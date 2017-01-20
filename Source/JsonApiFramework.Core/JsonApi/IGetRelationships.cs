// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>Abstracts any object that has a gettable <c>Relationships</c> property.</summary>
    public interface IGetRelationships
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>
        /// Gets the immutable json:api relationships object contained by this object.
        /// </summary>
        Relationships Relationships { get; }
        #endregion
    }
}