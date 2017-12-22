// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Properties;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.Metadata.Internal
{
    /// <summary>Extension methods specific to metadata for the JSON.NET JObject class.</summary>
    public static class JObjectExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static IAttributeInfo ReadAttributeInfoObject(this JObject jObject, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var context = new AttributeInfoContext()
            {
                ClrObjectType = clrObjectType
            };
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!AttributeInfoContextInitializerDictionary.TryGetValue(propertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var apiAttributeName = context.ApiAttributeName;
            var clrPropertyBinding = context.ClrPropertyBinding;

            var attributeInfo = Factory.CreateAttributeInfo(apiAttributeName, clrPropertyBinding);
            return attributeInfo;
        }

        public static IAttributesInfo ReadAttributesInfoObject(this JObject jObject, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var context = new AttributesInfoContext
            {
                ClrObjectType = clrObjectType
            };
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!AttributesInfoContextContextInitializerDictionary.TryGetValue(propertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var attributeInfoCollection = context.AttributeInfoCollection;

            var attributesInfo = Factory.CreateAttributesInfo(attributeInfoCollection);
            return attributesInfo;
        }

        public static IClrPropertyBinding ReadClrPropertyBindingObject(this JObject jObject, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var context = new ClrPropertyBindingContext();
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!ClrPropertyBindingContextInitializerDictionary.TryGetValue(propertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var clrPropertyName = context.ClrPropertyName;
            var clrPropertyType = context.ClrPropertyType;

            var clrPropertyBinding = Factory.CreateClrPropertyBinding(clrPropertyName, clrPropertyType, clrObjectType);
            return clrPropertyBinding;
        }

        public static IComplexType ReadComplexTypeObject(this JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var context = new ComplexTypeContext();
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!TypeBaseContextInitializerDictionary.TryGetValue(propertyName, out var typeBaseContextInitializer))
                {
                    if (!ComplexTypeContextInitializerDictionary.TryGetValue(propertyName, out var complexTypeContextInitializer))
                    {
                        // Unknown property
                        continue;
                    }

                    // Known property
                    complexTypeContextInitializer(jProperty, serializer, context);
                    continue;
                }

                // Known property
                typeBaseContextInitializer(jProperty, serializer, context);
            }

            var clrType = context.ClrType;
            if (clrType == null)
            {
                var jsonDescription = "invalid or missing JSON [property={0}]".FormatWith(nameof(ITypeBase.ClrType));
                var detail = CoreErrorStrings.MetadataDeserializationExceptionInvalidJsonDetail
                                             .FormatWith(nameof(IComplexType), jsonDescription);
                throw new MetadataDeserializationException(detail);
            }

            var attributesInfo = context.AttributesInfo;

            var complexType = Factory.CreateComplexType(clrType, attributesInfo);
            return complexType;
        }

        public static ILinksInfo ReadLinksInfoObject(this JObject jObject, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var context = new LinksInfoContext
            {
                ClrObjectType = clrObjectType
            };
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!LinksInfoContextInitializerDictionary.TryGetValue(propertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var clrLinksPropertyBinding = context.ClrLinksPropertyBinding;

            var linksInfo = Factory.CreateLinksInfo(clrLinksPropertyBinding);
            return linksInfo;
        }

        public static IRelationshipInfo ReadRelationshipInfoObject(this JObject jObject, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var context = new RelationshipInfoContext()
            {
                ClrObjectType = clrObjectType
            };
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!RelationshipInfoContextInitializerDictionary.TryGetValue(propertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var apiRel = context.ApiRel;
            var apiCardinality = context.ApiCardinality.GetValueOrDefault();
            var clrToType = context.ClrToType;
            var clrToPropertyBinding = context.ClrToPropertyBinding;

            var relationshipInfo = Factory.CreateRelationshipInfo(apiRel, apiCardinality, clrToType, clrToPropertyBinding);
            return relationshipInfo;
        }

        public static IRelationshipsInfo ReadRelationshipsInfoObject(this JObject jObject, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var context = new RelationshipsInfoContext
            {
                ClrObjectType = clrObjectType
            };
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!RelationshipsInfoContextInitializerDictionary.TryGetValue(propertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var clrRelationshipsPropertyBinding = context.ClrRelationshipsPropertyBinding;
            var relationshipInfoCollection = context.RelationshipInfoCollection;

            var relationshipsInfo = Factory.CreateRelationshipsInfo(clrRelationshipsPropertyBinding, relationshipInfoCollection);
            return relationshipsInfo;
        }

        public static IResourceIdentityInfo ReadResourceIdentityInfoObject(this JObject jObject, JsonSerializer serializer, Type clrObjectType)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);
            Contract.Requires(clrObjectType != null);

            var context = new ResourceIdentityInfoContext
            {
                ClrObjectType = clrObjectType
            };
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!ResourceIdentityInfoContextInitializerDictionary.TryGetValue(propertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var apiType = context.ApiType;
            var clrIdPropertyBinding = context.ClrIdPropertyBinding;

            var resourceIdentityInfo = Factory.CreateResourceIdentityInfo(apiType, clrIdPropertyBinding);
            return resourceIdentityInfo;
        }

        public static IResourceType ReadResourceTypeObject(this JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var context = new ResourceTypeContext();
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!TypeBaseContextInitializerDictionary.TryGetValue(propertyName, out var typeBaseContextInitializer))
                {
                    if (!ResourceTypeContextInitializerDictionary.TryGetValue(propertyName, out var resourceTypecontextInitializer))
                    {
                        // Unknown property
                        continue;
                    }

                    // Known property
                    resourceTypecontextInitializer(jProperty, serializer, context);
                    continue;
                }

                // Known property
                typeBaseContextInitializer(jProperty, serializer, context);
            }

            var clrType = context.ClrType;
            if (clrType == null)
            {
                var jsonDescription = "invalid or missing JSON [property={0}]".FormatWith(nameof(ITypeBase.ClrType));
                var detail = CoreErrorStrings.MetadataDeserializationExceptionInvalidJsonDetail
                                             .FormatWith(nameof(IResourceType), jsonDescription);
                throw new MetadataDeserializationException(detail);
            }

            var resourceIdentityInfo = context.ResourceIdentityInfo;
            if (resourceIdentityInfo == null)
            {
                var jsonDescription = "invalid or missing JSON [property={0}]".FormatWith(nameof(IResourceType.ResourceIdentityInfo));
                var detail = CoreErrorStrings.MetadataDeserializationExceptionInvalidJsonDetail
                                             .FormatWith(nameof(IResourceType), jsonDescription);
                throw new MetadataDeserializationException(detail);
            }

            var attributesInfo = context.AttributesInfo;
            var relationshipsInfo = context.RelationshipsInfo;
            var linksInfo = context.LinksInfo;

            var resourceType = Factory.CreateResourceType(clrType, resourceIdentityInfo, attributesInfo, relationshipsInfo, linksInfo);
            return resourceType;
        }

        public static IServiceModel ReadServiceModelObject(this JObject jObject, JsonSerializer serializer)
        {
            Contract.Requires(jObject != null);
            Contract.Requires(serializer != null);

            var context = new ServiceModelContext();
            foreach (var jProperty in jObject.Properties())
            {
                var propertyName = jProperty.Name;
                if (!ServiceModelContextInitializerDictionary.TryGetValue(propertyName, out var contextInitializer))
                {
                    // Unknown property
                    continue;
                }

                // Known property
                contextInitializer(jProperty, serializer, context);
            }

            var name = context.Name;
            var complexTypes = context.ComplexTypes;
            var resourceTypes = context.ResourceTypes;

            var serviceModel = Factory.CreateServiceModel(name, complexTypes, resourceTypes);
            return serviceModel;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, AttributesInfoContext>> AttributesInfoContextContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, AttributesInfoContext>>
        {
            {nameof(IAttributesInfo.AttributeInfoCollection), (jProperty, serializer, context) => {var attributeInfoCollection = jProperty.ReadAttributeInfoCollectionPropertyValue(serializer, context.ClrObjectType); context.AttributeInfoCollection = attributeInfoCollection;}},
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, AttributeInfoContext>> AttributeInfoContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, AttributeInfoContext>>
        {
            {nameof(IAttributeInfo.ApiAttributeName), (jProperty, serializer, context) => {var apiAttributeName = jProperty.ReadStringPropertyValue(); context.ApiAttributeName = apiAttributeName;}},
            {nameof(IAttributeInfo.ClrPropertyBinding), (jProperty, serializer, context) => {var clrPropertyBinding = jProperty.ReadClrPropertyBindingPropertyValue(serializer, context.ClrObjectType); context.ClrPropertyBinding = clrPropertyBinding;}}
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ClrPropertyBindingContext>> ClrPropertyBindingContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ClrPropertyBindingContext>>
        {
            {nameof(IClrPropertyBinding.ClrPropertyName), (jProperty, serializer, context) => {var clrPropertyName = jProperty.ReadStringPropertyValue(); context.ClrPropertyName = clrPropertyName;}},
            {nameof(IClrPropertyBinding.ClrPropertyType), (jProperty, serializer, context) => {var clrPropertyType = jProperty.ReadTypePropertyValue(); context.ClrPropertyType = clrPropertyType;}},
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ComplexTypeContext>> ComplexTypeContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ComplexTypeContext>>
        { };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, LinksInfoContext>> LinksInfoContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, LinksInfoContext>>
        {
            {nameof(ILinksInfo.ClrLinksPropertyBinding), (jProperty, serializer, context) => {var clrLinksPropertyBinding = jProperty.ReadClrPropertyBindingPropertyValue(serializer, context.ClrObjectType); context.ClrLinksPropertyBinding = clrLinksPropertyBinding;}}
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, RelationshipInfoContext>> RelationshipInfoContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, RelationshipInfoContext>>
        {
            {nameof(IRelationshipInfo.ApiRel), (jProperty, serializer, context) => {var apiRel = jProperty.ReadStringPropertyValue(); context.ApiRel = apiRel;}},
            {nameof(IRelationshipInfo.ApiCardinality), (jProperty, serializer, context) => {var apiCardinality = jProperty.ReadEnumPropertyValue<RelationshipCardinality>(); context.ApiCardinality = apiCardinality;}},
            {nameof(IRelationshipInfo.ClrRelatedResourceType), (jProperty, serializer, context) => {var clrToType = jProperty.ReadTypePropertyValue(); context.ClrToType = clrToType;}},
            {nameof(IRelationshipInfo.ClrRelatedResourcePropertyBinding), (jProperty, serializer, context) => {var clrToPropertyBinding = jProperty.ReadClrPropertyBindingPropertyValue(serializer, context.ClrObjectType); context.ClrToPropertyBinding = clrToPropertyBinding;}}
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, RelationshipsInfoContext>> RelationshipsInfoContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, RelationshipsInfoContext>>
        {
            {nameof(IRelationshipsInfo.ClrRelationshipsPropertyBinding), (jProperty, serializer, context) => {var clrRelationshipsPropertyBinding = jProperty.ReadClrPropertyBindingPropertyValue(serializer, context.ClrObjectType); context.ClrRelationshipsPropertyBinding = clrRelationshipsPropertyBinding;}},
            {nameof(IRelationshipsInfo.RelationshipInfoCollection), (jProperty, serializer, context) => {var relationshipInfoCollection = jProperty.ReadRelationshipInfoCollectionPropertyValue(serializer, context.ClrObjectType); context.RelationshipInfoCollection = relationshipInfoCollection;}},
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ResourceIdentityInfoContext>> ResourceIdentityInfoContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ResourceIdentityInfoContext>>
        {
            {nameof(IResourceIdentityInfo.ApiType), (jProperty, serializer, context) => {var apiType = jProperty.ReadStringPropertyValue(); context.ApiType = apiType;}},
            {nameof(IResourceIdentityInfo.ClrIdPropertyBinding), (jProperty, serializer, context) => {var clrIdPropertyBinding = jProperty.ReadClrPropertyBindingPropertyValue(serializer, context.ClrObjectType); context.ClrIdPropertyBinding = clrIdPropertyBinding;}},
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ResourceTypeContext>> ResourceTypeContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ResourceTypeContext>>
        {
            {nameof(IResourceType.ResourceIdentityInfo), (jProperty, serializer, context) => {var resourceIdentityInfo = jProperty.ReadResourceIdentityInfoPropertyValue(serializer, context.ClrType); context.ResourceIdentityInfo = resourceIdentityInfo;}},
            {nameof(IResourceType.RelationshipsInfo), (jProperty, serializer, context) => {var relationshipsInfo = jProperty.ReadRelationshipsInfoPropertyValue(serializer, context.ClrType); context.RelationshipsInfo = relationshipsInfo;}},
            {nameof(IResourceType.LinksInfo), (jProperty, serializer, context) => {var linksInfo = jProperty.ReadLinksInfoPropertyValue(serializer, context.ClrType); context.LinksInfo = linksInfo;}},
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, TypeBaseContext>> TypeBaseContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, TypeBaseContext>>
        {
            {nameof(ITypeBase.ClrType), (jProperty, serializer, context) => {var clrType = jProperty.ReadTypePropertyValue(); context.ClrType = clrType;}},
            {nameof(ITypeBase.AttributesInfo), (jProperty, serializer, context) => {var attributesInfo = jProperty.ReadAttributesInfoPropertyValue(serializer, context.ClrType); context.AttributesInfo = attributesInfo;}},
        };

        private static readonly IReadOnlyDictionary<string, Action<JProperty, JsonSerializer, ServiceModelContext>> ServiceModelContextInitializerDictionary = new Dictionary<string, Action<JProperty, JsonSerializer, ServiceModelContext>>
        {
            {nameof(IServiceModel.Name), (jProperty, serializer, context) => {var name = jProperty.ReadStringPropertyValue(); context.Name = name;}},
            {nameof(IServiceModel.ComplexTypes), (jProperty, serializer, context) => {var complexTypes = jProperty.ReadComplexTypesPropertyValue(serializer); context.ComplexTypes = complexTypes;}},
            {nameof(IServiceModel.ResourceTypes), (jProperty, serializer, context) => {var complexTypes = jProperty.ReadResourceTypesPropertyValue(serializer); context.ResourceTypes = complexTypes;}},
        };
        #endregion

        // PRIVATE TYPES ////////////////////////////////////////////////////
        #region Types
        private class AttributesInfoContext
        {
            public Type ClrObjectType { get; set; }
            public IEnumerable<IAttributeInfo> AttributeInfoCollection { get; set; }
        }

        private class AttributeInfoContext
        {
            public Type ClrObjectType { get; set; }
            public string ApiAttributeName { get; set; }
            public IClrPropertyBinding ClrPropertyBinding { get; set; }
        }

        private class ClrPropertyBindingContext
        {
            public string ClrPropertyName { get; set; }
            public Type ClrPropertyType { get; set; }
        }

        private class ComplexTypeContext : TypeBaseContext
        { }

        private class LinksInfoContext
        {
            public Type ClrObjectType { get; set; }
            public IClrPropertyBinding ClrLinksPropertyBinding { get; set; }
        }

        private class RelationshipInfoContext
        {
            public Type ClrObjectType { get; set; }
            public string ApiRel { get; set; }
            public RelationshipCardinality? ApiCardinality { get; set; }
            public Type ClrToType { get; set; }
            public IClrPropertyBinding ClrToPropertyBinding { get; set; }
        }

        private class RelationshipsInfoContext
        {
            public Type ClrObjectType { get; set; }
            public IClrPropertyBinding ClrRelationshipsPropertyBinding { get; set; }
            public IEnumerable<IRelationshipInfo> RelationshipInfoCollection { get; set; }
        }

        private class ResourceIdentityInfoContext
        {
            public Type ClrObjectType { get; set; }
            public string ApiType { get; set; }
            public IClrPropertyBinding ClrIdPropertyBinding { get; set; }
        }

        private class ResourceTypeContext : TypeBaseContext
        {
            public IResourceIdentityInfo ResourceIdentityInfo { get; set; }
            public IRelationshipsInfo RelationshipsInfo { get; set; }
            public ILinksInfo LinksInfo { get; set; }
        }

        private class ServiceModelContext
        {
            public string Name { get; set; }
            public IEnumerable<IComplexType> ComplexTypes { get; set; }
            public IEnumerable<IResourceType> ResourceTypes { get; set; }
        }

        private class TypeBaseContext
        {
            public Type ClrType { get; set; }
            public IAttributesInfo AttributesInfo { get; set; }
        }
        #endregion
    }
}
