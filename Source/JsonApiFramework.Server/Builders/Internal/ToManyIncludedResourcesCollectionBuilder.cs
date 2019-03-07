// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToManyIncludedResourcesCollectionBuilder : ResourceCollectionBuilder<IToManyIncludedResourcesBuilder>, IToManyIncludedResourcesBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IToManyIncludedResourcesBuilder Implementation
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
        internal ToManyIncludedResourcesCollectionBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IReadOnlyCollection<IToManyIncludedResources> toManyIncludedResourcesCollection)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toManyIncludedResourcesCollection?.FirstOrDefault()?.ToResourceType, toManyIncludedResourcesCollection.EmptyIfNull().SelectMany(x => x.ToResourceCollection))
        {
            Contract.Requires(toManyIncludedResourcesCollection != null);

            this.Builder = this;

            foreach (var toManyIncludedResources in toManyIncludedResourcesCollection.EmptyIfNull())
            {
                this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toManyIncludedResources);
            }
        }
        #endregion
    }

    internal class ToManyIncludedResourcesCollectionBuilder<TFromResource, TToResource> : ResourceCollectionBuilder<IToManyIncludedResourcesBuilder<TToResource>, TToResource>, IToManyIncludedResourcesBuilder<TToResource>
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
            return this.ParentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToManyIncludedResourcesCollectionBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IReadOnlyCollection<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toManyIncludedResourcesCollection?.FirstOrDefault()?.ToResourceType, toManyIncludedResourcesCollection.EmptyIfNull().SelectMany(x => x.ToResourceCollection))
        {
            Contract.Requires(toManyIncludedResourcesCollection != null);

            this.Builder = this;

            foreach (var toManyIncludedResources in toManyIncludedResourcesCollection.EmptyIfNull())
            {
                this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toManyIncludedResources);
            }
        }
        #endregion
    }
}
