// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToOneResourceLinkageCollectionBuilder<TFromResource, TToResource> : ResourceCollectionBuilder<IToOneResourceLinkageBuilder<TToResource>, TToResource>, IToOneResourceLinkageBuilder<TToResource>
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
        internal ToOneResourceLinkageCollectionBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IReadOnlyCollection<IToOneResourceLinkage<TFromResource, TToResource>> toOneResourceLinkageCollection)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toOneResourceLinkageCollection.Select(x => x.ToResource))
        {
            Contract.Requires(toOneResourceLinkageCollection != null);

            this.Builder = this;

            foreach (var toOneResourceLinkage in toOneResourceLinkageCollection)
            {
                this.AddResourceLinkage(toOneResourceLinkage);
            }
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
