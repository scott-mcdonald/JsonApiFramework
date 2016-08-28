// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi
{
    /// <summary>Abstracts any object that has a gettable <c>Attributes</c> property.</summary>
    public interface IGetAttributes
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        ApiObject Attributes { get; }
        #endregion
    }
}
