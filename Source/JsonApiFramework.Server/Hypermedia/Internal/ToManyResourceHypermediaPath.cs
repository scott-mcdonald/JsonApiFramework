// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    /// <summary>
    /// Implementation of <c>IHypermediaPath</c> that represents path segments
    /// of an individual to-many relationship resource.
    /// </summary>
    internal class ToManyResourceHypermediaPath : IHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToManyResourceHypermediaPath(Type clrResourceType, string apiRelationshipPathSegment, string apiId)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiRelationshipPathSegment) == false);
            Contract.Requires(String.IsNullOrWhiteSpace(apiId) == false);

            this.ClrResourceType = clrResourceType;
            this.ApiRelationshipPathSegment = apiRelationshipPathSegment;
            this.ApiId = apiId;

            this.PathSegments = new[] { apiRelationshipPathSegment, apiId };
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPath Implementation
        public IEnumerable<string> PathSegments
        { get; private set; }
        #endregion

        #region IHypermediaPath Implementation
        public HypermediaPathType HypermediaPathType
        { get { return HypermediaPathType.ToManyResourcePath; } }
        #endregion

        #region Properties
        public Type ClrResourceType
        { get; private set; }

        public string ApiRelationshipPathSegment
        { get; private set; }

        public string ApiId
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

    /// <summary>Generic implementation of <c>ToManyResourceHypermediaPath</c>.</summary>
    internal class ToManyResourceHypermediaPath<TResource> : ToManyResourceHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToManyResourceHypermediaPath(string apiRelationshipPathSegment, string apiId)
            : base(typeof(TResource), apiRelationshipPathSegment, apiId)
        { }
        #endregion
    }
}
