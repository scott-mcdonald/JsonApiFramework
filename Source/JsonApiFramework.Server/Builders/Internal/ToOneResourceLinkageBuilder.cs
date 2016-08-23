// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToOneResourceLinkageBuilder<TFromResource, TToResource> : ResourceBuilder<IToOneResourceLinkageBuilder<TToResource>, TToResource>, IToOneResourceLinkageBuilder<TToResource>
        where TFromResource : class, IResource
        where TToResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToOneResourceLinkageBuilder<TResource> Implementation
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
        internal ToOneResourceLinkageBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IToOneResourceLinkage<TFromResource, TToResource> toOneResourceLinkage)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toOneResourceLinkage.ToResource)
        {
            Contract.Requires(toOneResourceLinkage != null);

            this.Builder = this;

            this.AddResourceLinkage(toOneResourceLinkage);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourceLinkage(IToOneResourceLinkage<TFromResource, TToResource> toOneResourceLinkage)
        {
            Contract.Requires(toOneResourceLinkage != null);

            this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toOneResourceLinkage);
        }
        #endregion
    }
}
