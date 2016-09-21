// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework
{
    /// <summary>
    /// Abstracts any object that can create a deep clone of itself.
    /// </summary>
    public interface IDeepCloneable
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        object DeepClone();
        #endregion
    }
}
