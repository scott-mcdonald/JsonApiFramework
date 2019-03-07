// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal.Dom;

namespace JsonApiFramework.Server.Internal
{
    internal class ToOneIncludedResourceBuilder : ResourceBuilder<IToOneIncludedResourceBuilder>, IToOneIncludedResourceBuilder
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
        internal ToOneIncludedResourceBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IToOneIncludedResource toOneIncludedResource)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toOneIncludedResource?.ToResourceType, toOneIncludedResource?.ToResource)
        {
            Contract.Requires(toOneIncludedResource != null);

            this.Builder = this;

            this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toOneIncludedResource);
        }
        #endregion
    }

    internal class ToOneIncludedResourceBuilder<TFromResource, TToResource> : ResourceBuilder<IToOneIncludedResourceBuilder<TToResource>, TToResource>, IToOneIncludedResourceBuilder<TToResource>
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
        internal ToOneIncludedResourceBuilder(DocumentBuilder parentBuilder, DomDocument domDocument, IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            : base(parentBuilder, domDocument.GetOrAddIncluded(), toOneIncludedResource?.ToResourceType, toOneIncludedResource?.ToResource)
        {
            Contract.Requires(toOneIncludedResource != null);

            this.Builder = this;

            this.DocumentBuilderContext.AddResourceLinkage(this.ServiceModel, toOneIncludedResource);
        }
        #endregion
    }
}
