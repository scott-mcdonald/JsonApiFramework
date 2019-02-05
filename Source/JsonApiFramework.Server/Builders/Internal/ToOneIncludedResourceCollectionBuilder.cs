// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToOneIncludedResourceCollectionBuilder<TFromResource, TToResource> : ResourceCollectionBuilder<IToOneIncludedResourceBuilder<TToResource>, TToResource>, IToOneIncludedResourceBuilder<TToResource>
        where TFromResource : class
        where TToResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToOneIncludedResourceBuilder<TResource> Implementation
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
        internal ToOneIncludedResourceCollectionBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IReadOnlyCollection<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toOneIncludedResourceCollection.Select(x => x.ToResource))
        {
            Contract.Requires(toOneIncludedResourceCollection != null);

            this.Builder = this;

            foreach (var toOneIncludedResource in toOneIncludedResourceCollection)
            {
                this.AddResourceLinkage(toOneIncludedResource);
            }
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
