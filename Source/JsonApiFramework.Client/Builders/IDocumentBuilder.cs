// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Client
{
    public interface IDocumentBuilder : IDocumentWriter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        IDocumentBuilder SetMeta(Meta meta);

        // Resource /////////////////////////////////////////////////////////
        IPrimaryResourceBuilder<TResource> Resource<TResource>()
            where TResource : class;

        IPrimaryResourceBuilder<TResource> Resource<TResource>(TResource clrResource)
            where TResource : class;

        IPrimaryResourceBuilder<TResource> Resource<TResource>(IResourceSource<TResource> resourceSource)
            where TResource : class;

        // ResourceIdentifier ///////////////////////////////////////////////
        IPrimaryResourceIdentifierBuilder<TResource> ResourceIdentifier<TResource>()
            where TResource : class;

        IPrimaryResourceIdentifierBuilder<TResource> ResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class;

        IPrimaryResourceIdentifierBuilder<TResource> ResourceIdentifier<TResource, TResourceId>(IResourceIdentifierSource<TResourceId> resourceIdentifierSource)
            where TResource : class;

        IDocumentWriter SetResourceIdentifier<TResource>()
            where TResource : class;

        IDocumentWriter SetResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class;

        IDocumentWriter SetResourceIdentifier<TResource, TResourceId>(IResourceIdentifierSource<TResourceId> resourceIdentifierSource)
            where TResource : class;

        // ResourceIdentifierCollection /////////////////////////////////////
        IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource>()
            where TResource : class;

        IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class;

        IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource, TResourceId>(IResourceIdentifierCollectionSource<TResourceId> resourceIdentifierCollectionSource)
            where TResource : class;

        IDocumentWriter SetResourceIdentifierCollection<TResource>()
            where TResource : class;

        IDocumentWriter SetResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class;

        IDocumentWriter SetResourceIdentifierCollection<TResource, TResourceId>(IResourceIdentifierCollectionSource<TResourceId> resourceIdentifierCollectionSource)
            where TResource : class;
        #endregion
    }
}
