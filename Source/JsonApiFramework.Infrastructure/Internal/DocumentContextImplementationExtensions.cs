// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Conventions;

namespace JsonApiFramework.Internal
{
    internal static class DocumentContextImplementationExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static ConventionSet GetConventionSet(this IDocumentContextImplementation implementation)
        {
            Contract.Requires(implementation != null);

            var coreExtension = implementation.Options.GetExtension<CoreDocumentContextExtension>();
            var conventionSet = coreExtension.ConventionSet;
            return conventionSet;
        }

        public static IServiceModel GetServiceModel(this IDocumentContextImplementation implementation)
        {
            Contract.Requires(implementation != null);

            var coreExtension = implementation.Options.GetExtension<CoreDocumentContextExtension>();
            var serviceModel = coreExtension.ServiceModel;
            return serviceModel;
        }

        public static void SetConventionSet(this IDocumentContextImplementation implementation, ConventionSet conventionSet)
        {
            Contract.Requires(implementation != null);
            Contract.Requires(conventionSet != null);

            implementation.Options.ModifyExtension<CoreDocumentContextExtension>(x => x.ConventionSet = conventionSet);
        }

        public static void SetServiceModel(this IDocumentContextImplementation implementation, IServiceModel serviceModel)
        {
            Contract.Requires(implementation != null);
            Contract.Requires(serviceModel != null);

            implementation.Options.ModifyExtension<CoreDocumentContextExtension>(x => x.ServiceModel = serviceModel);
        }
        #endregion
    }
}