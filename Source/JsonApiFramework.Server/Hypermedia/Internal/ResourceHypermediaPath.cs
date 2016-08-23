// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    /// <summary>
    /// Implementation of <c>IHypermediaPath</c> that represents path segments
    /// of an individual resource.
    /// </summary>
    internal class ResourceHypermediaPath : IHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceHypermediaPath(Type clrResourceType, string apiCollectionPathSegment, string apiId)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiCollectionPathSegment) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiId) == false);

            this.ClrResourceType = clrResourceType;
            this.ApiCollectionPathSegment = apiCollectionPathSegment;
            this.ApiId = apiId;

            this.PathSegments = new[] { apiCollectionPathSegment, apiId };
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPath Implementation
        public IEnumerable<string> PathSegments
        { get; private set; }
        #endregion

        #region IHypermediaPath Implementation
        public HypermediaPathType HypermediaPathType
        { get { return HypermediaPathType.ResourcePath; } }
        #endregion

        #region Properties
        public Type ClrResourceType
        { get; private set; }

        public string ApiCollectionPathSegment
        { get; private set; }

        public string ApiId
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IHypermediaPath Implementation
        public Type GetClrResourceType()
        { return this.ClrResourceType; }
        #endregion
    }

    /// <summary>Generic implementation of <c>ResourceHypermediaPath</c>.</summary>
    internal class ResourceHypermediaPath<TResource> : ResourceHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceHypermediaPath(string apiCollectionPathSegment, string apiId)
            : base(typeof(TResource), apiCollectionPathSegment, apiId)
        { }
        #endregion
    }
}
