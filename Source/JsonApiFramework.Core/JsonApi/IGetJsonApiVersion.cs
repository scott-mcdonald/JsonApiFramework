// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts any object that has a <c>GetJsonApiVersion</c> method.
    /// </summary>
    public interface IGetJsonApiVersion
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        JsonApiVersion GetJsonApiVersion();
        #endregion
    }
}
