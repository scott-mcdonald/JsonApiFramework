// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;

namespace JsonApiFramework.ServiceModel
{
    public static class ResourceTypeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extensions Methods
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

        public static void MapApiAttributesToClrResource(this IResourceType resourceType, object clrResource, IGetAttributes apiGetAttributes)
        {
            Contract.Requires(resourceType != null);
            Contract.Requires(clrResource != null);
            Contract.Requires(apiGetAttributes != null);

            var attributes = resourceType.Attributes;
            var attributesCollection = attributes.Collection;
            foreach (var attribute in attributesCollection)
            {
                var apiAttribute = attribute.GetApiAttribute(apiGetAttributes);
                var clrAttribute = apiAttribute != null
                    ? apiAttribute.ValueAsObject()
                    : default(object);
                attribute.SetClrProperty(clrResource, clrAttribute);
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
