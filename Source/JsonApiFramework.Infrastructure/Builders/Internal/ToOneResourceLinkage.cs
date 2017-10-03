// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    internal class ToOneResourceLinkage<TResourceId> : IToOneResourceLinkage<TResourceId>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ToOneResourceLinkage()
        {
            this.HasValue = false;
            this.Value = default(TResourceId);
        }

        public ToOneResourceLinkage(TResourceId value)
        {
            this.HasValue = true;
            this.Value = value;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IToOneResourceLinkage Implementation
        public bool HasValue { get; }
        #endregion

        #region IToOneResourceLinkage<TResourceId> Implementation
        public TResourceId Value { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToOneResourceLinkage Implementation
        public ResourceIdentifier CreateApiResourceIdentifier(IResourceType resourceType)
        {
            Contract.Requires(resourceType != null);

            var clrResourceId = this.Value;
            var apiResourceIdentifier = resourceType.CreateApiResourceIdentifier(clrResourceId);
            return apiResourceIdentifier;
        }
        #endregion
    }
}