// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.Conventions;
using JsonApiFramework.ServiceModel.Configuration.Internal;

namespace JsonApiFramework.ServiceModel.Configuration
{
    public class ServiceModelBuilder : IServiceModelBuilder, IServiceModelFactory
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IServiceModelBuilder Implementation
        public IConfigurationCollection Configurations
        { get { return _configurations; } }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IServiceModelBuilder Implementation
        public IComplexTypeBuilder<TComplex> Complex<TComplex>()
        {
            var complexTypeConfiguration = _configurations.GetOrAddComplexTypeBuilder<TComplex>();
            return complexTypeConfiguration;
        }

        public IResourceTypeBuilder<TResource> Resource<TResource>()
            where TResource : class, IResource
        {
            var resourceTypeConfiguration = _configurations.GetOrAddResourceTypeBuilder<TResource>();
            return resourceTypeConfiguration;
        }
        #endregion

        #region IServiceModelFactory Implementation
        public IServiceModel Create(IConventions conventions)
        {
            var complexTypes = this.CreateComplexTypes(conventions);
            var resourceTypes = this.CreateResourceTypes(conventions);
            var serviceModel = new ServiceModel.Internal.ServiceModel(complexTypes, resourceTypes);
            return serviceModel;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private IEnumerable<IComplexType> CreateComplexTypes(IConventions conventions)
        {
            var complexTypeFactoryCollection = _configurations.GetComplexTypeFactoryCollection();
            var complexTypes = complexTypeFactoryCollection
                .Select(x => x.Create(conventions))
                .ToList();
            return complexTypes;
        }

        private IEnumerable<IResourceType> CreateResourceTypes(IConventions conventions)
        {
            var resourceTypeFactoryCollection = _configurations.GetResourceTypeFactoryCollection();
            var resourceTypes = resourceTypeFactoryCollection
                .Select(x => x.Create(conventions))
                .ToList();
            return resourceTypes;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private readonly ConfigurationCollection _configurations = new ConfigurationCollection();
        #endregion
    }
}