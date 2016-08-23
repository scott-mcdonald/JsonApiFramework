// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using JsonApiFramework.Internal;
using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Conventions;

namespace JsonApiFramework
{
    /// <summary>
    /// Extension methods to build a "core" document context extension object.
    /// </summary>
    public static class CoreDocumentContextExtensionBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static IDocumentContextOptionsBuilder UseConventionSet(this IDocumentContextOptionsBuilder documentContextOptionsBuilder, ConventionSet conventionSet)
        {
            Contract.Requires(documentContextOptionsBuilder != null);
            Contract.Requires(conventionSet != null);

            documentContextOptionsBuilder.ModifyExtension<CoreDocumentContextExtension>(x => x.ConventionSet = conventionSet);
            return documentContextOptionsBuilder;
        }

        public static IDocumentContextOptionsBuilder UseServiceModel(this IDocumentContextOptionsBuilder documentContextOptionsBuilder, IServiceModel serviceModel)
        {
            Contract.Requires(documentContextOptionsBuilder != null);
            Contract.Requires(serviceModel != null);

            documentContextOptionsBuilder.ModifyExtension<CoreDocumentContextExtension>(x => x.ServiceModel = serviceModel);
            return documentContextOptionsBuilder;
        }
        #endregion
    }
}