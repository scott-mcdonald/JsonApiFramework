// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    /// <summary>
    /// Implementation of <c>IHypermediaPath</c> that represents path segments
    /// of an individual to-one relationship resource.
    /// </summary>
    internal class ToOneResourceHypermediaPath : IHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToOneResourceHypermediaPath(Type clrResourceType, string apiRelationshipPathSegment)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(String.IsNullOrWhiteSpace(apiRelationshipPathSegment) == false);

            this.ClrResourceType = clrResourceType;
            this.ApiRelationshipPathSegment = apiRelationshipPathSegment;

            this.PathSegments = new[] { apiRelationshipPathSegment };
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPath Implementation
        public IEnumerable<string> PathSegments
        { get; private set; }
        #endregion

        #region IHypermediaPath Implementation
        public HypermediaPathType HypermediaPathType
        { get { return HypermediaPathType.ToOneResourcePath; } }
        #endregion

        #region Properties
        public Type ClrResourceType
        { get; private set; }

        public string ApiRelationshipPathSegment
        { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IHypermediaPath Implementation
        public Type GetClrResourceType()
        { return this.ClrResourceType; }
        #endregion
    }

    /// <summary>Generic implementation of <c>ToOneResourceHypermediaPath</c>.</summary>
    internal class ToOneResourceHypermediaPath<TResource> : ToOneResourceHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToOneResourceHypermediaPath(string apiRelationshipPathSegment)
            : base(typeof(TResource), apiRelationshipPathSegment)
        { }
        #endregion
    }
}
