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
        public IConfigurationCollection Configurations => _configurations;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IServiceModelBuilder Implementation
        public IComplexTypeBuilder Complex<TComplex>()
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

        public void HomeResource<TResource>()
            where TResource : class, IResource
        {
            var clrHomeResourceType = typeof(TResource);
            _configurations.SetHomeResourceType(clrHomeResourceType);
        }
        #endregion

        #region IServiceModelFactory Implementation
        public IServiceModel Create(IConventions conventions)
        {
            var complexTypes = this.CreateComplexTypes(conventions);
            var resourceTypes = this.CreateResourceTypes(conventions);
            var homeResourceType = this.GetHomeResourceType(resourceTypes);

            var serviceModel = new ServiceModel.Internal.ServiceModel(complexTypes, resourceTypes, homeResourceType);
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

        private IReadOnlyCollection<IResourceType> CreateResourceTypes(IConventions conventions)
        {
            var resourceTypeFactoryCollection = _configurations.GetResourceTypeFactoryCollection();
            var resourceTypes = resourceTypeFactoryCollection
                .Select(x => x.Create(conventions))
                .ToList();
            return resourceTypes;
        }

        private IResourceType GetHomeResourceType(IEnumerable<IResourceType> resourceTypes)
        {
            var clrHomeResourceType = _configurations.GetHomeResourceType();
            if (clrHomeResourceType == null)
                return null;

            var homeResourceType = resourceTypes.EmptyIfNull()
                                                .FirstOrDefault(x => x.ClrType == clrHomeResourceType);
            return homeResourceType;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private readonly ConfigurationCollection _configurations = new ConfigurationCollection();
        #endregion
    }
}