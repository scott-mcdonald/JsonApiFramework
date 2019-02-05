// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToManyIncludedResourcesBuilder<TFromResource, TToResource> : ResourceCollectionBuilder<IToManyIncludedResourcesBuilder<TToResource>, TToResource>, IToManyIncludedResourcesBuilder<TToResource>
        where TFromResource : class
        where TToResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToManyIncludedResourcesBuilder<TResource> Implementation
        public IIncludedResourcesBuilder IncludeEnd()
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
        internal ToManyIncludedResourcesBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toManyIncludedResources.ToResourceCollection)
        {
            Contract.Requires(toManyIncludedResources != null);

            this.Builder = this;

            this.AddResourceLinkage(toManyIncludedResources);
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourceLinkage(IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
        {
            Contract.Requires(toManyIncludedResources != null);

            this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toManyIncludedResources);
        }
        #endregion
    }
}
