// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Internal.Dom;
using JsonApiFramework.JsonApi;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Server.Internal
{
    internal class DocumentBuilderContext
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DocumentBuilderContext(Uri currentRequestUrl)
        {
            this.CurrentRequestUrl = currentRequestUrl;

            this.DomReadWriteResourceDictionary = new Dictionary<ResourceIdentifier, DomReadWriteResource>();
            this.ResourceLinkageDictionary = new Dictionary<ResourceLinkageKey, ResourceLinkage>();
        }

        public DocumentBuilderContext(string currentRequestUrl)
            : this(new Uri(currentRequestUrl))
        { }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public Uri CurrentRequestUrl { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public void AddDomReadWriteResource(ResourceIdentifier domResourceKey, DomReadWriteResource domReadWriteResource)
        {
            Contract.Requires(domResourceKey != null);
            Contract.Requires(domReadWriteResource != null);

            this.DomReadWriteResourceDictionary.Add(domResourceKey, domReadWriteResource);
        }

        public bool ContainsDomReadWriteResource(ResourceIdentifier domResourceKey)
        {
            Contract.Requires(domResourceKey != null);

            return this.DomReadWriteResourceDictionary.ContainsKey(domResourceKey);
        }

        public void AddResourceLinkage<TFromResource, TToResource>(IServiceModel serviceModel, IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(toOneIncludedResource != null);

            // Create ResourceLinkageKey from ToOneIncludedResource.
            var fromClrResourceType = typeof(TFromResource);
            var fromResourceType = serviceModel.GetResourceType(fromClrResourceType);

            var fromClrResource = toOneIncludedResource.FromResource;
            var fromApiResourceIdentifier = fromResourceType.GetApiResourceIdentifier(fromClrResource);

            var fromApiRel = toOneIncludedResource.FromRel;

            var resourceLinkageKey = new ResourceLinkageKey(fromApiResourceIdentifier, fromApiRel);

            // Create ResourceLinkage from ToOneIncludedResource
            var toApiResourceIdentifier = default(ResourceIdentifier);

            var toClrResource = toOneIncludedResource.ToResource;
            if (toClrResource != null)
            {
                var toClrResourceType = typeof(TToResource);
                var toResourceType = serviceModel.GetResourceType(toClrResourceType);

                toApiResourceIdentifier = toResourceType.GetApiResourceIdentifier(toClrResource);
            }

            var resourceLinkage = new ResourceLinkage(toApiResourceIdentifier);

            // Add ResourceLinkage to this DocumentBuilderContext
            this.AddResourceLinkage(resourceLinkageKey, resourceLinkage);
        }

        public void AddResourceLinkage<TFromResource, TToResource>(IServiceModel serviceModel, IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(toManyIncludedResources != null);

            // Create ResourceLinkageKey from ToManyIncludedResources.
            var fromClrResourceType = typeof(TFromResource);
            var fromResourceType = serviceModel.GetResourceType(fromClrResourceType);

            var fromClrResource = toManyIncludedResources.FromResource;
            var fromApiResourceIdentifier = fromResourceType.GetApiResourceIdentifier(fromClrResource);

            var fromApiRel = toManyIncludedResources.FromRel;

            var resourceLinkageKey = new ResourceLinkageKey(fromApiResourceIdentifier, fromApiRel);

            // Create ResourceLinkage from ToManyIncludedResources.
            var toApiResourceIdentifierCollection = Enumerable.Empty<ResourceIdentifier>()
                                                              .ToList();

            var toClrResourceCollection = toManyIncludedResources.ToResourceCollection;
            if (toClrResourceCollection != null)
            {
                var toClrResourceType = typeof(TToResource);
                var toResourceType = serviceModel.GetResourceType(toClrResourceType);

                toApiResourceIdentifierCollection = toClrResourceCollection.Select(toResourceType.GetApiResourceIdentifier)
                                                                        .ToList();
            }

            var resourceLinkage = new ResourceLinkage(toApiResourceIdentifierCollection);

            // Add ResourceLinkage to this DocumentBuilderContext
            this.AddResourceLinkage(resourceLinkageKey, resourceLinkage);
        }

        public bool TryGetResourceLinkage(ResourceLinkageKey resourceLinkageKey, out ResourceLinkage resourceLinkage)
        { return this.ResourceLinkageDictionary.TryGetValue(resourceLinkageKey, out resourceLinkage); }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDictionary<ResourceIdentifier, DomReadWriteResource> DomReadWriteResourceDictionary { get; set; }
        private IDictionary<ResourceLinkageKey, ResourceLinkage> ResourceLinkageDictionary { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourceLinkage(ResourceLinkageKey resourceLinkageKey, ResourceLinkage resourceLinkageNew)
        {
            Contract.Requires(resourceLinkageKey != null);
            Contract.Requires(resourceLinkageNew != null);

            ResourceLinkage resourceLinkageExisting;
            if (this.ResourceLinkageDictionary.TryGetValue(resourceLinkageKey, out resourceLinkageExisting) == false)
            {
                // Add initial new resource linkage
                this.ResourceLinkageDictionary.Add(resourceLinkageKey, resourceLinkageNew);
                return;
            }

            // Merge existing and new resource linkage
            var resourceLinkageExistingType = resourceLinkageExisting.Type;
            switch (resourceLinkageExistingType)
            {
                case ResourceLinkageType.ToOneResourceLinkage:
                    {
                        this.ResourceLinkageDictionary.Remove(resourceLinkageKey);
                        this.ResourceLinkageDictionary.Add(resourceLinkageKey, resourceLinkageNew);
                    }
                    break;

                case ResourceLinkageType.ToManyResourceLinkage:
                    {
                        var toManyResourceLinkage = resourceLinkageExisting.ToManyResourceLinkage
                                                                           .SafeToList();
                        toManyResourceLinkage.AddRange(resourceLinkageNew.ToManyResourceLinkage);

                        toManyResourceLinkage = toManyResourceLinkage.Distinct()
                                                                     .SafeToList();

                        var resourceLinkageMerged = new ResourceLinkage(toManyResourceLinkage);

                        this.ResourceLinkageDictionary.Remove(resourceLinkageKey);
                        this.ResourceLinkageDictionary.Add(resourceLinkageKey, resourceLinkageMerged);
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
