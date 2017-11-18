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
    internal static class JArrayExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static IEnumerable<IAttributeInfo> ReadAttributeInfoCollectionArray(this JArray jArray, JsonSerializer serializer, Type clrDeclaringType)
        {
            Contract.Requires(jArray != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrDeclaringType != null);

            var attributeInfoCollection = jArray
                .Where(jToken => jToken.Type == JTokenType.Object)
                .Select(jToken =>
                {
                    var jObject = (JObject)jToken;
                    var attribute = jObject.ReadAttributeInfoObject(serializer, clrDeclaringType);
                    return attribute;
                })
                .ToList();
            return attributeInfoCollection;
        }

        public static IEnumerable<IComplexType> ReadComplexTypesArray(this JArray jArray, JsonSerializer serializer)
        {
            Contract.Requires(jArray != null);
            Contract.Requires(serializer != null);

            var complexTypes = jArray
                .Where(jToken => jToken.Type == JTokenType.Object)
                .Select(jToken =>
                {
                    var jObject = (JObject)jToken;
                    var complexType = jObject.ReadComplexTypeObject(serializer);
                    return complexType;
                })
                .ToList();
            return complexTypes;
        }

        public static IEnumerable<IRelationshipInfo> ReadRelationshipInfoCollectionArray(this JArray jArray, JsonSerializer serializer, Type clrDeclaringType)
        {
            Contract.Requires(jArray != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrDeclaringType != null);

            var relationshipInfoCollection = jArray
                .Where(jToken => jToken.Type == JTokenType.Object)
                .Select(jToken =>
                {
                    var jObject = (JObject)jToken;
                    var relationship = jObject.ReadRelationshipInfoObject(serializer, clrDeclaringType);
                    return relationship;
                })
                .ToList();
            return relationshipInfoCollection;
        }

        public static IEnumerable<IResourceType> ReadResourceTypesArray(this JArray jArray, JsonSerializer serializer)
        {
            Contract.Requires(jArray != null);
            Contract.Requires(serializer != null);

            var resourceTypes = jArray
                .Where(jToken => jToken.Type == JTokenType.Object)
                .Select(jToken =>
                {
                    var jObject = (JObject)jToken;
                    var resourceType = jObject.ReadResourceTypeObject(serializer);
                    return resourceType;
                })
                .ToList();
            return resourceTypes;
        }
        #endregion
    }
}
