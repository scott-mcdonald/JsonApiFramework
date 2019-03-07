// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Internal
{
    internal class ToOneIncludedResource : IToOneIncludedResource
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToOneIncludedResource<TFromResource, TToResource> Implementation
        public Type   FromResourceType { get; }
        public object FromResource     { get; }
        public string FromRel          { get; }

        public Type   ToResourceType { get; }
        public object ToResource     { get; }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToOneIncludedResource(Type fromResourceType, object fromResource, string fromRel, Type toResourceType, object toResource)
        {
            Contract.Requires(fromResourceType != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel) == false);
            Contract.Requires(toResourceType != null);

            this.FromResourceType = fromResourceType;
            this.FromResource     = fromResource;
            this.FromRel          = fromRel;

            this.ToResourceType = toResourceType;
            this.ToResource     = toResource;
        }
        #endregion
    }

    internal class ToOneIncludedResource<TFromResource, TToResource> : IToOneIncludedResource<TFromResource, TToResource>
        where TFromResource : class
        where TToResource : class
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToOneIncludedResource<TFromResource, TToResource> Implementation
        public Type          FromResourceType { get; }
        public TFromResource FromResource     { get; }
        public string        FromRel          { get; }

        public Type        ToResourceType { get; }
        public TToResource ToResource     { get; }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToOneIncludedResource(Type fromResourceType, TFromResource fromResource, string fromRel, Type toResourceType, TToResource toResource)
        {
            Contract.Requires(fromResourceType != null);
            Contract.Requires(fromResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel) == false);
            Contract.Requires(toResourceType != null);

            this.FromResourceType = fromResourceType;
            this.FromResource     = fromResource;
            this.FromRel          = fromRel;

            this.ToResourceType = toResourceType;
            this.ToResource     = toResource;
        }
        #endregion
    }
}