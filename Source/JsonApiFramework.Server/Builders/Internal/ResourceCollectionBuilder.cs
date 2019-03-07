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
    internal abstract class ResourceCollectionBuilder<TBuilder> : IResourceBuilder<TBuilder>
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

            var metaReadOnlyList                    = metaCollection.SafeToReadOnlyList();
            var metaReadOnlyListCount               = metaReadOnlyList.Count;
            var domReadWriteResourceCollectionCount = this.DomReadWriteResourceCollection.Count;
            if (metaReadOnlyListCount != domReadWriteResourceCollectionCount)
            {
                var detail = InfrastructureErrorStrings.DocumentBuildExceptionDetailBuildResourceCollectionCountMismatch
                                                       .FormatWith(DomNodeType.Meta, domReadWriteResourceCollectionCount, this.ClrResourceType.Name, metaReadOnlyListCount);
                throw new DocumentBuildException(detail);
            }

            var count = domReadWriteResourceCollectionCount;
            for (var i = 0; i < count; ++i)
            {
                var domReadWriteResource = this.DomReadWriteResourceCollection[i];
                var meta                 = metaReadOnlyList[i];

                domReadWriteResource.SetDomReadOnlyMeta(meta);
            }

            return this.Builder;
        }

        public IResourcePathContextBuilder<TBuilder> Paths()
        {
            var resourcePathContextBuilder = this.NotBuildingResourceCollection
                ? (IResourcePathContextBuilder<TBuilder>)(new NullResourcePathContextBuilder<TBuilder>(this.Builder))
                : (IResourcePathContextBuilder<TBuilder>)(new ResourcePathContextCollectionBuilder<TBuilder>(this.Builder, this.ResourcePathContextBuilders));
            return resourcePathContextBuilder;
        }

        public IRelationshipsBuilder<TBuilder> Relationships()
        {
            var relationshipsBuilder = this.NotBuildingResourceCollection
                ? (IRelationshipsBuilder<TBuilder>)(new NullRelationshipsBuilder<TBuilder>(this.Builder))
                : (IRelationshipsBuilder<TBuilder>)(new RelationshipsCollectionBuilder<TBuilder>(this.Builder, this.ServiceModel, this.DomReadWriteResourceCollection, this.ClrResourceType, this.ClrResourceCollection));
            return relationshipsBuilder;
        }

        public IResourceLinksBuilder<TBuilder> Links()
        {
            var linksBuilder = this.NotBuildingResourceCollection
                ? (IResourceLinksBuilder<TBuilder>)(new NullResourceLinksBuilder<TBuilder>(this.Builder))
                : (IResourceLinksBuilder<TBuilder>)(new ResourceLinksCollectionBuilder<TBuilder>(this.Builder, this.DomReadWriteResourceCollection, this.ClrResourceType, this.ClrResourceCollection));
            return linksBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceCollectionBuilder(DocumentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, IEnumerable<object> clrResourceCollection)
        {
            Contract.Requires(domContainerNode != null);
            Contract.Requires(clrResourceType != null);

            this.ParentBuilder = parentBuilder;

            if (clrResourceType == null)
                return;

            var resourceType = this.ServiceModel.GetResourceType(clrResourceType);
            this.ResourceType = resourceType;

            this.InitializeResourceCollection(domContainerNode, clrResourceType, clrResourceCollection);
            this.InitializeResourcePathContextBuilder();
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region Properties
        protected DocumentBuilder ParentBuilder { get; }
        protected TBuilder        Builder       { get; set; }

        protected IReadOnlyList<DomReadWriteResource> DomReadWriteResourceCollection { get; private set; }
        protected IReadOnlyList<object>               ClrResourceCollection          { get; private set; }
        #endregion

        #region Inherited Properties
        protected DocumentBuilderContext DocumentBuilderContext => this.ParentBuilder.DocumentBuilderContext;

        protected bool NotBuildingResourceCollection => !this.BuildingResourceCollection;

        protected IServiceModel ServiceModel => this.ParentBuilder.ServiceModel;
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
        private IResourceType ResourceType { get; }

        private bool BuildingResourceCollection { get; set; }

        private Lazy<IReadOnlyList<ResourcePathContextBuilder<TBuilder>>> LazyResourcePathContextBuilders { get; set; }

        private IReadOnlyList<ResourcePathContextBuilder<TBuilder>> ResourcePathContextBuilders => this.LazyResourcePathContextBuilders.Value;
        #endregion

        #region Calculated Properties
        private Type ClrResourceType => this.ResourceType.ClrType;
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourcePathContext()
        {
            var count = this.DomReadWriteResourceCollection.Count;
            for (var i = 0; i < count; i++)
            {
                var clrResource                = this.ClrResourceCollection[i];
                var resourcePathContextBuilder = this.ResourcePathContextBuilders[i];
                var resourcePathContext        = resourcePathContextBuilder.CreateResourcePathContext(clrResource);

                var domReadWriteResource = this.DomReadWriteResourceCollection[i];
                domReadWriteResource.SetResourcePathContext(resourcePathContext);
            }
        }

        private void InitializeResourceCollection(IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, IEnumerable<object> clrResourceCollection)
        {
            Contract.Requires(clrResourceType != null);

            // ReSharper disable PossibleMultipleEnumeration
            if (clrResourceCollection == null || clrResourceCollection.Any() == false)
            {
                this.ClrResourceCollection = new List<object>();
                return;
            }

            // Map the incoming CLR resources to the DOM equivalent nodes.
            var serviceModel = this.ServiceModel;
            var resourceType = serviceModel.GetResourceType(clrResourceType);

            var domResourceTupleCollection1 = clrResourceCollection
                                              .Where(clrResource => clrResource != null)
                                              .Select(clrResource =>
                                              {
                                                  var domReadWriteResource = DomReadWriteResource.Create();
                                                  resourceType.MapClrTypeToDomResource(domReadWriteResource);
                                                  resourceType.MapClrIdToDomResource(domReadWriteResource, clrResource);

                                                  var domResource    = (IDomResource)domReadWriteResource;
                                                  var domResourceKey = domResource.CreateDomResourceKey();

                                                  var domResourceTuple = new Tuple<ResourceIdentifier, DomReadWriteResource, object>(domResourceKey, domReadWriteResource, clrResource);
                                                  return domResourceTuple;
                                              })
                                              .ToList();
            // ReSharper restore PossibleMultipleEnumeration

            // Add the DOM read/write resource nodes to the DOM document.
            var queryParameters    = this.DocumentBuilderContext.QueryParameters;
            var apiType            = resourceType.ResourceIdentityInfo.ApiType;
            var useSparseFieldsets = this.DocumentBuilderContext.SparseFieldsetsEnabled && queryParameters.ContainsField(apiType);

            var domResourceTupleCollection2 = new List<Tuple<ResourceIdentifier, DomReadWriteResource, object>>();
            foreach (var domResourceTuple in domResourceTupleCollection1)
            {
                // Add the DOM read/write resource nodes to the DOM document.
                var domResourceKey       = domResourceTuple.Item1;
                var domReadWriteResource = domResourceTuple.Item2;

                // Do not add the DOM read-write resource node if it already has been added to the DOM document.
                if (this.DocumentBuilderContext.ContainsDomReadWriteResource(domResourceKey))
                    continue;

                this.DocumentBuilderContext.AddDomReadWriteResource(domResourceKey, domReadWriteResource);
                domContainerNode.Add(domReadWriteResource);

                // Finish mapping the DOM read/write resource attributes nodes to the DOM document.
                var clrResource = domResourceTuple.Item3;

                if (!useSparseFieldsets)
                {
                    resourceType.MapClrAttributesToDomResource(domReadWriteResource, clrResource);
                }
                else
                {
                    resourceType.MapClrAttributesToDomResource(domReadWriteResource, clrResource, (x, y) => queryParameters.ContainsField(x, y));
                }

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

            this.BuildingResourceCollection = true;
        }

        private void InitializeResourcePathContextBuilder()
        {
            var clrResourceCollectionCount = this.ClrResourceCollection.Count;
            this.LazyResourcePathContextBuilders = new Lazy<IReadOnlyList<ResourcePathContextBuilder<TBuilder>>>(() =>
            {
                var resourcePathContextBuilders = new List<ResourcePathContextBuilder<TBuilder>>(clrResourceCollectionCount);
                for (var i = 0; i < clrResourceCollectionCount; i++)
                {
                    resourcePathContextBuilders.Add(new ResourcePathContextBuilder<TBuilder>(this.Builder, this.ServiceModel, this.ClrResourceType));
                }

                return resourcePathContextBuilders;
            });
        }
        #endregion
    }

    internal abstract class ResourceCollectionBuilder<TBuilder, TResource> : ResourceCollectionBuilder<TBuilder>, IResourceBuilder<TBuilder, TResource>
        where TResource : class
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceBuilder<TBuilder, TResource> Implementation
        public new IRelationshipsBuilder<TBuilder, TResource> Relationships()
        {
            var relationshipsBuilder = this.NotBuildingResourceCollection
                ? (IRelationshipsBuilder<TBuilder, TResource>)(new NullRelationshipsBuilder<TBuilder, TResource>(this.Builder))
                : (IRelationshipsBuilder<TBuilder, TResource>)(new RelationshipsCollectionBuilder<TBuilder, TResource>(this.Builder, this.ServiceModel, this.DomReadWriteResourceCollection, this.ClrResourceCollection.Cast<TResource>().ToList()));
            return relationshipsBuilder;
        }

        public new IResourceLinksBuilder<TBuilder, TResource> Links()
        {
            var linksBuilder = this.NotBuildingResourceCollection
                ? (IResourceLinksBuilder<TBuilder, TResource>)(new NullResourceLinksBuilder<TBuilder, TResource>(this.Builder))
                : (IResourceLinksBuilder<TBuilder, TResource>)(new ResourceLinksCollectionBuilder<TBuilder, TResource>(this.Builder, this.DomReadWriteResourceCollection, this.ClrResourceCollection.Cast<TResource>().ToList()));
            return linksBuilder;
        }
        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected ResourceCollectionBuilder(DocumentBuilder parentBuilder, IContainerNode<DomNodeType> domContainerNode, Type clrResourceType, IEnumerable<TResource> clrResourceCollection)
            : base(parentBuilder, domContainerNode, clrResourceType, clrResourceCollection)
        {
        }
        #endregion
    }
}
