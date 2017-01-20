// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi.Dom
{
    /// <summary>
    /// Represents specialized JSON.NET settings for the JSON serialization and deserialization of the DOM.
    /// </summary>
    public class DomJsonSerializerSettings
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Represents null value handling overrides per json:api property type.</summary>
        public IReadOnlyDictionary<PropertyType, NullValueHandling> NullValueHandlingOverrides { get; set; }
        #endregion
    }
}