// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Conventions;
using JsonApiFramework.ServiceModel;

namespace JsonApiFramework.Internal
{
    internal static class DocumentContextImplementationExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IConventions GetConventions(this IDocumentContextImplementation implementation)
        {
            Contract.Requires(implementation != null);

            var coreExtension = implementation.Options.GetExtension<CoreDocumentContextExtension>();
            var conventions = coreExtension.Conventions;
            return conventions;
        }

        public static IServiceModel GetServiceModel(this IDocumentContextImplementation implementation)
        {
            Contract.Requires(implementation != null);

            var coreExtension = implementation.Options.GetExtension<CoreDocumentContextExtension>();
            var serviceModel = coreExtension.ServiceModel;
            return serviceModel;
        }

        public static void SetConventions(this IDocumentContextImplementation implementation, IConventions conventions)
        {
            Contract.Requires(implementation != null);
            Contract.Requires(conventions != null);

            implementation.Options.ModifyExtension<CoreDocumentContextExtension>(x => x.Conventions = conventions);
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