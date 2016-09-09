// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

using JsonApiFramework.Reflection;
using JsonApiFramework.ServiceModel.Configuration;

namespace JsonApiFramework.Conventions.Internal
{
    /// <summary>
    /// Discovery and configuration of complex type public properties by
    /// convention with the properties being the following:
    /// 1. Attribute Property
    /// </summary>
    internal class PropertyDiscoveryComplexTypeConvention : IComplexTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IComplexTypeConvention Implementation
        public IComplexTypeBuilder Apply(IComplexTypeBuilder complexTypeConfiguration)
        {
            Contract.Requires(complexTypeConfiguration != null);

            // Use reflection, get all the directly declard, public, and instance-based type of properties for the given resource type.
            var clrComplexType = complexTypeConfiguration.ClrComplexType;
            var clrProperties = clrComplexType
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .ToList();

            // Remaining properties are attributes.
            var attributeProperties = DiscoverAttributeProperties(clrProperties);
            foreach (var attributeProperty in attributeProperties)
            {
                AttributePropertyDiscovered(complexTypeConfiguration, attributeProperty);
            }

            return complexTypeConfiguration;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static IEnumerable<PropertyInfo> DiscoverAttributeProperties(IEnumerable<PropertyInfo> clrProperties)
        {
            Contract.Requires(clrProperties != null);

            // Try and discover the collection of "attribute" properties of the complex type:

            // 1. By convention, any remaining properties will be attributes.
            return clrProperties;
        }

        private static void AttributePropertyDiscovered(IComplexTypeBuilder complexTypeConfiguration, PropertyInfo attributeProperty)
        {
            Contract.Requires(complexTypeConfiguration != null);
            Contract.Requires(attributeProperty != null);

            var clrPropertyName = attributeProperty.Name;
            var clrPropertyType = attributeProperty.PropertyType;

            complexTypeConfiguration.Attribute(clrPropertyName, clrPropertyType);
        }
        #endregion
    }
}