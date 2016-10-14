// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Converters;

namespace JsonApiFramework.Http
{
    /// <summary>
    /// Extension methods for the .NET UrlBuilder class.
    /// </summary>
    public static class UrlBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Add the generic object's string representation to the URL path.
        /// </summary>
        /// <remarks>
        /// Generic type parameter must be convertible to a string.
        /// </remarks>
        public static UrlBuilder Path<T>(this UrlBuilder urlBuilder, T pathSegment, bool includePath = true)
        {
            Contract.Requires(urlBuilder != null);

            if (includePath == false)
                return urlBuilder;

            var pathSegmentAsString = urlBuilder.TypeConverter.Convert<T, string>(pathSegment);
            if (String.IsNullOrWhiteSpace(pathSegmentAsString))
                return urlBuilder;

            urlBuilder.PathSegments.Add(pathSegmentAsString);
            return urlBuilder;
        }

        /// <summary>
        /// Add the generic object's string representation using the given
        /// format provider to the URL path.
        /// </summary>
        /// <remarks>
        /// Generic type parameter must be convertible to a string.
        /// </remarks>
        public static UrlBuilder Path<T>(this UrlBuilder urlBuilder, T pathSegment, IFormatProvider formatProvider, bool includePath = true)
        {
            Contract.Requires(urlBuilder != null);

            if (includePath == false)
                return urlBuilder;

            var context = new TypeConverterContext
                {
                    FormatProvider = formatProvider
                };
            var pathSegmentAsString = urlBuilder.TypeConverter.Convert<T, string>(pathSegment, context);
            if (String.IsNullOrWhiteSpace(pathSegmentAsString))
                return urlBuilder;

            urlBuilder.PathSegments.Add(pathSegmentAsString);
            return urlBuilder;
        }
        #endregion
    }
}
