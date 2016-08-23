// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Abstracts any object that has a settable <c>Attributes</c> property.</summary>
    public interface ISetAttributes
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        JObject Attributes { set; }
        #endregion
    }
}