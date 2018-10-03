// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace JsonApiFramework.ServiceModel.Configuration.Internal
{
    internal class ConfigurationCollection : IConfigurationCollection
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IConfigurationCollection Implementation
        public void Add<TComplex>(ComplexTypeBuilder<TComplex> complexTypeBuilder)
        {
            Contract.Requires(complexTypeBuilder != null);

            this.ComplexTypeBuilderDictionary = this.ComplexTypeBuilderDictionary ?? new Dictionary<Type, object>();

            var clrComplexType = typeof(TComplex);
            this.ComplexTypeBuilderDictionary.Add(clrComplexType, complexTypeBuilder);
        }

        public void Add<TResource>(ResourceTypeBuilder<TResource> resourceTypeBuilder)
            where TResource : class, IResource
        {
            Contract.Requires(resourceTypeBuilder != null);

            this.ResourceTypeBuilderDictionary = this.ResourceTypeBuilderDictionary ?? new Dictionary<Type, object>();

            var clrResourceType = typeof(TResource);
            this.ResourceTypeBuilderDictionary.Add(clrResourceType, resourceTypeBuilder);
        }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region Internal Methods
        internal IComplexTypeBuilder GetOrAddComplexTypeBuilder<TComplex>()
        {
            this.ComplexTypeBuilderDictionary = this.ComplexTypeBuilderDictionary ?? new Dictionary<Type, object>();

            var clrComplexType = typeof(TComplex);

            object complexTypeBuilder;
            if (this.ComplexTypeBuilderDictionary.TryGetValue(clrComplexType, out complexTypeBuilder))
            {
                return (IComplexTypeBuilder)complexTypeBuilder;
            }

            complexTypeBuilder = new ComplexTypeBuilder<TComplex>();
            this.ComplexTypeBuilderDictionary.Add(clrComplexType, complexTypeBuilder);
            return (IComplexTypeBuilder)complexTypeBuilder;
        }

        internal IResourceTypeBuilder<TResource> GetOrAddResourceTypeBuilder<TResource>()
            where TResource : class, IResource
        {
            this.ResourceTypeBuilderDictionary = this.ResourceTypeBuilderDictionary ?? new Dictionary<Type, object>();

            var clrResourceType = typeof(TResource);

            object resourceTypeBuilder;
            if (this.ResourceTypeBuilderDictionary.TryGetValue(clrResourceType, out resourceTypeBuilder))
            {
                return (IResourceTypeBuilder<TResource>)resourceTypeBuilder;
            }

            resourceTypeBuilder = new ResourceTypeBuilder<TResource>();
            this.ResourceTypeBuilderDictionary.Add(clrResourceType, resourceTypeBuilder);
            return (IResourceTypeBuilder<TResource>)resourceTypeBuilder;
        }

        internal IEnumerable<IComplexTypeFactory> GetComplexTypeFactoryCollection()
        {
            this.ComplexTypeBuilderDictionary = this.ComplexTypeBuilderDictionary ?? new Dictionary<Type, object>();

            var complexTypeConfigurationCollection = this.ComplexTypeBuilderDictionary
                                                         .Values
                                                         .ToList();

            var complexTypeFactoryCollection = complexTypeConfigurationCollection.Cast<IComplexTypeFactory>()
                                                                                 .ToList();
            return complexTypeFactoryCollection;
        }

        internal IEnumerable<IResourceTypeFactory> GetResourceTypeFactoryCollection()
        {
            this.ResourceTypeBuilderDictionary = this.ResourceTypeBuilderDictionary ?? new Dictionary<Type, object>();

            var resourceTypeConfigurationCollection = this.ResourceTypeBuilderDictionary
                                                          .Values
                                                          .ToList();

            var resourceTypeFactoryCollection = resourceTypeConfigurationCollection.Cast<IResourceTypeFactory>()
                                                                                   .ToList();
            return resourceTypeFactoryCollection;
        }

        internal Type GetHomeResourceType() { return this.ClrHomeResourceType; }

        internal void SetHomeResourceType(Type clrHomeResourceType) { this.ClrHomeResourceType = clrHomeResourceType; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDictionary<Type, object> ComplexTypeBuilderDictionary { get; set; }
        private IDictionary<Type, object> ResourceTypeBuilderDictionary { get; set; }
        private Type ClrHomeResourceType { get; set; }
        #endregion
    }
}