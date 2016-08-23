// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using JsonApiFramework.ServiceModel;
using JsonApiFramework.ServiceModel.Conventions;

namespace JsonApiFramework.Internal
{
    /// <summary>Core document context extension. Internal use only</summary>
    internal class CoreDocumentContextExtension : IDocumentContextExtension
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Core conventions.</summary>
        public ConventionSet ConventionSet { get; set; }

        /// <summary>Core service model.</summary>
        public IServiceModel ServiceModel { get; set; }
        #endregion
    }
}