// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    /// <summary>Implementation of <c>IHypermediaPath</c> that represents path segments of a singleton resource.</summary>
    internal class SingletonHypermediaPath : IHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public SingletonHypermediaPath(Type clrResourceType, string apiSingletonPathSegment)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiSingletonPathSegment) == false);

            this.ClrResourceType = clrResourceType;
            this.ApiSingletonPathSegment = apiSingletonPathSegment;

            this.PathSegments = new[] { apiSingletonPathSegment };
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPath Implementation
        public IEnumerable<string> PathSegments { get; }
        #endregion

        #region IHypermediaPath Implementation
        public HypermediaPathType HypermediaPathType => HypermediaPathType.SingletonPath;
        #endregion

        #region Properties
        public Type ClrResourceType { get; }
        public string ApiSingletonPathSegment { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IHypermediaPath Implementation
        public Type GetClrResourceType() { return this.ClrResourceType; }
        public bool HasClrResourceType() { return true; }
        #endregion
    }

    /// <summary>Generic implementation of <c>SingletonHypermediaPath</c>.</summary>
    internal class SingletonHypermediaPath<TResource> : SingletonHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public SingletonHypermediaPath(string apiCollectionPathSegment)
            : base(typeof(TResource), apiCollectionPathSegment)
        { }
        #endregion
    }
}
