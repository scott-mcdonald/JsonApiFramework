// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json.Serialization.Metadata;

namespace JsonApiFramework.ServiceModel;

public static class ServiceModelExtensions
{
    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region IServiceModel Extensions Methods
    public static IJsonTypeInfoResolver CreateComplexTypesJsonTypeInfoResolver(this IServiceModel serviceModel)
    {
        Contract.Requires(serviceModel != null);

        return serviceModel!.ComplexTypes.Any()
            ? new ComplexTypesJsonTypeInfoResolver(serviceModel)
            : null!;
    }

    public static IResourceType GetResourceType<TResource>(this IServiceModel serviceModel)
    {
        Contract.Requires(serviceModel != null);

        return serviceModel!.GetResourceType(typeof(TResource));
    }

    public static bool IsHomeResourceType(this IServiceModel serviceModel, IResourceType resourceType)
    {
        Contract.Requires(serviceModel != null);

        var homeResourceTypes = serviceModel!.HomeResourceTypes;
        if (homeResourceTypes == null)
            return false;

        var homeResourceTypeMatch = homeResourceTypes.SingleOrDefault(x => x.ClrType == resourceType.ClrType);
        return homeResourceTypeMatch != null;
    }

    public static bool TryGetResourceType<TResource>(this IServiceModel serviceModel, out IResourceType resourceType)
    {
        Contract.Requires(serviceModel != null);

        return serviceModel!.TryGetResourceType(typeof(TResource), out resourceType);
    }

    public static bool TryGetHomeResourceType(this IServiceModel serviceModel, out IResourceType homeResourceType)
    {
        Contract.Requires(serviceModel != null);

        homeResourceType = default!;

        var homeResourceTypes = serviceModel?.HomeResourceTypes;
        if (homeResourceTypes == null)
            return false;

        if (homeResourceTypes.Count() != 1)
            return false;

        homeResourceType = homeResourceTypes.Single();
        return true;
    }
    #endregion
}
