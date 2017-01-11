// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.Internal.Tree;
using JsonApiFramework.JsonApi;
using JsonApiFramework.Server.Internal.Dom;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal abstract class ResourceCollectionBuilder<TBuilder, TResource> : IResourceBuilder<TBuilder, TResource>
        where TBuilder : class, IResourceBuilder<TBuilder, TResource>
        where TResource : class, IResource
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceBuilder<TBuilder, TResource> Implementation
        public TBuilder SetMeta(Meta meta)
        {
            Contract.Requires(meta != null);

            if (this.NotBuildingResourceCollection)
                return this.Builder;

            foreach (var domReadWriteResource in this.DomReadWriteResourceCollection)
            {
                domReadWriteResource.SetDomReadOnlyMeta(meta);
            }

            return this.Builder;
        }

        public TBuilder SetMeta(IEnumerable<Meta> metaCollection)
        {
            Contract.Requires(metaCollection != null);

            if (this.NotBuildingResourceCollection)
                return this.Builder;

            var metaReadOnlyList = metaCollection.SafeToReadOnlyList();
            var metaReadOnlyListCount = metaReadOnlyList.Count;
            var domReadWriteResourceCollectionCount = this.DomReadWriteResourceCollection.Count;
            if (metaReadOnlyListCount != domReadWriteResourceCollectionCount)
            {
                var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionCountMismatch
                                                       .FormatWith(DomNodeType.Meta, domReadWriteResourceCollectionCount, typeof(TResource).Name, metaReadOnlyListCount);
                throw new DocumentBuildException(detail);
            }

            var count = domReadWriteResourceCollectionCount;
            for (var i = 0; i < count; ++i)
            {
                var domReadWriteResource = this.DomReadWriteResourceCollection[i];
                var meta = metaReadOnlyList[i];

                domReadWriteResource.SetDomReadOnlyMeta(meta);
            }

            return this.Builder;
        }

        public IResourcePathContextBuilder<TBuilder> Paths()
        {
            var resourcePathContextBuilder = this.NotBuildingResourceCollection
                ? (IResourcePathContextBuilder<TBuilder>)(new NullResourcePathContextBuilder<TBuilder>(this.Builder))
                : (IResourcePathContextBuilder<TBuilder>)(this.ResourcePathContextBuilder);
            return resourcePathContextBuilder;
        }

        public IRelationshipsBuilder<TBuilder, TResource> Relationships()
        {
            var relationshipsBuilder = this.NotBuildingResourceCollection
                ? (IRelationshipsBuilder<TBuilder, TResource>)(new NullRelationshipsBuilder<TBuilder, TResource>(this.Builder))
                : (IRelationshipsBuilder<TBuilder, TResource>)(new RelationshipsCollectionBuilder<TBuilder, TResource>(this.Builder, this.DomReadWriteResourceCollection, this.ClrResourceCollection));
            return relationshipsBuilder;
        }

        public IResourceLinksBuilder<TBuilder, TResource> Links()
        {
            var linksBuilder = this.NotBuildingResourceCollection
                ? (IResourceLinksBuilder<TBuilder, TResource>)(new NullResourceLinksBuilder<TBuilder, TResource>(this.Builder))
                : (IResourceLinksBuilder<TBuilder, TResource>)(new ResourceLinksCollectionBuilder<TBuilder, TResource>(this.Builder, this.DomReadWriteResourceCollection, this.ClrResourceCollection));
            return linksBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceCollectionBuilder(DocumentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, IEnumerable<TResource> clrResourceCollection)
        {
            Contract.Requires(domContainerNode != null);

            this.ParentBuilder = parentBuilder;

            this.InitializeResourcePathContextBuilder();
            this.InitializeResourceCollection(domContainerNode, clrResourceCollection);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected DocumentBuilder ParentBuilder { get; private set; }
        protected TBuilder Builder { private get; set; }
        #endregion

        #region Inherited Properties
        protected IServiceModel ServiceModel
        { get { return this.ParentBuilder.ServiceModel; } }

        protected DocumentBuilderContext DocumentBuilderContext
        { get { return this.ParentBuilder.DocumentBuilderContext; } }
        #endregion

        // PROTECTED METHODS ////////////////////////////////////////////////
        #region Methods
        protected void OnBuildEnd()
        {
            if (this.NotBuildingResourceCollection)
                return;

            this.AddResourcePathContext();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private Lazy<ResourcePathContextBuilder<TBuilder, TResource>> LazyResourcePathContextBuilder { get; set; }
        private ResourcePathContextBuilder<TBuilder, TResource> ResourcePathContextBuilder { get { return this.LazyResourcePathContextBuilder.Value; } }

        private bool BuildingResourceCollection { get { return this.DomReadWriteResourceCollection.Any(); } }
        private bool NotBuildingResourceCollection { get { return !this.BuildingResourceCollection; } }

        private IReadOnlyList<DomReadWriteResource> DomReadWriteResourceCollection { get; set; }
        private IReadOnlyList<TResource> ClrResourceCollection { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourcePathContext()
        {
            var resourcePathContext = this.ResourcePathContextBuilder.CreateResourcePathContext();
            foreach (var domReadWriteResource in this.DomReadWriteResourceCollection)
            {
                domReadWriteResource.SetResourcePathContext(resourcePathContext);
            }
        }

        private void InitializeResourceCollection(IContainerNode<DomNodeType> domContainerNode, IEnumerable<TResource> clrResourceCollection)
        {
            // Map the incoming CLR resources to the DOM equilvalent nodes.
            var serviceModel = this.ServiceModel;
            var clrResourceType = typeof(TResource);
            var resourceType = serviceModel.GetResourceType(clrResourceType);

            var domResourceTupleCollection1 = clrResourceCollection
                .EmptyIfNull()
                .Where(clrResource => clrResource != null)
                .Select(clrResource =>
                    {
                        var domReadWriteResource = DomReadWriteResource.Create();
                        resourceType.MapClrTypeToDomResource(domReadWriteResource);
                        resourceType.MapClrIdToDomResource(domReadWriteResource, clrResource);

                        var domResource = (IDomResource)domReadWriteResource;
                        var domResourceKey = domResource.CreateDomResourceKey();

                        var domResourceTuple = new Tuple<ResourceIdentifier, DomReadWriteResource, TResource>(domResourceKey, domReadWriteResource, clrResource);
                        return domResourceTuple;
                    })
                .ToList();

            // Add the DOM read/write resource nodes to the DOM document.
            var domResourceTupleCollection2 = new List<Tuple<ResourceIdentifier, DomReadWriteResource, TResource>>();
            foreach (var domResourceTuple in domResourceTupleCollection1)
            {
                // Add the DOM read/write resource nodes to the DOM document.
                var domResourceKey = domResourceTuple.Item1;
                var domReadWriteResource = domResourceTuple.Item2;

                // Do not add the DOM read-write resource node if it already has been added to the DOM document.
                if (this.DocumentBuilderContext.ContainsDomReadWriteResource(domResourceKey))
                    continue;

                this.DocumentBuilderContext.AddDomReadWriteResource(domResourceKey, domReadWriteResource);
                domContainerNode.Add(domReadWriteResource);

                // Finish mapping the DOM read/write resource attributes nodes to the DOM document.
                var clrResource = domResourceTuple.Item3;

                resourceType.MapClrAttributesToDomResource(domReadWriteResource, clrResource);

                // Keep track of the actual DOM read-write resource nodes added to the DOM document.
                domResourceTupleCollection2.Add(domResourceTuple);
            }

            var domReadWriteResourceCollection = domResourceTupleCollection2
                .Select(domResourceTuple => domResourceTuple.Item2)
                .ToList();
            this.DomReadWriteResourceCollection = domReadWriteResourceCollection;

            var clrResourceCollectionInternal = domResourceTupleCollection2
                .Select(domResourceTuple => domResourceTuple.Item3)
                .ToList();
            this.ClrResourceCollection = clrResourceCollectionInternal;
        }

        private void InitializeResourcePathContextBuilder()
        {
            this.LazyResourcePathContextBuilder =
                new Lazy<ResourcePathContextBuilder<TBuilder, TResource>>(
                    () => new ResourcePathContextBuilder<TBuilder, TResource>(this.Builder, this.ServiceModel));
        }
        #endregion
    }
}
