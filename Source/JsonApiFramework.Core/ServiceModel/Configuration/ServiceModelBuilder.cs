// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.ServiceModel.Configuration.Internal;
using JsonApiFramework.ServiceModel.Conventions;

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
        public IResourceTypeBuilder<TResource> Resource<TResource>()
            where TResource : class, IResource
        {
            var resourceTypeConfiguration = _configurations.GetOrAddResourceTypeBuilder<TResource>();
            return resourceTypeConfiguration;
        }
        #endregion

        #region IServiceModelFactory Implementation
        public IServiceModel Create(ConventionSet conventionSet)
        {
            var resourceTypes = this.CreateResourceTypes(conventionSet);
            var serviceModel = new ServiceModel.Internal.ServiceModel(resourceTypes);
            return serviceModel;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private IEnumerable<IResourceType> CreateResourceTypes(ConventionSet conventionSet)
        {
            var resourceTypeFactoryCollection = _configurations.GetResourceTypeFactoryCollection();
            var resourceTypes = resourceTypeFactoryCollection
                .Select(x => x.Create(conventionSet))
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