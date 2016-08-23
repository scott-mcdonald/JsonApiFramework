// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.Server.Hypermedia.Internal
{
    /// <summary>
    /// Implementation of <c>IHypermediaPath</c> that represents path
    /// segments that are not related to any resource type but are present in
    /// the URL anyway.
    /// </summary>
    internal class NonResourceHypermediaPath : IHypermediaPath
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NonResourceHypermediaPath(string pathSegment)
        {
            _pathSegments = !String.IsNullOrWhiteSpace(pathSegment)
                ? new List<string> { pathSegment }
                : new List<string>();
        }

        public NonResourceHypermediaPath(IEnumerable<string> pathSegments)
        {
            _pathSegments = pathSegments.Where(x => !String.IsNullOrWhiteSpace(x)).ToList() ?? new List<string>();
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IPath Implementation
        public IEnumerable<string> PathSegments
        { get { return _pathSegments; } }
        #endregion

        #region IHypermediaPath Implementation
        public HypermediaPathType HypermediaPathType
        { get { return HypermediaPathType.NonResourcePath; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IHypermediaPath Implementation
        public Type GetClrResourceType()
        {
            var hypermediaPathTypeName = typeof(NonResourceHypermediaPath).Name;
            var detail = ServerErrorStrings.InternalErrorExceptionDetailInvalidGetClrResourceTypeForHypermediaPath
                                           .FormatWith(hypermediaPathTypeName);
            throw new InternalErrorException(detail);
        }
        #endregion

        #region Methods
        public void AddPathSegment(string pathSegment)
        {
            if (String.IsNullOrWhiteSpace(pathSegment))
                return;

            _pathSegments.Add(pathSegment);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private readonly IList<string> _pathSegments;
        #endregion
    }
}
