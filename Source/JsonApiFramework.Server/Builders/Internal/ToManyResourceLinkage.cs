// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JsonApiFramework.Server.Internal
{
    internal class ToManyResourceLinkage<TFromResource, TToResource> : IToManyResourceLinkage<TFromResource, TToResource>
        where TFromResource : class, IResource
        where TToResource : class, IResource
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToManyResourceLinkage<TFromResource, TToResource> Implementation
        public Type FromClrResourceType { get { return typeof(TFromResource); } }
        public TFromResource FromResource { get; private set; }

        public string FromRel { get; private set; }

        public Type ToClrResourceType { get { return typeof(TToResource); } }
        public IEnumerable<TToResource> ToResourceCollection { get; private set; }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToManyResourceLinkage(TFromResource fromClrResource, string fromRel, IEnumerable<TToResource> toClrResourceCollection)
        {
            Contract.Requires(fromClrResource != null);
            Contract.Requires(String.IsNullOrWhiteSpace(fromRel) == false);

            this.FromResource = fromClrResource;
            this.FromRel = fromRel;

            this.ToResourceCollection = toClrResourceCollection.EmptyIfNull();
        }
        #endregion
    }
}