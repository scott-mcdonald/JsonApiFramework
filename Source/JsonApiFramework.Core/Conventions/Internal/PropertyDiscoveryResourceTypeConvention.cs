// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

using JsonApiFramework.JsonApi;
using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel.Configuration;

namespace JsonApiFramework.Conventions.Internal
{
    /// <summary>
    /// Discovery and configuration of resource type public properties by
    /// convention with the properties being one of the following:
    /// 1. Resource Identifier Property
    /// 2. Attribute Property
    /// 3. Relationships Property
    /// 4. Links Property
    /// 5. Meta Property
    /// </summary>
    internal class PropertyDiscoveryResourceTypeConvention : IResourceTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IResourceTypeConvention Implementation
        public IResourceTypeBuilder Apply(IResourceTypeBuilder resourceTypeConfiguration)
        {
            Contract.Requires(resourceTypeConfiguration != null);

            // Use reflection, get all the directly declard, public, and instance-based type of properties for the given resource type.
            var clrResourceType = resourceTypeConfiguration.ClrResourceType;
            var clrProperties = clrResourceType
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .ToList();

            var resourceIdentityProperty = DiscoverResourceIdentityProperty(clrResourceType, clrProperties);
            if (resourceIdentityProperty != null)
            {
                clrProperties.Remove(resourceIdentityProperty);
                ResourceIdentityPropertyDiscovered(resourceTypeConfiguration, resourceIdentityProperty);
            }

            var relationshipsProperty = DiscoverRelationshipsProperty(clrProperties);
            if (relationshipsProperty != null)
            {
                clrProperties.Remove(relationshipsProperty);
                RelationshipsPropertyDiscovered(resourceTypeConfiguration, relationshipsProperty);
            }

            var linksProperty = DiscoverLinksProperty(clrProperties);
            if (linksProperty != null)
            {
                clrProperties.Remove(linksProperty);
                LinksPropertyDiscovered(resourceTypeConfiguration, linksProperty);
            }

            var metaProperty = DiscoverMetaProperty(clrProperties);
            if (metaProperty != null)
            {
                clrProperties.Remove(metaProperty);
                MetaPropertyDiscovered(resourceTypeConfiguration, metaProperty);
            }

            // Remaining properties are attributes.
            var attributeProperties = DiscoverAttributeProperties(clrProperties);
            foreach (var attributeProperty in attributeProperties)
            {
                AttributePropertyDiscovered(resourceTypeConfiguration, attributeProperty);
            }

            return resourceTypeConfiguration;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static PropertyInfo DiscoverResourceIdentityProperty(Type clrResourceType, IReadOnlyCollection<PropertyInfo> clrProperties)
        {
            Contract.Requires(clrResourceType != null);
            Contract.Requires(clrProperties != null);

            // Try and discover the "primary key" of the resource type:

            // 1. By convention, any property named "Id" is the primary key.
            var idProperty = GetPropertyByName(clrProperties, "Id");
            if (idProperty != null)
            {
                // Found a property named "Id", by convention this is the primary key.
                return idProperty;
            }

            // 2. By convention, any property named "XXXId" where XXX is the class name is the primary key.
            var className = clrResourceType.Name;
            var classNameAndId = "{0}Id".FormatWith(className);
            var classNameAndIdProperty = GetPropertyByName(clrProperties, classNameAndId);
            if (classNameAndIdProperty != null)
            {
                // Found a property named "XXXId" where XXX is the class name, by convention this is the primary key.
                return classNameAndIdProperty;
            }

            // Could not discover the "primary key" of the resource type,
            // user will have to explicit set the primary key for this resource type.
            return null;
        }

        private static PropertyInfo DiscoverRelationshipsProperty(IEnumerable<PropertyInfo> clrProperties)
        {
            Contract.Requires(clrProperties != null);

            // Try and discover the "relationships" property of the resource type:

            // 1. By convention, any property named "Relationships" and of type "Relationships" is the "relationships" property.
            var relationshipsProperty = GetPropertyByNameAndType(clrProperties, "Relationships", typeof(Relationships));

            return relationshipsProperty;
        }

        private static PropertyInfo DiscoverLinksProperty(IEnumerable<PropertyInfo> clrProperties)
        {
            Contract.Requires(clrProperties != null);

            // Try and discover the "links" property of the resource type:

            // 1. By convention, any property named "Links" and of type "Links" is the "links" property.
            var linksProperty = GetPropertyByNameAndType(clrProperties, "Links", typeof(Links));

            return linksProperty;
        }

        private static PropertyInfo DiscoverMetaProperty(IEnumerable<PropertyInfo> clrProperties)
        {
            Contract.Requires(clrProperties != null);

            // Try and discover the "meta" property of the resource type:

            // 1. By convention, any property named "Meta" and of type "Meta" is the "meta" property.
            var metaProperty = GetPropertyByNameAndType(clrProperties, "Meta", typeof(Meta));

            return metaProperty;
        }

        private static IEnumerable<PropertyInfo> DiscoverAttributeProperties(IEnumerable<PropertyInfo> clrProperties)
        {
            Contract.Requires(clrProperties != null);

            // Try and discover the collection of "attribute" properties of the resource type:

            // 1. By convention, any remaining properties will be attributes.
            return clrProperties;
        }

        private static PropertyInfo GetPropertyByName(IEnumerable<PropertyInfo> clrProperties, string clrPropertyName)
        {
            var property = clrProperties.SingleOrDefault(x => String.Equals(x.Name, clrPropertyName, StringComparison.OrdinalIgnoreCase));
            return property;
        }

        private static PropertyInfo GetPropertyByNameAndType(IEnumerable<PropertyInfo> clrProperties, string clrPropertyName, Type clrPropertyType)
        {
            var property = clrProperties.SingleOrDefault(x => x.PropertyType == clrPropertyType && String.Equals(x.Name, clrPropertyName, StringComparison.OrdinalIgnoreCase));
            return property;
        }

        private static void ResourceIdentityPropertyDiscovered(IResourceTypeBuilder resourceTypeConfiguration, PropertyInfo resourceIdentityProperty)
        {
            Contract.Requires(resourceTypeConfiguration != null);
            Contract.Requires(resourceIdentityProperty != null);

            var clrPropertyName = resourceIdentityProperty.Name;
            var clrPropertyType = resourceIdentityProperty.PropertyType;

            resourceTypeConfiguration.ResourceIdentity(clrPropertyName, clrPropertyType);
        }

        private static void RelationshipsPropertyDiscovered(IResourceTypeBuilder resourceTypeConfiguration, PropertyInfo relationshipsProperty)
        {
            Contract.Requires(resourceTypeConfiguration != null);
            Contract.Requires(relationshipsProperty != null);

            var clrPropertyName = relationshipsProperty.Name;
            resourceTypeConfiguration.Relationships(clrPropertyName);
        }

        private static void LinksPropertyDiscovered(IResourceTypeBuilder resourceTypeConfiguration, PropertyInfo linksProperty)
        {
            Contract.Requires(resourceTypeConfiguration != null);
            Contract.Requires(linksProperty != null);

            var clrPropertyName = linksProperty.Name;
            resourceTypeConfiguration.Links(clrPropertyName);
        }

        private static void MetaPropertyDiscovered(IResourceTypeBuilder resourceTypeConfiguration, PropertyInfo metaProperty)
        {
            Contract.Requires(resourceTypeConfiguration != null);
            Contract.Requires(metaProperty != null);

            var clrPropertyName = metaProperty.Name;
            resourceTypeConfiguration.Meta(clrPropertyName);
        }

        private static void AttributePropertyDiscovered(IResourceTypeBuilder resourceTypeConfiguration, PropertyInfo attributeProperty)
        {
            Contract.Requires(resourceTypeConfiguration != null);
            Contract.Requires(attributeProperty != null);

            var clrPropertyName = attributeProperty.Name;
            var clrPropertyType = attributeProperty.PropertyType;

            resourceTypeConfiguration.Attribute(clrPropertyName, clrPropertyType);
        }
        #endregion
    }
}