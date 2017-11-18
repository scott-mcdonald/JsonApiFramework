// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using JsonApiFramework.Metadata.Internal;
using JsonApiFramework.Properties;

// ReSharper disable once CheckNamespace
namespace JsonApiFramework.Metadata
{
    /// <summary>Extension methods for the <see cref="IServiceModel"/> interface.</summary>
    public static class ServiceModelExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>
        /// Gets the complex type by CLR type parameter.
        /// 
        /// Throws an exception if the complex type does not exist in the service model.
        /// </summary>
        /// <param name="clrComplexType">The CLR type to lookup the complex type by. </param>
        /// <returns>The complex type in the service model.</returns>
        /// <exception cref="MetadataException">Thrown if the complex type does not exist in the service model.</exception>
#pragma warning disable 1573
        public static IComplexType GetComplexType(this IServiceModel serviceModel, Type clrComplexType)
#pragma warning restore 1573
        {
            Contract.Requires(serviceModel != null);
            Contract.Requires(clrComplexType != null);

            if (serviceModel.TryGetComplexType(clrComplexType, out var complexType))
            {
                return complexType;
            }

            var complexTypeDescription = "ComplexType [clrType={0}]".FormatWith(clrComplexType.Name);
            var detail = CoreErrorStrings.MetadataExceptionMissingMetadataDetail
                                         .FormatWith(nameof(ServiceModel), complexTypeDescription);
            throw new MetadataException(detail);
        }

        /// <summary>
        /// Gets the complex type by generic CLR typeparameter.
        /// 
        /// Throws an exception if the complex type does not exist in the service model.
        /// </summary>
        /// <typeparam name="T">CLR typeparameter to lookup the complex type by.</typeparam>
        /// <returns>The complex type in the service model.</returns>
        /// <exception cref="MetadataException">Thrown if the complex type does not exist in the service model.</exception>
#pragma warning disable 1573
        public static IComplexType GetComplexType<T>(this IServiceModel serviceModel)
#pragma warning restore 1573
        {
            Contract.Requires(serviceModel != null);

            var clrComplexType = typeof(T);
            return serviceModel.GetComplexType(clrComplexType);
        }

        /// <summary>
        /// Gets the resource type by CLR type parameter.
        /// 
        /// Throws an exception if the resource type does not exist in the service model.
        /// </summary>
        /// <param name="clrResourceType">The CLR type to lookup the resource type by. </param>
        /// <returns>The resource type in the service model.</returns>
        /// <exception cref="MetadataException">Thrown if the resource type does not exist in the service model.</exception>
#pragma warning disable 1573
        public static IResourceType GetResourceType(this IServiceModel serviceModel, Type clrResourceType)
#pragma warning restore 1573
        {
            Contract.Requires(serviceModel != null);

            if (serviceModel.TryGetResourceType(clrResourceType, out var resourceType))
            {
                return resourceType;
            }

            var resourceTypeDescription = "ResourceType [clrType={0}]".FormatWith(clrResourceType.Name);
            var detail = CoreErrorStrings.MetadataExceptionMissingMetadataDetail
                                         .FormatWith(nameof(ServiceModel), resourceTypeDescription);
            throw new MetadataException(detail);
        }

        /// <summary>
        /// Gets the resource type by generic CLR typeparameter.
        /// 
        /// Throws an exception if the resource type does not exist in the service model.
        /// </summary>
        /// <typeparam name="T">CLR typeparameter to lookup the resource type by.</typeparam>
        /// <returns>The resource type in the service model.</returns>
        /// <exception cref="MetadataException">Thrown if the resource type does not exist in the service model.</exception>
#pragma warning disable 1573
        public static IResourceType GetResourceType<T>(this IServiceModel serviceModel)
#pragma warning restore 1573
        {
            Contract.Requires(serviceModel != null);

            var clrResourceType = typeof(T);
            return serviceModel.GetResourceType(clrResourceType);
        }

        /// <summary>
        /// Try and get the complex type by generic CLR typeparameter.
        /// </summary>
        /// <typeparam name="T">CLR typeparameter to lookup the complex type by.</typeparam>
        /// <param name="complexType">The complex type if it exists in the service model, null otherwise.</param>
        /// <returns>True if complex type exists in the service model, False otherwise.</returns>
#pragma warning disable 1573
        public static bool TryGetComplexType<T>(this IServiceModel serviceModel, out IComplexType complexType)
#pragma warning restore 1573
        {
            Contract.Requires(serviceModel != null);

            var clrComplexType = typeof(T);
            return serviceModel.TryGetComplexType(clrComplexType, out complexType);
        }

        /// <summary>
        /// Try and get the resource type by generic CLR typeparameter.
        /// </summary>
        /// <typeparam name="T">CLR typeparameter to lookup the resource type by.</typeparam>
        /// <param name="resourceType">The resource type if it exists in the service model, null otherwise.</param>
        /// <returns>True if resource type exists in the service model, False otherwise.</returns>
#pragma warning disable 1573
        public static bool TryGetResourceType<T>(this IServiceModel serviceModel, out IResourceType resourceType)
#pragma warning restore 1573
        {
            Contract.Requires(serviceModel != null);

            var clrResourceType = typeof(T);
            return serviceModel.TryGetResourceType(clrResourceType, out resourceType);
        }
        #endregion
    }
}
