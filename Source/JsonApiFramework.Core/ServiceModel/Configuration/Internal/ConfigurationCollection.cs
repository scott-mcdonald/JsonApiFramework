// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System.Diagnostics.Contracts;

namespace JsonApiFramework.ServiceModel.Configuration.Internal;

internal class ConfigurationCollection : IConfigurationCollection
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region IConfigurationCollection Implementation
    public void Add(IComplexTypeBuilder complexTypeBuilder)
    {
        Contract.Requires(complexTypeBuilder != null);

        var clrComplexType = complexTypeBuilder!.ClrType;
        this.ComplexTypeBuilderDictionary.Add(clrComplexType, complexTypeBuilder);
    }

    public void Add(IResourceTypeBuilder resourceTypeBuilder)
    {
        Contract.Requires(resourceTypeBuilder != null);

        var clrResourceType = resourceTypeBuilder!.ClrType;
        this.ResourceTypeBuilderDictionary.Add(clrResourceType, resourceTypeBuilder);
    }
    #endregion

    // INTERNAL METHODS /////////////////////////////////////////////////
    #region Internal Methods
    internal IComplexTypeBuilder GetOrAddComplexTypeBuilder<TComplex>()
    {
        var clrComplexType = typeof(TComplex);

        if (this.ComplexTypeBuilderDictionary.TryGetValue(clrComplexType, out object? complexTypeBuilder))
        {
            return (IComplexTypeBuilder)complexTypeBuilder;
        }

        complexTypeBuilder = new ComplexTypeBuilder<TComplex>();
        this.ComplexTypeBuilderDictionary.Add(clrComplexType, complexTypeBuilder);
        return (IComplexTypeBuilder)complexTypeBuilder;
    }

    internal IResourceTypeBuilder<TResource> GetOrAddResourceTypeBuilder<TResource>()
        where TResource : class
    {
        var clrResourceType = typeof(TResource);

        if (this.ResourceTypeBuilderDictionary.TryGetValue(clrResourceType, out object? resourceTypeBuilder))
        {
            return (IResourceTypeBuilder<TResource>)resourceTypeBuilder;
        }

        resourceTypeBuilder = new ResourceTypeBuilder<TResource>();
        this.ResourceTypeBuilderDictionary.Add(clrResourceType, resourceTypeBuilder);
        return (IResourceTypeBuilder<TResource>)resourceTypeBuilder;
    }

    internal IEnumerable<IComplexTypeFactory> GetComplexTypeFactoryCollection()
    {
        var complexTypeConfigurationCollection = this.ComplexTypeBuilderDictionary
                                                     .Values
                                                     .ToList();

        var complexTypeFactoryCollection = complexTypeConfigurationCollection.Cast<IComplexTypeFactory>()
                                                                             .ToList();
        return complexTypeFactoryCollection;
    }

    internal IEnumerable<IResourceTypeFactory> GetResourceTypeFactoryCollection()
    {
        var resourceTypeConfigurationCollection = this.ResourceTypeBuilderDictionary
                                                      .Values
                                                      .ToList();

        var resourceTypeFactoryCollection = resourceTypeConfigurationCollection.Cast<IResourceTypeFactory>()
                                                                               .ToList();
        return resourceTypeFactoryCollection;
    }

    internal void AddHomeResourceType(Type clrHomeResourceType)
    {
        this.ClrHomeResourceTypeCollection.Add(clrHomeResourceType);
    }

    internal IEnumerable<Type> GetHomeResourceTypes() { return this.ClrHomeResourceTypeCollection; }

    // internal Type GetHomeResourceType() { return this.ClrHomeResourceType; }

    // internal void SetHomeResourceType(Type clrHomeResourceType) { this.ClrHomeResourceType = clrHomeResourceType; }
    #endregion

    // PRIVATE PROPERTIES ///////////////////////////////////////////////
    #region Properties
    private Dictionary<Type, object> ComplexTypeBuilderDictionary { get; } = [];
    private Dictionary<Type, object> ResourceTypeBuilderDictionary { get; } = [];
    private List<Type> ClrHomeResourceTypeCollection { get; } = [];

    // private Type ClrHomeResourceType { get; set; }
    #endregion
}