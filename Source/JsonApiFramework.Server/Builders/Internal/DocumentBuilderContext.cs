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

        public void AddResourceLinkage(ResourceLinkageKey resourceLinkageKey, ResourceLinkage resourceLinkage)
        {
            Contract.Requires(resourceLinkageKey != null);
            Contract.Requires(resourceLinkage != null);

            this.ResourceLinkageDictionary.Add(resourceLinkageKey, resourceLinkage);
        }

        public void AddResourceLinkage<TFromResource, TToResource>(IServiceModel serviceModel, IToOneResourceLinkage<TFromResource, TToResource> toOneResourceLinkage)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(toOneResourceLinkage != null);

            // Create ResourceLinkageKey from ToOneResourceLinkage.
            var fromClrResourceType = typeof(TFromResource);
            var fromResourceType = serviceModel.GetResourceType(fromClrResourceType);

            var fromClrResource = toOneResourceLinkage.FromResource;
            var fromApiResourceIdentifier = fromResourceType.GetApiResourceIdentifier(fromClrResource);

            var fromApiRel = toOneResourceLinkage.FromRel;

            var resourceLinkageKey = new ResourceLinkageKey(fromApiResourceIdentifier, fromApiRel);

            // Create ResourceLinkage from ToOneResourceLinkage
            var toApiResourceIdentifier = default(ResourceIdentifier);

            var toClrResource = toOneResourceLinkage.ToResource;
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

        public void AddResourceLinkage<TFromResource, TToResource>(IServiceModel serviceModel, IToManyResourceLinkage<TFromResource, TToResource> toManyResourceLinkage)
            where TFromResource : class, IResource
            where TToResource : class, IResource
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(toManyResourceLinkage != null);

            // Create ResourceLinkageKey from ToManyResourceLinkage.
            var fromClrResourceType = typeof(TFromResource);
            var fromResourceType = serviceModel.GetResourceType(fromClrResourceType);

            var fromClrResource = toManyResourceLinkage.FromResource;
            var fromApiResourceIdentifier = fromResourceType.GetApiResourceIdentifier(fromClrResource);

            var fromApiRel = toManyResourceLinkage.FromRel;

            var resourceLinkageKey = new ResourceLinkageKey(fromApiResourceIdentifier, fromApiRel);

            // Create ResourceLinkage from ToManyResourceLinkage.
            var toApiResourceIdentifierCollection = Enumerable.Empty<ResourceIdentifier>()
                                                              .ToList();

            var toClrResourceCollection = toManyResourceLinkage.ToResourceCollection;
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
    }
}
