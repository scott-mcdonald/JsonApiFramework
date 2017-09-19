// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToManyIncludedResourcesCollectionBuilder<TFromResource, TToResource> : ResourceCollectionBuilder<IToManyIncludedResourcesBuilder<TToResource>, TToResource>, IToManyIncludedResourcesBuilder<TToResource>
        where TFromResource : class, IResource
        where TToResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToManyIncludedResourcesBuilder<TResource> Implementation
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
        internal ToManyIncludedResourcesCollectionBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IReadOnlyCollection<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toManyIncludedResourcesCollection.SelectMany(x => x.ToResourceCollection))
        {
            Contract.Requires(toManyIncludedResourcesCollection != null);

            this.Builder = this;

            foreach (var toManyIncludedResources in toManyIncludedResourcesCollection)
            {
                this.AddResourceLinkage(toManyIncludedResources);
            }
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
