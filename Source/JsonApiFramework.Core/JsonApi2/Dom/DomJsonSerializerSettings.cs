// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json;

namespace JsonApiFramework.JsonApi2.Dom
{
    /// <summary>
    /// Represents settings for the JSON serialization and deserialization of
    /// the DOM.
    /// 
    /// These settings shall override any JSON.NET serializer settings if
    /// specified.
    /// </summary>
    public class DomJsonSerializerSettings
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>
        /// Gets or sets specifically how <c>meta</c> null meta values are
        /// handled during serialization to JSON.
        /// </summary>
        /// <remarks>
        /// If not set, default to the JSON.NET NullValueHandling value.
        /// </remarks>
        public NullValueHandling? MetaNullValueHandling { get; set; }
        #endregion
    }
}