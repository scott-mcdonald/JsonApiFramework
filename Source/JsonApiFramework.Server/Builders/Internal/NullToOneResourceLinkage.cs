// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal class NullToOneResourceLinkage : IToOneResourceLinkage
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NullToOneResourceLinkage()
        {
            this.HasValue = false;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToOneResourceLinkage Implementation
        public bool HasValue { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToOneResourceLinkage Implementation
        public ResourceIdentifier CreateApiResourceIdentifier(IResourceType resourceType)
        {
            Contract.Requires(resourceType != null);

            return null;
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly IToOneResourceLinkage Default = new NullToOneResourceLinkage();
        #endregion
    }
}