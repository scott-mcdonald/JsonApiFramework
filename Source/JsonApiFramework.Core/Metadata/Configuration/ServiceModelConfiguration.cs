// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using JsonApiFramework.Metadata.Conventions;
using JsonApiFramework.Metadata.Internal;

namespace JsonApiFramework.Metadata.Configuration
{
    /// <summary>
    /// Represents the configuration to build and create a service model from a collection of resource type and complex type configurations.
    /// </summary>
    /// <remarks>
    /// Both complex and resource type configurations will typically be added to the service model configuration in the following ways:
    /// <list type="bullet">
    /// <item><description>
    /// Call the <c>ComplexType</c> or <c>ResourceType</c> methods and directly building the respective resource type or complex type objects.
    /// </description></item>
    /// <item><description>
    /// Call the <c>Add</c> methods and pass as a parameter derived instances of <c>ComplexTypeConfiguration</c> or <c>ResourceTypeConfiguration</c>
    /// objects that in the constructor build the respective resource type or complex type objects.
    /// </description></item>
    /// <item><description>
    /// By conventions that discover resource or complex types from assemblies.
    /// </description></item>
    /// </list>
    /// </remarks>
    public class ServiceModelConfiguration : IServiceModelBuilder, IServiceModelFactory, IConfigurationCollection
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IServiceModelBuilder Implementation
        public void SetName(string name)
        { this.Name = name; }

        public IComplexTypeBuilder<TComplex> ComplexType<TComplex>()
        {
            var complexTypeConfiguration = this.GetOrAddComplexTypeConfiguration<TComplex>();
            return complexTypeConfiguration;
        }

        public IResourceTypeBuilder<TResource> ResourceType<TResource>()
        {
            var resourceTypeConfiguration = this.GetOrAddResourceTypeConfiguration<TResource>();
            return resourceTypeConfiguration;
        }
        #endregion

        #region IServiceModelFactory Implementation
        public IServiceModel Create(IMetadataConventions metadataConventions)
        {
            var name = this.Name;
            var complexTypes = this.CreateComplexTypes(metadataConventions);
            var resourceTypes = this.CreateResourceTypes(metadataConventions);

            var serviceModel = new ServiceModel(name, complexTypes, resourceTypes);
            return serviceModel;
        }
        #endregion

        #region IConfigurationCollection Implementation
        public void Add<TComplex>(ComplexTypeConfiguration<TComplex> complexTypeConfiguration)
        {
            Contract.Requires(complexTypeConfiguration != null);

            var clrComplexType = typeof(TComplex);
            this.ComplexTypeConfigurationDictionary.Add(clrComplexType, complexTypeConfiguration);
        }

        public void Add<TResource>(ResourceTypeConfiguration<TResource> resourceTypeConfiguration)
        {
            Contract.Requires(resourceTypeConfiguration != null);

            var clrResourceType = typeof(TResource);
            this.ResourceTypeConfigurationDictionary.Add(clrResourceType, resourceTypeConfiguration);
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private string Name { get; set; }
        private IDictionary<Type, object> ComplexTypeConfigurationDictionary { get; } = new Dictionary<Type, object>();
        private IDictionary<Type, object> ResourceTypeConfigurationDictionary { get; } = new Dictionary<Type, object>();
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private IEnumerable<IComplexType> CreateComplexTypes(IMetadataConventions metadataConventions)
        {
            var complexTypeFactoryCollection = this.ComplexTypeConfigurationDictionary
                                                   .Values
                                                   .Cast<IComplexTypeFactory>()
                                                   .ToList();

            var complexTypes = complexTypeFactoryCollection.Select(x => x.Create(metadataConventions))
                                                           .ToList();
            return complexTypes;
        }

        private IEnumerable<IResourceType> CreateResourceTypes(IMetadataConventions metadataConventions)
        {
            var resourceTypeFactoryCollection = this.ResourceTypeConfigurationDictionary
                                                    .Values
                                                    .Cast<IResourceTypeFactory>()
                                                    .ToList();

            var resourceTypes = resourceTypeFactoryCollection.Select(x => x.Create(metadataConventions))
                                                             .ToList();
            return resourceTypes;
        }

        private ComplexTypeConfiguration<TComplex> GetOrAddComplexTypeConfiguration<TComplex>()
        {
            var clrComplexType = typeof(TComplex);
            if (this.ComplexTypeConfigurationDictionary.TryGetValue(clrComplexType, out var complexTypeConfigurationAsObject))
            {
                var complexTypeConfigurationExisting = (ComplexTypeConfiguration<TComplex>)complexTypeConfigurationAsObject;
                return complexTypeConfigurationExisting;
            }

            var complexTypeConfigurationNew = new ComplexTypeConfiguration<TComplex>();
            this.Add(complexTypeConfigurationNew);
            return complexTypeConfigurationNew;
        }

        private ResourceTypeConfiguration<TResource> GetOrAddResourceTypeConfiguration<TResource>()
        {
            var clrResourceType = typeof(TResource);
            if (this.ResourceTypeConfigurationDictionary.TryGetValue(clrResourceType, out var resourceTypeConfigurationAsObject))
            {
                var resourceTypeConfigurationExisting = (ResourceTypeConfiguration<TResource>)resourceTypeConfigurationAsObject;
                return resourceTypeConfigurationExisting;
            }

            var resourceTypeConfigurationNew = new ResourceTypeConfiguration<TResource>();
            this.Add(resourceTypeConfigurationNew);
            return resourceTypeConfigurationNew;
        }
        #endregion
    }
}
