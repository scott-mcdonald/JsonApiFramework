// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Tree;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    internal class ResourcePathContext : IResourcePathContext, INodeAttributeValue
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourcePathContext(IEnumerable<IHypermediaPath> resourceSelfBasePath, ResourcePathMode resourceSelfPathMode)
        {
            Contract.Requires(resourceSelfBasePath != null);

            this.ResourceSelfBasePath = resourceSelfBasePath;
            this.ResourceSelfPathMode = resourceSelfPathMode;
        }

        public ResourcePathContext(IEnumerable<IHypermediaPath> resourceSelfBasePath, ResourcePathMode resourceSelfPathMode,
                                   IEnumerable<IHypermediaPath> resourceCanonicalBasePath, ResourcePathMode resourceCanonicalPathMode)
        {
            Contract.Requires(resourceSelfBasePath != null);
            Contract.Requires(resourceCanonicalBasePath != null);

            this.ResourceSelfBasePath = resourceSelfBasePath;
            this.ResourceSelfPathMode = resourceSelfPathMode;

            this.ResourceCanonicalBasePath = resourceCanonicalBasePath;
            this.ResourceCanonicalPathMode = resourceCanonicalPathMode;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPathContext Implementation
        public IEnumerable<Type> ClrResourceTypes
        { get { return this.ResourceSelfBasePath.GetClrResourceTypes(); } }
        #endregion

        #region IResourcePathContext Implementation
        public IEnumerable<IHypermediaPath> ResourceCanonicalBasePath
        { get; private set; }

        public ResourcePathMode ResourceCanonicalPathMode
        { get; private set; }

        public IEnumerable<IHypermediaPath> ResourceSelfBasePath
        { get; private set; }

        public ResourcePathMode ResourceSelfPathMode
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region INodeAttributeValue Implementation
        public string ToNodeAttributeValueString()
        {
            var resourceSelfBasePathAsString = CreatePathSegmentsAsString(this.ResourceSelfBasePath);
            var resourceCanonicalBasePathAsString = CreatePathSegmentsAsString(this.ResourceCanonicalBasePath);

            var nodeAttributeValueString = "self={0} canonical={1}"
                .FormatWith(resourceSelfBasePathAsString, resourceCanonicalBasePathAsString);
            return nodeAttributeValueString;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static string CreatePathSegmentsAsString(IEnumerable<IHypermediaPath> resourceBasePath)
        {
            var resourceBasePathSegments = resourceBasePath
                .EmptyIfNull()
                .SelectMany(x => x.PathSegments)
                .ToList();

            var resourceBasePathAsString = resourceBasePathSegments.Any()
                ? resourceBasePathSegments.Aggregate((current, next) => current + "/" + next)
                : String.Empty;

            return resourceBasePathAsString;
        }
        #endregion
    }
}