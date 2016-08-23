// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Abstracts any object that has a settable <c>Errors</c> property.</summary>
    public interface ISetErrors
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        List<Error> Errors { set; }
        #endregion
    }
}