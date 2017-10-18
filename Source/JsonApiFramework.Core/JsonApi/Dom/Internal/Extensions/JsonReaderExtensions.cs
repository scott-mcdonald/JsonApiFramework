// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>Extension methods for the JSON.NET JsonReader class.</summary>
    internal static class JsonReaderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Returns a JSON pointer [RFC6901] represented by this JSON.NET JsonReader object.</summary>
        public static string GetJsonPointer(this JsonReader jsonReader)
        {
            var jsonPointer = jsonReader?.Path.GetJsonPointer();
            return jsonPointer;
        }
        #endregion
    }
}
