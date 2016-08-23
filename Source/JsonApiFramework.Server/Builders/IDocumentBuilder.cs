// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.Server
{
    /// <summary>
    /// Abstracts framework-level json:api compliant "document" creation with
    /// a builder progressive fluent interface style.
    /// </summary>
    public interface IDocumentBuilder : IDocumentWriter
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Sets the "jsonapi version" member of the document.</summary>
        IDocumentBuilder SetJsonApiVersion(JsonApiVersion jsonApiVersion);

        /// <summary>Sets the "meta" member of the document.</summary>
        IDocumentBuilder SetMeta(Meta meta);

        /// <summary>Starts the building of the "links" member of the document.</summary>
        IDocumentLinksBuilder<IDocumentBuilder> Links();

        // Resource /////////////////////////////////////////////////////////
        /// <summary>Starts the building of the "data" member of the resource document.</summary>
        IPrimaryResourceBuilder<TResource> Resource<TResource>(TResource clrResource)
            where TResource : class, IResource;

        /// <summary>Starts the building of the "data" member of the resource document.</summary>
        IPrimaryResourceBuilder<TResource> Resource<TResource>(IResourceSource<TResource> resourceSource)
            where TResource : class, IResource;

        // ResourceCollection ///////////////////////////////////////////////

        /// <summary>Starts the building of the "data" member of the resource collection document.</summary>
        IPrimaryResourceCollectionBuilder<TResource> ResourceCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class, IResource;

        /// <summary>Starts the building of the "data" member of the resource collection document.</summary>
        IPrimaryResourceCollectionBuilder<TResource> ResourceCollection<TResource>(IResourceCollectionSource<TResource> resourceCollectionSource)
            where TResource : class, IResource;

        // ResourceIdentifier ///////////////////////////////////////////////

        /// <summary>Starts the building of the "data" member of the resource identifier document.</summary>
        IPrimaryResourceIdentifierBuilder<TResource> ResourceIdentifier<TResource>()
            where TResource : class, IResource;

        /// <summary>Starts the building of the "data" member of the resource identifier document.</summary>
        IPrimaryResourceIdentifierBuilder<TResource> ResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class, IResource;

        /// <summary>Starts the building of the "data" member of the resource identifier document.</summary>
        IPrimaryResourceIdentifierBuilder<TResource> ResourceIdentifier<TResource, TResourceId>(IResourceIdentifierSource<TResourceId> resourceIdentifierSource)
            where TResource : class, IResource;

        /// <summary>Sets the "data" member of the resource identifier document null.</summary>
        IDocumentWriter SetResourceIdentifier<TResource>()
            where TResource : class, IResource;

        /// <summary>Sets the "data" member of the resource identifier document to be a single resource identifier extracted from a CLR resource.</summary>
        IDocumentWriter SetResourceIdentifier<TResource>(TResource clrResource)
            where TResource : class, IResource;

        /// <summary>Sets the "data" member of the resource identifier document to be a single resource identifier abstracted by <c>IResourceIdentifierProxy</c>.</summary>
        IDocumentWriter SetResourceIdentifier<TResource, TResourceId>(IResourceIdentifierSource<TResourceId> resourceIdentifierSource)
            where TResource : class, IResource;

        // ResourceIdentifierCollection /////////////////////////////////////
        
        /// <summary>Starts the building of the "data" member of the resource identifier collection document.</summary>
        IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource>()
            where TResource : class, IResource;

        /// <summary>Starts the building of the "data" member of the resource identifier collection document.</summary>
        IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class, IResource;

        /// <summary>Starts the building of the "data" member of the resource identifier collection document.</summary>
        IPrimaryResourceIdentifierCollectionBuilder<TResource> ResourceIdentifierCollection<TResource, TResourceId>(IResourceIdentifierCollectionSource<TResourceId> resourceIdentifierCollectionSource)
            where TResource : class, IResource;

        /// <summary>Sets the "data" member of the resource identifier collection document empty.</summary>
        IDocumentWriter SetResourceIdentifierCollection<TResource>()
            where TResource : class, IResource;

        /// <summary>Sets the "data" member of the resource identifier collection document.</summary>
        IDocumentWriter SetResourceIdentifierCollection<TResource>(IEnumerable<TResource> clrResourceCollection)
            where TResource : class, IResource;

        /// <summary>Sets the "data" member of the resource identifier collection document.</summary>
        IDocumentWriter SetResourceIdentifierCollection<TResource, TResourceId>(IResourceIdentifierCollectionSource<TResourceId> resourceIdentifierCollectionSource)
            where TResource : class, IResource;

        // Errors ///////////////////////////////////////////////////////////

        /// <summary>Starts the building of the "errors" member of the errors document.</summary>
        IErrorsBuilder Errors();
        #endregion
    }
}
