// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace JsonApiFramework.JsonApi;

/// <summary>Abstracts any object that has a settable <c>Included</c> property.</summary>
public interface ISetIncluded
{
    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region Properties
    List<Resource> Included { set; }
    #endregion
}