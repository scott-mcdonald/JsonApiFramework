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
        public DocumentBuilderContext(Uri currentRequestUrl, QueryParameters queryParameters, bool sparseFieldsetsEnabled)
        {
            this.CurrentRequestUrl      = currentRequestUrl;
            this.QueryParameters        = queryParameters;
            this.SparseFieldsetsEnabled = sparseFieldsetsEnabled;

            this.DomReadWriteResourceDictionary = new Dictionary<ResourceIdentifier, DomReadWriteResource>();
            this.ApiResourceLinkageDictionary   = new Dictionary<ApiResourceLinkageKey, ApiResourceLinkage>();
        }

        public DocumentBuilderContext(string currentRequestUrl, QueryParameters queryParameters, bool sparseFieldsetsEnabled)
            : this(new Uri(currentRequestUrl), queryParameters, sparseFieldsetsEnabled)
        {
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public Uri             CurrentRequestUrl      { get; }
        public QueryParameters QueryParameters        { get; }
        public bool            SparseFieldsetsEnabled { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public void AddDomReadWriteResource(ResourceIdentifier domResourceKey, DomReadWriteResource domReadWriteResource)
        {
            Contract.Requires(domResourceKey       != null);
            Contract.Requires(domReadWriteResource != null);

            this.DomReadWriteResourceDictionary.Add(domResourceKey, domReadWriteResource);
        }

        public bool ContainsDomReadWriteResource(ResourceIdentifier domResourceKey)
        {
            Contract.Requires(domResourceKey != null);

            return this.DomReadWriteResourceDictionary.ContainsKey(domResourceKey);
        }

        public void AddResourceLinkage<TFromResource, TToResource>(IServiceModel serviceModel, IToOneIncludedResource<TFromResource, TToResource> toOneIncludedResource)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(serviceModel          != null);
            Contract.Requires(toOneIncludedResource != null);

            // Create ResourceLinkageKey from ToOneIncludedResource.
            var fromClrResourceType = typeof(TFromResource);
            var fromResourceType    = serviceModel.GetResourceType(fromClrResourceType);

            var fromClrResource           = toOneIncludedResource.FromResource;
            var fromApiResourceIdentifier = fromResourceType.GetApiResourceIdentifier(fromClrResource);

            var fromApiRel = toOneIncludedResource.FromRel;

            var apiResourceLinkageKey = new ApiResourceLinkageKey(fromApiResourceIdentifier, fromApiRel);

            // Create ResourceLinkage from ToOneIncludedResource
            var toApiResourceIdentifier = default(ResourceIdentifier);

            var toClrResource = toOneIncludedResource.ToResource;
            if (toClrResource != null)
            {
                var toClrResourceType = typeof(TToResource);
                var toResourceType    = serviceModel.GetResourceType(toClrResourceType);

                toApiResourceIdentifier = toResourceType.GetApiResourceIdentifier(toClrResource);
            }

            var apiResourceLinkage = new ApiResourceLinkage(toApiResourceIdentifier);

            // Add ResourceLinkage to this DocumentBuilderContext
            this.AddResourceLinkage(apiResourceLinkageKey, apiResourceLinkage);
        }

        public void AddResourceLinkage<TFromResource, TToResource>(IServiceModel serviceModel, IToManyIncludedResources<TFromResource, TToResource> toManyIncludedResources)
            where TFromResource : class
            where TToResource : class
        {
            Contract.Requires(serviceModel            != null);
            Contract.Requires(toManyIncludedResources != null);

            // Create ResourceLinkageKey from ToManyIncludedResources.
            var fromClrResourceType = typeof(TFromResource);
            var fromResourceType    = serviceModel.GetResourceType(fromClrResourceType);

            var fromClrResource           = toManyIncludedResources.FromResource;
            var fromApiResourceIdentifier = fromResourceType.GetApiResourceIdentifier(fromClrResource);

            var fromApiRel = toManyIncludedResources.FromRel;

            var apiResourceLinkageKey = new ApiResourceLinkageKey(fromApiResourceIdentifier, fromApiRel);

            // Create ResourceLinkage from ToManyIncludedResources.
            var toApiResourceIdentifierCollection = Enumerable.Empty<ResourceIdentifier>()
                                                              .ToList();

            var toClrResourceCollection = toManyIncludedResources.ToResourceCollection;
            if (toClrResourceCollection != null)
            {
                var toClrResourceType = typeof(TToResource);
                var toResourceType    = serviceModel.GetResourceType(toClrResourceType);

                toApiResourceIdentifierCollection = toClrResourceCollection.Select(toResourceType.GetApiResourceIdentifier)
                                                                           .ToList();
            }

            var apiResourceLinkage = new ApiResourceLinkage(toApiResourceIdentifierCollection);

            // Add ResourceLinkage to this DocumentBuilderContext
            this.AddResourceLinkage(apiResourceLinkageKey, apiResourceLinkage);
        }

        public bool TryGetResourceLinkage(ApiResourceLinkageKey apiResourceLinkageKey, out ApiResourceLinkage apiResourceLinkage)
        {
            return this.ApiResourceLinkageDictionary.TryGetValue(apiResourceLinkageKey, out apiResourceLinkage);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDictionary<ResourceIdentifier, DomReadWriteResource>  DomReadWriteResourceDictionary { get; set; }
        private IDictionary<ApiResourceLinkageKey, ApiResourceLinkage> ApiResourceLinkageDictionary   { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void AddResourceLinkage(ApiResourceLinkageKey apiResourceLinkageKey, ApiResourceLinkage apiResourceLinkageNew)
        {
            Contract.Requires(apiResourceLinkageKey != null);
            Contract.Requires(apiResourceLinkageNew != null);

            if (this.ApiResourceLinkageDictionary.TryGetValue(apiResourceLinkageKey, out var apiResourceLinkageExisting) == false)
            {
                // Add initial new resource linkage
                this.ApiResourceLinkageDictionary.Add(apiResourceLinkageKey, apiResourceLinkageNew);
                return;
            }

            // Merge existing and new resource linkage
            var apiResourceLinkageExistingType = apiResourceLinkageExisting.Type;
            switch (apiResourceLinkageExistingType)
            {
                case ApiResourceLinkageType.ToOneResourceLinkage:
                {
                    this.ApiResourceLinkageDictionary.Remove(apiResourceLinkageKey);
                    this.ApiResourceLinkageDictionary.Add(apiResourceLinkageKey, apiResourceLinkageNew);
                }
                    break;

                case ApiResourceLinkageType.ToManyResourceLinkage:
                {
                    var toManyResourceLinkage = apiResourceLinkageExisting.ToManyResourceLinkage
                                                                          .SafeToList();
                    toManyResourceLinkage.AddRange(apiResourceLinkageNew.ToManyResourceLinkage);

                    toManyResourceLinkage = toManyResourceLinkage.Distinct()
                                                                 .SafeToList();

                    var apiResourceLinkageMerged = new ApiResourceLinkage(toManyResourceLinkage);

                    this.ApiResourceLinkageDictionary.Remove(apiResourceLinkageKey);
                    this.ApiResourceLinkageDictionary.Add(apiResourceLinkageKey, apiResourceLinkageMerged);
                }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
