// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.Conventions;
using JsonApiFramework.ServiceModel.Configuration.Internal;

namespace JsonApiFramework.ServiceModel.Configuration;

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
        where TResource : class
    {
        var resourceTypeConfiguration = _configurations.GetOrAddResourceTypeBuilder<TResource>();
        return resourceTypeConfiguration;
    }

    public void HomeResource<TResource>()
        where TResource : class
    {
        var clrHomeResourceType = typeof(TResource);
        _configurations.AddHomeResourceType(clrHomeResourceType);
    }
    #endregion

    #region IServiceModelFactory Implementation
    public IServiceModel Create(IConventions conventions)
    {
        var complexTypes = this.CreateComplexTypes(conventions);
        var resourceTypes = this.CreateResourceTypes(conventions);
        var homeResourceTypes = this.GetHomeResourceTypes(resourceTypes);

        var serviceModel = new ServiceModel.Internal.ServiceModel(complexTypes, resourceTypes, homeResourceTypes);
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

    private IReadOnlyCollection<IResourceType> GetHomeResourceTypes(IEnumerable<IResourceType> apiResourceTypes)
    {
        var clrHomeResourceTypesFromConfiguration = _configurations.GetHomeResourceTypes().ToList();

        var clrHomeResourceTypesFromConvention = apiResourceTypes
            .EmptyIfNull()
            .Where(x => string.IsNullOrEmpty(x.HypermediaInfo.ApiCollectionPathSegment))
            .Select(x => x.ClrType)
            .ToList();

        if (clrHomeResourceTypesFromConfiguration.Count == 0 && clrHomeResourceTypesFromConvention.Count == 0)
        {
            return new List<IResourceType>();
        }

        var clrHomeResourceTypesComposite = new List<Type>();
        clrHomeResourceTypesComposite.AddRange(clrHomeResourceTypesFromConfiguration);
        clrHomeResourceTypesComposite.AddRange(clrHomeResourceTypesFromConvention);

        var clrHomeResourceTypes = clrHomeResourceTypesComposite.Distinct();

        var apiResourceTypesDictionary = apiResourceTypes
            .EmptyIfNull()
            .ToDictionary(x => x.ClrType);

        var apiHomeResourceTypes = clrHomeResourceTypes
            .Select(x =>
            {
                var apiHomeResourceType = apiResourceTypesDictionary[x];
                return apiHomeResourceType;
            })
            .ToList();

        return apiHomeResourceTypes;
    }
    #endregion

    // PRIVATE FIELDS ///////////////////////////////////////////////////
    #region Fields
    private readonly ConfigurationCollection _configurations = new();
    #endregion
}