// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.Metadata.Internal
{
    /// <summary>Extension methods specific to metadata for the JSON.NET JProperty class.</summary>
    internal static class JPropertyExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static IAttributesInfo ReadAttributesInfoPropertyValue(this JProperty jProperty, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(IAttributesInfo);

            var jObject = (JObject)jToken;
            var attributesInfo = jObject.ReadAttributesInfoObject(serializer, clrObjectType);

            return attributesInfo;
        }

        public static IEnumerable<IAttributeInfo> ReadAttributeInfoCollectionPropertyValue(this JProperty jProperty, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Array)
                return Enumerable.Empty<IAttributeInfo>();

            var jArray = (JArray)jToken;
            var attributeInfoCollection = jArray.ReadAttributeInfoCollectionArray(serializer, clrObjectType);

            return attributeInfoCollection;
        }

        public static IClrPropertyBinding ReadClrPropertyBindingPropertyValue(this JProperty jProperty, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(IClrPropertyBinding);

            var jObject = (JObject)jToken;
            var clrPropertyBinding = jObject.ReadClrPropertyBindingObject(serializer, clrObjectType);

            return clrPropertyBinding;
        }

        public static IEnumerable<IComplexType> ReadComplexTypesPropertyValue(this JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Array)
                return Enumerable.Empty<IComplexType>();

            var jArray = (JArray)jToken;
            var complexTypes = jArray.ReadComplexTypesArray(serializer);

            return complexTypes;
        }

        public static ILinksInfo ReadLinksInfoPropertyValue(this JProperty jProperty, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(ILinksInfo);

            var jObject = (JObject)jToken;
            var linksInfo = jObject.ReadLinksInfoObject(serializer, clrObjectType);

            return linksInfo;
        }

        public static IEnumerable<IRelationshipInfo> ReadRelationshipInfoCollectionPropertyValue(this JProperty jProperty, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Array)
                return Enumerable.Empty<IRelationshipInfo>();

            var jArray = (JArray)jToken;
            var relationshipInfoCollection = jArray.ReadRelationshipInfoCollectionArray(serializer, clrObjectType);

            return relationshipInfoCollection;
        }

        public static IRelationshipsInfo ReadRelationshipsInfoPropertyValue(this JProperty jProperty, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(IRelationshipsInfo);

            var jObject = (JObject)jToken;
            var relationshipsInfo = jObject.ReadRelationshipsInfoObject(serializer, clrObjectType);

            return relationshipsInfo;
        }

        public static IResourceIdentityInfo ReadResourceIdentityInfoPropertyValue(this JProperty jProperty, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Object)
                return default(IResourceIdentityInfo);

            var jObject = (JObject)jToken;
            var resourceIdentityInfo = jObject.ReadResourceIdentityInfoObject(serializer, clrObjectType);

            return resourceIdentityInfo;
        }

        public static IEnumerable<IResourceType> ReadResourceTypesPropertyValue(this JProperty jProperty, JsonSerializer serializer)
        {
            Contract.Requires(jProperty != null);
            Contract.Requires(serializer != null);

            var jToken = jProperty.Value;
            var jTokenType = jToken.Type;
            if (jTokenType != JTokenType.Array)
                return Enumerable.Empty<IResourceType>();

            var jArray = (JArray)jToken;
            var resourceTypes = jArray.ReadResourceTypesArray(serializer);

            return resourceTypes;
        }
        #endregion
    }
}
