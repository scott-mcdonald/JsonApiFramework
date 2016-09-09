// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using JsonApiFramework.Json;

using Newtonsoft.Json;

namespace JsonApiFramework.ServiceModel.Internal
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ServiceModel : JsonObject
        , IServiceModel
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ServiceModel(IEnumerable<IComplexType> complexTypes, IEnumerable<IResourceType> resourceTypes)
        {
            this.ComplexTypes = complexTypes.SafeToList();
            this.ResourceTypes = resourceTypes.SafeToList();

            this.Initialize();
        }

        public ServiceModel(IEnumerable<IResourceType> resourceTypes)
            : this(null, resourceTypes)
        { }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IServiceModel Implementation
        [JsonProperty] public IEnumerable<IComplexType> ComplexTypes { get; private set; }
        [JsonProperty] public IEnumerable<IResourceType> ResourceTypes { get; private set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IServiceModel Implementation
        public IComplexType GetComplexType(Type clrComplexType)
        {
            if (clrComplexType != null)
            {
                IComplexType resourceType;
                if (this.TryGetComplexType(clrComplexType, out resourceType))
                    return resourceType;
            }

            // No complex type found for the given CLR type.
            var clrComplexTypeName = clrComplexType != null ? clrComplexType.Name : String.Empty;
            var serviceModelDescription = typeof(ServiceModel).Name;
            var resourceTypeDescription = "{0} [clrType={1}]".FormatWith(typeof(ComplexType).Name, clrComplexTypeName);
            var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                         .FormatWith(serviceModelDescription, resourceTypeDescription);
            throw new ServiceModelException(detail);
        }

        public IResourceType GetResourceType(string apiResourceType)
        {
            if (String.IsNullOrWhiteSpace(apiResourceType) == false)
            {
                IResourceType resourceType;
                if (this.TryGetResourceType(apiResourceType, out resourceType))
                    return resourceType;
            }

            // No resource type found for the given JSON API type.
            var apiResourceTypeName = apiResourceType ?? String.Empty;
            var serviceModelDescription = typeof(ServiceModel).Name;
            var resourceTypeDescription = "{0} [apiType={1}]".FormatWith(typeof(ResourceType).Name, apiResourceTypeName);
            var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                         .FormatWith(serviceModelDescription, resourceTypeDescription);
            throw new ServiceModelException(detail);
        }

        public IResourceType GetResourceType(Type clrResourceType)
        {
            if (clrResourceType != null)
            {
                IResourceType resourceType;
                if (this.TryGetResourceType(clrResourceType, out resourceType))
                    return resourceType;
            }

            // No resource type found for the given CLR type.
            var clrResourceTypeName = clrResourceType != null ? clrResourceType.Name : String.Empty;
            var serviceModelDescription = typeof(ServiceModel).Name;
            var resourceTypeDescription = "{0} [clrType={1}]".FormatWith(typeof(ResourceType).Name, clrResourceTypeName);
            var detail = CoreErrorStrings.ServiceModelExceptionDetailMissingMetadata
                                         .FormatWith(serviceModelDescription, resourceTypeDescription);
            throw new ServiceModelException(detail);
        }

        public bool TryGetComplexType(Type clrComplexType, out IComplexType complexType)
        {
            complexType = null;
            return clrComplexType != null
                && this.ClrTypeToComplexTypeDictionary != null
                && this.ClrTypeToComplexTypeDictionary.TryGetValue(clrComplexType, out complexType);
        }

        public bool TryGetResourceType(string apiResourceType, out IResourceType resourceType)
        {
            resourceType = null;
            return String.IsNullOrWhiteSpace(apiResourceType) == false
                && this.ApiTypeToResourceTypeDictionary != null
                && this.ApiTypeToResourceTypeDictionary.TryGetValue(apiResourceType, out resourceType);
        }

        public bool TryGetResourceType(Type clrResourceType, out IResourceType resourceType)
        {
            resourceType = null;
            return clrResourceType != null
                && this.ClrTypeToResourceTypeDictionary != null
                && this.ClrTypeToResourceTypeDictionary.TryGetValue(clrResourceType, out resourceType);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Fields
        public static readonly ServiceModel Empty = new ServiceModel(null);
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ServiceModel()
        { }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Methods
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.ComplexTypes = this.ComplexTypes.SafeToList();
            this.ResourceTypes = this.ResourceTypes.SafeToList();

            this.Initialize();
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IReadOnlyDictionary<Type, IComplexType> ClrTypeToComplexTypeDictionary { get; set; }

        private IReadOnlyDictionary<string, IResourceType> ApiTypeToResourceTypeDictionary { get; set; }
        private IReadOnlyDictionary<Type, IResourceType> ClrTypeToResourceTypeDictionary { get; set; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void Initialize()
        {
            this.InitializeDictionaryProperties();
            this.InitializeComplexAndResourceTypes();
        }

        private void InitializeDictionaryProperties()
        {
            this.ClrTypeToComplexTypeDictionary = this.ComplexTypes
                                                      .ToDictionary(x => x.ClrType);

            this.ApiTypeToResourceTypeDictionary = this.ResourceTypes
                                                       .ToDictionary(x => x.ResourceIdentityInfo.ApiType);

            this.ClrTypeToResourceTypeDictionary = this.ResourceTypes
                                                       .ToDictionary(x => x.ClrType);
        }

        private void InitializeComplexAndResourceTypes()
        {
            if (this.ClrTypeToComplexTypeDictionary == null || this.ClrTypeToComplexTypeDictionary.Count == 0)
                return;

            foreach (var complexType in this.ClrTypeToComplexTypeDictionary.Values)
            {
                complexType.Initialize(this.ClrTypeToComplexTypeDictionary);
            }

            foreach (var resourceType in this.ClrTypeToResourceTypeDictionary.Values)
            {
                resourceType.Initialize(this.ClrTypeToComplexTypeDictionary);
            }
        }
        #endregion
    }
}