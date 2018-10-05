// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonApiFramework.ServiceModel
{
    public static class ResourceTypeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
        public static bool IsSingleton(this IResourceType resourceType)
        {
            Contract.Requires(resourceType != null);

            return resourceType.ResourceIdentityInfo.IsSingleton();
        }

        public static void MapApiMetaToClrResource(this IResourceType resourceType, object clrResource, IGetMeta apiGetMeta)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(apiGetMeta != null);

            var apiMeta = apiGetMeta.Meta;
            resourceType.SetClrMeta(clrResource, apiMeta);
        }

        public static void MapApiIdToClrResource(this IResourceType resourceType, object clrResource, IGetResourceIdentity apiGetResourceIdentity)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(apiGetResourceIdentity != null);

            var apiId = apiGetResourceIdentity.Id;
            var clrId = resourceType.ToClrId(apiId);
            resourceType.SetClrId(clrResource, clrId);
        }

        public static void MapApiAttributesToClrResource(this IResourceType resourceType, object clrResource, IGetAttributes apiGetAttributes, IServiceModel serviceModel)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(apiGetAttributes != null);
            Contract.Requires(serviceModel != null);

            var attributesInfo = resourceType.AttributesInfo;
            var attributesInfoCollection = attributesInfo.Collection;
            foreach (var attributeInfo in attributesInfoCollection)
            {
                var apiAttribute = attributeInfo.GetApiAttribute(apiGetAttributes);
                if (apiAttribute == null)
                    continue;

                var clrAttribute = apiAttribute.ValueAsObject();

                if (!attributeInfo.IsComplexType)
                {
                    attributeInfo.SetClrProperty(clrResource, clrAttribute);
                    continue;
                }

                // Handle attribute complex type with JSON.NET and IContractResolver.
                JToken clrAttributeAsJToken;
                if (!TypeConverter.TryConvert(clrAttribute, out clrAttributeAsJToken))
                {
                    clrAttributeAsJToken = JToken.FromObject(clrAttribute);
                }

                var clrAttributeClrType = attributeInfo.ClrPropertyType;
                var complexTypesContractResolver = serviceModel.CreateComplexTypesContractResolver();
                var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = complexTypesContractResolver
                    };
                var jsonSerializer = JsonSerializer.Create(jsonSerializerSettings);
                clrAttribute = clrAttributeAsJToken.ToObject(clrAttributeClrType, jsonSerializer);
                attributeInfo.SetClrProperty(clrResource, clrAttribute);
            }
        }

        public static void MapApiRelationshipsToClrResource(this IResourceType resourceType, object clrResource, IGetRelationships apiGetRelationships)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(apiGetRelationships != null);

            var apiRelationships = apiGetRelationships.Relationships;
            resourceType.SetClrRelationships(clrResource, apiRelationships);
        }

        public static void MapApiLinksToClrResource(this IResourceType resourceType, object clrResource, IGetLinks apiGetLinks)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(apiGetLinks != null);

            var apiLinks = apiGetLinks.Links;
            resourceType.SetClrLinks(clrResource, apiLinks);
        }
        #endregion
    }
}
