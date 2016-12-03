// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Abstracts any object that has a <c>GetMeta</c> method.
    /// </summary>
    public interface IGetMeta
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        Meta GetMeta();
        #endregion
    }
}
