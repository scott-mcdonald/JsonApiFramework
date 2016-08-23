// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal abstract class InfoObject : JsonObject
        , IInfoObject
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IInfoObject Implementation
        public virtual void Initialize(IServiceModel serviceModel, IResourceType resourceType)
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(resourceType != null);

            this.ServiceModel = serviceModel;
            this.ResourceType = resourceType;
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected IServiceModel ServiceModel { get; private set; }
        protected IResourceType ResourceType { get; private set; }
        #endregion
    }
}