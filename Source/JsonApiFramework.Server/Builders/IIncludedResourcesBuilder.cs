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

        // ToOne ////////////////////////////////////////////////////////////
        IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(IEnumerable<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(IToOneIncludedResourceSource<TFromResource, TToResource> toOneIncludedResourceSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToOneIncludedResourceBuilder<TToResource> Include<TFromResource, TToResource>(IToOneIncludedResourceCollectionSource<TFromResource, TToResource> toOneIncludedResourceCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IEnumerable<IToOneIncludedResource<TFromResource, TToResource>> toOneIncludedResourceCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToOneIncludedResourceSource<TFromResource, TToResource> toOneIncludedResourceSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToOneIncludedResourceCollectionSource<TFromResource, TToResource> toOneIncludedResourceCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        // ToMany ///////////////////////////////////////////////////////////
        IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(IEnumerable<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(IToManyIncludedResourcesSource<TFromResource, TToResource> toManyIncludedResourcesSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToManyIncludedResourcesBuilder<TToResource> Include<TFromResource, TToResource>(IToManyIncludedResourcesCollectionSource<TFromResource, TToResource> toOneIncludedResourcesCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IEnumerable<IToManyIncludedResources<TFromResource, TToResource>> toManyIncludedResourcesCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToManyIncludedResourcesSource<TFromResource, TToResource> toOneIncludedResourcesSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddInclude<TFromResource, TToResource>(IToManyIncludedResourcesCollectionSource<TFromResource, TToResource> toOneIncludedResourcesCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;
        #endregion
    }
}
