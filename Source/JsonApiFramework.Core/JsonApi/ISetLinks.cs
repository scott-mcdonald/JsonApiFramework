// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
namespace JsonApiFramework.JsonApi
{
    /// <summary>Abstracts any object that has a settable <c>Links</c> property.</summary>
    public interface ISetLinks
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        Links Links { set; }
        #endregion
    }
}