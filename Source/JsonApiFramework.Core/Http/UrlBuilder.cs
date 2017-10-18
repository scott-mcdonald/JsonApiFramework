// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Converters;

namespace JsonApiFramework.Http
{
    /// <summary>
    /// Represents a progressive fluent builder for building HTTP URL strings.
    /// </summary>
    public class UrlBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static UrlBuilder Create(IUrlBuilderConfiguration configuration, ITypeConverter typeConverter = null)
        {
            Contract.Requires(configuration != null);
            Contract.Requires(String.IsNullOrWhiteSpace(configuration.Scheme) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(configuration.Host) == false);

            var urlBuilder = new UrlBuilder(configuration, typeConverter ?? new TypeConverter());
            return urlBuilder;
        }

        public UrlBuilder Path(string pathSegment, bool includePath = true)
        {
            if (includePath == false || String.IsNullOrWhiteSpace(pathSegment))
                return this;

            this.PathSegments.Add(pathSegment);
            return this;
        }

        public UrlBuilder Path(IEnumerable<string> pathSegmentCollection, bool includePath = true)
        {
            if (includePath == false || pathSegmentCollection == null)
                return this;

            var pathSegments = pathSegmentCollection.Where(x => String.IsNullOrWhiteSpace(x) == false)
                                                    .ToList();
            this.PathSegments.AddRange(pathSegments);
            return this;
        }

        public UrlBuilder Path(IPath path, bool includePath = true)
        {
            if (includePath == false || path == null)
                return this;

            var pathSegments = path.PathSegments
                                   .EmptyIfNull()
                                   .Where(x => String.IsNullOrWhiteSpace(x) == false);

            this.PathSegments.AddRange(pathSegments);
            return this;
        }

        public UrlBuilder Path(IEnumerable<IPath> pathCollection, bool includePath = true)
        {
            if (includePath == false || pathCollection == null)
                return this;

            var pathSegments = pathCollection.SelectMany(x => x.PathSegments.EmptyIfNull())
                                             .Where(x => String.IsNullOrWhiteSpace(x) == false)
                                             .ToList();
            this.PathSegments.AddRange(pathSegments);
            return this;
        }

        public UrlBuilder RemoveLastPathSegment(bool removePathSegment = true)
        {
            if (removePathSegment == false || this.PathSegments.Count == 0)
                return this;

            var lastPathSegmentIndex = this.PathSegments.Count - 1;
            this.PathSegments.RemoveAt(lastPathSegmentIndex);
            return this;
        }

        public string Build()
        {
            var path = String.Join("/", this.PathSegments);

            var uriBuilder = new UriBuilder
            {
                Scheme = this.Scheme,
                Host = this.Host,
                Path = path
            };

            if (this.Port.HasValue)
            {

                var port = this.Port.Value;
                if (port > 0 && this.Port < 65535)
                {
                    uriBuilder.Port = port;
                }
            }

            var uri = uriBuilder.Uri;
            var url = uri.OriginalString;
            var urlTrimmedAtEnd = url.TrimEnd('/', '\\');
            return urlTrimmedAtEnd;
        }
        #endregion

        // PRIVATE CONSTRUCTORS /////////////////////////////////////////////
        #region Constructors
        private UrlBuilder(IUrlBuilderConfiguration configuration, ITypeConverter typeConverter)
        {
            Contract.Requires(configuration != null
                && String.IsNullOrWhiteSpace(configuration.Scheme) == false
                && String.IsNullOrWhiteSpace(configuration.Host) == false);

            Contract.Requires(typeConverter != null);

            this.Scheme = configuration.Scheme;
            this.Host = configuration.Host;
            this.Port = configuration.Port;
            this.PathSegments = new List<string>();
            this.PathSegments.AddRange(configuration.RootPathSegments ?? Enumerable.Empty<string>());
            this.TypeConverter = typeConverter;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private string Scheme { get; }
        private string Host { get; }
        private int? Port { get; }
        internal List<string> PathSegments { get; }
        internal ITypeConverter TypeConverter { get; }
        #endregion
    }
}
