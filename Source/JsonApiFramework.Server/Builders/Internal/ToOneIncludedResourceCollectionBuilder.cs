// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToOneIncludedResourceCollectionBuilder : ResourceCollectionBuilder<IToOneIncludedResourceBuilder>, IToOneIncludedResourceBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToOneIncludedResourceBuilder Implementation
        public IIncludedResourcesBuilder IncludeEnd()
        {
            // Notify base class building is done.
            this.OnBuildEnd();

            // Return the parent builder.
            return this.ParentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToOneIncludedResourceCollectionBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IReadOnlyCollection<IToOneIncludedResource> toOneIncludedResourceCollection)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toOneIncludedResourceCollection?.FirstOrDefault()?.ToResourceType, toOneIncludedResourceCollection.EmptyIfNull().Select(x => x.ToResource))
        {
            Contract.Requires(toOneIncludedResourceCollection != null);

            this.Builder = this;

            foreach (var toOneIncludedResource in toOneIncludedResourceCollection.EmptyIfNull())
            {
                this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toOneIncludedResource);
            }
        }
        #endregion
    }

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
            return this.ParentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToOneIncludedResourceCollectionBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IReadOnlyCollection<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toOneIncludedResourceCollection?.FirstOrDefault()?.ToResourceType, toOneIncludedResourceCollection.EmptyIfNull().Select(x => x.ToResource))
        {
            Contract.Requires(toOneIncludedResourceCollection != null);

            this.Builder = this;

            foreach (var toOneIncludedResource in toOneIncludedResourceCollection.EmptyIfNull())
            {
                this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toOneIncludedResource);
            }
        }
        #endregion
    }
}
