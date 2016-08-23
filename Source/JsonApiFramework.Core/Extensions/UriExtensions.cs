// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework
{
    public static class UriExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static IEnumerable<string> GetPathSegments(this Uri uri)
        {
            if (uri == null)
                return Enumerable.Empty<string>();

            return uri.IsAbsoluteUri
                ? PathSegmentsFromSegments(uri) // Handle Absolute URI
                : PathSegmentsFromUriOriginalString(uri); // Handle Relative URI
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IEnumerable<string> PathSegmentsFromUriOriginalString(Uri uri)
        {
            Contract.Requires(uri != null);

            var originalString = uri.OriginalString;
            if (String.IsNullOrWhiteSpace(originalString))
            {
                return Enumerable.Empty<string>();
            }

            var pathSegments = originalString.Split('/', '\\')
                                             .Where(x => !String.IsNullOrWhiteSpace(x));
            return pathSegments;
        }

        private static IEnumerable<string> PathSegmentsFromSegments(Uri uri)
        {
            Contract.Requires(uri != null);

            var segments = uri.Segments;
            if (segments == null)
            {
                return Enumerable.Empty<string>();
            }

            var pathSegments = segments.Select(x => x.Trim('/', '\\'))
                                       .Where(x => !String.IsNullOrWhiteSpace(x));
            return pathSegments;
        }
        #endregion
    }
}
