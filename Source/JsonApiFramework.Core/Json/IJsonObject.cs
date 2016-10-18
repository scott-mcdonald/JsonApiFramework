// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json;

namespace JsonApiFramework.Json
{
    /// <summary>
    /// Abstracts any object that can serialize itself into a JSON string
    /// representation.
    /// </summary>
    public interface IJsonObject
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Return a JSON representation of this object using default serializer settings.
        /// </summary>
        /// <returns>JSON representation of this object.</returns>
        string ToJson();

        /// <summary>
        /// Return a JSON representation of this object using explicit serializer settings.
        /// </summary>
        /// <returns>JSON representation of this object.</returns>
        string ToJson(JsonSerializerSettings serializerSettings);
        #endregion
    }
}
