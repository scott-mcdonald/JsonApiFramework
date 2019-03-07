// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

namespace JsonApiFramework.Server
{
    public interface IIncludedResourcesBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IDocumentWriter IncludedEnd();

        #region Generic Versions
        // ToOne ////////////////////////////////////////////////////////////
        IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            where TFromResource : class
            where TToResource : class;

        IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(IEnumerable<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
            where TFromResource : class
            where TToResource : class;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            where TFromResource : class
            where TToResource : class;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IEnumerable<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
            where TFromResource : class
            where TToResource : class;

        // ToMany ///////////////////////////////////////////////////////////
        IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            where TFromResource : class
            where TToResource : class;

        IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(IEnumerable<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
            where TFromResource : class
            where TToResource : class;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            where TFromResource : class
            where TToResource : class;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IEnumerable<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
            where TFromResource : class
            where TToResource : class;
        #endregion
        #endregion

        #region Non-Generic Versions
        // ToOne ////////////////////////////////////////////////////////////
        IToOneIncludedResourceBuilder Include(IToOneIncludedResource toOneIncludedResource);

        IToOneIncludedResourceBuilder Include(IEnumerable<IToOneIncludedResource> toOneIncludedResourceCollection);

        IIncludedResourcesBuilder AddInclude(IToOneIncludedResource toOneIncludedResource);

        IIncludedResourcesBuilder AddInclude(IEnumerable<IToOneIncludedResource> toOneIncludedResourceCollection);

        // ToMany ///////////////////////////////////////////////////////////
        IToManyIncludedResourcesBuilder Include(IToManyIncludedResources toManyIncludedResources);

        IToManyIncludedResourcesBuilder Include(IEnumerable<IToManyIncludedResources> toManyIncludedResourcesCollection);

        IIncludedResourcesBuilder AddInclude(IToManyIncludedResources toManyIncludedResources);

        IIncludedResourcesBuilder AddInclude(IEnumerable<IToManyIncludedResources> toManyIncludedResourcesCollection);
        #endregion
    }
}
