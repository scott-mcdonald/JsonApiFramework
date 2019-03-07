// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToManyIncludedResourcesBuilder : ResourceCollectionBuilder<IToManyIncludedResourcesBuilder>, IToManyIncludedResourcesBuilder
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
        internal ToManyIncludedResourcesBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IToManyIncludedResources toManyIncludedResources)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toManyIncludedResources?.ToResourceType, toManyIncludedResources?.ToResourceCollection)
        {
            this.Builder = this;

            this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toManyIncludedResources);
        }
        #endregion
    }

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
            return this.ParentBuilder;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ToManyIncludedResourcesBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toManyIncludedResources?.ToResourceType, toManyIncludedResources?.ToResourceCollection)
        {
            this.Builder = this;

            this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toManyIncludedResources);
        }
        #endregion
    }
}
