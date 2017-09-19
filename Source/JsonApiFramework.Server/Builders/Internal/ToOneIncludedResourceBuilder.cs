// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToOneIncludedResourceBuilder<TFromResource, TToResource> : ResourceBuilder<IToOneIncludedResourceBuilder<TToResource>, TToResource>, IToOneIncludedResourceBuilder<TToResource>
        where TFromResource : class, IResource
        where TToResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToOneIncludedResourceBuilder<TResource> Implementation
        public IIncludedResourcesBuilder ToOneEnd()
        {
            // Notify base class building is done.
            this.OnBuildEnd();

            // Return the parent builder.
            var parentBuilder = this.ParentBuilder;
            return parentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToOneIncludedResourceBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toOneIncludedResource.ToResource)
        {
            Contract.Requires(toOneIncludedResource != null);

            this.Builder = this;

            this.AddResourceLinkage(toOneIncludedResource);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourceLinkage(IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
        {
            Contract.Requires(toOneIncludedResource != null);

            this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toOneIncludedResource);
        }
        #endregion
    }
}
