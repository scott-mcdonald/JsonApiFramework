// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Internal
{
    internal class ToManyIncludedResources : IToManyIncludedResources
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToManyIncludedResources Implementation
        public Type   FromResourceType { get; }
        public object FromResource     { get; }
        public string FromRel          { get; }

        public Type                ToResourceType       { get; }
        public IEnumerable<object> ToResourceCollection { get; }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToManyIncludedResources(Type fromResourceType, object fromResource, string fromRel, Type toResourceType, IEnumerable<object> toResourceCollection)
        {
            Contract.Requires(fromResourceType != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel) == false);
            Contract.Requires(toResourceType != null);

            this.FromResourceType = fromResourceType;
            this.FromResource     = fromResource;
            this.FromRel          = fromRel;

            this.ToResourceType       = toResourceType;
            this.ToResourceCollection = toResourceCollection.EmptyIfNull();
        }
        #endregion
    }

    internal class ToManyIncludedResources<TFromResource, TToResource> : IToManyIncludedResources<TFromResource, TToResource>
        where TFromResource : class
        where TToResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToManyIncludedResources<TFromResource, TToResource> Implementation
        public Type          FromResourceType { get; }
        public TFromResource FromResource     { get; }
        public string        FromRel          { get; }

        public Type                     ToResourceType       { get; }
        public IEnumerable<TToResource> ToResourceCollection { get; }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToManyIncludedResources(Type fromResourceType, TFromResource fromResource, string fromRel, Type toResourceType, IEnumerable<TToResource> toResourceCollection)
        {
            Contract.Requires(fromResourceType != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel) == false);
            Contract.Requires(toResourceType != null);

            this.FromResourceType = fromResourceType;
            this.FromResource     = fromResource;
            this.FromRel          = fromRel;

            this.ToResourceType       = toResourceType;
            this.ToResourceCollection = toResourceCollection.EmptyIfNull();
        }
        #endregion
    }
}