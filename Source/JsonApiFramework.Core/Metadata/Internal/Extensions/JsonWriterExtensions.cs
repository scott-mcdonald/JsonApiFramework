// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.Metadata.Internal
{
    /// <summary>Extension methods specific to metadata for the JSON.NET JsonWriter class.</summary>
    internal static class JsonWriterExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Write Array Methods
        public static void WriteAttributeInfoCollectionArray(this JsonWriter writer, JsonSerializer serializer, IEnumerable<IAttributeInfo> attributeInfoCollection)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(attributeInfoCollection != null);

            writer.WriteStartArray();

            foreach (var attributeInfo in attributeInfoCollection)
            {
                writer.WriteAttributeInfoObject(serializer, attributeInfo);
            }

            writer.WriteEndArray();
        }

        public static void WriteComplexTypesArray(this JsonWriter writer, JsonSerializer serializer, IEnumerable<IComplexType> complexTypes)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(complexTypes != null);

            writer.WriteStartArray();

            foreach (var complexType in complexTypes)
            {
                writer.WriteComplexTypeObject(serializer, complexType);
            }

            writer.WriteEndArray();
        }

        public static void WriteRelationshipInfoCollectionArray(this JsonWriter writer, JsonSerializer serializer, IEnumerable<IRelationshipInfo> relationshipInfoCollection)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(relationshipInfoCollection != null);

            writer.WriteStartArray();

            foreach (var relationshipInfo in relationshipInfoCollection)
            {
                writer.WriteRelationshipInfoObject(serializer, relationshipInfo);
            }

            writer.WriteEndArray();
        }

        public static void WriteResourceTypesArray(this JsonWriter writer, JsonSerializer serializer, IEnumerable<IResourceType> resourceTypes)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(resourceTypes != null);

            writer.WriteStartArray();

            foreach (var resourceType in resourceTypes)
            {
                writer.WriteResourceTypeObject(serializer, resourceType);
            }

            writer.WriteEndArray();
        }
        #endregion

        #region Write Object Methods
        public static void WriteAttributeInfoObject(this JsonWriter writer, JsonSerializer serializer, IAttributeInfo attributeInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(attributeInfo != null);

            writer.WriteStartObject();

            writer.WriteStringProperty(serializer, nameof(IAttributeInfo.ApiAttributeName), attributeInfo.ApiAttributeName);
            writer.WriteStringProperty(serializer, nameof(IAttributeInfo.ClrPropertyName), attributeInfo.ClrPropertyName);
            writer.WriteTypeProperty(serializer, nameof(IAttributeInfo.ClrPropertyType), attributeInfo.ClrPropertyType);
            writer.WriteBoolProperty(serializer, nameof(IAttributeInfo.IsComplexType), attributeInfo.IsComplexType);
            writer.WriteBoolProperty(serializer, nameof(IAttributeInfo.IsCollection), attributeInfo.IsCollection);
            writer.WriteTypeProperty(serializer, nameof(IAttributeInfo.ClrCollectionItemType), attributeInfo.ClrCollectionItemType);
            writer.WriteBoolProperty(serializer, nameof(IAttributeInfo.IsCollectionItemComplexType), attributeInfo.IsCollectionItemComplexType);

            writer.WriteEndObject();
        }

        public static void WriteAttributesInfoObject(this JsonWriter writer, JsonSerializer serializer, IAttributesInfo attributesInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(attributesInfo != null);

            writer.WriteStartObject();

            writer.WriteAttributeInfoCollectionProperty(serializer, nameof(IAttributesInfo.AttributeInfoCollection), attributesInfo.AttributeInfoCollection);

            writer.WriteEndObject();
        }

        public static void WriteComplexTypeObject(this JsonWriter writer, JsonSerializer serializer, IComplexType complexType)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(complexType != null);

            writer.WriteStartObject();

            writer.WriteTypeProperty(serializer, nameof(ITypeBase.ClrType), complexType.ClrType);
            writer.WriteAttributesInfoProperty(serializer, nameof(ITypeBase.AttributesInfo), complexType.AttributesInfo);

            writer.WriteEndObject();
        }

        public static void WriteLinksInfoObject(this JsonWriter writer, JsonSerializer serializer, ILinksInfo linksInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(linksInfo != null);

            writer.WriteStartObject();

            writer.WriteStringProperty(serializer, nameof(ILinksInfo.ClrPropertyName), linksInfo.ClrPropertyName);

            writer.WriteEndObject();
        }

        public static void WriteRelationshipInfoObject(this JsonWriter writer, JsonSerializer serializer, IRelationshipInfo relationshipInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(relationshipInfo != null);

            writer.WriteStartObject();

            writer.WriteStringProperty(serializer, nameof(IRelationshipInfo.Rel), relationshipInfo.Rel);
            writer.WriteEnumProperty<RelationshipCardinality>(serializer, nameof(IRelationshipInfo.ToCardinality), relationshipInfo.ToCardinality);
            writer.WriteTypeProperty(serializer, nameof(IRelationshipInfo.ToClrType), relationshipInfo.ToClrType);

            writer.WriteEndObject();
        }

        public static void WriteRelationshipsInfoObject(this JsonWriter writer, JsonSerializer serializer, IRelationshipsInfo relationshipsInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(relationshipsInfo != null);

            writer.WriteStartObject();

            writer.WriteStringProperty(serializer, nameof(IRelationshipsInfo.ClrPropertyName), relationshipsInfo.ClrPropertyName);
            writer.WriteRelationshipInfoCollectionProperty(serializer, nameof(IRelationshipsInfo.RelationshipInfoCollection), relationshipsInfo.RelationshipInfoCollection);

            writer.WriteEndObject();
        }

        public static void WriteResourceIdentityInfoObject(this JsonWriter writer, JsonSerializer serializer, IResourceIdentityInfo resourceIdentityInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(resourceIdentityInfo != null);

            writer.WriteStartObject();

            writer.WriteStringProperty(serializer, nameof(IResourceIdentityInfo.ApiType), resourceIdentityInfo.ApiType);
            writer.WriteStringProperty(serializer, nameof(IResourceIdentityInfo.ClrIdPropertyName), resourceIdentityInfo.ClrIdPropertyName);
            writer.WriteTypeProperty(serializer, nameof(IResourceIdentityInfo.ClrIdPropertyType), resourceIdentityInfo.ClrIdPropertyType);

            writer.WriteEndObject();
        }

        public static void WriteResourceTypeObject(this JsonWriter writer, JsonSerializer serializer, IResourceType resourceType)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(resourceType != null);

            writer.WriteStartObject();

            writer.WriteTypeProperty(serializer, nameof(ITypeBase.ClrType), resourceType.ClrType);
            writer.WriteResourceIdentityInfoProperty(serializer, nameof(IResourceType.ResourceIdentityInfo), resourceType.ResourceIdentityInfo);
            writer.WriteAttributesInfoProperty(serializer, nameof(ITypeBase.AttributesInfo), resourceType.AttributesInfo);
            writer.WriteRelationshipsInfoProperty(serializer, nameof(IResourceType.RelationshipsInfo), resourceType.RelationshipsInfo);
            writer.WriteLinksInfoProperty(serializer, nameof(IResourceType.LinksInfo), resourceType.LinksInfo);

            writer.WriteEndObject();
        }

        public static void WriteServiceModelObject(this JsonWriter writer, JsonSerializer serializer, IServiceModel serviceModel)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(serviceModel != null);

            writer.WriteStartObject();

            writer.WriteStringProperty(serializer, nameof(IServiceModel.Name), serviceModel.Name);
            writer.WriteComplexTypesProperty(serializer, nameof(IServiceModel.ComplexTypes), serviceModel.ComplexTypes);
            writer.WriteResourceTypesProperty(serializer, nameof(IServiceModel.ResourceTypes), serviceModel.ResourceTypes);

            writer.WriteEndObject();
        }
        #endregion

        #region Write Property Methods
        public static void WriteAttributeInfoCollectionProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, IEnumerable<IAttributeInfo> attributeInfoCollection)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);
            Contract.Requires(attributeInfoCollection != null);

            writer.WritePropertyName(propertyName);
            writer.WriteAttributeInfoCollectionArray(serializer, attributeInfoCollection);
        }

        public static void WriteAttributesInfoProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, IAttributesInfo attributesInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);
            Contract.Requires(attributesInfo != null);

            writer.WritePropertyName(propertyName);
            writer.WriteAttributesInfoObject(serializer, attributesInfo);
        }

        public static void WriteComplexTypesProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, IEnumerable<IComplexType> complexTypes)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);
            Contract.Requires(complexTypes != null);

            writer.WritePropertyName(propertyName);
            writer.WriteComplexTypesArray(serializer, complexTypes);
        }

        public static void WriteLinksInfoProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, ILinksInfo linksInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            if (linksInfo == null)
            {
                writer.WriteNullProperty(serializer, propertyName);
                return;
            }

            writer.WritePropertyName(propertyName);
            writer.WriteLinksInfoObject(serializer, linksInfo);
        }

        public static void WriteRelationshipInfoCollectionProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, IEnumerable<IRelationshipInfo> relationshipInfoCollection)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);
            Contract.Requires(relationshipInfoCollection != null);

            writer.WritePropertyName(propertyName);
            writer.WriteRelationshipInfoCollectionArray(serializer, relationshipInfoCollection);
        }

        public static void WriteRelationshipsInfoProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, IRelationshipsInfo relationshipsInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            if (relationshipsInfo == null)
            {
                writer.WriteNullProperty(serializer, propertyName);
                return;
            }

            writer.WritePropertyName(propertyName);
            writer.WriteRelationshipsInfoObject(serializer, relationshipsInfo);
        }

        public static void WriteResourceIdentityInfoProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, IResourceIdentityInfo resourceIdentityInfo)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);

            if (resourceIdentityInfo == null)
            {
                writer.WriteNullProperty(serializer, propertyName);
                return;
            }

            writer.WritePropertyName(propertyName);
            writer.WriteResourceIdentityInfoObject(serializer, resourceIdentityInfo);
        }

        public static void WriteResourceTypesProperty(this JsonWriter writer, JsonSerializer serializer, string propertyName, IEnumerable<IResourceType> resourceTypes)
        {
            Contract.Requires(writer != null);
            Contract.Requires(serializer != null);
            Contract.Requires(String.IsNullOrWhiteSpace(propertyName) == false);
            Contract.Requires(resourceTypes != null);

            writer.WritePropertyName(propertyName);
            writer.WriteResourceTypesArray(serializer, resourceTypes);
        }
        #endregion
    }
}
