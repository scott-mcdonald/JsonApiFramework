// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Linq;

// ReSharper disable CheckNamespace
namespace JsonApiFramework.JsonApi.Dom.Internal
{
    /// <summary>
    /// Extension methods for the .NET String class.
    /// </summary>
    public static class StringExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static string GetJsonPointer(this string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                return String.Empty;

            var pathSplit = path.Split(PathToJsonPointerDelimiters);

            var jsonPointerMinusRoot = String.Join(
                JsonPointerPathSegment,
                pathSplit.Where(x => String.IsNullOrWhiteSpace(x) == false));

            var jsonPointer = JsonPointerPathSegment + jsonPointerMinusRoot;
            return jsonPointer;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string JsonPointerPathSegment = "/";
        private static readonly char[] PathToJsonPointerDelimiters = { '.', '[', ']' };
        #endregion
    }
}
