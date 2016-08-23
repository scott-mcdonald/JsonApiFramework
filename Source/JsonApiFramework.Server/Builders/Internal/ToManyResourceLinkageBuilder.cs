// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToManyResourceLinkageBuilder<TFromResource, TToResource> : ResourceCollectionBuilder<IToManyResourceLinkageBuilder<TToResource>, TToResource>, IToManyResourceLinkageBuilder<TToResource>
        where TFromResource : class, IResource
        where TToResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToManyResourceLinkageBuilder<TResource> Implementation
        public IIncludedResourcesBuilder ToManyEnd()
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
        internal ToManyResourceLinkageBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IToManyResourceLinkage<TFromResource, TToResource> toManyResourceLinkage)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toManyResourceLinkage.ToResourceCollection)
        {
            Contract.Requires(toManyResourceLinkage != null);

            this.Builder = this;

            this.AddResourceLinkage(toManyResourceLinkage);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourceLinkage(IToManyResourceLinkage<TFromResource, TToResource> toManyResourceLinkage)
        {
            Contract.Requires(toManyResourceLinkage != null);

            this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toManyResourceLinkage);
        }
        #endregion
    }
}
