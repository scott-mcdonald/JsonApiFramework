// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json.Linq;

namespace JsonApiFramework.JsonApi
{
    /// <summary>Abstracts any object that has a gettable <c>Attributes</c> property.</summary>
    public interface IGetAttributes
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        // ReSharper disable once ReturnTypeCanBeEnumerable.Global
        JObject Attributes { get; }
        #endregion
    }
}
