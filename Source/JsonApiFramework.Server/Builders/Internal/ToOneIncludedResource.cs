// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Internal
{
    internal class ToOneIncludedResource<TFromResource, TToResource> : IToOneIncludedResource<TFromResource, TToResource>
        where TFromResource : class, IResource
        where TToResource : class, IResource
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToOneIncludedResource<TFromResource, TToResource> Implementation
        public Type FromClrResourceType { get { return typeof(TFromResource); } }
        public TFromResource FromResource { get; private set; }

        public string FromRel { get; private set; }

        public Type ToClrResourceType { get { return typeof(TToResource); } }
        public TToResource ToResource { get; private set; }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToOneIncludedResource(TFromResource fromClrResource, string fromRel, TToResource toClrResource)
        {
            Contract.Requires(fromClrResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel) == false);

            this.FromResource = fromClrResource;
            this.FromRel = fromRel;

            this.ToResource = toClrResource;
        }
        #endregion
    }
}