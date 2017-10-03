// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal class NullToManyResourceLinkage : IToManyResourceLinkage
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullToManyResourceLinkage()
        {
            this.HasValueCollection = false;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToManyResourceLinkage Implementation
        public bool HasValueCollection { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToOneResourceLinkage Implementation
        public IEnumerable<ResourceIdentifier> CreateApiResourceIdentifierCollection(IResourceType resourceType)
        {
            Contract.Requires(resourceType != null);

            return Enumerable.Empty<ResourceIdentifier>();
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly IToManyResourceLinkage Default = new NullToManyResourceLinkage();
        #endregion
    }
}