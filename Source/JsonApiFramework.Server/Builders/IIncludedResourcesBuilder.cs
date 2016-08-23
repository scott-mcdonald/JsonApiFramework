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

        // ToOneResourceLinkage /////////////////////////////////////////////
        IToOneResourceLinkageBuilder<TToResource> ToOne<TFromResource, TToResource>(IToOneResourceLinkage<TFromResource, TToResource> toOneResourceLinkage)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToOneResourceLinkageBuilder<TToResource> ToOne<TFromResource, TToResource>(IEnumerable<IToOneResourceLinkage<TFromResource, TToResource>> toOneResourceLinkageCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToOneResourceLinkageBuilder<TToResource> ToOne<TFromResource, TToResource>(IToOneResourceLinkageSource<TFromResource, TToResource> toOneResourceLinkageSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToOneResourceLinkageBuilder<TToResource> ToOne<TFromResource, TToResource>(IToOneResourceLinkageCollectionSource<TFromResource, TToResource> toOneResourceLinkageCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(IToOneResourceLinkage<TFromResource, TToResource> toOneResourceLinkage)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(IEnumerable<IToOneResourceLinkage<TFromResource, TToResource>> toOneResourceLinkageCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(IToOneResourceLinkageSource<TFromResource, TToResource> toOneResourceLinkageSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddToOne<TFromResource, TToResource>(IToOneResourceLinkageCollectionSource<TFromResource, TToResource> toOneResourceLinkageCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        // ToManyResourceLinkage ////////////////////////////////////////////
        IToManyResourceLinkageBuilder<TToResource> ToMany<TFromResource, TToResource>(IToManyResourceLinkage<TFromResource, TToResource> toManyResourceLinkage)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToManyResourceLinkageBuilder<TToResource> ToMany<TFromResource, TToResource>(IEnumerable<IToManyResourceLinkage<TFromResource, TToResource>> toManyResourceLinkageCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToManyResourceLinkageBuilder<TToResource> ToMany<TFromResource, TToResource>(IToManyResourceLinkageSource<TFromResource, TToResource> toManyResourceLinkageSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IToManyResourceLinkageBuilder<TToResource> ToMany<TFromResource, TToResource>(IToManyResourceLinkageCollectionSource<TFromResource, TToResource> toOneResourceLinkageCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(IToManyResourceLinkage<TFromResource, TToResource> toManyResourceLinkage)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(IEnumerable<IToManyResourceLinkage<TFromResource, TToResource>> toManyResourceLinkageCollection)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(IToManyResourceLinkageSource<TFromResource, TToResource> toOneResourceLinkageSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;

        IIncludedResourcesBuilder AddToMany<TFromResource, TToResource>(IToManyResourceLinkageCollectionSource<TFromResource, TToResource> toOneResourceLinkageCollectionSource)
            where TFromResource : class, IResource
            where TToResource : class, IResource;
        #endregion
    }
}
