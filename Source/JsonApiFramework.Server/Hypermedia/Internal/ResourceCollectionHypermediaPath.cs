// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    /// <summary>
    /// Implementation of <c>IHypermediaPath</c> that represents path segments
    /// of a collection of resources.
    /// </summary>
    internal class ResourceCollectionHypermediaPath : IHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceCollectionHypermediaPath(Type clrResourceType, string apiCollectionPathSegment)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiCollectionPathSegment) == false);

            this.ClrResourceType = clrResourceType;
            this.ApiCollectionPathSegment = apiCollectionPathSegment;

            this.PathSegments = new[] { apiCollectionPathSegment };
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPath Implementation
        public IEnumerable<string> PathSegments
        { get; private set; }
        #endregion

        #region IHypermediaPath Implementation
        public HypermediaPathType HypermediaPathType
        { get { return HypermediaPathType.ResourceCollectionPath; } }
        #endregion

        #region Properties
        public Type ClrResourceType
        { get; private set; }

        public string ApiCollectionPathSegment
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IHypermediaPath Implementation
        public Type GetClrResourceType()
        { return this.ClrResourceType; }

        public bool HasClrResourceType()
        { return true; }
        #endregion
    }

    /// <summary>Generic implementation of <c>ResourceCollectionHypermediaPath</c>.</summary>
    internal class ResourceCollectionHypermediaPath<TResource> : ResourceCollectionHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ResourceCollectionHypermediaPath(string apiCollectionPathSegment)
            : base(typeof(TResource), apiCollectionPathSegment)
        { }
        #endregion
    }
}
